using System.Collections.Generic;
using DG.Tweening;
using DLC7.DDOS;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DLC7.Titan.Voice
{
	public class VoicePrintRole : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		public Image selectedImage;

		public Text nameText;

		private VoicePrintRoleModel _curModel;

		private bool _isSelected;

		private bool _canClick;

		private UnityAction<VoicePrintRole> _clickCallBack;

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

		public VoicePrintRoleModel CurModel => _curModel;

		public void InitData(VoicePrintRoleModel model, bool isSelected, UnityAction<VoicePrintRole> callback)
		{
			_clickCallBack = callback;
			_curModel = model;
			Selected(isSelected);
			if (model == null)
			{
				_canClick = false;
				nameText.text = "";
			}
			else
			{
				nameText.text = model.name;
				_canClick = true;
			}
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
			selectedImage.DOFade(_isSelected ? 1 : 0, 0f);
		}

		private void Awake()
		{
			TitanEventManager.Instance.onNoticeVoiceReset += NoticeVoiceReset;
		}

		private void NoticeVoiceReset()
		{
			if (_curModel == null)
			{
				return;
			}
			List<VoicePrintModel> modelList = _curModel.modelList;
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
	}
}
