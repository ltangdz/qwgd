using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvadeOpenLockBtn : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
	public InvadeOpenLock parobj;

	private Vector3 offset;

	private float yPosition;

	private float xPosition;

	private float zPosition;

	private bool openSce;

	private bool canDrag = true;

	private void Start()
	{
		xPosition = GetComponent<RectTransform>().position.x;
		yPosition = GetComponent<RectTransform>().position.y;
		zPosition = GetComponent<RectTransform>().position.z;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (canDrag)
		{
			xPosition = GetComponent<RectTransform>().position.x;
			yPosition = GetComponent<RectTransform>().position.y;
			zPosition = GetComponent<RectTransform>().position.z;
			RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out var worldPoint);
			offset = GetComponent<RectTransform>().position - worldPoint;
			Debug.Log(xPosition + " " + yPosition + " " + worldPoint.z);
			OnDragMove(eventData);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (canDrag)
		{
			GetComponent<RectTransform>().DOScale(new Vector3(1f, 1f, 1f), 0.1f);
			parobj.txtSuo.SetActive(value: false);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (canDrag)
		{
			OnDragMove(eventData);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!openSce && canDrag)
		{
			StartCoroutine(OpenFailed());
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (!openSce && canDrag)
		{
			StartCoroutine(OpenFailed());
		}
	}

	public void OnDragMove(PointerEventData eventData, bool endDrag = false)
	{
		if (!openSce && RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out var worldPoint))
		{
			if (worldPoint.x + offset.x > xPosition && worldPoint.x + offset.x < xPosition + 240f)
			{
				GetComponent<RectTransform>().position = new Vector3(worldPoint.x + offset.x, yPosition, zPosition);
			}
			if (worldPoint.x + offset.x >= xPosition + 238f)
			{
				StartCoroutine(OpenSuccess());
			}
		}
	}

	private IEnumerator OpenSuccess()
	{
		openSce = true;
		GetComponent<CanvasGroup>().DOFade(0.4f, 0.2f);
		yield return new WaitForSeconds(0.2f);
		GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
		base.transform.Find("img_icon").gameObject.SetActive(value: false);
		yield return new WaitForSeconds(0.1f);
		parobj.OpenSce();
	}

	private IEnumerator OpenFailed()
	{
		canDrag = false;
		GetComponent<RectTransform>().DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.1f);
		base.gameObject.SetActive(value: true);
		base.transform.Find("img_icon").gameObject.SetActive(value: true);
		base.gameObject.GetComponent<RectTransform>().DOLocalMoveX(-96f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		canDrag = true;
		parobj.txtSuo.SetActive(value: true);
	}
}
