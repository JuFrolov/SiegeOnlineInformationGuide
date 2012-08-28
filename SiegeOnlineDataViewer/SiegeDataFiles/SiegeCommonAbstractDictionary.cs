using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
	/// <summary>
	/// Класс для списков данных.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class SiegeCommonAbstractDictionary<T> : Dictionary<string, T>
	{
		/// <summary>
		/// Имя файла с данными.
		/// </summary>
		internal readonly List<string> Files = new List<string>();

		#region Управление элементами
		public new bool ContainsKey(string key)
		{
			return base.ContainsKey(key.ToLower());
		}

		public new T this[string key]
		{
			get
			{
				return base[key.ToLower()];
			}
			set
			{
				base[key.ToLower()] = value;
			}
		}

		public new void Add(string key, T item)
		{
			base.Add(key.ToLower(), item);
		}

		/// <summary>
		/// Получить элемент (при отсутствии вернёт пустое значение).
		/// </summary>
		/// <returns></returns>
		public T Get(string str)
		{
			T ret;
			Get(str, out ret);

			return ret;
		}

		/// <summary>
		/// Получить элемент.
		/// </summary>
		/// <returns></returns>
		public T Get(string str, out bool result)
		{
			T ret;
			result = Get(str, out ret);

			return ret;
		}

		/// <summary>
		/// Получить элемент
		/// </summary>
		/// <param name="str">Ключ элемента</param>
		/// <param name="result">Возвращает информацию, есть ли значение в словаре</param>
		/// <returns>true - элемент получен.</returns>
		public bool Get(string str, out T result)
		{
			result = default(T);

			if (!string.IsNullOrEmpty(str))
			{
				string code = str.ToLower();

				if (ContainsKey(code))
				{
					result = this[code];
					return true;
				}
			}

			return false;
		}
		#endregion
        
		/// <summary>
		/// Загрузка данных по зданиям в замке.
		/// </summary>
		/// <param name="progressBar">Индикатор загрузки.</param>
        public void LoadFromFile(ToolStripProgressBar progressBar)
		{
            if (Files.Count < 0)
		        throw new Exception("Для инициализации класса данных необходимо указывать имя файла!");

            Clear();

		    foreach (var file in Files)
		    {
                var patch = Path.Combine(SiegeDataBase.GamePath, file);

                if (File.Exists(patch))
		        {
		            SiegeDataBaseNode nodes;
                    SiegeDataBase.LoadDataFromFile(patch, null, out nodes);

		            NodeParse(nodes, progressBar);
		        }
		        else
		        {
		            MessageBox.Show(
                        string.Format("Не удалось найти файл '{0}'. Попробуйте переустановить игру!", Path.GetFileName(patch)), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

		        }
		    }
		}

	    /// <summary>
        /// Разбор дерева атрибутов и узлов.
        /// </summary>
		/// <param name="nodes">Узел дерева.</param>
		/// <param name="progressBar">Индикатор выполнения.</param>
		public abstract void NodeParse(SiegeDataBaseNode nodes, ToolStripProgressBar progressBar);
	}
}