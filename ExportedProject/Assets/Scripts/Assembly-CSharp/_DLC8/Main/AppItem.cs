using Aluba;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Main
{
	public class AppItem : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
	{
		public Image iconImage;

		public Sprite[] iconSprites;

		public Sprite[] iconSelectedSprites;

		public Text nameText;

		public CityGameType gameType;

		private bool _isUnlock;

		private ArchiveData _archiveData;

		private bool _isWarning;

		private void Start()
		{
			Init();
		}

		private bool IsLock()
		{
			if (_isWarning)
			{
				if (gameType == CityGameType.PUBLIC_OPINION)
				{
					_archiveData.UnlockAppList[(int)gameType] = true;
					_isUnlock = true;
					return false;
				}
				return true;
			}
			if (!_isUnlock)
			{
				return true;
			}
			return false;
		}

		public void Refresh(bool isWarning)
		{
			_isWarning = isWarning;
			Init();
		}

		private void Init()
		{
			iconImage.sprite = iconSprites[(int)gameType];
			_archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			nameText.text = I18N.instance.getValue(SingletonAutoMono<DLC8DataController>.GetInstance().GetGameNameKey(gameType));
			_isUnlock = _archiveData.UnlockAppList[(int)gameType];
			if (!IsLock())
			{
				iconImage.color = Color.white;
				nameText.color = Color.white;
			}
			else
			{
				iconImage.color = Color.gray;
				nameText.color = Color.gray;
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!IsLock())
			{
				SingletonAutoMono<DLC8DataController>.GetInstance().PlaySound(DLC8SoundType.CLICK_BUTTON);
				SingletonAutoMono<DLC8DataController>.GetInstance().Controller.ClickApp(gameType);
				iconImage.sprite = iconSelectedSprites[(int)gameType];
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			IsLock();
		}

		public void ResetIcon()
		{
			iconImage.sprite = iconSprites[(int)gameType];
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (_isUnlock)
			{
				_ = _isWarning;
			}
		}

		private void Awake()
		{
			DLC8EventManager.Instance.onNoticeCommonEvent += NoticeCommonEvent;
		}

		private void OnDestroy()
		{
			DLC8EventManager.Instance.onNoticeCommonEvent -= NoticeCommonEvent;
		}

		private void NoticeCommonEvent(DLC8CommonEvent arg1, int arg2)
		{
			switch (arg1)
			{
			case DLC8CommonEvent.UNLOCK_APP:
				Debug.LogError("UNLOCK_APP:" + arg2 + "---" + (CityGameType)arg2);
				_isWarning = false;
				if (gameType == (CityGameType)arg2)
				{
					_archiveData.UnlockApp(gameType);
				}
				Init();
				break;
			case DLC8CommonEvent.SHOW_WARNING:
				_isWarning = arg2 == 1;
				Init();
				break;
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
		}

		public void OnPointerExit(PointerEventData eventData)
		{
		}

		public void Unlock()
		{
			_archiveData.UnlockApp(gameType);
			Init();
		}
	}
}
