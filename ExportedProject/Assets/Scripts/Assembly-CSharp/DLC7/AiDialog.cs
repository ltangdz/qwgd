using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DLC7.DDOS;
using Honeti;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using tnt_deploy;

namespace DLC7
{
	public class AiDialog : MonoBehaviour
	{
		public RectTransform scrollViewRT;

		public RectTransform aiRT;

		public Button maskButton;

		public Button changeButton;

		public GameObject content;

		public Text nameText;

		public RectTransform iconRT;

		private bool _isOpen;

		private bool _isAnimation;

		private Vector2[] aiSize = new Vector2[2]
		{
			new Vector2(392f, 570f),
			new Vector2(392f, 170f)
		};

		private Vector2[] scrollSize = new Vector2[2]
		{
			new Vector2(392f, 479.51f),
			new Vector2(392f, 92f)
		};

		private GameManager _gameManager;

		public CanvasGroup canvasGroup;

		private Dictionary<string, DATA39> _dic39;

		private Dictionary<string, DATA40> _dic40;

		private DATA39 _curData39;

		public Dictionary<string, DATA39> Dic39
		{
			get
			{
				if (_dic39 == null)
				{
					_dic39 = GameManager.dataManager.dic39;
				}
				return _dic39;
			}
		}

		public Dictionary<string, DATA40> Dic40
		{
			get
			{
				if (_dic40 == null)
				{
					_dic40 = GameManager.dataManager.dic40;
				}
				return _dic40;
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
			nameText.text = GameManager.player.playerdata.aiNameDlc7;
			changeButton.onClick.AddListener(ChangeDialog);
			iconRT.DOScaleY(_isOpen ? 1 : (-1), 0f);
			scrollViewRT.DOSizeDelta(_isOpen ? scrollSize[0] : scrollSize[1], 0f);
			aiRT.DOSizeDelta(_isOpen ? aiSize[0] : aiSize[1], 0f);
			maskButton.gameObject.SetActive(value: false);
			ShowHistory();
			canvasGroup.alpha = (GameManager.player.playerdata.videotiplist.Contains("3910001") ? 1 : 0);
		}

		private void ShowHistory()
		{
			_isOpen = false;
			DialogAnimation(null);
			List<string> aiSpeakHistoryIds = GameManager.player.playerdata.aiSpeakHistoryIds;
			maskButton.gameObject.SetActive(value: false);
			List<DATA40> list = new List<DATA40>();
			for (int i = 0; i < aiSpeakHistoryIds.Count; i++)
			{
				if (Dic40.ContainsKey(aiSpeakHistoryIds[i]))
				{
					list.Add(Dic40[aiSpeakHistoryIds[i]]);
				}
			}
			StartCoroutine(Talk(list, isHistory: true, null));
		}

		private void Show(bool isShow)
		{
			Debug.Log("AIDialogShow:" + isShow + "--name:" + GameManager.player.playerdata.aiNameDlc7);
			canvasGroup.alpha = (isShow ? 1 : 0);
			nameText.text = GameManager.player.playerdata.aiNameDlc7;
		}

		private void ChangeDialog()
		{
			if (!_isAnimation)
			{
				_isAnimation = true;
				_isOpen = !_isOpen;
				DialogAnimation(null);
			}
		}

		private void DialogAnimation(UnityAction finishCallback)
		{
			iconRT.DOScaleY(_isOpen ? 1 : (-1), 0f);
			float duration = 0.38f;
			scrollViewRT.DOSizeDelta(_isOpen ? scrollSize[0] : scrollSize[1], duration);
			aiRT.DOSizeDelta(_isOpen ? aiSize[0] : aiSize[1], duration).OnComplete(delegate
			{
				_isAnimation = false;
				if (finishCallback != null)
				{
					finishCallback();
				}
			});
		}

		private void AITalk(string obj)
		{
			Show(isShow: true);
			if (obj == "" || !Dic39.ContainsKey(obj))
			{
				canvasGroup.alpha = 1f;
				return;
			}
			GameManager.soundManager.PlaySound(55);
			maskButton.gameObject.SetActive(value: true);
			_curData39 = Dic39[obj];
			string[] array = _curData39.content.Substring(1).Split(';');
			List<DATA40> datas = new List<DATA40>();
			for (int i = 0; i < array.Length; i++)
			{
				if (Dic40.ContainsKey(array[i]))
				{
					datas.Add(Dic40[array[i]]);
				}
			}
			_isOpen = true;
			DialogAnimation(delegate
			{
				string[] items = new string[0];
				string text = Dic39[obj].itemid.Substring(1);
				if (!string.IsNullOrEmpty(text) && text != "0")
				{
					items = text.Split(';');
				}
				StartCoroutine(Talk(datas, isHistory: false, items));
			});
		}

		private IEnumerator Talk(List<DATA40> datas, bool isHistory, string[] items)
		{
			if (datas.Count == 0)
			{
				yield break;
			}
			float textSpeed = ((I18N.instance.gameLang == LanguageCode.EN) ? 20f : 10f);
			for (int i = 0; i < datas.Count; i++)
			{
				DATA40 dATA = datas[i];
				if (!GameManager.player.playerdata.aiSpeakHistoryIds.Contains(dATA.id.ToString()))
				{
					GameManager.player.playerdata.aiSpeakHistoryIds.Add(dATA.id.ToString());
				}
				float num = 0f;
				if (dATA.extra.Equals(""))
				{
					string value = I18N.instance.getValue(dATA.content);
					num = (float)value.Length / textSpeed;
					Object.Instantiate(Resources.Load<AiTalkContentText>($"{DLCNameUtil.Instance.GetPrefabPathDLC(GameTypeEnum.DLC7)}AiContentText"), content.transform).Say(value, isHistory ? 0f : num);
				}
				yield return new WaitForSeconds(isHistory ? 0f : (num + 1.2f));
			}
			maskButton.gameObject.SetActive(value: false);
			if (!isHistory)
			{
				TalkFinished();
				if (items != null && items.Length != 0)
				{
					GameManager.homeScene.notebook.AddNewItems(items);
				}
				GameManager.saveManager.SavePlayerData(isshowlogo: true, isForce: true);
			}
		}

		private void TalkFinished()
		{
			if (_curData39 != null)
			{
				PlayerData playerdata = GameManager.player.playerdata;
				if (!playerdata.aiSpeakGroupIds.Contains(_curData39.ID.ToString()))
				{
					playerdata.aiWillSpeakGroupIds.Clear();
					playerdata.aiSpeakGroupIds.Add(_curData39.ID.ToString());
				}
				if (_curData39.ID == 3910002)
				{
					playerdata.dlc7Invades[0] = 1;
				}
				else if (_curData39.ID == 3910022)
				{
					playerdata.dlc7Invades[1] = 1;
				}
			}
			ChangeDialog();
		}

		private void Awake()
		{
			DLCEventManager.Instance.onNoticeAITalk += AITalk;
			DLCEventManager.Instance.onNoticeShowAITalk += Show;
		}

		private void OnDestroy()
		{
			DLCEventManager.Instance.onNoticeAITalk -= AITalk;
			DLCEventManager.Instance.onNoticeShowAITalk -= Show;
		}
	}
}
