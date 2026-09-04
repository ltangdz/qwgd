using System.Collections;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
	public GameManager gameManager;

	public Camera cam;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		cam = gameManager.startMainCanvas;
		StartCoroutine(StartMove());
		gameManager.Esc.GetComponent<HoldEsc>().sceneName = base.gameObject.name;
	}

	private void StartMusic()
	{
		gameManager.musicManager.PlayMusic(0);
	}

	public void Change(string scene)
	{
		if (!gameManager.holdEsc)
		{
			gameManager.startAniManager.ChangeScene(scene);
		}
	}

	private IEnumerator StartMove()
	{
		yield return new WaitForSeconds(0.5f);
		base.transform.GetComponent<Animator>().SetBool("start", value: true);
		yield return new WaitForSeconds(0.2f);
	}
}
