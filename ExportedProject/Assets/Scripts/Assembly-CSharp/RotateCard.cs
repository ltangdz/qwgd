using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotateCard : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	public bool isUp = true;

	public bool canClick = true;

	public GameObject cardUpGo;

	public GameObject cardDownGo;

	public Sprite redSpriteUp;

	public Sprite redSpriteDown;

	public Sprite blueSprite;

	public Sprite graySprite;

	private bool isShowingErrorTween;

	private Image cardUpImage;

	private Image cardDownImage;

	private void Start()
	{
		cardUpImage = cardUpGo.GetComponent<Image>();
		cardDownImage = cardDownGo.GetComponent<Image>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!canClick)
		{
			return;
		}
		canClick = false;
		isUp = !isUp;
		if (isUp)
		{
			base.transform.DOLocalRotate(new Vector3(0f, 90f, 0f), 0.1f).OnComplete(delegate
			{
				cardUpGo.SetActive(value: true);
				cardDownGo.SetActive(value: false);
				base.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.1f).OnComplete(delegate
				{
					canClick = true;
				});
			});
			return;
		}
		base.transform.DOLocalRotate(new Vector3(0f, 90f, 0f), 0.1f).OnComplete(delegate
		{
			cardUpGo.SetActive(value: false);
			cardDownGo.SetActive(value: true);
			base.transform.DOLocalRotate(new Vector3(0f, 180f, 0f), 0.1f).OnComplete(delegate
			{
				canClick = true;
			});
		});
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		base.transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.2f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		base.transform.DOScale(Vector3.one, 0.2f);
	}

	public void ShowErrorTween()
	{
		if (isShowingErrorTween)
		{
			return;
		}
		isShowingErrorTween = true;
		if (isUp)
		{
			cardUpImage.sprite = redSpriteUp;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(cardUpImage.DOFade(0.2f, 0.5f));
			sequence.Append(cardUpImage.DOFade(1f, 0.5f));
			sequence.Play().SetLoops(3).OnComplete(delegate
			{
				cardUpImage.sprite = graySprite;
				isShowingErrorTween = false;
			});
		}
		else
		{
			cardDownImage.sprite = redSpriteDown;
			Sequence sequence2 = DOTween.Sequence();
			sequence2.Append(cardDownImage.DOFade(0.2f, 0.5f));
			sequence2.Append(cardDownImage.DOFade(1f, 0.5f));
			sequence2.Play().SetLoops(3).OnComplete(delegate
			{
				cardDownImage.sprite = blueSprite;
				isShowingErrorTween = false;
			});
		}
	}
}
