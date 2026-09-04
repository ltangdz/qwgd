using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class BrowserTieba : MonoBehaviour
{
	public List<string> replyInfoList;

	public Transform pinglun;

	public Transform content;

	[SerializeField]
	private Text txt_title;

	[SerializeField]
	private Text txt_content;

	[SerializeField]
	private Text txt_fatieren;

	[SerializeField]
	private Text txt_date;

	private void Start()
	{
		txt_title.text = I18N.instance.getValue("^livename42");
		txt_content.text = I18N.instance.getValue("^tieba_maryRS01");
		txt_fatieren.text = I18N.instance.getValue("^readit_MaryRS01");
		txt_date.text = "2021-10";
		SetInfo();
	}

	private void SetInfo()
	{
		for (int i = 0; i < replyInfoList.Count; i++)
		{
			string[] array = replyInfoList[i].Split(';');
			string key = array[0];
			string key2 = array[1];
			string key3 = array[2];
			if (i == 0)
			{
				pinglun.Find("pingluntitle").GetComponent<I18NText>().updateTranslation2(key);
				pinglun.Find("pingluninfo").GetComponent<I18NText>().updateTranslation2(key2);
				pinglun.Find("replyTime").GetComponent<I18NText>().updateTranslation2(key3);
			}
			else
			{
				Transform obj = Object.Instantiate(pinglun, content);
				obj.Find("pingluntitle").GetComponent<I18NText>().updateTranslation2(key);
				obj.Find("pingluninfo").GetComponent<I18NText>().updateTranslation2(key2);
				obj.Find("replyTime").GetComponent<I18NText>().updateTranslation2(key3);
			}
		}
	}
}
