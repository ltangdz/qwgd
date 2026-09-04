using System.Collections.Generic;
using System.Linq;
using AlubaExcelData.Container;
using AlubaExcelData.DataClass;

namespace _DLC8.Main.Data
{
	public class TalkGroupInfoManager
	{
		public Dictionary<int, TalkGroupInfo> talkGroupDic = new Dictionary<int, TalkGroupInfo>();

		public Dictionary<int, TalkContentInfo> talkContentDic = new Dictionary<int, TalkContentInfo>();

		public void Init()
		{
			BinaryDataManager instance = BinaryDataManager.Instance;
			talkGroupDic.Clear();
			talkContentDic.Clear();
			Dictionary<int, TalkContent> dataDic = instance.GetTable<TalkContentContainer>().dataDic;
			for (int i = 0; i < dataDic.Values.Count; i++)
			{
				TalkContentInfo talkContentInfo = TalkContentInfo.CreateInfo(dataDic.Values.ElementAt(i));
				talkContentDic.Add(talkContentInfo.id, talkContentInfo);
			}
			Dictionary<int, TalkGroup> dataDic2 = instance.GetTable<TalkGroupContainer>().dataDic;
			for (int j = 0; j < dataDic2.Values.Count; j++)
			{
				TalkGroupInfo talkGroupInfo = TalkGroupInfo.CreateInfo(dataDic2.Values.ElementAt(j), talkContentDic);
				talkGroupDic.Add(talkGroupInfo.id, talkGroupInfo);
			}
		}
	}
}
