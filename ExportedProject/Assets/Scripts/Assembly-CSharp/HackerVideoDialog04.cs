using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class HackerVideoDialog04 : MonoBehaviour
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

	public GameObject van;

	public GameObject close;

	public GameObject img_black;

	public Canvas img_bottom;

	[SerializeField]
	private bool isSaying;

	private bool isover;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
		gameManager.musicManager.PlayMusicLoop(10, isneedlow: true);
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
		if (pos == 1.5f)
		{
			SetSelect(0, 1);
			return;
		}
		if (pos == 4.5f)
		{
			SetSelect(1, 2);
			return;
		}
		if (pos == 10.5f)
		{
			SetSelect(2, 5);
			return;
		}
		if (pos == 12.5f)
		{
			SetSelect(5, 8);
			return;
		}
		if (pos == 17.5f)
		{
			SetSelect(8, 9);
			return;
		}
		if (pos == 20.5f)
		{
			SetSelect(9, 11);
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
			hundown = true;
			txt_zimu2.text = "";
			GetComponent<Animator>().Play("ani_videoHide");
			gameManager.soundManager.Stop();
			gameManager.musicManager.Stop();
			gameManager.musicManager.PlayMusicLoop(3);
			gameManager.soundManager.PlaySound(20);
			gameManager.homeScene.cameraFilterPack_fx_Glitch1.enabled = true;
			if (!isover)
			{
				StartCoroutine(Over());
			}
		}
	}

	private void CioSaying()
	{
		if (!isSaying)
		{
			isSaying = true;
			txt_zimu2.GetComponent<Text>().text = "";
			van.GetComponent<Van>().ShowExpression(0);
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
				if (pos == 2f || pos == 5f || pos == 11f || pos == 13f || pos == 18f || pos == 21f)
				{
					pos -= 0.5f;
				}
				else if (pos == 22f)
				{
					pos = 23f;
				}
				else if (pos == 27f)
				{
					gameManager.homeScene.hackerBk.Crash(isaddcountdown: false);
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
			if (pos == 2f || pos == 5f || pos == 11f || pos == 13f || pos == 18f || pos == 21f)
			{
				pos -= 0.5f;
			}
			else if (pos == 22f)
			{
				pos = 23f;
			}
			else if (pos == 27f)
			{
				gameManager.homeScene.hackerBk.Crash(isaddcountdown: false);
			}
		}
	}

	private IEnumerator Over()
	{
		yield return new WaitForSeconds(4.2f);
		gameManager.homeScene.cameraFilterPack_fx_Glitch1.enabled = false;
		gameManager.homeScene.cameraFilterPack_Noise_TV_1.enabled = false;
		gameManager.homeScene.cameraFilterPack_Noise_TV_2.enabled = false;
		img_black.SetActive(value: true);
		close.SetActive(value: true);
		yield return new WaitForSeconds(2f);
		gameManager.soundManager.PlayHackerSound(4);
		gameManager.homeScene.cameraFilterPack_Noise_TV_2.enabled = false;
		Object.Instantiate(Resources.Load("Dialog/Hacker/hackerdialog") as GameObject, gameManager.homeScene.middle);
		yield return new WaitForSeconds(1f);
		Object.Destroy(base.gameObject);
	}

	private IEnumerator AudioPlayFinished(float time)
	{
		yield return new WaitForSeconds(time);
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
		if (pos == 20.5f)
		{
			switch (poss)
			{
			case 0:
				pos += 0.5f;
				break;
			case 1:
				pos += 1.5f;
				break;
			}
		}
		else
		{
			pos += 0.5f;
		}
		gameManager.soundManager.Stop();
		iscanclick = true;
		ClickZimu();
		selectGroup.HideSelect();
	}

	public void HideVideoDialog()
	{
		gameManager.CanShowSetting(-1);
		gameManager.homeScene.computerButtonBox.iscanclick = true;
		Object.Destroy(base.gameObject);
	}
}
