using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ShiWanEnd1 : MonoBehaviour
{
	public Image overTip;

	public Button btnAddWish;

	public Image logo;

	public ShiWanEnd parObj;

	public Button btnSteam;

	public Button btnContinue;

	public Button btnAddWish2;

	public GameObject cnBox;

	public GameObject enBox;

	public Button fb;

	public Button tt;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (I18N.instance.gameLang == LanguageCode.CN)
		{
			cnBox.SetActive(value: true);
			enBox.SetActive(value: false);
		}
		else if (I18N.instance.gameLang == LanguageCode.TC)
		{
			cnBox.SetActive(value: true);
			enBox.SetActive(value: false);
		}
		else if (I18N.instance.gameLang == LanguageCode.EN)
		{
			cnBox.SetActive(value: false);
			enBox.SetActive(value: true);
			fb.onClick.AddListener(DumpToFB);
			tt.onClick.AddListener(DumpToTT);
		}
		btnSteam.onClick.AddListener(BtnSteam);
		btnContinue.onClick.AddListener(Continue);
		btnAddWish2.onClick.AddListener(parObj.AddWish);
		btnAddWish.onClick.AddListener(parObj.AddWish);
	}

	private void BtnSteam()
	{
	}

	private void Continue()
	{
		gameManager.ShowFloatBox();
		Invoke("ChangePanel", 2f);
	}

	private void ChangePanel()
	{
		parObj.shiwan2.gameObject.SetActive(value: true);
		base.gameObject.SetActive(value: false);
	}

	private void DumpToFB()
	{
		Application.OpenURL("https://www.facebook.com/cybermanhunt");
	}

	private void DumpToTT()
	{
		Application.OpenURL("https://twitter.com/cyber_manhunt");
	}
}
