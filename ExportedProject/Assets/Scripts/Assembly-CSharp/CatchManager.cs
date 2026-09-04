using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using Dlc.Catch.model;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CatchManager : MonoBehaviour
{
	public RectTransform canvasRectTransform;

	public List<Text> _placeNameTexts;

	private bool isDrag;

	private List<WayPoint> _waypoints = new List<WayPoint>();

	public List<Image> _exitImages = new List<Image>();

	public Canvas canvas;

	public Image _splashImage;

	public Text _splashLoading;

	private UITool _uiTool;

	private List<PoliceAI> _policeAis;

	public Image _searchImage;

	public Image _searchPointImage;

	public Button continueButton;

	public Image _endImage;

	public Text _timeText;

	private List<Vector2> _searchPointList;

	public Image _startImage;

	public Text _showText;

	public Button _teachButton;

	public List<Image> teachImages;

	public int _teachIndex;

	public GameObject _teachObj;

	public CatchInfoPanel _infoPanel;

	private bool canSpeak2;

	private bool isHit;

	public Image dangerImage;

	public Button replayButton;

	public Button goHomeButton;

	public Image failImage;

	private GameManager gameManager;

	private bool isEnglish;

	private string[][] curSpeakDatas;

	private int curStep;

	private int _hitCount;

	private void Start()
	{
		Debug.Log("Start");
	}

	private void Awake()
	{
		Debug.Log("Awake");
		goHomeButton.onClick.AddListener(GoMain);
		replayButton.onClick.AddListener(ReplayGame);
		if (gameManager == null)
		{
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		}
		if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
		{
			isEnglish = false;
		}
		else if (I18N.instance.gameLang == LanguageCode.EN)
		{
			isEnglish = true;
		}
		gameManager.musicManager.Audiosource.clip = gameManager.musicManager.musics[22];
		gameManager.musicManager.Audiosource.Play();
		_teachButton.onClick.AddListener(delegate
		{
			if (_teachIndex >= 2)
			{
				_teachObj.SetActive(value: false);
				_infoPanel.Show();
			}
			else
			{
				_teachIndex++;
				teachImages[_teachIndex - 1].GetComponent<CanvasGroup>().DOFade(0f, 0f);
				teachImages[_teachIndex].GetComponent<CanvasGroup>().DOFade(1f, 0f);
			}
		});
		if (continueButton != null)
		{
			continueButton.onClick.AddListener(delegate
			{
				_endImage.gameObject.SetActive(value: true);
				CatchEvent.Instance.NoticeNextEvent(CatchEventEnum.GAME_SUCCESS);
			});
		}
		_uiTool = new UITool();
		long num = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		GameObject[] array = GameObject.FindGameObjectsWithTag("CatchWayPoint");
		_searchImage.DOFade(0f, 0f);
		_searchPointImage.DOFade(0f, 0f);
		_ = new string[5] { "wall (1)", "wall1 (105)", "wall1 (173)", "wall1 (187)", "wall1 (235)" };
		_searchPointList = new List<Vector2>();
		GameObject[] array2 = GameObject.FindGameObjectsWithTag("searchPos");
		_searchPointList.Add(_searchImage.transform.localPosition);
		for (int num2 = 0; num2 < array2.Length; num2++)
		{
			_searchPointList.Add(array2[num2].GetComponent<RectTransform>().localPosition);
		}
		for (int num3 = 0; num3 < array.Length; num3++)
		{
			GameObject gameObject = array[num3];
			if (!gameObject.TryGetComponent<WayPoint>(out var component))
			{
				continue;
			}
			RectTransform component2 = gameObject.GetComponent<RectTransform>();
			Rect rect = new Rect(component2.anchoredPosition, component2.sizeDelta);
			int pointType = component.PointType;
			if ((pointType == 0 && component.PathCount == 4) || (pointType == 1 && component.PathCount == 2))
			{
				continue;
			}
			component.Index = num3;
			foreach (GameObject gameObject2 in array)
			{
				if (gameObject2.TryGetComponent<WayPoint>(out var component3) && !(component.name == component3.name) && component3.PointType != pointType)
				{
					RectTransform component4 = gameObject2.GetComponent<RectTransform>();
					Rect other = new Rect(component4.anchoredPosition, component4.sizeDelta);
					if (rect.Overlaps(other))
					{
						component.BelongPaths.Add(component3);
					}
				}
			}
			component.ResetRound();
			_waypoints.Add(component);
		}
		for (int num5 = 0; num5 < _waypoints.Count; num5++)
		{
			_waypoints[num5].ResetSingleWayPoint();
		}
		GameObject[] array3 = GameObject.FindGameObjectsWithTag("Police");
		GameObject.FindGameObjectWithTag("Enemy").GetComponent<CatchEnemy>().Init(_waypoints);
		for (int num6 = 0; num6 < array3.Length; num6++)
		{
			array3[num6].GetComponent<PoliceAI>().Init(_waypoints);
		}
		Debug.Log((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000 - num);
		_uiTool.LoadingText(_splashLoading, I18N.instance.getValue("^95DA4A18-566C-E473-1D35-4BDBB5C54705"), 6);
		for (int num7 = 0; num7 < _placeNameTexts.Count; num7++)
		{
			string[] array4 = PlaceNameList();
			_placeNameTexts[num7].text = I18N.instance.getValue(array4[num7]);
		}
	}

	private void ReplayGame()
	{
		gameManager.ShowFloatBox();
		Invoke("ReplayScene", 1f);
	}

	private void GoMain()
	{
		gameManager.ShowFloatBox();
		Invoke("ChangeHomeScene", 1f);
	}

	private void ChangeHomeScene()
	{
		gameManager.musicManager.PlayMusicLoop(8);
		gameManager.txt_studio.SetActive(value: false);
		failImage.gameObject.SetActive(value: false);
		SceneManager.LoadSceneAsync("home");
	}

	private void ReplayScene()
	{
		gameManager.soundManager.audiosourceloop.Stop();
		gameManager.soundManager.audiosource.Stop();
		gameManager.musicManager.Audiosource.Stop();
		gameManager.musicManager.ResumeVol();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private string[] PlaceNameList()
	{
		return new string[23]
		{
			"^06BDCBC8-82FD-EFF6-2E7F-C86CDD6243B8", "^A7AFAF6A-22BD-412F-2A20-87F5EE8A1762", "^968BC71D-E2AC-2561-F3F3-238AC5A82DB0", "^5F774A3A-EB5D-38C1-E496-C02C33FE49E7", "^5C5E9D97-32F8-C593-410D-9E018B7CB96C", "^5862662D-F38D-09B0-9928-225E5A5DB3AE", "^42F23413-EAD4-D567-7F88-0D11E671902A", "^064FEF1A-4FFE-6747-9CCB-2ACC7B1817CE", "^C25983B2-EEF3-49D3-31A1-C9D71CE3B4CE", "^24AE917E-173D-DFD6-8B46-0545A86AD988",
			"^38C050C1-28A4-1FEE-2C6B-79492F606AC8", "^4F36ED2A-621C-014F-51FE-4BF21E543C17", "^FF428261-EB1D-425D-77C5-635469552A84", "^4188C7BB-405E-F1DE-3D08-C9CC6226F796", "^12681B56-6802-581B-048B-5A8823F0ED3E", "^84515F7B-7590-B6E6-4250-DC0552C7F56E", "^D81EFA13-AFA4-1DC1-1EE3-5610DEDBCEC5", "^83668377-49A1-AE83-3CC3-9A8E8CF549DF", "^DE8D791E-657A-508B-93FD-8E0BD308EC8E", "^0B37E2F9-2723-D86F-0848-6402C7846D08",
			"^1DC472B2-4E54-101F-3F78-AFCF7BD0A4F7", "^19F15CD5-510C-4A6E-F624-9B64344655BC", "^40997592-8A10-722D-91F0-B0A2BE6C77E0"
		};
	}

	private void SearchEnemyAnimation()
	{
		RectTransform rectTransform = GameObject.FindGameObjectWithTag("Enemy").transform as RectTransform;
		_searchPointList.Add(rectTransform.localPosition);
		CanvasGroup searchGroup = _searchImage.GetComponent<CanvasGroup>();
		searchGroup.DOFade(1f, 0.3f).OnComplete(delegate
		{
			gameManager.soundManager.PlaySoundLoop(52);
			_searchImage.DOFade(1f, 0f);
			_searchPointImage.DOFade(1f, 0f);
			Sequence sequence = DOTween.Sequence();
			sequence.Append(_searchImage.DOFade(0.5f, 0.5f));
			sequence.Append(_searchImage.DOFade(1f, 0.5f));
			sequence.SetLoops(-1);
			sequence.Play();
			Sequence sequence2 = DOTween.Sequence();
			sequence2.Append(_searchPointImage.DOFade(0.3f, 0.5f));
			sequence2.Append(_searchPointImage.DOFade(1f, 0.5f));
			sequence2.SetLoops(-1);
			sequence2.Play();
			RectTransform rectTransform2 = _searchImage.transform as RectTransform;
			Vector2 vector = rectTransform2.localPosition;
			Sequence sequence3 = DOTween.Sequence();
			Vector3[] array = new Vector3[_searchPointList.Count + 1];
			array[0] = vector;
			for (int i = 0; i < _searchPointList.Count; i++)
			{
				Vector2 vector2 = _searchPointList[i];
				array[i + 1] = vector2;
			}
			sequence3.Append(rectTransform2.DOLocalPath(array, 3f, PathType.CatmullRom).SetEase(Ease.Linear));
			sequence3.AppendInterval(2f);
			sequence3.Append(searchGroup.DOFade(0f, 0.3f));
			sequence3.Play().OnComplete(delegate
			{
				gameManager.soundManager.audiosourceloop.Stop();
				gameManager.soundManager.audiosource.Stop();
				CatchEvent.Instance.NoticeEnemyShow();
			});
		});
	}

	private void Hit()
	{
		if (_hitCount < 3)
		{
			Sequence sequence = DOTween.Sequence();
			dangerImage.gameObject.SetActive(value: true);
			_hitCount++;
			sequence.SetId("CatchManagerHaloSequence");
			sequence.Append(dangerImage.DOFade(0.3f, 0.5f));
			sequence.Append(dangerImage.DOFade(0.5f, 0.5f));
			sequence.SetLoops(-1);
			sequence.Play();
		}
	}

	private void HitFinished()
	{
		DOTween.Kill("CatchManagerHaloSequence");
		dangerImage.gameObject.SetActive(value: false);
		_ = _hitCount;
		_ = 3;
	}

	private void OnEnable()
	{
		CatchEvent.Instance.onNoticeShowSearch += SearchEnemyAnimation;
		CatchEvent.Instance.onNoticePoliceShow += NoticePoliceShow;
		CatchEvent.Instance.onNoticeNextEvent += NoticeNextEvent;
		CatchEvent.Instance.onNoticeStart += NoticeStart;
	}

	private void NoticeNextEvent(CatchEventEnum obj)
	{
		if (obj == CatchEventEnum.CATCH_HIT)
		{
			Hit();
		}
		if (obj == CatchEventEnum.CATCH_HIT_FINISHED)
		{
			HitFinished();
		}
		if (obj == CatchEventEnum.SHOW_CALL)
		{
			_endImage.gameObject.SetActive(value: true);
			_endImage.GetComponent<CatchEnd>().Begin();
		}
		if (obj == CatchEventEnum.SHOW_EXIT)
		{
			for (int i = 0; i < _exitImages.Count; i++)
			{
				_exitImages[i].DOFade(1f, 1.5f);
			}
		}
		if (obj == CatchEventEnum.GAME_SUCCESS)
		{
			_endImage.gameObject.SetActive(value: true);
			Invoke("GameOver", 1f);
		}
		if (obj == CatchEventEnum.CATCH_HIT)
		{
			canvasRectTransform.DOShakePosition(1f, new Vector3(5f, 6f, 7f));
			Speak2();
		}
		if (obj == CatchEventEnum.GAME_FAIL)
		{
			gameManager.soundManager.audiosourceloop.Stop();
			gameManager.soundManager.audiosource.Stop();
			gameManager.soundManager.catchsourceloop.Stop();
			gameManager.soundManager.PlaySound(26);
			failImage.gameObject.SetActive(value: true);
		}
	}

	private void GameOver()
	{
		_endImage.GetComponent<CatchEnd>().Begin();
	}

	private void NoticePoliceShow()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.AppendInterval(2f);
		sequence.Append(_startImage.GetComponent<CanvasGroup>().DOFade(1f, 1f));
		sequence.AppendInterval(2f);
		sequence.Append(_startImage.GetComponent<CanvasGroup>().DOFade(0f, 1f)).OnComplete(delegate
		{
			CatchEvent.Instance.NoticeStart();
		});
		sequence.Play();
	}

	private void OnDisable()
	{
		CatchEvent.Instance.onNoticeShowSearch -= SearchEnemyAnimation;
		CatchEvent.Instance.onNoticePoliceShow -= NoticePoliceShow;
		CatchEvent.Instance.onNoticeNextEvent -= NoticeNextEvent;
		CatchEvent.Instance.onNoticeStart -= NoticeStart;
	}

	private void NoticeStart()
	{
		Invoke("Speak1", 1f);
	}

	private void Speak1()
	{
		gameManager.soundManager.PlayCatch((!isEnglish) ? 1 : 7);
		string[][] array = ((!isEnglish) ? new string[4][]
		{
			new string[4] { "", "4.0", "0.0", "1.5" },
			new string[4] { "^vdev1008", "4.0", "0.0", "2.0" },
			new string[4] { "^vdev1010", "4.0", "0.0", "2.0" },
			new string[4] { "^vdev1011", "4.0", "2.0", "2.0" }
		} : new string[4][]
		{
			new string[4] { "", "1.226", "0.31", "1.5" },
			new string[4] { "^vdev1008", "3.155", "0.5", "2.0" },
			new string[4] { "^vdev1010", "2.982", "0.002", "1.0" },
			new string[4] { "^vdev1011", "3.922", "0.305", "2.0" }
		});
		curSpeakDatas = array;
		curStep = 1;
		StartCoroutine(SpeakIEnumerator());
	}

	private void Speak2()
	{
		if (canSpeak2 && !isHit)
		{
			isHit = true;
			gameManager.soundManager.PlayCatch(isEnglish ? 8 : 2);
			string[][] array = ((!isEnglish) ? new string[15][]
			{
				new string[4] { "", "2.0", "0.0", "0.0" },
				new string[4] { "^vdev1012", "2.5", "1.0", "1.0" },
				new string[4] { "^vdev1013", "6.0", "0.0", "3.0" },
				new string[4] { "^vdev1014", "2.5", "0.0", "1.0" },
				new string[4] { "^vdev1015", "2.5", "0.0", "1.0" },
				new string[4] { "^vdev1016", "8.0", "0.0", "4.0" },
				new string[4] { "^vdev1017", "4.5", "0.5", "2.0" },
				new string[4] { "^vdev1018", "3.0", "0.5", "2.0" },
				new string[4] { "", "2.0", "0.0", "1.0" },
				new string[4] { "^vdev1019", "3.5", "0.0", "2.0" },
				new string[4] { "^vdev1020", "6.5", "0.0", "3.4" },
				new string[4] { "", "3.0", "0.0", "1.0" },
				new string[4] { "^vdev1021", "6.0", "2.0", "2.0" },
				new string[4] { "^vdev1022", "3.0", "0.0", "1.5" },
				new string[4] { "^vdev1023", "4.0", "0.0", "1.0" }
			} : new string[17][]
			{
				new string[4] { "", "1.130", "0.001", "0.0" },
				new string[4] { "^vdev1012", "1.606", "0.003", "1.0" },
				new string[4] { "", "1.969", "0.0", "0.0" },
				new string[4] { "^vdev1013", "3.199", "0.58", "3.0" },
				new string[4] { "^vdev1014", "1.93", "0.243", "1.5" },
				new string[4] { "^vdev1015", "2.442", "0.014", "2.0" },
				new string[4] { "^vdev1016", "6.361", "0.205", "5.0" },
				new string[4] { "^vdev1017", "6.193", "1.076", "5.0" },
				new string[4] { "^vdev1018", "5.642", "0.3", "3.0" },
				new string[4] { "", "0.172", "0.001", "1.0" },
				new string[4] { "^vdev1019", "2.782", "0.404", "2.0" },
				new string[4] { "^vdev1020", "6.967", "0.0", "5.0" },
				new string[4] { "", "0.412", "0.0", "1.0" },
				new string[4] { "^vdev1021", "7.152", "0.453", "5.0" },
				new string[4] { "^vdev1022", "4.298", "1.189", "3.0" },
				new string[4] { "^vdev1023", "1.751", "0.0", "1.0" },
				new string[4] { "", "4.6198", "0.0", "1.0" }
			});
			curStep = 2;
			curSpeakDatas = array;
			StartCoroutine(SpeakIEnumerator());
		}
	}

	private IEnumerator SpeakIEnumerator()
	{
		if (curStep == 1)
		{
			CatchEvent.Instance.NoticeSpeak(CatchSpeakRole.LISA, isEnglish ? 13 : 21);
		}
		else
		{
			CatchEvent.Instance.NoticeSpeak(CatchSpeakRole.LISA, isEnglish ? 68 : 75);
		}
		for (int i = 0; i < curSpeakDatas.Length; i++)
		{
			string[] array = curSpeakDatas[i];
			float seconds = Convert.ToSingle(array[1], CultureInfo.InvariantCulture);
			float interval = Convert.ToSingle(array[2], CultureInfo.InvariantCulture);
			float duration = Convert.ToSingle(array[3], CultureInfo.InvariantCulture);
			string endValue = "";
			if (array[0].StartsWith("^"))
			{
				endValue = I18N.instance.getValue(array[0]);
			}
			_showText.DOText("", 0f);
			_showText.DOText(endValue, duration).SetEase(Ease.Linear);
			yield return new WaitForSeconds(seconds);
			_showText.DOText("", 0f);
			canSpeak2 = true;
			yield return new WaitForSeconds(interval);
		}
	}

	private void Speak(string[][] dataArray, int index)
	{
		if (index == 1)
		{
			CatchEvent.Instance.NoticeSpeak(CatchSpeakRole.LISA, isEnglish ? 13 : 21);
		}
		else
		{
			CatchEvent.Instance.NoticeSpeak(CatchSpeakRole.LISA, isEnglish ? 68 : 75);
		}
		Sequence sequence = DOTween.Sequence();
		foreach (string[] array in dataArray)
		{
			float num = float.Parse(array[1], CultureInfo.InvariantCulture);
			float interval = float.Parse(array[2], CultureInfo.InvariantCulture);
			float num2 = float.Parse(array[3], CultureInfo.InvariantCulture);
			string endValue = "";
			if (array[0].StartsWith("^"))
			{
				endValue = I18N.instance.getValue(array[0]);
			}
			sequence.Append(_showText.DOText("", 0f));
			sequence.Append(_showText.DOText(endValue, num2).SetEase(Ease.Linear));
			sequence.AppendInterval(num - num2);
			sequence.Append(_showText.DOText("", 0f).OnComplete(delegate
			{
				canSpeak2 = true;
			}));
			sequence.AppendInterval(interval);
		}
		sequence.Play();
	}
}
