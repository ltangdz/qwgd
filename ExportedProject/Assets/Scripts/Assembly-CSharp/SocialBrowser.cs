using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using tnt_deploy;

public class SocialBrowser : MonoBehaviour
{
	private GameManager gameManager;

	public string socialid;

	public MultiplyText txt_nickname;

	public MultiplyText txt_id;

	public Text txt_hobby;

	public Text txt_profession;

	public GameObject professionObj;

	public Text txt_sign;

	public GameObject signObj;

	public Text txt_address;

	public Image img_avatar;

	public Transform discussPanel;

	public bool isadmin;

	public int isover;

	public GameObject loginPanel;

	public Image img_littleavtar;

	public Text txt_name;

	public Button btn_logout;

	public ButtonBrowser buttonBrowser;

	public GameObject notloginPanel;

	public Button btn_login;

	public Image[] rightavatars;

	public Text[] rightnicknames;

	public Text[] rightinfors;

	public Text[] rightgoods;

	public GameObject zhedang;

	public GameObject img_lock;

	public GameObject img_logoff;

	public ScrollRect scrollRect;

	private DATA14 data14;

	public GameManager GameManager
	{
		get
		{
			if (gameManager == null)
			{
				gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			}
			return gameManager;
		}
	}

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Start()
	{
		gameManager.homeScene.socialBrowser = base.gameObject;
	}

	public void StartWelcome(DATA14 data, bool isadmin)
	{
		Debug.Log("StartWelcome");
		data14 = data;
		this.isadmin = isadmin;
		if (data14.missionID != "" && data14.missionID != " ")
		{
			gameManager.homeScene.goalDialog.CompleteItem(data14.missionID.Substring(1));
		}
		GetComponent<Animator>().Play("ani_welcome");
	}

	public void OverWelcome()
	{
		Init(data14, isadmin);
	}

	private void ExtraInfo(DATA14 data14)
	{
	}

	public void Init(DATA14 data14, bool isadmin)
	{
		ExtraInfo(data14);
		Debug.Log("Init" + isadmin);
		if (isover != 0)
		{
			return;
		}
		isover = 1;
		if (!isadmin)
		{
			GetComponent<Animator>().Play("ani_social2");
		}
		socialid = data14.ID.ToString();
		if (socialid.Equals("1400054"))
		{
			gameManager.UnlockAchievements("miranda");
		}
		else if (socialid.Equals("1400076"))
		{
			gameManager.UnlockAchievements("loveuphold");
		}
		string[] array = data14.related_avatar.Split(';');
		for (int i = 0; i < rightavatars.Length; i++)
		{
			rightavatars[i].sprite = Resources.Load<Sprite>("touxiang/" + array[i]);
		}
		string[] array2 = data14.related_nickname.Split(';');
		for (int j = 0; j < rightnicknames.Length; j++)
		{
			rightnicknames[j].GetComponent<I18NText>().updateTranslation2(array2[j]);
		}
		string[] array3 = data14.related_introduce.Split(';');
		for (int k = 0; k < rightinfors.Length; k++)
		{
			rightinfors[k].GetComponent<I18NText>().updateTranslation2(array3[k]);
		}
		string[] array4 = ((!gameManager.player.GetEventId().Equals("110004")) ? data14.likes.Split(';') : ((!data14.like.Equals("#0")) ? data14.like.Split(';') : data14.likes.Split(';')));
		for (int l = 0; l < rightgoods.Length; l++)
		{
			rightgoods[l].GetComponent<I18NText>().updateTranslation2(array4[l]);
		}
		if (data14.missionID != "" && data14.missionID != " ")
		{
			string[] array5 = data14.missionID.Substring(1).Split(';');
			gameManager.homeScene.goalDialog.CompleteItem(array5[isadmin ? 1 : 0]);
		}
		if (data14.clueID != null && !data14.clueID.Equals("") && !data14.clueID.Substring(1).Equals("0"))
		{
			txt_id.SetNewWidth(data14.user);
			if (data14.clueID.Substring(1).Equals("10074") && gameManager.player.GetEventId().Equals("110004"))
			{
				txt_id.otheritem = new string[2];
				txt_id.otheritem[0] = "10074";
				txt_id.otheritem[1] = "10369";
			}
			txt_id.SetContent3(data14.user, data14.clueID.Substring(1), data14.user);
		}
		else
		{
			txt_id.SetContent(data14.user);
		}
		if (data14.nick_clueID != null && !data14.nick_clueID.Equals("") && !data14.nick_clueID.Substring(1).Equals("0"))
		{
			string value = I18N.instance.getValue(data14.nickname);
			txt_nickname.SetNewWidth(value);
			if (data14.nick_clueID.Substring(1).Equals("10077") && gameManager.player.GetEventId().Equals("110004"))
			{
				txt_nickname.otheritem = new string[2];
				txt_nickname.otheritem[0] = "10371";
				txt_nickname.otheritem[1] = "10369";
			}
			txt_nickname.SetContent3(value, data14.nick_clueID.Substring(1), value);
		}
		else
		{
			txt_nickname.SetContent(data14.nickname);
		}
		txt_hobby.GetComponent<I18NText>().updateTranslation2(data14.hobby);
		if (data14.birth.Contains("#"))
		{
			string key = data14.birth.Substring(1);
			_ = gameManager.dataManager.dic1[key];
		}
		if (data14.profession == null || data14.profession == "")
		{
			professionObj.SetActive(value: false);
		}
		else
		{
			txt_profession.GetComponent<I18NText>().updateTranslation2(data14.profession);
		}
		if (gameManager.GameType == GameTypeEnum.DLC6 || gameManager.GameType == GameTypeEnum.DLC7)
		{
			if (data14.sign == null || data14.sign == "")
			{
				signObj.SetActive(value: false);
			}
			else
			{
				txt_sign.GetComponent<I18NText>().updateTranslation2(data14.sign);
			}
		}
		txt_address.GetComponent<I18NText>().updateTranslation2(data14.address);
		img_avatar.sprite = Resources.Load<Sprite>("touxiang/" + data14.avatar);
		if (!isadmin)
		{
			btn_login.onClick.AddListener(delegate
			{
				if (gameManager.homeScene.newbrowserDialog != null)
				{
					gameManager.homeScene.newbrowserDialog.currenttab.Close(isopenlast: false);
					gameManager.homeScene.newbrowserDialog.AddNewPanel("toothbook_login", "toothbook_login", "https://www.toothbook.com/login");
				}
			});
		}
		loginPanel.SetActive(isadmin);
		notloginPanel.SetActive(!isadmin);
		StartCoroutine(ShowVideo(data14));
		if ((gameManager.GameType == GameTypeEnum.DLC6 || gameManager.GameType == GameTypeEnum.DLC7) && data14.logoff == 1 && !isadmin)
		{
			img_logoff.SetActive(value: true);
			return;
		}
		if (data14.@lock == 1 && !isadmin)
		{
			img_lock.SetActive(value: true);
			return;
		}
		string[] array6 = ((!gameManager.player.GetEventId().Equals("110004")) ? data14.discussid.Substring(1).Split(';') : ((data14.inbox.Equals("") || data14.inbox == null) ? data14.discussid.Substring(1).Split(';') : data14.inbox.Substring(1).Split(';')));
		for (int num = 0; num < array6.Length; num++)
		{
			GameObject obj = (GameObject)Object.Instantiate(Resources.Load("socialitem"), discussPanel);
			obj.GetComponent<SocailItem1>().Init(array6[num], isadmin, gameManager);
			obj.name = "socialitem" + num;
		}
		if (isadmin)
		{
			img_littleavtar.sprite = Resources.Load<Sprite>("touxiang/" + data14.avatar);
			txt_name.GetComponent<I18NText>().updateTranslation2(data14.nickname);
			btn_logout.onClick.AddListener(delegate
			{
				if (gameManager.homeScene.newbrowserDialog != null)
				{
					gameManager.homeScene.newbrowserDialog.currenttab.Close(isopenlast: false);
					gameManager.homeScene.newbrowserDialog.AddNewPanel("toothbook_login", "toothbook_login", "https://www.toothbook.com/login");
				}
			});
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(discussPanel as RectTransform);
		if (gameManager.player.playerdata.isCourse03 == 0 && SceneManager.GetActiveScene().name == "homecourse")
		{
			gameManager.homeScene.courseManager.coursepanel03.tbscrollrect = scrollRect;
			gameManager.homeScene.courseManager.coursepanel03.tbPanel = base.gameObject;
			gameManager.homeScene.courseManager.ShowCourse3();
		}
		if (gameManager.player.playerdata.isCourse04 == 0 && SceneManager.GetActiveScene().name == "homecourse")
		{
			gameManager.homeScene.courseManager.coursepanel04.tbscrollrect = scrollRect;
		}
		if (gameManager.GameType == GameTypeEnum.DLC6 && socialid == "1410045")
		{
			gameManager.UnlockAchievements("goldenwheat");
		}
	}

	private IEnumerator ShowVideo(DATA14 d14)
	{
		if (d14.ID == 1400001 && !isadmin && !gameManager.player.playerdata.videotiplist.Contains("3700001"))
		{
			zhedang.SetActive(value: true);
		}
		else if (d14.ID == 1400022 && !isadmin && !gameManager.player.playerdata.videotiplist.Contains("3700020"))
		{
			zhedang.SetActive(value: true);
		}
		yield return new WaitForSeconds(3f);
		if (d14.ID == 1400001 && !isadmin)
		{
			zhedang.SetActive(value: false);
			gameManager.homeScene.ShowVideoTip("3700001");
			yield return new WaitForSeconds(1f);
		}
		else if (d14.ID == 1400022 && !isadmin)
		{
			zhedang.SetActive(value: false);
			gameManager.homeScene.ShowVideoTip("3700020");
			yield return new WaitForSeconds(1f);
		}
	}

	public void IsOver()
	{
		isover = 2;
	}

	private IEnumerator WelcomeBak()
	{
		base.transform.Find("welcome_back").gameObject.SetActive(value: true);
		yield return new WaitForSeconds(2f);
		base.transform.Find("welcome_back").gameObject.SetActive(value: false);
	}
}
