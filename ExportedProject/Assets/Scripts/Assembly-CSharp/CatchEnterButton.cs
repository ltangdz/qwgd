using UnityEngine;
using UnityEngine.UI;

public class CatchEnterButton : MonoBehaviour
{
	public Button _enterButton;

	private int _type;

	private GameObject netDialogDLC;

	private void Start()
	{
		_enterButton.onClick.AddListener(ShowDialog);
	}

	public void InitData(int type)
	{
		_type = type;
	}

	private void ShowDialog()
	{
		GameManager component = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (_type == 0)
		{
			if (netDialogDLC == null)
			{
				netDialogDLC = (GameObject)Object.Instantiate(Resources.Load("_DLC/Prefabs/NetDialogDLC"), component.homeScene.computerButtonBox.dialogtool);
				netDialogDLC.transform.parent.gameObject.SetActive(value: true);
			}
			netDialogDLC.GetComponent<NetDialogDLC>().Show();
		}
		else
		{
			component.homeScene.StartInvadeDecrypt();
		}
	}
}
