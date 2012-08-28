using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SiegeOnlineDataViewer.Properties;
using SiegeOnlineDataViewer.Utils;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
	public static class SiegeDataBase
	{
		public static SiegeBuildingsClass DataBuildings = new SiegeBuildingsClass(@"Data\Misc\Construction.dat");
		public static SiegeLanguagesClass DataLanguages = new SiegeLanguagesClass();
        public static SiegeObjectsUIClass DataObjectsUi = new SiegeObjectsUIClass(@"GameData\MenuDesc\ObjectsUIDesc.dat");
		public static SiegeObjectDescriptionClass DataObjectDescription = new SiegeObjectDescriptionClass(@"Data\Misc\ObjectDescription.dat");
		public static SiegeSoulClass DataSoul = new SiegeSoulClass(@"GameData\Misc\SoulDesc.dat");
		public static SiegeSquadsClass DataSquads = new SiegeSquadsClass(@"GameData\Misc\ServerSquadDesc.dat");
		public static SiegeUnitMoverClass DataUnitMover = new SiegeUnitMoverClass(@"GameData\Misc\ServerUnitMoverDesc.dat");
		public static SiegeUnitAIDesc DataUnitAi = new SiegeUnitAIDesc(@"GameData\Misc\ServerUnitAIDesc.dat");
		public static SiegeAoTDescClass DataAoT = new SiegeAoTDescClass(@"GameData\Misc\AoTDesc.dat");
		public static SiegeWeaponDescClass DataWeapon = new SiegeWeaponDescClass(@"GameData\Misc\WeaponDesc.dat");
		public static SiegeDamageDescClass DataDamage = new SiegeDamageDescClass(@"GameData\Misc\DamageDesc.dat");
		public static SiegeResourcePairsClass DataResoursePairs = new SiegeResourcePairsClass(@"Data\Misc\ResourcePairs.dat");
		public static SiegeQuestClass DataQuests = new SiegeQuestClass(@"GameData\Misc\QuestDescription.dat");
		public static SiegeArtefactDescClass DataArtefacts = new SiegeArtefactDescClass(@"GameData\Misc\ServerArtefactDesc.dat");

		//public static DataTable TableConstruction;
		//public static DataTable TableSquadDesc;
		//public static DataTable TableObjectDescription;

		/// <summary>
		/// Названия титулов
		/// </summary>
		public static Dictionary<string, string> TitleLanguage = new Dictionary<string, string>
		                                                         	{
		                                                         		{"knight", "Рыцарь"},
		                                                         		{"master", "Магистр"},
		                                                         		{"baron", "Барон"},
		                                                         		{"viscount", "Виконт"},
		                                                         		{"earl", "Граф"},
		                                                         		{"duke", "Герцог"},
		                                                         		{"prince", "Принц"},
		                                                         		{"king", "Король"}
		                                                         	};
		/// <summary>
		/// Названия титулов
		/// </summary>
		public static List<string> TitleCodes = new List<string>{ "knight", "master", "baron", "viscount", "earl", "duke", "prince", "king" };

		/*
		/// <summary>
		/// Названия лояльностей
		/// </summary>
		public static Dictionary<string, string> LoyalityLanguage = new Dictionary<string, string>()
		                                                         	{
		                                                         		{"hunger", "Сытость"},
		                                                         		{"trust", "Доверие"},
		                                                         		{"military", "Воинственность"},
		                                                         		{"product", "Производительность"},
		                                                         		{"culture", "Культура"}
		                                                         	};
		*/

		//---[ константы ]---
		public const string PictureNotAvailable = @"UI\Interface\Bkg_portrait_Select.tga";
		public const string PictureError = @"UI\grad_btn.tga";

		/// <summary>
		/// Путь до папки с игрой
		/// </summary>
		public static string GamePath = "";

		//=============================================================================================

		/// <summary>
		/// Загрузить содержимое файла
		/// </summary>
		/// <param name="file"></param>
		/// <param name="progressBar"></param>
		/// <param name="root"></param>
		/// <returns></returns>
		public static bool LoadDataFromFile(string file, ToolStripProgressBar progressBar, out SiegeDataBaseNode root)
		{
			root = new SiegeDataBaseNode();

			if (!File.Exists(file))
			{
				return false;
			}


			string[] fileContent = File.ReadAllLines(file);

			if (progressBar != null)
			{
				progressBar.Maximum = fileContent.Length;
				progressBar.Visible = true;
			}

			int line;
			int pos;
			GetNode(0, 0, fileContent, progressBar, out line, out pos, out root);

			if (progressBar != null)
				progressBar.Visible = false;

			return true;
		}

		/// <summary>
		/// Рекурсивный обход содержимого файла
		/// </summary>
		/// <param name="lineIn">Текущая позиция по строкам</param>
		/// <param name="posIn">Текущая позиция по символам в строке (нужно против батхёртов))))</param>
		/// <param name="fileContent">Содержимое файла построчно</param>
		/// <param name="progressBar">Прогрессбар</param>
		/// <param name="lineOut">Итоговая позиция по строкам</param>
		/// <param name="posOut">Итоговая позиция по символам в строке</param>
		/// <param name="node">Результат</param>
		/// <returns></returns>
		private static bool GetNode(int lineIn, int posIn, string[] fileContent, ToolStripProgressBar progressBar,
		                            out int lineOut, out int posOut, out SiegeDataBaseNode node)
		{
			node = new SiegeDataBaseNode();
			lineOut = lineIn;
			posOut = posIn;

			if (lineIn >= fileContent.Length)
				return false;

			// обновим текущий прогресс
			if (progressBar != null)
				progressBar.Value = lineIn <= progressBar.Maximum ? lineIn : progressBar.Maximum;

			while (lineOut < fileContent.Length)
			{
				string line = Functions.SubString(fileContent[lineOut], posOut);
				
				// пропускаем пустую строку заранее, чтобы лишний раз не молотить проц
				if (line.Trim() == "")
				{
					lineOut++;
					posOut = 0;
					continue;
				}

				// сначала режем все камменты, только потом обрабатываем строку!
				int comm = Functions.IndexOf(line, "#", 0, true, true, true);
				if (comm >= 0)
					line = Functions.SubString(line, 0, comm);

				// закончился очередной блок
				int skop = Functions.IndexOf(line, "}", 0, true, true, true);
				if (skop >= 0)
				{
					// необходимо для обработки некузявых строк вида "}\tId=..."
					// поэтому делаем лишь шаг вперёд и возвращаемся на строку назад, для повторной обработки текущей
					posOut += skop + 1;
					return true;
				}

				// необходимо для обработки некузявых строк вида "}\tId=..."
				// пост обработка оставшейся строки идёт ниже
				skop = Functions.IndexOf(line, "{", 0, true, true, true);
				if (skop >= 0)
					line = Functions.SubString(line, skop + 1);

				// пропускаем пустую строку, если нашли строку целиком лишь из каммента
				if (line.Trim() == "")
				{
					lineOut++;
					posOut = 0;
					continue;
				}

				int pos = Functions.IndexOf(line, "=", 0, true, true, true);
				// строка атрибутов
				if (pos >= 0)
				{
					string attr = line.Substring(0, pos).Trim();
					string val = Functions.SubString(line, pos + 1).Trim();
					if (val.StartsWith("\"") && val.EndsWith("\""))
						val = Functions.SubString(val, 1, -1);

					if (node.Attributes.ContainsKey(attr))
						node.Attributes[attr].Add(val);
					else
						node.Attributes.Add(attr, new List<string> {val});
				}
				else
				{
					SiegeDataBaseNode child;

					if (GetNode(lineOut, posOut + line.Length, fileContent, progressBar, out lineOut, out posOut, out child))
					{
						line = line.Trim();

						// попутно коцаем отключенные блоки
						if (!line.StartsWith("__") && child != null)
							if (node.Childs.ContainsKey(line))
								node.Childs[line].Add(child);
							else
								node.Childs.Add(line, new List<SiegeDataBaseNode> {child});
					}
				}

				if (posOut <= 0 || posOut >= fileContent[lineOut].Length)
				{
					lineOut++;
					posOut = 0;
				}
			}

			return true;
		}

       

		/// <summary>
		/// Загрузить дерево значений.
		/// </summary>
		public static bool InitTreeNode(string file, ToolStripProgressBar progressBar, ref SiegeDataBaseNode root)
		{
		    return LoadDataFromFile(file, progressBar, out root);
		}

	    /// <summary>
		/// Загрузка словаря из файла
		/// </summary>
		/// <returns></returns>
		public static bool LoadListFromFile(string file, out List<string> codeResourse)
		{
			codeResourse = new List<string>();

			if (!File.Exists(file))
			{
				return false;
			}

			string[] fileContent = File.ReadAllLines(file);

			foreach (string s in fileContent)
			{
				string[] str = s.Split('=');

				if (str.Length == 2)
				{
					str[0] = str[0].Trim();

					codeResourse.Add(str[0]);
				}
			}


			return true;
		}

		/// <summary>
		/// Обновление серверных кодов
		/// </summary>
		public static void UpdateServerCodes()
		{
			// загрузим серверные коды
			foreach (string upd in Resources.ServerCodes.Split('\n'))
			{
				string[] codes = upd.Split('=');

				if (codes.Length == 2)
				{
					codes[0] = codes[0].Trim();
					codes[1] = codes[1].Trim();

					string newCode;
					if (DataLanguages.Get(codes[1], out newCode))
					{
						string code = codes[0].ToLower();
						if (!DataLanguages.ContainsKey(code))
							DataLanguages.Add(code, newCode);
						else
							DataLanguages[code] = newCode;
					}
				}
			}

			// загрузим имена NPC
			foreach (string upd in Resources.ServerNPC.Split('\n'))
			{
				string[] codes = upd.Split('=');

				if (codes.Length == 2)
				{
					codes[0] = codes[0].Trim();
					codes[1] = codes[1].Trim();

					string newCode;
					if (!DataLanguages.Get(codes[0], out newCode))
					{
						if (!DataLanguages.ContainsKey(codes[0]))
							DataLanguages.Add(codes[0], codes[1]);
						else
							DataLanguages[codes[0]] = codes[1];
					}
				}
			}
		}

		/// <summary>
		/// Перевод строки требований к человеческому виду
		/// </summary>
		/// <returns></returns>
		public static string GetLocalizedStringRequirements(string str)
		{
			// уровень игрока
			var lang = new Dictionary<string, string>
			           	{
			           		{"level", "Уровень игрока: "},
			           		{"rank", "Уровень игрока: "},
			           		{"castlerating", "Строительный рейтинг: "}
			           	};
			foreach (var l in lang)
			{
				if (str.ToLower().StartsWith(l.Key))
					//return l.Value + Functions.SubString(str, l.Key.Length).Trim();
					return l.Value;
			}

			int pos = str.LastIndexOf(',');
			if (pos > 0)
			{
				string text = str.Substring(0, pos);
				string num = Functions.SubString(str, pos + 1).Trim();

				return string.Format("{0}: {1}", DataBuildings.Get(text).Name, num);
			}

			// репутация
			if (str.ToLower().StartsWith("reputation-"))
			{
				var tmp = GetLocalizedStringReputation(Functions.SubString(str, 11), '-');

				return tmp[0] + ": " + tmp[1];
			}

			/*if (str.StartsWith("Server"))
				str = Functions.SubString(str, 6);*/

			// перевод
			string name;
			if (DataLanguages.Get(str, out name))
				return name;

			// прочее
			switch (str)
			{
				case "SomeBuilding":
					return "Хотя бы 1 здание в замке";
			}

			// выуживаем наименование по ссылке от объекта
			SiegeBuildingStructure build;
			if (DataBuildings.Get(str, out build))
			{
				string code = build.ClientObjectRef;

				string ret;
				if (DataLanguages.Get(code, out ret))
					return ret;
			}

			return str;
		}

		/// <summary>
		/// Перевод строки репутации к человеческому виду
		/// </summary>
		/// <returns></returns>
		public static string[] GetLocalizedStringReputation(string str, char ch)
		{
			string[] rep = str.Split(ch);
			
			string ret = str;
			string ret2 = "";

			if (rep.Length == 3)
			{
				ret = (DataLanguages.Get(rep[0]) ?? rep[1]).Trim();

				switch (rep[1].ToLower())
				{
					case "player":
						ret += " (личная)";
						break;
					case "order":
						ret += " (орденская)";
						break;
				}

				ret2 = DataLanguages.Get(rep[2].Trim()) ?? rep[2].Trim();
			}

			return new [] { ret, ret2 };
		}

		/// <summary>
		/// Перевод строки лояльностей к человеческому виду
		/// </summary>
		/// <returns></returns>
		public static string[] GetLocalizedStringLoyality(string str, char ch)
		{
			string[] rep = str.Split(ch);

			if (rep.Length != 2)
				return new [] { str, "" };

			string ret = DataLanguages.Get(rep[0].Trim()) ?? rep[0].Trim();
			string ret2 = rep[1].Trim();

			return new [] { ret, ret2 };
		}

		/// <summary>
		/// Словарь связок ресурс-количество
		/// </summary>
		public class ResourseDictionary : SiegeCommonDataDictionary<double>
		{

		};

		public static string GetLocalizedStringTitle(string title)
		{
			title = title.ToLower();

			return TitleLanguage.ContainsKey(title) ? TitleLanguage[title] : title;
		}

		/// <summary>
		/// Получить наименование юнита (в т.ч. моба)
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string GetLocalizedUnitName(string str)
		{
			var res = DataLanguages.Get(str);

			SiegeSquadStructure squad;
			if (res == null && DataSquads.Get(str, out squad))
			{
				var clientName = squad.ObjectDescription.ClientObjectRef;

				res = DataLanguages.Get(clientName);
			}

			if (res == null)
			{
				
			}

			return res;
		}

	}
}