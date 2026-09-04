using System.Collections;
using Aluba;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using _DLC8;

public class TeachDLC8 : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public RectTransform img_top;

	public RectTransform img_bottom;

	public Text txt_course;

	public string[] i18nstring;

	public int pos;

	public bool isstart;

	public GameObject parentPanel;

	public GameManager gameManager;

	public bool islast;

	public Image img_arrow;

	public GameObject highLightObj;

	private Sequence _arrowSequence;

	private UnityAction _clickCallback;

	private float _clickInterval;

	private float _extraTime;

	public bool iscanclick = true;

	private bool _hasArrow;

	private void Start()
	{
		pos = 0;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: false);
	}

	public void ShowCourse(GameObject highlightobject, bool hasArrow, string[] contentKey, UnityAction clickCallback, bool isneedclick = false, float extraTime = 0f)
	{
		_extraTime = extraTime;
		Debug.LogError("ShowCourse");
		pos = 0;
		_hasArrow = hasArrow;
		_clickInterval = 0f;
		if (highLightObj != null && highlightobject != highLightObj)
		{
			DeleteHighLight(highLightObj);
		}
		highLightObj = null;
		base.gameObject.SetActive(value: true);
		_clickCallback = clickCallback;
		i18nstring = contentKey;
		highLightObj = highlightobject;
		AddHighLight(highLightObj, isneedclick);
		isstart = true;
		StartCoroutine(StartShowCourse());
		MoveArrow();
	}

	private IEnumerator StartShowCourse(float wait = 0f)
	{
		yield return new WaitForSeconds(wait);
		if (pos < i18nstring.Length)
		{
			img_top.DOAnchorPosY(0f, 0.2f);
			img_bottom.DOAnchorPosY(0f, 0.2f);
			yield return new WaitForSeconds(0.2f);
		}
		else
		{
			img_top.DOAnchorPosY(130f, 0f);
			img_bottom.DOAnchorPosY(-130f, 0f);
		}
		ShowText();
	}

	public void OnlyShowText(string[] texts)
	{
		i18nstring = texts;
		isstart = true;
		pos = 0;
		highLightObj = null;
		img_top.DOAnchorPosY(0f, 0.2f);
		img_bottom.DOAnchorPosY(0f, 0.2f);
		ShowText();
	}

	private void ShowText()
	{
		if (isstart && pos != i18nstring.Length && pos < i18nstring.Length)
		{
			txt_course.GetComponent<I18NText>().updateTranslation2(i18nstring[pos]);
			pos++;
		}
	}

	public void HideText()
	{
		img_top.transform.DOLocalMove(new Vector3(0f, 755f, 0f), 0.2f);
		img_bottom.transform.DOLocalMove(new Vector3(0f, -634f, 0f), 0.2f);
	}

	public void AfterHide()
	{
		DeleteHighLight(highLightObj);
	}

	public void BeforeShow()
	{
	}

	private void AddHighLight(GameObject highlightobject, bool isneedclick = false)
	{
		if (!(highlightobject == null) && !(highlightobject.GetComponent<Canvas>() != null))
		{
			highlightobject.AddComponent<Canvas>().overrideSorting = true;
			highlightobject.GetComponent<Canvas>().sortingOrder = 10;
			if (isneedclick)
			{
				highlightobject.AddComponent<GraphicRaycaster>();
			}
		}
	}

	public void DeleteHighLight(GameObject highlightobject, bool isneedclick = false)
	{
		if (!(highlightobject == null))
		{
			highlightobject.TryGetComponent<GraphicRaycaster>(out var component);
			if ((bool)component)
			{
				Object.Destroy(component);
			}
			highlightobject.TryGetComponent<Canvas>(out var component2);
			if ((bool)component2)
			{
				Object.Destroy(component2);
			}
		}
	}

	private void MoveArrow()
	{
		if ((bool)highLightObj && _hasArrow)
		{
			img_arrow.transform.position = highLightObj.transform.position;
			RectTransform component = highLightObj.GetComponent<RectTransform>();
			RectTransform component2 = img_arrow.GetComponent<RectTransform>();
			Vector2 vector = new Vector2(0.5f, 0.5f);
			img_arrow.DOFade(1f, 0f);
			Vector2 sizeDelta = component.sizeDelta;
			component2.anchorMax = vector;
			component2.anchorMin = vector;
			component2.pivot = vector;
			float num = component2.anchoredPosition.y + sizeDelta.y / 2f + 5f + component2.sizeDelta.y / 2f;
			if (_arrowSequence != null)
			{
				_arrowSequence.Kill();
				_arrowSequence = null;
			}
			component2.DOAnchorPosY(num, 0f);
			_arrowSequence = DOTween.Sequence();
			_arrowSequence.Append(component2.DOAnchorPosY(num + 5f, 0.5f).SetEase(Ease.Linear));
			_arrowSequence.Append(component2.DOAnchorPosY(num, 0.5f).SetEase(Ease.Linear));
			_arrowSequence.SetLoops(-1).Play();
		}
		else
		{
			img_arrow.DOFade(0f, 0f);
			if (_arrowSequence != null)
			{
				_arrowSequence.Kill();
				_arrowSequence = null;
			}
		}
	}

	public void HideCourse(bool isactive = true, UnityAction callback = null)
	{
		StartCoroutine(StartHideCourse(isactive, callback));
	}

	private IEnumerator StartHideCourse(bool isactive, UnityAction callback)
	{
		Debug.LogError("StartHideCourse");
		AfterHide();
		yield return new WaitForSeconds(0.5f);
		img_top.DOAnchorPosY(130f, 0.3f);
		img_bottom.DOAnchorPosY(-130f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		highLightObj = null;
		img_arrow.DOFade(0f, 0f);
		_clickInterval = 0f;
		callback?.Invoke();
		if (!isactive)
		{
			base.gameObject.SetActive(value: false);
		}
		SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: true);
	}

	private void FixedUpdate()
	{
		_clickInterval += Time.deltaTime;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!(_clickInterval < 0.8f + _extraTime))
		{
			_clickCallback?.Invoke();
			_clickInterval = 0f;
		}
	}
}
