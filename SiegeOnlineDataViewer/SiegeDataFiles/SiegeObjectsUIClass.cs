using System.Collections.Generic;
using System.Windows.Forms;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
    /// <summary>
    /// Класс описывающий объекты UI.
    /// </summary>
	public class SiegeObjectsUIClass : SiegeCommonAbstractDictionary<string>
	{
		/// <summary>
        /// Конструктор по умолчанию.
		/// </summary>
		public SiegeObjectsUIClass(string file)
		{
            Files.Clear();
            Files.Add(file);
		}

		/// <summary>
		/// Обработка данных.
		/// </summary>
		public override void NodeParse(SiegeDataBaseNode nodes, ToolStripProgressBar progressBar)
		{
			foreach (SiegeDataBaseNode node in nodes.GetChilds("SpriteInfo"))
			{
				if (node.Attributes.ContainsKey("name") && node.Attributes.ContainsKey("FileName"))
				{
					List<string> attr = node.Attributes["name"];
					string fileName = node.Attributes["FileName"][0];

					foreach (string a in attr)
					{
						string code = a.ToLower();
						if (!ContainsKey(code))
							Add(code, fileName);
					}
				}
			}
		}
	}
}