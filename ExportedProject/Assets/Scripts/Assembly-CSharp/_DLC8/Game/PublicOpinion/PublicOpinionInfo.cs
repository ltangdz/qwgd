using AlubaExcelData.DataClass;
using CodeStage.AntiCheat.ObscuredTypes;

namespace _DLC8.Game.PublicOpinion
{
	public class PublicOpinionInfo
	{
		public int id;

		public int eventId;

		public string name;

		public int type;

		public string city;

		public string newsInfo;

		public string upFeedback;

		public string downFeedback;

		public int trollType;

		public int trollTrigger;

		public string[] barrageList;

		public ObscuredInt roleNum;

		public int up;

		public int down;

		public PositionType positionType;

		public bool IsCorrect()
		{
			if (up != 1 || positionType != PositionType.UP)
			{
				if (down == 1)
				{
					return positionType == PositionType.DOWN;
				}
				return false;
			}
			return true;
		}

		public static PublicOpinionInfo CreatePublicOpinionInfo(AlubaExcelData.DataClass.PublicOpinion model)
		{
			PublicOpinionInfo publicOpinionInfo = new PublicOpinionInfo();
			publicOpinionInfo.id = model.ID;
			publicOpinionInfo.eventId = model.eventid;
			publicOpinionInfo.name = model.name;
			publicOpinionInfo.type = model.type;
			publicOpinionInfo.up = model.up;
			publicOpinionInfo.down = model.down;
			publicOpinionInfo.city = model.city;
			publicOpinionInfo.newsInfo = model.newsInfo;
			publicOpinionInfo.upFeedback = model.upFeedback;
			publicOpinionInfo.downFeedback = model.downFeedback;
			publicOpinionInfo.trollType = model.trollType;
			publicOpinionInfo.trollTrigger = model.trollTrigger;
			publicOpinionInfo.barrageList = model.barrage.Split(';');
			return publicOpinionInfo;
		}
	}
}
