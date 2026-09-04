using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class VideoDialog3700064 : MonoBehaviour
{
	public Text txt_name;

	public Text txt_zimu2;

	public string[] zimus;

	public string[] replys;

	public string[] yuyin;

	public int[] biaoqing;

	public HomeScene homeScene;

	public Image img_mouse;

	public Button btn_ringoff;

	public GameManager gameManager;

	public float pos;

	public string dataid;

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
		if (gameManager.homeScene.houtaiPanel != null)
		{
			gameManager.homeScene.houtaiPanel.Stop();
		}
		Init(dataid);
	}

	public void openClick()
	{
		gameManager.homeScene.eventsystem.SetActive(value: true);
		ClickZimu();
	}

	public void Init(string dataid)
	{
		gameManager.homeScene.computerButtonBox.iscanclick = false;
		this.dataid = dataid;
		homeScene = gameManager.homeScene;
		Debug.Log("data39:" + dataid);
		gameManager.musicManager.LowerVol();
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
		if (pos == 2.5f)
		{
			SetSelect(0, 1);
			return;
		}
		if (pos == 4.5f)
		{
			SetSelect(1, 2);
			return;
		}
		if (pos == 6.5f)
		{
			SetSelect(2, 4);
			return;
		}
		iscanclick = true;
		if (pos < (float)zimus.Length)
		{
			if (!isSaying)
			{
				isSaying = true;
				txt_zimu2.GetComponent<Text>().text = "";
				ashley.SetState(biaoqing[(int)pos]);
				StopAllCoroutines();
				gameManager.soundManager.Stop();
				float num = 0f;
				if (yuyin.Length >= 1)
				{
					num = gameManager.soundManager.PlayEventFinished(gameManager.player.GetEventId(), int.Parse(yuyin[(int)pos].Split(':')[1]));
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
					if (pos == 3f)
					{
						pos -= 0.5f;
					}
					else if (pos == 5f)
					{
						pos -= 0.5f;
					}
					else if (pos == 7f)
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
				if (pos == 3f)
				{
					pos -= 0.5f;
				}
				else if (pos == 5f)
				{
					pos -= 0.5f;
				}
				else if (pos == 7f)
				{
					pos -= 0.5f;
				}
			}
		}
		else if (pos >= (float)zimus.Length && !hundown)
		{
			gameManager.homeScene.isshowvideo = false;
			gameManager.musicManager.ResumeVol();
			gameManager.soundManager.Stop();
			ashley.Stop();
			hundown = true;
			txt_zimu2.text = "";
			GetComponent<Animator>().Play("ani_videoHide");
			gameManager.soundManager.PlaySound(20);
		}
	}

	private void HideBlack()
	{
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
		ashley.Stop();
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
		if (!selectGroup.iscanclick)
		{
			return;
		}
		gameManager.soundManager.Stop();
		iscanclick = true;
		pos += 0.5f;
		if (pos == 7f)
		{
			if (poss == 0)
			{
				Object.Instantiate(Resources.Load<GameObject>("zhadan/zhadancodebox"), gameManager.homeScene.middle);
			}
			else
			{
				gameManager.homeScene.zhadanInvoke.Failed();
			}
		}
		ClickZimu();
		selectGroup.HideSelect();
	}

	public void HideVideoDialog()
	{
		gameManager.CanShowSetting(-1);
		gameManager.homeScene.computerButtonBox.iscanclick = true;
		gameManager.musicManager.ResumeVol();
		gameManager.saveManager.SavePlayerData();
		Object.Destroy(base.gameObject);
	}
}
