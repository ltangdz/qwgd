using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class YulunCourse : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public GameObject bk;

	public Image[] pics;

	public GameObject[] txtGroups;

	public bool iscanclick;

	public GameObject txttip;

	public YulunCourseManager yulunCourseManager;

	public int couseid;

	public GameManager gameManager;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (iscanclick)
		{
			iscanclick = false;
			bk.GetComponent<CanvasGroup>().DOFade(0.2f, 0.2f);
			bk.transform.DOScale(Vector3.one, 0.3f).OnComplete(delegate
			{
				yulunCourseManager.ShowCourse(couseid + 1);
				gameManager.saveManager.SavePlayerData();
				gameManager.CanShowSetting(-1);
				gameManager.player.playerdata.isYulunCourse01 = 1;
				base.gameObject.SetActive(value: false);
			});
		}
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

	private void Update()
	{
	}
}
