using System;
using System.Collections.Generic;
using Aluba;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Card
{
	public class CompleteCard : BaseWorkCard
	{
		public List<GameObject> normalObjList;

		public List<GameObject> perfectObjList;

		public Text contentText;

		public Text dateText;

		public void Show(PrintPrefabType type, bool isPrint)
		{
			bool flag = type == PrintPrefabType.STAGE_CLEAR_PERFECT;
			base.RT.anchoredPosition = Vector2.zero;
			ArchiveData archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			string nickName = archiveData.NickName;
			contentText.text = (flag ? I18N.instance.getValue("^110009_common_55").Replace("{*Player*}", nickName).Replace("{*Title*}", I18N.instance.getValue("^110009_common_57")) : I18N.instance.getValue("^110009_common_56").Replace("{*Player*}", nickName));
			DateTime dateTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local).AddSeconds(archiveData.StageClearTimestamp);
			dateText.text = $"{dateTime.Day.ToString().PadLeft(2, '0')}/{dateTime.Month.ToString().PadLeft(2, '0')}/{dateTime.Year.ToString()}";
			Debug.LogError(" dateText.text:" + dateText.text);
			Debug.LogError("  contentText.text:" + contentText.text);
			for (int i = 0; i < normalObjList.Count; i++)
			{
				normalObjList[i].SetActive(!flag);
			}
			for (int j = 0; j < perfectObjList.Count; j++)
			{
				perfectObjList[j].SetActive(flag);
			}
		}
	}
}
