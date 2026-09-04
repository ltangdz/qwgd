using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScale : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public Button btn;

	public Transform tweenTarget;

	public bool showHover;

	public Vector3 hover = new Vector3(1.1f, 1.1f, 1.1f);

	public float hoverDuration = 0.2f;

	private Vector3 _mScale;

	private bool _mStarted;

	[SerializeField]
	private Text btn_txt;

	[SerializeField]
	private Image btn_icon;

	public void SetGray()
	{
		if (btn_txt != null)
		{
			btn_txt.color = new Color(1f, 1f, 1f, 0.5f);
		}
		if ((bool)btn_icon)
		{
			btn_icon.color = new Color(1f, 1f, 1f, 0.5f);
			base.enabled = false;
		}
	}

	public void SetWhite()
	{
		if (btn_txt != null)
		{
			btn_txt.color = Color.white;
		}
		if ((bool)btn_icon)
		{
			btn_icon.color = Color.white;
		}
	}

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

	private void OnHover(bool isOver)
	{
		if ((!(btn != null) || btn.interactable) && base.enabled && showHover)
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

	public void OnPointerEnter(PointerEventData eventData)
	{
		OnHover(isOver: true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		OnHover(isOver: false);
	}
}
