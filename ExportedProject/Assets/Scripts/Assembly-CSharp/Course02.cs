using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Course02 : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public GameObject bk;

	public Image[] pics1;

	public GameObject[] txtGroups1;

	public Image[] pics2;

	public GameObject[] txtGroups2;

	private bool iscanclick;

	public GameObject txttip;

	public int pos;

	public GameObject[] pageGroup;

	public int couseid;

	public GameManager gameManager;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!iscanclick)
		{
			return;
		}
		if (pos == 0)
		{
			pos++;
			pageGroup[0].SetActive(value: false);
			pageGroup[1].SetActive(value: true);
			iscanclick = false;
			StartCoroutine(StartInit(pics2, txtGroups2, islast: true));
		}
		else
		{
			bk.GetComponent<CanvasGroup>().DOFade(0.2f, 0.2f);
			bk.transform.DOScale(Vector3.one, 0.3f).OnComplete(delegate
			{
				gameManager.player.playerdata.isTuli03 = 1;
				gameManager.CanShowSetting(-1);
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
		StartCoroutine(StartInit(pics1, txtGroups1, islast: false));
	}

	private IEnumerator StartInit(Image[] pics, GameObject[] txtGroups, bool islast)
	{
		if (!islast)
		{
			pageGroup[0].SetActive(value: true);
			pageGroup[1].SetActive(value: false);
			bk.GetComponent<CanvasGroup>().DOFade(1f, 0.1f);
			bk.transform.DOScale(Vector3.one, 0.1f);
			yield return new WaitForSeconds(0.3f);
		}
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
		txttip.GetComponent<I18NText>().updateTranslation2(islast ? "^coursetip01" : "^coursetip02");
		txttip.SetActive(value: true);
		gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	private void Update()
	{
	}
}
