using DLC7.DDOS;
using UnityEngine;

namespace DLC7.SignalLight
{
	public abstract class TitanLightBase : MonoBehaviour
	{
		private TitanLightEventManager _eventManager;

		public TitanLightEventManager EventManager
		{
			get
			{
				if (_eventManager == null)
				{
					_eventManager = TitanLightEventManager.Instance;
				}
				return _eventManager;
			}
			set
			{
				_eventManager = value;
			}
		}

		protected abstract void NoticeIdle(int step);

		protected abstract void NoticeStartGame();

		protected abstract void NoticeSelectedResult(int curSelected, bool isSuccess);

		protected abstract void NoticeSuccess(int step);

		protected abstract void NoticeFail(int step);

		protected abstract void NoticeResetGame();

		private void Awake()
		{
			EventManager.onNoticeStartGame += NoticeStartGame;
			EventManager.onNoticeIdle += NoticeIdle;
			EventManager.onNoticeResetGame += NoticeResetGame;
			EventManager.onNoticeSuccess += NoticeSuccess;
			EventManager.onNoticeSelectedResult += NoticeSelectedResult;
			EventManager.onNoticeFail += NoticeFail;
		}

		private void OnDestroy()
		{
			EventManager.onNoticeStartGame -= NoticeStartGame;
			EventManager.onNoticeIdle -= NoticeIdle;
			EventManager.onNoticeResetGame -= NoticeResetGame;
			EventManager.onNoticeSuccess -= NoticeSuccess;
			EventManager.onNoticeSelectedResult -= NoticeSelectedResult;
			EventManager.onNoticeFail -= NoticeFail;
		}
	}
}
