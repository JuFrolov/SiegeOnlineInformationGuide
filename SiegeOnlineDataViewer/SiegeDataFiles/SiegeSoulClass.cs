using System.Windows.Forms;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
	public class SiegeSoulClass : SiegeCommonAbstractDictionary<SiegeSoulStructure>
	{
		/// Конструктор
		public SiegeSoulClass(string file)
		{
            Files.Clear();
            Files.Add(file);
		}

		/// <summary>
		/// Обработка данных
		/// </summary>
		public override void NodeParse(SiegeDataBaseNode nodes, ToolStripProgressBar progressBar)
		{
			foreach (SiegeDataBaseNode node in nodes.GetChilds("soul"))
			{
				if (!node.Attributes.ContainsKey("name"))
					continue;

				var name = node.GetAttribute("Name");

				var data = new SiegeSoulStructure
				           	{
				           		HP = node.GetAttributeAsInt("HP"),
				           		Regeneration = node.GetAttributeAsInt("Regeneration"),
				           		DodgeChance = node.GetAttributeAsDouble("DodgeChance"),
				           		Resilience = node.GetAttributeAsDouble("Resilience"),
				           		HealOnDamageChance = node.GetAttributeAsDouble("HealOnDamageChance"),
				           		HealOnDamageValue = node.GetAttributeAsDouble("HealOnDamageValue"),
				           		ReflectChance = node.GetAttributeAsDouble("ReflectChance")
				           		//ArmorAbs = node.GetChild("armor").GetAttributeAsInt("Real")
				           	};

				SiegeDataBaseNode armor;
				if (node.GetChild("armor", out armor))
				{
					data.ArmorAbs = armor.GetAttributeAsInt("Abs");
				}

				//var code = name.ToLower();
				if (!ContainsKey(name))
					Add(name, data);
			}

		}
	}

	public class SiegeSoulStructure
	{
		/// <summary>
		/// Здоровье юнита
		/// </summary>
		public int HP;

		/// <summary>
		/// Броня
		/// </summary>
		public int ArmorAbs;

		/// <summary>
		/// Регенерация
		/// </summary>
		public int Regeneration;

		/// <summary>
		/// Шанс блокировать урон
		/// </summary>
		public double DodgeChance;		

		/// <summary>
		/// Уменьшает критшанс бьющего, по умолчанию 0
		/// </summary>
		public double Resilience;			

		/// <summary>
		/// Шанс отхилиться на ударе, по умолчанию 0
		/// </summary>
		public double HealOnDamageChance;

		/// <summary>
		/// Величина отхила на ударе, по умолчанию 10 проц. от HP
		/// </summary>
		public double HealOnDamageValue;	

		/// <summary>
		/// Шанс что дамаг отразится на нападающего, по умолчанию 0
		/// </summary>
		public double ReflectChance;		
	}
}