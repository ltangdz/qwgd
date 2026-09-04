using UnityEngine;
using UnityEngine.UI;

public class InvadeSearchResult : MonoBehaviour
{
	public Button btnBak;

	public string[] list;

	private GameManager gameManager;

	public void Init(string[] fileList)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		list = fileList;
		btnBak.onClick.AddListener(Bak);
	}

	private void Bak()
	{
		Object.Instantiate(Resources.Load<GameObject>("Dialog/invadeSearchPanel"), base.transform.parent).GetComponent<InvadeSearchPanel>().Init(list, gameManager.homeScene.invadeDialog.listBox);
		Object.Destroy(base.gameObject);
	}
}
