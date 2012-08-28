using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
	public class SiegeUnitMoverClass : SiegeCommonAbstractDictionary<SiegeUnitMoverStructure>
	{
		/// Конструктор
		public SiegeUnitMoverClass(string file)
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

				var data = new SiegeUnitMoverStructure
				           	{
				           		Name = name,
				           		MaxSpeed = node.GetAttributeAsDouble("MaxSpeed"),
				           		AngularSpeed = node.GetAttributeAsDouble("AngularSpeed"),
				           		GetoutFromNonpassableArea = node.GetAttributeAsBoolean("GetoutFromNonpassableArea"),
				           		RegisterInPathfinder = node.GetAttributeAsBoolean("RegisterInPathfinder")
				           	};

				//var code = name.ToLower();
				if (!ContainsKey(name))
					Add(name, data);
			}

		}
	}

	public class SiegeUnitMoverStructure
	{
		/// <summary>
		/// Код мувера
		/// </summary>
		public string Name;		

		/// <summary>
		/// Скорость юнита
		/// </summary>
		public double MaxSpeed;		

		/// <summary>
		/// Скорость поворота (°/сек)
		/// </summary>
		public double AngularSpeed;

		/// <summary>
		/// Агресивность мобов друг к другу (будут ли они мешать друг другу)
		/// </summary>
		public bool GetoutFromNonpassableArea;	

		/// <summary>
		/// Используется ли серверный расчёт пути
		/// </summary>
		public bool RegisterInPathfinder;		
	}
}