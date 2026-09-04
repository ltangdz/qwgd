using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class VideoDialog06 : MonoBehaviour
{
	public Text txt_name;

	public Text txt_zimu2;

	public string[] zimus;

	public string[] replys;

	public string[] yuyin;

	public string[] looks;

	public HomeScene homeScene;

	public Image img_mouse;

	public Button btn_ringoff;

	public GameManager gameManager;

	public float pos;

	public string dataid;

	public string mailid;

	public string[] otherMailIds;

	public bool iscanclick = true;

	public SelectGroup selectGroup;

	private bool hundown;

	public SpriteAnimation ashley;

	[SerializeField]
	private bool isSaying;

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
	}

	public void Init(string dataid)
	{
		gameManager.homeScene.computerButtonBox.iscanclick = false;
		this.dataid = dataid;
		homeScene = gameManager.homeScene;
		DATA39 dATA = gameManager.dataManager.dic39[dataid];
		zimus = dATA.content.Split(';');
		yuyin = dATA.videoid.Split(';');
		yuyin = new string[20];
		for (int i = 0; i < 16; i++)
		{
			yuyin[i] = string.Concat(23 + i);
		}
		for (int j = 16; j < 20; j++)
		{
			yuyin[j] = string.Concat(88 + j - 16);
		}
		replys = dATA.reply.Split(';');
		looks = dATA.look.Split(';');
		Invoke("ClickZimu", 2f);
	}

	private void Update()
	{
		if ((Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) && !selectGroup.gameObject.activeSelf)
		{
			ClickZimu();
		}
	}

	public void ClickZimu()
	{
		img_mouse.gameObject.SetActive(value: true);
		iscanclick = false;
		if (pos == 0.5f)
		{
			SetSelect(0, 1);
			return;
		}
		if (pos == 5.5f)
		{
			SetSelect(1, 2);
			return;
		}
		if (pos == 12.5f)
		{
			SetSelect(2, 3);
			return;
		}
		if (pos == 16.5f)
		{
			SetSelect(3, 4);
			return;
		}
		iscanclick = true;
		if (pos < (float)zimus.Length)
		{
			if (!isSaying)
			{
				isSaying = true;
				ashley.SetState(int.Parse(looks[(int)pos]));
				txt_zimu2.GetComponent<Text>().text = "";
				StopAllCoroutines();
				gameManager.soundManager.Stop();
				float num = 0f;
				if (zimus.Length >= 1)
				{
					num = gameManager.soundManager.PlayEventFinished("110006", int.Parse(yuyin[(int)pos]));
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
					if (pos == 1f || pos == 6f || pos == 13f || pos == 17f)
					{
						pos -= 0.5f;
					}
					isSaying = false;
				});
			}
			else
			{
				txt_zimu2.DOKill();
				isSaying = false;
				txt_zimu2.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue(zimus[(int)pos].Trim()), gameManager.player.playerdata.nickname));
				pos += 1f;
				if (pos == 1f || pos == 6f || pos == 13f || pos == 17f)
				{
					pos -= 0.5f;
				}
			}
		}
		else if (pos >= (float)zimus.Length && !hundown)
		{
			gameManager.musicManager.ResumeVol();
			ashley.SetState(0);
			hundown = true;
			txt_zimu2.text = "";
			GetComponent<Animator>().Play("ani_videoHide");
			gameManager.soundManager.Stop();
			gameManager.musicManager.PlayMusicLoop(3);
			gameManager.soundManager.PlaySound(20);
			gameManager.player.playerdata.videotiplist.Add(dataid);
			otherMailIds = new string[2] { "1510010", "1510011" };
			homeScene.StartTasks(mailid, otherMailIds);
			gameManager.player.playerdata.isstarttask = true;
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
			pos += 0.5f;
			ClickZimu();
			selectGroup.HideSelect();
		}
	}

	public void HideVideoDialog()
	{
		if (PlayerPrefs.GetFloat("musicvol", 1f) > 0f)
		{
			gameManager.musicManager.ResumeVol();
		}
		gameManager.CanShowSetting(-1);
		gameManager.homeScene.computerButtonBox.iscanclick = true;
		Object.Destroy(base.gameObject);
	}
}
