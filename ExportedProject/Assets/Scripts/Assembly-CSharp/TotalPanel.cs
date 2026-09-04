using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TotalPanel : MonoBehaviour
{
	public BigItem developitem;

	public BigItem voiceitem;

	public BigItem emailitem;

	public BigItem personitem;

	[SerializeField]
	private GameObject totalpanel;

	[SerializeField]
	private GameObject voicepanel;

	[SerializeField]
	private GameObject personpanel;

	[SerializeField]
	private GameObject emailpanel;

	[SerializeField]
	private GameObject developmentpanel;

	public bool ishascio;

	public bool ishastom;

	public CanvasGroup houtaipanel;

	public GameManager gameManager;

	public HoutaiPanel houtaiPanel0;

	public List<string> zimus = new List<string>();

	public List<int> yuyins = new List<int>();

	public void ClosePanel()
	{
		if (gameManager.iscanhoutaiclose)
		{
			houtaipanel.DOFade(0f, 0.2f);
			houtaipanel.transform.DOScale(Vector3.zero, 0.2f).OnComplete(delegate
			{
				gameManager.musicManager.PlayMusicLoop(3);
				Object.Destroy(houtaipanel.gameObject);
			});
		}
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.musicManager.PlayMusicLoop(12);
		ishascio = gameManager.player.playerdata.ishasciovoice;
		ishastom = gameManager.player.playerdata.ishastomblancovoice;
		gameManager.player.playerdata.isenterhoutai = true;
		gameManager.saveManager.SavePlayerData();
		if (!gameManager.player.playerdata.isshowhoutaizimu0)
		{
			houtaiPanel0.ShowZimu(zimus, yuyins, 3f);
			gameManager.player.playerdata.isshowhoutaizimu0 = true;
			gameManager.saveManager.SavePlayerData();
		}
	}

	public void ShowPanel(int panelid)
	{
		totalpanel.SetActive(value: false);
		switch (panelid)
		{
		case 0:
			personpanel.SetActive(value: true);
			break;
		case 1:
			emailpanel.SetActive(value: true);
			break;
		case 2:
			voicepanel.SetActive(value: true);
			break;
		case 3:
			developmentpanel.SetActive(value: true);
			break;
		}
	}
}
