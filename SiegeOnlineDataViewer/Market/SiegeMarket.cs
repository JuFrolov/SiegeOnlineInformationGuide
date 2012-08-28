using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using SiegeOnlineDataViewer.SiegeDataFiles;
using SiegeOnlineDataViewer.Utils;

namespace SiegeOnlineDataViewer.Market
{
	public static class SiegeMarket
	{
		public const string ConfigFile = "SiegeOnlineMarket.cfg";

		public static Dictionary<byte, double> DiamondPrice = new Dictionary<byte, double>();
		public static Dictionary<string, ItemMarketClass> Items = new Dictionary<string, ItemMarketClass>();

		private static List<string> OptimizedItemToCalc = new List<string>();

		private const byte priceBuy = (byte) PriceType.Buy;
		private const byte priceSell = (byte) PriceType.Sell;

		/// <summary>
		/// Инициализация данных рынка
		/// </summary>
		public static void InitMarket()
		{
			Items.Clear();
			
			if (!SystemInfo.SecurCode.CheckAccess("market"))
				return;

			if (AppConfig.Mode != AppConfig.ModeList.AllIncluded)
				return;

			// загружаем конфиг
			var config = new Config(Path.Combine(Environment.CurrentDirectory, ConfigFile));

			var list = SiegeDataBase.DataResoursePairs.Keys.Where(s => SiegeDataBase.DataBuildings.NeededResources.Contains(s) || s == "diamond");

			foreach (var key in list)
			{
				var res = new KeyValuePair<string, SiegeResourcePairsStructure>(key, SiegeDataBase.DataResoursePairs[key]);

				var val = new ItemMarketClass()
				          	{
                                Name = res.Key,
								FormulaCalc = res.Value.Resources != null && res.Value.Resources.Count > 0,
                                PackSize = {
									{priceBuy, 1}, 
									{priceSell, 1}
								}
				          	};

				// голды не выводим
				if (res.Key == "gold")
					continue;

                // расчёт по формуле вырубим из-за циклических ссылок
				if (val.Name == "ironore")
					val.FormulaCalcDisabled = true;

				int i;
				double d;

				// загрузка прошлого состояния
				string s;
                s = config.ReadValue(res.Key, "CalcFormula", "-");
				if (s != "-")
					bool.TryParse(s, out val.FormulaCalc);
				
				int.TryParse(config.ReadValue(res.Key, "BuyPack", "1"), out i);
				val.PackSize[priceBuy] = i > 0 ? i : 0;

				int.TryParse(config.ReadValue(res.Key, "SellPack", "1"), out i);
				val.PackSize[priceSell] = i > 0 ? i : 0;
				
				d = Functions.ToDouble(config.ReadValue(res.Key, "Buy", "0"));
				val.UserGold[priceBuy] = d > 0 ? d : 0;

				d = Functions.ToDouble(config.ReadValue(res.Key, "Sell", "0"));
				val.UserGold[priceSell] = d > 0 ? d : 0;

				// сохраняем стоимость бриллиантов
				if (res.Key == "diamond")
				{
					DiamondPrice[priceBuy] = val.UserGold[priceBuy];
					DiamondPrice[priceSell] = val.UserGold[priceSell];
				}
				/*else
				{
					val.UserDiamond[priceBuy] = DiamondPrice[priceBuy] != 0 ? val.UserGold[priceBuy] / DiamondPrice[priceBuy] : 0;
					val.UserDiamond[priceSell] = DiamondPrice[priceSell] != 0 ? val.UserGold[priceSell] / DiamondPrice[priceSell] : 0;
				}*/

				// сохраним ссылки на зависимые производства
				var keys = SiegeDataBase.DataResoursePairs.Where(pair => pair.Value.Resources != null && pair.Value.Resources.ContainsKey(res.Key));
				foreach (var link in keys)
				{
					val.CalcLink.Add(link.Key);
				}

				Items.Add(res.Key, val);
			}
		}

		/// <summary>
		/// Перерасчёт данных рынка
		/// </summary>
        public static void RecalcMarket()
		{
			if (Items.Count == 0)
				return;

			if (AppConfig.Mode != AppConfig.ModeList.AllIncluded)
				return;


			var parsedItems = new List<string>();

			var order = OptimizedItemToCalc.Count == 0 ? Items.Keys.ToList() : OptimizedItemToCalc;

			var work = Items;

			Dictionary<string, ItemMarketClass> needRecalc = null;

			while (needRecalc == null || needRecalc.Count > 0)
			{
				var keys = needRecalc != null ? needRecalc.Keys.ToList() : order;

				needRecalc = new Dictionary<string, ItemMarketClass>();

				for (var k = 0; k < keys.Count; k++)
				{
					if (!work.ContainsKey(keys[k]))
						continue;

					var key = keys[k];
					var value = work[keys[k]];
				
					// пропускаем левые ресурсы, не использующиеся в производстве
					if (!SiegeDataBase.DataBuildings.NeededResources.Contains(key))
						continue;

					if (!value.FormulaCalc || !value.FormulaCalcEnabled || value.FormulaCalcDisabled)
					{
						Items[key].CalcGold[priceBuy] = value.UserGold[priceBuy];
						Items[key].CalcGold[priceSell] = value.UserGold[priceSell];

						parsedItems.Add(key);
					}
					else
					{
						var parsedRes = true;

						// расчёт стоимости по формуле
						var calcSell = 0.0;
						var calcBuy = 0.0;

						var resources = SiegeDataBase.DataResoursePairs[key].Resources;
						foreach (var res in resources)
						{
							var id = res.Key.ToLower();

							// голды не учитываем - они и так есть
							if (id == "gold")
								continue;

							if (parsedItems.Contains(id))
							{
								calcBuy += Items[id].CalcGold[priceBuy] * res.Value;
								calcSell += Items[id].CalcGold[priceSell] * res.Value;
							}
							else
							{
								parsedRes = false;
								break;
							}
						}

						if (parsedRes)
						{
							Items[key].CalcGold[priceBuy] = calcBuy;
							Items[key].CalcGold[priceSell] = calcSell;

							parsedItems.Add(key);
                        }
						else
						{
							needRecalc.Add(key, Items[key]);
						}
					}
				}

				work = needRecalc;
			}

			if (OptimizedItemToCalc.Count == 0)
				OptimizedItemToCalc = parsedItems;

		}


		public class ItemMarketClass
		{
			/// <summary>
			/// Код ресурса (позиции)
			/// </summary>
			public string Name;

			/// <summary>
			/// Количество ресурса в пачке (только для отображения!)
			/// </summary>
			public Dictionary<byte, double> PackSize = new Dictionary<byte, double>();

			/// <summary>
			/// Пользовательская цена ресурса в золоте (за единицу ресурса!!!)
			/// </summary>
			public Dictionary<byte, double> UserGold = new Dictionary<byte, double>();

			/// <summary>
			/// Пользовательская цена ресурса в бриллиантах (за единицу ресурса!!!)
			/// </summary>
			//public Dictionary<byte, double> UserDiamond = new Dictionary<byte, double>();

			/// <summary>
			/// Расчётная цена ресурса в золоте (за единицу ресурса!!!)
			/// </summary>
			public Dictionary<byte, double> CalcGold = new Dictionary<byte, double>();

			/// <summary>
			/// Расчётная цена ресурса в бриллиантах (за единицу ресурса!!!)
			/// </summary>
			//public Dictionary<byte, double> CalcDiamond = new Dictionary<byte, double>();

            /// <summary>
            /// Есть ли формула расчёта
            /// </summary>
			public bool FormulaCalc;

			/// <summary>
			/// Использовать ли расчёт по формуле, если она есть
			/// </summary>
			public bool FormulaCalcEnabled = true;

			/// <summary>
			/// Рубильник выключения воизбежание циклических ссылок
			/// </summary>
			public bool FormulaCalcDisabled;

			/// <summary>
			/// Линки на ресы, в которых используется этот
			/// </summary>
			public List<string> CalcLink = new List<string>();

		}

		/// <summary>
		/// Типы цен: покупка, продажа
		/// </summary>
		public enum PriceType : byte
		{
			Buy,
			Sell
		}
	}
}
