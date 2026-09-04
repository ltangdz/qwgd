using System.Collections;
using System.Collections.Generic;
using Aluba;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8;
using _DLC8.Main.Data;

public class VideoDialogDLC8 : MonoBehaviour
{
	public Text txt_name;

	public Text txt_zimu2;

	public Image img_mouse;

	public Button btn_ringoff;

	public GameManager gameManager;

	public int pos;

	public bool iscanclick = true;

	public SelectGroup selectGroup;

	private bool hundown;

	private List<DialogContentInfo> _dataList = new List<DialogContentInfo>();

	private DialogGroupInfo _dialogGroupData;

	public SpriteAnimation ashley;

	[SerializeField]
	private bool isSaying;

	private bool _isCanClickZimu;

	private int _tempCount;

	public Text talkNameText;

	public List<DialogContentInfo> DataList
	{
		get
		{
			if (_dataList == null)
			{
				_dataList = new List<DialogContentInfo>();
			}
			return _dataList;
		}
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: false);
		gameManager.musicManager.LowerVol();
	}

	public void Init(int groupId)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		txt_name.text = I18N.instance.getValue("^message_event0144");
		_dialogGroupData = SingletonAutoMono<DLC8DataController>.GetInstance().DialogGroupInfoManager.dialogGroupDic[groupId];
		_dataList = _dialogGroupData.contentList;
		Invoke("ClickZimu", 2f);
		gameManager.musicManager.LowerVol();
		DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.START_DIALOG, _dialogGroupData.id);
	}

	private void Update()
	{
		if ((Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !selectGroup.gameObject.activeSelf)
		{
			ClickZimu();
		}
	}

	private void OppositeSay(DialogContentInfo data40)
	{
		string value = I18N.instance.getValue(data40.name);
		string arg = value.Replace("{*Player*}", SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.NickName);
		talkNameText.text = $"{arg}： ";
		string text = $"{I18N.instance.getValue(data40.content)}".Replace("{*Player*}", SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.NickName);
		if (!isSaying)
		{
			if (value.Contains("{*Player*}"))
			{
				ashley.SetState(0);
				gameManager.soundManager.Stop();
			}
			else
			{
				ashley.SetState(1);
			}
			Debug.Log("saying");
			isSaying = true;
			txt_zimu2.GetComponent<Text>().text = "";
			StopAllCoroutines();
			gameManager.soundManager.Stop();
			float num = 2f;
			if (DataList.Count >= 1)
			{
				string sound = data40.sound;
				Debug.Log("data40Voice:" + sound);
				if (sound != "#0" && !string.IsNullOrEmpty(sound))
				{
					num = gameManager.soundManager.PlayDLCEventSound(_dialogGroupData.eventId.ToString(), _dialogGroupData.id.ToString(), sound.Substring(1));
				}
				Debug.Log("time:" + num);
				StartCoroutine(AudioPlayFinished((num == 0f) ? 3f : num));
			}
			float num2 = gameManager.CalculateLengthOfText(text, txt_zimu2);
			if (num2 < 1550f)
			{
				txt_zimu2.GetComponent<RectTransform>().sizeDelta = new Vector2(num2, 100f);
			}
			else
			{
				txt_zimu2.GetComponent<RectTransform>().sizeDelta = new Vector2(1550f, 100f);
			}
			txt_zimu2.DOText(text, num).SetEase(Ease.Linear).OnComplete(delegate
			{
				pos++;
				isSaying = false;
			});
		}
		else
		{
			txt_zimu2.DOKill();
			isSaying = false;
			txt_zimu2.GetComponent<I18NText>().updateTranslation2(text);
			pos++;
		}
	}

	public void ClickZimu()
	{
		Debug.Log(pos + ":" + DataList.Count);
		if (pos >= DataList.Count && !hundown)
		{
			gameManager.musicManager.ResumeVol();
			if (pos < DataList.Count)
			{
				ashley.SetState(0);
			}
			hundown = true;
			txt_zimu2.text = "";
			gameManager.soundManager.Stop();
			gameManager.musicManager.PlayMusicLoop(2);
			gameManager.soundManager.PlaySound(20);
			HideVideoDialog();
			return;
		}
		img_mouse.gameObject.SetActive(value: true);
		iscanclick = false;
		DialogContentInfo dialogContentInfo = DataList[pos];
		Debug.Log("id:" + dialogContentInfo.id);
		List<DialogContentInfo> list = new List<DialogContentInfo>();
		int num = pos;
		while (!string.IsNullOrEmpty(dialogContentInfo.option))
		{
			DialogContentInfo item = DataList[num];
			num++;
			list.Add(item);
		}
		if (list.Count > 0)
		{
			SelfSelect(list);
		}
		else
		{
			OppositeSay(dialogContentInfo);
		}
	}

	private void SelfSelect(List<DialogContentInfo> lists)
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
		ashley.SetState(0);
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

	public void HideVideoDialog()
	{
		GetComponent<Animator>().Play("ani_videoHide");
		DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.FINISH_DIALOG, _dialogGroupData.id);
		SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: true);
		gameManager.musicManager.ResumeVol();
		Object.Destroy(base.gameObject);
	}
}
