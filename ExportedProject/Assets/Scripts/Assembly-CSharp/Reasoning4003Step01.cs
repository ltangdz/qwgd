using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4003Step01 : MonoBehaviour
{
	public List<GameObject> img_dragrolelist = new List<GameObject>();

	public List<GameObject> img_roleblanklist = new List<GameObject>();

	public GameObject txt_tip;

	public GameObject txt_elsie;

	public List<Image> img_linelist = new List<Image>();

	public GameObject img_elsie;

	public GameObject img_arrow;

	public GameObject step02;

	public void Gotonext()
	{
		for (int i = 0; i < img_dragrolelist.Count; i++)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Join(img_dragrolelist[i].GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetEase(Ease.InCubic)).Join(img_dragrolelist[i].transform.DOBlendableLocalMoveBy(new Vector3(0f, 50f, 0f), 0.5f).SetEase(Ease.InCubic));
			sequence.Play();
		}
		for (int j = 0; j < img_roleblanklist.Count; j++)
		{
			Sequence sequence2 = DOTween.Sequence();
			sequence2.PrependInterval(1f).Join(img_roleblanklist[j].transform.DOBlendableLocalMoveBy(new Vector3(0f, 250f, 0f), 0.5f).SetEase(Ease.InCubic));
			sequence2.Play();
			img_roleblanklist[j].GetComponent<DragLine>().enabled = true;
		}
		txt_tip.transform.DOBlendableLocalMoveBy(new Vector3(0f, 460f, 0f), 0.5f).SetEase(Ease.InCubic).OnComplete(delegate
		{
			txt_tip.GetComponent<Text>().text = I18N.instance.getValue("^tuili0411");
		});
		for (int num = 0; num < img_linelist.Count; num++)
		{
			img_linelist[num].DOFillAmount(0f, 0.5f);
		}
		txt_elsie.SetActive(value: false);
		Sequence sequence3 = DOTween.Sequence();
		sequence3.Join(img_elsie.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetEase(Ease.InCubic)).Join(img_elsie.transform.DOBlendableLocalMoveBy(new Vector3(0f, 50f, 0f), 0.5f).SetEase(Ease.InCubic)).AppendCallback(delegate
		{
			step02.SetActive(value: true);
			for (int k = 0; k < img_roleblanklist.Count; k++)
			{
				img_roleblanklist[k].transform.parent = step02.transform;
				img_roleblanklist[k].transform.SetAsFirstSibling();
			}
		});
		sequence3.Play();
		img_arrow.SetActive(value: false);
	}

	public void RemoveAni()
	{
		GetComponent<Animator>().enabled = false;
		Object.Destroy(GetComponent<Animator>());
	}
}
