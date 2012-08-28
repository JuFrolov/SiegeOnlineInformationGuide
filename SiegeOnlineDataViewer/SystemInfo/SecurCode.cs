using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SiegeOnlineDataViewer.SystemInfo
{
	public static class SecurCode
	{
		/// <summary>
		/// Наименование файла ключей.
		/// </summary>
		public const string LicenseFileName = "SiegeOnlineMarket.key";

		/// <summary>
		/// Полный путь до файла ключей.
		/// </summary>
		private static readonly string LicenseFile;

		/// <summary>
		/// Собственный хэш, загруженный клиентом.
		/// </summary>
		public static readonly string SelfCode;

		/// <summary>
		/// Инициализация собственного хэша.
		/// </summary>
		static SecurCode()
		{
			if (AppConfig.Mode != AppConfig.ModeList.AllIncluded)
				return;

			var dir = Path.GetDirectoryName(Application.ExecutablePath); //Application.UserAppDataPath;

			//var arr = dir.Split(Path.DirectorySeparatorChar);

		    if (dir != null) 
                LicenseFile = Path.Combine(dir, LicenseFileName);

		    var path = Path.GetDirectoryName(Application.ExecutablePath);

		    if (path != null)
		    {
		        var file = Path.Combine(path, LicenseFile);

		        if (File.Exists(file))
		        {
		            try
		            {
		                SelfCode = File.ReadAllText(file);
		            }
		            catch
		            {
		            }
		        }
		    }
		}

		/// <summary>
		/// Простейшая верификация
		/// </summary>
		/// <returns></returns>
		public static bool CheckAccess(string str)
		{
			return SelfCode != null && SelfCode.ToLower() == GetHash(Info.GetInfo()).ToLower();
		}

		/// <summary>
		/// Простейшее снятие хэша с системы.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string GetHash(string str)
		{
			var md5Hasher = MD5.Create();

			var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));

			var sBuilder = new StringBuilder();

			var rnd = new Random();

			for (int i = 0; i < data.Length; i++)
			{
				var ch = data[i].ToString("x2");

				ch = rnd.NextDouble() > 0.5 ? ch.ToLower() : ch.ToUpper();
                
				sBuilder.Append(ch);
			}

			return sBuilder.ToString();

		}
	}
}
