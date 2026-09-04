using Aluba;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Main
{
	public class MainMapResourceGroup : MonoBehaviour
	{
		public Text text;

		private long _bugCount;

		private ArchiveData _archiveData;

		private void Awake()
		{
			DLC8EventManager.Instance.onNoticeCommonEvent += NoticeCommonEvent;
			DLC8EventManager.Instance.onNoticeControllerGameOver += NoticeControllerGameOver;
		}

		private void Start()
		{
			_archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			StartBalance();
		}

		private void StartBalance()
		{
			_bugCount = _archiveData.ResourceCount;
			text.text = _bugCount.ToString();
			DOTween.To(() => _bugCount, delegate(long x)
			{
				_bugCount = x;
			}, _archiveData.ResourceCount, 0.8f).SetEase(Ease.Linear).OnUpdate(delegate
			{
				text.text = _bugCount.ToString();
			});
		}

		private void OnDestroy()
		{
			DLC8EventManager.Instance.onNoticeCommonEvent -= NoticeCommonEvent;
			DLC8EventManager.Instance.onNoticeControllerGameOver -= NoticeControllerGameOver;
		}

		private void NoticeCommonEvent(DLC8CommonEvent arg1, int arg2)
		{
			if (arg1 == DLC8CommonEvent.UNLOCK_LEVEL || arg1 == DLC8CommonEvent.UNLOCK_MAP)
			{
				StartBalance();
			}
		}

		private void NoticeControllerGameOver(LevelRecord obj)
		{
			StartBalance();
		}
	}
}
