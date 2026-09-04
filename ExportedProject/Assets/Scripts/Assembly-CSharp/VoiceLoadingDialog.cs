using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class VoiceLoadingDialog : MonoBehaviour
{
	public Image img_sliderbk;

	public Image img_sliderfilled;

	public Text txt_loading;

	public Sprite[] sprites;

	public Color[] colors;

	public GameObject singlevoice;

	public GameObject singlevoice2;

	public GameObject voiceloadingdialog;

	public void Show(int type)
	{
		txt_loading.text = "";
		txt_loading.color = colors[0];
		img_sliderfilled.fillAmount = 0f;
		img_sliderfilled.sprite = sprites[0];
		if (type == 0 || type == 3)
		{
			img_sliderfilled.DOFillAmount(1f, 3f).OnUpdate(delegate
			{
				txt_loading.text = string.Format(I18N.instance.getValue("^houtai33"), ((int)(img_sliderfilled.fillAmount * 100f)).ToString());
			}).OnComplete(delegate
			{
				txt_loading.text = string.Format(I18N.instance.getValue("^houtai33"), "100");
				StartCoroutine(CloseDialog(type));
			});
		}
		else if (type == 1)
		{
			img_sliderfilled.DOFillAmount(1f, 3f).OnUpdate(delegate
			{
				txt_loading.text = string.Format(I18N.instance.getValue("^houtai51"), ((int)(img_sliderfilled.fillAmount * 100f)).ToString());
			}).OnComplete(delegate
			{
				img_sliderfilled.sprite = sprites[1];
				txt_loading.text = I18N.instance.getValue("^houtai52");
				txt_loading.color = colors[1];
				StartCoroutine(CloseDialog(type));
			});
		}
		else if (type == 2)
		{
			img_sliderfilled.DOFillAmount(1f, 3f).OnUpdate(delegate
			{
				txt_loading.text = string.Format(I18N.instance.getValue("^houtai51"), ((int)(img_sliderfilled.fillAmount * 100f)).ToString());
			}).OnComplete(delegate
			{
				img_sliderfilled.sprite = sprites[2];
				txt_loading.text = I18N.instance.getValue("^houtai53");
				txt_loading.color = colors[2];
				StartCoroutine(CloseDialog(type));
			});
		}
	}

	private IEnumerator CloseDialog(int type = 1)
	{
		yield return new WaitForSeconds(2f);
		base.gameObject.SetActive(value: false);
		switch (type)
		{
		case 0:
			singlevoice.SetActive(value: true);
			break;
		case 1:
			singlevoice.SetActive(value: false);
			singlevoice2.SetActive(value: false);
			break;
		case 3:
			singlevoice2.SetActive(value: true);
			break;
		}
	}
}
