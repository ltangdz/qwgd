using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LiveBroadingEnterBtn : MonoBehaviour
{
	public List<Image> circleList;

	public Button btnOpen;

	private GameManager gameManager;

	public float time = 3f;

	[SerializeField]
	private RectTransform img_top;

	[SerializeField]
	private RectTransform img_bottom;

	[SerializeField]
	private Image img_black;

	private bool _isShow;

	public void ShowCourse()
	{
		if (base.gameObject.GetComponent<Canvas>() == null)
		{
			base.gameObject.AddComponent<Canvas>().overrideSorting = true;
		}
		GetComponent<Canvas>().sortingOrder = 12;
		if (base.gameObject.GetComponent<GraphicRaycaster>() == null)
		{
			base.gameObject.AddComponent<GraphicRaycaster>();
		}
		gameManager.CanShowSetting(1);
		if (!gameManager.player.playerdata.showTitanButton)
		{
			img_black.gameObject.SetActive(value: true);
		}
		img_top.DOLocalMoveY(791f, 0.5f);
		img_bottom.DOLocalMoveY(-156f, 0.5f);
	}

	public void HideCourse()
	{
		img_black.gameObject.SetActive(value: false);
		gameManager.CanShowSetting(-1);
		img_top.DOLocalMoveY(1000f, 0.5f);
		img_bottom.DOLocalMoveY(-341f, 0.5f);
		Object.Destroy(GetComponent<GraphicRaycaster>());
		Object.Destroy(GetComponent<Canvas>());
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.homeScene.liveBroadingEnterBtn = this;
		Invoke("Init", 0.5f);
		if (!gameManager.player.playerdata.livesqlbtncourse || gameManager.player.playerdata.showTitanButton)
		{
			ShowCourse();
		}
		else
		{
			HideCourse();
		}
	}

	private void Init()
	{
		GetComponent<CanvasGroup>().DOFade(1f, 0.5f).OnComplete(delegate
		{
			btnOpen.onClick.AddListener(delegate
			{
				if (gameManager.Is_Dlc7())
				{
					if (!_isShow)
					{
						_isShow = true;
						if (gameManager.player.playerdata.dlc7Invades[2] == 2)
						{
							gameManager.musicManager.Stop();
							SceneManager.LoadSceneAsync("DDOS");
						}
						else
						{
							Object.Instantiate(Resources.Load<GameObject>($"{DLCNameUtil.Instance.GetPrefabPathDLC(GameTypeEnum.DLC7)}InvadeTitanLoading"), gameManager.homeScene.middle);
						}
					}
				}
				else
				{
					gameManager.player.playerdata.livesqlbtncourse = true;
					gameManager.saveManager.SavePlayerData();
					HideCourse();
					if (!gameManager.homeScene.middle.Find("dnaDialog"))
					{
						GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Livebroadcasting/dnaDialog"), gameManager.homeScene.middle);
						obj.name = "dnaDialog";
						obj.GetComponent<DNADialog>().Show();
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
