using System.Collections.Generic;
using DG.Tweening;
using DLC7.DDOS;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _DLC8.Game.Voice
{
	public class VoicePrintRoleDLC8 : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
	{
		public Image selectedImage;

		public Text nameText;

		private VoicePrintRoleModelDLC8 _curModelDlc8;

		public Image iconImage;

		public Sprite[] iconSprite;

		private bool _isSelected;

		private bool _canClick;

		private UnityAction<VoicePrintRoleDLC8> _clickCallBack;

		private VoicePrintEvent _eventManager;

		public VoicePrintEvent EventManager
		{
			get
			{
				if (_eventManager == null)
				{
					_eventManager = VoicePrintEvent.Instance;
				}
				return _eventManager;
			}
		}

		public VoicePrintRoleModelDLC8 CurModelDlc8 => _curModelDlc8;

		public void InitData(VoicePrintRoleModelDLC8 modelDlc8, bool isSelected, UnityAction<VoicePrintRoleDLC8> callback)
		{
			_clickCallBack = callback;
			_curModelDlc8 = modelDlc8;
			Selected(isSelected);
			selectedImage.DOFade(0f, 0f);
			if (modelDlc8 == null)
			{
				_canClick = false;
			}
			else
			{
				_canClick = true;
			}
			iconImage.gameObject.SetActive(_canClick);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (_canClick && _clickCallBack != null)
			{
				_clickCallBack(this);
			}
		}

		public void Selected(bool isSelected)
		{
			_isSelected = isSelected;
			iconImage.sprite = iconSprite[isSelected ? 1 : 0];
		}

		private void Awake()
		{
			TitanEventManager.Instance.onNoticeVoiceReset += NoticeVoiceReset;
		}

		private void NoticeVoiceReset()
		{
			if (_curModelDlc8 == null)
			{
				return;
			}
			List<VoicePrintModelDLC8> modelList = _curModelDlc8.modelList;
			if (modelList != null)
			{
				for (int i = 0; i < modelList.Count; i++)
				{
					modelList[i].isUsed = false;
				}
			}
		}

		private void OnDestroy()
		{
			TitanEventManager.Instance.onNoticeVoiceReset -= NoticeVoiceReset;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (_canClick)
			{
				selectedImage.DOFade(1f, 0f);
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (_canClick)
			{
				selectedImage.DOFade(0f, 0f);
			}
		}
	}
}
