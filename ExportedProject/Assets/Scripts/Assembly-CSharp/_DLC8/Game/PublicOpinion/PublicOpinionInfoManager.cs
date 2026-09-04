using System.Collections.Generic;
using System.Linq;
using AlubaExcelData.Container;
using AlubaExcelData.DataClass;
using UnityEngine;

namespace _DLC8.Game.PublicOpinion
{
	public class PublicOpinionInfoManager
	{
		public Dictionary<int, PublicOpinionInfo> titanData = new Dictionary<int, PublicOpinionInfo>();

		public Dictionary<int, PublicOpinionInfo> otherData = new Dictionary<int, PublicOpinionInfo>();

		public void Init()
		{
			BinaryDataManager instance = BinaryDataManager.Instance;
			if (!Application.isPlaying)
			{
				instance.InitData();
			}
			instance.InitData();
			titanData.Clear();
			otherData.Clear();
			Dictionary<int, AlubaExcelData.DataClass.PublicOpinion> dataDic = instance.GetTable<PublicOpinionContainer>().dataDic;
			for (int i = 0; i < dataDic.Values.Count; i++)
			{
				PublicOpinionInfo publicOpinionInfo = PublicOpinionInfo.CreatePublicOpinionInfo(dataDic.Values.ElementAt(i));
				if (publicOpinionInfo.type == 0)
				{
					otherData.Add(publicOpinionInfo.id, publicOpinionInfo);
				}
				else if (publicOpinionInfo.type > 0)
				{
					titanData.Add(publicOpinionInfo.id, publicOpinionInfo);
				}
			}
		}
	}
}
