using System;
using System.Collections.Generic;
using Aluba;
using AlubaExcelData.DataClass;

namespace _DLC8.Main.Data
{
	[Serializable]
	public class TalkGroupInfo
	{
		public int id;

		public int eventId;

		public List<TalkContentInfo> contentList;

		public static TalkGroupInfo CreateInfo(TalkGroup group, Dictionary<int, TalkContentInfo> dialogContentDic)
		{
			TalkGroupInfo talkGroupInfo = new TalkGroupInfo();
			talkGroupInfo.id = group.id;
			talkGroupInfo.eventId = group.eventId;
			talkGroupInfo.contentList = new List<TalkContentInfo>();
			string[] array = group.content.Substring(1).Split(';');
			for (int i = 0; i < array.Length; i++)
			{
				int key = AlubaUtils.StringParseToInt(array[i]);
				talkGroupInfo.contentList.Add(dialogContentDic[key]);
			}
			return talkGroupInfo;
		}
	}
}
