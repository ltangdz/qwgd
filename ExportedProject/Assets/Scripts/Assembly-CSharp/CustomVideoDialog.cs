using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class CustomVideoDialog : MonoBehaviour
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

	private bool isshowreasoning;

	private string missionid = "";

	private bool hundown;

	private string eventID;

	public SpriteAnimation ashley;

	public bool isstart;

	[SerializeField]
	private bool isSaying;

	private void Start()
	{
		gameManager.CanShowSetting(1);
		gameManager.homeScene.computerButtonBox.iscanclick = false;
		openClick();
	}

	public void openClick()
	{
		gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	public void Init(string dataid, bool isshowreasoning, string missionid)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.musicManager.LowerVol();
		eventID = gameManager.player.GetEventId();
		this.dataid = dataid;
		this.missionid = missionid;
		this.isshowreasoning = isshowreasoning;
		homeScene = gameManager.homeScene;
		DATA39 dATA = gameManager.dataManager.dic39[dataid];
		needbk = dATA.needbk;
		itemids = dATA.itemid.Substring(1);
		zimus = dATA.content.Split(';');
		if (dATA.videoid != "")
		{
			yuyin = dATA.videoid.Split(';');
		}
		if (needbk != "")
		{
			GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f);
		}
		Invoke("StartZimu", 1.5f);
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
		{
			ClickZimu();
		}
	}

	private void StartZimu()
	{
		if (!isstart)
		{
			isstart = true;
		}
		ClickZimu();
	}

	public void ClickZimu()
	{
		if (!isstart)
		{
			return;
		}
		img_mouse.gameObject.SetActive(value: true);
		if (pos < zimus.Length)
		{
			if (!isSaying)
			{
				txt_zimu2.GetComponent<Text>().text = "";
				isSaying = true;
				gameManager.soundManager.Stop();
				DATA39 dATA = gameManager.dataManager.dic39[dataid];
				if (dATA.look.Trim() == "" || dATA.look.Trim() == null)
				{
					ashley.SetState(1);
				}
				else
				{
					string s = dATA.look.Substring(1).Split(';')[pos];
					ashley.SetState(int.Parse(s));
				}
				StopAllCoroutines();
				float num = 0f;
				if (yuyin.Length >= 1)
				{
					num = gameManager.soundManager.PlayEventFinished(eventID, int.Parse(yuyin[pos].Split(':')[1]));
					StartCoroutine(AudioPlayFinished(num));
				}
				float num2 = gameManager.CalculateLengthOfText(string.Format(I18N.instance.getValue(zimus[pos].Trim()).Replace("<color=#CC1414>", "").Replace("<color=#ff0000>", "")
					.Replace("</color>", ""), gameManager.player.playerdata.nickname), txt_zimu2);
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
					pos++;
					isSaying = false;
				});
			}
			else
			{
				txt_zimu2.DOKill();
				isSaying = false;
				txt_zimu2.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue(zimus[pos].Trim()), gameManager.player.playerdata.nickname));
				pos++;
			}
		}
		else if (pos == zimus.Length && !hundown)
		{
			if (dataid.Equals("3700003"))
			{
				gameManager.musicManager.ResumeVol();
				ashley.SetState(0);
				hundown = true;
				txt_zimu2.text = "";
				gameManager.soundManager.Stop();
				gameManager.soundManager.PlaySound(20);
				GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
				gameManager.CanShowSetting(-1);
				GetComponent<Animator>().Play("ani_videoHide");
				if (dataid != "3700052")
				{
					gameManager.player.playerdata.videotiplist.Add(dataid);
				}
				AddLog();
				gameManager.homeScene.goalDialog.goalitemlist["2000001"].AddHighLight();
				return;
			}
			if (dataid.Equals("3700061"))
			{
				Invoke("LoadyulunEnterBtnPrefab", 0.3f);
				gameManager.musicManager.ResumeVol();
				ashley.SetState(0);
				hundown = true;
				txt_zimu2.text = "";
				gameManager.soundManager.Stop();
				gameManager.soundManager.PlaySound(20);
				GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
				gameManager.CanShowSetting(-1);
				GetComponent<Animator>().Play("ani_videoHide");
				gameManager.player.playerdata.videotiplist.Add(dataid);
				AddLog();
				return;
			}
			gameManager.musicManager.ResumeVol();
			ashley.SetState(0);
			hundown = true;
			txt_zimu2.text = "";
			gameManager.soundManager.Stop();
			gameManager.soundManager.PlaySound(20);
			GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
			gameManager.CanShowSetting(-1);
			if (dataid != "3700083")
			{
				GetComponent<Animator>().Play("ani_videoHide");
			}
			if (dataid != "3700084" && dataid != "3700052" && dataid != "3700057" && dataid != "3700071" && dataid != "3700072" && dataid != "3700065" && dataid != "3700066" && dataid != "3700075" && dataid != "3700076")
			{
				gameManager.player.playerdata.videotiplist.Add(dataid);
			}
			if (dataid == "3700071")
			{
				GameObject obj = Object.Instantiate(Resources.Load<GameObject>("zhadan/zhadanInvade"), gameManager.homeScene.middle);
				obj.GetComponent<ZhadanInvade>().Show();
				obj.GetComponent<ZhadanInvade>().userid = "3300010";
				obj.GetComponent<ZhadanInvade>().PojieSuccess();
			}
			if (dataid == "3700072")
			{
				Object.Instantiate(Resources.Load<GameObject>("zhadan/newzhadanlogin"), gameManager.homeScene.middle).GetComponent<NewZhadanLogin>().Init(isSucce: true, "3300011");
			}
			if (dataid == "3700084" && gameManager.homeScene.duikangDialog != null)
			{
				gameManager.homeScene.duikangDialog.StartSaySomething();
			}
			StartCoroutine(HideVideo());
			AddLog();
		}
		else if (pos == zimus.Length + 1 && dataid == "3700006")
		{
			StartCoroutine(ShowEndPanel());
		}
	}

	private void LoadyulunEnterBtnPrefab()
	{
		Object.Instantiate(Resources.Load<GameObject>("Dialog/Yulun/yulunEnterBtn"), gameManager.homeScene.middle);
		gameManager.player.playerdata.isCanPlayYulun = true;
		gameManager.saveManager.SavePlayerData();
	}

	private IEnumerator ShowEndPanel()
	{
		gameManager.ShowFloatBox("white");
		yield return new WaitForSeconds(2f);
		gameManager.homeScene.cameraFilterPack_fx_Glitch1.enabled = false;
		Object.Instantiate(Resources.Load<GameObject>("shiwanend"), gameManager.homeScene.middle);
		Object.Destroy(base.gameObject);
	}

	private IEnumerator DemoEndAni()
	{
		gameManager.homeScene.cameraFilterPack_fx_Glitch1.enabled = true;
		gameManager.homeScene.cameraFilterPack_fx_Glitch1.Glitch = 0.5f;
		yield return new WaitForSeconds(0.5f);
		gameManager.homeScene.cameraFilterPack_fx_Glitch1.Glitch = 0f;
		yield return new WaitForSeconds(1f);
		gameManager.homeScene.cameraFilterPack_fx_Glitch1.Glitch = 1f;
		yield return new WaitForSeconds(0.5f);
		gameManager.homeScene.cameraFilterPack_fx_Glitch1.Glitch = 0f;
		yield return new WaitForSeconds(0.5f);
		gameManager.homeScene.cameraFilterPack_fx_Glitch1.Glitch = 1f;
	}

	private IEnumerator ChangeToEnd()
	{
		gameManager.ShowFloatBox("white");
		yield return new WaitForSeconds(2f);
	}

	private IEnumerator AudioPlayFinished(float time)
	{
		yield return new WaitForSeconds(time);
		ashley.SetState(0);
	}

	private void AddLog()
	{
		string text = "[" + homeScene.hB3Top.crtTime.text + "]>>>>>>>>>>>>";
		DATA39 dATA = gameManager.dataManager.dic39[dataid];
		text += string.Format(I18N.instance.getValue("^logtip02"), I18N.instance.getValue("^CIO_Name"), I18N.instance.getValue(dATA.summary));
		homeScene.logPanel.AddLog(text);
	}

	private IEnumerator HideVideo()
	{
		yield return new WaitForSeconds(0.25f);
		if (dataid == "3700075" || dataid == "3700066" || dataid == "3700076")
		{
			gameManager.ShowFailedBlack();
		}
	}

	public void HideVideoDialog()
	{
		gameManager.musicManager.ResumeVol();
		gameManager.homeScene.computerButtonBox.iscanclick = true;
		if (isshowreasoning && dataid.Equals("3700003") && gameManager.player.playerdata.isCourse16 == 0)
		{
			homeScene.courseManager.ShowCourse16();
		}
		if (!missionid.Equals(""))
		{
			gameManager.homeScene.goalDialog.CompleteItem(missionid);
		}
		if (!itemids.Equals("0"))
		{
			int num = int.Parse(gameManager.dataManager.dic1[itemids].role.Substring(1));
			if (num >= 3100036 && num <= 3100047)
			{
				gameManager.homeScene.zhibojiannotebook.AddNewItems(itemids.Split(';'), isneedhighlight: true);
			}
			else
			{
				gameManager.homeScene.notebook.AddNewItems(itemids.Split(';'), isneedhighlight: true);
			}
		}
		DATA39 dATA = gameManager.dataManager.dic39[dataid];
		if (!dATA.emailid.Equals("#0") && dATA.emailid != "")
		{
			gameManager.homeScene.mailTip.SetMail("admin", dATA.emailid.Substring(1));
		}
		gameManager.homeScene.ShowNextVideo();
		if (dataid == "3700065")
		{
			gameManager.homeScene.SendMail1("1500089");
		}
		Object.Destroy(base.gameObject);
	}
}
