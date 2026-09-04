using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class HackerVideoDialog03 : MonoBehaviour
{
	public Text txt_name;

	public Text txt_zimu2;

	public string[] zimus;

	public string[] yuyin;

	public HomeScene homeScene;

	public Image img_mouse;

	public Button btn_ringoff;

	public GameManager gameManager;

	public int pos;

	public string dataid;

	public string itemids;

	public string needbk;

	private bool hundown;

	private string eventID;

	public SpriteAnimation ashley;

	[SerializeField]
	private GameObject videoplayer;

	[SerializeField]
	private bool isSaying;

	private void Start()
	{
		Init();
		gameManager.CanShowSetting(1);
		gameManager.homeScene.computerButtonBox.iscanclick = false;
	}

	public void openClick()
	{
		gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	public void Init()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.musicManager.LowerVol();
		gameManager.homeScene.eventsystem.SetActive(value: false);
		eventID = gameManager.player.GetEventId();
		homeScene = gameManager.homeScene;
		if (needbk != "")
		{
			GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f);
		}
		Invoke("ClickZimu", 1.5f);
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
		{
			ClickZimu();
		}
	}

	public void ClickZimu()
	{
		img_mouse.gameObject.SetActive(value: true);
		if (pos < zimus.Length)
		{
			CioSaying();
		}
		else if (pos == zimus.Length && !hundown)
		{
			ashley.SetState(3);
			hundown = true;
			txt_zimu2.text = "";
			gameManager.soundManager.Stop();
			gameManager.soundManager.PlaySound(20);
			GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
			gameManager.CanShowSetting(-1);
			gameManager.player.playerdata.videotiplist.Add(dataid);
			AddLog();
			StartCoroutine(StartShowVideo());
		}
	}

	private void CioSaying()
	{
		if (!isSaying)
		{
			isSaying = true;
			txt_zimu2.GetComponent<Text>().text = "";
			ashley.SetState(3);
			StopAllCoroutines();
			gameManager.soundManager.Stop();
			float num = 0f;
			if (zimus.Length >= 1)
			{
				num = ((zimus[pos] == "^hackervd0301") ? gameManager.soundManager.PlayEventFinished(eventID, 15) : ((!(zimus[pos] == "^hackervd0616")) ? gameManager.soundManager.PlayEventFinished(eventID, int.Parse(yuyin[pos].Split(':')[1])) : gameManager.soundManager.PlayEventFinished(eventID, 57)));
				StartCoroutine(AudioPlayFinished(num));
			}
			float num2 = gameManager.CalculateLengthOfText(string.Format(I18N.instance.getValue(zimus[pos].Trim()), gameManager.player.playerdata.nickname), txt_zimu2);
			if (num2 < 1650f)
			{
				txt_zimu2.GetComponent<RectTransform>().sizeDelta = new Vector2(num2, 100f);
			}
			else
			{
				txt_zimu2.GetComponent<RectTransform>().sizeDelta = new Vector2(1650f, 100f);
			}
			num = ((num > 0.3f) ? (num - 0.3f) : num);
			txt_zimu2.DOText(string.Format(I18N.instance.getValue(zimus[pos].Trim()), gameManager.player.playerdata.nickname), num).SetEase(Ease.Linear).OnComplete(delegate
			{
				if (pos == zimus.Length - 1)
				{
					gameManager.soundManager.PlayHackerSound(2);
				}
				pos++;
				isSaying = false;
			});
		}
		else
		{
			txt_zimu2.DOKill();
			isSaying = false;
			txt_zimu2.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue(zimus[pos].Trim()), gameManager.player.playerdata.nickname));
			if (pos == zimus.Length - 1)
			{
				gameManager.soundManager.PlayHackerSound(2);
			}
			pos++;
		}
	}

	private IEnumerator StartShowVideo()
	{
		videoplayer.SetActive(value: true);
		yield return new WaitForSeconds(4f);
		Object.Instantiate(Resources.Load("Dialog/Hacker/hackervideoDialog04") as GameObject, homeScene.middle);
		Object.Destroy(base.gameObject);
	}

	private IEnumerator AudioPlayFinished(float time)
	{
		yield return new WaitForSeconds(time);
		ashley.SetState(3);
	}

	private void AddLog()
	{
		string c = "[" + homeScene.hB3Top.crtTime.text + "]>>>>>>>>>>>>";
		homeScene.logPanel.AddLog(c);
	}

	public void HideVideoDialog()
	{
		gameManager.homeScene.computerButtonBox.iscanclick = true;
		Object.Destroy(base.gameObject);
	}
}
