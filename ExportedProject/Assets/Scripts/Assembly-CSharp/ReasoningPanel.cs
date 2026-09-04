using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DLC7.Reasoning;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ReasoningPanel : MonoBehaviour
{
	public string id;

	public GameManager gameManager;

	public Image img_bkmask;

	public List<ReasoningItem> reasoningItems;

	public List<SelectItem> selectItems;

	public Transform toppanel;

	public Transform bottompanel;

	public Text txt_top;

	public Button btn_begin;

	public Button btn_next;

	public Button btn_sure;

	public Button btn_exit;

	public Image img_middlebk;

	public Image img_bklight;

	public string[] tipstrs;

	public int[] sounds;

	public List<string> itemidlist = new List<string>();

	public List<string> compeletelistid = new List<string>();

	public string prevideotipid;

	public string lastvideotipid;

	public string lastvideotipid2;

	public string lastvideoneedcallid;

	public string missionid;

	public GameObject middlePanel;

	public bool isfinished;

	public GameObject itemGroup;

	public GameObject resultGroup;

	public int reasonIndex;

	public ReasoningMiddle reasoningMiddle;

	[SerializeField]
	private GameObject allmiddlecontent;

	[SerializeField]
	private GameObject img_result;

	[SerializeField]
	private Text txt_result;

	public int type;

	public bool playCioSound = true;

	public Button skipButton;

	private bool isover;

	public void ShowPreVideo()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (!prevideotipid.Equals("") && !gameManager.player.playerdata.videotiplist.Contains(prevideotipid))
		{
			gameManager.homeScene.ShowVideoTip(prevideotipid, missionid);
			prevideotipid = "";
		}
	}

	public bool IsAllCompeleted()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (prevideotipid.Equals("-1"))
		{
			prevideotipid = "";
			return false;
		}
		if (!prevideotipid.Equals(""))
		{
			if (!gameManager.player.playerdata.videotiplist.Contains(prevideotipid))
			{
				gameManager.homeScene.ShowVideoTip(prevideotipid, missionid);
				prevideotipid = "";
			}
			else
			{
				Show();
				isfinished = true;
			}
		}
		else
		{
			Show();
			isfinished = true;
		}
		return true;
	}

	public void Show()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
		base.transform.parent.SetAsLastSibling();
		base.gameObject.SetActive(value: true);
		Init();
	}

	private void Init()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		List<string> reasoninglist = gameManager.player.playerdata.reasoninglist;
		for (int i = 0; i < reasoninglist.Count; i++)
		{
			Debug.Log(reasoninglist[i].ToString());
		}
		Debug.Log("init");
		gameManager.musicManager.PlayMusicLoop(4);
		Invoke("Begin", 0.5f);
		StartCoroutine(InitReasonItem());
	}

	public bool GetResult()
	{
		gameManager.soundManager.PlaySound(22);
		if (reasoningMiddle.IsAllRight())
		{
			for (int i = 0; i < reasoningItems.Count; i++)
			{
				if (reasoningItems[i] != null)
				{
					reasoningItems[i].GetOut();
				}
			}
			Debug.Log("showallresult0");
			StartCoroutine(ShowAllResult());
			return true;
		}
		SetTopContent(tipstrs[1], sounds[1]);
		Debug.Log("SetTopContent");
		return false;
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (skipButton != null)
		{
			skipButton.onClick.AddListener(Skip);
		}
		btn_begin.onClick.AddListener(delegate
		{
			if (gameManager.Is_Dlc7())
			{
				for (int i = 0; i < reasoningItems.Count; i++)
				{
					reasoningItems[i].StartMoveLeft();
				}
				middlePanel.SetActive(value: true);
				btn_begin.gameObject.SetActive(value: false);
				img_middlebk.GetComponent<CanvasGroup>().DOFade(1f, 1f);
			}
			else if (gameManager.Is_Dlc6() && (missionid == "2010001" || missionid == "2010002" || missionid == "2010004"))
			{
				gameManager.soundManager.Stop();
				gameManager.soundManager.PlaySound(22);
				gameManager.CanShowSetting(-1);
				StopAllCoroutines();
				gameManager.musicManager.PlayMusicLoop(3);
				gameManager.soundManager.Stop();
				gameManager.homeScene.eventsystem.SetActive(value: true);
				StopAllCoroutines();
				gameManager.musicManager.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicvol", 1f);
				if (gameManager.reasoningManager.list.Contains(this))
				{
					gameManager.reasoningManager.list.Remove(this);
				}
				ReBuild();
				Object.Destroy(base.gameObject);
				gameManager.homeScene.ShowVideoTip(lastvideotipid, missionid);
			}
			else
			{
				for (int j = 0; j < reasoningItems.Count; j++)
				{
					reasoningItems[j].StartMoveLeft();
				}
				middlePanel.SetActive(value: true);
				reasoningMiddle.Init();
				btn_begin.gameObject.SetActive(value: false);
				if (gameManager.Is_Dlc6())
				{
					btn_next.gameObject.SetActive(value: true);
				}
			}
		});
		btn_exit.onClick.AddListener(delegate
		{
			if (gameManager.IsAllDlc())
			{
				gameManager.soundManager.Stop();
				gameManager.soundManager.PlaySound(22);
				gameManager.CanShowSetting(-1);
				StopAllCoroutines();
				gameManager.musicManager.PlayMusicLoop(3);
				gameManager.soundManager.Stop();
				gameManager.homeScene.eventsystem.SetActive(value: true);
				StopAllCoroutines();
				gameManager.musicManager.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicvol", 1f);
				if (gameManager.reasoningManager.list.Contains(this))
				{
					gameManager.reasoningManager.list.Remove(this);
				}
				ReBuild();
				Object.Destroy(base.gameObject);
			}
			else
			{
				gameManager.soundManager.Stop();
				gameManager.soundManager.PlaySound(22);
				gameManager.homeScene.eventsystem.SetActive(value: false);
				img_bklight.gameObject.SetActive(value: true);
				img_bklight.transform.DOScale(new Vector2(5f, 5f), 1.5f).SetEase(Ease.OutQuart).OnComplete(delegate
				{
					gameManager.CanShowSetting(-1);
					StopAllCoroutines();
					gameManager.musicManager.PlayMusicLoop(3);
					gameManager.soundManager.Stop();
					GetComponent<CanvasGroup>().DOFade(0f, 1.2f).OnComplete(delegate
					{
						gameManager.homeScene.eventsystem.SetActive(value: true);
						StopAllCoroutines();
						gameManager.musicManager.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicvol", 1f);
						if (gameManager.reasoningManager.list.Contains(this))
						{
							gameManager.reasoningManager.list.Remove(this);
						}
						ReBuild();
						Object.Destroy(base.gameObject);
					});
				});
			}
		});
	}

	private void Skip()
	{
	}

	private void NoticeResult(string obj)
	{
		if (obj == id)
		{
			StartCoroutine(ShowAllResult());
		}
	}

	private void ReBuild()
	{
		GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("ReasoningPanel/ReasoningPanel" + id), gameManager.reasoningManager.transform);
		gameObject.name = "ReasoningPanel" + id;
		gameManager.reasoningManager.list.Add(gameObject.GetComponent<ReasoningPanel>());
	}

	private IEnumerator ShowAllResult()
	{
		Debug.Log("showallresult");
		img_middlebk.rectTransform.DOSizeDelta(new Vector2(1553f, img_middlebk.rectTransform.sizeDelta.y), 0.5f);
		yield return new WaitForSeconds(0.2f);
		if (gameManager.IsAllDlc())
		{
			if (tipstrs.Length != 0)
			{
				SetTopContent(tipstrs[tipstrs.Length - 1], 0);
			}
		}
		else
		{
			SetTopContent(tipstrs[2], sounds[2]);
		}
		yield return new WaitForSeconds(0.5f);
		allmiddlecontent.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		allmiddlecontent.SetActive(value: false);
		img_result.SetActive(value: true);
		img_result.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		img_result.transform.DOScaleY(1f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		txt_result.DOFade(1f, 0.2f);
		yield return new WaitForSeconds(0.3f);
		if (!gameManager.player.playerdata.canweizhuangcondition.Contains(id))
		{
			gameManager.player.playerdata.canweizhuangcondition.Add(id);
		}
		if (!gameManager.player.playerdata.reasoninglist.Contains(id))
		{
			gameManager.player.playerdata.reasoninglist.Add(id);
		}
		gameManager.saveManager.SavePlayerData();
		if (gameManager.Is_Dlc6())
		{
			if (id == "4013")
			{
				StartCoroutine(ContactHerbert());
			}
		}
		else
		{
			isover = true;
		}
	}

	private IEnumerator ContactHerbert()
	{
		yield return new WaitForSeconds(3f);
		gameManager.soundManager.Stop();
		gameManager.soundManager.PlaySound(22);
		gameManager.CanShowSetting(-1);
		StopAllCoroutines();
		gameManager.musicManager.PlayMusicLoop(3);
		gameManager.soundManager.Stop();
		gameManager.homeScene.eventsystem.SetActive(value: true);
		StopAllCoroutines();
		gameManager.musicManager.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicvol", 1f);
		if (gameManager.reasoningManager.list.Contains(this))
		{
			gameManager.reasoningManager.list.Remove(this);
		}
		ReBuild();
		gameManager.homeScene.notebook.btn_submit.interactable = true;
		gameManager.player.playerdata.isovertask = true;
		gameManager.homeScene.goalDialog.CompeleteOneItem(missionid);
		gameManager.saveManager.SavePlayerData();
		Object.Destroy(base.gameObject);
	}

	public void SetTopContent(string key, int soundIndex)
	{
		txt_top.text = "";
		float num = CalculateLengthOfText(I18N.instance.getValue(key));
		float y = 120f;
		if (num > 1650f)
		{
			num = 1650f;
		}
		txt_top.rectTransform.sizeDelta = new Vector2(num, y);
		txt_top.DOText(I18N.instance.getValue(key), 1f);
		if (playCioSound)
		{
			gameManager.soundManager.PlayEvent(gameManager.player.GetEventId(), soundIndex);
		}
	}

	private void Begin()
	{
		img_bkmask.DOFade(0f, 0.5f);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(toppanel.DOLocalMoveY(480f, 1f));
		sequence.Append(toppanel.GetComponent<CanvasGroup>().DOFade(1f, 0.2f));
		sequence.Play();
		Sequence sequence2 = DOTween.Sequence();
		sequence2.Append(bottompanel.DOLocalMoveY(-480f, 1f));
		sequence2.Append(bottompanel.GetComponent<CanvasGroup>().DOFade(1f, 0.2f));
		sequence2.Play();
	}

	private IEnumerator InitReasonItem()
	{
		yield return new WaitForSeconds(1.5f);
		for (int i = 0; i < reasoningItems.Count; i++)
		{
			reasoningItems[i].Init();
		}
		yield return new WaitForSeconds(1.5f);
		bool flag = true;
		for (int j = 0; j < itemidlist.Count; j++)
		{
			if (!gameManager.player.playerdata.itemlist.Contains(itemidlist[j]) || gameManager.isbug)
			{
				flag = false;
				break;
			}
		}
		if (flag || gameManager.isbug)
		{
			btn_begin.interactable = false;
			btn_begin.gameObject.SetActive(value: true);
			btn_begin.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).OnComplete(delegate
			{
				btn_begin.interactable = true;
			});
			btn_exit.gameObject.SetActive(value: false);
		}
		else
		{
			btn_exit.interactable = false;
			btn_exit.gameObject.SetActive(value: true);
			btn_exit.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).OnComplete(delegate
			{
				btn_exit.interactable = true;
			});
			btn_begin.gameObject.SetActive(value: false);
		}
	}

	private float CalculateLengthOfText(string message)
	{
		float num = 0f;
		Font font = txt_top.font;
		font.RequestCharactersInTexture(message, txt_top.fontSize, txt_top.fontStyle);
		CharacterInfo info = default(CharacterInfo);
		char[] array = message.ToCharArray();
		foreach (char ch in array)
		{
			font.GetCharacterInfo(ch, out info, txt_top.fontSize);
			num += (float)info.advance;
		}
		return num;
	}

	private void ReasoningOver()
	{
		isover = false;
		gameManager.homeScene.eventsystem.SetActive(value: false);
		img_bklight.gameObject.SetActive(value: true);
		if (!gameManager.player.playerdata.reasoninglist.Contains(id))
		{
			gameManager.player.playerdata.reasoninglist.Add(id);
			gameManager.saveManager.SavePlayerData(isshowlogo: true, isForce: true);
		}
		img_bklight.transform.DOScale(new Vector2(5f, 5f), 1.5f).SetEase(Ease.OutQuart).OnComplete(delegate
		{
			StopAllCoroutines();
			gameManager.musicManager.PlayMusicLoop(3);
			gameManager.soundManager.Stop();
			GetComponent<CanvasGroup>().DOFade(0f, 1.2f).OnComplete(delegate
			{
				gameManager.CanShowSetting(-1);
				if (!gameManager.player.playerdata.canweizhuangcondition.Contains(id))
				{
					gameManager.player.playerdata.canweizhuangcondition.Add(id);
				}
				gameManager.istaohuashow = false;
				if (string.IsNullOrEmpty(lastvideotipid2) || string.IsNullOrEmpty(lastvideoneedcallid))
				{
					gameManager.homeScene.ShowVideoTip(lastvideotipid, missionid);
				}
				else if (!string.IsNullOrEmpty(lastvideoneedcallid))
				{
					if (gameManager.player.playerdata.phoneCall.Contains(lastvideoneedcallid))
					{
						gameManager.homeScene.ShowVideoTip(lastvideotipid, missionid);
					}
					else
					{
						gameManager.homeScene.ShowVideoTip(lastvideotipid2, missionid);
					}
				}
				if (!missionid.Equals(""))
				{
					gameManager.homeScene.goalDialog.CompeleteOneItem(missionid);
				}
				gameManager.homeScene.eventsystem.SetActive(value: true);
				StopAllCoroutines();
				gameManager.musicManager.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicvol", 1f);
				base.gameObject.SetActive(value: false);
			});
		});
	}

	private void Update()
	{
		if (Input.anyKeyDown && isover)
		{
			ReasoningOver();
		}
	}

	private void Awake()
	{
		DLC7.Reasoning.ReasoningManager.Instance.onNoticeResult += NoticeResult;
	}

	private void OnDestroy()
	{
		DLC7.Reasoning.ReasoningManager.Instance.onNoticeResult -= NoticeResult;
	}
}
