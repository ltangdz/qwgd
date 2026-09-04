using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBtn : MonoBehaviour
{
	public Sprite clickBtn;

	public List<GameObject> allBtn;

	private Sprite crtBak;

	private void Start()
	{
		crtBak = allBtn[0].transform.GetComponent<Image>().sprite;
	}

	public void MailBtn(GameObject btn)
	{
		ChangeBak(btn);
	}

	public void NewsBtn(GameObject btn)
	{
		ChangeBak(btn);
	}

	private void ChangeBak(GameObject btn)
	{
		for (int i = 0; i < allBtn.Count; i++)
		{
			allBtn[i].transform.GetComponent<Image>().sprite = crtBak;
		}
		btn.transform.GetComponent<Image>().sprite = clickBtn;
	}
}
