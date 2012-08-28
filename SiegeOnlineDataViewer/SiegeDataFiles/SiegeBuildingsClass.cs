using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SiegeOnlineDataViewer.Utils;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
    /// <summary>
    /// Класс описывающий строения.
    /// </summary>
	public class SiegeBuildingsClass : SiegeCommonAbstractDictionary<SiegeBuildingStructure>
	{
		public SiegeCommonDataList NeededResources = new SiegeCommonDataList();

		/// <summary>
        /// Конструктор по умолчанию.
		/// </summary>
		/// <param name="file"></param>
		public SiegeBuildingsClass(string file)
		{
            Files.Clear();
			Files.Add(file);
		}

		/// <summary>
		/// Обработка данных.
		/// </summary>
		public override void NodeParse(SiegeDataBaseNode nodes, ToolStripProgressBar progressBar)
		{
			foreach (SiegeDataBaseNode node in nodes.GetChilds("Building"))
			{
				if (!node.Attributes.ContainsKey("Name"))
					continue;

				var data = new SiegeBuildingStructure
				           	{
				           		ClientObjectRef = node.GetAttribute("ClientObjectRef"),
				           		Code = node.GetAttribute("Name")
				           	};
                string translate;

				if (SiegeDataBase.DataLanguages.Get(data.Code, out translate))
					data.Name = translate;

				// если перевода по имени нет, и есть ссылка на клиент-реф, то берём перевод по ней
				string newCode;

				if (string.IsNullOrEmpty(data.Name) && data.ClientObjectRef != null && SiegeDataBase.DataLanguages.Get(data.ClientObjectRef, out newCode))
					data.Name = newCode;

				// если всё равно имени нет, то пишем код
				/*if (string.IsNullOrEmpty(data.Name))
					data.Name = "? " + data.Code;*/

				// сохраним размеры
				string size = node.GetAttribute("Size");
				string[] arr = size.Split(';');

				if (arr.Length == 2)
				{
					short d;

					if (short.TryParse(arr[0].Trim(), out d))
						data.SizeA = d;

					if (short.TryParse(arr[1].Trim(), out d))
						data.SizeB = d;
				}

				// сохраним требования к постройке
				data.Requirements = node.GetAttributes("Requirement");
                
				// сохраним инфу по строительству здания
				SiegeDataBaseNode cost = node.GetChild("Cost");

				double timeCost;

				if (cost.GetAttribute("Time", out timeCost))
					data.BuildTime = timeCost;

				foreach (SiegeDataBaseNode child in cost.GetChilds("Fund"))
				{
					string id;
					int count;

					if (child.GetAttribute("ID", out id) && child.GetAttribute("Count", out count))
					{
						/*var item = new SiegeBuildingStructure.ResoursePair
						           	{
						           		ID = id,
						           		Count = count
						           	};*/

						data.BuildResources.Add(id, count);
					}
				}

				// сохраним инфу по производству
				string type;
				if (node.GetAttribute("Type", out type) && Functions.inList(type, "Training", "Production"))
				{
					data.Production = new SiegeBuildingStructure.ProductionClass();
					SiegeDataBaseNode consumption;

					if (node.GetChild("Consumption", out consumption))
					{
						double time;

						if (consumption.GetAttribute("Time", out time))
							data.Production.Time = time;

						List<SiegeDataBaseNode> funds = consumption.GetChilds("Fund");

						foreach (SiegeDataBaseNode fund in funds)
						{
							string id = fund.GetAttribute("ID");
							int count = fund.GetAttributeAsInt("Count");

							if (!NeededResources.Contains(id))
								NeededResources.Add(id);

							data.Production.Resources.Add(id, count);
						}
					}

					switch (type)
					{
						case "Training":
							// конкретный юнит
							data.Production.Type = SiegeBuildingStructure.ProductionType.Unit;

							string code;
							if (node.GetAttribute("TraineeSquad", out code))
							{
								data.Production.Products.Add(code, 1);
							}
							break;

						case "Production":
							// список продукций
							data.Production.Type = SiegeBuildingStructure.ProductionType.Artefact;

							SiegeDataBaseNode product;
							if (node.GetChild("Production", out product))
							{
								List<SiegeDataBaseNode> funds = product.GetChilds("Fund");
								foreach (SiegeDataBaseNode fund in funds)
								{
									string id = fund.GetAttribute("ID");
									int count = fund.GetAttributeAsInt("Count");
									data.Production.Products.Add(id, count);
								}
							}
							break;
					}
				}

				// сохраним инфу по апгрейду
				SiegeDataBaseNode upgrade;
				if (node.GetChild("UpgradeInfo", out upgrade))
				{
					data.UpgradeInfo = new SiegeBuildingStructure.UpgradeClass
					                   	{
					                   		MinLevel = upgrade.GetAttributeAsInt("MinPlayerLevel")
					                   	};

					SiegeDataBaseNode costUpgr = upgrade.GetChild("UpgradeCost");
					foreach (SiegeDataBaseNode fund in costUpgr.GetChilds("Fund"))
					{
						string id = fund.GetAttribute("ID");
						int count = fund.GetAttributeAsInt("Count");

						data.UpgradeInfo.Resources.Add(id, count);
					}

					foreach (SiegeDataBaseNode fund in cost.GetChilds("Fund"))
					{
						string id = fund.GetAttribute("ID");
						int count = fund.GetAttributeAsInt("Count");

						data.UpgradeInfo.Resources2.Add(id, count / 10.0 );
					}
				}

				// занесём в словарь
				string addCode = data.Code.ToLower();
				if (!ContainsKey(addCode))
					Add(addCode, data);
			}

			// обновим связи с родителями по линкам требований
			foreach (var list in this)
			{
				foreach (string requirement in list.Value.Requirements)
				{
					string[] req = requirement.Split(';');

					foreach (string s in req)
					{
						int pos = s.IndexOf(',');
						string building = pos > 0 ? Functions.SubString(s, 0, pos) : s;

						int count = 0;
						if (pos > 0)
							Int32.TryParse(Functions.SubString(s, pos + 1), out count);

						building = building.ToLower();
						if (ContainsKey(building))
							this[building].AfterBuild.Add(list.Key + (count > 1 ? ", " + count : ""));
					}
				}
			}

		}

		/// <summary>
		/// Найти здание по производимому войску
		/// </summary>
		/// <param name="str"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		public bool GetBySquad(string str, out SiegeBuildingStructure result)
		{
			result = null;

			foreach (var item in this)
			{
				if (item.Value.Production != null && item.Value.Production.Type == SiegeBuildingStructure.ProductionType.Unit
					&& item.Value.Production.Products.Count == 1 && item.Value.Production.Products.ContainsKey(str.ToLower()))
				{
					result = item.Value;
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Найти здание по производимому ресурсу/артефакту.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		public bool GetByProduction(string str, out SiegeBuildingStructure result)
		{
			result = null;

			foreach (var item in this)
			{
				if (item.Value.Production != null && item.Value.Production.Type == SiegeBuildingStructure.ProductionType.Artefact
					/*&& item.Value.Production.Products.Count == 1 && item.Value.Production.Products[0].ID.ToLower() == str.ToLower()*/)
				{
					foreach (var prod in item.Value.Production.Products)
					{
						if (prod.Key.ToLower() == str.ToLower() && prod.Value > 0)
						{
							result = item.Value;
							return true;
						}
					}
				}
			}

			return false;
		}
	}
    
    /// <summary>
    /// Класс описывающий структуру строений.
    /// </summary>
	public class SiegeBuildingStructure
	{
		#region ProductionType enum

        /// <summary>
        /// Типы продуктов которые производит строение.
        /// </summary>
		public enum ProductionType
		{
            /// <summary>
            /// Не производит.
            /// </summary>
			NotAvailable = 0,
            /// <summary>
            /// Артефакт.
            /// </summary>
			Artefact = 1,
            /// <summary>
            /// Юнит.
            /// </summary>
			Unit = 2
		}

		#endregion

		public List<string> AfterBuild = new List<string>();
		public SiegeDataBase.ResourseDictionary BuildResources = new SiegeDataBase.ResourseDictionary();
		public double BuildTime = -1;
		public string ClientObjectRef;

		/// <summary>
		/// Код здания.
		/// </summary>
		public string Code;

		/// <summary>
		/// Наименование здания.
		/// </summary>
		public string Name;

		public ProductionClass Production;
		public List<string> Requirements = new List<string>();

		public short SizeA = -1;
		public short SizeB = -1;

		public UpgradeClass UpgradeInfo;

		#region Nested type: ProductionClass

        /// <summary>
        /// Класс описывающий продукцию.
        /// </summary>
		public class ProductionClass
		{
			public SiegeDataBase.ResourseDictionary Products = new SiegeDataBase.ResourseDictionary();
			public SiegeDataBase.ResourseDictionary Resources = new SiegeDataBase.ResourseDictionary();
			public double Time;
			public ProductionType Type = ProductionType.NotAvailable;
		}

		#endregion

		#region Nested type: ResoursePair
		/*
		public class ResoursePair
		{
			public double Count;
			public string ID;

			public ResoursePair()
			{
			}

			public ResoursePair(string id, double count)
			{
				ID = id;
				Count = count;
			}
		}
		*/
		#endregion

		#region Nested type: UpgradeClass

        /// <summary>
        /// Класс описывающий апгрейды.
        /// </summary>
		public class UpgradeClass
		{
			public int MinLevel;
			public SiegeDataBase.ResourseDictionary Resources = new SiegeDataBase.ResourseDictionary();

			/// <summary>
			/// Стоимость апгрейда на 1 ур.
			/// </summary>
			public SiegeDataBase.ResourseDictionary Resources2 = new SiegeDataBase.ResourseDictionary();
		}

		#endregion
	}
}