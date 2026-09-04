using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YulunNewsControl : MonoBehaviour
{
	public YulunDialog yulunDialog;

	public YulunNewsControlBox yulunNewsControlBox;

	public YulunNewsChoiceBox boxChoiceBorder;

	public YulunNewsShuijunBox boxShuiJun;

	public YulunNewsPenziBox boxPenzi;

	public Button btnRun;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btnRun.onClick.AddListener(CalData);
	}

	private void CalData()
	{
		if (boxChoiceBorder.transform.childCount <= 0)
		{
			gameManager.soundManager.Stop();
			gameManager.soundManager.PlaySound(35);
			yulunNewsControlBox.gameObject.SetActive(value: false);
			yulunDialog.RefreshVal();
		}
	}

	public void Init(List<YulunNewsInfo> newsInfo)
	{
		Debug.Log("初始化newscontrol：1");
		boxChoiceBorder.Init(newsInfo);
		Debug.Log("初始化newscontrol：2");
		boxShuiJun.Init();
		Debug.Log("初始化newscontrol：3");
	}
}
