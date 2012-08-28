using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
	public class SiegeWeaponDescClass : SiegeCommonAbstractDictionary<SiegeWeaponStructure>
	{
		/// Конструктор
		public SiegeWeaponDescClass(string file)
		{
            Files.Clear();
            Files.Add(file);
		}

		/// <summary>
		/// Обработка данных
		/// </summary>
		public override void NodeParse(SiegeDataBaseNode nodes, ToolStripProgressBar progressBar)
		{
			foreach (SiegeDataBaseNode node in nodes.GetChilds("Weapon"))
			{
				if (!node.Attributes.ContainsKey("Name"))
					continue;

				var name = node.GetAttribute("Name");

				var data = new SiegeWeaponStructure
				           	{
				           		Name = name,
				           		RadiusMax = node.GetAttributeAsDouble("Radius"),
				           		RadiusMin = node.GetAttributeAsDouble("RadiusMin"),
				           		TCoolDown = node.GetAttributeAsDouble("TCoolDown"),
				           		TShoot = node.GetAttributeAsDouble("TShoot"),
				           		ChargeCount = node.GetAttributeAsInt("ChargeCount"),
				           		DamageAddress = node.GetAttribute("DamageAddress"),
								AimAtCenter = node.GetAttributeAsBoolean("AimAtCenter"),
								Mortar = node.GetAttributeAsBoolean("Mortar"),
								RemoveCommandPoints = node.GetAttributeAsInt("RemoveCommandPoints")
				           	};

				SiegeDataBaseNode exp;
				if (node.GetChild("explosion", out exp))
				{
                    data.RadiusAttack = exp.GetAttributeAsDouble("Radius");

					SiegeDamageStructure damage;
					if (SiegeDataBase.DataDamage.Get(exp.GetAttribute("Damage"), out damage))
						data.Damage = damage;

					if (data.Damage.AoT.Count == 0 && data.Damage.AoTCode.Count > 0)
						foreach (string aotCode in data.Damage.AoTCode)
						{
							SiegeAoTStructure aot;
							if (SiegeDataBase.DataAoT.Get(aotCode, out aot))
							{
								SiegeDamageStructure dmg = null;

								if (aot.Damage == null && !string.IsNullOrEmpty(aot.DamageCode))
								{
									if (SiegeDataBase.DataDamage.Get(aot.DamageCode, out dmg))
									{
										aot.Damage = dmg;
									}
								}

								data.Damage.AoT.Add(aot);

								if (dmg != null && dmg.AoT.Count == 0 && dmg.AoTCode.Count > 0)
									foreach (string aotCode2 in dmg.AoTCode)
									{
										if (data.Damage.AoTCode.Contains(aotCode2))
											continue;

										SiegeAoTStructure aot2;
										if (SiegeDataBase.DataAoT.Get(aotCode2, out aot2))
										{
											//dmg.AoT.Add(aot2);
											data.Damage.AoT.Add(aot2);
										}
									}
							}

						}
				}

				//var code = name.ToLower();
				if (!ContainsKey(name))
					Add(name, data);
			}

		}
	}

	public class SiegeWeaponStructure
	{
		/// <summary>
		/// Код оружия
		/// </summary>
		public string Name;

		/// <summary>
		/// Дальность действия
		/// </summary>
		public double RadiusMax;

		/// <summary>
		/// Мёртвая зона
		/// </summary>
		public double RadiusMin;

		/// <summary>
		/// Время перезарядки
		/// </summary>
		public double TCoolDown;

		/// <summary>
		/// Время выстрела
		/// </summary>
		public double TShoot;

		/// <summary>
		/// Количество снарядов
		/// </summary>
		public int ChargeCount;

		/// <summary>
		/// Направление атаки
		/// </summary>
		public string DamageAddress;

		/// <summary>
		/// Радиус поражения
		/// </summary>
		public double RadiusAttack;

		/// <summary>
		/// Для стреляющего оружия: снаряд будет попадать в центр цели, а не в ближайшую точку
		/// </summary>
		public bool AimAtCenter;

		/// <summary>
		/// Для стреляющего оружия: снаряд будет лететь по навесной траектории
		/// </summary>
		public bool Mortar;

		/// <summary>
		/// Снимаемые ОК за атаку
		/// </summary>
		public int RemoveCommandPoints;

		//-------------------------------

		public SiegeDamageStructure Damage;
	}
}