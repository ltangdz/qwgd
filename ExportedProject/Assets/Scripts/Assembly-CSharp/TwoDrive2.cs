using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class TwoDrive2 : MonoBehaviour
{
	public Button runBtn;

	public InputField inputObj;

	public string openID;

	public GameObject fileIndex;

	public GameObject fileInfo;

	public GameObject wrongWarning;

	[SerializeField]
	private Button btn_play;

	[SerializeField]
	private GameObject content;

	[SerializeField]
	public List<int> typelist = new List<int>();

	[SerializeField]
	public List<string> contentlist = new List<string>();

	public List<string> contentID = new List<string>();

	public List<string> contentkey = new List<string>();

	public string leftname = "^qietingname01";

	public string rightname = "^qietingname02";

	[SerializeField]
	private Image img_blue;

	[SerializeField]
	private Text time;

	private bool audioPlaying;

	private bool playend;

	private bool isPlay;

	private string stime;

	public int musicID = -1;

	private GameManager gameManager;

	private void Start()
	{
		stime = "14";
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btn_play.onClick.AddListener(delegate
		{
			gameManager.musicManager.LowerVol();
			StartCoroutine(PlayRadio());
		});
		time.text = "0:" + typelist.Count;
		if (runBtn != null)
		{
			runBtn.onClick.AddListener(OpenFile);
		}
	}

	public void CloseList()
	{
		btn_play.interactable = true;
		StopAllCoroutines();
		img_blue.fillAmount = 0f;
		img_blue.GetComponent<Image>().DOKill();
		time.text = "0:14";
		gameManager.istaohuashow = false;
		gameManager.homeScene.ShowNextVideo();
		audioPlaying = false;
		isPlay = false;
		if (gameManager.player.GetEventId().Equals("110005"))
		{
			gameManager.homeScene.ShowVideoTip("3700067");
		}
	}

	private IEnumerator PlayRadio()
	{
		if (!audioPlaying)
		{
			if (!isPlay)
			{
				GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Dialog/twoListDialog2"), gameManager.homeScene.middle);
				obj.GetComponent<TwoDriveListDialog>().Show();
				obj.GetComponent<TwoDriveListDialog>().Init(this);
			}
			img_blue.GetComponent<Image>().fillAmount = 0f;
			audioPlaying = true;
			yield return new WaitForSeconds(1f);
			gameManager.musicManager.LowerVol();
			gameManager.soundManager.PlayEvent(gameManager.player.GetEventId(), musicID);
			isPlay = true;
			img_blue.GetComponent<Image>().DOFillAmount(1f, float.Parse(stime)).SetEase(Ease.Linear);
			for (int i = int.Parse(stime); i > 0; i--)
			{
				yield return new WaitForSeconds(1f);
				string text = (i - 1).ToString();
				time.GetComponent<I18NText>().updateTranslation2(text + "s");
			}
			audioPlaying = false;
		}
	}

	private IEnumerator Init()
	{
		time.text = "0:" + typelist.Count;
		for (int i = 0; i < typelist.Count; i++)
		{
			if (typelist[i] == 0)
			{
				Transform obj = Object.Instantiate(Resources.Load<Transform>("twoDrive_item"), content.transform);
				obj.Find("txt_name").GetComponent<I18NText>().updateTranslation2(leftname);
				obj.Find("box/Text").gameObject.SetActive(value: true);
				obj.Find("box/tb_info").gameObject.SetActive(value: false);
				obj.Find("box/Text").GetComponent<I18NText>().updateTranslation2(contentlist[i]);
			}
			else if (typelist[i] == 1)
			{
				Transform obj2 = Object.Instantiate(Resources.Load<Transform>("twoDrive_itemBak"), content.transform);
				obj2.Find("inbox/txt_name").GetComponent<I18NText>().updateTranslation2(rightname);
				obj2.Find("inbox/box/Text").gameObject.SetActive(value: true);
				obj2.Find("inbox/box/tb_info").gameObject.SetActive(value: false);
				obj2.Find("inbox/box/Text").GetComponent<I18NText>().updateTranslation2(contentlist[i]);
			}
			else if (typelist[i] == 2)
			{
				Transform obj3 = Object.Instantiate(Resources.Load<Transform>("twoDrive_item"), content.transform);
				obj3.Find("txt_name").GetComponent<I18NText>().updateTranslation2(leftname);
				obj3.Find("box/Text").gameObject.SetActive(value: false);
				obj3.Find("box/tb_info").gameObject.SetActive(value: true);
				obj3.Find("box/tb_info").GetComponent<MultiplyText>().SetContent2(contentlist[i], contentID[i], I18N.instance.getValue(contentlist[i]));
			}
			else if (typelist[i] == 3)
			{
				Transform obj4 = Object.Instantiate(Resources.Load<Transform>("twoDrive_itemBak"), content.transform);
				obj4.Find("inbox/txt_name").GetComponent<I18NText>().updateTranslation2(rightname);
				obj4.Find("inbox/box/Text").gameObject.SetActive(value: false);
				obj4.Find("inbox/box/tb_info").gameObject.SetActive(value: true);
				obj4.Find("inbox/box/tb_info").GetComponent<MultiplyText>().SetContent2(contentlist[i], contentID[i], I18N.instance.getValue(contentlist[i]));
			}
			DOTween.To(() => content.transform.localPosition, delegate(Vector3 x)
			{
				content.transform.localPosition = x;
			}, new Vector3(0f, content.GetComponent<RectTransform>().sizeDelta.y, 0f), 0.29f).OnComplete(delegate
			{
				base.gameObject.SetActive(value: true);
			});
			yield return new WaitForSeconds(1f);
			time.text = "0:" + (typelist.Count - i - 1);
		}
	}

	private void OpenFile()
	{
		string text = inputObj.text;
		if (text.Trim() != "")
		{
			string message = gameManager.dataManager.dic1[openID].message;
			if (text.Trim().Equals(I18N.instance.getValue(message)))
			{
				fileIndex.SetActive(value: false);
				fileInfo.SetActive(value: true);
			}
			else if (!wrongWarning.activeInHierarchy)
			{
				StartCoroutine(ShowWarning());
			}
		}
	}

	private IEnumerator ShowWarning()
	{
		wrongWarning.SetActive(value: true);
		yield return new WaitForSeconds(2f);
		wrongWarning.SetActive(value: false);
	}
}
