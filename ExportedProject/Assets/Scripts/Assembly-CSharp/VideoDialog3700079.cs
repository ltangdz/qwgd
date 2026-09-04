using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class VideoDialog3700079 : MonoBehaviour
{
	public Text txt_zimu2;

	public string[] zimu1;

	public string[] zimu2;

	public string[] chenfuzimu;

	public string[] huimiezimu;

	public string[] replys;

	public string[] yuyin1;

	public string[] yuyin2;

	public string[] chenfuyuyin;

	public string[] huimieyuyin;

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

	private int sayType;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.player.playerdata.canPlayHideGame)
		{
			zimus = zimu2;
			yuyin = yuyin2;
			sayType = 2;
		}
		else
		{
			zimus = zimu1;
			yuyin = yuyin1;
			sayType = 1;
		}
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
		gameManager.homeScene.eventsystem.SetActive(value: true);
		ClickZimu();
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
		if (pos == 3.5f && sayType == 1)
		{
			SetSelect(0, 2);
			return;
		}
		if (pos == 2.5f && sayType == 2)
		{
			SetSelect(0, 2);
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
					if ((pos == 4f && sayType == 1) || (pos == 3f && sayType == 2))
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
				if ((pos == 4f && sayType == 1) || (pos == 3f && sayType == 2))
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
			gameManager.musicManager.ResumeVol();
			gameManager.soundManager.Stop();
			gameManager.soundManager.PlaySound(20);
			gameManager.ShowFloatBox();
			Invoke("HideVideoDialog", 2f);
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
			switch (poss)
			{
			case 0:
				zimus = chenfuzimu;
				yuyin = chenfuyuyin;
				break;
			case 1:
				zimus = huimiezimu;
				yuyin = huimieyuyin;
				break;
			}
			sayType = 3;
			pos = 0f;
			ClickZimu();
			selectGroup.HideSelect();
		}
	}

	public void HideVideoDialog()
	{
		Object.Instantiate(Resources.Load<GameObject>("Duikang/duikangDialog"), gameManager.homeScene.middle);
		gameManager.CanShowSetting(-1);
		gameManager.homeScene.computerButtonBox.iscanclick = true;
		gameManager.musicManager.ResumeVol();
		gameManager.saveManager.SavePlayerData();
		if (gameManager.homeScene.newZhadanDialog != null)
		{
			Object.Destroy(gameManager.homeScene.newZhadanDialog.gameObject);
		}
		Object.Destroy(base.gameObject);
	}
}
