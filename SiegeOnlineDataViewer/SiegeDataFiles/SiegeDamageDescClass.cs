using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
	public class SiegeDamageDescClass : SiegeCommonAbstractDictionary<SiegeDamageStructure>
	{
		/// <summary>
        /// Конструктор по умолчаню.
		/// </summary>
		public SiegeDamageDescClass(string file)
		{
            Files.Clear();
            Files.Add(file);
		}

		/// <summary>
		/// Обработка данных.
		/// </summary>
		public override void NodeParse(SiegeDataBaseNode nodes, ToolStripProgressBar progressBar)
		{
			foreach (SiegeDataBaseNode node in nodes.GetChilds("Damage"))
			{
				if (!node.Attributes.ContainsKey("Name"))
					continue;

				var name = node.GetAttribute("Name");
				var data = new SiegeDamageStructure
				           	{
				           		Name = name,
				           		Type = node.GetAttribute("Type"),
								HpMin = node.GetAttributeAsInt("HPMin"),
								HpMax = node.GetAttributeAsInt("HP"),
								CritChance = node.GetAttributeAsDouble("CritChance"),
								CritValue = node.GetAttributeAsDouble("CritValue")
				           	};

				foreach (string aot in node.GetAttributes("AoT"))
				{
					/*SiegeAoTStructure val;
					if (SiegeDataBase.DataAoT.Get(aot, out val))
						data.AoT.Add(val);*/

					data.AoTCode.Add(aot);
				}

				//var code = name.ToLower();
				if (!ContainsKey(name))
					Add(name, data);
			}

		}
	}

    /// <summary>
    /// Класс описывающий структуру дамага.
    /// </summary>
	public class SiegeDamageStructure
	{
		/// <summary>
		/// Код дамага
		/// </summary>
		public string Name;

		/// <summary>
		/// Тип дамага
		/// </summary>
		public string Type;

		/// <summary>
		/// Минимальный урон
		/// </summary>
		public int HpMin;

		/// <summary>
		/// Максимальный урон
		/// </summary>
		public int HpMax;

		/// <summary>
		/// Вероятность крита оружия
		/// </summary>
		public double CritChance;

		/// <summary>
		/// Величина критического удара
		/// </summary>
		public double CritValue;

		/// <summary>
		/// Бафы
		/// </summary>
		public List<SiegeAoTStructure> AoT = new List<SiegeAoTStructure>();

		public List<string> AoTCode = new List<string>();
	}
}