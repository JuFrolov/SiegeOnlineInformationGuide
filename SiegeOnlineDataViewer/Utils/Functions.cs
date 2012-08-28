using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SiegeOnlineDataViewer.Utils
{
	/// <summary>
	/// Класс прикладных методов
	/// </summary>
	public static class Functions
	{
		/// <summary>
		/// Пакетная замена символов в строке.
		/// </summary>
		/// <param name="str">Строка для замены</param>
		/// <param name="s1">Строка заменяемых символов</param>
		/// <param name="s2">Строка символов замены</param>
		/// <returns></returns>
		public static string ReplaceChars(string str, string s1, string s2)
		{
			if (str == null)
				return "";

			char ch2 = ' ';

			while (s1.Length > 0)
			{
				char ch1 = s1[0];

				if (s2.Length > 0)
					ch2 = s2[0];

				str = str.Replace(ch1, ch2);

				s1 = s1.Substring(1, s1.Length - 1);

				if (s2.Length > 1)
					s2 = s2.Substring(1, s2.Length - 1);
			}

			return str;
		}

		/// <summary>
		/// Строка из символов
		/// </summary>
		/// <param name="ch"></param>
		/// <param name="len"></param>
		/// <returns></returns>
		public static string RepeatChar(char ch, int len)
		{
			var ret = new StringBuilder();

			for (var i = 0; i < len; i++)
				ret.Append(ch);

			return ret.ToString();
		}

		/// <summary>
		/// Перевести кавычки в подстроках из формата Эксель (двойные) в формат Шарпа (экранированные)
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string ConvertDoubleQuotesToSingleSharp(string str)
		{
			var ret = str;

			var b = false;

			var pos = ret.IndexOf("\"");

			while (pos >= 0)
			{
				if (b)
				{
					if (SubString(ret, pos + 1, 1) == "\"")
					{
						ret = SubString(ret, 0, pos) + "\\\"" + SubString(ret, pos + 2);
						pos++;
					}
					else
						b = false;
				}
				else
					b = true;

				pos = ret.IndexOf("\"", pos + 1);
			}

			return ret;
		}

		/// <summary>
		/// Проверка наличия элемента во множестве
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="param"></param>
		/// <returns></returns>
		public static bool inList(object obj, params object[] param)
		{
			return param.Contains(obj);
		}

		/// <summary>
		/// Получить смещение по адресу в формате Excel
		/// </summary>
		/// <param name="address">Строка адреса в формате R1C3, R[3]C[-1] и т.д.</param>
		/// <param name="row">Текущий номер строки</param>
		/// <param name="col">Текущий номер столбца</param>
		/// <param name="mode">Режим разбора адреса: в формате A1, C4 и т.д. (=true), или в формате R[1]C[-2], R3C2 и т.д. (=false)</param>
		/// <param name="rowOut">Итоговая строка</param>
		/// <param name="colOut">Итоговый столбец</param>
		/// <returns>Результат анализа: false - произошла ошибка, true - всё хорошо</returns>
		public static bool GetExcelAddress(string address, int row, int col, bool mode, out int rowOut, out int colOut)
		{
			rowOut = row;
			colOut = col;

			if (mode)
			{
				var ae = new ASCIIEncoding();

				// формат адреса A1, C4 и т.д.
				short part = 1;
				string numStr = "";
				int numInt = 0;
				for (var i = 0; i < address.Length; i++)
				{
					var ch = address[i];
					var ch2 = i < address.Length - 1 ? address[i + 1] : '•';

					if (part == 1)
					{
						if (ch >= 'A' && ch <= 'Z')
						{
							byte[] x = ae.GetBytes(ch.ToString());

							numInt *= 26;
							numInt += x[0] - 64;
						}
						else
							return false;

						if (ch2 >= '0' && ch2 <= '9')
						{
							colOut = numInt;
							part = 2;
							continue;
						}

						if (ch2 == '•')
							return false;
					}

					if (part == 2)
					{
						if (ch >= '0' && ch <= '9')
						{
							numStr += ch;
						}
						else
							return false;

						if (ch2 == '•')
						{
							try
							{
								numInt = Convert.ToInt32(numStr);
							}
							catch (Exception)
							{
								return false;
							}

							rowOut = numInt;

							return true;
						}
					}
				}
			}
			else
			{
				// формат адреса R[1]C[-2], R3C2 и т.д.
				short dir = 0;
				short part = 0;
				bool rel = false;
				string numStr = "";
				for (var i = 0; i < address.Length; i++)
				{
					var ch = address[i];

					// анализ числа
					if (part > 0)
					{
						// анализ первых символов на квадратную скобку или минус
						if (part == 1)
						{
							if (ch == '[')
							{
								rel = true;
								continue;
							}

							if (ch == '-')
							{
								numStr += ch;
								part = 2;
								continue;
							}
						}

						part = 2;

						if (ch >= '0' && ch <= '9')
						{
							numStr += ch;
						}

						// заглянем в будущее
						var ch2 = i < address.Length - 1 ? address[i + 1] : '•';

						if (inList(ch2, 'R', 'C', ']', '•'))
						{
							if (rel && ch2 == ']')
							{
								// смещать дальше нельзя, это может быть конец строки - пусть всё идёт своим чередом
								//i++; 
								//rel = false;
								//part = 0;
								continue;
							}

							// подобьём итоги по смещению
							var num = 0;

							try
							{
								num = Convert.ToInt16(numStr);
							}
							catch (Exception)
							{
								return false;
							}

							if (!rel && num <= 0)
								return false;

							if (dir == 1)
								rowOut = rel ? row + num : num;

							if (dir == 2)
								colOut = rel ? col + num : num;

							dir = 0;
							part = 0;
							rel = false;
							numStr = "";
							continue;
						}

						if (ch < '1' || ch > '0')
							return false;
					}

					// анализ строки/колонки
					if (part == 0)
					{
						switch (ch)
						{
							case 'R':
								dir = 1;
								break;

							case 'C':
								dir = 2;
								break;
						}

						if (dir == 0)
							return false;

						part = 1;
						continue;
					}
				}

				return true;
			}

			return true;
		}

		/// <summary>
		/// Перевод строки в число (не зависимо от локализации)
		/// </summary>
		/// <param name="number"></param>
		/// <returns></returns>
		public static double ToDouble(string number)
		{
			switch (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
			{
				case ".":
					return
						Convert.ToDouble(number.Replace(",", "."));
				case ",":
					return
						Convert.ToDouble(number.Replace(".", ","));
				default:
					return 0.0;
			}
		}

		#region GetTokenString: Получить очередной "токен" в строке

		/// <summary>
		/// Получить очередной "токен" в строке (под токеном подразумевается серия "однотипных" символов - слов, чисел или кусков прочих одинаковых символов)
		/// </summary>
		/// <param name="str">Строка обработки</param>
		/// <param name="pos">Позиция обработки</param>
		/// <returns></returns>
		public static string GetTokenString(string str, int pos)
		{
			return GetTokenString(str, pos, true);
		}

		/// <summary>
		/// Получить очередной "токен" в строке (под токеном подразумевается серия "однотипных" символов - слов, чисел или кусков прочих одинаковых символов)
		/// </summary>
		/// <param name="str">Строка обработки</param>
		/// <param name="pos">Позиция обработки</param>
		/// <param name="doubleQuotes">Кавычки в строках двойные (=true) или экранированы слешем (=false)</param>
		/// <returns></returns>
		public static string GetTokenString(string str, int pos, bool doubleQuotes)
		{
			var ch = SubString(str, pos, 1);

			if (ch == "")
				return "";

			var token = ch;

			// отсекаем символы
			if (inList(ch, "^", "&", "*", "(", ")", ",", ";", "=", "+", "-", "|", "/", "\\", "<", ">", ":", " ", "\r", "\n", "\t"))
			{
				var i = 1;
				var ch2 = SubString(str, pos + i, 1);
				while (ch2 == ch && ch2 != "")
				{
					token += ch2;

					i++;
					ch2 = SubString(str, pos + i, 1);
				}

				return token;
			}

			// обработка токенов
			var chars = new Dictionary<string, string>
			            	{
			            		{"{", "}"},
			            		{"[", "]"},
			            		{"'", "'"}
			            	};
			foreach (var c in chars)
			{
				if (ch == c.Key)
				{
					for (var i = pos + 1; i < str.Length; i++)
					{
						ch = SubString(str, i, 1);
						token += ch;

						if (ch == c.Value)
							return token;
					}

					return token;
				}
			}

			// обработка строк в кавычках
			if (ch == "\"")
			{
				for (var i = pos + 1; i < str.Length; i++)
				{
					ch = str[i].ToString();
					token += ch;

					if (ch == "\"")
					{
						int slash = 0;
						while (i - slash > 0 && str[i - 1 - slash] == '\\')
							slash++;

						if (slash%2 == 0 && i > pos)
							return token;
					}
				}

				return token;
			}

			// пошли просто слова
			for (var i = pos + 1; i < str.Length; i++)
			{
				ch = SubString(str, i, 1);

				if (inList(ch, "!", "^", "&", "*", "(", ")", "[", "]", "\"", "'", ",", ";", "=", "+", "-", "|", "/", "\\", "<", ">",
				           ":", " ", "\r", "\n", "\t", ""))
					return token;

				token += ch;
			}

			return token;
		}

		/// <summary>
		/// Замена токенов в строке (альтернативный способ замены слов с учётом подстрок)
		/// </summary>
		/// <param name="str">Обрабатываемая строка</param>
		/// <param name="doubleQuotes">Кавычки в подстроках в виде двойных кавычек (=true) или экранированы слешем (=false)</param>
		/// <param name="tokens">Словарь заменяемых слов (что на что менять)</param>
		/// <returns></returns>
		public static string ReplaceTokensInString(string str, bool doubleQuotes, Dictionary<string, string> tokens)
		{
			var ret = "";
			var pos = 0;
			while (pos < str.Length)
			{
				var token = GetTokenString(str, pos, doubleQuotes);

				pos += token.Length;

				if (tokens.ContainsKey(token))
					token = tokens[token];

				ret += token;
			}

			return ret;
		}

		private static bool inSymbolList(string ch)
		{
			return inList(ch, "^", "&", "*", "(", ")", ",", ";", "=", "+", "-", "|", "/", ">", ":", "\\", " ", "\r", "\n", "\t");
		}

		#endregion

		#region SubString: Грамотный сабстринг (с автопроверкой длины и безопасным вычленением подстрок)

		/// <summary>
		/// Грамотный сабстринг (с автопроверкой длины и безопасным вычленением подстрок)
		/// </summary>
		/// <param name="str">Строка обработки</param>
		/// <param name="start">Индекс начала подстроки (начинается с нуля; если значение отрицательно, то старт отсчитывается от конца)</param>
		/// <returns></returns>
		public static string SubString(string str, int start)
		{
			if (start < 0)
				start = str.Length + start;

			return SubString(str, start, str.Length);
		}

		/// <summary>
		/// Грамотный сабстринг (с автопроверкой длины и безопасным вычленением подстрок)
		/// </summary>
		/// <param name="str">Строка обработки</param>
		/// <param name="start">Индекс начала подстроки (начинается с нуля; если значение отрицательно, то старт отсчитывается от конца)</param>
		/// <param name="len">Длина подстроки (при отрицательном значении отсекается len символов с конца)</param>
		/// <returns></returns>
		public static string SubString(string str, int start, int len)
		{
			if (start < 0)
			{
				start = str.Length + start;

				if (start < 0)
				{
					if (start + len > 0)
					{
						start = 0;
						len += start;
						return str.Substring(start, len);
					}

					return "";
				}
			}

			if (start > str.Length)
				return "";

			if (len < 0)
				len = str.Length - start + len;

			if (start + len > str.Length)
				len = str.Length - start;

			return str.Substring(start, len);
		}

		#endregion

		#region IndexOf: Умный поиск позиции вложения в строке (регистронезависимый с учётом подстрок)

		/// <summary>
		/// Умный поиск позиции вложения в строке (регистронезависимый с учётом подстрок)
		/// </summary>
		/// <param name="str">Обрабатываемая строка</param>
		/// <param name="of">Подстрока поиска</param>
		/// <param name="start">Начальный индекс поиска</param>
		public static int IndexOf(string str, string of, int start)
		{
			return IndexOf(str, of, start, true, false, false);
		}

		/// <summary>
		/// Умный поиск позиции вложения в строке (регистронезависимый с учётом подстрок)
		/// </summary>
		/// <param name="str">Обрабатываемая строка</param>
		/// <param name="of">Подстрока поиска</param>
		/// <param name="start">Начальный индекс поиска</param>
		/// <param name="SubStringOut">Искать ли за пределами подстрок (=true) или в них (=false)</param>
		/// <param name="CaseSensitive">Регистрозависимый поиск (=true)</param>
		/// <param name="SlashQuote">Кавычки экранируются слешем как в шарпе (=true) или просто двойные кавычки как в Эксель (=false)</param>
		/// <returns></returns>
		public static int IndexOf(string str, string of, int start, bool SubStringOut, bool CaseSensitive, bool SlashQuote)
		{
			var ret = -1;

			if (String.IsNullOrEmpty(str) || start > str.Length)
				return -1;

			if (String.IsNullOrEmpty(of))
				return 0;

			if (start < 0)
				start = 0;

			var b = SubStringOut ? 0 : 1;

			var pos = CaseSensitive ? str.IndexOf(of, start) : str.ToLower().IndexOf(of.ToLower(), start);
			var quotpos = str.IndexOf("\"");
			var quotes = 0;

			while (pos >= 0)
			{
				while (quotpos >= 0 && quotpos < pos)
				{
					if (!SlashQuote || SubString(str, quotpos - 1, 1) != "\\") // в Экселе в подстроках пишутся двойные кавычки!
						quotes++;

					quotpos = str.IndexOf("\"", quotpos + 1);
				}

				if (quotes%2 == b)
					return pos;

				pos = CaseSensitive ? str.IndexOf(of, pos + of.Length) : str.ToLower().IndexOf(of.ToLower(), pos + of.Length);
			}

			return ret;
		}

		#endregion

		#region ReplaceSubString: Замена подстроки, игнорируя строки в кавычках

		/// <summary>
		/// Замена подстроки, игнорируя строки в кавычках (c форматом Excel,где кавычки внутри строк - это дубль-кавычки) и обрабатывая только слова
		/// </summary>
		/// <param name="str">Обрабатываемая строка</param>
		/// <param name="A">Строка поиска</param>
		/// <param name="B">Строка замены</param>
		/// <returns></returns>
		public static string ReplaceSubString(string str, string A, string B)
		{
			return ReplaceSubString(str, A, B, true, false, false, false);
		}

		/// <summary>
		/// Замена подстроки, игнорируя строки в кавычках
		/// </summary>
		/// <param name="str">Обрабатываемая строка</param>
		/// <param name="A">Строка поиска</param>
		/// <param name="B">Строка замены</param>
		/// <param name="SubStringOut">Игнорировать подстроки в кавычках (или наоборот - работать только в них)</param>
		/// <returns></returns>
		public static string ReplaceSubString(string str, string A, string B, bool SubStringOut)
		{
			return ReplaceSubString(str, A, B, SubStringOut, false, false, false);
		}

		/// <summary>
		/// Замена подстроки, игнорируя строки в кавычках
		/// </summary>
		/// <param name="str">Обрабатываемая строка</param>
		/// <param name="a">Строка поиска</param>
		/// <param name="b">Строка замены</param>
		/// <param name="subStringOut">Игнорировать подстроки в кавычках (или наоборот - работать только в них)</param>
		/// <param name="caseSensitive">Регистрозависимая замена (по-умолчанию)</param>
		/// <param name="slashQuote">Кавычки экранируются слешем как в шарпе (=true) или просто двойные кавычки как в Эксель (=false)</param>
		/// <param name="singleWordsOnly">Обрабатывать только слова (ограниченные не буквенно-цифровыми символами) - данный параметр применим только для цифро-буквенных последовательностей, в остальных случаях оно не работоспособно! (юзайте соответствующую альтернативную функцию)</param>
		/// <returns></returns>
		public static string ReplaceSubString(string str, string a, string b, bool subStringOut, bool caseSensitive,
		                                      bool slashQuote, bool singleWordsOnly)
		{
			if (String.IsNullOrEmpty(str) || String.IsNullOrEmpty(a))
				return str;

			if (b == null)
				b = "";

			if (!str.Contains("\""))
			{
				if (!subStringOut)
					return str;

				return caseSensitive ? str.Replace(a, b) : ReplaceString(str, a, b, 0);
			}

			var result = str;

			var bOut = subStringOut ? 0 : 1;
			var pos = caseSensitive ? result.IndexOf(a) : result.ToLower().IndexOf(a.ToLower());
			var quotpos = result.IndexOf("\"");
			var quotes = 0;
			while (pos >= 0) // && (quotpos >= 0 || quotes % 2 == 1)
			{
				while (quotpos >= 0 && quotpos < pos)
				{
					if (!slashQuote || SubString(str, quotpos - 1, 1) != "\\") // в Экселе в подстроках пишутся двойные кавычки!
						quotes++;

					quotpos = result.IndexOf("\"", quotpos + 1);
				}

				if (quotes%2 == bOut)
				{
					// проверим граничность слов
					var bWord = true;
					if (singleWordsOnly)
					{
						var w1 = SubString(result, pos - 1, 1);
						var w2 = SubString(result, pos + a.Length, 1);

						bWord &= inList(w1, "^", "&", "*", "(", ")", ",", ";", "=", "+", "-", "|", "/", ">", ":", " ", "\r", "\n", "\t",
						                "\"");

						if (bWord)
							bWord &= inList(w2, "^", "&", "*", "(", ")", ",", ";", "=", "+", "-", "|", "/", ">", ":", " ", "\r", "\n", "\t",
							                "\"");

						if (bWord)
							bWord &= SubString(a, 0, 1) != w1;

						if (bWord)
							bWord &= SubString(a, -1, 1) != w2;
					}

					// производим замену подстрок
					if (bWord)
					{
						result = result.Substring(0, pos) + b + result.Substring(pos + a.Length);
						quotpos = quotpos >= 0 ? quotpos - a.Length + b.Length : -1;
						pos = caseSensitive ? result.IndexOf(a, pos + b.Length) : result.ToLower().IndexOf(a.ToLower(), pos + b.Length);
					}
					else
						pos = result.IndexOf(a, pos + 1);
				}
				else
					//pos = caseSensitive ? result.IndexOf(a, pos + a.Length) : result.ToLower().IndexOf(a.ToLower(), pos + a.Length);
					pos = caseSensitive ? result.IndexOf(a, pos + 1) : result.ToLower().IndexOf(a.ToLower(), pos + 1);
			}

			return result;
		}

		#endregion

		#region ReplaceString: Регистронезависимая замена подстроки

		/// <summary>
		/// Регистронезависимая замена подстроки
		/// </summary>
		/// <param name="strIn">Обрабатываемая строка</param>
		/// <param name="strA">Подстрока замены</param>
		/// <param name="strB">Строка для замены</param>
		/// <returns></returns>
		public static string ReplaceString(string strIn, string strA, string strB)
		{
			return ReplaceString(strIn, strA, strB, 0);
		}

		/// <summary>
		/// Регистронезависимая замена подстроки
		/// </summary>
		/// <param name="strIn">Обрабатываемая строка</param>
		/// <param name="strA">Подстрока замены</param>
		/// <param name="strB">Строка для замены</param>
		/// <param name="mode">Использовать ли регекспы (медленно)
		/// 0 - замена в лоб
		/// 1 - замена регекспами CalcFunctions.get...
		/// 2 - замена регекспами isUp_...</param>
		/// <returns></returns>
		public static string ReplaceString(string strIn, string strA, string strB, int mode)
		{
			return ReplaceString(strIn, strA, strB, mode, "FieldByName");
		}

		/// <summary>
		/// Регистронезависимая замена подстроки
		/// </summary>
		/// <param name="strIn">Обрабатываемая строка</param>
		/// <param name="strA">Подстрока замены</param>
		/// <param name="strB">Строка для замены</param>
		/// <param name="mode">Использовать ли регекспы (медленно)
		/// 0 - замена в лоб
		/// 1 - замена регекспами CalcFunctions.get...
		/// 2 - замена регекспами isUp_...</param>
		/// <param name="pattern">Наименование паттерна (Поле - FieldByName, Настройка - Variable)</param>
		/// <returns></returns>
		public static string ReplaceString(string strIn, string strA, string strB, int mode, string pattern)
		{
			return ReplaceString(strIn, strA, strB, mode, pattern, "fields, data");
		}

		/// <summary>
		/// Регистронезависимая замена подстроки
		/// </summary>
		/// <param name="strIn">Обрабатываемая строка</param>
		/// <param name="strA">Подстрока замены</param>
		/// <param name="strB">Строка для замены</param>
		/// <param name="mode">Использовать ли регекспы (медленно)
		/// 0 - замена в лоб
		/// 1 - замена регекспами CalcFunctions.get...
		/// 2 - замена регекспами isUp_...</param>
		/// <param name="pattern">Наименование паттерна (Поле - FieldByName, Настройка - Variable)</param>
		/// <param name="param">Перечень параметров</param>
		/// <returns></returns>
		public static string ReplaceString(string strIn, string strA, string strB, int mode, string pattern, string param)
		{
			string str = strIn;

			// прямая замена в лоб
			if (mode == 0)
			{
				if (string.IsNullOrEmpty(strIn))
					return "";

				if (string.IsNullOrEmpty(strA) || strA == strB)
					return strIn;

				int i = str.ToLower().IndexOf(strA.ToLower());

				while (i >= 0)
				{
					str = str.Substring(0, Math.Min(i, str.Length - 1)) + strB +
					      str.Substring(Math.Min(i + strA.Length, str.Length));

					i = str.ToLower().IndexOf(strA.ToLower(), Math.Min(str.Length, Math.Max(0, i + strB.Length)));
				}
			}
			else
				// замена через регекспы
			{
				if (strA.Contains("{") && strA.Contains("}"))
					// для Ревизора с его '%' в названиях переменных обход обработки через регекспы
					// чревато ошибками обработки имён полей, переменных и функций, содержащих '%'
					str = str.Replace(strA, strB);
				else
				{
					if (mode == 1)
						str = Regex.Replace(str,
						                    String.Format("(?<!CalcFunctions\\.get{0}(As\\w+)?\\({2},\\s*(\")?)\\b{1}\\b",
						                                  pattern,
						                                  strA, param), strB, RegexOptions.IgnoreCase);

					if (mode == 2)
						str = Regex.Replace(str,
						                    String.Format("\\b(?i:{0})\\b", strA),
						                    strB, RegexOptions.IgnoreCase);
				}
			}

			return str;
		}

		#endregion
	}
}