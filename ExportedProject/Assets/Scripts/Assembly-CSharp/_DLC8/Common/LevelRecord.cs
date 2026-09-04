using System.Collections.Generic;
using Aluba;
using AlubaExcelData.DataClass;
using CodeStage.AntiCheat.ObscuredTypes;
using Honeti;
using UnityEngine;

namespace _DLC8.Common
{
	public class LevelRecord
	{
		public string area;

		public bool isUnlock;

		public ObscuredInt _mapLevel;

		public ObscuredInt _level;

		private ObscuredInt _firstScore;

		private ObscuredInt _bestScore;

		private CityGameType _gameType;

		public List<int> _scoreHistory;

		public string Area
		{
			get
			{
				return area;
			}
			set
			{
				area = value;
			}
		}

		public bool IsUnlock
		{
			get
			{
				return isUnlock;
			}
			set
			{
				isUnlock = value;
			}
		}

		public int MapLevel
		{
			get
			{
				return _mapLevel;
			}
			set
			{
				_mapLevel = value;
			}
		}

		public int Level
		{
			get
			{
				return _level;
			}
			set
			{
				_level = value;
			}
		}

		public int FirstScore
		{
			get
			{
				return _firstScore;
			}
			set
			{
				_firstScore = value;
			}
		}

		public int BestScore
		{
			get
			{
				return _bestScore;
			}
			set
			{
				_bestScore = value;
			}
		}

		public CityGameType GameType
		{
			get
			{
				return _gameType;
			}
			set
			{
				_gameType = value;
			}
		}

		public List<int> ScoreHistory
		{
			get
			{
				return _scoreHistory;
			}
			set
			{
				_scoreHistory = value;
			}
		}

		public string GetI18NName()
		{
			Dictionary<string, CityMapData> cityMapDataDic = SingletonAutoMono<DLC8DataController>.GetInstance().CityMapDataDic;
			return I18N.instance.getValue(cityMapDataDic[area].name);
		}

		public string GetTimeScoreString(bool isBestScore)
		{
			int num = (isBestScore ? BestScore : FirstScore);
			int num2 = num / 60;
			int num3 = num % 60;
			return $"{num2.ToString().PadLeft(2, '0')}'{num3.ToString().PadLeft(2, '0')}\"";
		}

		public static LevelRecord CreateNewData(string area, int level, int mapLevel, CityGameType gameType)
		{
			return new LevelRecord
			{
				area = area,
				Level = level,
				MapLevel = mapLevel,
				GameType = gameType,
				FirstScore = -1,
				BestScore = -1,
				ScoreHistory = new List<int>()
			};
		}

		public int FinishedLevel(int score)
		{
			ArchiveData archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			bool flag = FirstScore == -1;
			int num = Random.Range(flag ? 10 : 5, flag ? 60 : 10);
			archiveData.AddPersonData(num);
			if (archiveData.StageClearTimestamp > 0)
			{
				archiveData.PersonClearStageData += num;
			}
			if (flag && GameType != CityGameType.DDOS)
			{
				DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.FIRST_FINISH_GAME, 0);
			}
			if (FirstScore == -1)
			{
				FirstScore = score;
			}
			if (GameType < CityGameType.DDOS)
			{
				if (BestScore <= 0 || BestScore > score)
				{
					BestScore = score;
				}
			}
			else if (GameType == CityGameType.DDOS && (BestScore <= 0 || BestScore < score))
			{
				BestScore = score;
			}
			return num;
		}
	}
}
