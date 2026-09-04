using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabBtn : MonoBehaviour
{
	public List<Sprite> clickBtnBak;

	public List<Sprite> btnBak;

	public List<Button> btn;

	public List<GameObject> dialog;

	private void Start()
	{
		for (int i = 0; i < btn.Count; i++)
		{
			int x = i;
			btn[x].onClick.AddListener(delegate
			{
				BtnFun(btn[x], dialog[x]);
			});
		}
		BtnFun(btn[0], dialog[0]);
	}

	public void BtnFun(Button tab, GameObject showDialog)
	{
		for (int i = 0; i < btn.Count; i++)
		{
			Sprite sprite = null;
			dialog[i].SetActive(value: false);
			sprite = ((!(btn[i].name == base.transform.GetChild(0).name)) ? btnBak[1] : btnBak[0]);
			btn[i].GetComponent<Image>().sprite = sprite;
		}
		if (tab.name == base.transform.GetChild(0).name)
		{
			tab.GetComponent<Image>().sprite = clickBtnBak[0];
		}
		else
		{
			tab.GetComponent<Image>().sprite = clickBtnBak[1];
		}
		showDialog.SetActive(value: true);
	}
}
