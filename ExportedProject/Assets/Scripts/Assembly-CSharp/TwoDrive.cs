using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class TwoDrive : MonoBehaviour
{
	public GameObject fileIndex;

	public GameObject fileInfo;

	public InputField inputObj;

	public Button runBtn;

	public GameObject wrongWarning;

	public Button playBtn;

	public Image wen;

	public Text time;

	public GameObject fileListBox;

	public GameObject content;

	public Sprite playSprite;

	public Text infoLabel;

	public Text soundTime;

	public string openID;

	public string infoType;

	private Sprite stopSprite;

	private bool isPlay;

	private GameManager gameManager;

	private string eventID;

	private bool playend;

	private string stime;

	private bool audioPlaying;

	public void CloseList()
	{
		playBtn.interactable = true;
		StopAllCoroutines();
		wen.fillAmount = 0f;
		wen.GetComponent<Image>().DOKill();
		time.text = "0:111";
		gameManager.istaohuashow = false;
		gameManager.homeScene.ShowNextVideo();
		audioPlaying = false;
		isPlay = false;
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		stime = gameManager.soundManager.event01[37].length.ToString("f0");
		eventID = gameManager.player.GetEventId();
		if (infoType == "0")
		{
			soundTime.GetComponent<I18NText>().updateTranslation2(stime + "s");
			stopSprite = playBtn.GetComponent<Image>().sprite;
			playBtn.onClick.AddListener(delegate
			{
				StartCoroutine(PlayRadio());
			});
		}
		runBtn.onClick.AddListener(OpenFile);
	}

	private void OnDisable()
	{
		if (infoType == "0")
		{
			if (!playend)
			{
				playBtn.GetComponent<Image>().sprite = stopSprite;
				isPlay = false;
				soundTime.GetComponent<I18NText>().updateTranslation2(stime + "s");
				infoLabel.text = I18N.instance.getValue("^twodrive_label08");
			}
			audioPlaying = false;
			soundTime.GetComponent<I18NText>().updateTranslation2(stime + "s");
			StopAllCoroutines();
			wen.GetComponent<Image>().DOKill();
			wen.GetComponent<Image>().fillAmount = 0f;
			gameManager.soundManager.Stop();
		}
	}

	private void OpenFile()
	{
		string text = inputObj.text;
		if (text.Trim() != "")
		{
			string text2 = "";
			text2 = ((openID.IndexOf("^") > -1) ? openID : gameManager.dataManager.dic1[openID].message);
			if (text.Trim().Equals(I18N.instance.getValue(text2)))
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

	private IEnumerator PlayRadio()
	{
		Debug.Log(audioPlaying + "***************" + isPlay);
		if (!audioPlaying)
		{
			if (!isPlay)
			{
				GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Dialog/twoListDialog"), gameManager.homeScene.middle);
				obj.GetComponent<TwoDriveListDialog>().Show();
				obj.GetComponent<TwoDriveListDialog>().Init(this);
			}
			wen.GetComponent<Image>().fillAmount = 0f;
			audioPlaying = true;
			yield return new WaitForSeconds(1f);
			gameManager.musicManager.LowerVol();
			gameManager.soundManager.PlayEvent(eventID, 37);
			isPlay = true;
			playBtn.GetComponent<Image>().sprite = playSprite;
			wen.GetComponent<Image>().DOFillAmount(1f, float.Parse(stime)).SetEase(Ease.Linear);
			for (int i = int.Parse(stime); i > 0; i--)
			{
				yield return new WaitForSeconds(1f);
				string text = (i - 1).ToString();
				time.GetComponent<I18NText>().updateTranslation2(text + "s");
			}
			audioPlaying = false;
			playBtn.GetComponent<Image>().sprite = stopSprite;
		}
	}

	private IEnumerator Loading()
	{
		string a = "";
		for (int i = 1; i <= 8; i++)
		{
			a = ((i % 4 == 0) ? "" : (a + "."));
			infoLabel.text = I18N.instance.getValue("^twodrive_label08") + a;
			yield return new WaitForSeconds(0.3f);
		}
		playend = true;
		ShowChatInfo();
	}

	private void ShowChatInfo()
	{
		for (int i = 1; i <= 15; i++)
		{
			string text = ((i.ToString().Length == 2) ? i.ToString() : ("0" + i));
			if (i % 2 == 0)
			{
				string key = "  Modi";
				string key2 = "^twodrive_talk" + text;
				Transform transform = Object.Instantiate(Resources.Load<Transform>("twoDrive_item"), content.transform);
				transform.Find("txt_name").GetComponent<I18NText>().updateTranslation6(key);
				switch (i)
				{
				case 10:
					transform.Find("box/Text").gameObject.SetActive(value: false);
					transform.Find("box/tb_info").gameObject.SetActive(value: true);
					transform.Find("box/tb_info").GetComponent<MultiplyText>().SetContent2(key2, "10047", I18N.instance.getValue("^twodrive_talk17"));
					break;
				case 14:
					transform.Find("box/Text").gameObject.SetActive(value: false);
					transform.Find("box/tb_info").gameObject.SetActive(value: true);
					transform.Find("box/tb_info").GetComponent<MultiplyText>().SetContent2(key2, "10048", I18N.instance.getValue("^twodrive_talk18"));
					break;
				default:
					transform.Find("box/Text").gameObject.SetActive(value: true);
					transform.Find("box/tb_info").gameObject.SetActive(value: false);
					transform.Find("box/Text").GetComponent<I18NText>().updateTranslation2(key2);
					break;
				}
			}
			else
			{
				string key3 = "Harris  ";
				string key4 = "^twodrive_talk" + text;
				Transform obj = Object.Instantiate(Resources.Load<Transform>("twoDrive_itemBak"), content.transform);
				obj.Find("inbox/txt_name").GetComponent<I18NText>().updateTranslation6(key3);
				obj.Find("inbox/box/Text").GetComponent<I18NText>().updateTranslation2(key4);
			}
		}
	}
}
