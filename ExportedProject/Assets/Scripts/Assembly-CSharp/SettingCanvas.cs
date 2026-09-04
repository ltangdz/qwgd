using UnityEngine;

public class SettingCanvas : MonoBehaviour
{
	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.setting = base.gameObject;
	}
}
