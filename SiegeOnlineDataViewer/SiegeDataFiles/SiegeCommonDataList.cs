using System.Collections.Generic;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
	/// <summary>
	/// Класс словаря с регистронезависимыми ключами
	/// </summary>
	public class SiegeCommonDataList : List<string>
	{
        /// <summary>
        /// Проверка наличия ключа.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <returns>true - ключ присутствует.</returns>
		public new bool Contains(string key)
		{
			return base.Contains(key.ToLower());
		}

        /// <summary>
        /// Изменение/ получение объекта для соответствующего ключа.
        /// </summary>
        /// <param name="key">Ключ.</param>
		public new string this[int key]
		{
            get { return base[key].ToLower(); }
            set { base[key] = value.ToLower(); }
		}

        /// <summary>
        /// Добавление ключа в словарь.
        /// </summary>
        /// <param name="key">Ключ.</param>
		public new void Add(string key)
		{
			base.Add(key.ToLower());
		}
	}
}
