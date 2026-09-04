using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class YulunNewsChoiceBox : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	public YulunNewsControlBox yulunNewsControlBox;

	public YulunNewsControl yulunNewsControl;

	public float[,] pos;

	public RectTransform dragTfm;

	public YulunNewsShuijunBox yulunNewsShuijunBox;

	public Text bottomText;

	private List<YulunNewsBox> newsList = new List<YulunNewsBox>();

	private Vector3 startpos;

	private string choiceType;

	private bool running;

	private GameManager gameManager;

	private void Awake()
	{
		startpos = dragTfm.localPosition;
		pos = new float[3, 4]
		{
			{ 0f, 0f, 1f, 1f },
			{ 0f, 31f, 0.95f, 0.4f },
			{ 0f, 62f, 0.9f, 0.2f }
		};
	}

	public void Init(List<YulunNewsInfo> newsInfo)
	{
		Debug.Log("拖动框：1");
		if (gameManager == null)
		{
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		}
		for (int i = 0; i < newsInfo.Count; i++)
		{
			YulunNewsBox yulunNewsBox = Object.Instantiate(Resources.Load<YulunNewsBox>("Dialog/Yulun/yulun_newsbox"), base.transform);
			yulunNewsBox.Init(newsInfo[i]);
			if (i < newsInfo.Count - 3)
			{
				Debug.Log("设置隐藏的卡片：1");
				yulunNewsBox.gameObject.SetActive(value: false);
				yulunNewsBox.GetComponent<RectTransform>().localScale = new Vector2(0.2f, 0.2f);
				Debug.Log("设置隐藏的卡片：2" + yulunNewsBox.GetComponent<RectTransform>().localScale.x + "****" + yulunNewsBox.GetComponent<RectTransform>().localScale.y);
			}
			else
			{
				int num = newsInfo.Count - i - 1;
				Debug.Log("设置隐藏的卡片：3  ****x位置：" + pos[num, 2]);
				yulunNewsBox.GetComponent<RectTransform>().localPosition = new Vector3(pos[num, 0], pos[num, 1], 0f);
				yulunNewsBox.GetComponent<RectTransform>().localScale = new Vector3(pos[num, 2], pos[num, 2], 1f);
				yulunNewsBox.GetComponent<CanvasGroup>().alpha = pos[num, 3];
			}
			newsList.Add(yulunNewsBox);
		}
		Debug.Log("拖动框：2");
		int penzi = newsList[newsList.Count - 1].newsInfo.penzi;
		int penziChufa = newsList[newsList.Count - 1].newsInfo.penziChufa;
		yulunNewsControl.boxPenzi.Init(penzi, penziChufa);
		Debug.Log("拖动框：3");
	}

	public void AddNewFiles(YulunNewsInfo newsInfo)
	{
		YulunNewsBox yulunNewsBox = Object.Instantiate(Resources.Load<YulunNewsBox>("Dialog/Yulun/yulun_newsbox"), base.transform);
		yulunNewsBox.Init(newsInfo);
		newsList.Add(yulunNewsBox);
		if (newsList.Count > 3)
		{
			newsList[newsList.Count - 4].gameObject.SetActive(value: false);
			newsList[newsList.Count - 4].GetComponent<RectTransform>().localScale = new Vector2(0.2f, 0.2f);
		}
		yulunNewsBox.GetComponent<RectTransform>().localPosition = new Vector3(pos[0, 0], pos[0, 1], 0f);
		yulunNewsBox.GetComponent<RectTransform>().localScale = new Vector3(pos[0, 2], pos[0, 2], 1f);
		yulunNewsBox.GetComponent<CanvasGroup>().alpha = pos[0, 3];
		yulunNewsControlBox.BakSlide(yulunNewsBox.newsInfo.newsid);
		int penzi = yulunNewsBox.newsInfo.penzi;
		int penziChufa = yulunNewsBox.newsInfo.penziChufa;
		yulunNewsControl.boxPenzi.Init(penzi, penziChufa);
		yulunNewsShuijunBox.choiceVal.GetComponent<I18NText>().updateTranslation2(yulunNewsBox.newsInfo.shuijunVal);
		yulunNewsShuijunBox.allShuijun += int.Parse(yulunNewsBox.newsInfo.shuijunVal);
		yulunNewsShuijunBox.Init();
		bottomText.GetComponent<I18NText>().updateTranslation2("^yulun_bottomlabel02");
		StartCoroutine(NiMove());
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
			if (list[i].gameObject.tag == "yulunFbox")
			{
				if (newsList[newsList.Count - 1].newsInfo.penziChufa == -1)
				{
					Debug.Log("喷子添加1");
					yulunNewsControlBox.yulunDialog.addPenziList.Add(newsList[newsList.Count - 1].newsInfo.penzi);
				}
				choiceType = "yulunFbox";
				yulunNewsControlBox.Slide("-1", newsList[newsList.Count - 1].newsInfo.newsid);
				newsList[newsList.Count - 1].newsInfo.slide = "-1";
				newsList[newsList.Count - 1].newsInfo.round = gameManager.dataManager.dic43[newsList[newsList.Count - 1].newsInfo.newsid].down.Split(';')[1];
				return list[i].gameObject;
			}
			if (list[i].gameObject.tag == "yulunZbox")
			{
				if (newsList[newsList.Count - 1].newsInfo.penziChufa == 1)
				{
					Debug.Log("喷子添加2");
					yulunNewsControlBox.yulunDialog.addPenziList.Add(newsList[newsList.Count - 1].newsInfo.penzi);
				}
				choiceType = "yulunZbox";
				yulunNewsControlBox.Slide("1", newsList[newsList.Count - 1].newsInfo.newsid);
				newsList[newsList.Count - 1].newsInfo.slide = "1";
				newsList[newsList.Count - 1].newsInfo.round = gameManager.dataManager.dic43[newsList[newsList.Count - 1].newsInfo.newsid].up.Split(';')[1];
				return list[i].gameObject;
			}
		}
		return null;
	}

	private IEnumerator Move()
	{
		running = true;
		for (int i = 0; i < newsList.Count; i++)
		{
			if (i >= newsList.Count - 3)
			{
				newsList[i].gameObject.SetActive(value: true);
				int num = newsList.Count - i - 1;
				newsList[i].GetComponent<RectTransform>().DOLocalMove(new Vector3(pos[num, 0], pos[num, 1], 0f), 0.2f);
				newsList[i].GetComponent<RectTransform>().DOScale(new Vector3(pos[num, 2], pos[num, 2], 1f), 0.2f);
				newsList[i].GetComponent<CanvasGroup>().DOFade(pos[num, 3], 0.2f);
			}
		}
		if (newsList.Count > 0)
		{
			int penzi = newsList[newsList.Count - 1].newsInfo.penzi;
			int penziChufa = newsList[newsList.Count - 1].newsInfo.penziChufa;
			yulunNewsControl.boxPenzi.Init(penzi, penziChufa);
		}
		else
		{
			yulunNewsControl.boxPenzi.Clear();
		}
		yield return new WaitForSeconds(0.2f);
		running = false;
	}

	private IEnumerator NiMove()
	{
		running = true;
		if (newsList.Count >= 2)
		{
			newsList[newsList.Count - 2].GetComponent<RectTransform>().DOLocalMove(new Vector3(pos[1, 0], pos[1, 1], 0f), 0.2f);
			newsList[newsList.Count - 2].GetComponent<RectTransform>().DOScale(new Vector3(pos[1, 2], pos[1, 2], 1f), 0.2f);
			newsList[newsList.Count - 2].GetComponent<CanvasGroup>().DOFade(pos[1, 3], 0.2f);
			if (newsList.Count >= 3)
			{
				newsList[newsList.Count - 3].GetComponent<RectTransform>().DOLocalMove(new Vector3(pos[2, 0], pos[2, 1], 0f), 0.2f);
				newsList[newsList.Count - 3].GetComponent<RectTransform>().DOScale(new Vector3(pos[2, 2], pos[2, 2], 1f), 0.2f);
				newsList[newsList.Count - 3].GetComponent<CanvasGroup>().DOFade(pos[2, 3], 0.2f);
			}
		}
		yield return new WaitForSeconds(0.2f);
		running = false;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (base.transform.childCount > 0 && !running)
		{
			dragTfm.gameObject.SetActive(value: true);
			base.transform.parent.GetComponent<CanvasGroup>().DOKill();
			base.transform.parent.GetComponent<CanvasGroup>().alpha = 1f;
			base.transform.parent.GetComponent<CanvasGroup>().DOFade(0.4f, 0.2f);
			SetDraggedPosition(eventData);
			newsList[newsList.Count - 1].gameObject.SetActive(value: false);
			float choicedShuijun = yulunNewsControl.boxShuiJun.choicedShuijun;
			newsList[newsList.Count - 1].GetComponent<YulunNewsBox>().newsInfo.shuijunVal = choicedShuijun.ToString();
			dragTfm.GetComponent<YulunDragFile>().Init(newsList[newsList.Count - 1].GetComponent<YulunNewsBox>().newsInfo, newsList[newsList.Count - 1].GetComponent<YulunNewsBox>().cityIcon.sprite);
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
		if (base.transform.childCount <= 0 || running)
		{
			return;
		}
		gameManager.soundManager.Stop();
		gameManager.soundManager.PlaySound(33);
		base.transform.parent.GetComponent<CanvasGroup>().DOKill();
		base.transform.parent.GetComponent<CanvasGroup>().alpha = 0.4f;
		base.transform.parent.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
		GameObject gameObject = IsPointerOverUIObject(eventData);
		if (gameObject != null)
		{
			if (choiceType == "yulunFbox")
			{
				gameObject.GetComponent<YulunFNews>().AddFile(newsList[newsList.Count - 1].newsInfo);
			}
			else
			{
				gameObject.GetComponent<YulunZNews>().AddFile(newsList[newsList.Count - 1].newsInfo);
			}
			Object.Destroy(newsList[newsList.Count - 1].gameObject);
			newsList.Remove(newsList[newsList.Count - 1]);
			StartCoroutine(Move());
			yulunNewsControl.boxShuiJun.choicedShuijun = 0f;
			yulunNewsControl.boxShuiJun.Init();
			if (newsList.Count <= 0)
			{
				yulunNewsShuijunBox.btnAdd.interactable = false;
				yulunNewsShuijunBox.btnResume.interactable = false;
				bottomText.GetComponent<I18NText>().updateTranslation2("^yulun_bottomlabel01");
			}
		}
		else
		{
			newsList[newsList.Count - 1].gameObject.SetActive(value: true);
		}
		dragTfm.localPosition = startpos;
		dragTfm.gameObject.SetActive(value: false);
	}
}
