using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class CoursePanel04 : MonoBehaviour
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

	public GameObject tbpic;

	public ScrollRect tbscrollrect;

	public bool islast;

	public List<float> maxoffy = new List<float>();

	public List<float> minoffy = new List<float>();

	public bool iscanclick = true;

	private void Start()
	{
		pos = 0;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
		gameManager.homeScene.newbrowserDialog.currenttab.browserPanel.GetComponent<SocialBrowser>().scrollRect.enabled = false;
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
		DeleteHighLight(tbpic, isneedclick: true);
	}

	public void BeforeShow()
	{
		int index = 0;
		if (I18N.instance.gameLang == LanguageCode.EN)
		{
			index = 0;
		}
		else if (I18N.instance.gameLang == LanguageCode.CN)
		{
			index = 1;
		}
		else if (I18N.instance.gameLang == LanguageCode.TC)
		{
			index = 2;
		}
		if (tbscrollrect.content.localPosition.y > maxoffy[index])
		{
			DOTween.To(() => tbscrollrect.content.localPosition, delegate(Vector3 x)
			{
				tbscrollrect.content.localPosition = x;
			}, new Vector3(0f, maxoffy[index], 0f), 0.29f).OnComplete(delegate
			{
				AddHighLight(tbpic, isneedclick: true);
			});
		}
		else if (tbscrollrect.content.localPosition.y < minoffy[index])
		{
			DOTween.To(() => tbscrollrect.content.localPosition, delegate(Vector3 x)
			{
				tbscrollrect.content.localPosition = x;
			}, new Vector3(0f, minoffy[index], 0f), 0.29f).OnComplete(delegate
			{
				AddHighLight(tbpic, isneedclick: true);
			});
		}
		else
		{
			AddHighLight(tbpic, isneedclick: true);
		}
	}

	public void Next()
	{
		if (iscanclick)
		{
			ShowText();
		}
	}

	private void AddHighLight(GameObject highlightobject, bool isneedclick = false)
	{
		if (!(highlightobject == null) && !(highlightobject.GetComponent<Canvas>() != null))
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
		if (highlightobject == null)
		{
			Debug.Log("high light object is null");
			return;
		}
		if (isneedclick)
		{
			Object.Destroy(highlightobject.GetComponent<GraphicRaycaster>());
		}
		Object.Destroy(highlightobject.GetComponent<Canvas>());
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
		gameManager.player.playerdata.isCourse04 = 1;
		if (gameManager.homeScene.newbrowserDialog != null)
		{
			gameManager.homeScene.newbrowserDialog.imgDragArea.SetActive(value: true);
		}
		if (gameManager.homeScene.pictureDialog != null)
		{
			gameManager.homeScene.pictureDialog.imgDragArea.SetActive(value: true);
		}
		gameManager.homeScene.courseManager.ShowTuli3();
		if (isactive)
		{
			gameManager.homeScene.newbrowserDialog.currenttab.browserPanel.GetComponent<SocialBrowser>().scrollRect.enabled = true;
			base.gameObject.SetActive(value: false);
			gameManager.CanShowSetting(-1);
		}
	}
}
