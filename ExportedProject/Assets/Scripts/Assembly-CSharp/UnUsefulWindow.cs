using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class UnUsefulWindow : MonoBehaviour
{
	[SerializeField]
	private Text txt_content;

	[SerializeField]
	private Image img_filled;

	[SerializeField]
	private Image img_ok;

	public List<string> contentlist = new List<string>();

	public int type;

	private void Start()
	{
		if (type == 0)
		{
			int index = Random.Range(0, contentlist.Count);
			txt_content.GetComponent<I18NText>().updateTranslation2(contentlist[index]);
			StartCoroutine(Init());
		}
		else
		{
			StartCoroutine(Init1());
		}
	}

	private IEnumerator Init1()
	{
		float x = Random.Range(-910f, 910f);
		float y = Random.Range(-490f, 490f);
		base.transform.localPosition = new Vector2(x, y);
		base.transform.DOScaleY(1f, 0.15f);
		GetComponent<CanvasGroup>().DOFade(1f, 0.15f);
		yield return new WaitForSeconds(0.3f);
		img_filled.DOFillAmount(1f, 3f);
		int percent = 0;
		DOTween.To(() => percent, delegate(int num)
		{
			percent = num;
		}, 100, 3f).OnUpdate(delegate
		{
			txt_content.text = percent + " %";
		}).OnComplete(delegate
		{
			txt_content.text = "100 %";
			img_ok.transform.DOScale(Vector3.one, 0.2f);
		});
	}

	public void HideWindow()
	{
		base.transform.DOScaleY(0f, 0.15f);
		GetComponent<CanvasGroup>().DOFade(0f, 0.15f).OnComplete(delegate
		{
			Object.Destroy(base.gameObject);
		});
	}

	private IEnumerator Init()
	{
		float x = Random.Range(-910f, 910f);
		float y = Random.Range(-490f, 490f);
		base.transform.localPosition = new Vector2(x, y);
		base.transform.DOScale(Vector3.one, 0.15f);
		GetComponent<CanvasGroup>().DOFade(1f, 0.15f);
		yield return new WaitForSeconds(0.2f);
	}
}
