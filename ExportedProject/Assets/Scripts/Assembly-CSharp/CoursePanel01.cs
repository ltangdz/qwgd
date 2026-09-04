using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class CoursePanel01 : MonoBehaviour
{
	public Image img_top;

	public Image img_bottom;

	public Image img_black;

	public Text txt_course;

	public string[] i18nstring;

	public int pos;

	public bool isstart;

	public GameObject parentPanel;

	public GameObject layer3Panel;

	public GameObject dragPanel;

	public int parenttype;

	public GameManager gameManager;

	public GameObject noteitem;

	public GameObject btn_browser;

	public GameObject browser_search;

	public GameObject img_arrow;

	public bool islast;

	public bool iscanclick = true;

	public bool isclickcopy;

	public bool isshowarrow;

	private bool ishide;

	private void Start()
	{
		pos = 0;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
	}

	public void ShowCourse(float wait = 0f)
	{
		isstart = true;
		parentPanel.transform.SetAsLastSibling();
		StartCoroutine(StartShowCourse(wait));
	}

	private IEnumerator StartShowCourse(float wait = 0f)
	{
		yield return new WaitForSeconds(wait);
		img_top.transform.DOLocalMove(new Vector3(0f, 540f, 0f), 0.2f);
		img_bottom.transform.DOLocalMove(new Vector3(0f, -482f, 0f), 0.2f);
		yield return new WaitForSeconds(0.5f);
		BeforeShow();
		img_black.gameObject.SetActive(value: true);
		Next();
	}

	public int ShowText()
	{
		if (!isstart)
		{
			return -1;
		}
		if (pos == i18nstring.Length)
		{
			return 1;
		}
		if (pos < i18nstring.Length)
		{
			txt_course.GetComponent<I18NText>().updateTranslation2(i18nstring[pos]);
			pos++;
		}
		if (pos > i18nstring.Length)
		{
			return 1;
		}
		return 0;
	}

	public void AfterHide()
	{
		DeleteHighLight(browser_search, isneedclick: true);
	}

	private void MoveArrow()
	{
		img_arrow.transform.DOLocalMoveY(-400f, 1f).OnComplete(delegate
		{
			img_arrow.transform.localPosition = new Vector2(img_arrow.transform.localPosition.x, -372f);
		}).SetLoops(-1);
	}

	public void BeforeShow()
	{
		noteitem.GetComponent<NoteItem>().SetHighLightItem(ihl: true);
		AddHighLight(noteitem, isneedclick: true);
	}

	public void ReshowBlack()
	{
		StartCoroutine(StartReshowBlack());
	}

	private IEnumerator StartReshowBlack()
	{
		txt_course.text = "";
		DeleteHighLight(btn_browser, isneedclick: true);
		img_arrow.SetActive(value: false);
		img_top.transform.DOLocalMove(new Vector3(0f, 540f, 0f), 0.2f);
		img_bottom.transform.DOLocalMove(new Vector3(0f, -482f, 0f), 0.2f);
		yield return new WaitForSeconds(0.3f);
		iscanclick = true;
		Next();
		img_black.raycastTarget = true;
	}

	public void HighLightBrowsersearch()
	{
		AddHighLight(browser_search, isneedclick: true);
	}

	public void HideBlack()
	{
		StartCoroutine(StartHideBlack());
	}

	private IEnumerator StartHideBlack()
	{
		noteitem.GetComponent<NoteItem>().SetHighLightItem(ihl: false);
		AddHighLight(btn_browser, isneedclick: true);
		btn_browser.transform.Find("img_search").GetComponent<Image>().color = Color.white;
		btn_browser.transform.Find("txt_content").GetComponent<Text>().color = Color.white;
		iscanclick = false;
		img_top.transform.DOLocalMove(new Vector3(0f, 755f, 0f), 0.2f);
		img_bottom.transform.DOLocalMove(new Vector3(0f, -634f, 0f), 0.2f);
		yield return new WaitForSeconds(0.3f);
		img_arrow.SetActive(value: true);
		MoveArrow();
	}

	public void Next()
	{
		if (iscanclick && (pos != 2 || isclickcopy))
		{
			if (pos == 3 && !ishide)
			{
				DeleteHighLight(noteitem, isneedclick: true);
				HideBlack();
				ishide = true;
			}
			else
			{
				ShowText();
			}
		}
	}

	private void AddHighLight(GameObject highlightobject, bool isneedclick = false)
	{
		if (!(highlightobject == null))
		{
			if (highlightobject.GetComponent<Canvas>() == null)
			{
				highlightobject.AddComponent<Canvas>().overrideSorting = true;
				highlightobject.GetComponent<Canvas>().sortingOrder = 3;
			}
			if (isneedclick)
			{
				highlightobject.AddComponent<GraphicRaycaster>();
			}
		}
	}

	private void DeleteHighLight(GameObject highlightobject, bool isneedclick = false)
	{
		if (!(highlightobject == null))
		{
			if (isneedclick)
			{
				Object.Destroy(highlightobject.GetComponent<GraphicRaycaster>());
			}
			if (highlightobject.GetComponent<GraphicRaycaster>() != null)
			{
				Object.Destroy(highlightobject.GetComponent<GraphicRaycaster>());
			}
			Object.Destroy(highlightobject.GetComponent<Canvas>());
		}
	}

	public void HideCourse(bool isactive = true)
	{
		StartCoroutine(StartHideCourse(isactive));
	}

	private IEnumerator StartHideCourse(bool isactive)
	{
		img_black.gameObject.SetActive(value: false);
		AfterHide();
		yield return new WaitForSeconds(0.5f);
		img_top.transform.DOLocalMove(new Vector3(0f, 755f, 0f), 0.2f);
		img_bottom.transform.DOLocalMove(new Vector3(0f, -634f, 0f), 0.2f);
		yield return new WaitForSeconds(0.3f);
		gameManager.player.playerdata.isCourse01 = 1;
		if (isactive)
		{
			gameManager.CanShowSetting(-1);
			base.gameObject.SetActive(value: false);
		}
	}
}
