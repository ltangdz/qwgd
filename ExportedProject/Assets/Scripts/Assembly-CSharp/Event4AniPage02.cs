using UnityEngine;

public class Event4AniPage02 : MonoBehaviour
{
	public Event4Video event4Video;

	public GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void ChangePage()
	{
		event4Video.ChangePage2();
	}

	public void PlaySound()
	{
		gameManager.soundManager.PlayEvent("110004", 48);
	}
}
