using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThanksPanel : MonoBehaviour
{
	public Text title;

	public Text stadioname;

	public Text time;

	public Button bakmain;

	public Button quitgame;

	public Animator confirm;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		bakmain.onClick.AddListener(BakMain);
		quitgame.onClick.AddListener(QuitGame);
		StartCoroutine(ShowThanks());
	}

	private void BakMain()
	{
		gameManager.txt_studio.SetActive(value: true);
		SceneManager.LoadScene("mainScene");
	}

	private void QuitGame()
	{
		confirm.gameObject.SetActive(value: true);
		confirm.Play("Exit Panel In");
	}

	public void CancelExitGame()
	{
		confirm.Play("Exit Panel Out");
	}

	public void Quit()
	{
		Application.Quit();
	}

	private IEnumerator ShowThanks()
	{
		yield return new WaitForSeconds(2f);
		title.GetComponent<CanvasGroup>().DOFade(1f, 2f);
		stadioname.GetComponent<CanvasGroup>().DOFade(1f, 2f);
		time.GetComponent<CanvasGroup>().DOFade(1f, 2f);
		yield return new WaitForSeconds(2.5f);
		bakmain.gameObject.SetActive(value: true);
		quitgame.gameObject.SetActive(value: true);
	}
}
