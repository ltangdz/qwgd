using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RedPoint : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	[SerializeField]
	private Image img_rotate;

	[SerializeField]
	private Image img_link;

	[SerializeField]
	private Image img_point0;

	[SerializeField]
	private Sprite bluesprite;

	[SerializeField]
	private DuikangPoint duikangPoint;

	private bool isblue;

	private void Init()
	{
		int num = Random.Range(-179, 179);
		img_rotate.transform.Rotate(new Vector3(0f, 0f, num));
		img_link.transform.localRotation = Quaternion.identity;
	}

	public void LongPress()
	{
		Init();
		img_link.transform.DOLocalRotate(new Vector3(0f, 0f, 359f), 2f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!isblue)
		{
			img_rotate.gameObject.SetActive(value: true);
			img_link.gameObject.SetActive(value: true);
			LongPress();
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (!isblue)
		{
			img_link.transform.DOKill();
			if (img_link.transform.localEulerAngles.z > img_rotate.transform.localEulerAngles.z - 42f && img_link.transform.localEulerAngles.z < img_rotate.transform.localEulerAngles.z + 42f)
			{
				isblue = true;
				img_point0.sprite = bluesprite;
				duikangPoint.Setblue();
			}
			img_link.transform.localRotation = Quaternion.identity;
			img_rotate.gameObject.SetActive(value: false);
			img_link.gameObject.SetActive(value: false);
		}
	}
}
