using System;
using UnityEngine;
using _DLC8.Common;
using _DLC8.Game.PublicOpinion;

namespace _DLC8
{
	public class DLC8EventManager
	{
		private static DLC8EventManager _instance;

		private GameManager _gameManager;

		public static DLC8EventManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new DLC8EventManager();
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

		public event Action<PublicOpinionMap> onNoticeSelectedMap;

		public event Action<DLC8CommonEvent, int> onNoticeCommonEvent;

		public event Action<DLC8SpecialEvent> onNoticeSpecialEvent;

		public event Action<int> onNoticeTalkFinish;

		public event Action<LevelRecord> onNoticeControllerGameOver;

		public void NoticeControllerGameOver(LevelRecord result)
		{
			if (this.onNoticeControllerGameOver != null)
			{
				this.onNoticeControllerGameOver(result);
			}
		}

		public void NoticeSelectedMap(PublicOpinionMap map)
		{
			if (this.onNoticeSelectedMap != null)
			{
				this.onNoticeSelectedMap(map);
			}
		}

		public void NoticeCommonEvent(DLC8CommonEvent type, int videoGroupId)
		{
			if (this.onNoticeCommonEvent != null)
			{
				this.onNoticeCommonEvent(type, videoGroupId);
			}
		}

		public void NoticeSpecialEvent(DLC8SpecialEvent type)
		{
			if (this.onNoticeSpecialEvent != null)
			{
				this.onNoticeSpecialEvent(type);
			}
		}
	}
}
