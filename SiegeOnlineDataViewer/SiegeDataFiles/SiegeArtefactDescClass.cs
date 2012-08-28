using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
    /// <summary>
    /// Класс описания Артефактов.
    /// </summary>
	public class SiegeArtefactDescClass : SiegeCommonAbstractDictionary<SiegeArtefactDescStructure>
	{
		public List<string> Maps { get; private set; }

		/// <summary>
        ///  Конструктор по умолчанию.
		/// </summary>
		/// <param name="file"></param>
		public SiegeArtefactDescClass(string file)
		{
            Files.Clear();
			Files.Add(file);
		}

		/// <summary>
		/// Обработка данных.
		/// </summary>
		public override void NodeParse(SiegeDataBaseNode nodes, ToolStripProgressBar progressBar)
		{
			Maps = new List<string>();

			foreach (SiegeDataBaseNode node in nodes.GetChilds("Artefact"))
			{
				if (!node.Attributes.ContainsKey("Name"))
					continue;

				var name = node.GetAttribute("Name");
				var data = new SiegeArtefactDescStructure
				           	{
				           		Name = name,
								Restorable = node.GetAttributeAsInt("Restorable") == 1
				           	};
                SiegeDataBaseNode child;

				if (node.GetChild("MapData", out child))
				{
					if (!Maps.Contains(name))
						Maps.Add(name);

					string reg;

					if (child.GetAttribute("MapRegion", out reg))
					{
                        var region = reg.Split(';');

						if (region.Length == 4)
						{
							data.MapData = new SiegeMapStructure
							               	{
							               		DrawPlayer = child.GetAttributeAsInt("DrawPlayer") == 1,
							               		DrawMarkers = child.GetAttributeAsInt("DrawMarkers") == 1,
							               		MapSprite = child.GetAttribute("MapSprite"),
							               		CityID = child.GetAttributeAsInt("CityID")
							               	};

							int.TryParse(region[0], out data.MapData.MapRegion.Point1.X);
							int.TryParse(region[1], out data.MapData.MapRegion.Point1.Y);
							int.TryParse(region[2], out data.MapData.MapRegion.Point2.X);
							int.TryParse(region[3], out data.MapData.MapRegion.Point2.Y);

							var markers = child.GetChilds("Marker");

							foreach (var marker in markers)
							{
								data.MapData.Markers.Add(new SiegeMapStructure.MarkerClass
								                         	{
								                         		Npc = marker.GetAttribute("Npc"),
								                         		Name = marker.GetAttribute("Name"),
								                         		Tooltip = marker.GetAttribute("Tooltip"),
								                         		Effect = marker.GetAttribute("Effect")
								                         	});
							}
						}
					}
				}

				data.Description = SiegeDataBase.DataLanguages.Get(data.Name);

				if (data.Description == null)
				{
					SiegeObjectDescriptionStructure desc;

					if (SiegeDataBase.DataObjectDescription.GetByServerArtefact(data.Name, out desc))
						data.Description = SiegeDataBase.DataLanguages.Get(desc.UnitName);
				}

				if (data.Description == null)
					data.Description = data.Name;
				
				//var code = name.ToLower();
				if (!ContainsKey(name))
					Add(name, data);
			}
        }
	}

    /// <summary>
    /// Структура описания артефакта
    /// </summary>
	public class SiegeArtefactDescStructure
	{
		/// <summary>
		/// Код артефакта
		/// </summary>
		public string Name;

		/// <summary>
		/// Наименование артефакта
		/// </summary>
		public string Description;

		/// <summary>
		/// Бесконечный артефакт
		/// </summary>
		public bool Restorable;

		/// <summary>
		/// Данные карты (если написано в артефакте, то он будет являтся картой)
		/// </summary>
		public SiegeMapStructure MapData;
	}

    /// <summary>
    /// Структура карты.
    /// </summary>
	public class SiegeMapStructure
	{
		public SizeStruct MapRegion;

		public bool DrawPlayer;
		public bool DrawMarkers;

		/// <summary>
		/// Текстура карты
		/// </summary>
		public string MapSprite;

		public int CityID;

		/// <summary>
		/// Изображение карты
		/// </summary>
		public Image Map;

		public List<MarkerClass> Markers = new List<MarkerClass>();

        /// <summary>
        /// Класс маркера.
        /// </summary>
		public class MarkerClass
		{
			public string Npc;
			public string Name;
			public string Tooltip;
			public string Effect;
		}

		public struct SizeStruct
		{
			public PointStruct Point1;
			public PointStruct Point2;
		}

		public struct PointStruct
		{
			public int X;
			public int Y;
		}
	}
}