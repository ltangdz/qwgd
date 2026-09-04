using System;
using System.Collections.Generic;
using System.Linq;
using AlubaExcelData.Container;
using AlubaExcelData.DataClass;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using _DLC8.Game.PublicOpinion;
using _DLC8.Main;
using _DLC8.Main.Data;

namespace _DLC8.Common
{
	[Serializable]
	public class ArchiveData
	{
		private string _nickName;

		private LevelRecord _ddosLevel;

		private int _ddosCurLevel;

		private List<LevelRecord> _voiceLevel = new List<LevelRecord>();

		private List<LevelRecord> _waterPipeLevel = new List<LevelRecord>();

		private List<LevelRecord> _baseStationLevel = new List<LevelRecord>();

		private List<LevelRecord> _virusLevel = new List<LevelRecord>();

		private List<bool> _unlockAppList;

		private List<string> _unlockedMapList = new List<string>();

		private List<bool> _unlockedDDOSTeachList = new List<bool> { false, false, false };

		private long _stageClearTimestamp;

		private int _stageClearTime;

		private ObscuredInt _curMapLevel;

		private ObscuredLong _totalData;

		private ObscuredLong _personData;

		private ObscuredLong _clearStagePersonData;

		private ObscuredLong _personClearStageData;

		private string _idNumber;

		private int _positionLevel;

		private ObscuredInt _min;

		private ObscuredLong _resourceCount;

		private bool _hasPlayedEndMovie;

		private List<int> _attentionIds = new List<int>();

		[SerializeField]
		private List<TalkContentInfo> _talkContentInfos = new List<TalkContentInfo>();

		private List<int> _chatIdList = new List<int>();

		private List<int> _dialogIdList = new List<int>();

		private ObscuredInt _exp = 0;

		private Dictionary<string, PublicOpinionInitData> _publicOpinionMapDataDic;

		private List<PublicOpinionNewsTitleInfo> _newsTitleList;

		private float _mapPositionX;

		private float _mapPositionY;

		public bool[] danielEmailFinishedList = new bool[2];

		public bool isShowTitle;

		public TeachDialogStepType teachStep;

		public bool isFinishedWarningTeach;

		public bool isFinishedRankTeach;

		public int[] lvProgress = new int[4] { 1044, 2168, 3372, 5136 };

		public long PersonData
		{
			get
			{
				return _personData;
			}
			set
			{
				_personData = value;
			}
		}

		public string NickName
		{
			get
			{
				return _nickName;
			}
			set
			{
				_nickName = value;
			}
		}

		public LevelRecord DdosLevel
		{
			get
			{
				return _ddosLevel;
			}
			set
			{
				_ddosLevel = value;
			}
		}

		public List<LevelRecord> VoiceLevel
		{
			get
			{
				return _voiceLevel;
			}
			set
			{
				_voiceLevel = value;
			}
		}

		public List<LevelRecord> WaterPipeLevel
		{
			get
			{
				return _waterPipeLevel;
			}
			set
			{
				_waterPipeLevel = value;
			}
		}

		public List<LevelRecord> BaseStationLevel
		{
			get
			{
				return _baseStationLevel;
			}
			set
			{
				_baseStationLevel = value;
			}
		}

		public List<LevelRecord> VirusLevel
		{
			get
			{
				return _virusLevel;
			}
			set
			{
				_virusLevel = value;
			}
		}

		public List<bool> UnlockAppList
		{
			get
			{
				return _unlockAppList;
			}
			set
			{
				_unlockAppList = value;
			}
		}

		public List<string> UnlockedMapList
		{
			get
			{
				return _unlockedMapList;
			}
			set
			{
				_unlockedMapList = value;
			}
		}

		public int CurMapLevel
		{
			get
			{
				return _curMapLevel;
			}
			set
			{
				_curMapLevel = value;
			}
		}

		public long TotalData
		{
			get
			{
				return _totalData;
			}
			set
			{
				_totalData = value;
			}
		}

		public string IDNumber
		{
			get
			{
				return _idNumber;
			}
			set
			{
				_idNumber = value;
			}
		}

		public int MIN
		{
			get
			{
				return _min;
			}
			set
			{
				_min = value;
			}
		}

		public long ResourceCount
		{
			get
			{
				return _resourceCount;
			}
			set
			{
				_resourceCount = value;
			}
		}

		public List<int> AttentionIds
		{
			get
			{
				return _attentionIds;
			}
			set
			{
				_attentionIds = value;
			}
		}

		public List<TalkContentInfo> TalkContentInfos
		{
			get
			{
				if (_talkContentInfos == null)
				{
					_talkContentInfos = new List<TalkContentInfo>();
				}
				return _talkContentInfos;
			}
			set
			{
				_talkContentInfos = value;
			}
		}

		public Dictionary<string, PublicOpinionInitData> PublicOpinionMapDataDic
		{
			get
			{
				return _publicOpinionMapDataDic;
			}
			set
			{
				_publicOpinionMapDataDic = value;
			}
		}

		public float MapPositionX
		{
			get
			{
				return _mapPositionX;
			}
			set
			{
				_mapPositionX = value;
			}
		}

		public float MapPositionY
		{
			get
			{
				return _mapPositionY;
			}
			set
			{
				_mapPositionY = value;
			}
		}

		public int DdosCurLevel
		{
			get
			{
				return _ddosCurLevel;
			}
			set
			{
				_ddosCurLevel = value;
			}
		}

		public List<int> ChatIdList
		{
			get
			{
				return _chatIdList ?? (_chatIdList = new List<int>());
			}
			set
			{
				_chatIdList = value;
			}
		}

		public List<int> DialogIdList
		{
			get
			{
				return _dialogIdList ?? (_dialogIdList = new List<int>());
			}
			set
			{
				_dialogIdList = value;
			}
		}

		public long StageClearTimestamp
		{
			get
			{
				return _stageClearTimestamp;
			}
			set
			{
				_stageClearTimestamp = value;
			}
		}

		public int PositionLevel
		{
			get
			{
				return _positionLevel;
			}
			set
			{
				_positionLevel = value;
			}
		}

		public long PersonClearStageData
		{
			get
			{
				return _personClearStageData;
			}
			set
			{
				_personClearStageData = value;
			}
		}

		public List<PublicOpinionNewsTitleInfo> NewsTitleList
		{
			get
			{
				if (_newsTitleList == null)
				{
					_newsTitleList = new List<PublicOpinionNewsTitleInfo>();
				}
				return _newsTitleList;
			}
			set
			{
				_newsTitleList = value;
			}
		}

		public TeachDialogStepType TeachStep
		{
			get
			{
				return teachStep;
			}
			set
			{
				teachStep = value;
			}
		}

		public int StageClearTime
		{
			get
			{
				return _stageClearTime;
			}
			set
			{
				_stageClearTime = value;
			}
		}

		public long ClearStagePersonData
		{
			get
			{
				return _clearStagePersonData;
			}
			set
			{
				_clearStagePersonData = value;
			}
		}

		public int Exp
		{
			get
			{
				return _exp;
			}
			set
			{
				_exp = value;
			}
		}

		public bool HasPlayedEndMovie
		{
			get
			{
				return _hasPlayedEndMovie;
			}
			set
			{
				_hasPlayedEndMovie = value;
			}
		}

		public List<bool> UnlockedDdosTeachList
		{
			get
			{
				if (_unlockedDDOSTeachList == null || _unlockedDDOSTeachList.Count == 0)
				{
					_unlockedDDOSTeachList = new List<bool> { false, false, false };
				}
				return _unlockedDDOSTeachList;
			}
			set
			{
				_unlockedDDOSTeachList = value;
			}
		}

		public StageClearType GetStageClearState()
		{
			if (StageClearTime <= 0)
			{
				return StageClearType.NONE;
			}
			if (StageClearTime <= 288)
			{
				return StageClearType.PERFECT;
			}
			return StageClearType.NORMAL;
		}

		public string[] GetAreaKeys()
		{
			return new string[5] { "Wilton", "Victor", "RomanWell", "Lester", "Pullman" };
		}

		public int GetMapProgress(string mapName)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < _voiceLevel.Count; i++)
			{
				LevelRecord levelRecord = _voiceLevel[i];
				if (levelRecord.area == mapName)
				{
					num++;
					if (levelRecord.BestScore > 0)
					{
						num2++;
					}
				}
			}
			for (int j = 0; j < _waterPipeLevel.Count; j++)
			{
				LevelRecord levelRecord2 = _waterPipeLevel[j];
				if (levelRecord2.area == mapName)
				{
					num++;
					if (levelRecord2.BestScore > 0)
					{
						num2++;
					}
				}
			}
			for (int k = 0; k < _baseStationLevel.Count; k++)
			{
				LevelRecord levelRecord3 = _baseStationLevel[k];
				if (levelRecord3.area == mapName)
				{
					num++;
					if (levelRecord3.BestScore > 0)
					{
						num2++;
					}
				}
			}
			for (int l = 0; l < _virusLevel.Count; l++)
			{
				LevelRecord levelRecord4 = _virusLevel[l];
				if (levelRecord4.area == mapName)
				{
					num++;
					if (levelRecord4.BestScore > 0)
					{
						num2++;
					}
				}
			}
			return Mathf.FloorToInt((float)num2 * 100f / (float)num);
		}

		public void UnlockApp(CityGameType type)
		{
			_unlockAppList[(int)type] = true;
		}

		public void UnlockMap(string name)
		{
			if (GetAreaKeys().Contains(name) && !_unlockedMapList.Contains(name))
			{
				_unlockedMapList.Add(name);
				int num = GetAreaKeys().ToList().IndexOf(name);
				if (num >= _ddosLevel.MapLevel)
				{
					_ddosLevel.MapLevel = num;
				}
			}
		}

		public LaborerMapEnum GetLaborerMapEnum(string name)
		{
			string[] areaKeys = GetAreaKeys();
			for (int i = 0; i < areaKeys.Length; i++)
			{
				if (areaKeys[i] == name)
				{
					return (LaborerMapEnum)i;
				}
			}
			return LaborerMapEnum.Wilton;
		}

		public bool IsUnlockMap(string mapName)
		{
			return _unlockedMapList.Contains(mapName);
		}

		public int DDOSMaxMapIndex()
		{
			return _unlockedMapList.Count - 1;
		}

		public LevelRecord GetNewestLevelRecord(LaborerMapEnum mapEnum, CityGameType gameType)
		{
			List<LevelRecord> levelRecordList = GetLevelRecordList(gameType, (int)mapEnum);
			for (int i = 0; i < levelRecordList.Count; i++)
			{
				LevelRecord levelRecord = levelRecordList[i];
				levelRecord.GameType = gameType;
				if (levelRecord.MapLevel == (int)mapEnum && levelRecord.FirstScore == -1)
				{
					return levelRecord;
				}
				if (i == levelRecordList.Count - 1)
				{
					return levelRecord;
				}
			}
			return null;
		}

		public List<LevelRecord> GetLevelRecordListByCityGameType(CityGameType gameType)
		{
			List<LevelRecord> result = new List<LevelRecord>();
			switch (gameType)
			{
			case CityGameType.VIRUS:
				result = _virusLevel;
				break;
			case CityGameType.VOICE:
				result = _voiceLevel;
				break;
			case CityGameType.WATER_PIPE:
				result = _waterPipeLevel;
				break;
			case CityGameType.BASE_STATION:
				result = _baseStationLevel;
				break;
			}
			return result;
		}

		public List<LevelRecord> GetLevelRecordList(CityGameType gameType, int mapLevel)
		{
			List<LevelRecord> list = new List<LevelRecord>();
			switch (gameType)
			{
			case CityGameType.VIRUS:
				list = _virusLevel;
				break;
			case CityGameType.VOICE:
				list = _voiceLevel;
				break;
			case CityGameType.WATER_PIPE:
				list = _waterPipeLevel;
				break;
			case CityGameType.BASE_STATION:
				list = _baseStationLevel;
				break;
			}
			List<LevelRecord> list2 = new List<LevelRecord>();
			for (int i = 0; i < list.Count; i++)
			{
				LevelRecord levelRecord = list[i];
				if (levelRecord.MapLevel == mapLevel)
				{
					list2.Add(levelRecord);
				}
			}
			return list2;
		}

		public void ResetData()
		{
			InitLevelData();
			_hasPlayedEndMovie = false;
			danielEmailFinishedList = new bool[2];
			isFinishedRankTeach = false;
			isFinishedWarningTeach = false;
			_unlockedDDOSTeachList = new List<bool> { false, false, false };
			isFinishedWarningTeach = false;
			_ddosCurLevel = 0;
			_stageClearTime = 0;
			_clearStagePersonData = 0L;
			isShowTitle = false;
			_positionLevel = 0;
			teachStep = TeachDialogStepType.UNSTART;
			_mapPositionX = -492.05f;
			_mapPositionY = 480.7813f;
			_ddosLevel = LevelRecord.CreateNewData("", 0, 0, CityGameType.DDOS);
			_attentionIds.Clear();
			_talkContentInfos.Clear();
			_unlockedMapList.Clear();
			_unlockedMapList.Add("Wilton");
			_unlockAppList = new List<bool> { false, false, false, false, false, false };
			_nickName = "";
			_curMapLevel = 0;
			_totalData = 0L;
			_personData = 0L;
			_idNumber = "";
			_resourceCount = 50L;
			_min = 0;
			_exp = 0;
			PublicOpinionInitDataContainer table = BinaryDataManager.Instance.GetTable<PublicOpinionInitDataContainer>();
			_publicOpinionMapDataDic = table.dataDic;
		}

		public void AddMin()
		{
			_min = (int)_min + 1;
		}

		public float NegativeProgress()
		{
			long num = 0L;
			long num2 = 0L;
			foreach (PublicOpinionInitData value in PublicOpinionMapDataDic.Values)
			{
				num += value.total;
				num2 += value.negative;
			}
			return (float)num2 * 1f / (float)num;
		}

		public void ClearInvalidData()
		{
			if (_baseStationLevel == null)
			{
				return;
			}
			List<LevelRecord> list = new List<LevelRecord>();
			for (int i = 0; i < _baseStationLevel.Count; i++)
			{
				LevelRecord levelRecord = _baseStationLevel[i];
				if (levelRecord.Level < 5)
				{
					list.Add(levelRecord);
				}
			}
			_baseStationLevel = list;
		}

		public void ChangePublicOpinionData()
		{
			for (int i = 0; i < _publicOpinionMapDataDic.Keys.Count; i++)
			{
				float tPositive = UnityEngine.Random.Range(2.6f, 4f) / 100f;
				float tNegative = UnityEngine.Random.Range(1f, 2.6f) / 100f;
				string key = _publicOpinionMapDataDic.Keys.ElementAt(i);
				PublicOpinionInitData data = _publicOpinionMapDataDic[key];
				_publicOpinionMapDataDic[key] = ChangePublicOpinionInitData(data, tPositive, tNegative);
			}
		}

		public PublicOpinionInitData ChangePublicOpinionInitData(PublicOpinionInitData data, float tPositive, float tNegative)
		{
			Debug.Log("dfdf:" + (float)data.negative * 1f / (float)data.total);
			int a = Mathf.CeilToInt((float)data.total * 0.8f);
			int b = Mathf.CeilToInt((float)data.total * tPositive);
			int num = Mathf.CeilToInt((float)data.total * tNegative);
			int num2 = Mathf.Min(data.positive, b);
			data.positive -= num2;
			int b2 = data.negative + num;
			int negative = Mathf.Min(a, b2);
			data.negative = negative;
			Debug.Log("dfdf:" + (float)data.negative * 1f / (float)data.total);
			Debug.Log("=====================================");
			return data;
		}

		private void InitLevelData()
		{
			_voiceLevel.Clear();
			_waterPipeLevel.Clear();
			_baseStationLevel.Clear();
			_virusLevel.Clear();
			InitNewsTitleList();
			string[] areaKeys = GetAreaKeys();
			for (int i = 0; i < areaKeys.Length; i++)
			{
				string areaKey = areaKeys[i];
				for (int j = 0; j < 8; j++)
				{
					if (j < 2)
					{
						LevelRecord item = InitLevelRecord(areaKey, j, i);
						_voiceLevel.Add(item);
					}
					if (j < 5)
					{
						LevelRecord item2 = InitLevelRecord(areaKey, j, i);
						_virusLevel.Add(item2);
						LevelRecord item3 = InitLevelRecord(areaKey, j, i);
						_baseStationLevel.Add(item3);
					}
					LevelRecord item4 = InitLevelRecord(areaKey, j, i);
					_waterPipeLevel.Add(item4);
				}
			}
		}

		private void InitNewsTitleList()
		{
			List<string> list = new List<string>();
			for (int i = 0; i < 9; i++)
			{
				list.Add($"^110009_TitanNews_{4 + i}");
			}
			NewsTitleList.Add(PublicOpinionNewsTitleInfo.Init(1, "^110009_TitanNews_1", 1));
			NewsTitleList.Add(PublicOpinionNewsTitleInfo.Init(2, "^110009_TitanNews_3", 3));
			NewsTitleList.Add(PublicOpinionNewsTitleInfo.Init(12, "^110009_TitanNews_2", 2));
			int num = 11;
			while (list.Count > 0)
			{
				string text = list[UnityEngine.Random.Range(0, list.Count)];
				list.Remove(text);
				NewsTitleList.Insert(2, PublicOpinionNewsTitleInfo.Init(num, text, 0));
				num--;
			}
			NewsTitleList = AlubaTools.Swap(NewsTitleList, 11, 5);
			NewsTitleList[5].rank = 6;
			NewsTitleList[11].rank = 12;
			Debug.LogError("InitNewsTitleList:" + _newsTitleList.Count);
		}

		private LevelRecord InitLevelRecord(string areaKey, int level, int mapLevel)
		{
			return new LevelRecord
			{
				ScoreHistory = new List<int>(),
				area = areaKey,
				Level = level,
				MapLevel = mapLevel,
				BestScore = -1,
				FirstScore = -1
			};
		}

		public bool ChangeResourceCount(ObscuredLong bugCount)
		{
			long num = (long)_resourceCount + (long)bugCount;
			_resourceCount = num;
			return true;
		}

		public void AddPersonData(long changeData)
		{
			_personData = (long)_personData + changeData;
			Debug.LogError("changeData:" + changeData);
			if (StageClearTimestamp > 0)
			{
				_clearStagePersonData = (long)_clearStagePersonData + changeData;
				if ((long)_clearStagePersonData >= 4000)
				{
					DLC8EventManager.Instance.NoticeSpecialEvent(DLC8SpecialEvent.STAGE_CLEAR_4000);
				}
			}
		}
	}
}
