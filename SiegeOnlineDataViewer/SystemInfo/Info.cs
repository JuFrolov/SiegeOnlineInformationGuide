using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace SiegeOnlineDataViewer.SystemInfo
{
    /// <summary>
    /// Статический класс для получени информации о компьютере.
    /// </summary>
	public static class Info
	{
		public static readonly string OsVersion;
		public static readonly string MachineName;

		public static MotherBoardInfo MotherBoard { get; private set; }
		public static List<DiskInfo> Disks { get; private set; }
		public static List<ProcessorInfo> Processors { get; private set; }
        
        /// <summary>
        /// Статический конструктор по умолчанию.
        /// </summary>
		static Info()
		{
			OsVersion = Environment.OSVersion.ToString();
			MachineName = Environment.MachineName;

			Disks = new DisksPack();
			MotherBoard = new MotherBoardInfo();
			Processors = new ProcessorsPack();
		}

        /// <summary>
        /// Получить информацию о системе.
        /// </summary>
        /// <returns>Строка с информацией о системе.</returns>
		public static string GetInfo()
		{
			var sb = new StringBuilder();

			sb.AppendLine("COMPUTER IDENTIFICATION");
			sb.AppendLine(string.Format("OS: {0}", OsVersion));
			sb.AppendLine(string.Format("MachineName: {0}", MachineName));
			sb.AppendLine(string.Format("ComputerID: {0}", MotherBoard.ComputerId));
			sb.AppendLine(string.Format("MB_Manufacturer: {0}", MotherBoard.Motherboard.Manufacturer));
			sb.AppendLine(string.Format("MB_Product: {0}", MotherBoard.Motherboard.Product));
			sb.AppendLine(string.Format("MB_SerialNumber: {0}", MotherBoard.Motherboard.SerialNumber));
			sb.AppendLine(string.Format("MB_Version: {0}", MotherBoard.Motherboard.Version));

			for (int index = 0; index < MotherBoard.OnBoardDevice.Count; index++)
                sb.AppendLine(string.Format("MB_OnBoardDevice{0}: {1}", index, MotherBoard.OnBoardDevice[index]));

			sb.AppendLine(string.Format("BIOS_Name: {0}", MotherBoard.Bios.Name));
			sb.AppendLine(string.Format("BIOS_Version: {0}", MotherBoard.Bios.Version));
			sb.AppendLine(string.Format("BIOS_SmVersion: {0}", MotherBoard.Bios.SmBiosVersion));

			for (int index = 0; index < Disks.Count; index++)
				sb.AppendLine(string.Format("DiskDrive{0}: {1} ({2} bytes)", index, Disks[index].Caption, Disks[index].Size));

			return sb.ToString();
		}

		/// <summary>
		/// Класс информации по диску.
		/// </summary>
		public class DiskInfo
		{
			public string Caption;
			public ulong Size;
		}

		/// <summary>
		/// Класс информации по процессору.
		/// </summary>
		public class ProcessorInfo
		{
			public string Caption;
			public string Name;
			public string ProcessorId;
		}

		/// <summary>
		/// Информации по дискам.
		/// </summary>
		public class DisksPack : List<DiskInfo>
		{
			public DisksPack()
			{
				try
				{
					using (var mgmtObjSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType <> 'USB'"))
					{
						using (var mgmtObjCollection = mgmtObjSearcher.Get())
						{
							foreach (ManagementBaseObject mbo in mgmtObjCollection)
							{
								var di = new DiskInfo
								{
									Caption = mbo.Properties["Caption"].Value.ToString(),
									Size = (ulong)mbo.Properties["Size"].Value
								};

								Add(di);
							}
						}
					}
				}
				catch (Exception) { }
			}
		}

		/// <summary>
		/// Информация по БИОСу.
		/// </summary>
		public class BiosInfo
		{
			public readonly string Name;
			public readonly string SmBiosVersion;
			public readonly string Version;

			public BiosInfo()
			{
				try
				{
					using (var mgmtObjSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS"))
					{
						using (var mgmtObjCollection = mgmtObjSearcher.Get())
						{
							foreach (ManagementBaseObject mbo in mgmtObjCollection)
							{
								Name = mbo.Properties["Name"].Value.ToString();
								SmBiosVersion = mbo.Properties["SMBIOSBIOSVersion"].Value.ToString();
								Version = mbo.Properties["Version"].Value.ToString();
							}
						}
					}
				}
				catch (Exception) { }
			}
		}

		/// <summary>
		/// Информация по мамке.
		/// </summary>
		public class MotherboardInfo
		{
			public string Manufacturer;
			public string Product;
			public string SerialNumber;
			public string Version;

			public MotherboardInfo()
			{
				try
				{
					using (var mgmtObjSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard"))
					{
						using (var mgmtObjCollection = mgmtObjSearcher.Get())
						{
							foreach (ManagementBaseObject mbo in mgmtObjCollection)
							{
								Manufacturer = mbo.Properties["Manufacturer"].Value.ToString();
								Product = mbo.Properties["Product"].Value.ToString();
								SerialNumber = mbo.Properties["SerialNumber"].Value.ToString();
								Version = mbo.Properties["Version"].Value.ToString();
							}
						}
					}
				}
				catch (Exception) { }
			}
		}

		/// <summary>
		/// Информация по мамке
		/// </summary>
		public class MotherBoardInfo
		{
			public string ComputerId;
			public BiosInfo Bios = new BiosInfo();
			public MotherboardInfo Motherboard = new MotherboardInfo();
			public List<string> OnBoardDevice = new List<string>();

			public MotherBoardInfo()
			{
				try
				{
					using (var mgmtObjSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystemProduct"))
					{
						using (var mgmtObjCollection = mgmtObjSearcher.Get())
						{
							foreach (ManagementBaseObject mbo in mgmtObjCollection)
							{
								ComputerId = mbo.Properties["UUID"].Value.ToString();
							}
						}
					}

					using (var mgmtObjSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_OnBoardDevice"))
					{
						using (var mgmtObjCollection = mgmtObjSearcher.Get())
						{
							foreach (ManagementBaseObject mbo in mgmtObjCollection)
							{
								OnBoardDevice.Add(mbo.Properties["Description"].Value.ToString());
							}
						}
					}
				}
				catch (Exception) { }
			}
		}

		/// <summary>
		/// Информация по процессорам.
		/// </summary>
		public class ProcessorsPack : List<ProcessorInfo>
		{
			public ProcessorsPack()
			{
				try
				{
					using (var mgmtObjSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
					{
						using (var mgmtObjCollection = mgmtObjSearcher.Get())
						{
							foreach (ManagementBaseObject mbo in mgmtObjCollection)
							{
								var pi = new ProcessorInfo
								{
									Caption = mbo.Properties["Caption"].Value.ToString(),
									Name = mbo.Properties["Name"].Value.ToString(),
									ProcessorId = mbo.Properties["ProcessorId"].Value.ToString()
								};

								Add(pi);
							}
						}
					}
				}
				catch
				{ }
			}
		}

	}
}
