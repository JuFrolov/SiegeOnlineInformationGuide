using System.Windows.Forms;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
	public class SiegeAoTDescClass : SiegeCommonAbstractDictionary<SiegeAoTStructure>
	{
		/// <summary>
        ///  Конструктор по умолчанию.
		/// </summary>
		public SiegeAoTDescClass(string file)
		{
            Files.Clear();
            Files.Add(file);
		}

		/// <summary>
		/// Обработка данных.
		/// </summary>
		public override void NodeParse(SiegeDataBaseNode nodes, ToolStripProgressBar progressBar)
		{
			foreach (SiegeDataBaseNode node in nodes.GetChilds("AoT"))
			{
				if (!node.Attributes.ContainsKey("Name"))
					continue;

				var name = node.GetAttribute("Name");
				var data = new SiegeAoTStructure
				           	{
				           		Name = name,
								Type = node.GetAttribute("Type"),
								Value = node.GetAttributeAsDouble("Value"),
								Period = node.GetAttributeAsDouble("Period"),
								LifeTime = node.GetAttributeAsDouble("LifeTime"),
				           		Stackable = node.GetAttributeAsBoolean("Stackable"),
								DamageRadius = node.GetChild("Explosion").GetAttributeAsDouble("Radius")
				           	};

				var damageCode = node.GetChild("Explosion").GetAttribute("Damage");
				/*SiegeDamageStructure damage;
				if (!string.IsNullOrEmpty(damage_code) && SiegeDataBase.DataDamage.Get(damage_code, out damage))
					data.Damage = damage;*/

				if (!string.IsNullOrEmpty(damageCode))
					data.DamageCode = damageCode;
				
				
				//var code = name.ToLower();
				if (!ContainsKey(name))
					Add(name, data);
			}
		}
	}

	public class SiegeAoTStructure
	{
		/// <summary>
		/// Код AoT
		/// </summary>
		public string Name;

		/// <summary>
		/// Тип AoT
		/// </summary>
		public string Type;

		/// <summary>
		/// Величина AoT
		/// </summary>
		public double Value;

		/// <summary>
		/// Период
		/// </summary>
		public double Period;

		/// <summary>
		/// Время жизни
		/// </summary>
		public double LifeTime;

		/// <summary>
		/// Кумулятивный или нет
		/// </summary>
		public bool Stackable;

		public SiegeDamageStructure Damage;
		public string DamageCode;

		public double DamageRadius;
	}
}