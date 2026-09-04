using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class SelectGroup : MonoBehaviour
{
	public delegate void callback(int a);

	public List<Text> lists;

	public List<Button> selectlists;

	public Animator hGroup;

	public bool iscanclick = true;

	private GameManager gm;

	public int selectpos;

	private void Start()
	{
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		for (int i = 0; i < selectlists.Count; i++)
		{
			selectlists[i].onClick.AddListener(delegate
			{
				_ = iscanclick;
			});
		}
	}

	public void ShowSelect()
	{
		iscanclick = true;
		hGroup.Play("ani_selectgroup");
	}

	public void HideSelect()
	{
		if (iscanclick)
		{
			gm.soundManager.PlaySound(23);
		}
		iscanclick = false;
		hGroup.Play("ani_selectgroupclose");
		Invoke("HideSelectGroup", 1f);
	}

	private void HideSelectGroup()
	{
		for (int i = 0; i < selectlists.Count; i++)
		{
			selectlists[i].GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			selectlists[i].GetComponent<HoverColorChange>().enterObj.GetComponent<CanvasGroup>().alpha = 0f;
		}
		base.gameObject.SetActive(value: false);
	}

	public void SetSelect(string[] selects, callback call)
	{
		for (int i = 0; i < selects.Length; i++)
		{
			lists[i].transform.parent.gameObject.SetActive(value: true);
			lists[i].GetComponent<I18NText>().updateTranslation2(selects[i]);
		}
		for (int j = selects.Length; j < lists.Count; j++)
		{
			if (j >= lists.Count)
			{
				return;
			}
			lists[j].transform.parent.gameObject.SetActive(value: false);
		}
		ShowSelect();
	}
}
