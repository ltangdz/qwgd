using Honeti;

namespace _DLC8.Main.Rank
{
	public class LaborerRankData
	{
		public int rank;

		public string nameString;

		public int score;

		public string scoreString;

		public void Init(int rank, string name, int score, bool isTime)
		{
			this.rank = rank;
			nameString = name;
			this.score = score;
			if (this.score <= 0)
			{
				scoreString = I18N.instance.getValue("^career_platform0303");
			}
			else if (isTime)
			{
				string arg = (score / 60).ToString().PadLeft(2, '0');
				string arg2 = (score % 60).ToString().PadLeft(2, '0');
				scoreString = $"{arg}'{arg2}\"";
			}
			else
			{
				scoreString = score.ToString();
			}
		}
	}
}
