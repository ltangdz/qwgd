using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class AnwangList : MonoBehaviour
{
	public GameObject parObj;

	public string title;

	public string artName;

	public string arttime;

	public List<string> info;

	public Sprite infoImg;

	public string itemid;

	public string linkUrl;

	public string linkJump;

	public List<string> commentArtName;

	public List<string> comment;

	public List<string> haveVideo;

	private GameManager gameManager;

	[SerializeField]
	private bool iscanopen;

	[SerializeField]
	private Animator cannotopenwindow;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		GetComponent<Button>().onClick.AddListener(ShowInfo);
	}

	private void ShowInfo()
	{
		if (iscanopen)
		{
			gameManager.homeScene.newbrowserDialog.AddNewAnWangPanel("evileyeInfo", "evileye", "https://www.Evileye.tt/?forum&title=" + I18N.instance.getValue(title), this);
			return;
		}
		StopAllCoroutines();
		StartCoroutine(StartOpen());
	}

	private IEnumerator StartOpen()
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		cannotopenwindow.gameObject.SetActive(value: true);
		cannotopenwindow.Play("Exit Panel In");
		yield return new WaitForSeconds(1.2f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	public void HideOpenWindow()
	{
		StartCoroutine(StartHideWindow());
	}

	private IEnumerator StartHideWindow()
	{
		cannotopenwindow.Play("Exit Panel Out");
		yield return new WaitForSeconds(1.2f);
		cannotopenwindow.gameObject.SetActive(value: false);
	}
}
