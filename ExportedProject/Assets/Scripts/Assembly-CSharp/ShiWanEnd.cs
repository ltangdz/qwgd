using DG.Tweening;
using UnityEngine;

public class ShiWanEnd : MonoBehaviour
{
	public bool isClickWishBtn;

	public bool dontAddWish;

	public ShiWanEnd1 shiwan1;

	public ShiWanEnd2 shiwan2;

	public GameObject alertBox;

	public GameObject img_light;

	private void Start()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.AppendInterval(2f).Append(img_light.transform.DOLocalMoveX(187f, 2f).OnComplete(delegate
		{
			img_light.transform.localPosition = new Vector2(-187f, 0f);
		}));
		sequence.Play().SetLoops(-1);
	}

	public void SureAddWish()
	{
		alertBox.GetComponent<Animator>().Play("Exit Panel Out");
		AddWish();
	}

	public void AddWish()
	{
		isClickWishBtn = true;
	}

	public void DontAddWish()
	{
		dontAddWish = true;
		alertBox.GetComponent<Animator>().Play("Exit Panel Out");
	}
}
