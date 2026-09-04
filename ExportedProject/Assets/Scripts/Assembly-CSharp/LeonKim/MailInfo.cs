using System.Collections.Generic;
using Honeti;
using UnityEngine;

namespace LeonKim
{
	public class MailInfo : MonoBehaviour
	{
		private List<Sprite> headPor;

		private string[] mailName;

		private string[] mailTitle;

		private BaseLoopList bll;

		private void Start()
		{
			mailName = new string[2] { "QQQ公司", "QQQ公司" };
			mailTitle = new string[2] { "2019.1.1任务", "欢迎入职" };
			bll = GetComponent<BaseLoopList>();
			bll.Init(BakFun);
			bll.ShowList(mailName.Length);
		}

		public void BakFun(GameObject cell, int i)
		{
			cell.transform.Find("txt_mailName").GetComponent<I18NText>().updateTranslation2(mailName[i - 1]);
			cell.transform.Find("txt_mailTitle").GetComponent<I18NText>().updateTranslation2(mailTitle[i - 1]);
		}
	}
}
