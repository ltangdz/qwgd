using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using DLC7.DDOS;
using Honeti;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.TitanWeb
{
	public class TitanQueryProgress : MonoBehaviour
	{
		public Transform bgTransform;

		public Transform contentTransform;

		public List<Image> topProgressImages;

		public List<Image> centerProgressImages;

		public Text tipText;

		public Text progressText;

		public Image resultImage;

		public Text resultText;

		public CanvasGroup canvasGroup;

		public Image leftFrameImage;

		public Image rightFrameImage;

		public Text collectedText;

		public Button backButton;

		public Button closeButton;

		public Button queryButton;

		public InputField matchInput;

		private Vector2 leftFrameToPos = new Vector2(-489.7f, 75.1f);

		private Vector2 rightFrameToPos = new Vector2(490.8f, -88.1f);

		private RectTransform _leftFrameRt;

		private RectTransform _rightFrameRt;

		public List<CanvasGroup> canvasGroups;

		public List<Sprite> centerSpriteList;

		public List<Color> resultColorList;

		private int _progress;

		private string _loadingTip;

		private bool _isLoading;

		private bool _isSuccess;

		[Header("第一阶段查询结果部分")]
		public List<GameObject> contentGroup;

		public CanvasGroup canvasGroup2;

		public List<GameObject> baseDBGroups;

		public List<GameObject> advancedDBGroups;

		public List<Text> baseContentTextList;

		public List<Image> avatarList;

		public CanvasGroup idGroup;

		public List<GameObject> travelContentList;

		private List<Vector2> leftFrameToPosList = new List<Vector2>
		{
			new Vector2(-489.7f, 150f),
			new Vector2(-489.7f, 348f)
		};

		private List<Vector2> rightFrameToPosList = new List<Vector2>
		{
			new Vector2(490.8f, -163f),
			new Vector2(490.8f, -357f)
		};

		private List<Vector3> scaleList = new List<Vector3>
		{
			new Vector3(1f, 0.63f, 1f),
			new Vector3(1f, 0.325f, 1f)
		};

		private List<Vector2> sizeList = new List<Vector2>
		{
			new Vector2(976.5f, 455.5f),
			new Vector2(976.5f, 873.8f)
		};

		private bool _isAdvanced;

		private List<Text> _baseDataTextList;

		private string _tureName;

		private string _extraInfo;

		private GameManager _gameManager;

		private string[] _dataList;

		private Dictionary<string, string> _avatarDic;

		private string[] _items;

		private string[] _keyList = new string[11]
		{
			"^psw_socialnum", "^76054F9F-5186-A575-0399-9074EBB4DBD7", "^psw_birth", "^110008_common_20", "^110008_common_21", "^psw_phone", "^houtai17", "^110008_common_22", "^110008_common_23", "^110008_common_24",
			"^110008_common_25"
		};

		private Dictionary<string, string> _travelDataDic;

		public Dictionary<string, string> AvatarDic
		{
			get
			{
				if (_avatarDic == null)
				{
					_avatarDic = new Dictionary<string, string>();
					_avatarDic.Add("Lucrezia Borgia", "touxiang02");
					_avatarDic.Add("Simon Cavendish", "touxiang04");
					_avatarDic.Add("Tom Blanco", "touxiang05");
					_avatarDic.Add("Vincent Valen Park", "touxiang06");
					_avatarDic.Add("Shirley Payten", "touxiang03");
					_avatarDic.Add("Cloud Shawn", "touxiang07");
				}
				return _avatarDic;
			}
		}

		public Dictionary<string, string> TravelDataDic
		{
			get
			{
				if (_travelDataDic == null)
				{
					_travelDataDic = new Dictionary<string, string>();
					_travelDataDic.Add("lucrezia borgia", "[{\"col_1\":\"2020.01.31\",\"col_2\":\"Aridru\",\"col_3\":\"G3204\",\"col_4\":\"Narriott\",\"col_5\":\"6001\",\"col_6\":\"2020.02.01\",\"col_7\":\"Gauti\",\"col_8\":\"G7471\"},{\"col_1\":\"2016.02.09\",\"col_2\":\"Driord\",\"col_3\":\"A9502\",\"col_4\":\"Narriott\",\"col_5\":\"0410\",\"col_6\":\"2012.02.10\",\"col_7\":\"Gauti\",\"col_8\":\"A5538\"},{\"col_1\":\"2009.03.20\",\"col_2\":\"Glalos\",\"col_3\":\"G8752\",\"col_4\":\"Best Eastern\",\"col_5\":\"9012\",\"col_6\":\"2009.03.21\",\"col_7\":\"Gauti\",\"col_8\":\"G2496\"},{\"col_1\":\"2009.03.07\",\"col_2\":\"Slutiarm\",\"col_3\":\"A7535\",\"col_4\":\"Vesidence\",\"col_5\":\"2310\",\"col_6\":\"2009.03.08\",\"col_7\":\"Gauti\",\"col_8\":\"A7231\"},{\"col_1\":\"2009.02.28\",\"col_2\":\"Tawilah\",\"col_3\":\"G7651\",\"col_4\":\"Narriott\",\"col_5\":\"1822\",\"col_6\":\"2009.02.29\",\"col_7\":\"Gauti\",\"col_8\":\"G7651\"}]");
					_travelDataDic.Add("simon cavendish", "[{\"col_1\":\"2016.02.09\",\"col_2\":\"Phax\",\"col_3\":\"A9502\",\"col_4\":\"Moonwood\",\"col_5\":\"\",\"col_6\":\"\",\"col_7\":\"\",\"col_8\":\"\"},{\"col_1\":\"2009.03.20\",\"col_2\":\"Glalos\",\"col_3\":\"G8752\",\"col_4\":\"Best Eastern\",\"col_5\":\"9013\",\"col_6\":\"2009.03.21\",\"col_7\":\"Gauti\",\"col_8\":\"G2496\"},{\"col_1\":\"2009.03.07\",\"col_2\":\"Slutiarm\",\"col_3\":\"A7535\",\"col_4\":\"Vesidence\",\"col_5\":\"2311\",\"col_6\":\"2009.03.08\",\"col_7\":\"Gauti\",\"col_8\":\"A7231\"},{\"col_1\":\"2009.02.28\",\"col_2\":\"Tawilah\",\"col_3\":\"G7651\",\"col_4\":\"Narriott\",\"col_5\":\"1823\",\"col_6\":\"2009.02.29\",\"col_7\":\"Gauti\",\"col_8\":\"G7651\"}]");
					_travelDataDic.Add("tom blanco", "[{\"col_1\":\"2021.01.01\",\"col_2\":\"Driord\",\"col_3\":\"A7413\",\"col_4\":\"Narriott\",\"col_5\":\"9701\",\"col_6\":\"2021.01.03\",\"col_7\":\"Gauti\",\"col_8\":\"T3786\"},{\"col_1\":\"2020.06.15\",\"col_2\":\"Dreg\",\"col_3\":\"G6320\",\"col_4\":\"Lovin\",\"col_5\":\"820\",\"col_6\":\"2020.06.17\",\"col_7\":\"Gauti\",\"col_8\":\"G7365\"},{\"col_1\":\"2020.03.14\",\"col_2\":\"Slutiarm\",\"col_3\":\"A7539\",\"col_4\":\"Vesidence\",\"col_5\":\"305\",\"col_6\":\"2020.03.15\",\"col_7\":\"Gauti\",\"col_8\":\"A8729\"},{\"col_1\":\"2019.11.27\",\"col_2\":\"Glalos\",\"col_3\":\"T2133\",\"col_4\":\"Narriott\",\"col_5\":\"0716\",\"col_6\":\"2019.11.29\",\"col_7\":\"Gauti\",\"col_8\":\"T8450\"}]");
					_travelDataDic.Add("vincent valen park", "[{\"col_1\":\"2020.12.25\",\"col_2\":\"Glalos\",\"col_3\":\"G8752\",\"col_4\":\"Vesidence\",\"col_5\":\"0421\",\"col_6\":\"2020.12.26\",\"col_7\":\"Gauti\",\"col_8\":\"G2496\"},{\"col_1\":\"2020.01.31\",\"col_2\":\"Aridru\",\"col_3\":\"G3204\",\"col_4\":\"Narriott\",\"col_5\":\"6009\",\"col_6\":\"2020.02.01\",\"col_7\":\"Gauti\",\"col_8\":\"G7471\"},{\"col_1\":\"2019.11.13\",\"col_2\":\"Uyagh\",\"col_3\":\"A3317\",\"col_4\":\"Narriott\",\"col_5\":\"714\",\"col_6\":\"2019.11.14\",\"col_7\":\"Gauti\",\"col_8\":\"A5532\"}]");
					_travelDataDic.Add("shirley payten", "[{\"col_1\":\"2020.12.25\",\"col_2\":\"Glalos\",\"col_3\":\"G8752\",\"col_4\":\"Vesidence\",\"col_5\":\"0422\",\"col_6\":\"2020.12.26\",\"col_7\":\"Gauti\",\"col_8\":\"G2496\"},{\"col_1\":\"2020.11.12\",\"col_2\":\"Tawilah\",\"col_3\":\"A4154\",\"col_4\":\"Narriott\",\"col_5\":\"0719\",\"col_6\":\"2020.11.13\",\"col_7\":\"Gauti\",\"col_8\":\"A6480\"},{\"col_1\":\"2020.10.24\",\"col_2\":\"Aridru\",\"col_3\":\"A5546\",\"col_4\":\"Best Eastern\",\"col_5\":\"9909\",\"col_6\":\"2020.10.25\",\"col_7\":\"Gauti\",\"col_8\":\"A0971\"},{\"col_1\":\"2020.08.17\",\"col_2\":\"Aridru\",\"col_3\":\"A5546\",\"col_4\":\"Best Eastern\",\"col_5\":\"9909\",\"col_6\":\"2020.08.19\",\"col_7\":\"Gauti\",\"col_8\":\"A0971\"},{\"col_1\":\"2019.11.13\",\"col_2\":\"Uyagh\",\"col_3\":\"A3317\",\"col_4\":\"Narriott\",\"col_5\":\"713\",\"col_6\":\"2019.11.14\",\"col_7\":\"Gauti\",\"col_8\":\"A5532\"}]");
					_travelDataDic.Add("cloud shawn", "[{\"col_1\":\"2017.09.21\",\"col_2\":\"Dreg\",\"col_3\":\"A776\",\"col_4\":\"Best Eastern\",\"col_5\":\"0708\",\"col_6\":\"2017.09.22\",\"col_7\":\"Gauti\",\"col_8\":\"A3219\"},{\"col_1\":\"2016.07.31\",\"col_2\":\"Driord\",\"col_3\":\"T2153\",\"col_4\":\"Vesidence\",\"col_5\":\"1001\",\"col_6\":\"2016.08.02\",\"col_7\":\"Gauti\",\"col_8\":\"T0734\"},{\"col_1\":\"2016.04.01\",\"col_2\":\"Slutiarm\",\"col_3\":\"A7539\",\"col_4\":\"Narriott\",\"col_5\":\"1204\",\"col_6\":\"2016.04.02\",\"col_7\":\"Gauti\",\"col_8\":\"A9980\"}]");
				}
				return _travelDataDic;
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

		private void Start()
		{
			canvasGroup.alpha = 0f;
			canvasGroup2.alpha = 0f;
			queryButton.onClick.AddListener(Query);
			backButton.onClick.AddListener(Back);
			closeButton.onClick.AddListener(CloseDialog);
			_leftFrameRt = leftFrameImage.GetComponent<RectTransform>();
			_rightFrameRt = rightFrameImage.GetComponent<RectTransform>();
			_rightFrameRt.DOAnchorPos(new Vector2(7f, -8.5f), 0f);
			_leftFrameRt.DOAnchorPos(new Vector2(-6f, 0f), 0f);
			bgTransform.DOScale(new Vector3(0.01f, 0.4f), 0f);
			_loadingTip = I18N.instance.getValue("^110008_common_13");
			tipText.text = _loadingTip;
			for (int i = 0; i < canvasGroups.Count; i++)
			{
				canvasGroups[i].DOFade(0f, 0f);
			}
			canvasGroup.DOFade(1f, 0.2f).OnComplete(delegate
			{
				_leftFrameRt.DOAnchorPosY(leftFrameToPos.y, 0.2f);
				_rightFrameRt.DOAnchorPosY(rightFrameToPos.y, 0.2f);
				bgTransform.DOScaleY(1f, 0.2f).OnComplete(delegate
				{
					StartCoroutine("StartAnimation");
				});
			});
		}

		private void Query()
		{
			string text = matchInput.text;
			if (!(text == ""))
			{
				Object.Instantiate(Resources.Load<Sql3Dlc7>($"{DLCNameUtil.Instance.GetPrefabPathDLC(GameTypeEnum.DLC7)}dlc7_sql3"), base.transform.parent).Init(_tureName.ToLower().Trim(), text.ToLower().Trim(), TravelDataDic, base.gameObject);
				base.gameObject.SetActive(value: false);
			}
		}

		private void Back()
		{
			((GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetSqlDialogName()), base.transform.parent)).transform.parent.gameObject.SetActive(value: true);
			Hide();
		}

		private void CloseDialog()
		{
			Debug.Log("关闭");
			Hide();
		}

		private void Hide()
		{
			contentTransform.DOScale(Vector3.zero, 0.5f).OnComplete(delegate
			{
				Object.Destroy(base.gameObject);
			});
		}

		private void InitDBSecondStep()
		{
			if (GameManager.player.playerdata.dlc7Invades[1] == 2)
			{
				_isAdvanced = false;
			}
			_baseDataTextList = new List<Text>();
			canvasGroup2.transform.GetComponent<RectTransform>().DOSizeDelta(sizeList[_isAdvanced ? 1 : 0], 0f);
			canvasGroup2.transform.GetComponent<RectTransform>().DOScale(scaleList[_isAdvanced ? 1 : 0], 0f);
			avatarList[2].color = new Color(0.2f, 18f / 85f, 4f / 15f);
			for (int i = 0; i < baseDBGroups.Count; i++)
			{
				baseDBGroups[i].GetComponent<CanvasGroup>().DOFade(0f, 0f);
			}
			for (int j = 0; j < advancedDBGroups.Count; j++)
			{
				advancedDBGroups[j].GetComponent<CanvasGroup>().DOFade(0f, 0f);
			}
			canvasGroup2.gameObject.SetActive(value: false);
		}

		private void ShowDBAnimation()
		{
			canvasGroup2.DOFade(1f, 0.2f).OnComplete(delegate
			{
				canvasGroup2.transform.GetComponent<RectTransform>().DOScale(Vector3.one, 0.46f).OnComplete(delegate
				{
					StartCoroutine("ShowDBAnimation2");
					StartCoroutine("avatarAnimation");
				});
				leftFrameImage.GetComponent<RectTransform>().DOAnchorPos(leftFrameToPosList[_isAdvanced ? 1 : 0], 0.46f);
				rightFrameImage.GetComponent<RectTransform>().DOAnchorPos(rightFrameToPosList[_isAdvanced ? 1 : 0], 0.46f);
			});
		}

		private IEnumerator ShowDBAnimation2()
		{
			if (!_isAdvanced)
			{
				for (int i = 0; i < advancedDBGroups.Count; i++)
				{
					advancedDBGroups[i].SetActive(value: false);
				}
			}
			float interval = 0.45f / (float)(_isAdvanced ? (baseDBGroups.Count + advancedDBGroups.Count) : baseDBGroups.Count);
			for (int j = 0; j < baseDBGroups.Count; j++)
			{
				string text = baseContentTextList[j].text;
				baseContentTextList[j].text = "";
				baseContentTextList[j].DOText(text, 0f);
				baseDBGroups[j].GetComponent<CanvasGroup>().DOFade(1f, interval * 1.5f);
				yield return new WaitForSeconds(interval);
			}
			if (_isAdvanced)
			{
				List<Dictionary<string, string>> list = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(TravelDataDic[_tureName]);
				for (int k = 0; k < travelContentList.Count; k++)
				{
					Text[] componentsInChildren = travelContentList[k].GetComponentsInChildren<Text>();
					for (int l = 0; l < componentsInChildren.Length; l++)
					{
						if (l != 0)
						{
							Text text2 = componentsInChildren[l];
							if (k == 0)
							{
								text2.text = I18N.instance.getValue($"^110008_common_{26 + l}");
								Debug.Log(text2.text);
							}
							else if (k > list.Count)
							{
								text2.text = "";
							}
							else
							{
								Dictionary<string, string> dictionary = list[k - 1];
								text2.text = dictionary[$"col_{l}"];
							}
						}
					}
				}
				for (int j = 0; j < advancedDBGroups.Count; j++)
				{
					advancedDBGroups[j].GetComponent<CanvasGroup>().DOFade(1f, interval * 1.5f);
					yield return new WaitForSeconds(interval);
				}
			}
			float interval2 = 0.6f / (float)contentGroup.Count;
			for (int j = 0; j < contentGroup.Count; j++)
			{
				contentGroup[j].GetComponent<CanvasGroup>().DOFade(1f, interval2 * 1.5f);
				yield return new WaitForSeconds(interval2);
			}
			if (_items != null && _items.Length != 0)
			{
				GameManager.homeScene.notebook.AddNewItems(_items);
				collectedText.gameObject.SetActive(value: true);
			}
		}

		private IEnumerator avatarAnimation()
		{
			avatarList[0].GetComponent<RectTransform>().DOAnchorPosX(0f, 0.4f);
			yield return new WaitForSeconds(0.4f);
			avatarList[2].DOColor(Color.white, 0.2f);
			yield return new WaitForSeconds(0.2f);
			avatarList[2].DOFade(0f, 0.16f);
			avatarList[3].DOFade(0f, 0.16f);
		}

		public void InitData(string name, string extraInfo)
		{
			_tureName = name;
			_extraInfo = extraInfo;
			List<string[]> list = Result(_tureName, _extraInfo);
			if (list.Count == 0)
			{
				InitData(isSuccess: false);
				return;
			}
			_dataList = list[0];
			baseContentTextList[0].text = DbContent(_dataList[0]);
			baseContentTextList[1].text = DbContent(_dataList[6]);
			baseContentTextList[2].text = DbContent(_dataList[2]);
			baseContentTextList[3].text = DbContent(_dataList[1]);
			baseContentTextList[4].text = DbContent(_dataList[12]);
			baseContentTextList[5].text = DbContent(_dataList[4]);
			baseContentTextList[6].text = DbContent(_dataList[3]);
			baseContentTextList[7].text = DbContent(_dataList[5]);
			baseContentTextList[8].text = DbContent(_dataList[7]);
			baseContentTextList[9].text = DbContent(_dataList[11]);
			baseContentTextList[10].text = DbContent(_dataList[10]);
			baseContentTextList[11].text = DbContent(_dataList[9]);
			string text = _dataList[8];
			if (!string.IsNullOrEmpty(text))
			{
				string[] array = text.Split(';');
				List<string> list2 = new List<string>();
				List<string> itemlist = GameManager.player.playerdata.itemlist;
				foreach (string item in array)
				{
					if (!itemlist.Contains(item))
					{
						list2.Add(item);
					}
				}
				if (list2.Count > 0)
				{
					_items = list2.ToArray();
					collectedText.gameObject.SetActive(value: false);
				}
				else
				{
					collectedText.gameObject.SetActive(value: true);
				}
			}
			if (AvatarDic.ContainsKey(_dataList[0]))
			{
				avatarList[1].sprite = Resources.Load<Sprite>($"touxiang/{AvatarDic[_dataList[0]]}_1");
				avatarList[2].sprite = Resources.Load<Sprite>($"touxiang/{AvatarDic[_dataList[0]]}");
				InitData(isSuccess: true);
			}
			else
			{
				InitData(isSuccess: false);
			}
		}

		private string DbContent(string text)
		{
			string text2 = text;
			if (text2.EndsWith(".0"))
			{
				text2 = text2.Replace(".0", "");
			}
			if (text2.StartsWith("^"))
			{
				text2 = I18N.instance.getValue(text2);
			}
			if (string.IsNullOrEmpty(text2))
			{
				return "null";
			}
			return text2;
		}

		private List<string[]> Result(string inputVal, string otherVal)
		{
			new List<string[]>();
			return GameManager.sqlManager.SelectWherePersonTable(inputVal, otherVal);
		}

		private void InitData(bool isSuccess)
		{
			_isSuccess = isSuccess;
			if (!_isSuccess)
			{
				canvasGroup2.gameObject.SetActive(value: false);
				return;
			}
			if (!GameManager.player.playerdata.sqlFinishedNames.Contains(_tureName))
			{
				GameManager.player.playerdata.sqlFinishedNames.Add(_tureName);
			}
			InitDBSecondStep();
		}

		private IEnumerator StartAnimation()
		{
			float interval1 = 0.7f;
			bgTransform.DOScaleX(1f, interval1);
			_leftFrameRt.DOAnchorPosX(leftFrameToPos.x, interval1);
			_rightFrameRt.DOAnchorPosX(rightFrameToPos.x, interval1);
			yield return new WaitForSeconds(0.2f);
			float num = 0.1f;
			for (int i = 0; i < canvasGroups.Count; i++)
			{
				canvasGroups[i].DOFade(1f, num);
			}
			yield return new WaitForSeconds(interval1 - num);
			_isLoading = true;
			StartCoroutine("TextLoading");
			DOTween.To(() => _progress, delegate(int x)
			{
				_progress = x;
			}, 100, 2.4f).SetEase(Ease.Linear).OnUpdate(delegate
			{
				progressText.text = $"{_progress}%";
			});
			StartCoroutine("TopAnimation");
			StartCoroutine("CenterAnimation");
			yield return new WaitForSeconds(2f);
			StopCoroutine("TextLoading");
			resultText.text = I18N.instance.getValue(_isSuccess ? "^110008_common_15" : "^110008_common_16");
			resultImage.color = resultColorList[_isSuccess ? 1 : 0];
			resultImage.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
			for (int num2 = 0; num2 < centerProgressImages.Count; num2++)
			{
				centerProgressImages[num2].sprite = centerSpriteList[_isSuccess ? 1 : 0];
			}
			yield return new WaitForSeconds(0.3f);
			resultImage.GetComponent<CanvasGroup>().DOFade(0.5f, 0.5f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.5f);
			resultImage.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.5f);
			resultImage.GetComponent<CanvasGroup>().DOFade(0.5f, 0.5f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.5f);
			resultImage.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(0.5f);
			if (_isSuccess)
			{
				bgTransform.DOScale(Vector3.zero, 0.15f);
				canvasGroup2.gameObject.SetActive(value: true);
				ShowDBAnimation();
				yield break;
			}
			if (_tureName.Trim().ToLower() == "benjamin engel")
			{
				GameManager.UnlockAchievements("nosuchperson");
				if (!GameManager.player.playerdata.aiSpeakHistoryIds.Contains("3910021"))
				{
					DLCEventManager.Instance.NoticeAITalk("3910021");
				}
			}
			Back();
		}

		private IEnumerator TopAnimation()
		{
			int count = topProgressImages.Count;
			float topInterval = 2f / (float)count;
			WaitForSeconds waitForSeconds = new WaitForSeconds(topInterval);
			for (int i = 0; i < topProgressImages.Count; i++)
			{
				topProgressImages[i].DOFade(1f, topInterval).SetEase(Ease.Linear);
				yield return waitForSeconds;
			}
		}

		private IEnumerator CenterAnimation()
		{
			int count = centerProgressImages.Count;
			float topInterval = 2f / (float)count;
			WaitForSeconds waitForSeconds = new WaitForSeconds(topInterval);
			for (int i = 0; i < centerProgressImages.Count; i++)
			{
				centerProgressImages[i].DOFade(1f, topInterval * 2f).SetEase(Ease.Linear);
				yield return waitForSeconds;
			}
		}

		private IEnumerator TextLoading()
		{
			WaitForSeconds _loadingWaitInterval = new WaitForSeconds(0.3f);
			while (_isLoading)
			{
				for (int i = 0; i < 4; i++)
				{
					StringBuilder stringBuilder = new StringBuilder(_loadingTip);
					for (int j = 0; j < i; j++)
					{
						stringBuilder.Append(".");
					}
					tipText.text = stringBuilder.ToString();
					yield return _loadingWaitInterval;
				}
			}
		}
	}
}
