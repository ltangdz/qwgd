using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LiveBroadingChatEnterBtn : MonoBehaviour
{
	public List<Image> circleList;

	public Button btnOpen;

	private GameManager gameManager;

	public float time = 3f;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.homeScene.liveBroadingchatEnterBtn = this;
		Invoke("Init", 2f);
	}

	private void Init()
	{
		GetComponent<CanvasGroup>().DOFade(1f, 3f).OnComplete(delegate
		{
			btnOpen.onClick.AddListener(delegate
			{
				if (gameManager.player.playerdata.livebroadinglefttime > 0)
				{
					if (!gameManager.homeScene.isloginzhibochat)
					{
						GameObject obj = Object.Instantiate(Resources.Load("Livebroadcasting/livebroadingchatLogin") as GameObject, gameManager.homeScene.middle);
						obj.GetComponent<LiveBroadingChatLogin>().Show();
						obj.name = "livebroadingchatLogin";
						gameManager.homeScene.isloginzhibochat = true;
					}
					else if (!gameManager.homeScene.middle.Find("livebroadingchatDialog"))
					{
						GameObject obj2 = Object.Instantiate(Resources.Load("Livebroadcasting/livebroadingchatDialog") as GameObject, gameManager.homeScene.middle);
						obj2.GetComponent<LiveBroadingChatBox>().Show();
						obj2.gameObject.name = "livebroadingchatDialog";
					}
				}
			});
		});
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
