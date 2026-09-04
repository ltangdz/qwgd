using System.Collections.Generic;
using Honeti;
using LeonKim;
using UnityEngine;

public class SearchInfo : MonoBehaviour
{
	private List<Sprite> headPor;

	private string[] webTitle;

	private string[] webInfo;

	public BaseLoopList bll;

	private void Start()
	{
		webTitle = new string[2] { "<color=#E70202>牵手网</color>--帮你找到最对的人", "招聘网--炒老板鱿鱼" };
		webInfo = new string[2] { "有合适女士推荐", "老板叨叨受不了？" };
		bll.Init(BakFun);
		bll.ShowList(webInfo.Length);
	}

	public void BakFun(GameObject cell, int i)
	{
		cell.transform.Find("txt_info").GetComponent<I18NText>().updateTranslation2(webInfo[i - 1]);
		cell.transform.Find("txt_title").GetComponent<I18NText>().updateTranslation2(webTitle[i - 1]);
	}
}
