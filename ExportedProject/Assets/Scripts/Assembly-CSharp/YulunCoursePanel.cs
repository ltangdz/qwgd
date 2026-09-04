using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class YulunCoursePanel : MonoBehaviour
{
	public Image img_top;

	public Image img_bottom;

	public Image img_black;

	public Text txt_course;

	public string[] i18nstring;

	public int pos;

	public bool isstart;

	public YulunCourseManager parentPanel;

	public GameManager gameManager;

	public bool islast;

	public Image img_arrow;

	public int courseID;

	public GameObject goalDialog;

	private bool isLastPanel;

	public bool iscanclick = true;

	private void Start()
	{
		pos = 0;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
	}

	public void ShowCourse(float runtime = 0f, bool lastPanel = false, float wait = 0f)
	{
		isstart = true;
		isLastPanel = lastPanel;
		StartCoroutine(StartShowCourse(wait, runtime));
	}

	private IEnumerator StartShowCourse(float wait = 0f, float runtime = 0f)
	{
		yield return new WaitForSeconds(wait);
		img_top.transform.DOLocalMove(new Vector3(0f, 540f, 0f), runtime);
		img_bottom.transform.DOLocalMove(new Vector3(0f, -482f, 0f), runtime);
		yield return new WaitForSeconds(runtime);
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
		DeleteHighLight(goalDialog);
	}

	public void BeforeShow()
	{
	}

	public void Next()
	{
		if (iscanclick)
		{
			if (pos == 0)
			{
				AddHighLight(goalDialog);
			}
			if (ShowText() == 1)
			{
				HideCourse();
			}
		}
	}

	private void AddHighLight(GameObject highlightobject, bool isneedclick = false)
	{
		if (!(highlightobject == null) && !(highlightobject.GetComponent<Canvas>() != null))
		{
			highlightobject.AddComponent<Canvas>().overrideSorting = true;
			highlightobject.GetComponent<Canvas>().sortingOrder = 6;
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

	private void MoveArrow()
	{
		img_arrow.transform.DOLocalMoveY(-400f, 1f).OnComplete(delegate
		{
			img_arrow.transform.localPosition = new Vector2(img_arrow.transform.localPosition.x, -372f);
		}).SetLoops(-1);
	}

	public void HideCourse(bool isactive = true)
	{
		StartCoroutine(StartHideCourse(isactive));
	}

	private IEnumerator StartHideCourse(bool isactive)
	{
		if (isLastPanel)
		{
			img_black.gameObject.SetActive(value: false);
		}
		AfterHide();
		if (isLastPanel)
		{
			yield return new WaitForSeconds(0.5f);
			img_top.transform.DOLocalMove(new Vector3(0f, 755f, 0f), 0.2f);
			img_bottom.transform.DOLocalMove(new Vector3(0f, -634f, 0f), 0.2f);
		}
		gameManager.homeScene.logPanel.Open();
		if (isactive)
		{
			gameManager.CanShowSetting(-1);
			parentPanel.ShowCoursePanel(courseID + 1);
			if (courseID == 2)
			{
				gameManager.player.playerdata.isYulunCourse04 = 1;
				parentPanel.gameObject.SetActive(value: false);
			}
			base.gameObject.SetActive(value: false);
		}
	}
}
