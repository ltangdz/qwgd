namespace _DLC8.Game.PublicOpinion
{
	public class PublicOpinionNewsTitleInfo
	{
		public int rank;

		public string titleKey;

		public int type;

		public static PublicOpinionNewsTitleInfo Init(int rank, string titleKey, int type)
		{
			return new PublicOpinionNewsTitleInfo
			{
				rank = rank,
				titleKey = titleKey,
				type = type
			};
		}
	}
}
