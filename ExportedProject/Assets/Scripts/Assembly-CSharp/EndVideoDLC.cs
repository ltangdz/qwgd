using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndVideoDLC : MonoBehaviour
{
	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.musicManager.Stop();
		GetComponent<AudioSource>().volume = gameManager.musicManager.GetMusicVoice();
		StartCoroutine(Run());
	}

	private IEnumerator Run()
	{
		yield return new WaitForSeconds(95f);
		StartCoroutine(End());
	}

	public void JumpEnd()
	{
		StartCoroutine(End());
	}

	private IEnumerator End()
	{
		gameManager.ShowFloatBox();
		yield return new WaitForSeconds(2f);
		gameManager.istaohuashow = false;
		gameManager.iscancollect = true;
		SceneManager.LoadScene("home");
	}
}
