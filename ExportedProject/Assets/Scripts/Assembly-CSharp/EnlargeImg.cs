using UnityEngine;
using UnityEngine.UI;

public class EnlargeImg : MonoBehaviour
{
	public GameObject group;

	public Button closeBtn;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		closeBtn.onClick.AddListener(delegate
		{
			Object.Destroy(base.gameObject);
		});
		gameManager.homeScene.largeDialog = base.gameObject;
	}
}
