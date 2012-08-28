using System.Collections.Generic;
using System.Windows.Forms;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
	public class SiegeLanguagesClass : SiegeCommonAbstractDictionary<string>
	{
		/// <summary>
        /// Динамическая инициализация имени файла.
		/// </summary>
		/// <param name="lang">Язык.</param>
		public void SetLanguagePack(string lang)
		{
            Files.Clear();
			Files.Add(string.Format(@"Data\Misc\Strings_{0}.dat", frmMain.CurrentLanguage));
            Files.Add(string.Format(@"Data\Misc\Strings2_{0}.dat", frmMain.CurrentLanguage));
            Files.Add(string.Format(@"Data\Misc\Strings3_{0}.dat", frmMain.CurrentLanguage));
		}

        /// <summary>
        /// Обработка данных.
        /// </summary>
		/// <param name="nodes">Узел дерева.</param>
		/// <param name="progressBar">Индикатор выполнения.</param>
		public override void NodeParse(SiegeDataBaseNode nodes, ToolStripProgressBar progressBar)
		{
            foreach (SiegeDataBaseNode node in nodes.Childs["Item"])
			{
				if (node.Attributes.ContainsKey("Id"))
				{
					List<string> attr = node.Attributes["Id"];
					string name = node.Attributes.ContainsKey("Name") ? node.Attributes["Name"][0] : "";

					foreach (string a in attr)
					{
						string code = a.ToLower();

						if (!ContainsKey(code))
							Add(code, name);
					}
				}
			}
		}
	}
}