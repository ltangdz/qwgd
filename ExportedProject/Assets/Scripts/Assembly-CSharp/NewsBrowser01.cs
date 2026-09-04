using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NewsBrowser01 : MonoBehaviour
{
	public string newsid;

	public Button btn_get;

	public GameObject txt_gray;

	public GameManager gameManager;

	public bool isclicked;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Start()
	{
		btn_get.gameObject.SetActive(!gameManager.player.playerdata.getMask);
		txt_gray.SetActive(gameManager.player.playerdata.getMask);
		btn_get.onClick.AddListener(delegate
		{
			if (!isclicked)
			{
				isclicked = true;
				StartCoroutine(StartGetNews(newsid));
				gameManager.player.playerdata.getMask = true;
				btn_get.gameObject.SetActive(!gameManager.player.playerdata.getMask);
				txt_gray.SetActive(gameManager.player.playerdata.getMask);
			}
		});
	}

	private IEnumerator StartGetNews(string nid)
	{
		yield return new WaitForSeconds(1.5f);
		gameManager.homeScene.notebook.AddNewItems(gameManager.dataManager.dic13[nid].unlock.Substring(1).Split(';'));
	}
}
