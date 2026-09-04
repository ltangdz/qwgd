using System;
using AlubaExcelData.DataClass;

namespace _DLC8.Main.Data
{
	[Serializable]
	public class TalkContentInfo
	{
		public int id;

		public string name;

		public string avatar;

		public string content;

		public bool isTip;

		public static TalkContentInfo CreateInfo(TalkContent content)
		{
			return new TalkContentInfo
			{
				id = content.id,
				name = content.name,
				avatar = content.avatar,
				content = content.content,
				isTip = (content.isTip == "#1")
			};
		}
	}
}
