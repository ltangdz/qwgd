using System;

namespace DLC7.DDOS
{
	public class TitanEventManager
	{
		private static TitanEventManager _instance;

		public static TitanEventManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new TitanEventManager();
				}
				return _instance;
			}
		}

		public event Action<int> onNoticeDocumentSuccess;

		public event Action<string> onNoticeShowReport;

		public event Action<int> onNoticeClickLeftPanel;

		public event Action onNoticeVoiceReset;

		public void NoticeVoiceReset()
		{
			if (this.onNoticeVoiceReset != null)
			{
				this.onNoticeVoiceReset();
			}
		}

		public void NoticeClickLeftPanel(int number)
		{
			if (this.onNoticeClickLeftPanel != null)
			{
				this.onNoticeClickLeftPanel(number);
			}
		}

		public void NoticeShowReport(string number)
		{
			if (this.onNoticeShowReport != null)
			{
				this.onNoticeShowReport(number);
			}
		}

		public void NoticeDocumentSuccess(int id)
		{
			if (this.onNoticeDocumentSuccess != null)
			{
				this.onNoticeDocumentSuccess(id);
			}
		}
	}
}
