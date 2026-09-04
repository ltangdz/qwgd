using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginCanvas : MonoBehaviour
{
	public bool iscanclick;

	public string scene1 = "Canvas01";

	private GameManager gameManager;

	public bool isjump;

	public GameObject video_logo;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Object.Destroy(gameManager.Esc);
		if (GetComponent<Animator>() == null)
		{
			SetCanClick();
		}
	}

	public void SetCanClick()
	{
		iscanclick = true;
	}

	private void Update()
	{
		if (Input.anyKeyDown && iscanclick)
		{
			StartCoroutine(ChangeScene());
		}
	}

	private IEnumerator ChangeScene()
	{
		if (iscanclick)
		{
			gameManager.ShowFloatBox();
			yield return new WaitForSeconds(2f);
			gameManager.musicManager.PlayMusicLoop(8);
			gameManager.txt_studio.SetActive(value: true);
			SceneManager.LoadScene("home");
		}
	}

	public void TitleSound()
	{
		gameManager.soundManager.PlaySound(18, delegate
		{
			if (isjump && gameManager.musicManager != null)
			{
				gameManager.musicManager.Stop();
				gameManager.musicManager.PlayMusicLoop(8);
			}
		});
	}

	public void ShowTitle()
	{
		if (video_logo != null)
		{
			video_logo.SetActive(value: true);
		}
	}
}
