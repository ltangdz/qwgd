using System.Collections.Generic;
using UnityEngine;
using tnt_deploy;

public class MailBtn : MonoBehaviour
{
	public int btnType = -1;

	private List<DATA15> mailList;

	public void ResetList(int type)
	{
		if (btnType == -1)
		{
			btnType = type;
		}
		base.transform.Find("img_inbox").gameObject.SetActive(value: true);
		base.transform.Find("img_on").GetComponent<CanvasGroup>().alpha = 0f;
	}

	public void Focus()
	{
		base.transform.Find("img_on").GetComponent<CanvasGroup>().alpha = 1f;
	}

	public void AddMailData(List<DATA15> sendMailList)
	{
		mailList = sendMailList;
	}

	public List<DATA15> GetMailList()
	{
		return mailList;
	}
}
