using System;
using System.Collections.Generic;
using SiegeOnlineDataViewer.Utils;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
	/// <summary>
	/// Класс одного узла дерева.
	/// </summary>
	public class SiegeDataBaseNode
	{
		/// <summary>
		/// Атрибуты узла
		/// </summary>
		public SiegeCommonDataDictionary<List<string>> Attributes = new SiegeCommonDataDictionary<List<string>>();

		/// <summary>
		/// Дочерние ветви узла
		/// </summary>
		public SiegeCommonDataDictionary<List<SiegeDataBaseNode>> Childs = new SiegeCommonDataDictionary<List<SiegeDataBaseNode>>();

		//--------------------------------------------------

		#region Методы работы с атрибутами
		/// <summary>
		/// Запросить первое значение атрибута
		/// </summary>
		/// <param name="attr"></param>
		/// <returns></returns>
		public string GetAttribute(string attr)
		{
			string ret;
			GetAttribute(attr, out ret);

			return ret;
		}

		/// <summary>
		/// Запросить первое значение атрибута
		/// </summary>
		/// <param name="attr"></param>
		/// <param name="ret"></param>
		/// <returns></returns>
		public bool GetAttribute(string attr, out string ret)
		{
			ret = null;

			List<string> val = GetAttributes(attr, true);

			if (val.Count > 0)
			{
				ret = val[0];
				return true;
			}

			return false;
		}

		/// <summary>
		/// Запросить первое значение атрибута
		/// </summary>
		/// <param name="attr"></param>
		/// <returns></returns>
		public int GetAttributeAsInt(string attr)
		{
			int ret;
			GetAttribute(attr, out ret);

			return ret;
		}

		/// <summary>
		/// Запросить первое значение атрибута
		/// </summary>
		/// <param name="attr"></param>
		/// <param name="ret"></param>
		/// <returns></returns>
		public bool GetAttribute(string attr, out int ret)
		{
			ret = 0;

			List<string> val = GetAttributes(attr, true);

			if (val.Count > 0)
				return Int32.TryParse(val[0], out ret);

			return false;
		}

		/// <summary>
		/// Запросить первое значение атрибута
		/// </summary>
		/// <param name="attr"></param>
		/// <returns></returns>
		public double GetAttributeAsDouble(string attr)
		{
			double ret;
			GetAttribute(attr, out ret);

			return ret;
		}

		/// <summary>
		/// Запросить первое значение атрибута
		/// </summary>
		/// <param name="attr"></param>
		/// <param name="ret"></param>
		/// <returns></returns>
		public bool GetAttribute(string attr, out double ret)
		{
			ret = 0;

			List<string> val = GetAttributes(attr, true);

			if (val.Count > 0)
				try
				{
					ret = Functions.ToDouble(val[0]);
					return true;
				}
				catch (Exception) { }

			return false;
		}

		/// <summary>
		/// Запросить первое значение атрибута
		/// </summary>
		/// <param name="attr"></param>
		/// <returns></returns>
		public bool GetAttributeAsBoolean(string attr)
		{
			bool ret;
			GetAttribute(attr, out ret);

			return ret;
		}

		/// <summary>
		/// Запросить первое значение атрибута
		/// </summary>
		/// <param name="attr"></param>
		/// <param name="ret"></param>
		/// <returns></returns>
		public bool GetAttribute(string attr, out bool ret)
		{
			ret = false;

			List<string> val = GetAttributes(attr, true);

			if (val.Count > 0)
			{
				try
				{
					var d = Functions.ToDouble(val[0]);
					return d > 0;
				}
				catch (Exception) { }

				return Boolean.TryParse(val[0], out ret);
			}

			return false;
		}

		/// <summary>
		/// Запросить все значения атрибута
		/// </summary>
		/// <param name="attr"></param>
		/// <returns></returns>
		public List<string> GetAttributes(string attr)
		{
			return GetAttributes(attr, false);
		}

		/// <summary>
		/// Запросить все значения атрибута
		/// </summary>
		public bool GetAttributes(string attr, out List<string> ret)
		{
			ret = GetAttributes(attr, false);

			return ret.Count > 0;
		}

		/// <summary>
		/// Запросить значения атрибута
		/// </summary>
		/// <param name="attr"></param>
		/// <param name="oneOnly"></param>
		/// <returns></returns>
		private List<string> GetAttributes(string attr, bool oneOnly)
		{
			List<string> ret = new List<string>();

			//attr = attr.ToLower();

			if (Attributes.ContainsKey(attr))
				ret = oneOnly ? new List<string> {Attributes[attr][0]} : Attributes[attr];

			// покоцаем кавычки
			for (int i = 0; i < ret.Count; i++)
			{
				if (ret[i].StartsWith("\"") && ret[i].EndsWith("\""))
					ret[i] = Functions.SubString(ret[i], 1, -1);
			}

			return ret;
		}
		#endregion

		#region Методы работы с дочерними узлами
		/// <summary>
		/// Запросить первую ветку
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public SiegeDataBaseNode GetChild(string node)
		{
			List<SiegeDataBaseNode> ret = GetChilds(node, true);

			return ret.Count > 0 ? ret[0] : new SiegeDataBaseNode();
		}

		/// <summary>
		/// Запросить первую ветку
		/// </summary>
		/// <param name="node"></param>
		/// <param name="ret"></param>
		/// <returns></returns>
		public bool GetChild(string node, out SiegeDataBaseNode ret)
		{
			ret = new SiegeDataBaseNode();

			List<SiegeDataBaseNode> val = GetChilds(node, true);

			if (val.Count > 0)
			{
				ret = val[0];

				return true;
			}

			return false;
		}

		/// <summary>
		/// Запросить все ветки по имени
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public List<SiegeDataBaseNode> GetChilds(string node)
		{
			return GetChilds(node, false);
		}

		/// <summary>
		/// Запросить все ветки по имени
		/// </summary>
		public bool GetChilds(string node, out List<SiegeDataBaseNode> childs)
		{
			childs = GetChilds(node, false);

			return childs.Count > 0;
		}

		/// <summary>
		/// Запросить прикреплённые ветки
		/// </summary>
		/// <param name="node"></param>
		/// <param name="oneOnly"></param>
		/// <returns></returns>
		private List<SiegeDataBaseNode> GetChilds(string node, bool oneOnly)
		{
			var ret = new List<SiegeDataBaseNode>();

			//node = node.ToLower();

			if (Childs.ContainsKey(node))
				ret = oneOnly ? new List<SiegeDataBaseNode> {Childs[node][0]} : Childs[node];

			return ret;
		}
		#endregion
	}
}