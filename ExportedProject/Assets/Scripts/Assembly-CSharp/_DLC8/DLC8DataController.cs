using System;
using System.Collections.Generic;
using Aluba;
using AlubaExcelData.Container;
using AlubaExcelData.DataClass;
using Honeti;
using Newtonsoft.Json;
using UnityEngine;
using _DLC8.Common;
using _DLC8.Game.PublicOpinion;
using _DLC8.Main.Data;

namespace _DLC8
{
	public class DLC8DataController : SingletonAutoMono<DLC8DataController>
	{
		private string[] _gameTypeNames = new string[6] { "^110009_common_100", "^110009_common_99", "^110009_common_97", "^110009_common_98", "^110009_common_14", "^110009_common_15" };

		private string[] _levelStrings = new string[5] { "C", "B", "A", "S", "Ω" };

		private ArchiveData _archiveData;

		private BinaryDataManager _binaryDataManager;

		private Dictionary<string, CityMapData> _cityMapDataDic;

		private PublicOpinionInfoManager _publicOpinionInfoDataManager;

		private TalkGroupInfoManager _talkGroupInfoManager;

		private DialogGroupInfoManager _dialogGroupInfoManager;

		private DLC8Controller _controller;

		private List<int> _hotNewsIdList = new List<int>();

		private Dictionary<int, VoiceLevel> _voiceLevelDic;

		private GameManager _gameManager;

		public ArchiveData ArchiveData
		{
			get
			{
				if (_archiveData == null)
				{
					_archiveData = new ArchiveData();
					_archiveData.ResetData();
				}
				return _archiveData;
			}
		}

		public Dictionary<string, CityMapData> CityMapDataDic => _cityMapDataDic;

		public PublicOpinionInfoManager PublicOpinionInfoDataManager => _publicOpinionInfoDataManager;

		public List<int> HotNewsIdList => _hotNewsIdList;

		public BinaryDataManager BinaryDataManager => _binaryDataManager;

		public TalkGroupInfoManager TalkGroupInfoManager => _talkGroupInfoManager;

		public DialogGroupInfoManager DialogGroupInfoManager => _dialogGroupInfoManager;

		public Dictionary<int, VoiceLevel> VoiceLevelDic
		{
			get
			{
				if (_voiceLevelDic == null)
				{
					_voiceLevelDic = _binaryDataManager.GetTable<VoiceLevelContainer>().dataDic;
				}
				return _voiceLevelDic;
			}
		}

		public GameManager GameManager
		{
			get
			{
				if (_gameManager == null)
				{
					_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
				}
				return _gameManager;
			}
		}

		public DLC8Controller Controller
		{
			get
			{
				if (_controller == null)
				{
					_controller = GameObject.Find("DLC8Controller(Clone)").GetComponent<DLC8Controller>();
				}
				return _controller;
			}
			set
			{
				_controller = value;
			}
		}

		private void Awake()
		{
			InitBinaryData();
			foreach (int key in _publicOpinionInfoDataManager.otherData.Keys)
			{
				if (!ArchiveData.AttentionIds.Contains(key))
				{
					_hotNewsIdList.Add(key);
				}
			}
			_cityMapDataDic = _binaryDataManager.GetTable<CityMapDataContainer>().dataDic;
			_voiceLevelDic = _binaryDataManager.GetTable<VoiceLevelContainer>().dataDic;
		}

		public string LevelString(int mapLevel)
		{
			return _levelStrings[mapLevel];
		}

		private void InitBinaryData()
		{
			_binaryDataManager = BinaryDataManager.Instance;
			_binaryDataManager.InitData();
			_publicOpinionInfoDataManager = new PublicOpinionInfoManager();
			_publicOpinionInfoDataManager.Init();
			_dialogGroupInfoManager = new DialogGroupInfoManager();
			_dialogGroupInfoManager.Init();
			_talkGroupInfoManager = new TalkGroupInfoManager();
			_talkGroupInfoManager.Init();
		}

		public void CanShowSetting(bool isCan)
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.canShowSetting = ((!isCan) ? 1 : 0);
		}

		public string GetGameNameKey(CityGameType gameType)
		{
			return _gameTypeNames[(int)gameType];
		}

		public void PlaySound(DLC8SoundType type)
		{
			switch (type)
			{
			case DLC8SoundType.CLICK_BUTTON:
				GameManager.soundManager.PlaySound(20);
				break;
			case DLC8SoundType.CLOSE_DIALOG:
				GameManager.soundManager.PlaySound(8);
				break;
			}
		}

		public void DeleteSaveFile()
		{
			try
			{
				ES3.DeleteFile("LaborerSaveData.es3");
				_archiveData = null;
			}
			catch (Exception)
			{
				_archiveData = null;
			}
		}

		public bool LoadSaveData()
		{
			try
			{
				string text = ES3.Load<string>("laborer", "LaborerSaveData.es3");
				Debug.LogError(text);
				text = text.Replace("\"ClearStagePersonData\":{},", "\"ClearStagePersonData\":0,");
				text = text.Replace("\"Exp\":{},", "\"Exp\":0,");
				Debug.LogError(text);
				_archiveData = JsonConvert.DeserializeObject<ArchiveData>(text);
				_archiveData.ClearInvalidData();
				return true;
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				return false;
			}
		}

		public void SaveData()
		{
			_ = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
			if (_archiveData != null)
			{
				_archiveData.MapPositionX = Controller.mapContentRt.anchoredPosition.x;
				_archiveData.MapPositionY = Controller.mapContentRt.anchoredPosition.y;
				string text = JsonConvert.SerializeObject(_archiveData);
				try
				{
					ES3.Save("laborer", text, "LaborerSaveData.es3");
					Debug.LogError(text);
				}
				catch (Exception ex)
				{
					Debug.Log("存储失败：" + ex.ToString());
				}
			}
		}

		public CityMapData GetDDOSCityMapData()
		{
			int mapLevel = ArchiveData.DdosLevel.MapLevel;
			foreach (KeyValuePair<string, CityMapData> item in CityMapDataDic)
			{
				if (item.Value.level == mapLevel)
				{
					return item.Value;
				}
			}
			return null;
		}

		public string GetDesignationName(int level)
		{
			string[] array = new string[7] { "^110009_common_60", "^110009_common_113", "^110009_common_114", "^110009_common_116", "^110009_common_117", "^110009_common_61", "^110009_common_62" };
			return I18N.instance.getValue(array[level]);
		}

		public string GetRankName(CityGameType gameType)
		{
			return (new string[5] { "Waterpipe", "Virus", "Voiceprint", "BaseStation", "DDOSScore" })[(int)gameType];
		}
	}
}
