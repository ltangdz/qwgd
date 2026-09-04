using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aluba;
using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using Honeti;
using Steamworks;
using Steamworks.NET;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using _DLC8.Card;
using _DLC8.Common;
using _DLC8.Game.DDOS;
using _DLC8.Game.PublicOpinion;
using _DLC8.Main;
using _DLC8.Main.Data;
using _DLC8.Main.Invade;
using _DLC8.Main.Rank;

namespace _DLC8
{
	public class DLC8Controller : MonoBehaviour
	{
		public Image content;

		public RectTransform mapContentRt;

		public Text timeText;

		public InvadeController invadeControllerPrefab;

		public LaborerGameContent gameContentPrefab;

		public LaborerRankLevelController rankLevelControllerPrefab;

		public PublicOpinionController publicOptionPrefab;

		public DDOSGameControllerDLC8 ddosPrefab;

		public List<CityMap> mapList;

		[Header("地图功能")]
		public MainMapAttentionGroup attentionGroup;

		public MainMapResourceGroup resourceGroup;

		public RectTransform codeRunRT;

		public MainMapDataGroup dataGroup;

		public MainMapUserInfoGroup userInfoGroup;

		public TitanTalkGroup talkGroup;

		public TitanTalkGroup forceTalkGroup;

		public MainMapWarningGroup warningGroup;

		public AppGroup appGroup;

		public VideoTipDlC8 videoTipDlc8;

		public TeachDialog teachDialog;

		public PrintCanvasDLC8 printCanvasDlc8;

		public Button stageClearButton;

		public WorkCardGroup workCardGroup;

		public CanvasGroup tipCanvasGroup;

		public Text outTipText;

		public EmployeeBook employeeBook;

		public GameObject whileTrueGroup;

		public Image whileTrueImage;

		public Image maskImage;

		public GameObject overWindow;

		public Button goMainButton;

		public Button continueButton;

		private LaborerGameContent _gameContent;

		private LaborerRankLevelController _rankLevelController;

		private InvadeController _invadeServerController;

		private GameManager _gameManager;

		private bool _isShowWarning;

		private ArchiveData _archiveData;

		private DLC8EventManager _eventManager;

		private SteamStats _steamStats = new SteamStats();

		private bool _isShowNoMoneyTip;

		private bool _isShowVideo;

		private int[] _forceChatIds = new int[7] { 2310096, 2310106, 2310107, 2310108, 2310109, 2310110, 2310111 };

		private CallResult<GlobalStatsReceived_t> m_statsReceived = new CallResult<GlobalStatsReceived_t>();

		private GameManager GameManager
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

		public DLC8EventManager EventManager
		{
			get
			{
				if (_eventManager == null)
				{
					_eventManager = DLC8EventManager.Instance;
				}
				return _eventManager;
			}
		}

		public ArchiveData ArchiveData
		{
			get
			{
				if (_archiveData == null)
				{
					_archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
				}
				return _archiveData;
			}
		}

		private void AllHide(bool isAnimation)
		{
			float duration = (isAnimation ? 0.38f : 0f);
			codeRunRT.DOAnchorPosX(-400f, duration);
			appGroup.GetComponent<RectTransform>().DOAnchorPosY(-100f, duration);
			resourceGroup.GetComponent<RectTransform>().DOAnchorPosX(-400f, duration);
			attentionGroup.GetComponent<RectTransform>().DOAnchorPosX(-400f, duration);
			dataGroup.GetComponent<RectTransform>().DOAnchorPosX(-400f, duration);
			userInfoGroup.GetComponent<RectTransform>().DOAnchorPosX(400f, duration);
			talkGroup.GetComponent<RectTransform>().DOAnchorPosX(400f, duration);
		}

		private void AllShow(bool isAnimation)
		{
			float duration = (isAnimation ? 0.38f : 0f);
			codeRunRT.DOAnchorPosX(24.5f, duration);
			appGroup.GetComponent<RectTransform>().DOAnchorPosY(19f, duration);
			resourceGroup.GetComponent<RectTransform>().DOAnchorPosX(24.5f, duration);
			attentionGroup.GetComponent<RectTransform>().DOAnchorPosX(24.5f, duration);
			dataGroup.GetComponent<RectTransform>().DOAnchorPosX(24.5f, duration);
			userInfoGroup.GetComponent<RectTransform>().DOAnchorPosX(-26f, duration);
			talkGroup.GetComponent<RectTransform>().DOAnchorPosX(-26f, duration);
		}

		private void NoticeControllerGameOver(LevelRecord obj)
		{
			if (_archiveData.teachStep == TeachDialogStepType.UNLOCK_LEVEL_FINISH)
			{
				teachDialog.Show(TeachDialogStepType.GAME_SUCCESS, dataGroup.gameObject);
			}
			for (int i = 0; i < appGroup.AppItemList.Length; i++)
			{
				appGroup.AppItemList[i].Refresh(isWarning: false);
			}
			Debug.Log("NoticeControllerGameOver");
			StartCoroutine("Check");
		}

		private void CheckLevelRecord()
		{
			int num = 0;
			List<int> list = new List<int> { 0, 0, 0, 0, 0 };
			int num2 = 0;
			for (int i = 0; i < _archiveData.VoiceLevel.Count; i++)
			{
				LevelRecord levelRecord = _archiveData.VoiceLevel[i];
				if (levelRecord.BestScore > 0)
				{
					num2++;
					list[levelRecord.MapLevel] = list[levelRecord.MapLevel] + 1;
					num++;
				}
			}
			if (num2 == _archiveData.VoiceLevel.Count)
			{
				EventManager.NoticeSpecialEvent(DLC8SpecialEvent.VOICE_PRINT_STAGE_CLEAR);
			}
			num2 = 0;
			for (int j = 0; j < _archiveData.BaseStationLevel.Count; j++)
			{
				LevelRecord levelRecord2 = _archiveData.BaseStationLevel[j];
				if (levelRecord2.BestScore > 0)
				{
					num2++;
					list[levelRecord2.MapLevel] = list[levelRecord2.MapLevel] + 1;
					num++;
				}
			}
			if (num2 == _archiveData.BaseStationLevel.Count)
			{
				EventManager.NoticeSpecialEvent(DLC8SpecialEvent.BASE_STATION_STAGE_CLEAR);
			}
			num2 = 0;
			for (int k = 0; k < _archiveData.WaterPipeLevel.Count; k++)
			{
				LevelRecord levelRecord3 = _archiveData.WaterPipeLevel[k];
				if (levelRecord3.BestScore > 0)
				{
					num2++;
					list[levelRecord3.MapLevel] = list[levelRecord3.MapLevel] + 1;
					num++;
				}
			}
			if (num2 == _archiveData.WaterPipeLevel.Count)
			{
				EventManager.NoticeSpecialEvent(DLC8SpecialEvent.WATER_PIPE_STAGE_CLEAR);
			}
			num2 = 0;
			for (int l = 0; l < _archiveData.VirusLevel.Count; l++)
			{
				LevelRecord levelRecord4 = _archiveData.VirusLevel[l];
				if (levelRecord4.BestScore > 0)
				{
					num2++;
					list[levelRecord4.MapLevel] = list[levelRecord4.MapLevel] + 1;
					num++;
				}
			}
			if (num2 == _archiveData.VirusLevel.Count)
			{
				EventManager.NoticeSpecialEvent(DLC8SpecialEvent.VIRUS_STAGE_CLEAR);
			}
			int[] array = new int[8] { 2310097, 2310098, 2310099, 23100100, 2310101, 2310103, 2310104, 2310105 };
			int[] array2 = new int[8] { 20, 30, 40, 50, 60, 70, 80, 90 };
			for (int m = 0; m < array2.Length; m++)
			{
				if (array2[m] == num)
				{
					EventManager.NoticeCommonEvent(DLC8CommonEvent.SHOW_CHAT, array[m]);
				}
			}
			if (num == 100 && !ArchiveData.DialogIdList.Contains(3910031))
			{
				ArchiveData.StageClearTimestamp = AlubaUtils.TimeStampSeconds();
				EventManager.NoticeSpecialEvent(DLC8SpecialEvent.STAGE_CLEAR);
			}
		}

		private void UpdateWorkDurationText()
		{
			ObscuredInt obscuredInt = ArchiveData.MIN;
			string text = ((int)obscuredInt / 60).ToString().PadLeft(2, '0');
			string text2 = ((int)obscuredInt % 60).ToString().PadLeft(2, '0');
			timeText.text = string.Format("{0}:{1}{2}{3}{4}", I18N.instance.getValue("^110009_common_36"), text, I18N.instance.getValue("^110009_common_37"), text2, I18N.instance.getValue("^110009_common_38"));
		}

		private void UpdateWorkDuration()
		{
			ArchiveData.AddMin();
			try
			{
				_steamStats.Init("stat_data_count", delegate(long data)
				{
					if (data > ArchiveData.TotalData)
					{
						ArchiveData.TotalData = data;
					}
					Debug.LogError("stat_data_count:" + data);
				});
			}
			catch (Exception)
			{
				Debug.LogError("SteamUserStats UpdateWorkDuration()");
			}
			UpdateWorkDurationText();
		}

		private void CloseContent()
		{
			if ((bool)_rankLevelController)
			{
				_rankLevelController.Hide();
			}
			for (int i = 0; i < appGroup.AppItemList.Length; i++)
			{
				appGroup.AppItemList[i].ResetIcon();
			}
			content.raycastTarget = false;
		}

		private void NoticeSpecialEvent(DLC8SpecialEvent obj)
		{
			switch (obj)
			{
			case DLC8SpecialEvent.GUIDE_FIRST_SETP_COMPLETE:
				ArchiveData.PositionLevel = 1;
				userInfoGroup.Init();
				EventManager.NoticeCommonEvent(DLC8CommonEvent.SHOW_CHAT, 2310096);
				GameManager.UnlockAchievements("dlc3_worker");
				break;
			case DLC8SpecialEvent.GUIDE_COMPLETE:
				EventManager.NoticeCommonEvent(DLC8CommonEvent.AUTO_SAVE, 0);
				break;
			case DLC8SpecialEvent.STAGE_CLEAR:
				EventManager.NoticeCommonEvent(DLC8CommonEvent.SHOW_DIALOG_TIP, 3910031);
				ArchiveData.PositionLevel = 6;
				ArchiveData.StageClearTime = ArchiveData.MIN;
				userInfoGroup.Init();
				EventManager.NoticeCommonEvent(DLC8CommonEvent.AUTO_SAVE, 0);
				GameManager.UnlockAchievements("dlc3_completed");
				break;
			case DLC8SpecialEvent.STAGE_CLEAR_4000:
				ArchiveData.PositionLevel = 7;
				userInfoGroup.Init();
				EventManager.NoticeCommonEvent(DLC8CommonEvent.AUTO_SAVE, 0);
				GameManager.UnlockAchievements("dlc3_information_hunter");
				break;
			case DLC8SpecialEvent.ALUBA_HIGHEST:
				GameManager.UnlockAchievements("dlc3_hardcore_fans");
				break;
			case DLC8SpecialEvent.ALUBA_LOWEST:
				GameManager.UnlockAchievements("dlc3_anti_fans");
				break;
			case DLC8SpecialEvent.DANEL_LOWEST:
				GameManager.UnlockAchievements("dlc3_promoted");
				break;
			case DLC8SpecialEvent.DDOS_40000:
				GameManager.UnlockAchievements("dlc3_mouse_dead");
				break;
			case DLC8SpecialEvent.BASE_STATION_STAGE_CLEAR:
				GameManager.UnlockAchievements("dlc3_invade_basestation");
				break;
			case DLC8SpecialEvent.WATER_PIPE_STAGE_CLEAR:
				GameManager.UnlockAchievements("dlc3_invade_network");
				break;
			case DLC8SpecialEvent.VOICE_PRINT_STAGE_CLEAR:
				GameManager.UnlockAchievements("dlc3_invade_voiceprint");
				break;
			case DLC8SpecialEvent.VIRUS_STAGE_CLEAR:
				GameManager.UnlockAchievements("dlc3_invade_equipment");
				break;
			}
		}

		private void NoticeCommonEvent(DLC8CommonEvent arg1, int arg2)
		{
			switch (arg1)
			{
			case DLC8CommonEvent.SHOW_DIALOG_TIP:
				if (!_isShowVideo)
				{
					_isShowVideo = true;
					Debug.LogError("DLC8CommonEvent.SHOW_DIALOG_TIP:" + arg2);
					content.raycastTarget = true;
					videoTipDlc8.SetTip("^message_event0144", "avatar_event0103tb", arg2);
					if (arg2 == 3910028)
					{
						teachDialog.Show(TeachDialogStepType.VIDEO_TIP, videoTipDlc8.gameObject);
					}
					EventManager.NoticeCommonEvent(DLC8CommonEvent.ALL_HIDE, 0);
				}
				break;
			case DLC8CommonEvent.SHOW_CHAT:
				if (!ArchiveData.ChatIdList.Contains(arg2))
				{
					Dictionary<int, TalkGroupInfo> talkGroupDic = SingletonAutoMono<DLC8DataController>.GetInstance().TalkGroupInfoManager.talkGroupDic;
					if (talkGroupDic.ContainsKey(arg2))
					{
						List<TalkContentInfo> contentList = talkGroupDic[arg2].contentList;
						ArchiveData.TalkContentInfos.AddRange(contentList);
						ArchiveData.ChatIdList.Add(arg2);
						EventManager.NoticeCommonEvent(DLC8CommonEvent.AUTO_SAVE, 0);
						talkGroup.Show(contentList, _forceChatIds.Contains(arg2), arg2);
						forceTalkGroup.Show(contentList, _forceChatIds.Contains(arg2), arg2);
					}
				}
				break;
			case DLC8CommonEvent.CLOSE_CHAT:
				if (arg2 == 1)
				{
					forceTalkGroup.CloseAnimation();
					talkGroup.ShowAnimation();
				}
				else
				{
					talkGroup.CloseAnimation();
					forceTalkGroup.ShowAnimation();
				}
				break;
			case DLC8CommonEvent.OUT_OF_RESOURCES:
			case DLC8CommonEvent.OUT_OF_LV:
				if (!_isShowNoMoneyTip)
				{
					_isShowNoMoneyTip = true;
					if (arg1 == DLC8CommonEvent.OUT_OF_RESOURCES)
					{
						outTipText.text = I18N.instance.getValue("^110009_common_73");
					}
					else
					{
						outTipText.text = I18N.instance.getValue("^110009_common_154");
					}
					Sequence sequence = DOTween.Sequence();
					sequence.Append(tipCanvasGroup.DOFade(1f, 0.3f).SetEase(Ease.Linear));
					sequence.AppendInterval(3f);
					sequence.Append(tipCanvasGroup.DOFade(0f, 0.3f).SetEase(Ease.Linear).OnComplete(delegate
					{
						_isShowNoMoneyTip = false;
					}));
					sequence.Play();
				}
				break;
			case DLC8CommonEvent.SHOW_DIALOG:
				if (ArchiveData.teachStep == TeachDialogStepType.VIDEO_TIP)
				{
					teachDialog.Hide();
				}
				break;
			case DLC8CommonEvent.FINISH_DIALOG:
				switch (arg2)
				{
				case 3910028:
					AllShow(isAnimation: true);
					teachDialog.Show(TeachDialogStepType.UNLOCK_LEVEL, null);
					break;
				case 3910029:
					EventManager.NoticeSpecialEvent(DLC8SpecialEvent.GUIDE_FIRST_SETP_COMPLETE);
					break;
				case 3910031:
					workCardGroup.ShowFirst(stageClearButton.transform);
					break;
				}
				if (!ArchiveData.DialogIdList.Contains(arg2))
				{
					ArchiveData.DialogIdList.Add(arg2);
				}
				EventManager.NoticeCommonEvent(DLC8CommonEvent.ALL_SHOW, 0);
				EventManager.NoticeCommonEvent(DLC8CommonEvent.AUTO_SAVE, 0);
				_isShowVideo = false;
				break;
			case DLC8CommonEvent.SHOW_WARNING:
				ShowWarning();
				break;
			case DLC8CommonEvent.SHOW_TEACHING:
				Debug.LogError(string.Concat(arg1, "---", arg2));
				if (arg2 == 11)
				{
					UnlockDDOSTeaching();
				}
				break;
			case DLC8CommonEvent.AUTO_SAVE:
				SingletonAutoMono<DLC8DataController>.GetInstance().SaveData();
				GameManager.saveManager.ShowSaveLogo();
				break;
			case DLC8CommonEvent.ALL_HIDE:
				AllHide(isAnimation: true);
				break;
			case DLC8CommonEvent.ALL_SHOW:
				AllShow(isAnimation: true);
				content.raycastTarget = false;
				break;
			case DLC8CommonEvent.SHOW_CLEAR_STAGE_BUTTON:
				stageClearButton.transform.DOScale(0f, 0f);
				CheckStageClearButton();
				stageClearButton.transform.DOScale(1f, 0.38f).OnComplete(delegate
				{
					StartCoroutine("ClearStageAnimation");
				});
				break;
			case DLC8CommonEvent.CLOSE_CONTENT:
			{
				CloseContent();
				for (int num = 0; num < appGroup.AppItemList.Length; num++)
				{
					appGroup.AppItemList[num].Refresh(isWarning: false);
				}
				Debug.Log("CLOSE_CONTENT");
				StartCoroutine("Check");
				GameManager.musicManager.PlayMusicLoop(2);
				break;
			}
			case DLC8CommonEvent.SHOW_EMPLOYEE_BOOK:
				employeeBook.Show();
				break;
			case DLC8CommonEvent.PLAY_END_MOVIE:
			{
				content.raycastTarget = true;
				maskImage.DOFade(0f, 0f);
				maskImage.gameObject.SetActive(value: true);
				GameObject movieDialog = null;
				_gameManager.musicManager.Stop();
				Sequence s = DOTween.Sequence();
				maskImage.DOFade(1f, 3f).OnComplete(delegate
				{
					StringBuilder stringBuilder = new StringBuilder();
					if (I18N.instance.gameLang == LanguageCode.CN)
					{
						stringBuilder.Append("Dialog/Movie/DLC8endVideoCN");
					}
					else if (I18N.instance.gameLang == LanguageCode.TC)
					{
						stringBuilder.Append("Dialog/Movie/DLC8endVideoTW");
					}
					else if (I18N.instance.gameLang == LanguageCode.EN)
					{
						stringBuilder.Append("Dialog/Movie/DLC8endVideoEN");
					}
					movieDialog = UnityEngine.Object.Instantiate(Resources.Load<GameObject>(stringBuilder.ToString()), maskImage.transform);
				});
				s.AppendInterval(66f);
				s.Append(maskImage.DOFade(1f, 2f).OnComplete(delegate
				{
					if (movieDialog != null)
					{
						UnityEngine.Object.Destroy(movieDialog);
					}
					content.raycastTarget = false;
					overWindow.SetActive(value: true);
					SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySound(16);
					overWindow.GetComponent<Animator>().Play("Exit Panel In");
				}));
				break;
			}
			case DLC8CommonEvent.START_DIALOG:
			case DLC8CommonEvent.START_CHAT:
			case DLC8CommonEvent.START_GAME:
			case DLC8CommonEvent.FINISH_GAMME:
			case DLC8CommonEvent.FIRST_FINISH_GAME:
			case DLC8CommonEvent.UNLOCK_MAP:
			case DLC8CommonEvent.UNLOCK_LEVEL:
			case DLC8CommonEvent.UNLOCK_APP:
			case DLC8CommonEvent.CLOSE_PRINT_CANVAS:
			case DLC8CommonEvent.CHANGE_PRINT_PREFAB:
			case DLC8CommonEvent.DOWNLOAD_PRINT_PREFAB:
				break;
			}
		}

		private IEnumerator ClearStageAnimation()
		{
			content.raycastTarget = true;
			SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: false);
			yield return new WaitForSeconds(0.5f);
			_gameManager.musicManager.Stop();
			CameraFilterPack_FX_Glitch1 cameraFilterPackFXGlitch1 = Camera.main.GetComponent<CameraFilterPack_FX_Glitch1>();
			cameraFilterPackFXGlitch1.enabled = true;
			DOTween.To(() => cameraFilterPackFXGlitch1.Glitch, delegate(float x)
			{
				cameraFilterPackFXGlitch1.Glitch = x;
			}, 1f, 0.5f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.5f);
			GameManager.soundManager.PlaySound(60);
			whileTrueGroup.SetActive(value: true);
			CanvasGroup whileTrueCanvasGroup = whileTrueGroup.GetComponent<CanvasGroup>();
			Sequence sequence = DOTween.Sequence();
			sequence.Append(whileTrueCanvasGroup.DOFade(1f, 0.5f));
			sequence.AppendInterval(3f);
			sequence.Append(whileTrueCanvasGroup.DOFade(0f, 0.5f).OnComplete(delegate
			{
				whileTrueCanvasGroup.gameObject.SetActive(value: false);
			}));
			sequence.Append(DOTween.To(() => cameraFilterPackFXGlitch1.Glitch, delegate(float x)
			{
				cameraFilterPackFXGlitch1.Glitch = x;
			}, 0f, 0.5f).OnComplete(delegate
			{
				cameraFilterPackFXGlitch1.enabled = false;
			}));
			sequence.AppendInterval(2f);
			sequence.Append(whileTrueCanvasGroup.DOFade(0f, 0f).OnComplete(delegate
			{
				DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.SHOW_CHAT, 2310111);
				workCardGroup.firstButton.gameObject.SetActive(value: false);
				content.raycastTarget = false;
			}));
			sequence.Play();
		}

		private void CheckDialog3910030()
		{
			if (ArchiveData.DialogIdList.Contains(3910030))
			{
				return;
			}
			for (int i = 0; i < ArchiveData.NewsTitleList.Count; i++)
			{
				PublicOpinionNewsTitleInfo publicOpinionNewsTitleInfo = ArchiveData.NewsTitleList[i];
				if (publicOpinionNewsTitleInfo.type == 3 && publicOpinionNewsTitleInfo.rank > 10)
				{
					DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.SHOW_DIALOG_TIP, 3910030);
				}
			}
		}

		private void UnlockDDOSTeaching()
		{
			ArchiveData.DdosLevel.isUnlock = true;
			GameObject gameObject = appGroup.AppItemByCityGameType(CityGameType.DDOS).gameObject;
			AppItem component = gameObject.GetComponent<AppItem>();
			ArchiveData.UnlockApp(CityGameType.DDOS);
			component.Unlock();
			EventManager.NoticeCommonEvent(DLC8CommonEvent.UNLOCK_APP, 4);
			teachDialog.Show(TeachDialogStepType.UNLOCK_DOOS, gameObject);
		}

		private void ShowWarning()
		{
			if (!_isShowWarning)
			{
				_isShowWarning = true;
				SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySoundLoop(59);
				warningGroup.Show();
				if (!ArchiveData.isFinishedWarningTeach)
				{
					teachDialog.Show(TeachDialogStepType.WARNING, warningGroup.gameObject);
				}
				else if (ArchiveData.teachStep == TeachDialogStepType.WARNING_FINISH || ArchiveData.teachStep == TeachDialogStepType.UNLOCK_PUBLICOPINION)
				{
					teachDialog.Show(TeachDialogStepType.UNLOCK_PUBLICOPINION, appGroup.AppItemByCityGameType(CityGameType.PUBLIC_OPINION).gameObject);
				}
			}
		}

		private void Start()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: true);
			GameManager.musicManager.PlayMusicLoop(2);
			goMainButton.onClick.AddListener(GoMain);
			continueButton.onClick.AddListener(Continue);
			UpdateWorkDurationText();
			InvokeRepeating("UpdateWorkDuration", 60f, 60f);
			StartCoroutine("Check");
		}

		private void Continue()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySound(16);
			overWindow.SetActive(value: false);
			maskImage.DOFade(0f, 2f).OnComplete(delegate
			{
				maskImage.gameObject.SetActive(value: false);
				content.raycastTarget = false;
				SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: true);
				GameManager.musicManager.PlayMusic(2);
			});
		}

		private void GoMain()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySound(16);
			overWindow.SetActive(value: false);
			SceneManager.LoadScene("mainScene");
		}

		private IEnumerator Check()
		{
			yield return new WaitForSeconds(1f);
			CheckTeaching();
			CheckLevelRecord();
			CheckStageClearButton();
			CheckDialog3910030();
			CheckNotice();
		}

		private void CheckNotice()
		{
			if (ArchiveData.NegativeProgress() > 0.75f)
			{
				EventManager.NoticeCommonEvent(DLC8CommonEvent.SHOW_WARNING, 1);
			}
			if (ArchiveData.DdosLevel.BestScore >= 10000)
			{
				DLC8EventManager.Instance.NoticeSpecialEvent(DLC8SpecialEvent.DDOS_40000);
			}
			int[] lvProgress = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.lvProgress;
			if (ArchiveData.PositionLevel > 0 && ArchiveData.PositionLevel < 6)
			{
				int[] array = new int[4] { 2310106, 2310107, 2310108, 2310109 };
				int num = -1;
				for (int i = 0; i < lvProgress.Length; i++)
				{
					int num2 = lvProgress[i];
					if (ArchiveData.PersonData >= num2)
					{
						num = i;
					}
				}
				if (num > -1 && ArchiveData.PositionLevel < num + 2)
				{
					ArchiveData.PositionLevel = num + 2;
					DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.SHOW_CHAT, array[num]);
				}
			}
			if (!ArchiveData.DialogIdList.Contains(3910028) && !_isShowVideo)
			{
				EventManager.NoticeCommonEvent(DLC8CommonEvent.SHOW_DIALOG_TIP, 3910028);
				EventManager.NoticeCommonEvent(DLC8CommonEvent.ALL_HIDE, 0);
			}
			userInfoGroup.Init();
		}

		private void CheckTeaching()
		{
			if (new List<TeachDialogStepType>
			{
				TeachDialogStepType.VIDEO_TIP,
				TeachDialogStepType.GAME_SUCCESS,
				TeachDialogStepType.UNLOCK_LEVEL,
				TeachDialogStepType.WARNING,
				TeachDialogStepType.UNLOCK_PUBLICOPINION
			}.Contains(_archiveData.teachStep))
			{
				Debug.LogError("CheckTeaching:" + _archiveData.teachStep);
				teachDialog.Show(_archiveData.teachStep, (_archiveData.teachStep == TeachDialogStepType.WARNING) ? warningGroup.gameObject : null);
			}
			if (_archiveData.teachStep == TeachDialogStepType.GAME_SUCCESS_FINISH && !_archiveData.DialogIdList.Contains(3910029))
			{
				EventManager.NoticeCommonEvent(DLC8CommonEvent.SHOW_DIALOG_TIP, 3910029);
			}
		}

		private void CheckStageClearButton()
		{
			if (ArchiveData.StageClearTime > 0 && ArchiveData.DialogIdList.Contains(3910031))
			{
				stageClearButton.onClick.AddListener(OpenWorkCardGroup);
				stageClearButton.gameObject.SetActive(value: true);
				Material material = stageClearButton.GetComponent<Image>().material;
				Sequence sequence = DOTween.Sequence();
				sequence.Append(material.DOFloat(0f, "_ShineLocation", 1f).SetEase(Ease.Linear));
				sequence.AppendInterval(6f);
				sequence.Append(material.DOFloat(1f, "_ShineLocation", 1f).SetEase(Ease.Linear));
				sequence.AppendInterval(10f);
				sequence.SetLoops(-1).Play();
			}
		}

		private void OpenWorkCardGroup()
		{
			printCanvasDlc8.Show();
			workCardGroup.Show();
		}

		public void ClickApp(CityGameType cityGameTyp)
		{
			switch (cityGameTyp)
			{
			case CityGameType.WATER_PIPE:
			case CityGameType.VIRUS:
			case CityGameType.VOICE:
			case CityGameType.BASE_STATION:
				if ((bool)_rankLevelController)
				{
					if (_rankLevelController.GameType == cityGameTyp)
					{
						break;
					}
				}
				else
				{
					_rankLevelController = UnityEngine.Object.Instantiate(rankLevelControllerPrefab, content.transform);
				}
				content.raycastTarget = true;
				_rankLevelController.Show(cityGameTyp);
				if (!SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.isFinishedRankTeach)
				{
					teachDialog.Show(TeachDialogStepType.RANK, _rankLevelController.rankGroup.gameObject);
				}
				break;
			case CityGameType.DDOS:
				if ((bool)_rankLevelController)
				{
					UnityEngine.Object.Destroy(_rankLevelController.gameObject);
				}
				if ((bool)_invadeServerController)
				{
					UnityEngine.Object.Destroy(_invadeServerController.gameObject);
				}
				content.raycastTarget = true;
				_invadeServerController = UnityEngine.Object.Instantiate(invadeControllerPrefab, content.transform);
				if (_archiveData.teachStep == TeachDialogStepType.UNLOCK_DOOS)
				{
					teachDialog.Hide();
				}
				break;
			case CityGameType.PUBLIC_OPINION:
				if ((bool)_rankLevelController)
				{
					UnityEngine.Object.Destroy(_rankLevelController.gameObject);
				}
				warningGroup.Hide();
				SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.Stop();
				if (_archiveData.teachStep == TeachDialogStepType.UNLOCK_PUBLICOPINION)
				{
					_archiveData.teachStep = TeachDialogStepType.UNLOCK_PUBLICOPINION_FINISH;
					teachDialog.Hide();
				}
				ArchiveData.isFinishedWarningTeach = true;
				UnityEngine.Object.Instantiate(publicOptionPrefab, content.transform);
				_isShowWarning = false;
				break;
			}
		}

		public void ShowGameContent(LevelRecord levelRecord)
		{
			GameManager.musicManager.PlayMusicLoop(16);
			content.raycastTarget = true;
			_gameContent = UnityEngine.Object.Instantiate(gameContentPrefab, content.transform);
			_gameContent.Show(levelRecord, delegate
			{
			});
		}

		private void FixedUpdate()
		{
			if (_gameContent != null || _rankLevelController != null)
			{
				content.raycastTarget = true;
			}
		}

		public void ShowDDosGame()
		{
			content.raycastTarget = true;
			UnityEngine.Object.Instantiate(ddosPrefab, content.transform);
		}

		private void Awake()
		{
			EventManager.onNoticeCommonEvent += NoticeCommonEvent;
			EventManager.onNoticeSpecialEvent += NoticeSpecialEvent;
			EventManager.onNoticeControllerGameOver += NoticeControllerGameOver;
		}

		private void OnDestroy()
		{
			EventManager.onNoticeCommonEvent -= NoticeCommonEvent;
			EventManager.onNoticeControllerGameOver -= NoticeControllerGameOver;
			EventManager.onNoticeSpecialEvent -= NoticeSpecialEvent;
		}
	}
}
