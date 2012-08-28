using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SiegeOnlineDataViewer.SiegeDataFiles
{
    /// <summary>
    /// Класс описывающий квесты.
    /// </summary>
	public class SiegeQuestClass : SiegeCommonAbstractDictionary<SiegeQuestStructure>
	{
		/// <summary>
        /// Конструктор по умолчанию.
		/// </summary>
		/// <param name="file"></param>
		public SiegeQuestClass(string file)
		{
            Files.Clear();
            Files.Add(file);
		}

		/// <summary>
		/// Обработка данных
		/// </summary>
		public override void NodeParse(SiegeDataBaseNode nodes, ToolStripProgressBar progressBar)
		{
			foreach (SiegeDataBaseNode node in nodes.GetChilds("Quest"))
			{
				if (!node.Attributes.ContainsKey("ID"))
					continue;

				var data = new SiegeQuestStructure
				           	{
								ID = node.GetAttribute("ID"),
								QuestGiver = node.GetAttribute("QuestGiver"),
								QuestTaker = node.GetAttribute("QuestTaker"),
								Description = node.GetAttribute("Description"),
								CompleteDescription = node.GetAttribute("CompleteDescription"),
								ResetTime = node.GetAttributeAsDouble("ResetTime")
				           	};
                
				data.Name = SiegeDataBase.DataLanguages.Get(data.ID) ?? data.ID;
                
				if (!string.IsNullOrEmpty(data.QuestGiver))
					data.QuestGiver = data.QuestGiver.Trim();

				if (!string.IsNullOrEmpty(data.QuestTaker))
					data.QuestTaker = data.QuestTaker.Trim();

				// вытащим описание квеста
				string str;
				if (SiegeDataBase.DataLanguages.Get(data.Description, out str))
				{
					var arr = str.Split(new [] { @"\n" }, StringSplitOptions.None);
					
					data.Description = String.Join(Environment.NewLine, arr);
				}

				// вытащим завершение квеста
				if (SiegeDataBase.DataLanguages.Get(data.CompleteDescription, out str))
				{
					var arr = str.Split(new[] { @"\n" }, StringSplitOptions.None);

					data.CompleteDescription = String.Join(Environment.NewLine, arr);
				}


				SiegeDataBaseNode child;
				List<SiegeDataBaseNode> childs;
				
				#region время обновления квеста
				if (node.GetChilds("TimeInterval", out childs))
				{
					data.TimeInterval = new List<SiegeQuestStructure.TimeIntervalStruct>();

					foreach (SiegeDataBaseNode ch in childs)
					{
						var t = new SiegeQuestStructure.TimeIntervalStruct
						        	{
										Begin = ch.GetAttribute("Begin").Trim(),
						        		Hours = ch.GetAttributeAsDouble("Hours")
						        	};

						data.TimeInterval.Add(t);
					}
				}

				if (node.GetChilds("DateTimeInterval", out childs))
				{
					data.DateTimeInterval = new List<SiegeQuestStructure.TimeIntervalStruct>();

					foreach (SiegeDataBaseNode ch in childs)
					{
						var t = new SiegeQuestStructure.TimeIntervalStruct
						{
							Begin = ch.GetAttribute("Begin").Trim(),
							Hours = ch.GetAttributeAsDouble("Hours")
						};

						data.DateTimeInterval.Add(t);
					}
				}

				if (node.GetChilds("WeekDayInterval", out childs))
				{
					data.WeekDayInterval = new Dictionary<string, SiegeQuestStructure.TimeIntervalStruct>();

					foreach (SiegeDataBaseNode ch in childs)
					{
						var t = new SiegeQuestStructure.TimeIntervalStruct
						{
							Begin = ch.GetAttribute("Begin").Trim(),
							Hours = ch.GetAttributeAsDouble("Hours")
						};

						var weekday = (ch.GetAttribute("WeekDay") ?? "").Trim();

						if (weekday != "" && !data.WeekDayInterval.ContainsKey(weekday))
							data.WeekDayInterval.Add(weekday, t);
					}
				}
				#endregion

				#region обработаем условия получения квеста
				if (node.GetChild("AcceptConditions", out child))
				{
					data.AcceptConditions = new SiegeQuestStructure.AcceptConditionsClass();

					List<string> ret;
					if (child.GetAttributes("CompleteQuest", out ret))
						data.AcceptConditions.CompleteQuest = ret;
					

					data.AcceptConditions.BeginLevel = child.GetAttributeAsInt("BeginLevel");
					data.AcceptConditions.EndLevel = child.GetAttributeAsInt("EndLevel");

					data.AcceptConditions.ControlPointOwned = child.GetAttribute("ControlPointOwned");
					if (data.AcceptConditions.ControlPointOwned != null)
						data.AcceptConditions.ControlPointOwned = SiegeDataBase.DataLanguages.Get(data.AcceptConditions.ControlPointOwned);

					if (child.GetAttributes("Reputation", out ret))
						data.AcceptConditions.Reputation = ret;

					if (child.GetAttributes("ReputationGreaterEqual", out ret))
						data.AcceptConditions.ReputationGreaterEqual = ret;

					if (child.GetAttributes("ReputationLess", out ret))
						data.AcceptConditions.ReputationLess = ret;

					if (child.GetAttributes("ReputationEqual", out ret))
						data.AcceptConditions.ReputationEqual = ret;

					if (child.GetAttributes("RepRequiredNotToBe", out ret))
						data.AcceptConditions.RepRequiredNotToBe = ret;

					data.AcceptConditions.TitleRequired = child.GetAttribute("TitleRequired");

					data.AcceptConditions.MinLoyaltyOfParentHouse = child.GetAttributeAsInt("MinLoyaltyOfParentHouse");

					data.AcceptConditions.QuestForBuilderOfParentHouse = child.GetAttributeAsInt("QuestForBuilderOfParentHouse");
				}
				#endregion

				#region обработаем условия выполнения квеста
				if (node.GetChild("CompleteConditions", out child))
				{
					data.CompleteConditions = new SiegeQuestStructure.CompleteConditionsClass
					                              {
					                                  Condition = child.GetAttribute("Condition"),
					                                  ConditionList = new SiegeCommonDataDictionary<object>()
					                              };
				    var attrList = new List<string>
					            	{
					            		"BagItem", "Item", "EquipItem", "EnemyName", "ControlPoint", "Reputation"
					            	};

					foreach (var attr in attrList)
					{
						var arr = child.GetAttributes(attr);

						if (arr != null && arr.Count > 0)
							data.CompleteConditions.ConditionList.Add(attr, arr);
					}

					var attrInt = new List<string>
					            	{
					            		"RemoveItems", "RemoveAllItems", "RemoveResource", "Count", "Rank", "Rating", "MinTitle", "MaxTitle", "LandCount"
					            	};

					foreach (var attr in attrInt)
					{
						var i = child.GetAttributeAsInt(attr);

						if (i > 0)
							data.CompleteConditions.ConditionList.Add(attr, i);
					}

					var attrRes = new Dictionary<string, string>
					              	{
                                        {"Fund", "ID"}, 
										{"Building", "Name"}
									};

					foreach (var pairs in attrRes)
					{
						var funds = child.GetChilds(pairs.Key);

						if (funds != null && funds.Count > 0)
						{
							var res = new SiegeDataBase.ResourseDictionary();

							foreach (var fund in funds)
							{
								var code = fund.GetAttribute(pairs.Value);
								var count = fund.GetAttributeAsDouble("Count");

								if (!string.IsNullOrEmpty(code) && count > 0)
									res.Add(code, count);
							}

							data.CompleteConditions.ConditionList.Add(pairs.Key, res);
						}
					}
				}
				#endregion

				#region обработаем предметы для квеста
				if (node.GetChild("GiveOnAccept", out child))
				{
					data.GiveOnAccept = new SiegeQuestStructure.GiveOnAcceptClass
					                    	{
												Name = "Accept",
					                    		Xp = child.GetAttributeAsInt("XP"),
					                    		Item = child.GetAttributes("Item")
					                    	};
                    var funds = child.GetChilds("Fund");

					if (funds != null && funds.Count > 0)
					{
						data.GiveOnAccept.Fund = new SiegeDataBase.ResourseDictionary();

						foreach (var fund in funds)
						{
							var code = fund.GetAttribute("ID");
							var count = fund.GetAttributeAsDouble("Count");

							data.GiveOnAccept.Fund.Add(code, count);
						}
					}

					var arr = child.GetAttributes("Reputation");

					if (arr != null && arr.Count > 0)
						data.GiveOnAccept.Reputation = arr;

					data.GiveOnAccept.SetCastle = child.GetAttribute("SetCastle");

				}
				#endregion

				#region обработаем награды за квест
				if (node.GetChild("Reward", out child))
				{
					data.Reward = new SiegeQuestStructure.GiveOnAcceptClass
					{
						Name = "Reward",
						Xp = child.GetAttributeAsInt("XP"),
						Item = child.GetAttributes("Item"),
						Title = child.GetAttribute("Title")
					};

					int pos = 0;
					while (pos >= 0 && pos < data.Reward.Item.Count)
					{
						var item = data.Reward.Item[pos];
						var c = data.Reward.Item.FindAll(s => s == item).Count;

						if (c > 1)
						{
							data.Reward.Item.RemoveAll(s => s == item);

							var a = item.Split(';');
							var name = a[0];
							var level = 0;

							if (a.Length > 1)
								int.TryParse(a[1], out level);

							var count = 1;

							if (a.Length > 2)
								int.TryParse(a[2], out count);

							data.Reward.Item.Add(string.Format("{0};{1};{2}", name, level, c * count));

							pos = 0;

							continue;
						}

						pos++;
					}

					var funds = child.GetChilds("Fund");

					if (funds != null && funds.Count > 0)
					{
						data.Reward.Fund = new SiegeDataBase.ResourseDictionary();

						foreach (var fund in funds)
						{
							var code = fund.GetAttribute("ID");
							var count = fund.GetAttributeAsDouble("Count");

							data.Reward.Fund.Add(code, count);
						}
					}

					var arr = child.GetAttributes("Reputation");
					if (arr != null && arr.Count > 0)
						data.Reward.Reputation = arr;

					arr = child.GetAttributes("Loyalty");
                    if (arr != null && arr.Count > 0)
						data.Reward.Loyalty = arr;

					data.Reward.SetCastle = child.GetAttribute("SetCastle");

				}
				#endregion

				#region обработаем остатки
				if (node.GetChild("ExtractRewards", out child))
				{
					data.ExtractRewards = new SiegeDataBase.ResourseDictionary();

					var funds = child.GetChilds("Fund");
					foreach (SiegeDataBaseNode fund in funds)
					{
						var name = fund.GetAttribute("ID");
						var val = fund.GetAttributeAsDouble("Count");

						if (!string.IsNullOrEmpty(name) && val > 0)
							data.ExtractRewards.Add(name, val);
					}
				}

				if (node.GetChild("TeleportOnGiveUp", out child))
				{
					var city = child.GetAttributeAsInt("CityID");
					var point = child.GetAttribute("Point");

					if (!string.IsNullOrEmpty(point))
					{
						data.Teleport.CityId = city;
						data.Teleport.City = "Карты в данной версии отключены";
						data.Teleport.Point = point;
					}
				}
			

				#endregion

				if (!ContainsKey(data.ID))
					Add(data.ID, data);
			}

		}
	}

    /// <summary>
    /// Класс описывающий структуры Квеста.
    /// </summary>
	public class SiegeQuestStructure
	{
		/// <summary>
		/// Идентификатор квеста (оно же имя - из strings.dat)
		/// </summary>
		public string ID;

        /// <summary>
        /// Название квеста.
        /// </summary>
		public string Name;

        /// <summary>
        /// У кого берется квест.
        /// </summary>
		public string QuestGiver;

        /// <summary>
        /// Кому сдается квест.
        /// </summary>
		public string QuestTaker;

		/// <summary>
		/// Описание квеста (берется из Strings.dat)
		/// </summary>
		public string Description;

		/// <summary>
		/// Финальный текст (strings.dat после выполнения квеста, выводится энписёй, которой сдаем квест)
		/// </summary>
		public string CompleteDescription;

		/// <summary>
		/// Частота регенерации квеста
		/// </summary>
		public double ResetTime;

		/// <summary>
		/// Описание квеста в квестлоге (если нету - совпадает с Description)
		/// </summary>
		public string LogDescription;

		/// <summary>
		/// Коротко-записаные задания (если не написано, то по умолчанию берется - ID_Object)
		/// </summary>
		public string Objectives;

        /// <summary>
		/// Квест может быть сдан только в том-же городе что и взят (по умолчанию 1)
        /// </summary>
		public int Local;

		/// <summary>
		/// Проверять выполнение следующих репутаций (может быть несколько) в момент выполнения квеста
		/// Квест не будет выполнен, если эти условия на репы не выполнены (логическое и)
		/// </summary>
		public List<string> CheckReputationsOnComplete;

		/// <summary>
		/// Диапазон лояльности в рамках которой невозможно выполнить (сдать) задание.(логическое И)
		/// </summary>
		public List<CheckLoyaltyStruct> GiveUpConditions;

		/// <summary>
		/// Интервал без учета даты
		/// </summary>
		public List<TimeIntervalStruct> TimeInterval;

		/// <summary>
		/// Интервал с учетом даты
		/// </summary>
		public List<TimeIntervalStruct> DateTimeInterval;

		/// <summary>
		/// Список интервалов с днями недели (ключ - это день недели)
		/// </summary>
		public Dictionary<string, TimeIntervalStruct> WeekDayInterval;

		/// <summary>
		/// Условия получения квеста от НПС
		/// </summary>
		public AcceptConditionsClass AcceptConditions;

		/// <summary>
		/// Условия выполнения данного квеста, далее идет перечисление условий с соответствующими параметрами
		/// </summary>
		public CompleteConditionsClass CompleteConditions;

		/// <summary>
		/// Награда за взятие квеста
		/// </summary>
		public GiveOnAcceptClass GiveOnAccept;

		/// <summary>
		/// Награда за выполнение квеста
		/// </summary>
		public GiveOnAcceptClass Reward;

		/// <summary>
		/// Раварды, которые выкапываются из позиции квестовзятеля (только фунды, может быть несколько)
		/// </summary>
		public SiegeDataBase.ResourseDictionary ExtractRewards;

		/// <summary>
		/// Телепортирование игрока после сдачи квеста
		/// </summary>
		public TeleportStruct Teleport;

		//--------------------------------------------------
		public struct CheckLoyaltyStruct
		{
			public string Name;
			public int MaxValue;
			public int MinValue;
		}

		public struct TeleportStruct
		{
			/// <summary>
			/// Айдишник города, куда телепортировать
			/// </summary>
			public int CityId;

			/// <summary>
			/// Наименование города
			/// </summary>
			public string City;

			/// <summary>
			/// Координаты точки
			/// </summary>
			public string Point;
		}

		public struct TimeIntervalStruct
		{
			/// <summary>
			/// Формат именно такой: ЧЧ:ММ:СС ("ГГГГ-ММ-ДД ЧЧ:ММ:СС")
			/// </summary>
			public string Begin;

			/// <summary>
			/// Длина интервала в часах
			/// </summary>
			public double Hours;
		}

		public class AcceptConditionsClass
		{
			/// <summary>
			/// Необходимо выполнить для получения данного квеста
			/// </summary>
			public List<string> CompleteQuest;

			// необходимо обладать уровнем в пределах
			public int BeginLevel;           
			public int EndLevel;

			/// <summary>
			/// Необходимо чтобы орден игрока обладал контрольной точкой (указывается какой именно)
			/// </summary>
			public string ControlPointOwned;

			/// <summary>
			/// Проверять выполнение следующих репутаций (может быть несколько) в момент выполнения квеста
			/// Квест не будет выполнен, если эти условия на репы не выполнены (логическое и)
			/// </summary>
			public List<string> Reputation;
			public List<string> ReputationGreaterEqual;
			public List<string> ReputationLess;

			/// <summary>
			/// Если вместо 'Reputation' написано ReputationEqual, то будет требоваться точное соответсвие конкретной репы
			/// конкретному ее уровню (если выходит например за рамки Neutrals, то уже не срабатывает), может быть список (логическое и)
			/// </summary>
			public List<string> ReputationEqual;

			/// <summary>
			/// Требуется чтобы списка нижеследующих репутаций не было у игрока вообще
			/// </summary>
			public List<string> RepRequiredNotToBe;

			/// <summary>
			/// Требование титула (Возможные значения: Knight, Master, Baron, Viscount, Earl, Duke, Prince, King)
			/// </summary>
			public string TitleRequired;

			public int MinLoyaltyOfParentHouse;
			public int QuestForBuilderOfParentHouse;
		}

		public class CompleteConditionsClass
		{
			/// <summary>
			/// Условие: поговорить или наличие предмета
			/// </summary>
			public string Condition;

			public SiegeCommonDataDictionary<object> ConditionList;
		}

		public class GiveOnAcceptClass
		{
			/// <summary>
			/// Имя экземпляра класса
			/// </summary>
			public string Name;

			/// <summary>
			/// Экспа
			/// </summary>
			public int Xp;

			/// <summary>
			/// Юнит в формате: название юнита; его уровень прокачки; количество
			/// </summary>
			public List<string> Item;

			/// <summary>
			/// Титул
			/// </summary>
			public string Title;

			/// <summary>
			/// Ресуры
			/// </summary>
			public SiegeDataBase.ResourseDictionary Fund;

			/// <summary>
			/// Репутация
			/// </summary>
			public List<string> Reputation;

			/// <summary>
			/// Установить/сбросить модельный замок
			/// </summary>
			public string SetCastle;

			/// <summary>
			/// Hunger(100% за 7 суток), Trust(100% за 7 суток), Military(100% за 14 суток), Product(не падает за время), Culture(100% за 21 суток для 100 населения) - разные типы лояльностей
			/// </summary>
			public List<string> Loyalty;
		}
	}
}