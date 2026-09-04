using UnityEngine;
using UnityEngine.UI;

public class ShiWanEnd2 : MonoBehaviour
{
	public Button btnAddWish;

	public Button btnContinue;

	public ShiWanEnd parObj;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btnAddWish.onClick.AddListener(parObj.AddWish);
		btnContinue.onClick.AddListener(Continue);
	}

	public void Continue()
	{
		if (parObj.isClickWishBtn || parObj.dontAddWish)
		{
			gameManager.ShowFloatBox();
			Invoke("ShowEnd", 2f);
		}
		else
		{
			parObj.alertBox.SetActive(value: true);
			parObj.alertBox.GetComponent<Animator>().Play("Exit Panel In");
		}
	}

	public void AlertContinue()
	{
		gameManager.ShowFloatBox();
		parObj.alertBox.SetActive(value: false);
		parObj.alertBox.GetComponent<Animator>().Play("Exit Panel Out");
		Invoke("ShowEnd", 2f);
	}

	private void ShowEnd()
	{
		Object.Instantiate(Resources.Load<GameObject>("Dialog/endPanel"), gameManager.homeScene.middle);
		Object.Destroy(base.gameObject);
	}
}
