using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class CoursePanel : MonoBehaviour
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

	public bool iscanclick = true;

	private void Start()
	{
		pos = 0;
	}

	public void ShowCourse(float wait = 0f)
	{
		isstart = true;
		parentPanel.transform.SetAsLastSibling();
		BeforeShow();
		StartCoroutine(StartShowCourse(wait));
	}

	private IEnumerator StartShowCourse(float wait = 0f)
	{
		yield return new WaitForSeconds(wait);
		img_top.transform.DOLocalMove(new Vector3(0f, 540f, 0f), 0.2f);
		img_bottom.transform.DOLocalMove(new Vector3(0f, -479f, 0f), 0.2f);
		yield return new WaitForSeconds(0.5f);
		img_black.gameObject.SetActive(value: true);
		ShowText();
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
	}

	public void Next()
	{
		if (iscanclick && ShowText() == 1)
		{
			HideCourse();
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
		if (isactive)
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
