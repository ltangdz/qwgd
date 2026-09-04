using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BrowserCampusTab : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public GameObject bottomLine;

	public GameObject openObj;

	public GameObject topShowObj;

	public BrowserCampus parObj;

	private GameManager gameManager;

	private bool choiced;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		GetComponent<Button>().onClick.AddListener(Focus);
	}

	public void Focus()
	{
		for (int i = 0; i < base.transform.parent.childCount; i++)
		{
			if (base.transform.parent.GetChild(i).name != base.name)
			{
				base.transform.parent.GetChild(i).GetComponent<BrowserCampusTab>().Blur();
			}
		}
		Canvas.ForceUpdateCanvases();
		parObj.GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;
		Canvas.ForceUpdateCanvases();
		choiced = true;
		bottomLine.SetActive(value: true);
		base.transform.GetChild(0).GetComponent<Text>().fontStyle = FontStyle.Bold;
		if (parObj.topShowObj == null || parObj.topShowObj.name != topShowObj.name)
		{
			if (parObj.topShowObj != null)
			{
				parObj.topShowObj.SetActive(value: false);
			}
			topShowObj.SetActive(value: true);
			parObj.topShowObj = topShowObj;
		}
		if (parObj.openObj == null || parObj.openObj != openObj)
		{
			if (parObj.openObj != null)
			{
				parObj.openObj.SetActive(value: false);
			}
			openObj.SetActive(value: true);
			parObj.openObj = openObj;
		}
	}

	public void Blur()
	{
		choiced = false;
		bottomLine.SetActive(value: false);
		base.transform.GetChild(0).GetComponent<Text>().fontStyle = FontStyle.Normal;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!choiced)
		{
			base.transform.GetChild(0).GetComponent<Text>().fontStyle = FontStyle.Bold;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!choiced)
		{
			base.transform.GetChild(0).GetComponent<Text>().fontStyle = FontStyle.Normal;
		}
	}
}
