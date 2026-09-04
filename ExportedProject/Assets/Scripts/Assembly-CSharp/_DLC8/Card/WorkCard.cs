using System;
using Aluba;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Card
{
	public class WorkCard : BaseWorkCard
	{
		public GameObject frontGroup;

		public GameObject backGroup;

		public Text dateText;

		public Text nameText;

		public Text numberText;

		public Text departmentText;

		public void Show(PrintPrefabType type, bool isPrint)
		{
			bool flag = type == PrintPrefabType.WORK_CARD_FRONT;
			base.RT.anchoredPosition = Vector2.zero;
			frontGroup.SetActive(flag);
			backGroup.SetActive(!flag);
			ArchiveData archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			DateTime dateTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local).AddSeconds(archiveData.StageClearTimestamp);
			dateText.text = $"{dateTime.Month.ToString().PadLeft(2, '0')}/{dateTime.Day.ToString().PadLeft(2, '0')}/{dateTime.Year.ToString()}";
			nameText.text = string.Format("{0}:{1}", I18N.instance.getValue("^invade_file0245"), archiveData.NickName);
			numberText.text = string.Format("{0}:{1}", I18N.instance.getValue("^110009_common_25"), archiveData.IDNumber);
			departmentText.text = string.Format("{0}:{1}", I18N.instance.getValue("^110009_common_24"), I18N.instance.getValue("^110009_common_26"));
		}
	}
}
