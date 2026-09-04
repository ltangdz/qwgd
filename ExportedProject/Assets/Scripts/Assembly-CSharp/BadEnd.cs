using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BadEnd : MonoBehaviour
{
	public Text title;

	public Text info;

	public Button bakGame;

	public Button bakMain;

	public GameObject bakWindow;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.UnlockAchievements("artificial");
		StartCoroutine(ShowAni());
		bakGame.onClick.AddListener(delegate
		{
			gameManager.homeScene.newsPanel.GetComponent<RectTransform>().localScale = Vector3.one;
			gameManager.homeScene.goalDialog.GetComponent<RectTransform>().localScale = Vector3.one;
			gameManager.homeScene.logPanel.GetComponent<RectTransform>().localScale = Vector3.one;
			if (gameManager.homeScene.computerButton != null)
			{
				gameManager.homeScene.computerButton.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
			}
			if (gameManager.homeScene.notebook != null)
			{
				gameManager.homeScene.notebook.GetComponent<RectTransform>().localPosition = new Vector3(1251f, -104f, 0f);
				gameManager.homeScene.notebook.isshow = false;
			}
			Object.Destroy(base.gameObject);
		});
		bakMain.onClick.AddListener(delegate
		{
			bakWindow.SetActive(value: true);
			bakWindow.GetComponent<Animator>().Play("Exit Panel In");
		});
	}

	public void SureBakMain()
	{
		SceneManager.LoadScene("mainScene");
	}

	public void Cancel()
	{
		bakWindow.GetComponent<Animator>().Play("Exit Panel Out");
		Invoke("HideWindow", 1f);
	}

	private void HideWindow()
	{
		bakWindow.SetActive(value: false);
	}

	private IEnumerator ShowAni()
	{
		yield return new WaitForSeconds(2f);
		title.DOFade(1f, 2f);
		yield return new WaitForSeconds(2f);
		title.GetComponent<RectTransform>().DOMoveY(256f, 1f);
		yield return new WaitForSeconds(1f);
		info.DOFade(1f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		bakGame.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
		bakMain.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
	}
}
