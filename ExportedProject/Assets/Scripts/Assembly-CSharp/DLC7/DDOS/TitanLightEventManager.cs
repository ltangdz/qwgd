using System;

namespace DLC7.DDOS
{
	public class TitanLightEventManager
	{
		private static TitanLightEventManager _instance;

		public static TitanLightEventManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new TitanLightEventManager();
				}
				return _instance;
			}
		}

		public event Action<int> onNoticeIdle;

		public event Action onNoticeStartGame;

		public event Action<int> onNoticeSuccess;

		public event Action<int> onNoticeFail;

		public event Action onNoticeResetGame;

		public event Action<int, bool> onNoticeSelectedResult;

		public void NoticeFail(int step)
		{
			if (this.onNoticeFail != null)
			{
				this.onNoticeFail(step);
			}
		}

		public void NoticeSuccess(int index)
		{
			if (this.onNoticeSuccess != null)
			{
				this.onNoticeSuccess(index);
			}
		}

		public void NoticeResetGame()
		{
			if (this.onNoticeResetGame != null)
			{
				this.onNoticeResetGame();
			}
		}

		public void NoticeStartGame()
		{
			if (this.onNoticeStartGame != null)
			{
				this.onNoticeStartGame();
			}
		}

		public void NoticeSelectedResult(int number, bool isSuccess)
		{
			if (this.onNoticeSelectedResult != null)
			{
				this.onNoticeSelectedResult(number, isSuccess);
			}
		}

		public void NoticeIdle(int number)
		{
			if (this.onNoticeIdle != null)
			{
				this.onNoticeIdle(number);
			}
		}
	}
}
