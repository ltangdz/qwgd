using System;
using UnityEngine;

namespace DLC7.DDOS
{
	public class DDOSEventManager
	{
		private static DDOSEventManager _instance;

		public static DDOSEventManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new DDOSEventManager();
				}
				return _instance;
			}
		}

		public event Action<int, bool, GameObject> onNoticeWallInjured;

		public event Action<int> onNoticeWallHeal;

		public event Action<GameResult> onNoticeGameResult;

		public event Action<BuyResultType> onNoticeBuyFail;

		public event Action<int> onNoticeAddCoin;

		public event Action onNoticChangeCoin;

		public event Action<float> onNoticeChangeEnergy;

		public event Action<int> onNoticeLevel;

		public event Action<DdosSound> onNoticeSound;

		public void NoticeSound(DdosSound sound)
		{
			if (this.onNoticeSound != null)
			{
				this.onNoticeSound(sound);
			}
		}

		public void NoticeLevel(int level)
		{
			if (this.onNoticeLevel != null)
			{
				this.onNoticeLevel(level);
			}
		}

		public void NoticeChangeEnergy(float energy)
		{
			if (this.onNoticeChangeEnergy != null)
			{
				this.onNoticeChangeEnergy(energy);
			}
		}

		public void NoticChangeCoin()
		{
			if (this.onNoticChangeCoin != null)
			{
				this.onNoticChangeCoin();
			}
		}

		public void NoticeAddCoin(int coin)
		{
			if (this.onNoticeAddCoin != null)
			{
				this.onNoticeAddCoin(coin);
			}
		}

		public void NoticeBuyFail(BuyResultType type)
		{
			if (this.onNoticeBuyFail != null)
			{
				this.onNoticeBuyFail(type);
			}
		}

		public void NoticeWallInjured(int damaged, bool isEnemy, GameObject from)
		{
			if (this.onNoticeWallInjured != null)
			{
				this.onNoticeWallInjured(damaged, isEnemy, from);
			}
		}

		public void NoticeWallHeal(int maxHp)
		{
			if (this.onNoticeWallHeal != null)
			{
				this.onNoticeWallHeal(maxHp);
			}
		}

		public void NoticeGameResult(GameResult result)
		{
			if (this.onNoticeGameResult != null)
			{
				this.onNoticeGameResult(result);
			}
		}
	}
}
