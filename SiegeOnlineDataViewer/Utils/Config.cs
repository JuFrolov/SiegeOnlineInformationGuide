using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SiegeOnlineDataViewer.Utils
{
	public class Config
	{
		private Dictionary<string, Dictionary<string, string>> Current = new Dictionary<string, Dictionary<string, string>>();
		
		public Config()
		{
			
		}

        public Config(string strFilename)
		{
			Current.Clear();

			if (!File.Exists(strFilename))
				return;

			var file = File.ReadAllLines(strFilename);

			var section = "";

			foreach (string s in file)
			{
				var str = s.Trim();

				if (str.StartsWith("[") && str.EndsWith("]"))
				{
					section = Functions.SubString(str, 1, -1);
					continue;
				}

				if (!Current.ContainsKey(section))
					Current.Add(section, new Dictionary<string, string>());

				var sec = Current[section];

				var pos = str.IndexOf('=');

				if (pos > 0)
				{
					var name = str.Substring(0, pos).Trim();
					var val = Functions.SubString(str, pos + 1).Trim();

					if (sec.ContainsKey(name))
						sec[name] = val;
					else
						sec.Add(name, val);
				}
			}
		}

		public string ReadValue(string section, string key, string def)
		{
			if (Current.ContainsKey(section) && Current[section].ContainsKey(key))
				return Current[section][key];

			return def;
		}

		public void WriteValue(string section, string key, string val)
		{
			if (!Current.ContainsKey(section))
				Current.Add(section, new Dictionary<string, string>());
			
			if (!Current[section].ContainsKey(key))
				Current[section].Add(key, val);
			else
				Current[section][key] = val;
		}

		public void Save(string fileName)
		{
			var sb = new StringBuilder();

			foreach (var sec in Current)
			{
				sb.AppendLine(string.Format("[{0}]", sec.Key));

				foreach (var str in sec.Value)
				{
					sb.AppendLine(string.Format("{0}={1}", str.Key, str.Value));
				}
			}

			File.WriteAllText(fileName, sb.ToString());
		}
	}


}