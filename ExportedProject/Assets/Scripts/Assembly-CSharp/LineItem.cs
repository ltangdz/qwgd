using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LineItem : MonoBehaviour
{
	[SerializeField]
	private List<Sprite> sprites = new List<Sprite>();

	[SerializeField]
	private Image img_box;

	[SerializeField]
	private Image img_line;

	[SerializeField]
	private Image img_cube;

	public Image img_lingxing;

	public FaceItem currentfaceitem;

	public void SetBlue()
	{
		img_box.sprite = sprites[4];
		img_line.sprite = sprites[5];
		img_cube.sprite = sprites[6];
		img_lingxing.sprite = sprites[7];
	}

	public void SetGray(bool isneedremovefaceitem = true)
	{
		img_box.sprite = sprites[0];
		img_line.sprite = sprites[1];
		img_cube.sprite = sprites[2];
		img_lingxing.sprite = sprites[3];
		img_box.color = Color.white;
		img_line.color = Color.white;
		img_cube.color = Color.white;
		img_lingxing.color = Color.white;
		if (isneedremovefaceitem)
		{
			currentfaceitem = null;
		}
	}

	public void SetRed()
	{
		img_box.sprite = sprites[8];
		img_line.sprite = sprites[9];
		img_box.DOFade(0.2f, 0.5f).SetLoops(3);
		img_line.DOFade(0.2f, 0.5f).SetLoops(3).OnComplete(delegate
		{
			SetGray();
		});
		if (currentfaceitem != null)
		{
			currentfaceitem.SetRed();
		}
	}

	public void StartLingXingAnimation()
	{
		if (currentfaceitem == null)
		{
			SetGray();
		}
		else
		{
			SetBlue();
		}
		img_cube.transform.DOScale(new Vector3(1f, 0f, 1f), 0.1f);
		img_lingxing.gameObject.SetActive(value: true);
		img_lingxing.transform.DOScaleY(1f, 0.1f).OnComplete(delegate
		{
			img_lingxing.transform.DORotate(new Vector3(0f, 180f, img_lingxing.transform.rotation.z), 1.5f).SetEase(Ease.Linear).SetLoops(-1);
		});
	}

	public void StopLingXingAnimation()
	{
		if (currentfaceitem == null)
		{
			SetGray();
		}
		else
		{
			SetBlue();
		}
		img_lingxing.transform.DOKill();
		img_lingxing.transform.rotation = Quaternion.Euler(0f, 0f, img_lingxing.transform.rotation.z);
		img_lingxing.transform.DOScale(new Vector3(1f, 0f, 1f), 0.1f).OnComplete(delegate
		{
			img_lingxing.gameObject.SetActive(value: false);
		});
		img_cube.transform.DOScaleY(1f, 0.1f);
	}
}
