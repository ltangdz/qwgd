using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class TwoDriveVanLoadList : MonoBehaviour
{
	public int index;

	public Text percent;

	public Image jindu;

	public int type;

	private GameManager gameManager;

	public List<GameObject> box;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		type = gameManager.player.playerdata.twodriveVanType;
		if (type == 1)
		{
			ShowBox(2);
		}
	}

	private void ShowBox(int a)
	{
		for (int i = 0; i < box.Count; i++)
		{
			box[i].SetActive(value: false);
		}
		box[a].SetActive(value: true);
	}

	public void StartLoading()
	{
		ShowBox(1);
		int a = 0;
		DOTween.To(() => a, delegate(int x)
		{
			a = x;
		}, 100, 1f).OnUpdate(delegate
		{
			percent.GetComponent<I18NText>().updateTranslation2(a + "%");
		});
		jindu.GetComponent<RectTransform>().DOScaleX(1f, 1f).OnComplete(delegate
		{
			ShowBox(2);
		});
	}
}
