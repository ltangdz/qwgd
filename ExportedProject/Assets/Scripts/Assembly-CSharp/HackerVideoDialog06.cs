using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class HackerVideoDialog06 : MonoBehaviour
{
	public Text txt_name;

	public Text txt_zimu2;

	public string[] zimus;

	public string[] yuyin;

	public string[] replys;

	public HomeScene homeScene;

	public Image img_mouse;

	public Button btn_ringoff;

	public GameManager gameManager;

	public float pos;

	public string dataid;

	public string itemids;

	public string needbk;

	private bool hundown;

	private string eventID;

	public bool iscanclick = true;

	public SelectGroup selectGroup;

	public SpriteAnimation ashley;

	public GameObject startpc;

	[SerializeField]
	private bool isSaying;

	private bool isover;

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
		if (Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			ClickZimu();
		}
	}

	public void ClickZimu()
	{
		Debug.Log("ClickZimu");
		img_mouse.gameObject.SetActive(value: true);
		iscanclick = false;
		if (pos == 3.5f)
		{
			SetSelect(0, 2);
			return;
		}
		iscanclick = true;
		if (pos < (float)zimus.Length)
		{
			CioSaying();
		}
		else if (pos >= (float)zimus.Length && !hundown)
		{
			gameManager.musicManager.ResumeVol();
			Debug.Log(pos + "Over");
			ashley.SetState(0);
			hundown = true;
			txt_zimu2.text = "";
			gameManager.soundManager.Stop();
			gameManager.soundManager.PlaySound(20);
			GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
			gameManager.CanShowSetting(-1);
			GetComponent<Animator>().Play("ani_videoHide");
			AddLog();
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
				num = gameManager.soundManager.PlayEventFinished("110003", int.Parse(yuyin[(int)pos].Split(':')[1]));
				StartCoroutine(AudioPlayFinished(num));
			}
			float num2 = gameManager.CalculateLengthOfText(string.Format(I18N.instance.getValue(zimus[(int)pos].Trim()), gameManager.player.playerdata.nickname), txt_zimu2);
			if (num2 < 1650f)
			{
				txt_zimu2.GetComponent<RectTransform>().sizeDelta = new Vector2(num2, 100f);
			}
			else
			{
				txt_zimu2.GetComponent<RectTransform>().sizeDelta = new Vector2(1650f, 100f);
			}
			num = ((num > 0.3f) ? (num - 0.3f) : num);
			txt_zimu2.DOText(string.Format(I18N.instance.getValue(zimus[(int)pos].Trim()), gameManager.player.playerdata.nickname), num).SetEase(Ease.Linear).OnComplete(delegate
			{
				pos += 1f;
				if (pos == 6f && isover)
				{
					Debug.Log("isover");
					pos = zimus.Length + 1;
				}
				else
				{
					if (pos == 4f && !isover)
					{
						pos -= 0.5f;
					}
					else if (pos == 5f && !isover)
					{
						pos = 8f;
					}
					isSaying = false;
				}
			});
		}
		else
		{
			txt_zimu2.DOKill();
			isSaying = false;
			txt_zimu2.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue(zimus[(int)pos].Trim()), gameManager.player.playerdata.nickname));
			pos += 1f;
			if (pos == 6f && isover)
			{
				Debug.Log("isover");
				pos = zimus.Length + 1;
			}
			else if (pos == 4f && !isover)
			{
				pos -= 0.5f;
			}
			else if (pos == 5f && !isover)
			{
				pos = 8f;
			}
		}
	}

	private IEnumerator AudioPlayFinished(float time)
	{
		yield return new WaitForSeconds(time);
		ashley.SetState(0);
	}

	private void AddLog()
	{
		string c = "[" + homeScene.hB3Top.crtTime.text + "]>>>>>>>>>>>>";
		homeScene.logPanel.AddLog(c);
	}

	public void HideVideoDialog()
	{
		gameManager.homeScene.computerButtonBox.iscanclick = true;
		gameManager.homeScene.notebook.StartCloseAllDialog();
		Object.Destroy(base.gameObject);
		Object.Destroy(startpc);
	}

	private void SetSelect(int begin, int end)
	{
		StartCoroutine(StartSetSelect(begin, end));
	}

	private IEnumerator StartSetSelect(int begin, int end)
	{
		yield return new WaitForSeconds(0f);
		if (begin < 0)
		{
			yield return new WaitForSeconds(1f);
		}
		string[] array = new string[end - begin];
		for (int i = begin; i < end; i++)
		{
			array[i - begin] = replys[i];
		}
		selectGroup.gameObject.SetActive(value: true);
		selectGroup.SetSelect(array, ClickSelect);
	}

	public void ClickSelect(int poss)
	{
		if (selectGroup.iscanclick)
		{
			gameManager.soundManager.Stop();
			iscanclick = true;
			if (!gameManager.player.playerdata.cioAnwser.ContainsKey(zimus[(int)(pos - 0.5f)]))
			{
				gameManager.player.playerdata.cioAnwser.Add(zimus[(int)(pos - 0.5f)], poss);
			}
			else
			{
				gameManager.player.playerdata.cioAnwser[zimus[(int)(pos - 0.5f)]] = poss;
			}
			gameManager.saveManager.SavePlayerData();
			if (poss == 0)
			{
				pos = 4f;
			}
			else
			{
				pos += 1.5f;
			}
			Debug.Log("zizi:" + pos);
			ClickZimu();
			selectGroup.HideSelect();
		}
	}
}
