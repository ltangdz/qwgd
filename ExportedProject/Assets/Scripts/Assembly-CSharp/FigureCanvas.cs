using UnityEngine;
using UnityEngine.UI;

public class FigureCanvas : MonoBehaviour
{
	public InputField inputField;

	public GameManager gameManager;

	public Button btn_sure;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
	{
	}

	public void Sure()
	{
		if (gameManager.homeScene.goalDialog != null && !inputField.text.Equals(""))
		{
			gameManager.homeScene.goalDialog.CompleteItem(inputField.text);
		}
	}
}
