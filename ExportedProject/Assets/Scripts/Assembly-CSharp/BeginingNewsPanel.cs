using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginingNewsPanel : MonoBehaviour
{
	public Image img_whiteline;

	public int newspos;

	public List<BeginingNewsWindow> newswindowlist;

	public Animator manynewsani;

	public Animator manynewsani2;

	public CameraFilterPack_FX_Glitch3 cameraFilterPack_FX_Glitch3;

	public CameraFilterPack_NewGlitch4 cameraFilterPack_NewGlitch4;

	public Image img_black;

	public I18NText txt_system;

	public GameManager gameManager;

	public int lastpos;

	public string[] lastzimus;

	public GameObject warning;

	private bool isshowwarning;

	public Image slider_restart;

	public GameObject img_sliderrestart;

	private int pos;

	public string[] zimus;

	private void SetLastZimu()
	{
		if (lastpos >= lastzimus.Length)
		{
			Debug.Log("showwarning:" + isshowwarning);
			if (isshowwarning)
			{
				warning.SetActive(value: true);
				StartCoroutine(StartBlack());
			}
			isshowwarning = true;
		}
		else
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("beginingnewstxt"), base.transform);
			gameObject.GetComponent<I18NText>().updateTranslation2(lastzimus[lastpos]);
			gameObject.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, -419f);
			gameObject.transform.localScale = Vector3.zero;
			StartCoroutine(WaitGoUPLast(gameObject, (lastpos == 0) ? 0f : 0.6f));
		}
	}

	private IEnumerator WaitGoUPLast(GameObject zimu, float wait)
	{
		yield return new WaitForSeconds(wait);
		zimu.GetComponent<Text>().color = Color.white;
		zimu.transform.DOScale(Vector3.one, 0.1f);
		yield return new WaitForSeconds(0.1f);
		lastpos++;
		SetLastZimu();
		yield return new WaitForSeconds(0.4f);
		zimu.GetComponent<Text>().DOColor(Color.gray, 0.3f).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(zimu.gameObject);
		});
		zimu.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.3f);
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Invoke("AddNews", 2f);
	}

	private void AddNews()
	{
		if (newspos >= newswindowlist.Count)
		{
			Debug.Log("系统过载");
			cameraFilterPack_FX_Glitch3.enabled = true;
			cameraFilterPack_NewGlitch4.enabled = true;
			manynewsani.gameObject.SetActive(value: true);
			manynewsani.Play("ani_manynews");
			StartCoroutine(StartLastNews());
			return;
		}
		img_whiteline.rectTransform.sizeDelta = new Vector2(0f, 5f);
		img_whiteline.gameObject.SetActive(value: true);
		img_whiteline.rectTransform.DOSizeDelta(new Vector3(1016f, 5f), 0.5f).SetEase(Ease.InCubic).OnComplete(delegate
		{
			img_whiteline.gameObject.SetActive(value: false);
			newswindowlist[newspos].transform.DOScaleY(1f, 0.1f).SetEase(Ease.InCubic);
			newswindowlist[newspos].PlayMusic();
			Array.Clear(zimus, 0, zimus.Length);
			zimus = newswindowlist[newspos].zimus;
			pos = 0;
			SetZimu();
		});
	}

	private IEnumerator StartLastNews()
	{
		SetLastZimu();
		yield return new WaitForSeconds(5f);
		lastpos = 0;
		manynewsani2.gameObject.SetActive(value: true);
		manynewsani2.Play("ani_manynews");
		SetLastZimu();
	}

	private void SetZimu()
	{
		if (pos < zimus.Length)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("beginingnewstxt"), base.transform);
			gameObject.GetComponent<I18NText>().updateTranslation2(zimus[pos]);
			StartCoroutine(WaitGoUP(gameObject, (pos == 0) ? 0f : 1.1f));
			if (pos == zimus.Length - 1)
			{
				gameObject.name = "last";
			}
		}
	}

	private IEnumerator WaitGoUP(GameObject zimu, float wait)
	{
		yield return new WaitForSeconds(wait);
		zimu.GetComponent<Text>().DOColor(Color.white, 0.2f);
		zimu.transform.DOScale(Vector3.one, 0.2f);
		zimu.transform.DOLocalMoveY(-419f, 0.2f).SetEase(Ease.InOutCirc);
		yield return new WaitForSeconds(0.3f);
		pos++;
		SetZimu();
		yield return new WaitForSeconds(0.9f);
		zimu.GetComponent<Text>().DOColor(Color.gray, 0.3f);
		zimu.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.3f);
		zimu.transform.DOLocalMoveY(-369f, 0.3f).SetEase(Ease.OutCirc).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(zimu.gameObject);
			if (zimu.name.Equals("last"))
			{
				StartNewNews();
			}
		});
	}

	private void StartNewNews()
	{
		if (newspos >= newswindowlist.Count)
		{
			cameraFilterPack_FX_Glitch3.enabled = true;
			cameraFilterPack_NewGlitch4.enabled = true;
			manynewsani.gameObject.SetActive(value: true);
			manynewsani.Play("ani_manynews");
		}
		else
		{
			newswindowlist[newspos].transform.DOScale(Vector3.zero, 0.1f).OnComplete(delegate
			{
			});
			newspos++;
			AddNews();
		}
	}

	private IEnumerator StartBlack()
	{
		img_black.gameObject.SetActive(value: true);
		img_black.DOFade(1f, 5f).OnComplete(delegate
		{
			cameraFilterPack_FX_Glitch3.enabled = false;
			cameraFilterPack_NewGlitch4.enabled = false;
			StartCoroutine(LowMusic());
			txt_system.gameObject.SetActive(value: true);
			txt_system.updateTranslation5(I18N.instance.getValue("^systemrestart02"));
			img_sliderrestart.SetActive(value: true);
			slider_restart.DOFillAmount(1f, 3f).OnComplete(delegate
			{
				CancelInvoke();
				if (gameManager.musicManager != null)
				{
					gameManager.musicManager.Stop();
				}
				gameManager.ShowFloatBox();
				Invoke("ChangeScene", 2f);
			});
		});
		yield return new WaitForSeconds(6f);
		cameraFilterPack_FX_Glitch3.enabled = false;
		cameraFilterPack_NewGlitch4.enabled = false;
	}

	private IEnumerator LowMusic()
	{
		float vol = PlayerPrefs.GetFloat("musicvol", 1f);
		gameManager.musicManager.GetComponent<AudioSource>().volume = vol;
		while (vol > 0f)
		{
			vol -= 0.02f;
			yield return new WaitForSeconds(0.05f);
			gameManager.musicManager.GetComponent<AudioSource>().volume = vol;
		}
	}

	private void SetSystemText()
	{
		if (txt_system.GetComponent<Text>().text.Length == 0)
		{
			txt_system.updateTranslation5(I18N.instance.getValue("^systemrestart02"));
		}
		else
		{
			txt_system.updateTranslation5(txt_system.GetComponent<Text>().text + ">");
		}
		if (txt_system.GetComponent<Text>().text.Length >= 50)
		{
			CancelInvoke();
			if (gameManager.musicManager != null)
			{
				gameManager.musicManager.Stop();
			}
			gameManager.ShowFloatBox();
			Invoke("ChangeScene", 2f);
		}
	}

	private void ChangeScene()
	{
		Debug.Log("跳转场景");
		gameManager.musicManager.Stop();
		gameManager.musicManager.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicvol", 1f);
		PlayerPrefs.SetInt("isfirstshowbeginning", 1);
		gameManager.txt_studio.SetActive(value: false);
		SceneManager.LoadScene("homecourse");
	}

	private void Update()
	{
	}
}
