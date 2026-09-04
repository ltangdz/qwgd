using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class CoursePanel02 : MonoBehaviour
{
	public Image img_top;

	public Image img_bottom;

	public Image img_black;

	public Text txt_course;

	public string[] i18nstring;

	public int pos;

	public bool isstart;

	public GameObject parentPanel;

	public GameManager gameManager;

	public GameObject browserscrollview;

	public GameObject searchitem;

	public ScrollRect scrollRect;

	public bool islast;

	public bool iscanclick = true;

	private bool islaststep;

	private void Start()
	{
		pos = 0;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
	}

	public void ShowCourse(float wait = 0f)
	{
		isstart = true;
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

	private int ShowText()
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
		DeleteHighLight(searchitem, isneedclick: true);
		scrollRect.vertical = true;
		searchitem.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
	}

	public void BeforeShow()
	{
	}

	public void Next()
	{
		if (iscanclick)
		{
			if (pos != 0 && pos >= 2 && !islaststep)
			{
				DeleteHighLight(browserscrollview);
				scrollRect.vertical = false;
				AddHighLight(searchitem, isneedclick: true);
				islaststep = true;
			}
			ShowText();
		}
	}

	private void AddHighLight(GameObject highlightobject, bool isneedclick = false)
	{
		if (!(highlightobject == null))
		{
			highlightobject.AddComponent<Canvas>().overrideSorting = true;
			highlightobject.GetComponent<Canvas>().sortingOrder = 3;
			if (isneedclick)
			{
				highlightobject.AddComponent<GraphicRaycaster>();
			}
			if (searchitem.GetComponent<Image>() != null)
			{
				searchitem.GetComponent<Image>().color = Color.white;
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
		gameManager.player.playerdata.isCourse02 = 1;
		if (isactive)
		{
			gameManager.CanShowSetting(-1);
			base.gameObject.SetActive(value: false);
		}
	}
}
