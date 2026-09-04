using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class YulunZNews : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	public float[,] pos;

	public RectTransform dragTfm;

	private bool running;

	private GameManager gameManager;

	private Vector3 startpos;

	private List<GameObject> filesList = new List<GameObject>();

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		pos = new float[5, 2]
		{
			{ 372.5f, -669f },
			{ 338.5f, -564f },
			{ 304.5f, -459f },
			{ 270.5f, -354f },
			{ 236.5f, -249f }
		};
		startpos = dragTfm.localPosition;
	}

	public void AddFile(YulunNewsInfo newsInfo)
	{
		if (!running)
		{
			FileMove(newsInfo);
		}
	}

	public void ClearFiles()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Object.Destroy(base.transform.GetChild(i).gameObject);
		}
		filesList.Clear();
	}

	private void SetDraggedPosition(PointerEventData eventData)
	{
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(dragTfm, eventData.position, eventData.pressEventCamera, out var worldPoint))
		{
			Vector3 position = worldPoint;
			dragTfm.position = position;
		}
	}

	private GameObject IsPointerOverUIObject(PointerEventData eventDataCurrentPosition)
	{
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, list);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].gameObject.tag == "yulunnewsbox")
			{
				return list[i].gameObject;
			}
		}
		return null;
	}

	private void FileMove(YulunNewsInfo newsInfo)
	{
		running = true;
		GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Dialog/Yulun/yulun_ffile"), base.transform);
		filesList.Add(gameObject);
		gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos[pos.GetLength(0) - filesList.Count, 0], pos[pos.GetLength(0) - filesList.Count, 1]);
		gameObject.GetComponent<YulunFFile>().Init(newsInfo);
		running = false;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (base.transform.childCount > 0 && !running)
		{
			dragTfm.gameObject.SetActive(value: true);
			base.transform.GetComponent<CanvasGroup>().DOKill();
			base.transform.GetComponent<CanvasGroup>().alpha = 1f;
			base.transform.GetComponent<CanvasGroup>().DOFade(0.4f, 0.2f);
			SetDraggedPosition(eventData);
			filesList[filesList.Count - 1].gameObject.SetActive(value: false);
			YulunFFile component = filesList[filesList.Count - 1].GetComponent<YulunFFile>();
			dragTfm.GetComponent<YulunDragFile>().Init(component.newsInfo, component.whiteIconList[component.iconIndex]);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (base.transform.childCount > 0 && !running)
		{
			SetDraggedPosition(eventData);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (base.transform.childCount > 0 && !running)
		{
			gameManager.soundManager.Stop();
			gameManager.soundManager.PlaySound(33);
			base.transform.GetComponent<CanvasGroup>().DOKill();
			base.transform.GetComponent<CanvasGroup>().alpha = 0.4f;
			base.transform.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
			GameObject gameObject = IsPointerOverUIObject(eventData);
			if (gameObject != null)
			{
				gameObject.GetComponent<YulunNewsChoiceBox>().AddNewFiles(filesList[filesList.Count - 1].GetComponent<YulunFFile>().newsInfo);
				Object.Destroy(filesList[filesList.Count - 1].gameObject);
				filesList.Remove(filesList[filesList.Count - 1]);
			}
			else
			{
				filesList[filesList.Count - 1].gameObject.SetActive(value: true);
			}
			dragTfm.localPosition = startpos;
			dragTfm.gameObject.SetActive(value: false);
		}
	}
}
