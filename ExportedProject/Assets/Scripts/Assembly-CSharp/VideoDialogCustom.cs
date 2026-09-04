using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DLC7;
using DLC7.DDOS;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using tnt_deploy;

public class VideoDialogCustom : MonoBehaviour
{
	public Text txt_name;

	public Text txt_zimu2;

	public Image img_mouse;

	public Button btn_ringoff;

	private GameManager gameManager;

	private int pos;

	private string dataid;

	private string mailid;

	private string _missonId;

	private string[] otherMailIds;

	public bool iscanclick = true;

	public SelectGroup selectGroup;

	private bool hundown;

	public Image normalImage;

	public Image speakImage;

	private List<DATA40> _dataList;

	private DATA39 _dialogGroupData;

	[SerializeField]
	private bool isSaying;

	private bool _showName;

	private int _event_id;

	private int _tempCount;

	public Material material;

	private bool _isStartTask;

	public InputField inputField;

	public GameObject inputNameDialog;

	public Button inputNameButton;

	public Image tipImage;

	public Text talkNameText;

	private bool _isCanClickZimu = true;

	public Text tipText;

	private bool _isHide;

	public List<DATA40> DataList
	{
		get
		{
			if (_dataList == null)
			{
				_dataList = new List<DATA40>();
			}
			return _dataList;
		}
	}

	private void Start()
	{
		speakImage.material = material;
		normalImage.material = material;
		normalImage.material.EnableKeyword("GLITCH_ON");
		speakImage.material.EnableKeyword("GLITCH_ON");
		Debug.Log("start");
		inputNameDialog.SetActive(value: false);
		inputNameButton.onClick.AddListener(Sure);
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
		gameManager.homeScene.eventsystem.SetActive(value: false);
		if (dataid == "3910001")
		{
			gameManager.saveManager.GetAllSaveList();
		}
		if (dataid == "3910008")
		{
			gameManager.musicManager.Stop();
			gameManager.musicManager.PlayMusicLoop(28);
			normalImage.material.DOFloat(30f, "_GlitchAmount", 0f);
			speakImage.material.DOFloat(30f, "_GlitchAmount", 0f);
		}
		else
		{
			normalImage.material.DOFloat(1f, "_GlitchAmount", 0f);
			speakImage.material.DOFloat(1f, "_GlitchAmount", 0f);
			gameManager.musicManager.LowerVol();
		}
	}

	private void Sure()
	{
		if (inputField.text.Length == 0)
		{
			return;
		}
		if (inputField.text.Length > 15)
		{
			ShowTip(I18N.instance.getValue("^110008_common_68"));
			return;
		}
		string[] word = DLCNameUtil.Instance.GetWord();
		for (int i = 0; i < word.Length; i++)
		{
			if (inputField.text.Contains(word[i]))
			{
				ShowTip(I18N.instance.getValue("^110008_common_93"));
				return;
			}
		}
		if (gameManager.player.playerdata.basicNameList.Contains(inputField.text))
		{
			gameManager.UnlockAchievements("yourname");
		}
		gameManager.player.playerdata.aiNameDlc7 = inputField.text;
		txt_name.text = gameManager.player.playerdata.aiNameDlc7;
		inputNameDialog.gameObject.SetActive(value: false);
		_isCanClickZimu = true;
	}

	private void ShowInputDialog()
	{
		_isCanClickZimu = false;
		inputNameDialog.gameObject.SetActive(value: true);
	}

	private void ShowTip(string name)
	{
		tipText.text = name;
		CanvasGroup component = tipImage.GetComponent<CanvasGroup>();
		component.alpha = 0f;
		tipImage.gameObject.SetActive(value: true);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(component.DOFade(1f, 0.2f));
		sequence.Append(component.DOFade(1f, 1.5f));
		sequence.Append(component.DOFade(0f, 0.4f).OnComplete(delegate
		{
			tipImage.gameObject.SetActive(value: false);
		}));
		sequence.Play();
	}

	public void openClick()
	{
		gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	public void Init(string dataid, bool showName, bool isStartTask, string email_id)
	{
		Debug.Log("Init");
		mailid = email_id;
		_isStartTask = isStartTask;
		Speak(isSpeak: false);
		_showName = showName;
		Init(dataid);
	}

	public void Init(string dataid)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.homeScene.computerButtonBox.iscanclick = false;
		this.dataid = dataid;
		txt_name.text = gameManager.player.playerdata.aiNameDlc7;
		_dialogGroupData = gameManager.dataManager.dic39[dataid];
		string[] array = _dialogGroupData.content.Substring(1).Split(';');
		Dictionary<string, DATA40> dic = gameManager.dataManager.dic40;
		DataList.Clear();
		foreach (string key in array)
		{
			DATA40 item = dic[key];
			DataList.Add(item);
		}
		_event_id = _dialogGroupData.eventid;
		Invoke("ClickZimu", 2f);
	}

	private void Update()
	{
		if (_isCanClickZimu && (Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) && !selectGroup.gameObject.activeSelf)
		{
			ClickZimu();
		}
	}

	private void SelfSelect(List<DATA40> lists)
	{
		List<string> list = new List<string>();
		for (int i = 0; i < lists.Count; i++)
		{
			list[i] = lists[i].option;
		}
		_tempCount = list.Count;
		selectGroup.gameObject.SetActive(value: true);
		selectGroup.SetSelect(list.ToArray(), ClickSelect);
	}

	private void Speak(bool isSpeak)
	{
		if (_isHide)
		{
			normalImage.gameObject.SetActive(value: false);
			speakImage.gameObject.SetActive(value: false);
			return;
		}
		normalImage.gameObject.SetActive(!isSpeak);
		speakImage.gameObject.SetActive(isSpeak);
		if (isSpeak)
		{
			speakImage.GetComponent<FrameAnimation2D>().Play();
		}
		else
		{
			normalImage.GetComponent<FrameAnimation2D>().Play();
		}
	}

	private void OppositeSay(DATA40 data40)
	{
		_ = data40.content;
		_ = data40.voice;
		string text = "";
		if (gameManager.Is_Dlc7())
		{
			text = I18N.instance.getValue(data40.name);
			if (text == "$ai$")
			{
				text = gameManager.player.playerdata.aiNameDlc7;
			}
		}
		talkNameText.text = $"{text}： ";
		string text2 = $"{I18N.instance.getValue(data40.content)}";
		if (data40.id == 4010119)
		{
			_isHide = true;
			if (speakImage.isActiveAndEnabled)
			{
				speakImage.DOFade(0f, 2f);
			}
			if (normalImage.isActiveAndEnabled)
			{
				normalImage.DOFade(0f, 2f);
			}
			txt_name.DOFade(0f, 2f);
		}
		if (data40.id == 4010120)
		{
			gameManager.musicManager.Stop();
		}
		if (data40.id == 4010121)
		{
			gameManager.musicManager.Stop();
			_isCanClickZimu = false;
			gameManager.musicManager.PlayMusic(29);
		}
		_ = data40.id;
		_ = 4010125;
		if (!isSaying)
		{
			Debug.Log("saying");
			isSaying = true;
			txt_zimu2.GetComponent<Text>().text = "";
			if (data40.id < 4010119)
			{
				Speak(I18N.instance.getValue(data40.name) == "$ai$");
			}
			StopAllCoroutines();
			gameManager.soundManager.Stop();
			float num = 0f;
			if (DataList.Count >= 1)
			{
				string voice = data40.voice;
				Debug.Log("data40Voice:" + voice);
				if (voice != "#0" && !string.IsNullOrEmpty(voice))
				{
					num = gameManager.soundManager.PlayDLCEventSound(_event_id.ToString(), dataid, voice.Substring(1));
				}
				Debug.Log("time:" + num);
				if (data40.id == 4010125)
				{
					Invoke("ShowFloatBox", num + 1f);
					Invoke("PlayEndMusic", num + 3f);
				}
				StartCoroutine(AudioPlayFinished((num == 0f) ? 3f : num));
			}
			float num2 = gameManager.CalculateLengthOfText(text2, txt_zimu2);
			if (num2 < 1550f)
			{
				txt_zimu2.GetComponent<RectTransform>().sizeDelta = new Vector2(num2, 100f);
			}
			else
			{
				txt_zimu2.GetComponent<RectTransform>().sizeDelta = new Vector2(1550f, 100f);
			}
			txt_zimu2.DOText(text2, num).SetEase(Ease.Linear).OnComplete(delegate
			{
				if (data40.id == 4010037)
				{
					ShowInputDialog();
				}
				if (dataid == "3910008" && data40.id >= 4010121 && pos < DataList.Count)
				{
					pos++;
					Debug.Log("下一句：" + DataList[pos].id + "----voice:" + DataList[pos].voice);
					isSaying = false;
					OppositeSay(DataList[pos]);
				}
				else
				{
					pos++;
					isSaying = false;
					if (dataid == "3910008" && pos == DataList.Count - 1)
					{
						Debug.Log("最后结束");
					}
				}
			});
		}
		else
		{
			txt_zimu2.DOKill();
			isSaying = false;
			txt_zimu2.GetComponent<I18NText>().updateTranslation2(text2);
			pos++;
			if (data40.id == 4010037)
			{
				ShowInputDialog();
			}
		}
	}

	private void ShowFloatBox()
	{
		gameManager.OnlyShowFloatBox();
	}

	private void PlayEndMusic()
	{
		SceneManager.LoadSceneAsync("Dlc7End");
	}

	public void ClickZimu()
	{
		Debug.Log(pos + ":" + DataList.Count);
		if (dataid == "3910008" && pos == DataList.Count - 1)
		{
			_isCanClickZimu = false;
		}
		else if (pos >= DataList.Count && !hundown)
		{
			gameManager.musicManager.ResumeVol();
			if (pos < DataList.Count && DataList[pos].id < 4010119)
			{
				Speak(isSpeak: false);
			}
			hundown = true;
			txt_zimu2.text = "";
			gameManager.soundManager.Stop();
			gameManager.musicManager.PlayMusicLoop(3);
			gameManager.soundManager.PlaySound(20);
			gameManager.player.playerdata.videotiplist.Add(dataid);
			if (_isStartTask)
			{
				ShowHome();
			}
			else
			{
				HideVideoDialog();
			}
			return;
		}
		img_mouse.gameObject.SetActive(value: true);
		iscanclick = false;
		DATA40 dATA = DataList[pos];
		Debug.Log("id:" + dATA.id);
		List<DATA40> list = new List<DATA40>();
		int num = pos;
		while (!string.IsNullOrEmpty(dATA.option))
		{
			DATA40 item = DataList[num];
			num++;
			list.Add(item);
		}
		if (list.Count > 0)
		{
			SelfSelect(list);
		}
		else
		{
			OppositeSay(dATA);
		}
	}

	private IEnumerator LowMusic()
	{
		float vol = PlayerPrefs.GetFloat("musicvol", 1f);
		gameManager.musicManager.GetComponent<AudioSource>().volume = vol;
		while (vol > 0f)
		{
			vol -= 0.02f;
			yield return new WaitForSeconds(0.02f);
			gameManager.musicManager.GetComponent<AudioSource>().volume = vol;
		}
	}

	private IEnumerator PlayMusic()
	{
		yield return new WaitForSeconds(1f);
		GetComponent<Animator>().Play("ani_videoHide");
		gameManager.musicManager.PlayMusicLoop(3);
	}

	private IEnumerator AudioPlayFinished(float time)
	{
		yield return new WaitForSeconds(time);
		Speak(isSpeak: false);
	}

	private IEnumerator LargeMusic()
	{
		float vol = 0f;
		gameManager.musicManager.GetComponent<AudioSource>().volume = vol;
		while (vol < PlayerPrefs.GetFloat("musicvol", 1f))
		{
			vol += 0.05f;
			yield return new WaitForSeconds(0.05f);
			gameManager.musicManager.GetComponent<AudioSource>().volume = vol;
		}
	}

	public void ClickSelect(int poss)
	{
		if (selectGroup.iscanclick)
		{
			gameManager.soundManager.Stop();
			iscanclick = true;
			pos += _tempCount;
			_tempCount = 0;
			ClickZimu();
			selectGroup.HideSelect();
		}
	}

	private void ShowHome()
	{
		gameManager.musicManager.ResumeVol();
		txt_zimu2.text = "";
		GetComponent<Animator>().Play("ani_videoHide");
		gameManager.soundManager.Stop();
		gameManager.musicManager.PlayMusicLoop(3);
		gameManager.soundManager.PlaySound(20);
		gameManager.player.playerdata.isShowNote = true;
		gameManager.player.playerdata.videotiplist.Add(dataid);
		gameManager.homeScene.GetTask(mailid);
		gameManager.player.playerdata.isstarttask = true;
		if (dataid == "3910001")
		{
			gameManager.player.playerdata.canweizhuangcondition.Add("31000");
		}
		gameManager.homeScene.StartTask2();
		DLCEventManager.Instance.NoticeShowAITalk(isShow: true);
	}

	public void HideVideoDialog()
	{
		if (PlayerPrefs.GetFloat("musicvol", 1f) > 0f)
		{
			gameManager.musicManager.ResumeVol();
		}
		gameManager.player.playerdata.isstarttask = true;
		gameManager.CanShowSetting(-1);
		gameManager.homeScene.computerButtonBox.iscanclick = true;
		gameManager.homeScene.StartTask2();
		string itemid = _dialogGroupData.itemid;
		if (dataid == "3910004")
		{
			gameManager.player.playerdata.isShowNote = true;
			gameManager.player.playerdata.toolDLC7 = new string[8] { "1", "2", "5", "9", "10", "12", "15", "0" };
			DLCEventManager.Instance.NoticeRefreshTool();
			gameManager.homeScene.ShowNoteDlc7();
		}
		if (dataid == "3910005")
		{
			DLCEventManager.Instance.NoticeAITalk("3910025");
		}
		if (dataid == "3910006")
		{
			gameManager.player.playerdata.isShowNote = true;
			gameManager.player.playerdata.showTitanButton = true;
			gameManager.homeScene.ShowNoteDlc7();
		}
		if (itemid != "#0")
		{
			gameManager.homeScene.notebook.AddNewItems(itemid.Substring(1).Split(';'));
		}
		gameManager.saveManager.SavePlayerData(isshowlogo: true, isForce: true);
		Object.Destroy(base.gameObject);
	}
}
