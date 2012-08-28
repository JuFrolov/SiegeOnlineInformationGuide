using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SiegeOnlineDataViewer.Utils
{
	/// <summary>
	/// Класс для работы с путями.
	/// </summary>
	public static class PathHelper
	{
		/// <summary>
		/// Запрещенные символы в названии файлов.
		/// </summary>
		public static readonly char[] FILE_INVALID_CHARS = new[] {'\\', '/', ':', '*', '?', '"', '<', '>'};

		/// <summary>
		/// Запрещенные символы в пути.
		/// </summary>
		public static readonly char[] PATH_INVALID_CHARS = new[] {'/', '*', '?', '"', '<', '>'};

		/// <summary>
		/// Каталог Startup
		/// </summary>
		public static string StartupPath
		{
			get { return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location); }
		}

		/// <summary>
		/// Базовая версия [major]
		/// todo: переименовать в MajorVersion
		/// </summary>
		/// <returns></returns>
		public static string BaseVersion1
		{
			get
			{
				string[] ver = VersionParts;
				if (ver == null || ver.Length < 1) return null;
				return ver[0];
			}
		}

		/// <summary>
		/// Базовая версия [major].[minor]
		/// </summary>
		/// <returns></returns>
		public static string BaseVersion
		{
			get
			{
				string[] ver = VersionParts;
				if (ver == null || ver.Length < 2) return null;
				return ver[0] + "." + ver[1];
			}
		}

		/// <summary>
		/// Версия базы данных [major].[build]
		/// </summary>
		/// <returns></returns>
		public static string DataBaseVersion
		{
			get
			{
				string[] ver = VersionParts;
				if (ver == null || ver.Length < 3) return null;
				return ver[0] + "." + ver[2];
			}
		}

		/// <summary>
		/// Версия клиента
		/// </summary>
		public static string Version
		{
			get { return GetVersion(Assembly.GetEntryAssembly()); }
		}

		/// <summary>
		/// Части версии
		/// </summary>
		private static string[] VersionParts
		{
			get
			{
				string ver = Version;

				if (ver == null) return null;

				return ver.Split('.');
			}
		}

		/// <summary>
		/// Получить версию сборки <paramref name = "assembly" />
		/// </summary>
		/// <param name = "assembly"></param>
		/// <returns></returns>
		public static string GetVersion(Assembly assembly)
		{
			if (assembly == null) return null;

			object[] attributes = assembly.GetCustomAttributes(typeof (AssemblyFileVersionAttribute), true);
			if (attributes.Length < 1) return null;

			var attr = (AssemblyFileVersionAttribute) attributes[0];

			return attr.Version;
		}

		/// <summary>
		/// Получить наименование директории
		/// </summary>
		/// <param name = "path"></param>
		/// <returns></returns>
		public static string GetDirectoryName(string path)
		{
			string str = path.TrimEnd(Path.DirectorySeparatorChar);
			int n = str.LastIndexOf(Path.DirectorySeparatorChar);

			return n < 0 ? string.Empty : str.Substring(n + 1);
		}

		/// <summary>
		/// Получить родительскую папку
		/// </summary>
		/// <param name = "path"></param>
		/// <returns></returns>
		public static string GetPathParent(string path)
		{
			return GetPathParent(path, Path.DirectorySeparatorChar);
		}

		/// <summary>
		/// Получить родительскую папку
		/// </summary>
		/// <param name = "path"></param>
		/// <param name = "separator"></param>
		/// <returns></returns>
		public static string GetPathParent(string path, char separator)
		{
			string str = path.TrimEnd(separator);
			int n = str.LastIndexOf(separator);

			return n < 0 ? string.Empty : str.Substring(0, n);
		}

		/// <summary>
		/// Перенести содержимое одной папки в другую
		/// </summary>
		/// <param name = "srcDir"></param>
		/// <param name = "destDir"></param>
		public static bool MoveFiles(string srcDir, string destDir)
		{
			bool res = true;

			if (!Directory.Exists(destDir))
			{
				Directory.CreateDirectory(destDir);
			}

			foreach (string file in Directory.GetFiles(srcDir))
			{
				try
				{
					File.Move(file, Path.Combine(destDir, Path.GetFileName(file)));
				}
				catch
				{
					res = false;
				}
			}

			foreach (string directory in Directory.GetDirectories(srcDir))
			{
				if (MoveFiles(directory, Path.Combine(destDir, GetDirectoryName(directory))))
				{
					try
					{
						Directory.Delete(directory, true);
					}
// ReSharper disable EmptyGeneralCatchClause
					catch
// ReSharper restore EmptyGeneralCatchClause
					{
					}
				}
				else
				{
					res = false;
				}
			}

			return res;
		}

		/// <summary>
		/// Удалить файл, игнорируя все ошибки
		/// </summary>
		/// <param name = "path"></param>
		public static void DeleteFile(string path)
		{
			try
			{
				File.Delete(path);
			}
// ReSharper disable EmptyGeneralCatchClause
			catch
// ReSharper restore EmptyGeneralCatchClause
			{
			}
		}

		/// <summary>
		/// Создать абсолютный путь из относительного
		/// </summary>
		/// <param name = "relativePath"></param>
		/// <returns></returns>
		public static string CreateAbsolutePath(string relativePath)
		{
			string dir;
			if (relativePath.StartsWith("~/"))
				relativePath = relativePath.Substring(2);
			dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
			dir = Path.GetFullPath(dir);
			return dir;
		}

		/// <summary>
		/// Возвращает путь, относительный к указанному корню.
		/// Если <paramref name = "resourcePath" /> находится вне указанного <paramref name = "rootPath" />,
		/// то возвращается <paramref name = "resourcePath" />
		/// </summary>
		/// <param name = "rootPath"></param>
		/// <param name = "resourcePath"></param>
		/// <returns></returns>
		public static string GetRelativePath(string rootPath, string resourcePath)
		{
			if (!Path.IsPathRooted(resourcePath))
				return resourcePath;
			string root = Path.GetFullPath(rootPath);
			string resource = Path.GetFullPath(resourcePath);
			int pos = resource.IndexOf(root, StringComparison.InvariantCultureIgnoreCase);
			if (pos != 0) return resource;
			return "/" + resource.Substring(root.Length);
		}

		/// <summary>
		/// Создать каталог, если отсутствует
		/// </summary>
		/// <param name = "path"></param>
		public static void MakeSureDirectoryExists(string path)
		{
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
		}

		/// <summary>
		/// Проверить имя файла на наличие запрещенных символов
		/// </summary>
		/// <param name = "path"></param>
		/// <returns>Обнаруженные запрещенные символы</returns>
		public static char[] CheckInvalidChars(string path)
		{
			if (path.IndexOfAny(FILE_INVALID_CHARS) < 0)
				return new char[] {};

			var result = new List<char>();

			foreach (var ch in FILE_INVALID_CHARS)
			{
				if (path.IndexOf(ch) >= 0)
					result.Add(ch);
			}

			return result.ToArray();
		}

		/// <summary>
		/// Проверить путь на наличие запрещенных символов
		/// </summary>
		/// <param name = "path"></param>
		/// <returns>Обнаруженные запрещенные символы</returns>
		public static char[] CheckInvalidPathChars(string path)
		{
			if (path.IndexOfAny(PATH_INVALID_CHARS) < 0)
				return new char[] {};

			var result = new List<char>();

			foreach (var ch in PATH_INVALID_CHARS)
			{
				if (path.IndexOf(ch) >= 0)
					result.Add(ch);
			}

			return result.ToArray();
		}

		/// <summary>
		/// Заменяет знаком подчеркивания все недопустимые в имени файла символы
		/// </summary>
		/// <param name = "fileName"></param>
		/// <returns></returns>
		public static string SupressInvalidFileNameChars(this string fileName)
		{
			var sb = new StringBuilder();
			foreach (char c in fileName)
			{
				sb.Append(c.ToString().IndexOfAny(FILE_INVALID_CHARS) >= 0 ? '_' : c);
			}
			return sb.ToString();
		}

		/// <summary>
		/// Очистка указанного каталога
		/// </summary>
		/// <param name="folderName"></param>
		public static void ClearFolder(string folderName)
		{
			var dir = new DirectoryInfo(folderName);

			foreach (FileInfo fi in dir.GetFiles())
			{
				fi.IsReadOnly = false;
				try
				{
					fi.Delete();
				}
// ReSharper disable EmptyGeneralCatchClause
				catch
// ReSharper restore EmptyGeneralCatchClause
				{
				}
			}

			foreach (DirectoryInfo di in dir.GetDirectories())
			{
				ClearFolder(di.FullName);
				try
				{
					di.Delete();
				}
// ReSharper disable EmptyGeneralCatchClause
				catch
// ReSharper restore EmptyGeneralCatchClause
				{
				}
			}
		}
	}
}