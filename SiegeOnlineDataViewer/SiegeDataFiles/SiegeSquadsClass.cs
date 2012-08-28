using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SiegeOnlineDataViewer.Utils;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
	public class SiegeSquadsClass : SiegeCommonAbstractDictionary<SiegeSquadStructure>
	{
		private const string SquadConfigFile = "SiegeOnlineSquads.cfg";

		/// Конструктор
		public SiegeSquadsClass(string file)
		{
            Files.Clear();
            Files.Add(file);
		}

		/// <summary>
		/// Обработка данных
		/// </summary>
		public override void NodeParse(SiegeDataBaseNode nodes, ToolStripProgressBar progressBar)
		{
			foreach (SiegeDataBaseNode node in nodes.Childs["Squad"])
			{
				if (!node.Attributes.ContainsKey("Name"))
					continue;

				var name = node.GetAttribute("Name");

				var data = new SiegeSquadStructure
				           	{
				           		Name = name,
				           		UnitName = node.GetAttribute("UnitName"),
				           		Restorable = node.GetAttributeAsInt("Restorable"),
				           		RequiredInfo = new SiegeSquadStructure.RequiredClass
				           		               	{
				           		               		RequiredRank = node.GetAttributeAsInt("RequiredRank"),
				           		               		RequiredRate = node.GetAttributeAsInt("RequiredRate"),
				           		               		Reputation = node.GetAttributes("Reputation")
				           		               	},
				           		BattleInfo = new SiegeSquadStructure.BattleInfoClass
				           		             	{
				           		             		CoolDown = node.GetAttributeAsDouble("Tcooldown"),
				           		             		RemoveCommandPoints = node.GetAttributeAsDouble("RemoveCommandPoints"),
				           		             		CommandPointsRegenPrice = node.GetAttributeAsDouble("CommandPointsRegenPrice"),
				           		             		ChargeCount = node.GetAttributeAsDouble("ChargeCount")
				           		             	}
				           	};

				/*SiegeObjectDescriptionStructure desc;
				if (SiegeDataBase.DataObjectDescription.GetByServerSquad(name, out desc))
				{
					data.ObjectDescription = new SiegeSquadStructure.ObjectDescriptionClass
												{
													UnitName = desc.UnitName,
													ServerSquad = name
												};
					data.NameDescription = SiegeDataBase.DataLanguages.Get(desc.UnitName) ?? desc.UnitName;
				}*/

				// сохраним ссылку на данные линков
				if (SiegeDataBase.DataObjectDescription.GetByServerSquad(name, out data.ObjectDescription))
				{
					// определим наименование юнита
					data.NameDescription = SiegeDataBase.DataLanguages.Get(data.ObjectDescription.UnitName) ??
					                       data.ObjectDescription.UnitName;
				}

				SiegeObjectDescriptionStructure unitDesc;
				if (SiegeDataBase.DataObjectDescription.Get(data.UnitName, out unitDesc))
				{
					// определим характеристики сквада
					if (!string.IsNullOrEmpty(unitDesc.ServerSoul))
						SiegeDataBase.DataSoul.Get(unitDesc.ServerSoul, out data.SoulInfo);

					// определим скорость сквада
					if (!string.IsNullOrEmpty(unitDesc.ServerUnitMover))
						SiegeDataBase.DataUnitMover.Get(unitDesc.ServerUnitMover, out data.UnitMoverInfo);

					// определим интеллект сквада
					if (!string.IsNullOrEmpty(unitDesc.ServerUnitAI))
						SiegeDataBase.DataUnitAi.Get(unitDesc.ServerUnitAI, out data.UnitAiInfo);

					// определим орудие сквада
					if (!string.IsNullOrEmpty(unitDesc.ServerWeapon))
						SiegeDataBase.DataWeapon.Get(unitDesc.ServerWeapon, out data.UnitWeaponInfo);
				}

				// код берём по связке
				data.Code = data.ObjectDescription.UnitName;


				// информация по апгрейду
				foreach (var s in data.UnitUpgradeAttributes)
				{
					double d;
					if (node.GetAttribute(s, out d) && d != 0)
						data.UnitUpgradeInfo.Add(s, d);
				}

				// сохраним ссылку на родительское здание
				SiegeDataBase.DataBuildings.GetBySquad(data.Code, out data.ParentBuilding);

				// определим необходимые для постройки ресурсы
				if (data.ParentBuilding != null)
				{
					var prodAllTypes = data.ParentBuilding.Production.Products.Count;

					var prodMyCount = 0.0;
					var prodAllCount = 0.0;
					foreach (var prod in data.ParentBuilding.Production.Products)
					{
						if (prod.Key == data.Code.ToLower())
							prodMyCount += prod.Value;

						prodAllCount += prod.Value;
					}

					foreach (var res in data.ParentBuilding.Production.Resources)
					{
						data.RequiredRes.Add(res.Key, prodAllTypes > 0 && prodMyCount > 0 ? res.Value / prodAllTypes / prodMyCount : 0);
					}
				}

				var code = data.Code.ToLower();
				if (!ContainsKey(code))
					Add(code, data);
			}

		}

		/// <summary>
		/// Загрузить состояние апгрейда юнитов
		/// </summary>
		public void LoadUpgradeInfo()
		{
			var dir = Path.GetDirectoryName(Application.ExecutablePath);

			if (dir == null || !Directory.Exists(dir))
				return;

			var file = Path.Combine(dir, SquadConfigFile);

			if (!File.Exists(file))
				return;

			var config = new Config(file);

			foreach (var item in this.Where(item => item.Value.UnitUpgradeInfo.Count > 0))
			{
				double.TryParse(config.ReadValue(item.Key, "Level", "0"), out this[item.Key].UpgradeToLevelValue);
				double.TryParse(config.ReadValue(item.Key, "TryCounts", "0"), out this[item.Key].UpgradeToLevelInTry);
				double.TryParse(config.ReadValue(item.Key, "DiamondsInTry", "0"), out this[item.Key].UpgradeToLevelInDiamonds);
				bool.TryParse(config.ReadValue(item.Key, "OnlyLast", "true"), out this[item.Key].UpgradeToLevelOnlyLast);
			}
		}

		/// <summary>
		/// Сохранить состояние апгрейда юнитов
		/// </summary>
		public void SaveUpgradeInfo()
		{
			var dir = Path.GetDirectoryName(Application.ExecutablePath);

			if (dir == null || !Directory.Exists(dir))
				return;

			var file = Path.Combine(dir, SquadConfigFile);

			var config = new StringBuilder();

			foreach (var item in this.Where(item => item.Value.UnitUpgradeInfo.Count > 0))
			{
				config.AppendLine(string.Format("[{0}]", item.Key));
				config.AppendLine(string.Format("Level = {0}", Math.Round(item.Value.UpgradeToLevelValue, 2)));
				config.AppendLine(string.Format("TryCounts = {0}", Math.Round(item.Value.UpgradeToLevelInTry, 2)));
				config.AppendLine(string.Format("DiamondsInTry = {0}", Math.Round(item.Value.UpgradeToLevelInDiamonds, 2)));
				config.AppendLine(string.Format("OnlyLast = {0}", item.Value.UpgradeToLevelOnlyLast));
			}

			File.WriteAllText(file, config.ToString());
		}
	}
    
	public class SiegeSquadStructure
	{
		public string Code;					// уникальный код записи в словаре (опирается на код юнита)

		//----------------------------------

		public string NameDescription;		// Наименование юнита
		public string Name;					// код юнита (в файле - Name)
		public string UnitName;				//
		
		public SiegeObjectDescriptionStructure ObjectDescription;
        public SiegeBuildingStructure ParentBuilding;
		public SiegeSoulStructure SoulInfo;
		public SiegeUnitAIStructure UnitAiInfo;
		public SiegeUnitMoverStructure UnitMoverInfo;
		public SiegeWeaponStructure UnitWeaponInfo;

		public int Restorable;				// Лимитные войска

		public Dictionary<string, double> UnitUpgradeInfo = new Dictionary<string, double>();
		public BattleInfoClass BattleInfo = new BattleInfoClass();
		public RequiredClass RequiredInfo = new RequiredClass();

		public SiegeDataBase.ResourseDictionary RequiredRes = new SiegeDataBase.ResourseDictionary();


		/// <summary>
		/// Среднее количество попыток на апгрейд уровня
		/// </summary>
		public double UpgradeToLevelInTry = 0;
		
		/// <summary>
		/// Среднее количество бриллиантов на апгрейд уровня
		/// </summary>
		public double UpgradeToLevelInDiamonds = 0;

		/// <summary>
		/// Расчётный уровень апгрейда
		/// </summary>
		public double UpgradeToLevelValue = 0;

        /// <summary>
        /// Расчитывать только последний уровень
        /// </summary>
		public bool UpgradeToLevelOnlyLast = false;
		//==================================
		
		/*public class ObjectDescriptionClass
		{
			public string UnitName;			// код юнита
			public string ServerSquad;		// ссылка на Name в SquadDesc
		}*/

		public class BattleInfoClass
		{
			public double ChargeCount;
            public double CommandPointsRegenPrice;
			public double CoolDown;
			public int MaxEquipCount;
			public double RemoveCommandPoints;

			
		}

		public class RequiredClass
		{
			public int RequiredRank;			// Необходимый уровень
			public int RequiredRate;			// Необходимый Боевой Рейтинг

			/// <summary>
			/// Требуемая репутация ("OrderLand;player;Benefit_Level_1")
			/// </summary>
			public List<string> Reputation;
		}

		#region Nested type: UnitUpgradeInfoClass

		public readonly string[] UnitUpgradeAttributes = new[]
		                                                 	{
		                                                 		"UpgAdditionalTargetsCount",
		                                                 		"UpgArmorAbs",
		                                                 		"UpgCommandPointOut",
		                                                 		"UpgCommandPointRegen",
		                                                 		"UpgCooldownOut",
		                                                 		"UpgDamage",
		                                                 		"UpgDodgeChance",
		                                                 		"UpgFriendlyFireChance",
		                                                 		"UpgHealFromDamageChance",
		                                                 		"UpgHP",
		                                                 		"UpgMovingSpeed",
		                                                 		"UpgMovingSpeedProc",
		                                                 		"UpgRechargeWeapon",
		                                                 		"UpgReflectChance",
		                                                 		"UpgResilience",
		                                                 		"UpgSmartTargetingChance",
		                                                 		"UpgWeaponCritChance",
		                                                 		"UpgWeaponRangeMax",
		                                                 		"UpgWeaponRangeMin"
		                                                 	};
		#endregion
		
	}
}