using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Expect : MonoBehaviour
{
	public Button btnGoon;

	public Text title;

	private GameManager gameManager;

	public bool isshowsavepanel;

	private void Start()
	{
		Invoke("Init", 2f);
		Invoke("ShowBtn", 5f);
	}

	private void Init()
	{
		title.GetComponent<CanvasGroup>().DOFade(1f, 5f);
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btnGoon.onClick.AddListener(delegate
		{
			if (!isshowsavepanel)
			{
				StartCoroutine(Goon());
			}
			else
			{
				gameManager.saveManager.savePanel.isOver = true;
				gameManager.saveManager.ShowSavePanel(3);
			}
		});
	}

	private void StopHuaping()
	{
		if ((SceneManager.GetActiveScene().name == "homego" || SceneManager.GetActiveScene().name == "homeDLC") && gameManager.homeScene != null)
		{
			gameManager.homeScene.cameraFilterPack_Noise_TV_2.enabled = false;
		}
		else if (SceneManager.GetActiveScene().name == "home" && gameManager.maincamera != null)
		{
			gameManager.maincamera.GetComponent<CameraFilterPack_Noise_TV_2>().enabled = false;
		}
	}

	private void ShowBtn()
	{
		btnGoon.gameObject.SetActive(value: true);
	}

	private IEnumerator Goon()
	{
		gameManager.ShowFloatBox();
		yield return new WaitForSeconds(2f);
		Object.Instantiate(Resources.Load<GameObject>("Dialog/endPanel"), base.transform.parent);
		base.gameObject.SetActive(value: false);
	}
}
