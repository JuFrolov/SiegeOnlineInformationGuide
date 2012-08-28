using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SiegeOnlineDataViewer.SiegeDataFiles;
using SiegeOnlineDataViewer.Utils;

namespace SiegeOnlineDataViewer.FormControls
{
	internal class IconListControl
	{
		#region Свойства контрола

		/// <summary>
		/// Флаг линковки текста
		/// </summary>
		public bool LinkedText;

		/// <summary>
		/// Контрол родителя - инициализируется только один раз!
		/// </summary>
		public Control ParentControl
		{
			get { return _parentControl; }
			set { if (_parentControl == null) _parentControl = value; }
		}

		/// <summary>
		/// Прицепленный дочерний контрол
		/// </summary>
		public IconListControl LinkedControl { get; set; }

		/// <summary>
		/// Левый верхний угол контрола
		/// </summary>
		public int Left { get; set; }

		/// <summary>
		/// Левый верхний угол контрола
		/// </summary>
		public int Top { get; set; }

		/// <summary>
		/// Формат вывода строки ({0} = описание, {1} = количество)
		/// Если в формате есть разделитель "|", то второй формат - для форматирования числа
		/// </summary>
		public string LabelFormat { get; set; }

		/// <summary>
		/// Приводить ли строку к нижнему регистру
		/// </summary>
		public bool LabelToLower { get; set; }

		/// <summary>
		/// Размер текста (pt)
		/// </summary>
		public int[] IconSize { get; set; }

		/// <summary>
		/// Размер текста (pt)
		/// </summary>
		public float[] FontSize { get; set; }

		/// <summary>
		/// Ширина расчётного списка (при отрицательной или равной нулю величине ширина расчитывается от правого края)
		/// </summary>
		public int Width { get; set; }

		/// <summary>
		/// Заголовок списка
		/// </summary>
		public string Title { get; set; }

		#endregion

		//----[ публичные свойства ]----

		//private readonly List<Label> _labelsList = new List<Label>();
		private const int ConstLinkedControlPadding = 8; // расстояние между блоками текста на странице продукции здания
		private const int ConstTitlePaddingBottom = 2; // расстояние после заголовка
		private readonly List<LinkLabel> _labelsList = new List<LinkLabel>();
		private readonly List<PictureBox> _picturesList = new List<PictureBox>();

		private int _itemsCount;
		private Label _labelTitle;

		private Control _parentControl;

		#region Устарело

		/*
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="parent">Родительский контрол</param>
		/// <param name="pointLeftTop">Верхняя левая точка отрисовки</param>
		/// <param name="width">Ширина расчётного списка (при отрицательной или равной нулю величине ширина расчитывается от правого края)</param>
		/// <param name="gamePath">Путь до папки с игрой</param>
		/// <param name="dataObjectsUI">Словарь текстур</param>
		/// <param name="dataLanguage">Словарь локализации</param>
		/// <param name="labelFormat">Формат вывода строки (0 = описание, 1 = количество)</param>
		/// <param name="labelToLower">Приводить ли строку к нижнему регистру</param>
		/// <param name="iconSize">Размер иконки (px)</param>
		/// <param name="fontSize">Размер текста (pt)</param>
		public IconListControl(Control parent, Point pointLeftTop, int width, string gamePath, string labelFormat, bool labelToLower, int iconSize, float fontSize, IDictionary<string, string> dataObjectsUI, SiegeLanguagesClass dataLanguage)
		{
			Parent = parent;
			PointLeftTop = pointLeftTop;
			GamePath = gamePath;
			LabelFormat = labelFormat;
			LabelToLower = labelToLower;
			IconSize = iconSize;
			FontSize = fontSize;
			DataObjectsUi = dataObjectsUI;
			DataLanguage = dataLanguage;
			_parentControl = parent;
			_pointLeftTop = pointLeftTop;
			Width = width;
			_gamePath = gamePath;
			_labelFormat = labelFormat;
			_labelToLower = labelToLower;
			_iconSize = iconSize;
			_fontSize = fontSize;
			_dataObjectsUI = dataObjectsUI;
			_dataLanguage = dataLanguage;
		}
		*/

		#endregion

		//----[ константы ]----

		//=============================================================================================

		/// <summary>
		/// Конструктор
		/// </summary>
		public IconListControl()
		{
			Left = 10;
			Top = 10;
			LabelFormat = "";
			IconSize = new[] {32, 48};
			FontSize = new[] {(float) 8.25, 10};
		}

		public event LinkLabelLinkClickedEventHandler OnClick;

		/*
		/// <summary>
		/// Обновить контрол
		/// </summary>
		/// <param name="dataList">Список строк (пара строка-количество)</param>
		/// <param name="sizeChecked">Размер иконок (true = большие)</param>
		public void UpdateControl(IList<SiegeBuildingStructure.ResoursePair> dataList, bool sizeChecked)
		{
			UpdateControl(dataList, sizeChecked, Top);
		}
		*/

		/// <summary>
		/// Обновить контрол
		/// </summary>
		/// <param name="strList">Список строк (пара строка-количество)</param>
		/// <param name="sizeChecked">Размер иконок (true = большие)</param>
		public void UpdateControl(IList<string> strList, bool sizeChecked)
		{
			var list = new SiegeDataBase.ResourseDictionary();

            foreach (var str in strList)
			{
				var pos = str.IndexOf(',');
				
				var id = pos > 0 ? Functions.SubString(str, 0, pos).Trim() : str;
				
				var count = 1;
				if (pos > 0)
					Int32.TryParse(Functions.SubString(str, pos + 1).Trim(), out count);

				list.Add(id, count);
			}

			UpdateControl(list, sizeChecked);
		}

		/// <summary>
		/// Обновить контрол
		/// </summary>
		/// <param name="dataListIn">Список строк (пара строка-количество)</param>
		/// <param name="sizeChecked">Размер иконок (true = большие)</param>
		public void UpdateControl(SiegeDataBase.ResourseDictionary dataListIn, bool sizeChecked)
		{
			var iconSize = IconSize[Convert.ToInt32(sizeChecked)];
			var fontSize = FontSize[Convert.ToInt32(sizeChecked)];

			// при необходимости добавим заголовок
			var addintionalTopFromTitle = 0;
			if (!string.IsNullOrEmpty(Title))
			{
				if (_labelTitle == null)
				{
					_labelTitle = new Label
					              	{
					              		Text = Title,
					              		Location = new Point(Left, Top),
					              		AutoSize = true
					              	};

					_parentControl.Controls.Add(_labelTitle);
				}
				else
				{
					_labelTitle.Left = Left;
					_labelTitle.Top = Top;
				}

				_labelTitle.Visible = dataListIn.Count > 0;

				if (dataListIn.Count > 0)
					addintionalTopFromTitle = _labelTitle.Height + ConstTitlePaddingBottom;
			}

			// размножим список, если нужно
			var dataList = new SiegeDataBase.ResourseDictionary();
			foreach (var data in dataListIn)
			{
				var id = data.Key.Split(';');
				var count = data.Value;

				for (var i = 0; i < id.Length; i++)
				{
					var code = id[i];
					if (i > 0)
						code = "•" + code;

					dataList.Add(code, count);
				}
			}


			_itemsCount = dataList.Count;

			var hideenLines = 0;
			var hiddenList = new List<string>();

			// рассчитаем внешний вид контрола
			//var maxPict = Math.Max(_picturesList.Count, dataList.Count);
			//for (var i = 0; i < dataList.Count; i++)
			var k = -1;
			foreach (var data in dataList)
			{
				k++;

				var formatString = LabelFormat;

				// сохранили код
				var resCode = data.Key;

				// может ли строка быть линком
				bool linked = false;

				bool hidden = false;

				// определим или-строки
				var orCode = resCode.StartsWith("•");
				if (orCode)
					resCode = Functions.SubString(resCode, 1);

				// рассчитаем описание строки
				string resName = null;
				if (!Functions.inList(resCode.ToLower(), "level", "rank", "castlerating"))
					linked = SiegeDataBase.DataLanguages.Get(resCode, out resName);

				if (string.IsNullOrEmpty(resName))
				{
					bool res;
					var building = SiegeDataBase.DataBuildings.Get(resCode, out res);

					if (res)
					{
						resName = building.Name;
						linked = true;
					}
				}

				// заодно определим файл иконки (ибо потом поздно будет)
				var resFile = SiegeDataBase.DataObjectsUi.Get(resCode);

				// если всё равно имени нет, значит там не код, а просто текст
				if (string.IsNullOrEmpty(resName))
				{
					resName = SiegeDataBase.GetLocalizedStringRequirements(resCode);
					resFile = SiegeDataBase.PictureNotAvailable;
				}

				if (hiddenList.Contains(resName))
				{
					hidden = true;
					hideenLines++;
				}
				else
					hiddenList.Add(resName);


				PictureBox pict;
				LinkLabel label;
				if (_picturesList.Count <= k)
				{
					// создаём каждую новую строчку
					pict = new PictureBox
					       	{
					       		Size = new Size(iconSize, iconSize),
					       		Anchor = AnchorStyles.Left | AnchorStyles.Top,
					       		SizeMode = PictureBoxSizeMode.Zoom
					       	};

					label = new LinkLabel
					        	{
					        		Anchor = Width > 0
					        		         	? AnchorStyles.Left | AnchorStyles.Top
					        		         	: AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
					        		TextAlign = ContentAlignment.MiddleLeft,
					        		LinkArea = new LinkArea()
					        	};

					label.AutoSize = Width == 0;

					if (OnClick != null)
						label.LinkClicked += OnClick;

					_parentControl.Controls.Add(pict);
					_parentControl.Controls.Add(label);

					_picturesList.Add(pict);
					_labelsList.Add(label);
				}
				else
				{
					pict = _picturesList[k];
					label = _labelsList[k];

					pict.Visible = true;
					label.Visible = true;
				}

				if (hidden)
				{
					pict.Visible = false;
					label.Visible = false;
					continue;
				}

				pict.Left = Left + 2;
				pict.Top = Top + addintionalTopFromTitle + iconSize*(k - hideenLines);
				pict.Width = iconSize;
				pict.Height = iconSize;

				label.Left = pict.Left + iconSize;
				label.Top = Top + addintionalTopFromTitle + iconSize*(k - hideenLines);
				label.Width = _parentControl.ClientSize.Width - pict.Left + Width - iconSize;
				label.Height = iconSize;
				label.Font = new Font(label.Font.FontFamily, fontSize, label.Font.Style);
				label.Tag = linked ? resCode : null;

				// рассчитаем иконку
				var img = Path.Combine(SiegeDataBase.GamePath, @"Data\Textures\");
				img = Path.Combine(img, resFile ?? SiegeDataBase.PictureError);

				pict.Image = TgaImages.GetFromFile(img);


				// расчитаем текст
				if (!linked)
					formatString = formatString.Replace("•", "");

				// проверяем формат вывода строки
				// 0 - формат всей строки
				// 1 - формат количественных данных
				var format = formatString.Split('|');

				// цифра количества форматируется лишь в том случае, если строка является ссылкой 
				// (при этом нулевое значение преобразуется в пустое)
				var formatedCount = linked && format.Length > 1
					? data.Value > 1 ? string.Format(format[1], data.Value) : ""
					: data.Value.ToString();

				label.Text = string.Format(format[0],
				                           LabelToLower ? (resName ?? "").ToLower() : resName,
				                           format.Length == 1
											? data.Value.ToString()
											: formatedCount);

				// отметим пометочкой или-строки
				if (orCode)
				{
					label.Text = "или " + label.Text;
					pict.Visible = false;
				}

				// расчитаем границы линка, еслиэто необходимо
				var link1 = label.Text.IndexOf("•");
				var link2 = label.Text.LastIndexOf("•");
                label.LinkArea = LinkedText && linked && link1 >= 0
				                 	? new LinkArea(link1, link2 - 2)
				                 	: new LinkArea();

				// удаляем метки линка
				if (link1 >= 0)
					label.Text = label.Text.Replace("•", "");
			}

			// убираем лишние строки
			for (var i = dataList.Count; i < _picturesList.Count; i++)
			{
				_picturesList[i].Visible = false;
				_labelsList[i].Visible = false;
			}


			// сместим дочерний контрол, при необходимости
			if (LinkedControl != null)
			{
				LinkedControl.Top = Top + addintionalTopFromTitle + iconSize*(_itemsCount - hideenLines) + ConstLinkedControlPadding;
			}
		}

		/// <summary>
		/// Очистить список
		/// </summary>
		public void Clear()
		{
			foreach (var pict in _picturesList)
			{
				pict.Visible = false;
			}

			foreach (var label in _labelsList)
			{
				label.Visible = false;
			}

			if (_labelTitle != null)
				_labelTitle.Visible = false;
		}

		/*
		/// <summary>
		/// Расчитать нижнюю границу контрола
		/// </summary>
		/// <param name="sizeChecked">Размер иконок (true = большие)</param>
		/// <returns></returns>
		public int GetBottomLine(bool sizeChecked)
		{
			var line = PointLeftTop.Y;

			var iconSize = IconSize[Convert.ToInt32(sizeChecked)];

			line += _itemsCount * iconSize;
			
			if (_labelTitle != null)
				line +=  _labelTitle.Height + ConstTitlePaddingBottom;

			return line;
		}
		*/
	}
}