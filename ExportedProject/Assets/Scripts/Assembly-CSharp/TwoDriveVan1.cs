using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoDriveVan1 : MonoBehaviour
{
	public List<TwoDriveVanLoadList> loadList;

	public Button btnLoad;

	private bool loading;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btnLoad.onClick.AddListener(delegate
		{
			if (!loading)
			{
				loading = true;
				StartCoroutine(LoadFile());
			}
		});
		if (gameManager.player.playerdata.twodriveVanType == 1)
		{
			btnLoad.interactable = false;
		}
	}

	private IEnumerator LoadFile()
	{
		gameManager.player.playerdata.twodriveVanType = 1;
		for (int i = 0; i < loadList.Count; i++)
		{
			if (loadList[i].type == 0)
			{
				loadList[i].StartLoading();
				yield return new WaitForSeconds(0.3f);
			}
		}
		btnLoad.interactable = false;
	}
}
