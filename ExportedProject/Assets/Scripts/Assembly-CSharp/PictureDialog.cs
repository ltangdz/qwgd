using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PictureDialog : CustomDialog
{
	public Image img;

	public Button searchInfo;

	public Button searchSame;

	public Image searchLine;

	public List<GameObject> resultBox;

	private bool searching;

	public bool haveInfo;

	public bool haveSeachResult;

	public GameObject noinforPanel;

	public List<string> searchID;

	public GameObject imgDragArea;

	private void Start()
	{
		gameManager.homeScene.pictureDialog = this;
		searchInfo.onClick.AddListener(delegate
		{
			if (haveInfo)
			{
				if (!searching)
				{
					StartCoroutine(Search());
				}
			}
			else if (noinforPanel != null)
			{
				noinforPanel.SetActive(value: true);
				CancelInvoke("HideNoinfoPanel");
				Invoke("HideNoinfoPanel", 3f);
			}
		});
		searchSame.onClick.AddListener(delegate
		{
			if (haveSeachResult)
			{
				if (gameManager.homeScene.newbrowserDialog == null)
				{
					gameManager.homeScene.computerButtonBox.btn_search.SelectTool(2);
				}
				else if (gameManager.homeScene.newbrowserDialog.GetComponent<NewBrowserDialog>().isminimize)
				{
					gameManager.homeScene.newbrowserDialog.GetComponent<NewBrowserDialog>().ResumeMinimize();
				}
				else
				{
					gameManager.homeScene.newbrowserDialog.transform.SetAsLastSibling();
				}
				gameManager.homeScene.newbrowserDialog.AddImgSearchItem(searchID, img.sprite);
			}
			else if (noinforPanel != null)
			{
				noinforPanel.SetActive(value: true);
				CancelInvoke("HideNoinfoPanel");
				Invoke("HideNoinfoPanel", 3f);
			}
		});
	}

	private void HideNoinfoPanel()
	{
		noinforPanel.SetActive(value: false);
	}

	private IEnumerator Search()
	{
		int time = 0;
		searching = true;
		float imgHeight = img.GetComponent<RectTransform>().rect.height;
		float lineHeight = img.transform.Find("search_line").GetComponent<RectTransform>().rect.height;
		for (; time < 2; time++)
		{
			searchLine.transform.DOLocalMoveY((imgHeight - lineHeight) / 2f + lineHeight, 0.8f).SetEase(Ease.Linear).OnComplete(delegate
			{
				searchLine.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
			});
			yield return new WaitForSeconds(0.8f);
			searchLine.transform.DOLocalMoveY((0f - (imgHeight + lineHeight)) / 2f, 0.8f).SetEase(Ease.Linear).OnComplete(delegate
			{
				searchLine.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			});
			yield return new WaitForSeconds(0.8f);
		}
		gameManager.soundManager.PlaySound(25);
		for (int num = 0; num < resultBox.Count; num++)
		{
			resultBox[num].SetActive(value: true);
		}
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(bk.GetComponent<RectTransform>());
	}
}
