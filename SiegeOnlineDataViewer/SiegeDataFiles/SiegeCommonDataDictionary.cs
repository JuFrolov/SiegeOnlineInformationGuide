using System.Collections.Generic;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
	/// <summary>
	/// Класс словаря с регистронезависимыми ключами.
	/// </summary>
	public class SiegeCommonDataDictionary<T> : Dictionary<string, T>
	{
        /// <summary>
        /// Проверка наличия ключа.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <returns>true - ключ присутствует.</returns>
		public new bool ContainsKey(string key)
		{
			return base.ContainsKey(key.ToLower());
		}

        /// <summary>
        /// Изменение/ получение объекта для соответствующего ключа.
        /// </summary>
        /// <param name="key">Ключ.</param>
		public new T this[string key]
		{
            get { return base[key.ToLower()]; }
            set { base[key.ToLower()] = value; }
		}

        /// <summary>
        /// Добавление ключа в словарь.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="item">Объект.</param>
		public new void Add(string key, T item)
		{
			base.Add(key.ToLower(), item);
		}
	}
}
