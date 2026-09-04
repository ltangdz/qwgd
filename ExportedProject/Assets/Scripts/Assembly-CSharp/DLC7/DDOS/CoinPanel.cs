using UnityEngine.UI;

namespace DLC7.DDOS
{
	public class CoinPanel : DDosMonoBehaviour
	{
		public Text coinText;

		private void Start()
		{
			coinText.text = base.DdosManager.Coin.ToString();
			base.DdosEventManager.onNoticeAddCoin += NoticeAddCon;
			base.DdosEventManager.onNoticChangeCoin += NoticeChangeCoin;
		}

		private void NoticeAddCon(int coinCount)
		{
			base.DdosManager.Coin = base.DdosManager.Coin + coinCount;
		}

		private void OnDestroy()
		{
			base.DdosEventManager.onNoticChangeCoin -= NoticeChangeCoin;
			base.DdosEventManager.onNoticeAddCoin -= NoticeAddCon;
		}

		private void NoticeChangeCoin()
		{
			coinText.text = base.DdosManager.Coin.ToString();
		}

		private void Awake()
		{
		}
	}
}
