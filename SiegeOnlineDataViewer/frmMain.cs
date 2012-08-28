//#define LIMITED

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using SiegeOnlineDataViewer.FormControls;
using SiegeOnlineDataViewer.Market;
using SiegeOnlineDataViewer.Properties;
using SiegeOnlineDataViewer.SiegeDataFiles;
using SiegeOnlineDataViewer.Utils;

namespace SiegeOnlineDataViewer
{
	public partial class frmMain : Form
	{
		//----[ публичные свойства ]----

		/// <summary>
		/// Код текущего языка локализации базы
		/// </summary>
		public static string CurrentLanguage = "";

		private string calcDisable = "В данной версии калькулятор отключен";

		// константы
		private const byte priceBuy = (byte)SiegeMarket.PriceType.Buy;
		private const byte priceSell = (byte)SiegeMarket.PriceType.Sell;
		public const int maxSquadLevel = 20;

		//----[ локальные переменные ]----

		private IconListControl _controlBuildingAfterBuild;
		private IconListControl _controlBuildingConstructionCost;
		private IconListControl _controlBuildingProductionCost;
		private IconListControl _controlBuildingProductionList;
		private IconListControl _controlBuildingRequirements;
		private IconListControl _controlBuildingUpgrade;
		private IconListControl _controlBuildingUpgrade2;

		/// <summary>
		/// Коды языков локализаций
		/// </summary>
		private List<string> _dataLangsCode = new List<string>();

		private frmMarket formMarket = null;
		private frmMaps formMaps = null;

		// таймер моргания доната
		private Timer timerDonate;

		#region Таблицы для отображения пользователю
		private DataTable _dtBuildingsList = new DataTable();
		private DataView _dtBuildingsListView = new DataView();
		private DataTable _dtSquadsList = new DataTable();
		private DataView _dtSquadsListView = new DataView();
		private DataTable _dtQuestsList = new DataTable();
		private DataView _dtQuestsListView = new DataView();
		#endregion

		//----[ константы ]----

		//private const int ConstBuildProductionBlockSpace = 8; // расстояние между блоками текста на странице продукции здания


		//==================================================================================

		public frmMain()
		{
			InitializeComponent();

			// Загрузим и проинициализируем настройки
			SettingsLoad();

			tabPages_SelectedIndexChanged(tabsMain, EventArgs.Empty);
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			//SettingsSave();

			if (SiegeDataBase.DataSquads != null)
				SiegeDataBase.DataSquads.SaveUpgradeInfo();
		}

		//----------------------------------------------------------------------------------

		/// <summary>
		/// Инициализация списка доступных языков
		/// </summary>
		/// <param name="dir"></param>
		private void LocalizationInit(string dir)
		{
			if (!Directory.Exists(dir))
			{
				MessageBox.Show("Указанная папка не распознана как папка игры.", "Ошибка", MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}

			var files = Directory.GetFiles(dir, "Strings_??.dat");
			listBoxLanguages.Items.Clear();
			foreach (var s in files.OrderBy(s => s))
			{
				var lang = Path.GetFileNameWithoutExtension(s);
				lang = lang.Substring(lang.Length - 2).ToLower();

				var desc = "Иностранный";
				switch (lang)
				{
					case "ru":
						desc = "Русский";
						break;
					case "en":
						desc = "Английский";
						break;
				}
				_dataLangsCode.Add(lang);
				listBoxLanguages.Items.Add(string.Format("{0} ({1})", desc, lang));
			}

			var k = 0;
			foreach (var language in _dataLangsCode)
			{
				if (language == "ru")
				{
					listBoxLanguages.SelectedIndex = k;
					break;
				}
				k++;
			}
			if (listBoxLanguages.SelectedIndex < 0 && listBoxLanguages.Items.Count > 0)
				listBoxLanguages.SelectedIndex = 0;

			if (listBoxLanguages.SelectedIndex >= 0)
				CurrentLanguage = _dataLangsCode[listBoxLanguages.SelectedIndex];
		}

		// Обновление информации о выделенном объекте
		private void UpdateInfo()
		{
			// текущая страница данных
			int page = tabsMain.SelectedIndex;

			DataGridView dataGridView = null; // текущий датагрид
			switch (page)
			{
				case 1:
					dataGridView = dataGridBuildings;
					break;

				case 2:
					dataGridView = dataGridSquads;
					break;

				case 3:
					dataGridView = dataGridQuests;
					break;
			}

			if (dataGridView == null)
				return;

			// код текущего объекта
			var code = (dataGridView.Tag ?? "").ToString();
			/*var code = dataGridView.SelectedRows.Count > 0 
				? dataGridView.SelectedRows[0].Cells["code"].Value.ToString()
				: "";*/

			switch (page)
			{
				case 1:
					UpdateBuildingInfo(code);
					break;

				case 2:
					UpdateSquadInfo(code);
					break;

				case 3:
					UpdateQuestInfo(code);
					break;
			}
		}

		/// <summary>
		/// Обновление информации по зданиям
		/// </summary>
		/// <param name="codeBuilding"></param>
		private void UpdateBuildingInfo(string codeBuilding)
		{
			int subPage = tabsBuildingInfo.SelectedIndex;

			pictBuilding.Image = null;

			SiegeBuildingStructure building = null;
			if (string.IsNullOrEmpty(codeBuilding) || !SiegeDataBase.DataBuildings.Get(codeBuilding, out building))
			{
				if (!string.IsNullOrEmpty(codeBuilding))
				{
					lblBuildingTitle.Text = "Здание не найдено!";
					lblBuildingTitle.ForeColor = Color.Red;
				}
				else
				{
					lblBuildingTitle.Text = "- Здание не выбрано -";
					lblBuildingTitle.ForeColor = Color.Black;
				}

				lblBuildingCode.Text = "";
				lblBuildingSize.Text = "-";

				// очистим данные ткущей субстраницы
				UpdateBuildingSubInfo(building, subPage);

				return;
			}

			lblBuildingTitle.Text = string.IsNullOrEmpty(building.Name) ? "- Названия нет -" : building.Name;
			lblBuildingTitle.ForeColor = Color.Black;

			lblBuildingCode.Text = "Код: " + building.Code;
			lblBuildingSize.Text = string.Format("{0} x {1}", building.SizeA, building.SizeB);


			// обновим изображение здания
			var file = SiegeDataBase.DataObjectsUi.Get(codeBuilding);
			if (file == "")
				file = string.Format(@"UI\ConstructionIcons\{0}_icon.tga", codeBuilding);

			var imageFile = Path.Combine(SiegeDataBase.GamePath, @"Data\Textures\");
			imageFile = Path.Combine(imageFile, file ?? SiegeDataBase.PictureError);

			if (File.Exists(imageFile))
			{
				var image = TgaImages.GetFromFile(imageFile);
				pictBuilding.Image = image;
			}

			// обновим данные текущей субстраницы
			UpdateBuildingSubInfo(building, subPage);
		}

		/// <summary>
		/// Обновление информации о войсках
		/// </summary>
		/// <param name="codeSquad"></param>
		private void UpdateSquadInfo(string codeSquad)
		{
			// махинации с видимостью закладок
			if (_tabSquadCommon == null)
			{
				_tabSquadCommon = tabSquadCommon;
				_tabSquadTTH = tabSquadTTH;
				_tabSquadUpgrade = tabSquadUpgrade;
				_tabSquadCalc = tabSquadCalc;
				//tabsSquadsInfo.TabPages.Remove(_tabSquadCalc);
			}

			string subPageTag = (tabsSquadsInfo.SelectedTab.Tag ?? "").ToString();

			pictSquad.Image = null;

			SiegeSquadStructure squad;
			if (string.IsNullOrEmpty(codeSquad) || !SiegeDataBase.DataSquads.Get(codeSquad, out squad))
			{
				if (!string.IsNullOrEmpty(codeSquad))
				{
					lblSquadTitle.Text = "Войско не найдено!";
					lblSquadTitle.ForeColor = Color.Red;
				}
				else
				{
					lblSquadTitle.Text = "- Войско не выбрано -";
					lblSquadTitle.ForeColor = Color.Black;
				}
				lblSquadCode.Text = "";

				UpdateSquadSubInfo(null, subPageTag);

				return;
			}

			var name = SiegeDataBase.GetLocalizedUnitName(squad.Code);
			lblSquadTitle.Text =  name ?? "- Названия нет -";
			lblSquadTitle.ForeColor = Color.Black;
			lblSquadCode.Text = "Код: " + squad.Name;


			// обновим изображение войска
			//var unit = SiegeDataBase.DataSquads.Get(codeSquad);
			var file = SiegeDataBase.DataObjectsUi.Get(codeSquad);
			if (file == "")
				file = string.Format(@"UI\ConstructionIcons\{0}_icon.tga", codeSquad);

			var imageFile = Path.Combine(SiegeDataBase.GamePath, @"Data\Textures\");
			imageFile = Path.Combine(imageFile, file ?? SiegeDataBase.PictureError);

			if (File.Exists(imageFile))
			{
				var image = TgaImages.GetFromFile(imageFile);
				pictSquad.Image = image;
			}

			// обновим основную информацию о войске
			txtbxSquadMainDescription.Clear();
			txtbxSquadMainDescription.Text += string.Format("Уровень{0}: {1}{2}", squad.ParentBuilding == null ? " (NPC)" : "",
															squad.RequiredInfo.RequiredRank, Environment.NewLine);

			if (squad.UnitAiInfo != null)
			{
				string str;
				if (!SiegeDataBase.DataLanguages.Get(squad.UnitAiInfo.UnitClass, out str))
					str = squad.UnitAiInfo.UnitClass;

				txtbxSquadMainDescription.Text += string.Format("Класс: {0}{1}", str, Environment.NewLine);
			}

			if (squad.Restorable > 0)
				txtbxSquadMainDescription.Text += string.Format("Безлимитные войска{0}", Environment.NewLine);

			txtbxSquadMainDescription.Text += Environment.NewLine;

			// ссылка на родительское здание
			if (squad.ParentBuilding != null)
			{
				linkSquadBuilding.Text = squad.ParentBuilding.Name;
				linkSquadBuilding.Tag = squad.ParentBuilding.Code;
			}
			else
			{
				linkSquadBuilding.Text = "";
			}


			_updateInfoEnabled = false;


			#region Удаляем ненужные закладки
			// Страница ТТХ только, если юнит - моб (сделать доступным только избранным)
			tabsSquadsInfo.TabPages.Remove(_tabSquadTTH);	//if (squad.ParentBuilding != null && tabsSquadsInfo.TabPages.Contains(_tabSquadTTH))
			// Страница апгрейд только, если есть данные по апгрейду (доступно всем)
			tabsSquadsInfo.TabPages.Remove(_tabSquadUpgrade);
			// Страница калькулятора только, если юнит производимый (доступно всем, заблокировано)
			tabsSquadsInfo.TabPages.Remove(_tabSquadCalc);
			#endregion

			#region Добавляем отсутствующие закладки и выделяем нужную
			if (squad.ParentBuilding == null)	// && !tabsSquadsInfo.TabPages.Contains(_tabSquadTTH)
				tabsSquadsInfo.TabPages.Add(_tabSquadTTH); // Insert(Math.Min(tabsSquadsInfo.TabPages.Count, 1), 
			if (squad.UnitUpgradeInfo.Count > 0)
				tabsSquadsInfo.TabPages.Add(_tabSquadUpgrade);
			if (squad.ParentBuilding != null)
				tabsSquadsInfo.TabPages.Add(_tabSquadCalc);

			if (tabsSquadsInfo.Tag != null)
				foreach (TabPage page in tabsSquadsInfo.TabPages)
				{
					if (page.Tag == tabsSquadsInfo.Tag)
					{
						tabsSquadsInfo.SelectedTab = page;
						break;
					}
				}
			#endregion

			subPageTag = (tabsSquadsInfo.SelectedTab.Tag ?? "").ToString();

			// обновим данные текущей субстраницы
			UpdateSquadSubInfo(squad, subPageTag);

			_updateInfoEnabled = true;
		}

		/// <summary>
		/// Обновление информации о квестах
		/// </summary>
		/// <param name="codeQuest"></param>
		private void UpdateQuestInfo(string codeQuest)
		{
			txtbxQuest.Tag = codeQuest;

			lblQuestCode.Text = "Код: " + codeQuest;

			txtbxQuest.Clear();

			SiegeQuestStructure quest = new SiegeQuestStructure();
			if (string.IsNullOrEmpty(codeQuest) || !SiegeDataBase.DataQuests.Get(codeQuest, out quest))
			{
				/*sb.AppendLine(!string.IsNullOrEmpty(codeQuest)
				                	? @"<p style='color:red'>- Квест не найден! -</p>"
				                	: "- Квест не выбран -");*/
			}
			else
			{
				var bold = new Font(txtbxQuest.Font, FontStyle.Bold);

				txtbxQuest.AddLine(quest.Name, new Font(txtbxQuest.Font.FontFamily, 10, FontStyle.Bold));

				txtbxQuest.AddLine(quest.Description); //txtbxQuest.Font

				txtbxQuest.AddLine();

				txtbxQuest.AddLine("Текст после выполнения квеста:", bold);
				txtbxQuest.AddLine(quest.CompleteDescription);
				txtbxQuest.AddLine();

				var npcQuestTaker = quest.QuestTaker;
				if (npcQuestTaker != null)
				{
					txtbxQuest.AddText("Квест выдаёт: ");
					npcQuestTaker = SiegeDataBase.GetLocalizedUnitName(quest.QuestTaker);
					txtbxQuest.AddLink(npcQuestTaker, "NPC:" + quest.QuestTaker);
					txtbxQuest.AddLine();
				}

				var npcQuestGiver = quest.QuestGiver;
				if (npcQuestGiver != null)
				{
					txtbxQuest.AddText("Квест принимает: ");
					npcQuestGiver = SiegeDataBase.GetLocalizedUnitName(quest.QuestGiver);
					txtbxQuest.AddLink(npcQuestGiver, "NPC:" + quest.QuestGiver);
					txtbxQuest.AddLine();
				}

				if (quest.QuestTaker != null || quest.QuestGiver != null)
					txtbxQuest.AddLine();

				#region Квестовые предметы и награда за квест

				var rewards = new List<SiegeQuestStructure.GiveOnAcceptClass>()
				              	{
				              		quest.GiveOnAccept,
				              		quest.Reward
				              	};

				foreach (var rew in rewards)
				{
                    if (rew != null)
                    {
                    	txtbxQuest.AddLine(rew.Name == "accept"
                    	                   	? "Квестовые инструменты (даются вместе с квестом):"
                    	                   	: "Награда за выполнение квеста:", bold);

						if (!string.IsNullOrEmpty(rew.Title))
							txtbxQuest.AddLine(string.Format("- титул {0}",
							                                 SiegeDataBase.TitleLanguage.ContainsKey(rew.Title)
							                                 	? SiegeDataBase.TitleLanguage[rew.Title]
							                                 	: rew.Title));

						if (rew.Xp > 0)
							txtbxQuest.AddLine(string.Format("- {0} опыта", rew.Xp));

						if (rew.Item != null)
							foreach (var item in rew.Item)
							{
								var arr = item.Split(';');
								var code = arr[0].Trim();

								int level = 0;
								if (arr.Length >= 2)
									int.TryParse(arr[1], out level);

								var count = 1;
								if (arr.Length >= 3)
									int.TryParse(arr[2], out count);

								if (count > 1)
									txtbxQuest.AddText(string.Format("- {0} пачки ", arr[2]));
								else
									txtbxQuest.AddText("- ");

								txtbxQuest.AddLink(SiegeDataBase.DataLanguages.Get(code) ?? code, code);

								if (level > 0)
									txtbxQuest.AddText(string.Format(" (+{0} ур.)", arr[1]));

								txtbxQuest.AddLine();
							}

						if (rew.Fund != null)
							foreach (var res in rew.Fund)
								txtbxQuest.AddLine(string.Format("- {0} ед. {1}", res.Value, SiegeDataBase.DataLanguages.Get(res.Key) ?? res.Key));

						if (rew.Reputation != null)
						{
							foreach (string rep in rew.Reputation)
							{
								var arr = SiegeDataBase.GetLocalizedStringReputation(rep, ';');

								txtbxQuest.AddLine(string.Format("- репутация '{0}' увеличится на {1}", arr[0], arr[1]));
							}
						}

						if (rew.Loyalty != null)
						{
							foreach (string loyal in rew.Loyalty)
							{
								var arr = SiegeDataBase.GetLocalizedStringLoyality(loyal, ',');

								txtbxQuest.AddLine(string.Format("- {0} увеличится на {1}", arr[0].ToLower(), arr[1]));
							}
						}

						// SetCastle

						txtbxQuest.AddLine();

					}
				}

				#endregion

				#region Время обновления квеста

				if (quest.ResetTime != 0)
				{
					txtbxQuest.AddText("Время обновления квеста: ", bold);
					txtbxQuest.AddLine(GetTimeString(quest.ResetTime));
				}

				if (quest.DateTimeInterval != null)
				{
					txtbxQuest.AddLine("Разовая установка обновления квеста:", bold);
					foreach (SiegeQuestStructure.TimeIntervalStruct time in quest.DateTimeInterval)
					{
						DateTime dt;
						string t = DateTime.TryParse(time.Begin, out dt) ? dt.ToString() : time.Begin;
						
						txtbxQuest.AddLine(string.Format("- начиная с {0} через каждые {1}", t, GetTimeString(time.Hours)));
					}
				}

				if (quest.TimeInterval != null)
				{
					txtbxQuest.AddLine("Ежедневное время обновления квеста:", bold);
					foreach (SiegeQuestStructure.TimeIntervalStruct time in quest.TimeInterval)
					{
						txtbxQuest.AddLine(string.Format("- начиная с {0} через каждые {1}", time.Begin, GetTimeString(time.Hours)));
					}
				}

				if (quest.WeekDayInterval != null)
				{
					txtbxQuest.AddLine("Еженедельное время обновления квеста:", bold);
					foreach (KeyValuePair<string, SiegeQuestStructure.TimeIntervalStruct> time in quest.WeekDayInterval)
					{
						txtbxQuest.AddLine(string.Format("- начиная с {0} {1} через каждые {2}", time.Key, time.Value.Begin, GetTimeString(time.Value.Hours)));
					}
				}

				if (quest.ResetTime != 0 || quest.DateTimeInterval != null || quest.TimeInterval != null || quest.WeekDayInterval != null)
					txtbxQuest.AddLine();

				#endregion
                
				#region Условия получения квеста
				if (quest.AcceptConditions != null)
				{
					txtbxQuest.AddLine("Условия получения квеста:", bold);

					var flg = false;

					if (quest.AcceptConditions.CompleteQuest != null)
					{
						foreach (var cond in quest.AcceptConditions.CompleteQuest)
						{
							txtbxQuest.AddText("- выполнение квеста ");
							txtbxQuest.AddLink((SiegeDataBase.DataLanguages.Get(cond) ?? cond).Trim(), "quest:" + cond);
							txtbxQuest.AddLine();
						}

						flg = true;
					}

					if (quest.AcceptConditions.BeginLevel > 0 || quest.AcceptConditions.EndLevel > 0)
					{
						txtbxQuest.AddText("- уровень игрока");

						if (quest.AcceptConditions.BeginLevel > 0)
							txtbxQuest.AddText(" от " + quest.AcceptConditions.BeginLevel);

						if (quest.AcceptConditions.EndLevel > 0)
							txtbxQuest.AddText(string.Format(" до {0} (включительно)", quest.AcceptConditions.EndLevel));

						txtbxQuest.AddLine();

						flg = true;
					}

					if (!string.IsNullOrEmpty(quest.AcceptConditions.ControlPointOwned))
					{
						txtbxQuest.AddLine(string.Format("- контроль над PvP-точкой '{0}'", quest.AcceptConditions.ControlPointOwned));

						flg = true;
					}

					if (quest.AcceptConditions.Reputation != null)
					{
						foreach (string rep in quest.AcceptConditions.Reputation)
						{
							var arr = SiegeDataBase.GetLocalizedStringReputation(rep, ';');

							txtbxQuest.AddLine(string.Format("- репутация '{0}' равная '{1}'", arr[0], arr[1]));
						}

						flg = true;
					}

					if (quest.AcceptConditions.ReputationGreaterEqual != null)
					{
						foreach (string rep in quest.AcceptConditions.ReputationGreaterEqual)
						{
							var arr = SiegeDataBase.GetLocalizedStringReputation(rep, ';');

							txtbxQuest.AddLine(string.Format("- репутация '{0}' больше либо равная '{1}'", arr[0], arr[1]));
						}

						flg = true;
					}

					if (quest.AcceptConditions.ReputationLess != null)
					{
						foreach (string rep in quest.AcceptConditions.ReputationLess)
						{
							var arr = SiegeDataBase.GetLocalizedStringReputation(rep, ';');

							txtbxQuest.AddLine(string.Format("- репутация '{0}' меньше, чем '{1}'", arr[0], arr[1]));
						}

						flg = true;
					}

					if (quest.AcceptConditions.ReputationEqual != null)
					{
						foreach (string rep in quest.AcceptConditions.ReputationEqual)
						{
							var arr = SiegeDataBase.GetLocalizedStringReputation(rep, ';');

							txtbxQuest.AddLine(string.Format("- репутация '{0}' строго равная '{1}'", arr[0], arr[1]));
						}

						flg = true;
					}

					if (quest.AcceptConditions.RepRequiredNotToBe != null)
					{
						foreach (string rep in quest.AcceptConditions.RepRequiredNotToBe)
						{
							var arr = SiegeDataBase.GetLocalizedStringReputation(rep, ';');

							txtbxQuest.AddLine(string.Format("- репутация '{0}' не равна '{1}'", arr[0], arr[1]));
						}

						flg = true;
					}

					if (!string.IsNullOrEmpty(quest.AcceptConditions.TitleRequired))
					{
						txtbxQuest.AddLine(string.Format("- титул игрока от '{0}'", SiegeDataBase.GetLocalizedStringTitle(quest.AcceptConditions.TitleRequired)));

						flg = true;
					}

					if (quest.AcceptConditions.MinLoyaltyOfParentHouse > 0)
					{
						txtbxQuest.AddLine(string.Format("- минимальная лояльность дома {0}", npcQuestTaker));

						flg = true;
					}

					if (quest.AcceptConditions.QuestForBuilderOfParentHouse > 0)
					{
						txtbxQuest.AddLine(string.Format("- необходимо быть владельцем дома {0}", npcQuestTaker));

						flg = true;
					}

					if (!flg)
						txtbxQuest.AddLine("- квест выдаётся без условий");
				}
				#endregion

				#region Условия выполнения квеста
				if (quest.CompleteConditions != null && !string.IsNullOrEmpty(quest.CompleteConditions.Condition))
				{
					txtbxQuest.AddLine("Условия выполнения квеста:", bold);

					var condition = quest.CompleteConditions.Condition.ToLower();

					switch (condition)
					{
						case "talk":
							txtbxQuest.AddText("- поговорить с ");
							txtbxQuest.AddLink(npcQuestGiver, "NPC:" + quest.QuestGiver);
							txtbxQuest.AddLine();
							break;

						case "item":
							var remove = quest.CompleteConditions.ConditionList.ContainsKey("RemoveItems")
							             && (int) quest.CompleteConditions.ConditionList["RemoveItems"] > 0;
							
							if (quest.CompleteConditions.ConditionList.ContainsKey("BagItem"))
							{
								var bag = (List<string>)quest.CompleteConditions.ConditionList["BagItem"];
								foreach (var item in bag)
								{
									txtbxQuest.AddText("- принести в рюкзаке ");
									txtbxQuest.AddLink(SiegeDataBase.DataLanguages.Get(item) ?? item, "item:" + item);
									txtbxQuest.AddLine(remove ? " [будет отдано за выполнение квеста]" : "");
								}
							}

							if (quest.CompleteConditions.ConditionList.ContainsKey("Item"))
							{
								var bag = (List<string>)quest.CompleteConditions.ConditionList["Item"];
								foreach (var item in bag)
								{
									var arr = item.Split(';');
									var code = arr[0].Trim();
									
									int level = 0;
									if (arr.Length >= 2)
										int.TryParse(arr[1], out level);

									var count = 1;
									if (arr.Length >= 3)
										int.TryParse(arr[2], out count);
									

									if (count > 1)
										txtbxQuest.AddText(string.Format("- принести {0} пачек ", arr[2]));
									else
										txtbxQuest.AddText("- принести пачку ");

									txtbxQuest.AddLink(SiegeDataBase.DataLanguages.Get(code) ?? code, "squad:" + code);

									if (level > 0)
										txtbxQuest.AddText(string.Format(" (+{0} ур.)", arr[1]));

									txtbxQuest.AddLine(remove ? " [будет отдано за выполнение квеста]" : "");
								}
							}

							if (quest.CompleteConditions.ConditionList.ContainsKey("EquipItem"))
							{
								var bag = (List<string>)quest.CompleteConditions.ConditionList["EquipItem"];
								foreach (var item in bag)
								{
									txtbxQuest.AddText("- наличие в башнях ");
									txtbxQuest.AddLink(SiegeDataBase.DataLanguages.Get(item) ?? item, "item:" + item);
									txtbxQuest.AddLine(remove ? " [будет отдано за выполнение квеста]" : "");
								}
							}

							if (quest.CompleteConditions.ConditionList.ContainsKey("RemoveAllItems"))
							{
								if ((int)quest.CompleteConditions.ConditionList["RemoveAllItems"] > 0)
									txtbxQuest.AddLine("[все войска игрока будут отданы за выполнение квеста]");
							}
							break;

						case "randomduel":
						case "duel":
							if (quest.CompleteConditions.ConditionList.ContainsKey("EnemyName"))
							{
								foreach (var name in (List<string>)quest.CompleteConditions.ConditionList["EnemyName"])
								{
									var count = quest.CompleteConditions.ConditionList.ContainsKey("Count")
										? (int)quest.CompleteConditions.ConditionList["Count"]
										: 1;

									txtbxQuest.AddText("- выиграть бой с ");
									txtbxQuest.AddLink((SiegeDataBase.DataLanguages.Get(name) ?? name).Trim(), "NPC:" + name);
									txtbxQuest.AddLine(count > 1 ? string.Format(" {0} раз(а)", count) : "");
								}
							}
							break;

						case "rank":
							if (quest.CompleteConditions.ConditionList.ContainsKey("Rank"))
							{
								txtbxQuest.AddLine(string.Format("- достигнуть {0} уровня", (int)quest.CompleteConditions.ConditionList["Rank"]));
							}
							break;

						case "rating":
							if (quest.CompleteConditions.ConditionList.ContainsKey("Rating"))
							{
								txtbxQuest.AddLine(string.Format("- заработать {0} боевого рейтинга", (int)quest.CompleteConditions.ConditionList["Rating"]));
							}
							break;

						case "capturecontrolpoint":
							if (quest.CompleteConditions.ConditionList.ContainsKey("ControlPoint"))
							{
								foreach (var point in (List<string>)quest.CompleteConditions.ConditionList["ControlPoint"])
								{
									txtbxQuest.AddLine(string.Format("- захватить PvP-точку '{0}'", (SiegeDataBase.DataLanguages.Get(point) ?? point).Trim()));
								}
							}
							break;

						case "pvpkill":
							var min = quest.CompleteConditions.ConditionList.ContainsKey("MinTitle")
							          	? (int) quest.CompleteConditions.ConditionList["MinTitle"]
							          	: 0;
							var minTitle = min >= 0 && min < SiegeDataBase.TitleCodes.Count
							               	? SiegeDataBase.TitleLanguage[SiegeDataBase.TitleCodes[min]]
							               	: "";

							var max = quest.CompleteConditions.ConditionList.ContainsKey("MaxTitle")
							          	? (int) quest.CompleteConditions.ConditionList["MaxTitle"]
							          	: 0;
							var maxTitle = max >= 0 && max < SiegeDataBase.TitleCodes.Count
											? SiegeDataBase.TitleLanguage[SiegeDataBase.TitleCodes[max]]
							               	: "";

							var count2 = quest.CompleteConditions.ConditionList.ContainsKey("Count")
									? (int)quest.CompleteConditions.ConditionList["Count"]
									: 1;

							txtbxQuest.AddText(string.Format("- победить в бою с игроком титула {0}{1}", minTitle, max > min ? "-" + maxTitle : ""));

							if (count2 > 1)
								txtbxQuest.AddText(string.Format(" {0} раз(а)", count2));

							txtbxQuest.AddLine();
							break;

						case "reputation":
							if (quest.CompleteConditions.ConditionList.ContainsKey("Reputation"))
							{
								foreach (string rep in (List<string>)quest.CompleteConditions.ConditionList["Reputation"])
								{
									var arr = SiegeDataBase.GetLocalizedStringReputation(rep, ';');

									txtbxQuest.AddLine(string.Format("- достигнуть репутации '{0}: {1}'", arr[0], arr[1]));
								}
							}
							break;

						case "resourse":
							var remove2 = quest.CompleteConditions.ConditionList.ContainsKey("RemoveItems")
										 && (int)quest.CompleteConditions.ConditionList["RemoveItems"] > 0;

							if (quest.CompleteConditions.ConditionList.ContainsKey("Fund"))
							{
								foreach (var res in (SiegeDataBase.ResourseDictionary)quest.CompleteConditions.ConditionList["Fund"])
								{
									txtbxQuest.AddText(string.Format("- принести {0} ед. {1}", res.Value, SiegeDataBase.DataLanguages.Get(res.Key) ?? res.Key));
									//txtbxQuest.AddLink(SiegeDataBase.DataLanguages.Get(res.Key) ?? res.Key, res.Key);
									txtbxQuest.AddLine(remove2 ? " [будет отдано за выполнение квеста]" : "");
								}
							}
							break;

						case "construction":
							if (quest.CompleteConditions.ConditionList.ContainsKey("Building"))
							{
								foreach (var res in (SiegeDataBase.ResourseDictionary)quest.CompleteConditions.ConditionList["Building"])
								{
									txtbxQuest.AddText(string.Format("- построить в замке {0} ед. ", res.Value));
									txtbxQuest.AddLink(SiegeDataBase.DataLanguages.Get(res.Key) ?? res.Key, res.Key);
									txtbxQuest.AddLine();
								}
							}
							break;

						default:
							txtbxQuest.AddText(string.Format("- действие_не_определено: {0}", condition));
							break;
					}

					txtbxQuest.AddLine();
				}

				#endregion

				#region Остальное
				if (quest.ExtractRewards != null && quest.ExtractRewards.Count > 0)
				{
					txtbxQuest.AddLine("Выкапываемые награды:", bold);

					foreach (var ext in quest.ExtractRewards)
						txtbxQuest.AddLine(string.Format("- {1} ед. {0}", SiegeDataBase.DataLanguages.Get(ext.Key) ?? ext.Key, ext.Value));

					txtbxQuest.AddLine();
				}

				if (!string.IsNullOrEmpty(quest.Teleport.Point))
				{
					txtbxQuest.AddLine("Телепортирование игрока после сдачи квеста:", bold);

					txtbxQuest.AddLine(string.Format("- в данной версии карты не подключены (координаты: {0})", quest.Teleport.Point));

					txtbxQuest.AddLine();
				}
				#endregion
			}
		}

		#region Табы сквада
		private TabPage _tabSquadCommon;
		private TabPage _tabSquadTTH;
		private TabPage _tabSquadUpgrade;
		private TabPage _tabSquadCalc;
		#endregion

		private bool internalUpdate = false;

		/// <summary>
		/// Обновление суб-страницы информации по зданиям
		/// </summary>
		/// <param name="building"></param>
		/// <param name="subPage"></param>
		private void UpdateBuildingSubInfo(SiegeBuildingStructure building, int subPage)
		{
			if (building == null)
				building = new SiegeBuildingStructure();

			// обновим блок требований для постройки здания
			if (subPage < 0 || subPage == 0)
				_controlBuildingRequirements.UpdateControl(building.Requirements, chbxIconSize.Checked);

			// обновим список открытой линейки зданий
			if (subPage < 0 || subPage == 1)
				_controlBuildingAfterBuild.UpdateControl(building.AfterBuild, chbxIconSize.Checked);

			// обновим инфу по стоимости постройки
			if (subPage < 0 || subPage == 2)
			{
				lblBuildingConstTime.Text = GetTimeString(building.BuildTime);
				_controlBuildingConstructionCost.UpdateControl(building.BuildResources, chbxIconSize.Checked);
			}

			// обновим инфу по продукции постройки
			if (subPage < 0 || subPage == 3)
			{
				if (building.Production != null && building.Production.Products.Count > 0)
				{
					lblBuildingProductionTime.Text = GetTimeString(building.Production.Time);

					switch (building.Production.Type)
					{
						case SiegeBuildingStructure.ProductionType.Artefact:
							lblBuildingProductionType.Text = "Артефакт/ресурс";
							break;

						case SiegeBuildingStructure.ProductionType.Unit:
							lblBuildingProductionType.Text = "Юнит";
							break;

						default:
							lblBuildingProductionType.Text = "<не_определено>";
							break;
					}

					_controlBuildingProductionList.UpdateControl(building.Production.Products, chbxIconSize.Checked);
					_controlBuildingProductionCost.UpdateControl(building.Production.Resources, chbxIconSize.Checked);
				}
				else
				{
					lblBuildingProductionType.Text = "-";
					lblBuildingProductionTime.Text = "-";

					_controlBuildingProductionList.Clear();
					_controlBuildingProductionCost.Clear();
				}
			}

			// обновим инфу по апгрейду здания
			if (subPage < 0 || subPage == 4)
			{
				if (building.UpgradeInfo != null)
				{
					lblBuildingUpgradeMinLevel.Text = building.UpgradeInfo.MinLevel.ToString();
					_controlBuildingUpgrade.UpdateControl(building.UpgradeInfo.Resources, chbxIconSize.Checked);
					_controlBuildingUpgrade2.UpdateControl(building.UpgradeInfo.Resources2, chbxIconSize.Checked);
				}
				else
				{
					lblBuildingUpgradeMinLevel.Text = "-";
					_controlBuildingUpgrade.Clear();
					_controlBuildingUpgrade2.Clear();
				}
			}
		}

		/// <summary>
		/// Обновление суб-страницы информации по войскам
		/// </summary>
		/// <param name="squad"></param>
		/// <param name="subPage"></param>
		private void UpdateSquadSubInfo(SiegeSquadStructure squad, string subPageTag)
		{
			if (squad == null)
				squad = new SiegeSquadStructure();


			if (subPageTag == "*" || subPageTag == "Common")
			{
				#region Общие характеристики
				var txt = new StringBuilder();

				txt.AppendLine("Общедоступная информация");
				txt.AppendLine();

				if (squad.SoulInfo != null)
				{
					txt.AppendLine("Броня: " + squad.SoulInfo.ArmorAbs);
					txt.AppendLine("Здоровье: " + squad.SoulInfo.HP);
				}

				if (squad.UnitWeaponInfo != null)
				{
					if (squad.UnitWeaponInfo.Damage != null)
						txt.AppendLine(string.Format("Наносимый урон: {0} - {1}",
													 squad.UnitWeaponInfo.Damage.HpMin, squad.UnitWeaponInfo.Damage.HpMax));

					txt.AppendLine(string.Format("Дальность атаки: {0} - {1}", squad.UnitWeaponInfo.RadiusMin, squad.UnitWeaponInfo.RadiusMax));
				}

				txt.AppendLine();

				txt.AppendLine("Требует очков командования: " + squad.BattleInfo.RemoveCommandPoints);

				txt.AppendLine("Поглощает очков командования в бою: " + squad.BattleInfo.CommandPointsRegenPrice);

				if (squad.SoulInfo != null)
				{
					txt.AppendLine(string.Format("Скорость передвижения: {0} м/с",
												 squad.UnitMoverInfo.MaxSpeed));
				}

				if (squad.UnitWeaponInfo != null)
					txt.AppendLine(string.Format("Пауза между атаками: {0} сек", squad.UnitWeaponInfo.TCoolDown + squad.UnitWeaponInfo.TShoot));

				txt.AppendLine(string.Format("Время готовности: {0} сек", squad.BattleInfo.CoolDown));
				txt.AppendLine("Количество в отряде: " + squad.BattleInfo.ChargeCount);

				var requires = new List<string>();
				if (squad.ParentBuilding != null)
				{
					foreach (var rep in squad.RequiredInfo.Reputation)
					{
						requires.Add(SiegeDataBase.GetLocalizedStringRequirements("Reputation-" + rep.Replace(';', '-')));
					}

					if (squad.RequiredInfo.RequiredRank > 0)
						requires.Add("Уровень: " + squad.RequiredInfo.RequiredRank);

					if (squad.RequiredInfo.RequiredRate > 0)
						requires.Add("Боевой рейтинг: " + squad.RequiredInfo.RequiredRate);

					if (squad.BattleInfo.MaxEquipCount > 1)
						requires.Add("Максимально допустимо к экипировке: " + squad.BattleInfo.MaxEquipCount);
				}

				if (requires.Count > 0)
				{
					txt.AppendLine();
					txt.AppendLine("ТРЕБОВАНИЯ:");
					foreach (var req in requires)
						txt.AppendLine("  " + req);

				}

				txtbxSquadBattleDesc.Text = txt.ToString();
				#endregion
			}

			if (subPageTag == "*" || subPageTag == "Upgrade")
			{
				#region Информация по апгрейду
				var title = new string[]
				            	{
				            		"Улучшения юнита:",
				            		"Расчёт для "+ maxSquadLevel +"-го уровня:"
				            	};

				var txt = new StringBuilder();

				txt.AppendLine("Апгрейд армии");
				txt.AppendLine();

				if (squad.UnitUpgradeInfo.Count > 0)
					for (var k = 1; k <= maxSquadLevel; k = k + 19)
					{
						txt.AppendLine(title[k / maxSquadLevel]);

						foreach (var inf in squad.UnitUpgradeInfo)
						{
							var str = SiegeDataBase.DataLanguages.Get(inf.Key);
							var val = inf.Value * k;
							var plus = val > 0 ? "+" : "";
							txt.AppendLine(string.Format("  {0}: {1}{2}", !string.IsNullOrEmpty(str) ? str : inf.Key,
														 plus, val));
						}

						txt.AppendLine();
					}
				else
					txt.AppendLine("Апгрейду не подлежит.");

				txtbxSquadUpgradeInfo.Text = txt.ToString();
				#endregion
			}

			if (subPageTag == "*" || subPageTag == "TTH")
			{
				#region Информация по ТТХ
				var txt = new StringBuilder();

				txt.AppendLine("Тактико-технические характеристики юнита");

				txt.AppendLine();
				txt.AppendLine("ЗАЩИТА");

				if (squad.SoulInfo != null)
				{
					txt.AppendLine(string.Format("  Здоровье: {0}", squad.SoulInfo.HP));
					txt.AppendLine(string.Format("  Броня: {0}", squad.SoulInfo.ArmorAbs));

					if (squad.SoulInfo.Regeneration > 0)
						txt.AppendLine(string.Format("  Регенерация: {0}", squad.SoulInfo.Regeneration));

					if (squad.SoulInfo.DodgeChance > 0)
						txt.AppendLine(string.Format("  Шанс блокирования урона: {0}%", squad.SoulInfo.DodgeChance * 100));

					if (squad.SoulInfo.ReflectChance > 0)
						txt.AppendLine(string.Format("  Шанс отражения атаки: {0}%", squad.SoulInfo.ReflectChance * 100));

					if (squad.SoulInfo.HealOnDamageChance > 0)
					{
						txt.AppendLine(string.Format("  Шанс вылечиться вовремя атаки: {0}%", squad.SoulInfo.HealOnDamageChance * 100));
						txt.AppendLine(string.Format("  ~ величина лечения во время атаки: {0}", squad.SoulInfo.HealOnDamageValue));
					}

					if (squad.SoulInfo.Resilience > 0)
						txt.AppendLine(string.Format("  Уменьшение крит.шанса атакующего: {0}", squad.SoulInfo.Resilience));
				}

				txt.AppendLine();
				txt.AppendLine("АТАКА");

				if (squad.UnitWeaponInfo != null)
				{
					if (squad.UnitWeaponInfo.Damage != null)
					{
						txt.AppendLine(string.Format("  Наносимый урон: {0} - {1}", squad.UnitWeaponInfo.Damage.HpMin,
													 squad.UnitWeaponInfo.Damage.HpMax));

						var str = "- не определена -";
						switch (squad.UnitWeaponInfo.Damage.Type.ToLower())
						{
							case "normal":
								str = "обычный (физический)";
								break;
							case "acid":
								str = "разъедающий (кислотный)";
								break;
							case "ice":
								str = "замедляющий (замораживающий)";
								break;
							default:
								str = squad.UnitWeaponInfo.Damage.Type;
								break;
						}

						txt.AppendLine(string.Format("  Тип урона: {0}", str));
					}

					txt.AppendLine(string.Format("  Дистанция атаки: {0} - {1} м", squad.UnitWeaponInfo.RadiusMin, squad.UnitWeaponInfo.RadiusMax));

					txt.AppendLine(string.Format("  Радиус поражения: {0} м ({1})", squad.UnitWeaponInfo.RadiusAttack,
												 squad.UnitWeaponInfo.AimAtCenter ? "строго по центру" : "по ближайшей цели"));

					txt.AppendLine();

					txt.AppendLine(string.Format("  Направление атак: по {0}",
												 squad.UnitWeaponInfo.DamageAddress.ToLower() == "enemies"
													? "врагу"
													: squad.UnitWeaponInfo.DamageAddress.ToLower() == "friends" ? "своим" : "всем (своим и чужим)"));

					txt.AppendLine(string.Format("  Количество возможных атак: {0}",
												 squad.UnitWeaponInfo.ChargeCount >= 0
													? squad.UnitWeaponInfo.ChargeCount.ToString()
													: "не ограничено"));

					if (squad.UnitWeaponInfo.RemoveCommandPoints > 0)
						txt.AppendLine(string.Format("  'Съедает' ОК за выстрел: {0} ОК", squad.UnitWeaponInfo.RemoveCommandPoints));

					txt.AppendLine(string.Format("  Время перезарядки орудия: {0} с", squad.UnitWeaponInfo.TCoolDown));

					txt.AppendLine(string.Format("  Время атаки/полёта снаряда: {0} с", squad.UnitWeaponInfo.TShoot));


					if (squad.UnitWeaponInfo.Damage != null)
					{
						if (squad.UnitWeaponInfo.Damage != null && squad.UnitWeaponInfo.Damage.AoT.Count > 0)
						{
							txt.AppendLine();
							txt.AppendLine("БАФЫ/ДЕБАФЫ");

							foreach (SiegeAoTStructure aot in squad.UnitWeaponInfo.Damage.AoT)
							{
								var aot_value = aot.Value;
								var aot_value_str = "";
								if (aot.Damage != null)
									aot_value_str = string.Format("{0} - {1}", aot.Damage.HpMin, aot.Damage.HpMax);

								var aot_desc = "";
								var aot_pt = "";
								switch (aot.Type)
								{
									case "RechargeWeaponTime":
										aot_desc = aot_value > 0 ? "Ускорение скорости атаки" : "Замедление скорости атаки";
										break;
									case "WeaponRangeMin":
										aot_desc = "Кинжальная точность (сокращение мёртвой зоны)";
										aot_pt = " м";
										break;
									case "DoT":
										aot_desc = "Урон";
										break;
									case "SpeedProc":
										aot_desc = aot_value > 0 ? "Ускоренние" : "Замедление";
										aot_value = aot_value * 100;
										aot_pt = "%";
										break;
									case "Speed":
										aot_desc = aot_value > 0 ? "Ускорение" : "Замедление";
										aot_pt = " м/с";
										break;
									default:
										aot_desc = aot.Name;
										break;
								}

								aot_value = Math.Abs(aot_value);

								var str = new StringBuilder();

								str.AppendFormat("  {0} на {1}{2}", aot_desc,
														aot_value_str == "" ? aot_value.ToString() : aot_value_str,
														aot_pt);
								if (aot.Period > 1)
									str.AppendFormat(" серией в {0} раз(а)", aot.Period);

								if (aot.LifeTime > 0)
									str.AppendFormat(" длительностью {0} с", aot.LifeTime);

								if (aot.Stackable)
									str.Append(" (кумулятивно)");

								txt.AppendLine(str.ToString());
							}
						}
					}
				}

				if (squad.UnitAiInfo != null)
				{
					txt.AppendLine();
					txt.AppendLine("ПРИОРИТЕТ АТАКИ");

					var t = 1;
					var arr = new SiegeCommonDataDictionary<string>()
				          	{
                                {"special", "Приоритетные цели (по пятиугольной схеме)"},
								{"equalclass", "Цель своего класса"},
								{"common", "Ближайшая цель"},
								{"castle", "Замок"}
				          	};

					foreach (string target in squad.UnitAiInfo.TargetType)
					{
						var str = arr.ContainsKey(target) ? arr[target] : target;
						txt.AppendLine(string.Format("  {0}) {1}", t, str));

						t++;
					}
				}

				txtbxSquadTTH.Text = txt.ToString();

				#endregion
			}

			if (subPageTag == "*" || subPageTag == "Calc")
			{
				#region Информация по стоимости
#if !LIMITED
				bool disabled = SiegeMarket.Items.Count == 0;
				var upgrade = squad.UnitUpgradeInfo != null && squad.UnitUpgradeInfo.Count > 0;

				internalUpdate = true;

				numSquadCalcLevel.Enabled = upgrade;
				numSquadCalcLevel.Value = (decimal)(upgrade ? squad.UpgradeToLevelValue : 0);

				numSquadCalcTryCounts.Enabled = upgrade;
				numSquadCalcTryCounts.Value = (decimal)(upgrade ? squad.UpgradeToLevelInTry : 0);

				numSquadCalcTryDiamonds.Enabled = upgrade;
				numSquadCalcTryDiamonds.Value = (decimal)(upgrade ? squad.UpgradeToLevelInDiamonds : 0);

				chbxSquadCalcLastLevels.Enabled = upgrade;
				chbxSquadCalcLastLevels.Checked = upgrade ? squad.UpgradeToLevelOnlyLast : false;

				internalUpdate = false;

				var txt = new StringBuilder();
				var goldBuy = 0.0;
				var goldSell = 0.0;
				var sbBuy = new StringBuilder();
				var sbSell = new StringBuilder();

				if (!disabled)
				{
					foreach (var res in squad.RequiredRes)
					{
						if (res.Key != "gold" && !SiegeMarket.Items.ContainsKey(res.Key))
						{
							disabled = true;
							break;
						}

						if (res.Key != "gold")
						{
							var item = SiegeMarket.Items[res.Key];

							var gold = item.FormulaCalc && item.FormulaCalcEnabled && !item.FormulaCalcDisabled
										? item.CalcGold
										: item.UserGold;

							goldBuy += gold[(byte)SiegeMarket.PriceType.Buy] * res.Value;
							goldSell += gold[(byte)SiegeMarket.PriceType.Sell] * res.Value;

							sbBuy.AppendLine(string.Format("  {0} шт х {1} зол ({2})", res.Value,
														   Math.Round(gold[(byte)SiegeMarket.PriceType.Buy], 2),
														   SiegeDataBase.DataLanguages.Get(res.Key)));
							sbSell.AppendLine(string.Format("  {0} шт х {1} зол ({2})", res.Value,
															Math.Round(gold[(byte)SiegeMarket.PriceType.Sell], 2),
															SiegeDataBase.DataLanguages.Get(res.Key)));
						}
						else
						{
							goldBuy += res.Value;
							goldSell += res.Value;

							sbBuy.AppendLine(string.Format("  {0} золотых монет (Золото)", res.Value));
							sbSell.AppendLine(string.Format("  {0} золотых монет (Золото)", res.Value));
						}
					}
				}

				numSquadCalcPack.Tag = squad.Code;

				if (!disabled)
				{
					txt.AppendLine("Формула покупки юнита:");
					txt.AppendLine(sbBuy.ToString().TrimEnd());
					txt.AppendLine("Итого: " + calcPriceStr(goldBuy, 1, SiegeMarket.PriceType.Buy));
					txt.AppendLine();

					txt.AppendLine("Формула продажи юнита:");
					txt.AppendLine(sbSell.ToString().TrimEnd());
					txt.AppendLine("Итого: " + calcPriceStr(goldSell, 1, SiegeMarket.PriceType.Sell));
					txt.AppendLine();

					lblSquadCalcBuy.Tag = goldBuy;
					lblSquadCalcSell.Tag = goldSell;

					numSquadCalc_ValueChanged(numSquadCalcPack, EventArgs.Empty);

					txtbxSquadCalc.Text = txt.ToString().Trim();
				}
				else
				{
					lblSquadCalcBuy.Text = calcDisable;
					lblSquadCalcSell.Text = calcDisable;
					txtbxSquadCalc.Text = calcDisable;
				}
#else
				tabSquadCalc.Enabled = false;
				txtbxSquadCalc.Text = calcDisable;
#endif
				#endregion
			}
		}

		// пересчёт стоимости сквада
		private void numSquadCalc_ValueChanged(object sender, EventArgs e)
		{
#if !LIMITED
			var control = (Control)sender;

			if (control == null || numSquadCalcPack.Tag == null)
				return;

			var codeSquad = numSquadCalcPack.Tag.ToString();

			// сохраним состояние
			if (control.Name != "numSquadCalcPack" && !internalUpdate)
			{
				SiegeSquadStructure squad;
				if (!SiegeDataBase.DataSquads.Get(codeSquad, out squad))
					return;

				if (control.Name.StartsWith("numSquadCalcLevel"))
					squad.UpgradeToLevelValue = (double)(((NumericUpDown)sender).Value);

				if (control.Name.StartsWith("numSquadCalcTryCounts"))
					squad.UpgradeToLevelInTry = (double)(((NumericUpDown)sender).Value);

				if (control.Name.StartsWith("numSquadCalcTryDiamonds"))
					squad.UpgradeToLevelInDiamonds = (double)(((NumericUpDown)sender).Value);

				if (control.Name == "chbxSquadCalcLastLevels")
					squad.UpgradeToLevelOnlyLast = ((CheckBox)sender).Checked;
			}

			// стоимость производства юнита
			var gold = new double[2];
			gold[priceBuy] = (double)lblSquadCalcBuy.Tag;
			gold[priceSell] = (double)lblSquadCalcSell.Tag;

			// стоимость на уровень
			var goldToLevel = new double[2];
			goldToLevel[priceBuy] = 0;
			goldToLevel[priceSell] = 0;

			// стоимость 1 попытки апгрейда
			SiegeBuildingStructure build;
			if (numSquadCalcLevel.Enabled && numSquadCalcLevel.Value > 0 && SiegeDataBase.DataBuildings.GetBySquad(codeSquad, out build))
				for (byte priceType = 0; priceType < 2; priceType++)
				{
					// средняя стоимость ресов на 1 попытку апгрейда
					var goldMiddle = 0.0;

					if (build.UpgradeInfo != null)
					{
						var middle = 0.0;

						foreach (var res in build.UpgradeInfo.Resources2)
						{
							// стоимость текущего ресурса
							double g;

							// затраты на 1 попытку апгрейда
							if (res.Key == "gold")
								g = 1;
							else
								g = (SiegeMarket.Items[res.Key].FormulaCalc && SiegeMarket.Items[res.Key].FormulaCalcEnabled &&
											   !SiegeMarket.Items[res.Key].FormulaCalcDisabled
												? SiegeMarket.Items[res.Key].CalcGold[priceType]
												: SiegeMarket.Items[res.Key].UserGold[priceType]);

							goldToLevel[priceType] += g * res.Value;

							// затраты на каждый шаг апгрейда
							middle += g;
						}

						if (goldMiddle == 0)
							goldMiddle = middle / build.UpgradeInfo.Resources2.Count();
					}

					// стоимость в бриллиантах (на апгрейд)
					goldToLevel[priceType] += (double)numSquadCalcTryDiamonds.Value * SiegeMarket.DiamondPrice[priceType];

					// добавим стоимость каждого шага в попытке
					goldToLevel[priceType] += (double)numSquadCalcTryCounts.Value * goldMiddle * 11;
				}


			// добавим стоимость бриллиантов
			gold[priceBuy] += chbxSquadCalcLastLevels.Checked ? goldToLevel[priceBuy] : goldToLevel[priceBuy] * (double)numSquadCalcLevel.Value;
			gold[priceSell] += chbxSquadCalcLastLevels.Checked ? goldToLevel[priceSell] : goldToLevel[priceSell] * (double)numSquadCalcLevel.Value;

			if (lblSquadCalcBuy.Tag != null)
			{
				lblSquadCalcBuy.Text = string.Format("Покупка : {0}", calcPriceStr(gold[priceBuy], (double)numSquadCalcPack.Value, SiegeMarket.PriceType.Buy));
				lblSquadCalcSell.Text = string.Format("Продажа: {0}", calcPriceStr(gold[priceSell], (double)numSquadCalcPack.Value, SiegeMarket.PriceType.Sell));
			}
			else
			{
				lblSquadCalcBuy.Text = "";
				lblSquadCalcSell.Text = "";
			}
#endif
		}

		// расчёт стоимости апгрейда юнита
		private string calcPriceStr(double price, double num, SiegeMarket.PriceType priceType)
		{
			var gold = price * num;

			var diamond = gold / SiegeMarket.DiamondPrice[(byte)priceType];

			return string.Format("{0} зол ({1} бр)", Math.Round(gold, 2),
											Math.Round(diamond, 2));
		}

		/// <summary>
		/// Преобразовать время в строку
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		private static string GetTimeString(double time)
		{
			if (time == 0)
				return "-";

			var str = "";

			var hour = (int)time;
			var minutes_all = time - hour;
			var minutes = 60 * minutes_all % 60;
			var seconds = minutes % 1;

			// расчитаем окончание
			if (hour > 0)
			{
				var str2 = "";
				var h = hour % 10;
				if (h >= 2 && h <= 4 && (hour < 10 || hour > 20))
					str2 = "а";
				else if (h == 1 && hour > 20)
					str2 = "";
				else
					str2 = "ов";

				str += string.Format("{0} час{1} ", hour, str2);
			}

			if ((int) minutes > 0)
			{
				var str2 = "";
				var m = minutes % 10;
				if (m >= 2 && m <= 4 && (minutes < 10 || minutes > 20))
					str2 = "ы";
				else if (m == 1 && minutes > 20)
					str2 = "а";
				else
					str2 = "";

				str += string.Format("{0} минут{1} ", Math.Round(minutes, 2), str2);
			}

			if ((int) seconds > 0)
			{
				var str2 = "";
				var s = seconds % 10;
				if (s >= 2 && s <= 4 && (minutes < 10 || minutes > 20))
					str2 = "ы";
				else if (s == 1 && minutes > 20)
					str2 = "а";
				else
					str2 = "";

				str += string.Format("{0} секунд{1} ", Math.Round(minutes, 2), str2);
			}

			if (hour == 0 && (int)minutes == 0 && Math.Round(seconds) == 0)
			{
				str = "менее секунды";
			}

			return str;
		}

		// обработчик кликов на линки
		private void ControlListLinkTextOnClick(object sender, LinkLabelLinkClickedEventArgs args)
		{
			if (sender == null || ((Control)sender).Tag == null)
				return;

			var code = ((Control)sender).Tag.ToString().ToLower();

			LinkClick(code);
		}

		/// <summary>
		/// Обработчик перекрёстных ссылок
		/// </summary>
		/// <param name="code"></param>
		private void LinkClick(string code)
		{
			if (SiegeDataBase.DataBuildings.ContainsKey(code))
			{
				InitPageBuildings();
				tabsMain.SelectTab(1);

				dataGridBuildings.ClearSelection();
				dataGridBuildings.Tag = code;

				if (_updateInfoEnabled)
					UpdateInfo();

				dataGridBuildings.Refresh();

				return;
			}

			if (SiegeDataBase.DataSquads.ContainsKey(code))
			{
				InitPageSquads();
				tabsMain.SelectTab(2);

				dataGridSquads.ClearSelection();
				dataGridSquads.Tag = code;

				if (_updateInfoEnabled)
					UpdateInfo();

				dataGridSquads.Refresh();

				return;
			}

			if (SiegeDataBase.DataQuests.ContainsKey(code))
			{
				InitPageQuests();
				tabsMain.SelectTab(3);

				dataGridQuests.ClearSelection();
				dataGridQuests.Tag = code;

				if (_updateInfoEnabled)
					UpdateInfo();

				dataGridQuests.Refresh();

				return;
			}


			MessageBox.Show("Упс! А информация то есть только по зданиям, войскам и квестам.\n\rА тута не понятно чаво... о_О", "Аларма! Аларма! Аларма!",
							MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		#region Загрузка/выгрузка настроек

		/*
		/// <summary>
		/// Сохранить настройки программы
		/// </summary>
		private void SettingsSave()
		{
			GamePath = tbxBasePath.Text;
			if (GamePath != "")
				Config.WriteValue("SiegeOnline", "InstalledPath", tbxBasePath.Text);

			Config.WriteValue("SiegeOnline", "CurrentLanguage", CurrentLanguage);
		}
		*/

		/// <summary>
		/// Загрузить настройки программы
		/// </summary>
		private void SettingsLoad()
		{
			SiegeDataBase.GamePath = "";

			RegistryKey regGame = Registry.CurrentUser.OpenSubKey("Software\\QuantGames\\Siege Online");

			if (regGame != null)
				SiegeDataBase.GamePath = (string)regGame.GetValue("InstallPath");

			if (SiegeDataBase.GamePath == "" && File.Exists(Path.Combine(Application.StartupPath, "SiegeOnline.exe")))
				SiegeDataBase.GamePath = Application.StartupPath;

			if (SiegeDataBase.GamePath == "")
			{
				MessageBox.Show("Папка размещения игры не найдена. Попробуйте переустановить игру.", "Ошибка", MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				return;
			}


			tbxBasePath.Text = SiegeDataBase.GamePath;

			if (SiegeDataBase.GamePath != "")
			{
				var dir = Path.Combine(SiegeDataBase.GamePath, @"Data\Misc");
				if (!Directory.Exists(dir))
				{
					MessageBox.Show("Директория данных повреждена. Попробуйте переустановить игру.", "Ошибка", MessageBoxButtons.OK,
									MessageBoxIcon.Error);
					return;
				}

				LocalizationInit(dir);
			}
			else
			{
				MessageBox.Show("НЕ удалось определить папку с игрой. Необходимо указать её место расположения.", "Внимание",
								MessageBoxButtons.OK,
								MessageBoxIcon.Information);
			}

			/*
			// загрузим инфу по ресам
			var loader = new SiegeFileLoader();
			var file = Path.Combine(GamePath, @"Data\Misc\ResourcePairs.dat");

			List<string> pairs;
			loader.LoadListFromFile(file, out pairs);
			*/

			//txtbxHistory.Text = Resources.History;
		}

		#endregion


		//==================================================================================

		// инициализация во время загрузки формы
		private void frmMain_Load(object sender, EventArgs e)
		{
			// Проинициализируем внешний вид приложения
			Text += string.Format(" (версия {0})", PathHelper.Version);

#if LIMITED
			Text += " Limited Edition (ограниченная версия)";
#endif

#if DEBUG
			Text += " Debug (отладочная версия)";
#endif

			#region Инициализация контролов списков

			const int leftList = 4;

			#region Страница требований к постройке здания

			_controlBuildingRequirements = new IconListControl
											{
												ParentControl = tabBuildingRequirements,
												Left = leftList,
												Top = 3,
												Width = -10,
												LabelFormat = " •{0}•{1}| x{0}",
												LabelToLower = false,
												LinkedText = true,
												Title = "Необходимые условия для постройки:"
											};
			_controlBuildingRequirements.OnClick += ControlListLinkTextOnClick;

			#endregion

			#region Страница доступных зданий после постройки

			_controlBuildingAfterBuild = new IconListControl
											{
												ParentControl = tabBuildingAfterBuild,
												Left = leftList,
												Top = 3,
												Width = -10,
												LabelFormat = " •{0}•{1}| (после {0} шт.)",
												LabelToLower = false,
												LinkedText = true,
												Title = "Доступные после постройки здания:"
											};
			_controlBuildingAfterBuild.OnClick += ControlListLinkTextOnClick;

			#endregion

			#region Страница стоимости постройки здания

			_controlBuildingConstructionCost = new IconListControl
												{
													ParentControl = tabBuildingConstruction,
													Left = leftList,
													Top = 18,
													Width = -10,
													LabelFormat = " {1} •{0}•",
													//LinkedText = true,
													Title = "Необходимые для постройки ресурсы:",
													LabelToLower = true
												};
			//_controlBuildingConstructionCost.OnClick += ControlBuildingConstructionCostOnOnClick;

			#endregion

			#region Страница производимой продукции (список продукции)

			_controlBuildingProductionList = new IconListControl
												{
													ParentControl = tabBuildingProducts,
													Left = leftList,
													Top = 33,
													Width = -10,
													LabelFormat = " •{0}•{1}| x{0}",
													LinkedText = true,
													Title = "Список производимой продукции:",
													LabelToLower = false
												};
			_controlBuildingProductionList.OnClick += ControlListLinkTextOnClick;

			#endregion

			#region Страница производимой продукции (список необходимых ресурсов)

			_controlBuildingProductionCost = new IconListControl
												{
													ParentControl = tabBuildingProducts,
													Left = leftList,
													Top = 50,
													Width = -10,
													LabelFormat = " •{0}•{1}| x{0}",
													LabelToLower = false,
													//LinkedText = true,
													Title = "Необходимые для производства ресурсы:"
												};
			// свяжем два списка
			_controlBuildingProductionList.LinkedControl = _controlBuildingProductionCost;

			#endregion

			#region Страница апгрейда здания (список основных ресов)

			_controlBuildingUpgrade = new IconListControl
										{
											ParentControl = tabBuildingUpgrade,
											Left = leftList,
											Top = 18,
											Width = -10,
											LabelFormat = " {1} •{0}•",
											LabelToLower = true,
											//LinkedText = true,
											Title = "Необходимые для апгрейда ресурсы:"
										};

			#endregion

			#region Страница апгрейда здания (список общих ресов)

			_controlBuildingUpgrade2 = new IconListControl
										{
											ParentControl = tabBuildingUpgrade,
											Left = leftList,
											Top = 50,
											Width = -10,
											LabelFormat = " {1} •{0}•",
											LabelToLower = true,
											//LinkedText = true,
											Title = "Из основных ресурсов:"
										};
			// свяжем два списка
			_controlBuildingUpgrade.LinkedControl = _controlBuildingUpgrade2;

			#endregion

			#endregion


			var bold = FontStyle.Bold;

			ControlTools.RichTextAddMessage(richTextBoxAbout, "ОСАДА ОНЛАЙН", Color.Empty, bold);
			/*ControlTools.RichTextAddMessage(richTextBoxAbout, Environment.NewLine);
			ControlTools.RichTextAddMessage(richTextBoxAbout, Environment.NewLine);
			ControlTools.RichTextAddMessage(richTextBoxAbout, "Если эта программа-справочник Вам понравилась и вы желаете поблагодарить её автора", Color.Red, bold);*/

			// разукрашка титульной страницы
			var arr = new string[]
			          	{
                            "Уникальная боевая система",
                            "Развитие собственного замка",
							"Захват и контроль территорий",
                            "Дипломатия, торговля, охота, геология, грабежи и зоны свободного ПВП"
			          	};
			foreach (var s in arr)
			{
				var i1 = richTextBoxAbout.Text.IndexOf(s);
				if (i1 >= 0)
				{
					richTextBoxAbout.SelectionStart = i1;
					richTextBoxAbout.SelectionLength = s.Length;
					richTextBoxAbout.SelectionFont = new Font(richTextBoxAbout.Font, bold);
				}
			}


		}

		private void frmMain_ResizeEnd(object sender, EventArgs e)
		{
			statProgressBar.Width = statusBar.ClientSize.Width - statText.Width - 16;
		}

		/// Переключение закладок страниц приложения
		private void tabPages_SelectedIndexChanged(object sender, EventArgs e)
		{
			statText.Text = "Осада Онлайн (www.siegeonline.ru)";

			if (tabsMain.SelectedIndex != 0 /*&& tabsMain.SelectedIndex != tabsMain.TabCount - 1*/)
			{
				if (timerDonate != null)
					timerDonate.Stop();

				if (String.IsNullOrEmpty(tbxBasePath.Text) || !Directory.Exists(tbxBasePath.Text))
				{
					MessageBox.Show("Не удалось найти папку с игрой. Проверьте правильность настройки пути!",
									"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					tabsMain.SelectTab(0);
					return;
				}

				SiegeDataBase.GamePath = tbxBasePath.Text;

				if (SiegeDataBase.DataLanguages.Count == 0)
				{
					MessageBox.Show("Не найдена информация по локализации. Проверьте правильность настройки локализации!",
									"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					tabsMain.SelectTab(0);
					return;
				}

				if (tabsMain.SelectedIndex >= 1 /*&& tabsMain.SelectedIndex <= 2*/)
				{
					InitPageBuildings();
					InitPageSquads();
					InitPageQuests();
				}
			}
			else
			{
				// инициализация первой страницы
				if (/*lblDonate.Visible &&*/ timerDonate == null)
				{
					timerDonate = new Timer();
					timerDonate.Interval = 100;
					timerDonate.Tick += TimerDonateOnTick;
				}

				//timerDonate.Start();
			}
		}

		// инициализация страницы войск
		private void InitPageSquads()
		{
			if (_dtSquadsList.Rows.Count == 0 || tabSquads.Tag == null || tabSquads.Tag.ToString() != CurrentLanguage)
			{
				tabSquads.Tag = CurrentLanguage;

				/*if (SiegeDataBase.DataSquads.Count == 0)
					SiegeDataBase.DataSquads.LoadFromFile(statProgressBar);*/

				_dtSquadsList = new DataTable();
				_dtSquadsList.Columns.Add("name", typeof(string));
				_dtSquadsList.Columns.Add("code", typeof(string));
				_dtSquadsList.Columns.Add("level", typeof(int));
				_dtSquadsList.Columns.Add("basic", typeof(int));
				_dtSquadsList.Columns.Add("rank", typeof(int));
				_dtSquadsList.Columns.Add("guild", typeof(int));
				_dtSquadsList.Columns.Add("upgrade", typeof(int));

				foreach (var list in SiegeDataBase.DataSquads)
				//.Where(pair => !String.IsNullOrEmpty(pair.Value.Name)).OrderBy(pair => pair.Value.Name))
				{
					var dr = _dtSquadsList.NewRow();
					dr["name"] = SiegeDataBase.GetLocalizedUnitName(list.Value.Code) ?? "? " + list.Value.Code; //String.IsNullOrEmpty(list.Value.NameDescription) ? "? " + list.Value.Name : list.Value.NameDescription;

					if (list.Value.ParentBuilding == null)
						dr["name"] = dr["name"] + " [NPC]";

					dr["code"] = list.Value.Code;
					dr["level"] = list.Value.ParentBuilding == null ? 0 : list.Value.RequiredInfo.RequiredRank;

					var orderland = -1;
					foreach (var rep in list.Value.RequiredInfo.Reputation)
					{
						orderland = rep.IndexOf("OrderLand");

						if (orderland >= 0)
							break;
					}


					dr["rank"] = list.Value.ParentBuilding == null ? 0 : Math.Sign(list.Value.RequiredInfo.RequiredRate);
					dr["guild"] = list.Value.ParentBuilding == null ? 0 : orderland + 1;
					dr["upgrade"] = list.Value.ParentBuilding == null ? 0 : list.Value.UnitUpgradeInfo.Count;
					dr["basic"] = list.Value.ParentBuilding == null ? 0 : ((int)dr["rank"] + (int)dr["guild"] + (int)dr["upgrade"] == 0 ? 1 : 0);

					_dtSquadsList.Rows.Add(dr);
				}

				_dtSquadsListView = _dtSquadsList.DefaultView;

				dataGridSquads.DataSource = _dtSquadsListView;
				dataGridSquads.Columns[0].Width = dataGridBuildings.ClientSize.Width;
				dataGridSquads.Columns[0].HeaderText = "Наименование";
				//dataGridSquads.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				dataGridSquads.Columns[1].Visible = false;
				dataGridSquads.Columns[2].Width = 35;
				//dataGridSquads.Columns[2].Visible = false;
				dataGridSquads.Columns[3].Width = 35;
				//dataGridSquads.Columns[3].Visible = false;
				dataGridSquads.Columns[4].Width = 35;
				//dataGridSquads.Columns[4].Visible = false;
				dataGridSquads.Columns[5].Width = 35;
				//dataGridSquads.Columns[5].Visible = false;
				dataGridSquads.Columns[6].Width = 35;

				statText.Text = string.Format("Всего записей: {0}", dataGridSquads.Rows.Count);

				// инициализация доп.фильтров
				_chlistSquadDisable = true;
				for (int index = 0; index < chlstFilterSquadTypes.Items.Count; index++)
					_filterSquad.SquadFilterList.Add(false);

				for (int index = 0; index < chlstFilterQuestTypes.Items.Count; index++)
					_filterQuest.QuestFilterList.Add(false);
				
				_chlistSquadDisable = false;
			}
		}

		// инициализация страницы зданий
		private void InitPageBuildings()
		{
			if (_dtBuildingsList.Rows.Count == 0 || tabBuildings.Tag == null ||
				tabBuildings.Tag.ToString() != CurrentLanguage)
			{
				tabBuildings.Tag = CurrentLanguage;

				/*if (SiegeDataBase.DataBuildings.Count == 0)
					SiegeDataBase.DataBuildings.LoadFromFile(statProgressBar);*/

				//dataGridBuildings.DataSource = dataBuildings.List.Values; //.OrderBy(pair => pair.Key);

				_dtBuildingsList = new DataTable();
				_dtBuildingsList.Columns.Add("name", typeof(string));
				_dtBuildingsList.Columns.Add("code", typeof(string));
				_dtBuildingsList.Columns.Add("size_a", typeof(int));
				_dtBuildingsList.Columns.Add("size_b", typeof(int));
				_dtBuildingsList.Columns.Add("level", typeof(int));

				foreach (var list in SiegeDataBase.DataBuildings)
				//.Where(pair => !String.IsNullOrEmpty(pair.Value.Name)).OrderBy(pair => pair.Value.Name))
				{
					var dr = _dtBuildingsList.NewRow();
					dr["name"] = String.IsNullOrEmpty(list.Value.Name) ? "? " + list.Value.Code : list.Value.Name;
					dr["code"] = list.Value.Code;
					dr["size_a"] = list.Value.SizeA > list.Value.SizeB ? list.Value.SizeB : list.Value.SizeA;
					dr["size_b"] = list.Value.SizeA > list.Value.SizeB ? list.Value.SizeA : list.Value.SizeB;

					int level = 0;
					foreach (var requirement in list.Value.Requirements)
					{
						if (requirement.StartsWith("Level,"))
							Int32.TryParse(Functions.SubString(requirement.Substring(6), 0), out level);
					}
					dr["level"] = level == 0 ? 1 : level;

					_dtBuildingsList.Rows.Add(dr);
				}

				_dtBuildingsListView = _dtBuildingsList.DefaultView; //SiegeDataBase.TableConstruction.DefaultView;

				dataGridBuildings.DataSource = _dtBuildingsListView;
				dataGridBuildings.Columns[0].Width = dataGridBuildings.ClientSize.Width;
				dataGridBuildings.Columns[0].HeaderText = "Наименование";
				//dataGridBuildings.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				dataGridBuildings.Columns[1].Visible = false;
				dataGridBuildings.Columns[2].Visible = false;
				dataGridBuildings.Columns[3].Visible = false;
				dataGridBuildings.Columns[4].Visible = false;
				dataGridBuildings.Columns[4].Width = 35;

				statText.Text = string.Format("Всего записей: {0}", dataGridBuildings.Rows.Count);
			}
		}

		// инициализация страницы квестов
		private void InitPageQuests()
		{
			if (_dtQuestsList.Rows.Count == 0 || tabQuest.Tag == null ||
				tabQuest.Tag.ToString() != CurrentLanguage)
			{
				tabQuest.Tag = CurrentLanguage;

				_dtQuestsList = new DataTable();
				_dtQuestsList.Columns.Add("name", typeof(string));
				_dtQuestsList.Columns.Add("code", typeof(string));
				_dtQuestsList.Columns.Add("levelBegin", typeof(string));
				_dtQuestsList.Columns.Add("levelEnd", typeof(string));
				_dtQuestsList.Columns.Add("daily", typeof(int));
				_dtQuestsList.Columns.Add("order", typeof(int));

				foreach (var list in SiegeDataBase.DataQuests)
				{
					var dr = _dtQuestsList.NewRow();
					dr["code"] = list.Value.ID;
					dr["name"] = String.IsNullOrEmpty(list.Value.Name) ? "? " + list.Value.ID : list.Value.Name;
					dr["levelBegin"] = list.Value.AcceptConditions != null ? list.Value.AcceptConditions.BeginLevel : 0;
					dr["levelEnd"] = list.Value.AcceptConditions != null ? list.Value.AcceptConditions.EndLevel : 0;
					dr["daily"] = list.Value.ResetTime > 0 && list.Value.ResetTime <= 24 ? 1 : 0;
					dr["order"] = 0;

					_dtQuestsList.Rows.Add(dr);
				}

				_dtQuestsListView = _dtQuestsList.DefaultView;

				dataGridQuests.DataSource = _dtQuestsListView;
				dataGridQuests.Columns[0].Width = dataGridQuests.ClientSize.Width;
				dataGridQuests.Columns[0].HeaderText = "Наименование";
				dataGridQuests.Columns[1].Visible = false;
				dataGridQuests.Columns[2].Width = 35;
				dataGridQuests.Columns[3].Width = 35;
				dataGridQuests.Columns[4].Width = 35;
				dataGridQuests.Columns[5].Width = 35;

				statText.Text = string.Format("Всего записей: {0}", dataGridQuests.Rows.Count);
			}
		}

		/// Переключение страниц (первичная инициализация списков)
		private void listBoxLanguages_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxLanguages.SelectedIndex <= _dataLangsCode.Count)
			{
				Application.DoEvents();

				CurrentLanguage = _dataLangsCode[listBoxLanguages.SelectedIndex];

				SiegeDataBase.DataLanguages.SetLanguagePack(frmMain.CurrentLanguage);
				SiegeDataBase.DataLanguages.LoadFromFile(statProgressBar);

				SiegeDataBase.UpdateServerCodes();
				SiegeDataBase.DataObjectsUi.LoadFromFile(statProgressBar);
				SiegeDataBase.DataObjectDescription.LoadFromFile(statProgressBar);
				SiegeDataBase.DataBuildings.LoadFromFile(statProgressBar);
				SiegeDataBase.DataSoul.LoadFromFile(statProgressBar);
				SiegeDataBase.DataUnitMover.LoadFromFile(statProgressBar);
				SiegeDataBase.DataAoT.LoadFromFile(statProgressBar);
				SiegeDataBase.DataUnitAi.LoadFromFile(statProgressBar);
				SiegeDataBase.DataDamage.LoadFromFile(statProgressBar);
				SiegeDataBase.DataWeapon.LoadFromFile(statProgressBar);

				SiegeDataBase.DataSquads.LoadFromFile(statProgressBar);
				SiegeDataBase.DataSquads.LoadUpgradeInfo();

				SiegeDataBase.DataResoursePairs.LoadFromFile(statProgressBar);

				SiegeDataBase.DataQuests.LoadFromFile(statProgressBar);

				SiegeDataBase.DataArtefacts.LoadFromFile(statProgressBar);

#if !LIMITED
				SiegeMarket.InitMarket();
#endif

				SiegeMarket.RecalcMarket();
			}
		}

		// информация по количеству строк в гриде в статус
		private void dataGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			var control = (DataGridView)sender;

			statText.Text = string.Format("Всего записей: {0}", control.Rows.Count);
		}

		// авторастяжка колонки в гриде до ширины грида
		private void dataGrid_Resize(object sender, EventArgs e)
		{
			var control = (DataGridView)sender;
			if (control.Columns.Count > 0)
				control.Columns[0].Width = control.ClientSize.Width;
		}

		// апдейт инфы при изменении текущей строки в гриде
		private void dataGrid_SelectionChanged(object sender, EventArgs e)
		{
			var control = (DataGridView)sender;
			var code = "";

			if (control.SelectedRows.Count > 0 && control.CurrentRow != null)
			{
				code = control.CurrentRow.Cells[1].Value.ToString();
			}

			control.Tag = code;

			if (_updateInfoEnabled)
				UpdateInfo();

			control.Focus();
		}

		private void TimerDonateOnTick(object sender, EventArgs eventArgs)
		{
			lblDonate.Visible = !lblDonate.Visible;

			timerDonate.Interval = lblDonate.Visible ? 1500 : 300;
		}

		// при изменении суб-страницы обновляем её
		private void tabsInfo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!_updateInfoEnabled || sender == null)
				return;

			var control = (TabControl)sender;

			var page = control.SelectedIndex;

			control.Tag = control.SelectedTab.Tag;

			UpdateInfo();
		}

		private bool _updateInfoEnabled = true;

		// автоапдейт изменения размера иконок
		private void chbxIconSize_CheckedChanged(object sender, EventArgs e)
		{
			if (_updateInfoEnabled)
				UpdateInfo();
		}

		private void btnBasePath_Click(object sender, EventArgs e)
		{
			var dlg = new FolderBrowserDialog();
			dlg.Description = "Укажите папку размещения игры 'Осада Онлайн'";
			dlg.ShowNewFolderButton = false;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				tbxBasePath.Text = dlg.SelectedPath;
				SiegeDataBase.GamePath = tbxBasePath.Text;

				LocalizationInit(Path.Combine(SiegeDataBase.GamePath, @"Data\Misc"));
			}
		}

		// обработчик кликов по http-линкам
		private void linkSiege_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(linkSiege.Text);
		}

		#region Обработчик ввода фильтра

		// активность интерактивности фильтра
		private readonly FilterClass _filterBuilding = new FilterClass("building")
														{
															SizeCondition = ">=",
															SizeA = 1,
															SizeB = 1
														};

		private readonly FilterClass _filterSquad = new FilterClass("squad")
														{
															LevelMin = 0
														};

		private readonly FilterClass _filterQuest = new FilterClass("quest")
		                                            	{
		                                            		LevelMin = 0
		                                            	};

		private void txtbxFilter_TextChanged(object sender, EventArgs e)
		{
			if (sender == null)
				return;

			var control = sender as TextBox;

			FilterClass filter;
			int page;
			if (GetCurrentFilter(out filter, out page) && control != null)
			{
				var param = control.Tag != null ? control.Tag.ToString() : "";
				switch (param)
				{
					case "MaskName":
						filter.MaskName = control.Text;
						break;
					case "MaskCode":
						filter.MaskCode = control.Text;
						break;
				}

				SetFilterApply(false, filter, page);
			}
		}

		private void cmbbxFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (sender == null)
				return;

			var control = sender as ComboBox;

			FilterClass filter;
			int page;
			if (GetCurrentFilter(out filter, out page) && control != null)
			{
				var param = control.Tag != null ? control.Tag.ToString() : "";

				switch (param)
				{
					case "SizeCondition":
						if (control.SelectedIndex < 0)
							control.SelectedIndex = 3;	// устанавливаем операцию '>=' по-умолчанию

						filter.SizeCondition = control.Items[control.SelectedIndex].ToString();
						break;
				}

				SetFilterApply(false, filter, page);
			}
		}

		private void numFilter_ValueChanged(object sender, EventArgs e)
		{
			if (sender == null)
				return;

			var control = sender as NumericUpDown;

			FilterClass filter;
			int page;
			if (GetCurrentFilter(out filter, out page) && control != null)
			{
				var param = control.Tag != null ? control.Tag.ToString() : "";

				switch (param)
				{
					case "SizeA":
						filter.SizeA = (int)control.Value;
						break;
					case "SizeB":
						filter.SizeB = (int)control.Value;
						break;
					case "LevelMin":
						filter.LevelMin = (int)control.Value;
						break;
					case "LevelMax":
						filter.LevelMax = (int)control.Value;
						break;
				}

				SetFilterApply(false, filter, page);
			}
		}

		// получить класс текущего фильтра
		private bool GetCurrentFilter(out FilterClass filter, out int page)
		{
			filter = new FilterClass("");

			page = tabsMain.SelectedIndex;

			if (page < 1 || page > tabsMain.TabCount - 1)
				return false;

			switch (page)
			{
				case 1:
					filter = _filterBuilding;
					break;

				case 2:
					filter = _filterSquad;
					break;

				case 3:
					filter = _filterQuest;
					break;
			}

			return true;
		}

		// сформировать строку фильтра для датавью
		private static string GenerateFilterString(FilterClass filter)
		{
			var filterStr = "";

			if (!string.IsNullOrEmpty(filter.MaskName))
			{
				filter.MaskName = Functions.ReplaceChars(filter.MaskName, "'[]", "?");
				filterStr += string.Format(" and name like '%{0}%'", filter.MaskName);
			}

			if (!string.IsNullOrEmpty(filter.MaskCode))
			{
				filter.MaskCode = Functions.ReplaceChars(filter.MaskCode, "'[]", "?");
				filterStr += string.Format(" and code like '%{0}%'", filter.MaskCode);
			}

			if (!string.IsNullOrEmpty(filter.SizeCondition))
			{
				var min = Math.Min(filter.SizeA, filter.SizeB);
				var max = Math.Max(filter.SizeA, filter.SizeB);

				filterStr += string.Format(" and size_a {0} {1} and size_b {0} {2}",
										   filter.SizeCondition, min, max);
			}

			if (filter.Type != "quest")
			{
				if (filter.LevelMin >= 0)
					filterStr += string.Format(" and level >= {0}", filter.LevelMin);

				if (filter.LevelMax >= 0)
					filterStr += string.Format(" and level <= {0}", filter.LevelMax);
			}
			else
			{
				if (filter.LevelMin >= 0)
					filterStr += string.Format(" and levelEnd >= {0}", filter.LevelMin);

				if (filter.LevelMax >= 0)
					filterStr += string.Format(" and levelBegin <= {0}", filter.LevelMax);
			}

			if (filterStr != "")
				filterStr = filterStr.Substring(5);

			if (filter.SquadFilterList.Count > 0 || filter.QuestFilterList.Count > 0)
			{
				var add = "";

				for (var k = 0; k < filter.SquadFilterList.Count; k++)
				{
					if (filter.SquadFilterList[k])
					{
						switch (k)
						{
							case 0:
								// NPC
								add += " or level = 0";
								break;
							case 1:
								// Basic squads
								add += " or basic > 0";
								break;
							case 2:
								// Upgradable
								add += " or upgrade > 0";
								break;
							case 3:
								// Rank squads
								add += " or rank > 0";
								break;
							case 4:
								// Guild squads
								add += " or guild > 0";
								break;
						}
					}
				}

				for (var k = 0; k < filter.QuestFilterList.Count; k++)
				{
					if (filter.QuestFilterList[k])
					{
						switch (k)
						{
							case 0:
								// дейлики
								add += " or daily = 1";
								break;
						}
					}
				}

				if (add != "")
				{
					add = add.Substring(4);

					filterStr += string.Format(" and ({0})", add);
				}

			}

			return filterStr;
		}

		// применить фильтр
		private void SetFilterApply(bool forceUpdate)
		{
			SetFilterApply(forceUpdate, null, 0);
		}

		private void SetFilterApply(bool forceUpdate, FilterClass filter, int page)
		{
			DataView dataView = null;

			bool ret = filter != null || GetCurrentFilter(out filter, out page);

			if (ret && page > 0)
			{
				switch (page)
				{
					case 1:
						dataView = _dtBuildingsListView;
						break;

					case 2:
						dataView = _dtSquadsListView;
						break;

					case 3:
						dataView = _dtQuestsListView;
						break;
				}


				if (filter != null && dataView != null && filter.Enabled && (filter.AutoUpdate || forceUpdate))
					dataView.RowFilter = GenerateFilterString(filter);
			}
		}

		// Сброс значений фильтра
		private void btnFilterReset_Click(object sender, EventArgs e)
		{
			FilterClass filter;
			int page;
			if (GetCurrentFilter(out filter, out page))
			{
				filter = new FilterClass("");

				switch (page)
				{
					case 1:
						filter = new FilterClass("building");

						if (filter.LevelMin <= 0)
							filter.LevelMin = 1;

						txtbxFilterBuildingName.Text = filter.MaskName;
						txtbxFilterBuildingCode.Text = filter.MaskCode;
						cmbbxFilterBuildingSizeMode.Text = filter.SizeCondition;
						numFilterBuildingSizeA.Value = numFilterBuildingSizeA.Minimum;
						numFilterBuildingSizeB.Value = numFilterBuildingSizeB.Minimum;
						numFilterBuildingLevelMin.Value = filter.LevelMin;
						numFilterBuildingLevelMax.Value = filter.LevelMax;
						break;

					case 2:
						filter = new FilterClass("squad");
						txtbxFilterSquadName.Text = filter.MaskName;
						txtbxFilterSquadCode.Text = filter.MaskCode;
						numFilterSquadLevelMin.Value = filter.LevelMin;
						numFilterSquadLevelMax.Value = filter.LevelMax;
						break;

					case 3:
						filter = new FilterClass("quest");
						txtbxFilterQuestName.Text = filter.MaskName;
						txtbxFilterQuestCode.Text = filter.MaskCode;
						numFilterQuestLevelMin.Value = filter.LevelMin;
						numFilterQuestLevelMax.Value = filter.LevelMax;
						break;
				}


				SetFilterApply(true, filter, page);
			}
		}

		private void chbxFilterAutoUpdate_CheckedChanged(object sender, EventArgs e)
		{
			FilterClass filter;
			int page;
			if (GetCurrentFilter(out filter, out page))
			{
				switch (page)
				{
					case 1:
						btnFilterBuildingApply.Enabled = !chbxFilterBuildingAutoUpdate.Checked;
						filter.AutoUpdate = chbxFilterBuildingAutoUpdate.Checked;
						break;

					case 2:
						btnFilterSquadApply.Enabled = !chbxFilterSquadAutoUpdate.Checked;
						filter.AutoUpdate = chbxFilterSquadAutoUpdate.Checked;
						break;

					case 3:
						btnFilterQuestApply.Enabled = !chbxFilterQuestAutoUpdate.Checked;
						filter.AutoUpdate = chbxFilterQuestAutoUpdate.Checked;
						break;
				}
			}
		}

		private void btnFilterApply_Click(object sender, EventArgs e)
		{
			FilterClass filter;
			int page;
			if (GetCurrentFilter(out filter, out page))
			{
				SetFilterApply(true, filter, page);
			}
		}

		private void cmbbxFilter_Leave(object sender, EventArgs e)
		{
			if (sender == null)
				return;

			var control = (ComboBox)sender;

			if (control.SelectedIndex < 0)
				control.SelectedIndex = 3;

			control.Text = control.Items[control.SelectedIndex].ToString();
		}

		private bool _chlistSquadDisable = false;

		// чеклист доп.фильтров
		private void chlstFilterTypes_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (sender == null)
				return;

			var control = (CheckedListBox)sender;

			FilterClass filter;
			int page;
			if (GetCurrentFilter(out filter, out page))
			{
				// для страницы войск
				if (page == 2)
				{
					if (e.Index >= 0 && e.Index < filter.SquadFilterList.Count)
					{
						filter.SquadFilterList[e.Index] = e.NewValue == CheckState.Checked;

						SetFilterApply(true, filter, page);
					}
				}

				// для страницы квестов
				if (page == 3)
				{
					if (e.Index >= 0 && e.Index < filter.QuestFilterList.Count)
					{
						filter.QuestFilterList[e.Index] = e.NewValue == CheckState.Checked;

						SetFilterApply(true, filter, page);
					}
				}
			}



		}

		private class FilterClass
		{
			public readonly string Type;

            public bool AutoUpdate = true;
			public bool Enabled = true;
			public int LevelMax = 25;
			public int LevelMin = 0;

			public string MaskCode;
			public string MaskName;
			public int SizeA;
			public int SizeB;
			public string SizeCondition;

			public List<bool> SquadFilterList = new List<bool>();
			public List<bool> QuestFilterList = new List<bool>();
            
			public FilterClass(string str)
			{
				Type = str;
			}

			public void Clear()
			{
				Enabled = true;
				MaskName = null;
				MaskCode = null;
				SizeCondition = null;
				SizeA = 0;
				SizeB = 0;
				LevelMin = 1;
				LevelMax = 25;
			}
		}

		#endregion

		private void dataGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
		{
			var gridView = sender as DataGridView;
			if (gridView == null)
				return;

			var select = (gridView.Tag ?? "").ToString().ToLower();
			if (select == "")
				return;

			var cell = gridView["code", e.RowIndex];
			var value = ((string)cell.Value).ToLower();

			if (value != null && select == value)
			{
				cell.Style.BackColor = SystemColors.Highlight;
				SetCellStyle(cell.OwningRow, cell.Style);
			}
			else
			{
				SetCellStyle(cell.OwningRow, null);
			}
		}

		private void SetCellStyle(DataGridViewRow row, DataGridViewCellStyle style)
		{
			foreach (DataGridViewTextBoxCell cell in row.Cells)
			{
				cell.Style = style;
			}
		}

		private void frmMain_KeyUp(object sender, KeyEventArgs e)
		{
			// Окно о программе
			if (e.KeyCode == Keys.F1)
			{
				var frm = new frmAbout();
				frm.ShowDialog();
			}

#if !LIMITED
			// Окно цен рынка
			if (e.KeyCode == Keys.F4 && SystemInfo.SecurCode.CheckAccess("market"))
			{
				if (formMarket == null)
				{
					formMarket = new frmMarket();

					formMarket.Show();

					formMarket.InitControls();
				}
				else
					formMarket.Show();

				formMarket.BringToFront();
			}

			// Окно карт
			if (e.KeyCode == Keys.F8 && SystemInfo.SecurCode.CheckAccess("maps"))
			{
				if (formMaps == null)
				{
					formMaps = new frmMaps();

					formMaps.Show();
				}
				else
					formMaps.Show();

				formMaps.BringToFront();
			}
#endif
		}

		private void txtbxQuest_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			var link = e.LinkText.Split('#');

			if (link.Length == 2)
			{
				var code = link[1].Split(':');
				
				if (link[1].StartsWith("NPC:"))
				{
					MessageBox.Show(
						string.Format("В данной версии карты не подключены.{1}Информация по местоположению моба '{0}' отсутствует.",
						              SiegeDataBase.DataLanguages.Get(code[1]) ?? code[1], Environment.NewLine),
						"Ошибко",
						MessageBoxButtons.OK, MessageBoxIcon.Stop);

					return;
				}

				if (link[1].StartsWith("item:"))
				{
					LinkClick(code[1]);
					return;
				}

				if (link[1].StartsWith("quest:"))
				{
					LinkClick(code[1]);
					return;
				}

				LinkClick(code[0]);
				
				return;
			}
			
			MessageBox.Show("Не известная ссылко. Не работает.", "Ошибко", MessageBoxButtons.OK, MessageBoxIcon.Stop);
		}


		/*
		/// <summary>
		/// Конвертер серверных кодов объектов
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, EventArgs e)
		{
			var dict = new Dictionary<string, string>();

			var file = File.ReadAllLines("compare.txt");

			foreach (var s in file)
			{
				var t = s.Split('=');

				if (t.Length == 2)
				{
					t[0] = t[0].Trim();
					t[1] = t[1].Trim();

					if (t[0] != "" && t[1] != "" && !dict.ContainsKey(t[0]))
						dict.Add(t[0], t[1]);
				}
			}

			var output = new string[dict.Count];

			var k = 0;
			foreach (var d in dict)
			{
				output[k] = string.Format("{0}={1}", d.Key, d.Value);
				k++;
			}

			File.WriteAllLines("compare2.txt", output);
		}
		*/
	}

	/// <summary>
	/// Конфиг-класс
	/// </summary>
	public static class AppConfig
	{
		public static ModeList Mode =
#if LIMITED
ModeList.LimitedEdition
#else
ModeList.AllIncluded
#endif
;

		public enum ModeList
		{
			/// <summary>
			/// Ограниченная версия
			/// </summary>
			LimitedEdition,

			/// <summary>
			/// Все разработки в коде
			/// </summary>
			AllIncluded
		}
	}

}