using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadeLoading : MonoBehaviour
{
	public Text txtType;

	public Image loadingBox;

	public List<Color> mainColor;

	public Image icon;

	public List<Sprite> iconSprite;

	private bool isSuc;

	private GameManager gameManager;

	public void Loading(bool success)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		isSuc = success;
		StartCoroutine(StartLoading());
	}

	private IEnumerator StartLoading()
	{
		loadingBox.GetComponent<RectTransform>().localScale = new Vector3(0f, 1f, 1f);
		loadingBox.GetComponent<RectTransform>().DOScaleX(1f, 2.5f);
		int a = 0;
		string txt = I18N.instance.getValue("^invade_soundlabel01");
		DOTween.To(() => a, delegate(int x)
		{
			a = x;
		}, 100, 2.5f).SetEase(Ease.Linear).OnUpdate(delegate
		{
			txtType.GetComponent<I18NText>().updateTranslation2(txt + a + "%");
		});
		yield return new WaitForSeconds(2.5f);
		if (isSuc)
		{
			txtType.GetComponent<I18NText>().updateTranslation2("^invade_soundlabel03");
			txtType.color = mainColor[1];
			loadingBox.color = mainColor[1];
			icon.gameObject.SetActive(value: true);
			icon.sprite = iconSprite[0];
		}
		else
		{
			txtType.GetComponent<I18NText>().updateTranslation2("^invade_soundlabel02");
			txtType.color = mainColor[2];
			loadingBox.color = mainColor[2];
			icon.gameObject.SetActive(value: true);
			icon.sprite = iconSprite[1];
		}
		yield return new WaitForSeconds(1f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		base.gameObject.SetActive(value: false);
		icon.gameObject.SetActive(value: false);
		txtType.color = mainColor[0];
		loadingBox.color = mainColor[0];
	}
}
