using UnityEngine;

public class CameraMove : MonoBehaviour
{
	public GameManager gameManager;

	public UIScale uIScale;

	public bool ismove;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.maincamera = this;
		gameManager.setting.GetComponent<Setting>().cameramove = PlayerPrefs.GetInt("cameramove", 1);
	}

	public void NoteMoveTransform()
	{
		if (!gameManager.IsAllDlc() && gameManager.setting.GetComponent<Setting>().cameramove == 1)
		{
			uIScale.Scale(uIScale.gameObject, 1f, new Vector2(0.5f, 0.5f), Vector3.one, new Vector3(1.3f, 1.3f, 1.3f));
		}
	}

	public void NoteEmpty()
	{
		if (!gameManager.IsAllDlc() && gameManager.setting.GetComponent<Setting>().cameramove == 1)
		{
			uIScale.Scale(uIScale.gameObject, 1f, new Vector2(0.5f, 0.5f), new Vector3(1.3f, 1.3f, 1.3f), Vector3.one);
		}
	}
}
