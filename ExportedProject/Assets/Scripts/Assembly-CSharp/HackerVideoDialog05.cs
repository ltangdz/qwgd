using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class HackerVideoDialog05 : MonoBehaviour
{
	public Text txt_name;

	public Text txt_zimu2;

	public string[] zimus;

	public string[] replys;

	public string[] yuyin;

	public HomeScene homeScene;

	public Image img_mouse;

	public Button btn_ringoff;

	public GameManager gameManager;

	public float pos;

	public bool iscanclick = true;

	public SelectGroup selectGroup;

	private bool hundown;

	public SpriteAnimation ashley;

	public GameObject close;

	[SerializeField]
	private bool isSaying;

	private bool isover;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
		gameManager.homeScene.eventsystem.SetActive(value: false);
		Init();
	}

	public void openClick()
	{
		gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	public void Init()
	{
		gameManager.homeScene.computerButtonBox.iscanclick = false;
		homeScene = gameManager.homeScene;
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
			SetSelect(0, 2);
			return;
		}
		iscanclick = true;
		if (pos < (float)zimus.Length)
		{
			CioSaying();
		}
		else
		{
			if (!(pos >= (float)zimus.Length) || hundown)
			{
				return;
			}
			ashley.SetState(0);
			hundown = true;
			txt_zimu2.text = "";
			gameManager.soundManager.Stop();
			gameManager.musicManager.Stop();
			gameManager.musicManager.PlayMusicLoop(3);
			gameManager.soundManager.PlaySound(20);
			if (!isover)
			{
				StartCoroutine(Over());
			}
			DOTween.To(() => gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade_Distortion, delegate(float x)
			{
				gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade_Distortion = x;
			}, 0.3f, 1f);
			DOTween.To(() => gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade_Additive, delegate(float x)
			{
				gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade_Additive = x;
			}, 0.15f, 1f).OnComplete(delegate
			{
				DOTween.To(() => gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade, delegate(float x)
				{
					gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade = x;
				}, 1f, 2f);
				DOTween.To(() => gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade_Additive, delegate(float x)
				{
					gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade_Additive = x;
				}, 1f, 2f);
				gameManager.homeScene.cameraFilterPack_Noise_TV_1.enabled = true;
			});
		}
	}

	private void CioSaying()
	{
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
				if (pos == 1f)
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
			ashley.SetState(0);
			gameManager.soundManager.Stop();
			txt_zimu2.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue(zimus[(int)pos].Trim()), gameManager.player.playerdata.nickname));
			pos += 1f;
			if (pos == 1f)
			{
				pos -= 0.5f;
			}
		}
	}

	private IEnumerator Over()
	{
		isover = true;
		yield return new WaitForSeconds(5f);
		gameManager.homeScene.cameraFilterPack_Noise_TV_1.enabled = false;
		DOTween.To(() => gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade_Distortion, delegate(float x)
		{
			gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade_Distortion = x;
		}, 0f, 1f);
		DOTween.To(() => gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade, delegate(float x)
		{
			gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade = x;
		}, 0f, 1f);
		DOTween.To(() => gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade_Additive, delegate(float x)
		{
			gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade_Additive = x;
		}, 0f, 1f);
		close.SetActive(value: true);
		yield return new WaitForSeconds(0.8f);
		Object.Destroy(base.gameObject);
		Object.Instantiate(Resources.Load("Dialog/Hacker/hackerdialog") as GameObject, gameManager.homeScene.middle);
		yield return new WaitForSeconds(1f);
		gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade_Additive = 0.01f;
		gameManager.homeScene.cameraFilterPack_Noise_TV_2.Fade_Distortion = 0.02f;
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
		gameManager.musicManager.Stop();
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
			if (poss == 0)
			{
				pos += 0.5f;
			}
			else
			{
				pos += 1.5f;
			}
			ClickZimu();
			selectGroup.HideSelect();
		}
	}

	public void HideVideoDialog()
	{
		gameManager.CanShowSetting(-1);
		gameManager.homeScene.computerButtonBox.iscanclick = true;
		Object.Destroy(base.gameObject);
	}
}
