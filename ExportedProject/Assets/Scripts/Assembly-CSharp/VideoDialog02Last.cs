using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class VideoDialog02Last : MonoBehaviour
{
	public Text txt_name;

	public Text txt_zimu2;

	public string[] zimus;

	public string[] selects;

	public string[] replys;

	public string[] yuyin;

	public string[] yuyinReplys;

	public HomeScene homeScene;

	public Image img_mouse;

	public Button btn_ringoff;

	public GameManager gameManager;

	public float pos;

	public string dataid;

	public string mailid;

	public string othermailid;

	public bool iscanclick = true;

	public SelectGroup selectGroup;

	private bool hundown;

	public SpriteAnimation ashley;

	[SerializeField]
	private bool isSaying;

	private int clickType;

	private int replyLabelID = -1;

	private int zimuIndex;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
		gameManager.homeScene.eventsystem.SetActive(value: false);
		gameManager.musicManager.LowerVol();
		Init(dataid);
	}

	public void openClick()
	{
		gameManager.homeScene.eventsystem.SetActive(value: true);
		ClickZimu();
	}

	public void Init(string dataid)
	{
		this.dataid = dataid;
		homeScene = gameManager.homeScene;
		gameManager.musicManager.LowerVol();
	}

	private void Update()
	{
		if ((Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) && !selectGroup.gameObject.activeSelf)
		{
			ClickZimu();
		}
	}

	private void StopAshleySpeak()
	{
		ashley.SetState(0);
	}

	public void ClickZimu()
	{
		img_mouse.gameObject.SetActive(value: true);
		iscanclick = false;
		if (pos == 0f)
		{
			CioSaying((int)pos);
			return;
		}
		if (pos == 1f)
		{
			if (clickType == 0)
			{
				SetSelect(0, 3);
				clickType = 1;
			}
			else
			{
				CioReplys(replyLabelID, 0);
			}
			return;
		}
		if (pos == 2f)
		{
			CioSaying(1);
			return;
		}
		if (pos == 3f)
		{
			SetSelect(3, 6);
			return;
		}
		if (pos == 4f)
		{
			CioSaying(2);
			return;
		}
		if (pos == 5f)
		{
			SetSelect(6, 9);
			return;
		}
		if (pos == 6f)
		{
			CioSaying(3);
			return;
		}
		if (pos == 7f)
		{
			if (clickType == 0)
			{
				SetSelect(9, 12);
				clickType = 1;
			}
			else
			{
				CioReplys(replyLabelID, 1);
			}
			return;
		}
		if (pos == 8f)
		{
			CioSaying(4);
			return;
		}
		iscanclick = true;
		if (pos >= (float)zimus.Length && !hundown)
		{
			gameManager.musicManager.ResumeVol();
			ashley.SetState(0);
			hundown = true;
			txt_zimu2.text = "";
			gameManager.soundManager.Stop();
			gameManager.musicManager.PlayMusicLoop(3);
			gameManager.soundManager.PlaySound(20);
			GetComponent<Animator>().Play("ani_videoHide");
			StartCloseAllDialog();
		}
	}

	private void CioSaying(int i)
	{
		zimuIndex = i;
		if (!isSaying)
		{
			isSaying = true;
			txt_zimu2.GetComponent<Text>().text = "";
			ashley.SetState(1);
			StopAllCoroutines();
			gameManager.soundManager.Stop();
			float num = 0f;
			if (zimus.Length >= 1)
			{
				num = gameManager.soundManager.PlayEventFinished("110002", int.Parse(yuyin[i].Split(':')[1]));
				StartCoroutine(AudioPlayFinished(num));
			}
			float num2 = gameManager.CalculateLengthOfText(string.Format(I18N.instance.getValue(zimus[i].Trim()), gameManager.player.playerdata.nickname), txt_zimu2);
			if (num2 < 1650f)
			{
				txt_zimu2.GetComponent<RectTransform>().sizeDelta = new Vector2(num2, 100f);
			}
			else
			{
				txt_zimu2.GetComponent<RectTransform>().sizeDelta = new Vector2(1650f, 100f);
			}
			num = ((num > 0.3f) ? (num - 0.3f) : num);
			txt_zimu2.DOText(string.Format(I18N.instance.getValue(zimus[i].Trim()), gameManager.player.playerdata.nickname), num).SetEase(Ease.Linear).OnComplete(delegate
			{
				pos += 1f;
				clickType = 0;
				isSaying = false;
			});
		}
		else
		{
			txt_zimu2.DOKill();
			isSaying = false;
			txt_zimu2.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue(zimus[i].Trim()), gameManager.player.playerdata.nickname));
			pos += 1f;
			clickType = 0;
		}
	}

	public void StartCloseAllDialog()
	{
		StartCoroutine(CloseAllDialog());
	}

	private IEnumerator CloseAllDialog()
	{
		for (int i = 0; i < base.transform.parent.childCount; i++)
		{
			if (base.transform.parent.GetChild(i).name.Contains("Clone") || base.transform.parent.GetChild(i).name.Contains("pic"))
			{
				SqlDialog component2;
				if (base.transform.parent.GetChild(i).TryGetComponent<CustomDialog>(out var component))
				{
					component.Close();
					yield return new WaitForSeconds(0.5f);
				}
				else if (base.transform.parent.GetChild(i).TryGetComponent<SqlDialog>(out component2))
				{
					component2.Close();
					yield return new WaitForSeconds(0.5f);
				}
			}
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
			array[i - begin] = selects[i];
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
			selectGroup.HideSelect();
			if (!gameManager.player.playerdata.cioAnwser.ContainsKey(zimus[zimuIndex]))
			{
				gameManager.player.playerdata.cioAnwser.Add(zimus[zimuIndex], poss);
			}
			else
			{
				gameManager.player.playerdata.cioAnwser[zimus[zimuIndex]] = poss;
			}
			gameManager.saveManager.SavePlayerData();
			if (pos == 1f)
			{
				CioReplys(poss, 0);
				return;
			}
			if (pos == 7f)
			{
				CioReplys(poss, 1);
				return;
			}
			pos += 1f;
			ClickZimu();
		}
	}

	private void CioReplys(int i, int index = -1)
	{
		string[] array = replys[index].Split(';');
		string[] array2 = yuyinReplys[index].Split(';');
		if (!isSaying)
		{
			replyLabelID = i;
			isSaying = true;
			txt_zimu2.GetComponent<Text>().text = "";
			ashley.SetState(1);
			StopAllCoroutines();
			gameManager.soundManager.Stop();
			float num = 0f;
			if (zimus.Length >= 1)
			{
				num = gameManager.soundManager.PlayEventFinished("110002", int.Parse(array2[i].Split(':')[1]));
				StartCoroutine(AudioPlayFinished(num));
			}
			float num2 = gameManager.CalculateLengthOfText(string.Format(I18N.instance.getValue(array[i].Trim()), gameManager.player.playerdata.nickname), txt_zimu2);
			if (num2 < 1650f)
			{
				txt_zimu2.GetComponent<RectTransform>().sizeDelta = new Vector2(num2, 100f);
			}
			else
			{
				txt_zimu2.GetComponent<RectTransform>().sizeDelta = new Vector2(1650f, 100f);
			}
			num = ((num > 0.3f) ? (num - 0.3f) : num);
			txt_zimu2.DOText(string.Format(I18N.instance.getValue(array[i].Trim()), gameManager.player.playerdata.nickname), num).SetEase(Ease.Linear).OnComplete(delegate
			{
				pos += 1f;
				clickType = 0;
				isSaying = false;
			});
		}
		else
		{
			txt_zimu2.DOKill();
			isSaying = false;
			txt_zimu2.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue(array[i].Trim()), gameManager.player.playerdata.nickname));
			pos += 1f;
			clickType = 0;
		}
	}

	public void HideVideoDialog()
	{
		gameManager.CanShowSetting(-1);
		Object.Instantiate(Resources.Load("Dialog/missionresultDialog") as GameObject, base.transform.parent);
		gameManager.musicManager.ResumeVol();
		Object.Destroy(base.gameObject);
	}
}
