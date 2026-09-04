using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TapEmail : MonoBehaviour
{
	[SerializeField]
	private Button btn_play;

	[SerializeField]
	private GameObject content;

	[SerializeField]
	public List<int> typelist = new List<int>();

	[SerializeField]
	public List<string> contentlist = new List<string>();

	public List<string> contentID = new List<string>();

	public string leftname = "^qietingname01";

	public string rightname = "^qietingname02";

	[SerializeField]
	private Sprite stopSprite;

	[SerializeField]
	private Sprite playSprite;

	[SerializeField]
	private Image img_blue;

	[SerializeField]
	private Text time;

	[SerializeField]
	private int videoID;

	private bool audioPlaying;

	private bool playend;

	private bool isPlay;

	private Tweener tween;

	private string stime;

	private string eventID;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		eventID = gameManager.player.GetEventId();
		btn_play.onClick.AddListener(delegate
		{
			gameManager.musicManager.LowerVol();
			gameManager.CanShowSetting(1);
			btn_play.interactable = false;
			StartCoroutine(PlayRadio());
			GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Dialog/mailListDialog"), gameManager.homeScene.middle);
			obj.GetComponent<MailListDialog>().Show();
			obj.GetComponent<MailListDialog>().Init(this);
			gameManager.soundManager.Stop();
			gameManager.soundManager.PlayEvent(eventID, videoID);
		});
		time.text = "0:" + typelist.Count;
	}

	private IEnumerator PlayRadio()
	{
		yield return new WaitForSeconds(1f);
		img_blue.GetComponent<Image>().DOFillAmount(1f, typelist.Count).SetEase(Ease.Linear);
		StartCoroutine(Init());
	}

	private IEnumerator Init()
	{
		time.text = "0:" + typelist.Count;
		for (int i = 0; i < typelist.Count; i++)
		{
			yield return new WaitForSeconds(1f);
			time.text = "0:" + (typelist.Count - i - 1);
		}
	}

	public void CloseList()
	{
		btn_play.interactable = true;
		StopAllCoroutines();
		img_blue.fillAmount = 0f;
		img_blue.GetComponent<Image>().DOKill();
		time.text = "0:13";
	}
}
