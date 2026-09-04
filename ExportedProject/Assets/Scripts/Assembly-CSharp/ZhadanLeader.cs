using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZhadanLeader : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	public ZhadanGroup prtObj;

	public Transform hideLine;

	private ZhadanDialog zhadanDialog;

	private void Start()
	{
		zhadanDialog = prtObj.prtObj;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.name == "zulan")
		{
			prtObj.StopMoving();
			StopAllCoroutines();
			collision.gameObject.SetActive(value: false);
		}
		else if (collision.name == base.name && !prtObj.isCanClick && prtObj.step != -1)
		{
			StopAllCoroutines();
			prtObj.ShowRedAni(collision.GetComponent<Image>(), GetComponent<Image>());
		}
		if (prtObj.door.Count != 0 && !prtObj.isCanClick && collision.name == "door")
		{
			if (collision.GetComponent<ZhadanDoor>().type == 0)
			{
				StopAllCoroutines();
				prtObj.ShowRedAni(collision.GetComponent<Image>(), GetComponent<Image>());
			}
			else
			{
				collision.gameObject.SetActive(value: false);
			}
		}
	}

	private IEnumerator ShowAni()
	{
		while (!zhadanDialog.isGameOver)
		{
			InitLine();
			hideLine.GetComponent<RectTransform>().DOScale(Vector3.one, 0.3f);
			hideLine.GetComponent<CanvasGroup>().DOFade(1f, 0.3f).OnComplete(delegate
			{
				HideLine();
			});
			yield return new WaitForSeconds(0.8f);
		}
	}

	private void HideLine()
	{
		for (int i = 0; i < hideLine.childCount; i++)
		{
			hideLine.GetChild(i).GetComponent<RectTransform>().DOSizeDelta(new Vector2(0f, 1.8f), 0.3f);
		}
	}

	private void InitLine()
	{
		for (int i = 0; i < hideLine.childCount; i++)
		{
			hideLine.GetChild(i).DOKill();
			hideLine.GetChild(i).GetComponent<RectTransform>().sizeDelta = new Vector2(44f, 1.8f);
		}
		hideLine.GetComponent<CanvasGroup>().alpha = 0f;
		hideLine.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 0.6f);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (prtObj.isCanClick)
		{
			StartCoroutine(ShowAni());
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (prtObj.isCanClick)
		{
			StopAllCoroutines();
			HideLine();
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (prtObj.isCanClick && !zhadanDialog.isGameOver)
		{
			LeaderClick();
			zhadanDialog.ChangeDoor();
			if (prtObj.prtObj.name.IndexOf("zhadan01") > -1 && base.transform.parent.Find("hand") != null)
			{
				base.transform.parent.Find("hand").gameObject.SetActive(value: false);
			}
		}
	}

	public void LeaderClick()
	{
		if (prtObj.isCanClick && !zhadanDialog.isGameOver)
		{
			StopAllCoroutines();
			InitLine();
			StartCoroutine(prtObj.LeaderMove());
			if (prtObj.haveSaveTeam)
			{
				prtObj.sameTeam.GetComponent<ZhadanGroup>().leader.GetComponent<ZhadanLeader>().LeaderClick();
			}
		}
	}
}
