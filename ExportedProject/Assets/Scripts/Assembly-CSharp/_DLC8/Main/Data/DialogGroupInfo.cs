using System.Collections.Generic;
using Aluba;
using AlubaExcelData.DataClass;

namespace _DLC8.Main.Data
{
	public class DialogGroupInfo
	{
		public int id;

		public int eventId;

		public List<DialogContentInfo> contentList;

		public static DialogGroupInfo CreateInfo(DialogGroup group, Dictionary<int, DialogContentInfo> dialogContentDic)
		{
			DialogGroupInfo dialogGroupInfo = new DialogGroupInfo();
			dialogGroupInfo.id = group.id;
			dialogGroupInfo.eventId = group.eventId;
			dialogGroupInfo.contentList = new List<DialogContentInfo>();
			string[] array = group.content.Substring(1).Split(';');
			for (int i = 0; i < array.Length; i++)
			{
				int key = AlubaUtils.StringParseToInt(array[i]);
				dialogGroupInfo.contentList.Add(dialogContentDic[key]);
			}
			return dialogGroupInfo;
		}
	}
}
