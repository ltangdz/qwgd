using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZhadanVan : MonoBehaviour
{
	public Button btn_close;

	public Image van;

	public List<Sprite> vanList;

	private GameManager gameManager;

	private int i;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btn_close.onClick.AddListener(delegate
		{
			gameManager.homeScene.zhadanInvade.GameOver();
			Object.Destroy(gameManager.homeScene.zhadanInvade.gameObject);
		});
	}

	private IEnumerator Van()
	{
		while (true)
		{
			float seconds = Random.Range(1, 3);
			yield return new WaitForSeconds(seconds);
			int shakeTimes = Random.Range(10, 20);
			for (int j = 0; j < shakeTimes; j++)
			{
				i = ((i < vanList.Count - 1) ? (i + 1) : 0);
				van.sprite = vanList[i];
				yield return new WaitForSeconds(0.02f);
			}
			van.sprite = vanList[0];
		}
	}
}
