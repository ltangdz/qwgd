using UnityEngine.UI;

namespace _DLC8.Game.DDOS
{
	public class CoinPanelDLC8 : DDosMonoBehaviourDLC8
	{
		public Text coinText;

		private void Start()
		{
			coinText.text = base.DdosManagerDlc8.Coin.ToString();
			base.DdosEventManagerDlc8.onNoticeAddCoin += NoticeAddCon;
			base.DdosEventManagerDlc8.onNoticChangeCoin += NoticeChangeCoin;
		}

		private void NoticeAddCon(int coinCount)
		{
			base.DdosManagerDlc8.Coin = base.DdosManagerDlc8.Coin + coinCount;
		}

		private void OnDestroy()
		{
			base.DdosEventManagerDlc8.onNoticChangeCoin -= NoticeChangeCoin;
			base.DdosEventManagerDlc8.onNoticeAddCoin -= NoticeAddCon;
		}

		private void NoticeChangeCoin()
		{
			coinText.text = base.DdosManagerDlc8.Coin.ToString();
		}

		private void Awake()
		{
		}
	}
}
