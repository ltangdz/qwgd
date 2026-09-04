using System;
using UnityEngine;

namespace DLC7.DDOS
{
	public class DLCEventManager
	{
		private static DLCEventManager _instance;

		private GameManager _gameManager;

		public static DLCEventManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new DLCEventManager();
				}
				return _instance;
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

		public event Action onNoticeBackGame;

		public event Action onNoticeGameSuccess;

		public event Action onNoticeRefreshTool;

		public event Action<string> onNoticeAITalk;

		public event Action<bool> onNoticeShowAITalk;

		public void NoticeAITalk(string dialogId)
		{
			if (!GameManager.player.playerdata.aiSpeakGroupIds.Contains(dialogId))
			{
				GameManager.player.playerdata.aiWillSpeakGroupIds.Add(dialogId);
				if (this.onNoticeAITalk != null)
				{
					this.onNoticeAITalk(dialogId);
				}
			}
		}

		public void NoticeShowAITalk(bool isShow)
		{
			if (this.onNoticeShowAITalk != null)
			{
				this.onNoticeShowAITalk(isShow);
			}
		}

		public void NoticeGameSuccess()
		{
			if (this.onNoticeGameSuccess != null)
			{
				this.onNoticeGameSuccess();
			}
		}

		public void NoticeBackGame()
		{
			if (this.onNoticeBackGame != null)
			{
				this.onNoticeBackGame();
			}
		}

		public void NoticeRefreshTool()
		{
			if (this.onNoticeRefreshTool != null)
			{
				this.onNoticeRefreshTool();
			}
		}
	}
}
