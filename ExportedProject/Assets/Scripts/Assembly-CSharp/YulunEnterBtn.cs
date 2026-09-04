using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class YulunEnterBtn : MonoBehaviour
{
	public List<Image> circleList;

	public Button btnOpen;

	private GameManager gameManager;

	public float time = 3f;

	private void Start()
	{
		if (btnOpen != null)
		{
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			gameManager.homeScene.yulunEnterBtn = this;
		}
		Invoke("Init", 2f);
	}

	private void Init()
	{
		if (btnOpen != null)
		{
			GetComponent<CanvasGroup>().DOFade(1f, 3f).OnComplete(delegate
			{
				btnOpen.onClick.AddListener(delegate
				{
					if (!gameManager.player.playerdata.videotiplist.Contains("3711111"))
					{
						Object.Instantiate(Resources.Load<GameObject>("Dialog/danielvideoDialog"), gameManager.homeScene.middle).GetComponent<DanielVideoDialog>().endLoadPrefab = "Dialog/Yulun/yulunDialog";
					}
					else
					{
						Object.Instantiate(Resources.Load<GameObject>("Dialog/Yulun/yulunDialog"), gameManager.homeScene.middle);
						Object.Destroy(base.gameObject);
					}
				});
			});
		}
		StartCoroutine(ShowLight());
	}

	private IEnumerator ShowLight()
	{
		int a = 0;
		while (true)
		{
			circleList[a].GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			circleList[a].GetComponent<CanvasGroup>().alpha = 1f;
			circleList[a].GetComponent<RectTransform>().DOScale(new Vector3(2f, 2f, 2f), time);
			circleList[a].GetComponent<CanvasGroup>().DOFade(0f, time);
			a = ((a + 1 < circleList.Count) ? (a + 1) : 0);
			yield return new WaitForSeconds(time / (float)circleList.Count);
		}
	}
}
