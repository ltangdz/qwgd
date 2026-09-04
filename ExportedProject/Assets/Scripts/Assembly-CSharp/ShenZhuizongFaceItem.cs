using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShenZhuizongFaceItem : MonoBehaviour
{
	[SerializeField]
	private List<Image> facelist = new List<Image>();

	[SerializeField]
	private GameObject img_scanline;

	[SerializeField]
	private Image img_topframe;

	[SerializeField]
	private Image img_bottomframe;

	[SerializeField]
	private float scanlinepos0;

	[SerializeField]
	private float scanlinepos1;

	private void Start()
	{
		StartCoroutine(Init());
	}

	private IEnumerator Init()
	{
		yield return new WaitForSeconds(2f);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(img_scanline.transform.DOLocalMoveY(scanlinepos0, 2f).SetEase(Ease.InOutCirc));
		sequence.Append(img_scanline.transform.DOLocalMoveY(scanlinepos1, 2f).SetEase(Ease.InOutCirc));
		sequence.Play().SetLoops(-1);
		img_topframe.transform.DOScaleY(1f, 0.2f);
		img_topframe.transform.DOLocalMoveY(54.4f, 0.2f);
		img_bottomframe.transform.DOScaleY(1f, 0.2f);
		img_bottomframe.transform.DOLocalMoveY(-43.9f, 0.2f);
		yield return new WaitForSeconds(0.3f);
		for (int i = 0; i < facelist.Count; i++)
		{
			facelist[i].DOFade(1f, 0.3f);
			facelist[i].transform.DOScale(Vector3.one, 0.3f);
			yield return new WaitForSeconds(0.3f);
		}
		for (int i = 0; i < facelist.Count; i++)
		{
			facelist[i].transform.GetChild(0).GetComponent<Image>().DOFade(1f, 0.3f);
			facelist[i].transform.GetChild(0).DOScale(Vector3.one, 0.3f);
			yield return new WaitForSeconds(0.3f);
		}
	}
}
