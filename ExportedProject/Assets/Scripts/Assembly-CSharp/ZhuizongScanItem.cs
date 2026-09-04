using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ZhuizongScanItem : MonoBehaviour
{
	[SerializeField]
	private List<Image> facelist = new List<Image>();

	[SerializeField]
	private GameObject img_scanline;

	[SerializeField]
	private Image img_topframe;

	[SerializeField]
	private Image img_bottomframe;

	private void Start()
	{
		StartCoroutine(Init());
	}

	private IEnumerator Init()
	{
		yield return new WaitForSeconds(2f);
		img_scanline.transform.DOLocalMoveY(98.4f, 10f).OnComplete(delegate
		{
			img_scanline.transform.localPosition = new Vector3(img_scanline.transform.localPosition.x, -96f, img_scanline.transform.localPosition.z);
		}).SetLoops(-1);
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

	private void Update()
	{
	}
}
