using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class YulunNewsShuijunBox : MonoBehaviour
{
	public Text txtShuijun;

	public Button btnAdd;

	public Button btnResume;

	public Text choiceVal;

	public List<Sprite> addBtnSprite;

	public List<Sprite> resumeBtnSprite;

	public float choicedShuijun;

	public float allShuijun;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btnAdd.onClick.AddListener(AddVal);
		btnResume.onClick.AddListener(ResumeVal);
	}

	public void Init()
	{
		txtShuijun.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^yulun_label247") + "(" + choicedShuijun + "/" + allShuijun + ")");
		choiceVal.GetComponent<I18NText>().updateTranslation2("0");
		if (choicedShuijun < allShuijun)
		{
			btnAdd.GetComponent<Image>().sprite = addBtnSprite[0];
			btnAdd.interactable = true;
			btnResume.GetComponent<Image>().sprite = resumeBtnSprite[1];
			btnResume.interactable = false;
		}
		if (choicedShuijun == allShuijun)
		{
			btnResume.interactable = false;
			btnResume.GetComponent<Image>().sprite = resumeBtnSprite[1];
		}
	}

	private void AddVal()
	{
		float num = float.Parse(choiceVal.text);
		gameManager.soundManager.Stop();
		gameManager.soundManager.PlaySound(34);
		if (allShuijun > 0f)
		{
			num += 1f;
			choicedShuijun += 1f;
			allShuijun -= 1f;
			btnResume.interactable = true;
			btnResume.GetComponent<Image>().sprite = resumeBtnSprite[0];
			if (allShuijun <= 0f)
			{
				btnAdd.interactable = false;
				btnAdd.GetComponent<Image>().sprite = addBtnSprite[1];
			}
		}
		choiceVal.GetComponent<I18NText>().updateTranslation2(choicedShuijun.ToString());
		txtShuijun.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^yulun_label247") + "(" + choicedShuijun + "/" + allShuijun + ")");
	}

	private void ResumeVal()
	{
		float num = float.Parse(choiceVal.text);
		gameManager.soundManager.Stop();
		gameManager.soundManager.PlaySound(34);
		if (num > 0f)
		{
			num -= 1f;
			choicedShuijun -= 1f;
			allShuijun += 1f;
			btnAdd.interactable = true;
			btnAdd.GetComponent<Image>().sprite = addBtnSprite[0];
			if (num <= 0f)
			{
				btnResume.interactable = false;
				btnResume.GetComponent<Image>().sprite = resumeBtnSprite[1];
			}
		}
		choiceVal.GetComponent<I18NText>().updateTranslation2(choicedShuijun.ToString());
		txtShuijun.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^yulun_label247") + "(" + choicedShuijun + "/" + allShuijun + ")");
	}
}
