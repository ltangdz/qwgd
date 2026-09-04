using Aluba;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Game.PublicOpinion
{
	public class PublicOpinionNewsTitle : MonoBehaviour
	{
		public Text numberText;

		public Text infoText;

		public Transform content;

		private PublicOpinionNewsTitleInfo _info;

		private RectTransform _rt;

		public RectTransform RT => _rt;

		public PublicOpinionNewsTitleInfo Info => _info;

		public void Show(PublicOpinionNewsTitleInfo info)
		{
			_info = info;
			numberText.text = $"NO.{_info.rank}";
			infoText.text = string.Format("{0}", I18N.instance.getValue(_info.titleKey).Replace(" ", "\u00a0"));
		}

		public void SetRank(int rank)
		{
			_info.rank = rank;
			Show(_info);
			for (int i = 0; i < SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.NewsTitleList.Count; i++)
			{
				if (SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.NewsTitleList[i].titleKey == Info.titleKey)
				{
					SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.NewsTitleList[i].rank = rank;
				}
			}
			if (_info.type == 2)
			{
				if (_info.rank == 1)
				{
					DLC8EventManager.Instance.NoticeSpecialEvent(DLC8SpecialEvent.ALUBA_HIGHEST);
				}
				else if (_info.rank > 10)
				{
					DLC8EventManager.Instance.NoticeSpecialEvent(DLC8SpecialEvent.ALUBA_LOWEST);
				}
			}
			if (_info.type == 3 && _info.rank > 10)
			{
				DLC8EventManager.Instance.NoticeSpecialEvent(DLC8SpecialEvent.DANEL_LOWEST);
			}
		}
	}
}
