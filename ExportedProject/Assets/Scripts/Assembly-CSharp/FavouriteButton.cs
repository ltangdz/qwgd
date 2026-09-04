using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class FavouriteButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public Transform img_tip;

	public Transform tweenTarget;

	public bool showHover;

	public Vector3 hover = new Vector3(1.1f, 1.1f, 1.1f);

	public float hoverDuration = 0.2f;

	private Vector3 _mScale;

	private bool _mStarted;

	private void OnEnable()
	{
		ResetButton();
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
		if (base.enabled && showHover)
		{
			if (!_mStarted)
			{
				Start();
			}
			Vector3 b = new Vector3((tweenTarget.localScale.x > 0f) ? hover.x : hover.x, hover.y, hover.z);
			Vector3 vector = new Vector3((tweenTarget.localScale.x > 0f) ? _mScale.x : _mScale.x, _mScale.y, _mScale.z);
			Vector3 endValue = (isOver ? Vector3.Scale(_mScale, b) : vector);
			tweenTarget.DOScale(endValue, hoverDuration).SetEase(Ease.InOutQuad);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (img_tip != null)
		{
			img_tip.gameObject.SetActive(value: true);
		}
		img_tip.DOKill();
		img_tip.localPosition = new Vector3(0f, 0f, 0f);
		img_tip.GetComponent<CanvasGroup>().alpha = 0f;
		img_tip.DOLocalMoveY(-67.8f, 0.3f);
		img_tip.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		img_tip.DOKill();
		img_tip.localPosition = new Vector3(0f, 0f, 0f);
		img_tip.GetComponent<CanvasGroup>().alpha = 0f;
		img_tip.gameObject.SetActive(value: false);
	}
}
