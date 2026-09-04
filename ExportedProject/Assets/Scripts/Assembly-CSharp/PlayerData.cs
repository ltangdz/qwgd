using System;
using System.Collections.Generic;
using UnityEngine;
using tnt_deploy;

[Serializable]
public class PlayerData
{
	public float[] resultDayVal;

	public float[] resultHourVal;

	public string nickname = "YAHU";

	private int _eventid = 1;

	[Header("DLC7")]
	public bool isShowNote = true;

	public int[] dlc7Invades = new int[3];

	public string[] toolDLC7 = new string[6] { "0", "1", "2", "10", "12", "15" };

	public int titanStep;

	private List<int> _titanDocumentUnlock = new List<int>();

	public string aiNameDlc7 = "???";

	public List<string> aiSpeakHistoryIds = new List<string>();

	public List<string> aiSpeakGroupIds = new List<string>();

	public List<string> aiWillSpeakGroupIds = new List<string>();

	public List<string> unReadMailIds = new List<string>();

	public List<string> sqlFinishedNames = new List<string>();

	public List<string> sqlCompareNames = new List<string>();

	public List<string> basicNameList = new List<string>();

	public bool showTitanButton;

	private Dictionary<string, bool> _hackerDlc7 = new Dictionary<string, bool>
	{
		{ "ddos", true },
		{ "bomb", true }
	};

	private List<string> _reportShowedList = new List<string>();

	private List<string> _nearlyItemIds = new List<string>();

	[Header("本体及DLC6")]
	public string eventno = "";

	public long startTime;

	public long endTime;

	public long accountTime;

	public bool getMask;

	public bool getSql;

	public int danger;

	public float newsHotVal;

	public Dictionary<string, string> missionlist = new Dictionary<string, string>();

	public string chatLoginID = "1400016";

	[Header("邮箱")]
	public Dictionary<string, List<Dictionary<string, int>>> maillist = new Dictionary<string, List<Dictionary<string, int>>>();

	public Dictionary<string, string> loginedMail = new Dictionary<string, string>();

	public List<string> OpenedMail = new List<string>();

	public List<string> OpenMail = new List<string>();

	public Dictionary<string, Dictionary<string, int>> weizhuang = new Dictionary<string, Dictionary<string, int>>();

	public bool isovertask;

	public int notoversubmitcount = -1;

	public Dictionary<string, int> sendMess = new Dictionary<string, int>();

	public int isCourseOver;

	public int isCourse00;

	public int isCourse01;

	public int isCourse02;

	public int isCourse03;

	public int isCourse04;

	public int isCourse05;

	public int isCourse06;

	public int isCourse07;

	public int isCourse08;

	public int isCourse09;

	public int isCourse10;

	public int isCourse11;

	public int isCourse12;

	public int isCourse13;

	public int isCourse14;

	public int isCourse15;

	public int isCourse16;

	public int isTuli01;

	public int isTuli02;

	public int isTuli03;

	public int isTuli04;

	public int isTuli05;

	public int isTuli06;

	public int isTuli07;

	public int isYulunCourse01;

	public int isYulunCourse04;

	public int isstartgetemailitem;

	public int weizhuangpos;

	public Dictionary<string, bool> delSec = new Dictionary<string, bool>();

	public Dictionary<string, List<string>> camChatInfo = new Dictionary<string, List<string>>();

	public Dictionary<string, List<string>> mainChatInfo = new Dictionary<string, List<string>>();

	public Dictionary<string, int> fishLink = new Dictionary<string, int>();

	public Dictionary<string, List<Vector2>> surveillancelist = new Dictionary<string, List<Vector2>>();

	public List<string> phoneCall = new List<string>();

	public Dictionary<string, List<string>> phoneRecord = new Dictionary<string, List<string>>();

	public Dictionary<string, List<string>> calledStep = new Dictionary<string, List<string>>();

	public List<string> canweizhuangcondition = new List<string>();

	public List<string> videotiplist = new List<string>();

	public List<string> itemlist = new List<string>();

	public bool isDLC;

	private GameTypeEnum _gameType;

	public bool isstarttask;

	public List<string> newsidlist = new List<string>();

	public List<string> loglist = new List<string>();

	public List<string> reasoninglist = new List<string>();

	public Dictionary<string, List<Vector2>> surveillanceRecord = new Dictionary<string, List<Vector2>>();

	public long savetime;

	public bool lookupnews;

	public Dictionary<string, int> camFailedTime = new Dictionary<string, int>();

	public Dictionary<string, string> logRealName = new Dictionary<string, string>();

	public Dictionary<int, string> alllevelinfo = new Dictionary<int, string>();

	public bool islast4;

	public bool ishasciovoice;

	public bool ishastomblancovoice;

	public bool isvoiceopen;

	public bool isenterhoutai;

	public bool isopenreport;

	public bool isopenfolder2;

	public bool isopenfolder3;

	public bool isopenfolder4;

	public bool isCanPlayYulun;

	public bool isYulunGameOver;

	public int twodriveVanType;

	public Dictionary<string, int> cioAnwser = new Dictionary<string, int>();

	public bool isshowhoutaizimu0;

	public bool isshowhoutaizimu1;

	public bool isshowhoutaizimu2;

	public bool isshowhoutaizimu3;

	public bool isshowhoutaizimu4;

	public int isreport1open;

	public int isreport2open;

	public int isreport3open;

	public int isreport4open;

	public int isreport5open;

	public int isreport6open;

	public int isreport7open;

	public int isreport8open;

	public int isreport9open;

	public int isreport10open;

	public int isreport11open;

	public int isreport12open;

	public int isreport13open;

	public int isreport14open;

	public bool isstartselectnored;

	public bool isfixredline;

	public bool isTriggerBoom;

	public float boomLastTime;

	public string[] boomList = new string[5] { "3300007", "3300008", "3300009", "3300010", "3300011" };

	public bool canPlayHideGame;

	public bool completeHideGame;

	public bool zhadanhide;

	public bool isZhadanStart;

	public float zhadantime;

	public List<string> temporaryhopelist = new List<string>();

	public List<int> hopelist = new List<int>();

	public int livebroadingstep;

	public int livebroadingcurrenthopeid = -1;

	public int hopestep = -1;

	public List<int> leftshowspecials = new List<int>();

	public int livebroadinglefttime;

	public bool islivecourse;

	public List<int> leftshowspecials0102 = new List<int>();

	public List<int> leftshowspecials0304 = new List<int>();

	public List<int> leftshowspecials05 = new List<int>();

	public List<string> compeletehopelist = new List<string>();

	public List<string> livebroadinganswerrecords = new List<string>();

	public int livebroadingchatstep;

	public bool islookcio2300087;

	public bool islookcio2300088;

	public bool islookcio2300089;

	public bool isDelVan;

	public int livebroadingfailedcount;

	public bool livesqlbtncourse;

	public bool iszhiboover;

	public int livebroadtotaltime;

	public bool isDecryptInvade;

	public bool isCanCatch;

	public int clickRedBagCount;

	public int iamPoliceWrongCount;

	public int taohua_dlc;

	public List<string> NearlyItemIds
	{
		get
		{
			if (_nearlyItemIds == null)
			{
				_nearlyItemIds = new List<string>();
			}
			return _nearlyItemIds;
		}
	}

	public List<string> ReportShowedList
	{
		get
		{
			if (_reportShowedList == null)
			{
				_reportShowedList = new List<string>();
			}
			return _reportShowedList;
		}
	}

	public List<int> TitanDocumentUnlock
	{
		get
		{
			if (_titanDocumentUnlock == null)
			{
				_titanDocumentUnlock = new List<int>();
			}
			return _titanDocumentUnlock;
		}
	}

	public Dictionary<string, bool> HackerDlc7
	{
		get
		{
			if (_hackerDlc7 == null)
			{
				_hackerDlc7 = new Dictionary<string, bool>
				{
					{ "ddos", true },
					{ "bomb", true }
				};
			}
			return _hackerDlc7;
		}
	}

	public GameTypeEnum GameType
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

	public int Eventid
	{
		get
		{
			return _eventid;
		}
		set
		{
			_eventid = value;
			if (_eventid == 7)
			{
				isDLC = true;
				GameType = GameTypeEnum.DLC6;
			}
			else if (_eventid == 8)
			{
				isDLC = false;
				GameType = GameTypeEnum.DLC7;
			}
			else
			{
				GameType = GameTypeEnum.BASIC;
				isDLC = false;
			}
		}
	}

	private void ResetDLC()
	{
		sqlFinishedNames.Clear();
		showTitanButton = false;
		aiNameDlc7 = "???";
		titanStep = 0;
		_nearlyItemIds = new List<string>();
		_hackerDlc7 = new Dictionary<string, bool>
		{
			{ "ddos", true },
			{ "bomb", true }
		};
		aiSpeakHistoryIds = new List<string>();
		_titanDocumentUnlock = new List<int>();
		dlc7Invades = new int[3];
		toolDLC7 = new string[6] { "0", "1", "2", "10", "12", "15" };
		if (unReadMailIds != null)
		{
			unReadMailIds.Clear();
		}
		unReadMailIds = new List<string> { "1510039", "1510038" };
		aiWillSpeakGroupIds = new List<string>();
		aiSpeakGroupIds = new List<string>();
		basicNameList = new List<string>();
		sqlCompareNames = new List<string>();
		sqlFinishedNames = new List<string>();
		isShowNote = true;
		_reportShowedList = new List<string>();
		_hackerDlc7 = new Dictionary<string, bool>
		{
			{ "ddos", true },
			{ "bomb", true }
		};
	}

	public void ResetLiveBroading(int start, bool isclearjiaocheng = true)
	{
		for (int num = temporaryhopelist.Count - 1; num >= 0; num--)
		{
			if (int.Parse(temporaryhopelist[num]) >= 10530 && int.Parse(temporaryhopelist[num]) <= 10601)
			{
				temporaryhopelist.Remove(temporaryhopelist[num]);
			}
		}
		temporaryhopelist.Clear();
		if (loginedMail.ContainsKey("Er1cam1r1am@uu.com"))
		{
			loginedMail.Remove("Er1cam1r1am@uu.com");
		}
		livesqlbtncourse = false;
		hopelist.Clear();
		livebroadingstep = start;
		livebroadingcurrenthopeid = 0;
		leftshowspecials.Clear();
		for (int i = 0; i < 10; i++)
		{
			leftshowspecials.Add(i);
		}
		livebroadinglefttime = 0;
		livebroadtotaltime = 0;
		hopestep = -1;
		islivecourse = false;
		livebroadinganswerrecords.Clear();
		compeletehopelist.Clear();
		leftshowspecials0102.Clear();
		leftshowspecials0304.Clear();
		leftshowspecials05.Clear();
		string[] array = "0;1".Split(';');
		string[] array2 = "2;4".Split(';');
		string[] array3 = "5;8".Split(';');
		for (int j = 0; j < array.Length; j++)
		{
			leftshowspecials0102.Add(int.Parse(array[j]));
		}
		for (int k = 0; k < array2.Length; k++)
		{
			leftshowspecials0304.Add(int.Parse(array2[k]));
		}
		for (int l = 0; l < array3.Length; l++)
		{
			leftshowspecials05.Add(int.Parse(array3[l]));
		}
		livebroadingchatstep = 0;
		livebroadingfailedcount = 0;
		islivecourse = !isclearjiaocheng;
	}

	public void OpenReport(int id)
	{
		switch (id)
		{
		case 1:
			isreport1open = 1;
			break;
		case 2:
			isreport2open = 1;
			break;
		case 3:
			isreport3open = 1;
			break;
		case 4:
			isreport4open = 1;
			break;
		case 5:
			isreport5open = 1;
			break;
		case 6:
			isreport6open = 1;
			break;
		case 7:
			isreport7open = 1;
			break;
		case 8:
			isreport8open = 1;
			break;
		case 9:
			isreport9open = 1;
			break;
		case 10:
			isreport10open = 1;
			break;
		case 11:
			isreport11open = 1;
			break;
		case 12:
			isreport12open = 1;
			break;
		case 13:
			isreport13open = 1;
			break;
		case 14:
			isreport14open = 1;
			break;
		}
	}

	public bool Isallreportopen()
	{
		if (isreport1open == 1 && isreport2open == 1 && isreport3open == 1 && isreport4open == 1 && isreport5open == 1 && isreport6open == 1 && isreport7open == 1 && isreport8open == 1 && isreport9open == 1 && isreport10open == 1 && isreport11open == 1 && isreport12open == 1 && isreport13open == 1 && isreport14open == 1)
		{
			return true;
		}
		return false;
	}

	public void ClearCourse()
	{
		Eventid = 1;
		SetCourse(0);
		ClearEvent0();
	}

	public void GoToEventID(int eventid, bool isclear = false)
	{
		GameManager component = GameObject.Find("GameManager").GetComponent<GameManager>();
		Eventid = eventid;
		switch (eventid)
		{
		case 7:
			isDLC = true;
			GameType = GameTypeEnum.DLC6;
			component.GameType = GameTypeEnum.DLC6;
			nickname = "Aogesi Will";
			break;
		case 8:
			isDLC = false;
			GameType = GameTypeEnum.DLC7;
			component.GameType = GameTypeEnum.DLC7;
			nickname = "Benjamin Engle";
			break;
		default:
		{
			GameType = GameTypeEnum.BASIC;
			string normalPlayerName = component.saveManager.GetNormalPlayerName();
			component.GameType = GameTypeEnum.BASIC;
			nickname = normalPlayerName;
			isDLC = false;
			component.saveManager.SavePlayerData();
			break;
		}
		}
		SetCourse(1);
		if (isclear)
		{
			ClearEvent0();
		}
	}

	public void CreateNewPlayerData()
	{
		ClearEvent0();
		Eventid = 1;
		lookupnews = false;
		islast4 = false;
		accountTime = 0L;
		alllevelinfo.Clear();
		SetCourse(0);
		cioAnwser.Clear();
		iamPoliceWrongCount = 0;
		clickRedBagCount = 0;
		taohua_dlc = 0;
		ResetDLC();
	}

	public void ClearEvent0()
	{
		ResetDLC();
		clickRedBagCount = 0;
		taohua_dlc = 0;
		iamPoliceWrongCount = 0;
		getMask = false;
		missionlist.Clear();
		itemlist.Clear();
		maillist.Clear();
		loginedMail.Clear();
		loglist.Clear();
		newsidlist.Clear();
		reasoninglist.Clear();
		isovertask = false;
		notoversubmitcount = -1;
		sendMess.Clear();
		isstarttask = false;
		camChatInfo.Clear();
		chatLoginID = "1400016";
		fishLink.Clear();
		phoneCall.Clear();
		phoneRecord.Clear();
		calledStep.Clear();
		delSec.Clear();
		surveillanceRecord.Clear();
		canweizhuangcondition.Clear();
		videotiplist.Clear();
		endTime = 0L;
		logRealName.Clear();
		islast4 = false;
		camFailedTime.Clear();
		isstartgetemailitem = 0;
		weizhuangpos = 0;
		isYulunCourse01 = 0;
		isYulunCourse04 = 0;
		ishasciovoice = false;
		isvoiceopen = false;
		isenterhoutai = false;
		isopenreport = false;
		isopenfolder2 = false;
		isopenfolder3 = false;
		isopenfolder4 = false;
		twodriveVanType = 0;
		isCanPlayYulun = false;
		isYulunGameOver = false;
		ishastomblancovoice = false;
		isshowhoutaizimu0 = false;
		isshowhoutaizimu1 = false;
		isshowhoutaizimu2 = false;
		isshowhoutaizimu3 = false;
		isshowhoutaizimu4 = false;
		isreport1open = 0;
		isreport2open = 0;
		isreport3open = 0;
		isreport4open = 0;
		isreport5open = 0;
		isreport6open = 0;
		isreport7open = 0;
		isreport8open = 0;
		isreport9open = 0;
		isreport10open = 0;
		isreport11open = 0;
		isreport12open = 0;
		isreport13open = 0;
		isreport14open = 0;
		isstartselectnored = false;
		isfixredline = false;
		isTriggerBoom = false;
		boomLastTime = 0f;
		boomList = new string[5] { "3300007", "3300008", "3300009", "3300010", "3300011" };
		canPlayHideGame = false;
		OpenedMail.Clear();
		OpenMail.Clear();
		zhadanhide = false;
		isZhadanStart = false;
		zhadantime = 0f;
		isDelVan = false;
		ResetLiveBroading(0, isclearjiaocheng: false);
		iszhiboover = false;
		islookcio2300087 = false;
		islookcio2300088 = false;
		islookcio2300089 = false;
		isDecryptInvade = false;
		isCanCatch = false;
	}

	public void SetCourse(int status)
	{
		isCourseOver = status;
		isCourse00 = status;
		isCourse01 = status;
		isCourse02 = status;
		isCourse03 = status;
		isCourse04 = status;
		isCourse05 = status;
		isCourse06 = status;
		isCourse07 = status;
		isCourse08 = status;
		isCourse09 = status;
		isCourse10 = status;
		isCourse11 = status;
		isCourse12 = status;
		isCourse13 = status;
		isCourse14 = status;
		isCourse15 = status;
		isTuli01 = status;
		isTuli02 = status;
		isTuli03 = status;
		isTuli04 = status;
		isTuli05 = status;
		isTuli06 = status;
		isTuli07 = status;
	}

	public bool ContainItemList(string[] itemids)
	{
		bool result = true;
		for (int i = 0; i < itemids.Length; i++)
		{
			if (!itemlist.Contains(itemids[i]))
			{
				return false;
			}
		}
		return result;
	}

	public void AddNewsHotVal(float hotVal)
	{
		newsHotVal += hotVal;
	}

	public void UseSocialMethod(int methodID)
	{
	}

	public List<string> MailNamelist()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, string> item in loginedMail)
		{
			list.Add(item.Key);
		}
		return list;
	}

	public List<string> MailKeylist(string name)
	{
		List<string> list = new List<string>();
		if (maillist.ContainsKey(name))
		{
			foreach (KeyValuePair<string, int> item in maillist[name][0])
			{
				list.Add(item.Key);
			}
		}
		else
		{
			List<Dictionary<string, int>> list2 = new List<Dictionary<string, int>>();
			list2.Add(new Dictionary<string, int>());
			maillist.Add(name, list2);
		}
		return list;
	}

	public int NoReadMail(string name)
	{
		int num = 0;
		if (maillist.ContainsKey(name) && maillist[name].Count > 0)
		{
			foreach (KeyValuePair<string, int> item in maillist[name][0])
			{
				if (item.Value == 0)
				{
					num++;
				}
			}
		}
		return num;
	}

	public bool MailReadType(string name, string mailid)
	{
		bool result = false;
		if (maillist[name][0][mailid] == 1)
		{
			result = true;
		}
		return result;
	}

	public void AddGameTime(long time)
	{
		endTime += time;
		accountTime += time;
	}

	public void AddHaveLogedMail(string mailName, string pw)
	{
		if (!loginedMail.ContainsKey(mailName))
		{
			loginedMail.Add(mailName, pw);
		}
	}

	public void InitMissionList(List<DATA20> lists)
	{
		for (int i = 0; i < lists.Count; i++)
		{
			if (!missionlist.ContainsKey(lists[i].ID.ToString()))
			{
				missionlist.Add(lists[i].ID.ToString(), "0");
			}
		}
	}

	public string GetMissionItemStatus(string misid)
	{
		return missionlist[misid];
	}

	public void CompleteMissionItem(string misid)
	{
		if (missionlist.ContainsKey(misid))
		{
			missionlist[misid] = "1";
		}
	}

	public void AddCamChatInfo(string userID, List<string> chatID)
	{
		if (!camChatInfo.ContainsKey(userID))
		{
			camChatInfo.Add(userID, chatID);
		}
		else
		{
			camChatInfo[userID] = chatID;
		}
	}

	public void AddMainChatInfo(string userID, string chatID)
	{
		if (!mainChatInfo.ContainsKey(userID))
		{
			List<string> list = new List<string>();
			list.Add(chatID);
			mainChatInfo.Add(userID, list);
		}
		else if (!mainChatInfo[userID].Contains(chatID))
		{
			mainChatInfo[userID].Add(chatID);
		}
	}

	public void SetDlcNickname()
	{
		if (GameType == GameTypeEnum.DLC6)
		{
			nickname = "Aogesi Will";
		}
		else if (GameType == GameTypeEnum.DLC7)
		{
			nickname = "Benjamin Engle";
		}
	}
}
