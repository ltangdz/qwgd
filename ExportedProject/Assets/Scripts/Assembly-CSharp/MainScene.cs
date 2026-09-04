using UnityEngine;

public class MainScene : MonoBehaviour
{
	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.musicManager.Stop();
		gameManager.musicManager.PlayMusicLoop(8);
	}

	private void Update()
	{
	}
}
