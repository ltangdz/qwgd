using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterItem : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	public string letter;

	private Image self;

	[SerializeField]
	private List<Sprite> sprites = new List<Sprite>();

	[SerializeField]
	private Image img_big;

	[SerializeField]
	private Image img_gray;

	[SerializeField]
	private ReasoningMiddle4004 reasoningMiddle4004;

	public Vector3 oldpos;

	public float delay;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (reasoningMiddle4004.Click(this))
		{
			img_gray.gameObject.SetActive(value: true);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		img_big.gameObject.SetActive(value: true);
		self.sprite = sprites[1];
		self.SetNativeSize();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		img_big.gameObject.SetActive(value: false);
		self.sprite = sprites[0];
		self.SetNativeSize();
	}

	private void Start()
	{
		self = GetComponent<Image>();
	}

	public void ResetPosition()
	{
		base.transform.DOKill();
		base.gameObject.SetActive(value: true);
		base.transform.DORotate(Vector3.zero, 0.1f);
		base.transform.DOLocalMove(oldpos, 0.5f);
		img_gray.gameObject.SetActive(value: false);
	}

	public void Init()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.AppendInterval(delay);
		sequence.Append(base.transform.DOScale(Vector3.one, 0.2f));
		sequence.Append(base.transform.DOLocalMove(oldpos, 0.2f));
		sequence.Play();
	}
}
