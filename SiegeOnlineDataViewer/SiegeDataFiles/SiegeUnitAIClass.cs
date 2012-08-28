using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
	public class SiegeUnitAIDesc : SiegeCommonAbstractDictionary<SiegeUnitAIStructure>
	{
		/// Конструктор
		public SiegeUnitAIDesc(string file)
		{
            Files.Clear();
            Files.Add(file);
		}

		/// <summary>
		/// Обработка данных
		/// </summary>
		public override void NodeParse(SiegeDataBaseNode nodes, ToolStripProgressBar progressBar)
		{
			foreach (SiegeDataBaseNode node in nodes.GetChilds("Unit"))
			{
				if (!node.Attributes.ContainsKey("Name"))
					continue;

				var name = node.GetAttribute("Name");

				var data = new SiegeUnitAIStructure
				           	{
				           		Name = name,
				           		UnitClass = node.GetAttribute("UnitClass"),
				           		TargetType = node.GetAttributes("TargetType"),
				           		SmartTargetingChance = node.GetAttributeAsBoolean("SmartTargetingChance")
				           	};

				//var code = name.ToLower();
				if (!ContainsKey(name))
					Add(name, data);
			}
		}
	}

	public class SiegeUnitAIStructure
	{
		/// <summary>
		/// Код элемента
		/// </summary>
		public string Name;
		
		/// <summary>
		/// Класс юнита
		/// </summary>
		public string UnitClass;

		/// <summary>
		/// Приоритеты цели
		/// </summary>
		public List<string> TargetType;

		/// <summary>
		/// Будут ли юниты учитывать целеуказания других юнитов
		/// </summary>
		public bool SmartTargetingChance;
	}
}