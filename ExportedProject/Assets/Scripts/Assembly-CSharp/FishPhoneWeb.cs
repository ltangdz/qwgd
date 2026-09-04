using Honeti;
using UnityEngine;
using tnt_deploy;

public class FishPhoneWeb : MonoBehaviour
{
	public GameObject content;

	public void Init(string[] filelist, GameManager gm)
	{
		for (int i = 0; i < filelist.Length; i++)
		{
			DATA35 dATA = gm.dataManager.dic35[filelist[i]];
			if (dATA.highlight.Substring(1) != "0")
			{
				Transform obj = Object.Instantiate(Resources.Load<Transform>("fishPhoneHistoryYes"), content.transform);
				obj.Find("txt_info").GetComponent<MultiplyText>().SetNewWidth(I18N.instance.getValue(dATA.fileinfo));
				obj.Find("txt_info").GetComponent<MultiplyText>().SetContent2(dATA.fileinfo, dATA.highlight.Substring(1), I18N.instance.getValue(dATA.fileinfo));
			}
			else
			{
				Object.Instantiate(Resources.Load<Transform>("fishPhoneHistoryNo"), content.transform).GetChild(1).GetComponent<I18NText>()
					.updateTranslation2(dATA.fileinfo);
			}
		}
	}
}
