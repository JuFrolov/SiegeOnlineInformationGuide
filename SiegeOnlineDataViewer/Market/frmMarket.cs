using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SiegeOnlineDataViewer.SiegeDataFiles;
using SiegeOnlineDataViewer.Utils;

namespace SiegeOnlineDataViewer.Market
{
	public partial class frmMarket : Form
	{
		// список обновлённых строк
		private List<int> rowsNeedToSave = new List<int>();

		/// <summary>
		/// Словарь кодов ресурсов и номеров строк
		/// </summary>
		private Dictionary<string, int> HashList = new Dictionary<string, int>();

		private const byte priceBuy = (byte) SiegeMarket.PriceType.Buy;
		private const byte priceSell = (byte) SiegeMarket.PriceType.Sell;

		private const double maxSizeAdd = 10000;

		private bool forceModeResize = false;
		private bool forceModeSet = false;

		#region Префиксы имён для динамически создаваемых контролов
		const string _rowIcon = "rowIcon_";
		const string _rowName = "rowName_";
		const string _rowFormulaCalc = "rowFormulaCalc_";
		
		private Dictionary<byte, string> _rowPack = new Dictionary<byte, string>()
		                                           	{
		                                           		{priceBuy, "rowBuyPack_"},
		                                           		{priceSell, "rowSellPack_"}
		                                           	};

		private Dictionary<byte, string> _rowGold = new Dictionary<byte, string>()
		                                           	{
		                                           		{priceBuy, "rowBuyGold_"},
		                                           		{priceSell, "rowSellGold_"}
		                                           	};

		private Dictionary<byte, string> _rowDiamond = new Dictionary<byte, string>()
		                                           	{
		                                           		{priceBuy, "rowBuyDiamond_"},
		                                           		{priceSell, "rowSellDiamond_"}
		                                           	};

		private Dictionary<byte, string> _rowDiamondReset = new Dictionary<byte, string>()
		                                           	{
		                                           		{priceBuy, "rowBuyDiamondReset_"},
		                                           		{priceSell, "rowSellDiamondReset_"}
		                                           	};
		#endregion

		private int[][] columnsWidth = new int[][]
		                             	{
											new int[] {32, 8},		// 0: иконка
											new int[] {120, 20},	// 1: наименование
											new int[] {15, 28},		// 2: чек по формуле
											new int[] {56, 8},		// 3: размер в пачке
											new int[] {92, 8},		// 4: покупка в золоте
											new int[] {58, 4},		// 5: покупка в брилах
											new int[] {16, 20},		// 6: ресет брилов
											new int[] {56, 8},		// 7: размер в пачке
											new int[] {92, 8},		// 8: покупка в золоте
											new int[] {58, 4},		// 9: покупка в брилах
											new int[] {16, 20}		// 10: ресет брилов
		                             	};

		// флаг процесса обновления контролов
		private static bool UpdateProcessed;

		//==================================================================

		public frmMarket()
		{
			InitializeComponent();

			//InitControls();
        }

		// динамическое размещение контролов (таблица цен)
        public void InitControls()
		{
			if (UpdateProcessed)
				return;

			UpdateProcessed = true;


			const int heightSize = 32;
			const int verticalSpace = 2;

			var neddedResources = SiegeMarket.Items.Keys; //.Where(s => SiegeDataBase.DataBuildings.NeededResources.Contains(s) || s == "diamond");
			
			progressBar.Visible = true;
			progressBar.Maximum = neddedResources.Count();
			progressBar.Value = 0;

			panelMain.Enabled = false;
			panelMain.Controls.Clear();
			HashList.Clear();

			var i = 0;
			foreach (string key in neddedResources)
			{
				progressBar.Value = i;

				Application.DoEvents();
                
				var item = new KeyValuePair<string, SiegeMarket.ItemMarketClass>(key, SiegeMarket.Items[key]);

				var topLine = 3 + i * (heightSize + verticalSpace);
				var leftLine = 3;
				const int heightLine = 32;

				#region Расчитаем изображение ресурса
				var file = SiegeDataBase.DataObjectsUi.Get(item.Key);
				if (file == "")
					file = string.Format(@"UI\ConstructionIcons\{0}_icon.tga", item.Key);

				var imageFile = Path.Combine(SiegeDataBase.GamePath, @"Data\Textures\");
				imageFile = Path.Combine(imageFile, file ?? SiegeDataBase.PictureError);

				Bitmap image = null;
				if (File.Exists(imageFile))
					image = TgaImages.GetFromFile(imageFile);
				#endregion

				var curCol = 0;

				#region Иконка ресурса
				var icon = new PictureBox
				           	{
								Location = new Point(leftLine, topLine),
				           		Name = _rowIcon + i,
								Size = new Size(columnsWidth[curCol][0], heightLine),
								Image = image,
                                SizeMode = PictureBoxSizeMode.Zoom,
								Tag = key
				           	};
				#endregion

				leftLine += columnsWidth[curCol][0] + columnsWidth[curCol][1];
				curCol++;

				#region Название ресурса
				var label = new TextBox
				            	{
									Location = new Point(leftLine, topLine + 11),
				            		Name = _rowName + i,
									Size = new Size(columnsWidth[curCol][0], heightLine),
				            		Text = SiegeDataBase.DataLanguages.Get(item.Key),
									TextAlign = HorizontalAlignment.Left,
									ReadOnly = true,
                                    //Multiline = true,
                                    BorderStyle = BorderStyle.None,
									Tag = key
				            	};
				#endregion

				leftLine += columnsWidth[curCol][0] + columnsWidth[curCol][1];
				curCol++;

				#region Расчитывать ли по формуле
				var calcByFormula = item.Value.FormulaCalc && item.Value.FormulaCalcEnabled && !item.Value.FormulaCalcDisabled;

				// вести ли расчёт по формуле
				var checkbox = new CheckBox
				               	{
				               		AutoSize = true,
				               		Name = _rowFormulaCalc + i,
									Location = new Point(leftLine, topLine + 11),
				               		Size = new Size(15, 14),
				               		UseVisualStyleBackColor = true,
				               		Checked = calcByFormula,
									Enabled = item.Value.FormulaCalc,
									Tag = key
				               	};
                checkbox.CheckedChanged += PackSizeOnCheckedChanged;
				#endregion

				panelMain.Controls.Add(icon);
				panelMain.Controls.Add(label);
				panelMain.Controls.Add(checkbox);

				leftLine += columnsWidth[curCol][0] + columnsWidth[curCol][1];
				curCol++;

				var types = Enum.GetValues(typeof(SiegeMarket.PriceType));
				foreach (byte priceType in types)
				{
					// размер пачки
					var numboxPackSize = createNumericUpDown(new Point(leftLine, topLine + 8), _rowPack[priceType] + i, columnsWidth[curCol][0], item.Value.PackSize[priceType], key, null);

					leftLine += columnsWidth[curCol][0] + columnsWidth[curCol][1];
					curCol++;

					var priceEnabled = !item.Value.FormulaCalc || !item.Value.FormulaCalcEnabled;

					// стоимость в золоте
					var numboxGold = createNumericUpDown(new Point(leftLine, topLine + 8), _rowGold[priceType] + i, columnsWidth[curCol][0], 0, key, priceEnabled);

					leftLine += columnsWidth[curCol][0] + columnsWidth[curCol][1];
					curCol++;

					// рассчитаем стоимость в брилах
					var numboxDiamond = createNumericUpDown(new Point(leftLine, topLine + 8), _rowDiamond[priceType] + i, columnsWidth[curCol][0], 0, key, priceEnabled);

					leftLine += columnsWidth[curCol][0] + columnsWidth[curCol][1];
					curCol++;

					// кнопка сброса ресов
					var buttonDiamondReset = new Button
					{
						Location = new Point(leftLine, topLine + 8),
						Size = new Size(12, 12),
						Text = "*",
						Name = _rowDiamondReset[priceType] + i,
						Tag = key
					};
					buttonDiamondReset.Click += ButtonDiamondResetOnClick;
					toolTip.SetToolTip(buttonDiamondReset, "Сброс размера пачки до 1 бр");

					leftLine += columnsWidth[curCol][0] + columnsWidth[curCol][1];
					curCol++;

					// добавляем все контролы
					panelMain.Controls.Add(numboxPackSize);
					panelMain.Controls.Add(numboxGold);
					panelMain.Controls.Add(numboxDiamond);
					panelMain.Controls.Add(buttonDiamondReset);
				}

				// словарь номеров строк по коду реса
				HashList.Add(key, i);
                                                   
				// тултип формулы
				if (SiegeDataBase.DataResoursePairs.ContainsKey(item.Key))
				{
					var res = SiegeDataBase.DataResoursePairs[item.Key].Resources;

					if (res != null)
					{
						var text = "";
						foreach (KeyValuePair<string, double> re in res)
						{
							text += string.Format("{0}: {1} шт{2}", SiegeDataBase.DataLanguages.Get(re.Key).Trim(), re.Value, Environment.NewLine);
						}

						if (text != "")
							toolTip.SetToolTip(checkbox, text.Trim());
					}
				}
				else
					toolTip.SetToolTip(checkbox, "Формулы расчёта нет");
                                                   
				UpdatePriceAfterChangeFormulaCalc(key, i, calcByFormula);

				i++;
			}

			panelMain.Enabled = true;

			progressBar.Visible = false;

			UpdateProcessed = false;
		}

		// создание нумерика
		private NumericUpDown createNumericUpDown(Point point, string name, int width, double value, string code)
		{
			var control = new NumericUpDown
			{
				Location = point,
				Maximum = int.MaxValue,
				Name = name,
				Size = new Size(width, 20),
				TextAlign = HorizontalAlignment.Right,
				ThousandsSeparator = true,
				Value = (decimal)value,
				Tag = code
			};

			control.GotFocus += ControlOnGotFocus;
			control.Enter += ControlOnEnter;
			control.Leave += ControlOnLeave;
			control.EnabledChanged += ControlOnEnabledChanged;

			return control;
		}

		// создание нумерика
		private NumericUpDown createNumericUpDown(Point point, string name, int width, double value, string code, bool? enabled)
		{
			var control = createNumericUpDown(point, name, width, value, code);

			control.DecimalPlaces = 2;

			// стоимость брила в брилах дизаблим
			if (name.Contains("Diamond") && code == "diamond")
			{
				control.Enabled = false;
				control.Value = 1;
			}

			if (!enabled.HasValue)
			{
				control.ValueChanged += NumboxPackSizeOnValueChanged;
			}
			else
			{
				control.ValueChanged += NumboxMoneyOnValueChanged;
				control.Enabled = enabled.Value;
			}

			toolTip.SetToolTip(control, control.Value.ToString());

			return control;
		}


		// флаг программного обновления данных
		private bool internalUpdateControls;

		// кнопка сброса бриллиантов покупки до единицы
		private void ButtonDiamondResetOnClick(object sender, EventArgs eventArgs)
		{
			if (internalUpdateControls)
				return;

			var control = (Button) sender;

			if (control == null)
				return;

			var code = control.Tag.ToString();
			var num = HashList[code];
			var name = control.Name;


			//internalUpdateControls = true; -- в данном случае пересчёт необходим!

			var priceType = name.StartsWith("rowBuy") ? priceBuy : priceSell;

			var numControl = ((NumericUpDown)panelMain.Controls[_rowDiamond[priceType] + num]);
			
			var pack = SiegeMarket.Items[code].PackSize[priceType];

			if (pack != 0)
			{
				var diamond = (!SiegeMarket.Items[code].FormulaCalc || !SiegeMarket.Items[code].FormulaCalcEnabled ||
				               SiegeMarket.Items[code].FormulaCalcDisabled
				               	? SiegeMarket.Items[code].UserGold[priceType]
				               	: SiegeMarket.Items[code].CalcGold[priceType]) / SiegeMarket.DiamondPrice[priceType];

				diamond *= pack;

				var lastValue = (numControl.MaximumSize.Width - maxSizeAdd*maxSizeAdd) / maxSizeAdd;

				double koef = numControl.MaximumSize.Width >= maxSizeAdd
				              	? 1 / lastValue
				              	: double.NaN;

				forceModeResize = true;

				if (diamond != 0 && !double.IsInfinity(koef) && !double.IsNaN(koef))
					SetPackSizeValue((NumericUpDown) panelMain.Controls[_rowPack[priceType] + num], pack * koef);
			}
		}

		/// <summary>
		/// Обработчик изменений в строке: изменение цены
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		private void NumboxMoneyOnValueChanged(object sender, EventArgs eventArgs)
		{
			if (internalUpdateControls)
				return;

			var control = (NumericUpDown) sender;

			if (control == null)
				return;

			var code = control.Tag.ToString();
			var num = HashList[code];
			var name = control.Name;
			var value = (double) control.Value;

			internalUpdateControls = true;

			var priceType = name.StartsWith("rowBuy") ? priceBuy : priceSell;
			var priceMoney = name.Contains("Gold");

			var item = SiegeMarket.Items[code];

			var flg = false;
			if (priceMoney)
			{
				#region рассчёты для изменений в золоте
				if ((!Console.CapsLock || forceModeResize) && !forceModeSet)
				{
					// масштабирование пачки
					var lastValue = control.MaximumSize.Width >= maxSizeAdd
						? (control.MaximumSize.Width - maxSizeAdd * maxSizeAdd) / maxSizeAdd
						: double.NaN;

					double koef = value / lastValue;

					if (!double.IsInfinity(koef) && !double.IsNaN(koef))
					{
						item.PackSize[priceType] = item.PackSize[priceType] * koef;

						var cont = (NumericUpDown) panelMain.Controls[_rowPack[priceType] + num];

						cont.Value = (decimal)item.PackSize[priceType];

						toolTip.SetToolTip(cont, cont.Value.ToString());

						internalUpdateControls = false;
						// обновим отображение цен
						UpdatePriceAfterChangeFormulaCalc(code, num, item.FormulaCalcEnabled);
						internalUpdateControls = true;

					}
					else
						flg = true;
				}

				// если изменение размера произошло с ошибкой, то меняем цену
				if ((Console.CapsLock && !forceModeResize) || forceModeSet || flg)
				{
					// изменение цены пачки
					if (item.PackSize[priceType] > 0)
					{
						item.UserGold[priceType] = value / item.PackSize[priceType];

						/*item.UserDiamond[priceType] = SiegeMarket.DiamondPrice[priceType] > 0
																			? value / SiegeMarket.DiamondPrice[priceType] /
																			  item.PackSize[priceType]
						                                                 	: 0;*/
					}

					// если это цена брила, то обновим и наверху
					if (code == "diamond")
					{
						SiegeMarket.DiamondPrice[priceType] = item.UserGold[priceType];
						//item.UserDiamond[priceType] = 1;

						SetDiamondsToAll(_rowDiamond[priceType], item.UserGold[priceType]);
					}
					else
					{
						var cont = (NumericUpDown) panelMain.Controls[_rowDiamond[priceType] + num];

						cont.Value = SiegeMarket.DiamondPrice[priceType] != 0
						             	? Convert.ToDecimal(value/SiegeMarket.DiamondPrice[priceType])
						             	: 0;

						toolTip.SetToolTip(cont, cont.Value.ToString());
					}
				}
				#endregion
			}
			else
			{
				if ((!Console.CapsLock || forceModeResize) && !forceModeSet)
				{
					// масштабирование пачки
					double koef = control.MaximumSize.Width >= maxSizeAdd
						? value / ((control.MaximumSize.Width - maxSizeAdd * maxSizeAdd) / maxSizeAdd )
						: double.NaN;

					if (!double.IsInfinity(koef) && !double.IsNaN(koef))
					{
						item.PackSize[priceType] = item.PackSize[priceType] * koef;
						control.Value = (decimal)(value * koef);
						toolTip.SetToolTip(control, control.Value.ToString());

						var cont = (NumericUpDown)panelMain.Controls[_rowGold[priceType] + num];
                        cont.Value = Convert.ToDecimal(value * SiegeMarket.DiamondPrice[priceType]);
						toolTip.SetToolTip(cont, cont.Value.ToString());

						SetPackSizeValue(((NumericUpDown)panelMain.Controls[_rowPack[priceType] + num]), item.PackSize[priceType]);
					}
					else
						flg = true;
				}

				if ((Console.CapsLock && !forceModeResize) || forceModeSet || flg)
				{
					// изменение цены пачки
					if (item.PackSize[priceType] > 0)
					{
						item.UserGold[priceType] = SiegeMarket.DiamondPrice[priceType] * value / item.PackSize[priceType];
						//item.UserDiamond[priceType] = value / item.PackSize[priceType];

						var cont = (NumericUpDown)panelMain.Controls[_rowGold[priceType] + num];
						cont.Value = Convert.ToDecimal(value * SiegeMarket.DiamondPrice[priceType]);
						toolTip.SetToolTip(cont, cont.Value.ToString());
					}
				}
			}

			SetNumericUpDownValue(control, value, true);

			internalUpdateControls = false;

			// обновим цены зависимых ресов
			if (Console.CapsLock)
				RecalcLinkedRes(code);

			toolTip.SetToolTip(control, control.Value.ToString());

			forceModeResize = false;
			forceModeSet = false;
		}

		// переинициализация стоимости всех ресов в брилах
		private void SetDiamondsToAll(string controlName, double goldsInDiamond)
		{
			var market = SiegeMarket.Items.Keys; //.Where(s => SiegeDataBase.DataBuildings.NeededResources.Contains(s)).ToList();

			internalUpdateControls = true;

			foreach (var code in market)
			{
				var num = HashList[code];

				if (!panelMain.Controls.ContainsKey(controlName + num))
					continue;

				var control = panelMain.Controls[controlName + num];

				if (control == null)
					continue;

				var priceType = controlName.StartsWith("rowBuy") ? priceBuy : priceSell;

				var gold = SiegeMarket.Items[code].UserGold[priceType];
				var pack = SiegeMarket.Items[code].PackSize[priceType];
                
				// апдейт отображения без пересчёта сумм
				var cont = (NumericUpDown) control;
				cont.Value = (decimal)(gold / goldsInDiamond * pack);
				toolTip.SetToolTip(cont, cont.Value.ToString());
			}

			internalUpdateControls = false;
		}

		/// <summary>
		/// Обновить отображение цен (с пересчётом формулы, если она есть)
		/// </summary>
		/// <param name="code"></param>
		/// <param name="num"></param>
		/// <param name="calcFormula"></param>
		private void UpdatePriceAfterChangeFormulaCalc(string code, int num, bool calcFormula)
		{
			if (internalUpdateControls)
				return;

			SiegeMarket.Items[code].FormulaCalcEnabled = calcFormula;

			var item = SiegeMarket.Items[code];

			internalUpdateControls = true;

#if !DEBUG
			try
			{
#endif
				var types = Enum.GetValues(typeof(SiegeMarket.PriceType));
				foreach (byte priceType in types)
				{
					var priceGold = item.UserGold[priceType];
					var priceDiamond = SiegeMarket.DiamondPrice[priceType] != 0 
						? priceGold / SiegeMarket.DiamondPrice[priceType]
						: 0;

					if (calcFormula)
					{
						#region расчёт стоимости по формуле

						var calcGold = 0.0;

						var resources = SiegeDataBase.DataResoursePairs[code].Resources;
						foreach (var res in resources)
						{
							var id = res.Key.ToLower();

							if (id == "gold")
							{
								calcGold += res.Value;
								continue;
							}

							calcGold += (SiegeMarket.Items[id].FormulaCalc && SiegeMarket.Items[id].FormulaCalcEnabled && !SiegeMarket.Items[id].FormulaCalcDisabled
								? SiegeMarket.Items[id].CalcGold[priceType]
								: SiegeMarket.Items[id].UserGold[priceType]) 
								* res.Value;
						}

						item.CalcGold[priceType] = calcGold;

						/*item.CalcDiamond[priceType] = SiegeMarket.DiamondPrice[priceType] != 0
							? code != "diamond" ? calcGold / SiegeMarket.DiamondPrice[priceType] : 1
							: 0;*/

						priceGold = item.CalcGold[priceType];
						priceDiamond = SiegeMarket.DiamondPrice[priceType] != 0
										? priceGold / SiegeMarket.DiamondPrice[priceType]
										: 0;

						#endregion
					}

					var control = ((NumericUpDown)panelMain.Controls[_rowGold[priceType] + num]);
					control.Value = (decimal)(priceGold * item.PackSize[priceType]);
					control.MaximumSize = new Size((int)(maxSizeAdd * maxSizeAdd + (double)control.Value * maxSizeAdd), (int)maxSizeAdd);

					toolTip.SetToolTip(control, control.Value.ToString());



					var control2 = (NumericUpDown)panelMain.Controls[_rowDiamond[priceType] + num];

					//if (control2.Value != 1)
					{
						control2.Value = code != "diamond"
								? (decimal)(priceDiamond * item.PackSize[priceType])
								: 1;

						toolTip.SetToolTip(control2, control2.Value.ToString());

						// так как обновления не идёт, фиксируем сами
						control2.MaximumSize = new Size((int)(maxSizeAdd * maxSizeAdd + (double)control2.Value * maxSizeAdd), (int)maxSizeAdd);
					}
					//else
					//	NumboxMoneyOnValueChanged(control2, EventArgs.Empty);
						
				}
#if !DEBUG
			}
			catch (Exception)
			{

			}
			finally
			{
#endif
				internalUpdateControls = false;
#if !DEBUG
			}
#endif

			panelMain.Controls[_rowGold[priceBuy] + num].Enabled = !calcFormula;
			panelMain.Controls[_rowDiamond[priceBuy] + num].Enabled = !calcFormula && code != "diamond";
			panelMain.Controls[_rowGold[priceSell] + num].Enabled = !calcFormula;
			panelMain.Controls[_rowDiamond[priceSell] + num].Enabled = !calcFormula && code != "diamond";

		}

		// обработчик изменений в строке: чекбокс расчёта по формуле
		private void PackSizeOnCheckedChanged(object sender, EventArgs eventArgs)
		{
			if (internalUpdateControls)
				return;

			var control = (CheckBox)sender;

			if (control == null)
				return;

			var code = control.Tag.ToString();
			var num = HashList[code];

			UpdatePriceAfterChangeFormulaCalc(code, num, control.Checked);

			RecalcLinkedRes(code);
		}

		/// <summary>
		/// Рекурсивный пересчёт цен по формулам
		/// </summary>
		private void RecalcLinkedRes(string code)
		{
			RecalcLinkedRes(code, true);
		}

		private void RecalcLinkedRes(string code, bool first)
		{
			// здесь отсекаются ресы, которые в дальнейшем нигде не используются
			if (!HashList.ContainsKey(code))
				return;

			if (!first)
				UpdatePriceAfterChangeFormulaCalc(code, HashList[code],
				                                  SiegeMarket.Items[code].FormulaCalc && SiegeMarket.Items[code].FormulaCalcEnabled &&
				                                  !SiegeMarket.Items[code].FormulaCalcDisabled);

			if (SiegeMarket.Items[code].CalcLink != null)
				foreach (var res in SiegeMarket.Items[code].CalcLink)
				{
					RecalcLinkedRes(res, false);
				}
		}

		// обработчик изменений в строке: количество ресов в пачке
		private void NumboxPackSizeOnValueChanged(object sender, EventArgs eventArgs)
		{
			if (internalUpdateControls)
				return;

			var control = (NumericUpDown)sender;

			if (control == null)
				return;

			SetPackSizeValue(control, (int)control.Value);

			var code = control.Tag.ToString();
			//var item = SiegeMarket.Items[code];

			// обновим цены зависимых ресов
			if (Console.CapsLock)
				RecalcLinkedRes(code);
		}

        private void SetPackSizeValue(NumericUpDown control, double value)
		{
			SetPackSizeValue(control, value, false);
		}

		private void SetPackSizeValue(NumericUpDown control, double value, bool recurse)
		{
			var code = control.Tag.ToString();
			var num = HashList[code];
			var item = SiegeMarket.Items[code];

			var priceType = control.Name.StartsWith("rowBuy") ? priceBuy : priceSell;

			// если идёт изменение пачки под цену, тогда подкорректируем цену за одну штуку реса
			if ((Console.CapsLock && !forceModeResize) && value != 0)
			{
				item.UserGold[priceType] = item.PackSize[priceType] / (double)value * item.UserGold[priceType];

				/*if (code != "diamond")
					item.UserDiamond[priceType] = item.PackSize[priceType] / (double)value * item.UserDiamond[priceType];*/
			}

			item.PackSize[priceType] = value;

			if ((int)control.Value != value)
			{
				internalUpdateControls = true;
				SetNumericUpDownValue(control, value, false);
				internalUpdateControls = false;
			}

			// если идёт масштабирование пачки, то пересчитаем цену пачки
			UpdatePriceAfterChangeFormulaCalc(code, num, item.FormulaCalcEnabled);

			// поменяем размер второй пачки, если они связаны
			if (chbxFixedPack.Checked && !Console.CapsLock && !recurse)
			{
				SetPackSizeValue((NumericUpDown)panelMain.Controls[_rowPack[priceType] + num], value, true);
			}

		}

        
		//------------------------------------------------------------------

		// обработчик нажатия кнопок в пределах формы
		private void frmMarket_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F4)
				InitControls();

			if (e.Control && e.KeyCode == Keys.S)
				SaveMarket();

			if (ActiveControl.GetType() == typeof(NumericUpDown))
			{
				forceModeSet = ActiveControl.Name.Contains("Gold") && ActiveControl.Tag.ToString() == "diamond";

				ActiveControl.BackColor = (Console.CapsLock && !forceModeResize) || forceModeSet ? Color.LightCoral : Color.LightGreen;
			}
		}

		#region Подсветка контролов по КапсЛоку
        private void ControlOnGotFocus(object sender, EventArgs eventArgs)
		{
			ControlOnEnter(sender, eventArgs);
		}

		private void ControlOnLeave(object sender, EventArgs eventArgs)
		{
			var control = (NumericUpDown)sender;

			if (control == null)
				return;

			control.BackColor = TransparencyKey;
		}

		private void ControlOnEnter(object sender, EventArgs eventArgs)
		{
			var control = (NumericUpDown)sender;

			if (control == null)
				return;

			forceModeSet = control.Name.Contains("Gold") && control.Tag.ToString() == "diamond";

			SetNumericUpDownValue(control, (double)control.Value, true);

			control.BackColor = (Console.CapsLock && !forceModeResize) || forceModeSet ? Color.LightCoral : Color.LightGreen;

		}
		#endregion

		private void ControlOnEnabledChanged(object sender, EventArgs e)
		{
			var control = (NumericUpDown)sender;

			if (control == null)
				return;

			SetNumericUpDownValue(control, (double)control.Value, true);
		}

		private void SetNumericUpDownValue(Control control, double value, bool onlyLastValue)
		{
			if (control == null || control.GetType() != typeof(NumericUpDown))
				return;

			if (!onlyLastValue)
			{
				var cont = (NumericUpDown) control;
				cont.Value = (decimal) value;
				toolTip.SetToolTip(cont, cont.Value.ToString());
			}

			control.MaximumSize = new Size((int)(maxSizeAdd * maxSizeAdd + value * maxSizeAdd), (int) maxSizeAdd);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Hide();

			SaveMarket();
		}

		// вместо закрытия просто прячем окошко
		private void frmMarket_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				Hide();

				e.Cancel = true;
			}
			
			SaveMarket();
		}

		// сохраняем конфиг маркета
		private void SaveMarket()
		{
			if (SystemInfo.SecurCode.CheckAccess("market"))
			{
				var market = SiegeMarket.Items.Keys; //.Where(s => SiegeDataBase.DataBuildings.NeededResources.Contains(s) || s == "diamond").ToList();
				if (market.Count > 0)
				{
					var config = new Config();

					foreach (string key in market)
					{
						var item = SiegeMarket.Items[key];

						if (item.FormulaCalc && !item.FormulaCalcDisabled)
							config.WriteValue(key, "CalcFormula", item.FormulaCalc.ToString());

						config.WriteValue(key, "BuyPack", item.PackSize[priceBuy].ToString());
						config.WriteValue(key, "Buy", item.UserGold[priceBuy].ToString());
						config.WriteValue(key, "SellPack", item.PackSize[priceSell].ToString());
						config.WriteValue(key, "Sell", item.UserGold[priceSell].ToString());
					}

					config.Save(Path.Combine(Environment.CurrentDirectory, SiegeMarket.ConfigFile));
				}
			}
		}

		private void panelMain_Click(object sender, EventArgs e)
		{
			ActiveControl = (Panel) sender;
		}


	}
}
