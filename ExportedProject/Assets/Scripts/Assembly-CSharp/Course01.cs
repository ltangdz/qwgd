using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Course01 : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public GameObject bk;

	public Image[] pics;

	public GameObject[] txtGroups;

	public bool iscanclick;

	public GameObject txttip;

	public int couseid;

	public GameManager gameManager;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!iscanclick)
		{
			return;
		}
		iscanclick = false;
		bk.GetComponent<CanvasGroup>().DOFade(0.2f, 0.2f);
		bk.transform.DOScale(Vector3.one, 0.3f).OnComplete(delegate
		{
			switch (couseid)
			{
			case 1:
				gameManager.homeScene.notebook.ShowFirst2();
				gameManager.player.playerdata.isTuli01 = 1;
				break;
			case 2:
				gameManager.iscancollect = true;
				gameManager.player.playerdata.isTuli02 = 1;
				gameManager.homeScene.courseManager.coursepanel03.AddClick();
				break;
			case 3:
				gameManager.player.playerdata.isTuli03 = 1;
				break;
			case 4:
				gameManager.player.playerdata.isTuli04 = 1;
				break;
			case 5:
				gameManager.player.playerdata.isTuli05 = 1;
				break;
			case 6:
				gameManager.player.playerdata.isTuli06 = 1;
				gameManager.player.playerdata.isCourse08 = 1;
				if (gameManager.player.playerdata.isCourse15 == 0)
				{
					gameManager.homeScene.courseManager.ShowCourse15();
				}
				break;
			case 7:
				gameManager.player.playerdata.isTuli07 = 1;
				if (gameManager.player.playerdata.isCourse10 == 0)
				{
					gameManager.homeScene.courseManager.ShowCourse10();
				}
				break;
			}
			gameManager.saveManager.SavePlayerData();
			gameManager.CanShowSetting(-1);
			base.gameObject.SetActive(value: false);
		});
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
	}

	public void Init()
	{
		StartCoroutine(StartInit());
	}

	private IEnumerator StartInit()
	{
		iscanclick = false;
		bk.GetComponent<CanvasGroup>().DOFade(1f, 0.1f);
		bk.transform.DOScale(Vector3.one, 0.1f);
		yield return new WaitForSeconds(0.3f);
		for (int i = 0; i < pics.Length; i++)
		{
			pics[i].transform.DOScale(Vector3.one, 0.3f);
			yield return new WaitForSeconds(0.3f);
			float totaltime = 0f;
			for (int j = 0; j < txtGroups[i].transform.childCount; j++)
			{
				float num = txtGroups[i].transform.GetChild(j).GetComponent<TypeEffectBKText>().Init();
				totaltime += num;
				yield return new WaitForSeconds(num);
			}
			yield return new WaitForSeconds(0.5f);
		}
		iscanclick = true;
		txttip.SetActive(value: true);
		gameManager.homeScene.eventsystem.SetActive(value: true);
	}
}
