using System;

namespace DLC7.Reasoning
{
	public class ReasoningManager
	{
		private static ReasoningManager _instance;

		public static ReasoningManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new ReasoningManager();
				}
				return _instance;
			}
		}

		public event Action<string> onRemoveAnswer;

		public event Action onNoticeReset;

		public event Action<int> onConfirmResultNotice;

		public event Action onEnterNextPageNotice;

		public event Action<int> onResetResultNotice;

		public event Action<string> onNoticeResult;

		public void NoticeResult(string id)
		{
			if (this.onNoticeResult != null)
			{
				this.onNoticeResult(id);
			}
		}

		public void ConfirmResult(int id)
		{
			if (this.onConfirmResultNotice != null)
			{
				this.onConfirmResultNotice(id);
			}
		}

		public void ResetResult(int id)
		{
			if (this.onResetResultNotice != null)
			{
				this.onResetResultNotice(id);
			}
		}

		public void EnterNextPage()
		{
			if (this.onEnterNextPageNotice != null)
			{
				this.onEnterNextPageNotice();
			}
		}

		public void RemoveAnswer(string titleKey)
		{
			if (this.onRemoveAnswer != null)
			{
				this.onRemoveAnswer(titleKey);
			}
		}

		public void NoticeReset()
		{
			if (this.onNoticeReset != null)
			{
				this.onNoticeReset();
			}
		}
	}
}
