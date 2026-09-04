using System.Collections;
using System.Collections.Generic;
using Aluba;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using _DLC8.Common;
using _DLC8.Game.DDOS;
using _DLC8.Main.Data;

namespace _DLC8.Main
{
	public class TitanTalkGroup : DDosMonoBehaviourDLC8
	{
		public TitanTalkItem talkItemPrefab;

		public TitanTalkTipItem talkTipItemPrefab;

		[FormerlySerializedAs("content")]
		public Transform talkContent;

		private RectTransform _talkContentRT;

		public RectTransform contentRT;

		private ArchiveData _archiveData;

		public ScrollRect scrollRect;

		public Button closeButton;

		public bool isCenter;

		private bool _isShowForce;

		private bool _isShow;

		private int _groupId;

		private List<TalkContentInfo> _curInfos = new List<TalkContentInfo>();

		private List<List<TalkContentInfo>> _infoStacks = new List<List<TalkContentInfo>>();

		private bool _isAddChatContent;

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

		private void Start()
		{
			_talkContentRT = talkContent.GetComponent<RectTransform>();
			_curInfos = ArchiveData.TalkContentInfos;
			StartCoroutine(ShowData(isInit: true));
			closeButton.onClick.AddListener(Close);
			InvokeRepeating("CheckNext", 0.5f, 0.5f);
		}

		private void Close()
		{
			if (!_isShowForce)
			{
				SingletonAutoMono<DLC8DataController>.GetInstance().PlaySound(DLC8SoundType.CLICK_BUTTON);
				DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.CLOSE_CHAT, isCenter ? 1 : 0);
				if (_groupId == 2310111 && !ArchiveData.HasPlayedEndMovie)
				{
					ArchiveData.HasPlayedEndMovie = true;
					DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.AUTO_SAVE, 0);
					DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.PLAY_END_MOVIE, 0);
				}
			}
		}

		public void CloseAnimation()
		{
			_isShow = false;
			GameObject o = base.gameObject;
			contentRT.DOScaleY(2f / contentRT.sizeDelta.y, 0.3f).OnComplete(delegate
			{
				contentRT.DOScaleX(0f, 0.3f).OnComplete(delegate
				{
					contentRT.DOScaleY(0f, 0f);
					o.transform.DOScale(0f, 0f);
				});
			});
		}

		public void ShowAnimation()
		{
			_isShow = true;
			base.gameObject.transform.DOScale(0f, 0.6f).OnComplete(delegate
			{
				base.gameObject.transform.DOScale(1f, 0f);
				contentRT.DOScale(0f, 0f);
				contentRT.DOScaleY(2f / contentRT.sizeDelta.y, 0f);
				contentRT.DOScaleX(2f / contentRT.sizeDelta.x, 0f);
				contentRT.DOScaleX(1f, 0.3f).OnComplete(delegate
				{
					contentRT.DOScaleY(1f, 0.3f).OnComplete(delegate
					{
					});
				});
			});
		}

		public void Show(List<TalkContentInfo> infos, bool isForce, int groupId)
		{
			_isShowForce = isForce;
			if (_isShowForce)
			{
				closeButton.interactable = false;
				if (_isShowForce == isCenter)
				{
					ShowAnimation();
				}
				else
				{
					CloseAnimation();
				}
			}
			_groupId = groupId;
			_infoStacks.Add(infos);
		}

		private IEnumerator ShowData(bool isInit)
		{
			if (_isShowForce)
			{
				yield return new WaitForSeconds(1f);
			}
			for (int i = 0; i < _curInfos.Count; i++)
			{
				TalkContentInfo talkContentInfo = _curInfos[i];
				if (talkContentInfo.isTip)
				{
					Object.Instantiate(talkTipItemPrefab, talkContent).Init(talkContentInfo);
				}
				else
				{
					Object.Instantiate(talkItemPrefab, talkContent).Init(talkContentInfo, isCenter);
				}
				if (!isInit && _isShow)
				{
					DLC8EventManager.Instance.GameManager.soundManager.PlaySound(55);
				}
				Canvas.ForceUpdateCanvases();
				Vector2 vector = new Vector2(0.5f, (_talkContentRT.sizeDelta.y < (float)(isCenter ? 697 : 400)) ? 1 : 0);
				_talkContentRT.anchorMin = vector;
				_talkContentRT.anchorMax = vector;
				_talkContentRT.pivot = vector;
				_talkContentRT.DOAnchorPosY(0f, 0f);
				yield return new WaitForSeconds(isInit ? 0f : Random.Range(1.6f, 2.5f));
			}
			_isShowForce = false;
			closeButton.interactable = true;
			_isAddChatContent = false;
		}

		private void CheckNext()
		{
			if (!_isAddChatContent && _infoStacks.Count > 0)
			{
				_isAddChatContent = true;
				_curInfos = _infoStacks[0];
				_infoStacks.RemoveAt(0);
				StartCoroutine(ShowData(isInit: false));
			}
		}
	}
}
