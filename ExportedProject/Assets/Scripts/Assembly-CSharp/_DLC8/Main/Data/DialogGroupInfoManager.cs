using System.Collections.Generic;
using System.Linq;
using AlubaExcelData.Container;
using AlubaExcelData.DataClass;

namespace _DLC8.Main.Data
{
	public class DialogGroupInfoManager
	{
		public Dictionary<int, DialogGroupInfo> dialogGroupDic = new Dictionary<int, DialogGroupInfo>();

		public Dictionary<int, DialogContentInfo> dialogContentDic = new Dictionary<int, DialogContentInfo>();

		public void Init()
		{
			BinaryDataManager instance = BinaryDataManager.Instance;
			dialogGroupDic.Clear();
			dialogContentDic.Clear();
			Dictionary<int, DialogContent> dataDic = instance.GetTable<DialogContentContainer>().dataDic;
			for (int i = 0; i < dataDic.Values.Count; i++)
			{
				DialogContentInfo dialogContentInfo = DialogContentInfo.CreateInfo(dataDic.Values.ElementAt(i));
				dialogContentDic.Add(dialogContentInfo.id, dialogContentInfo);
			}
			Dictionary<int, DialogGroup> dataDic2 = instance.GetTable<DialogGroupContainer>().dataDic;
			for (int j = 0; j < dataDic2.Values.Count; j++)
			{
				DialogGroupInfo dialogGroupInfo = DialogGroupInfo.CreateInfo(dataDic2.Values.ElementAt(j), dialogContentDic);
				dialogGroupDic.Add(dialogGroupInfo.id, dialogGroupInfo);
			}
		}
	}
}
