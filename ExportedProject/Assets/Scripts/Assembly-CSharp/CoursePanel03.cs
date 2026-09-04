using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class CoursePanel03 : MonoBehaviour
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

	public GameObject tbPanel;

	public GameObject tbname;

	public ScrollRect tbscrollrect;

	public MultiplyTextRedImage nameredimage;

	public MultiplyTextRedImage tbnicknameredimage;

	public bool islast;

	public bool iscanclick1;

	public bool isshowtuli2;

	public bool iscanclose;

	private void Start()
	{
		pos = 0;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.iscancollect = false;
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
	}

	public void BeforeShow()
	{
		AddHighLight(tbPanel);
		iscanclick1 = true;
	}

	public void Next()
	{
		if (!iscanclick1)
		{
			return;
		}
		if (pos == 1)
		{
			iscanclick1 = false;
			DeleteHighLight(tbPanel);
			tbscrollrect.vertical = false;
			DOTween.To(() => tbscrollrect.content.localPosition, delegate(Vector3 x)
			{
				tbscrollrect.content.localPosition = x;
			}, new Vector3(0f, 258f, 0f), 0.5f).OnComplete(delegate
			{
				AddHighLight(tbname);
				iscanclick1 = true;
			});
			ShowText();
		}
		else if (pos == 2 && !isshowtuli2)
		{
			isshowtuli2 = true;
			gameManager.homeScene.courseManager.ShowTuli2();
		}
		else if (pos == 3 && iscanclose)
		{
			HideCourse();
			tbscrollrect.vertical = true;
		}
		else if (pos != 2 || !isshowtuli2)
		{
			ShowText();
		}
	}

	public void AddClick()
	{
		tbname.AddComponent<GraphicRaycaster>();
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

	public void HideCourse0()
	{
		StartCoroutine(StartHideCourse0());
	}

	private IEnumerator StartHideCourse0()
	{
		DeleteHighLight(tbname, isneedclick: true);
		img_black.gameObject.SetActive(value: false);
		AfterHide();
		yield return new WaitForSeconds(0.5f);
		img_top.transform.DOLocalMove(new Vector3(0f, 755f, 0f), 0.2f);
		img_bottom.transform.DOLocalMove(new Vector3(0f, -634f, 0f), 0.2f);
		yield return new WaitForSeconds(5f);
		txt_course.text = "";
		img_top.transform.DOLocalMove(new Vector3(0f, 540f, 0f), 0.2f);
		img_bottom.transform.DOLocalMove(new Vector3(0f, -482f, 0f), 0.2f);
		yield return new WaitForSeconds(0.5f);
		img_black.gameObject.SetActive(value: true);
		iscanclose = true;
		ShowText();
	}

	public void HideCourse()
	{
		StartCoroutine(StartHideCourse());
	}

	private IEnumerator StartHideCourse()
	{
		img_black.gameObject.SetActive(value: false);
		yield return new WaitForSeconds(0.5f);
		img_top.transform.DOLocalMove(new Vector3(0f, 755f, 0f), 0.2f);
		img_bottom.transform.DOLocalMove(new Vector3(0f, -634f, 0f), 0.2f);
		yield return new WaitForSeconds(0.3f);
		gameManager.player.playerdata.isCourse03 = 1;
		gameManager.CanShowSetting(-1);
		base.gameObject.SetActive(value: false);
	}
}
