using System;
using System.IO;
using System.Windows.Forms;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
	public class SiegeResourcePairsClass : SiegeCommonAbstractDictionary<SiegeResourcePairsStructure>
	{
		/// <summary>
        /// Конструктор по умолчанию.
		/// </summary>
		/// <param name="file">Путь к файлу.</param>
		public SiegeResourcePairsClass(string file)
		{
            Files.Clear();
			Files.Add(file);
		}

		/// <summary>
		/// Обработка данных.
		/// </summary>
		public override void NodeParse(SiegeDataBaseNode nodes, ToolStripProgressBar progressBar)
		{
			throw new Exception("Метод SiegeResourcePairsClass.NodeParse() не определён!");
		}

		/// <summary>
		/// Обработка данных.
		/// </summary>
		public new void LoadFromFile(ToolStripProgressBar progressBar)
		{
			if (Files.Count == 0)
				throw new Exception("Для инициализации класса данных необходимо указывать имя файла!");

            Clear();

		    foreach (var file in Files)
		    {
                var patch = Path.Combine(SiegeDataBase.GamePath, file);

                if (File.Exists(patch))
                {
                    string[] fileContent = File.ReadAllLines(patch);

                    if (progressBar != null)
                    {
                        progressBar.Maximum = fileContent.Length;
                        progressBar.Visible = true;
                    }

                    for (var i = 0; i < fileContent.Length; i++)
                    {
                        if (progressBar != null)
                            progressBar.Value = i;

                        string[] str = fileContent[i].Split('=');

                        if (str.Length == 2)
                        {
                            var name = str[0].Trim();
                            var data = new SiegeResourcePairsStructure { Name = name };
                            SiegeBuildingStructure building;

                            if (SiegeDataBase.DataBuildings.GetByProduction(name, out building))
                                data.Resources = CalcRes(name, building.Production);
                            
                            if (!ContainsKey(name))
                                Add(name, data);
                        }
                    }

                    if (progressBar != null)
                        progressBar.Visible = false;
                }
                else
                {
                    MessageBox.Show(string.Format("Не удалось найти файл '{0}'. Попробуйте переустановить игру!", Path.GetFileName(patch)), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
		    }
		}

		/// <summary>
		/// Рассчитать необходимое количество ресов для производства текущего
		/// </summary>
		/// <returns></returns>
		private SiegeDataBase.ResourseDictionary CalcRes(string name, SiegeBuildingStructure.ProductionClass prods)
		{
			var ret = new SiegeDataBase.ResourseDictionary();
			var prodAllTypes = prods.Products.Count;
			var prodMyCount = 0.0;
			var prodAllCount = 0.0;

			foreach (var prod in prods.Products)
			{
				if (prod.Key == name.ToLower())
					prodMyCount += prod.Value;

				prodAllCount += prod.Value;
			}

			foreach (var res in prods.Resources)
			{
				ret.Add(res.Key, prodAllTypes > 0 && prodMyCount > 0 ? res.Value / prodAllTypes / prodMyCount : 0);
			}

			return ret;
		}
	}

	public class SiegeResourcePairsStructure
	{
		/// <summary>
		/// Код ресурса.
		/// </summary>
		public string Name;

		/// <summary>
		/// Перечень ресурсов, из которых производится текущее и их необходимое количество.
		/// </summary>
		public SiegeDataBase.ResourseDictionary Resources;
		//public Dictionary<string, int> Resources;
	}
}