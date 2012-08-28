using System.Windows.Forms;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
    /// <summary>
    /// Класс реализующий описание объектов.
    /// </summary>
	public class SiegeObjectDescriptionClass : SiegeCommonAbstractDictionary<SiegeObjectDescriptionStructure>
	{
		/// <summary>
        /// Конструктор по умолчанию.
		/// </summary>
		public SiegeObjectDescriptionClass(string file)
		{
            Files.Clear();
            Files.Add(file);
		}

		/// <summary>
		/// Обработка данных.
		/// </summary>
		public override void NodeParse(SiegeDataBaseNode nodes, ToolStripProgressBar progressBar)
		{
			foreach (SiegeDataBaseNode unit in nodes.Childs["Unit"])
			{
				if (!unit.Attributes.ContainsKey("UnitName"))
					continue;

				var code = unit.GetAttribute("UnitName");

				if (code == "")
					continue;

				var state = unit.GetChild("State");

				var data = new SiegeObjectDescriptionStructure
				           	{
				           		UnitName = code,
				           		ClientObjectRef = state.GetAttribute("ClientObjectRef"),
				           		ServerSquad = state.GetAttribute("ServerSquad"),
				           		ServerSoul = state.GetAttribute("ServerSoul"),
				           		ServerUnitAI = state.GetAttribute("ServerUnitAI"),
				           		ServerUnitMover = state.GetAttribute("ServerUnitMover"),
								ServerWeapon = state.GetAttribute("ServerWeapon"),
								ServerArtefact = state.GetAttribute("ServerArtefact")
				           	};

				Add(code, data);
			}

		}

		/// <summary>
		/// Получить инфу по линкам через код сквада.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		public bool GetByServerSquad(string str, out SiegeObjectDescriptionStructure result)
		{
			result = null;

			foreach (var item in this)
			{
				if (item.Value.ServerSquad == str)
				{
					result = item.Value;
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Получить инфу по линкам через код сквада.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		public bool GetByServerArtefact(string str, out SiegeObjectDescriptionStructure result)
		{
			result = null;

			foreach (var item in this)
			{
				if (item.Value.ServerArtefact == str)
				{
					result = item.Value;
					return true;
				}
			}

			return false;
		}
	}

	//=======================================================================================================

	public class SiegeObjectDescriptionStructure
	{
		/// <summary>
		/// Код юнита
		/// </summary>
		public string UnitName;			
        
		/// <summary>
		/// Локальный код
		/// </summary>
		public string ClientObjectRef;	
		
		/// <summary>
		/// Ссылка на сквад
		/// </summary>
		public string ServerSquad;		
		
		/// <summary>
		/// Характеристики тела
		/// </summary>
		public string ServerSoul;		
		
		/// <summary>
		/// Класс интеллекта юнита
		/// </summary>
		public string ServerUnitAI;		
		
		/// <summary>
		/// Характеристики скорости
		/// </summary>
		public string ServerUnitMover;	
		
		/// <summary>
		/// Характеристики оружия
		/// </summary>
		public string ServerWeapon;

		/// <summary>
		/// Характеристики артефакта
		/// </summary>
		public string ServerArtefact;	
	}
}