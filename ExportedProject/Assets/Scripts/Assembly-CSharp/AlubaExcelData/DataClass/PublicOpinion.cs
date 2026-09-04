using System;

namespace AlubaExcelData.DataClass
{
	[Serializable]
	public class PublicOpinion
	{
		public int ID;

		public int eventid;

		public string name;

		public int up;

		public int down;

		public int type;

		public string city;

		public string newsInfo;

		public string upFeedback;

		public string downFeedback;

		public int trollType;

		public int trollTrigger;

		public string barrage;
	}
}
