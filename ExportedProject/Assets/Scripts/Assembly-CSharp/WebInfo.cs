using System.Collections.Generic;
using Honeti;
using LeonKim;
using UnityEngine;

public class WebInfo : MonoBehaviour
{
	private List<Sprite> headPor;

	private string[] webTitle;

	private string[] webInfo;

	private BaseLoopList bll;

	private void Start()
	{
		webTitle = new string[2] { "China <color=#ce4c32>bashers</color> can’t dictate US policy", "China bashers can’t <color=#ce4c32>dictate</color> US policy" };
		webInfo = new string[2] { "More than 100 <color=#ce4c32>Americans recently published an open</color> letter addressed to US President Donald Trump, calling on the US <color=#ce4c32>government to adhere to a tough policy against China.</color> Earlier in July, over 100 US experts signed an open letter addressed to Trump and members of the Congress titled ", "More than 100 Americans recently published an open letter addressed to US President Donald Trump, calling on the US government to adhere to a tough policy against China. Earlier in July, over 100 US experts signed an open letter addressed to Trump and members of the Congress titled" };
		bll = GetComponent<BaseLoopList>();
		bll.Init(BakFun);
		bll.ShowList(webTitle.Length);
	}

	public void BakFun(GameObject cell, int i)
	{
		cell.transform.Find("txt_webTitle").GetComponent<I18NText>().updateTranslation2(webTitle[i - 1]);
		cell.transform.Find("txt_webInfo").GetComponent<I18NText>().updateTranslation2(webInfo[i - 1]);
	}
}
