using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4007Step03 : MonoBehaviour
{
	[SerializeField]
	private GameObject step01;

	[SerializeField]
	private GameObject step02;

	[SerializeField]
	private GameObject txt_name1;

	[SerializeField]
	private GameObject txt_name2;

	[SerializeField]
	private GameObject txt_name3;

	[SerializeField]
	private GameObject txt_name4;

	[SerializeField]
	private GameObject dragcloudgray01;

	[SerializeField]
	private GameObject dragcloudgray02;

	[SerializeField]
	private GameObject dragcloudgray03;

	[SerializeField]
	private GameObject dragcloudgray04;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Text txt_summry2;

	[SerializeField]
	private List<DragCloudItem> dragCloudItems = new List<DragCloudItem>();

	public ReasoningMiddle4007 reasoningMiddle4007;

	public int isover;

	private void Start()
	{
		btn_continue.onClick.AddListener(delegate
		{
			btn_continue.gameObject.SetActive(value: false);
			txt_name1.SetActive(value: false);
			txt_name2.SetActive(value: false);
			txt_name3.SetActive(value: false);
			txt_name4.SetActive(value: false);
			dragcloudgray01.SetActive(value: false);
			dragcloudgray02.SetActive(value: false);
			dragcloudgray03.SetActive(value: false);
			dragcloudgray04.SetActive(value: false);
			txt_summry2.DOText(I18N.instance.getValue("^tuili0463"), 1.5f).OnComplete(delegate
			{
				isover = 1;
			});
		});
	}

	private void Update()
	{
		if (isover == 1 && Input.anyKey)
		{
			isover = 2;
			step01.GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
			txt_summry2.gameObject.SetActive(value: false);
			step02.SetActive(value: true);
		}
	}

	public void Check1()
	{
		bool flag = true;
		for (int i = 0; i < dragCloudItems.Count; i++)
		{
			if (dragCloudItems[i].gameObject.activeSelf)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			btn_continue.gameObject.SetActive(value: true);
			btn_continue.GetComponent<Image>().DOFade(1f, 0.2f);
		}
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
