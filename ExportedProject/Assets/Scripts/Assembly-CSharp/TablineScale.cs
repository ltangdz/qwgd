using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TablineScale : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	public Transform tweenTarget;

	public string key;

	public int status;

	public bool showHover;

	public Vector3 hover = new Vector3(1.03f, 1.03f, 1.03f);

	public float hoverDuration = 0.2f;

	public bool showPressed;

	public Vector3 pressed = new Vector3(0.97f, 0.97f, 0.97f);

	public float pressedDuration = 0.2f;

	private Vector3 _mScale;

	private bool _mStarted;

	public Sprite[] sprites;

	private Image img;

	private Text txt;

	public Color[] colors;

	private bool _isPressed;

	private void Start()
	{
		if (!_mStarted)
		{
			_mStarted = true;
			if (tweenTarget == null)
			{
				tweenTarget = base.transform;
			}
			_mScale = new Vector3(1f, 1f, 1f);
			img = base.transform.Find("img").GetComponent<Image>();
			txt = base.transform.Find("txt").GetComponent<Text>();
		}
	}

	public void ResetMScale()
	{
		_mScale = new Vector3(1f, 1f, 1f);
	}

	public void ResetButton()
	{
		OnHover(isOver: false);
	}

	private void OnPress(bool isPressed)
	{
		_isPressed = isPressed;
		if (base.enabled && showPressed)
		{
			if (!_mStarted)
			{
				Start();
			}
			Vector3 endValue = (isPressed ? Vector3.Scale(_mScale, pressed) : ((EventSystem.current.currentSelectedGameObject == base.gameObject) ? Vector3.Scale(_mScale, hover) : _mScale));
			tweenTarget.DOScale(endValue, pressedDuration).SetEase(Ease.InOutQuad);
		}
	}

	private void OnHover(bool isOver)
	{
		if (base.enabled && showHover)
		{
			if (!_mStarted)
			{
				Start();
			}
			Vector3 b = new Vector3((tweenTarget.localScale.x > 0f) ? hover.x : (0f - hover.x), hover.y, hover.z);
			Vector3 vector = new Vector3((tweenTarget.localScale.x > 0f) ? _mScale.x : (0f - _mScale.x), _mScale.y, _mScale.z);
			Vector3 endValue = (isOver ? Vector3.Scale(_mScale, b) : vector);
			tweenTarget.DOScale(endValue, hoverDuration).SetEase(Ease.InOutQuad);
		}
	}

	public void SetStatus(int s)
	{
		img.sprite = sprites[s];
		txt.color = colors[s];
		status = s;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		OnHover(isOver: true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		OnHover(isOver: false);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		OnPress(isPressed: true);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		OnPress(isPressed: false);
	}
}
