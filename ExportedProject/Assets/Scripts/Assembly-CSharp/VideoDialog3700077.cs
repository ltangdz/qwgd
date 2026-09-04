using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class VideoDialog3700077 : MonoBehaviour
{
	public Text txt_zimu2;

	public string[] allzimus;

	public string[] yeszimus;

	public string[] nozimus;

	public string[] replys;

	public string[] allyuyin;

	public string[] yesyuyin;

	public string[] noyuyin;

	public Image img_mouse;

	public float pos;

	public string dataid;

	public GameObject imgClick;

	public bool iscanclick = true;

	public SelectGroup selectGroup;

	private bool hundown;

	public Van van;

	[SerializeField]
	private bool isSaying;

	private string[] zimus;

	private string[] yuyin;

	private GameManager gameManager;

	private int input;

	private GameObject badendsavepanel;

	private GameObject badendvan;

	public bool canPass = true;

	private IEnumerator AutoJump1;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		zimus = allzimus;
		yuyin = allyuyin;
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
		Debug.Log("data39:" + dataid);
		gameManager.musicManager.LowerVol();
	}

	private void Update()
	{
		if ((Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) && input != 1 && !selectGroup.gameObject.activeSelf)
		{
			ClickZimu();
		}
	}

	public void ClickZimu()
	{
		img_mouse.gameObject.SetActive(value: true);
		iscanclick = false;
		if (pos == 0.5f && input == 0)
		{
			SetSelect(0, 1);
			return;
		}
		if (pos == 3.5f && input == 0)
		{
			SetSelect(1, 2);
			return;
		}
		if (pos == 10f && input == 0)
		{
			canPass = false;
			Object.Instantiate(Resources.Load<GameObject>("Dialog/badend/badenddosVan"), gameManager.homeScene.transform).GetComponent<BadEndDosVan>().videoDialog = this;
			input = 1;
			return;
		}
		iscanclick = true;
		if (pos < (float)zimus.Length)
		{
			if (!isSaying)
			{
				isSaying = true;
				txt_zimu2.GetComponent<Text>().text = "";
				gameManager.soundManager.Stop();
				float num = 5f;
				if (yuyin.Length >= 1)
				{
					num = gameManager.soundManager.PlayEventFinished(gameManager.player.GetEventId(), int.Parse(yuyin[(int)pos].Split(':')[1]));
				}
				if (input == 3)
				{
					input = 5;
					StartCoroutine(HideAllPanel(num + 5f));
					ShowDelAni(num + 5f);
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
					if ((pos == 1f || pos == 4f) && input == 0)
					{
						pos -= 0.5f;
					}
					isSaying = false;
				});
				if (pos == 1f && input == 5)
				{
					AutoJump1 = AutoJump(num + 3f);
					StartCoroutine(AutoJump1);
				}
				if (pos == (float)(zimus.Length - 1) && input == 5)
				{
					StopCoroutine(AutoJump1);
					canPass = false;
					badendvan = Object.Instantiate(Resources.Load<GameObject>("Dialog/badend/badendvan"), gameManager.homeScene.middle);
					Invoke("ShowCundang", 1f);
				}
			}
			else if (canPass)
			{
				txt_zimu2.DOKill();
				isSaying = false;
				txt_zimu2.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue(zimus[(int)pos].Trim()), gameManager.player.playerdata.nickname));
				pos += 1f;
				if ((pos == 1f || pos == 4f) && input == 0)
				{
					pos -= 0.5f;
				}
			}
		}
		else if (pos >= (float)zimus.Length && !hundown)
		{
			gameManager.homeScene.isshowvideo = false;
			hundown = true;
			txt_zimu2.text = "";
			if (input == 5)
			{
				Object.Destroy(img_mouse.gameObject);
				gameManager.ShowFloatBox();
				Invoke("ShowBadEnd", 2f);
				gameManager.homeScene.computerButtonBox.iscanclick = true;
				gameManager.CanShowSetting(-1);
			}
			else if (input == 4)
			{
				gameManager.musicManager.ResumeVol();
				gameManager.soundManager.Stop();
				GetComponent<Animator>().Play("ani_videoHide");
				gameManager.musicManager.PlayMusicLoop(3);
				gameManager.soundManager.PlaySound(20);
			}
		}
	}

	private IEnumerator AutoJump(float time)
	{
		yield return new WaitForSeconds(time);
		ClickZimu();
	}

	private void ShowCundang()
	{
		ClickZimu();
		badendsavepanel = Object.Instantiate(Resources.Load<GameObject>("Dialog/badend/badendSavePanel"), gameManager.homeScene.middle);
	}

	private void ShowBadEnd()
	{
		Object.Destroy(badendvan);
		gameManager.homeScene.glitch4.enabled = false;
		Object.Instantiate(Resources.Load<GameObject>("Dialog/badend/badend"), gameManager.homeScene.middle);
		Object.Destroy(base.gameObject);
		Object.Destroy(badendsavepanel);
	}

	private IEnumerator HideAllPanel(float time)
	{
		Transform middle = gameManager.homeScene.middle;
		float jiange = middle.childCount + 4;
		Debug.Log("所有的弹框：" + jiange);
		for (int i = 0; (float)i < jiange - 4f; i++)
		{
			middle.GetChild(i).GetComponent<RectTransform>().DOScale(Vector3.zero, 0.2f);
			yield return new WaitForSeconds(time / jiange);
		}
		for (int j = 0; (float)j < jiange - 4f; j++)
		{
			Object.Destroy(middle.GetChild(j).gameObject);
		}
		Debug.Log(gameManager.homeScene.computerButton);
		gameManager.homeScene.newsPanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.2f);
		yield return new WaitForSeconds(time / jiange);
		gameManager.homeScene.goalDialog.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.2f);
		yield return new WaitForSeconds(time / jiange);
		gameManager.homeScene.logPanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.2f);
		yield return new WaitForSeconds(time / jiange);
		if (gameManager.homeScene.computerButton != null)
		{
			Debug.Log("消失按键");
			gameManager.homeScene.computerButton.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.2f);
		}
		if (gameManager.homeScene.notebook != null)
		{
			gameManager.homeScene.notebook.GetComponent<RectTransform>().DOMoveX(1251f, 0.2f);
		}
		yield return new WaitForSeconds(time / jiange);
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
		gameManager.CanShowSetting(-1);
		gameManager.homeScene.computerButtonBox.iscanclick = true;
		gameManager.musicManager.ResumeVol();
		if (input == 4)
		{
			gameManager.homeScene.ShowVideoTip("3700063");
		}
		gameManager.saveManager.SavePlayerData();
		Object.Destroy(base.gameObject);
	}

	public void ChoiceYes()
	{
		zimus = yeszimus;
		yuyin = yesyuyin;
		pos = 0f;
		input = 3;
		ClickZimu();
	}

	public void ChoiceNo()
	{
		zimus = nozimus;
		yuyin = noyuyin;
		pos = 0f;
		input = 4;
		ClickZimu();
	}

	private void ShowDelAni(float waitTime)
	{
		gameManager.homeScene.glitch4.enabled = true;
		float a = 0f;
		DOTween.To(() => a, delegate(float x)
		{
			a = x;
		}, 0.75f, waitTime).OnUpdate(delegate
		{
			gameManager.homeScene.glitch4.__Speed = a;
		});
		DOTween.To(() => a, delegate(float x)
		{
			a = x;
		}, 1f, waitTime).OnUpdate(delegate
		{
			gameManager.homeScene.glitch4._Fade = a;
		});
	}
}
