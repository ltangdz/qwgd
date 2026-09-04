using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class VideoDialogTaskFailed : MonoBehaviour
{
	public Text txt_name;

	public Text txt_zimu2;

	public string[] allZimu;

	public HomeScene homeScene;

	public Image img_mouse;

	public Button btn_ringoff;

	public GameManager gameManager;

	public float pos;

	public string dataid;

	public string mailid;

	public SelectGroup selectGroup;

	public bool iscanclick = true;

	private bool hundown;

	public SpriteAnimation ashley;

	private int type;

	private List<string> zimus = new List<string>();

	private int faceLook;

	private int time;

	[SerializeField]
	private bool isSaying;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
		gameManager.homeScene.computerButtonBox.iscanclick = false;
		gameManager.musicManager.LowerVol();
		openClick();
	}

	public void openClick()
	{
		gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	public void Init(string funName)
	{
		switch (funName)
		{
		case "chat":
			type = 0;
			break;
		case "phone":
			type = 1;
			break;
		case "invade":
			type = 2;
			break;
		case "invadephone":
			type = 2;
			break;
		}
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (!gameManager.player.playerdata.camFailedTime.ContainsKey(funName))
		{
			gameManager.player.playerdata.camFailedTime.Add(funName, 0);
		}
		gameManager.player.playerdata.camFailedTime[funName]++;
		SetZimu(funName);
		homeScene = gameManager.homeScene;
		gameManager.musicManager.LowerVol();
		Invoke("ClickZimu", 1.5f);
	}

	private void SetZimu(string funName)
	{
		time = gameManager.player.playerdata.camFailedTime[funName];
		zimus.Clear();
		if (time == 1)
		{
			faceLook = 1;
			zimus.Add(allZimu[0]);
			zimus.Add(allZimu[1]);
		}
		else if (time == 2)
		{
			faceLook = 2;
			zimus.Add(allZimu[2]);
			zimus.Add(allZimu[3]);
		}
		else if (time >= 3)
		{
			faceLook = 3;
			zimus.Add(allZimu[4]);
			zimus.Add(allZimu[5]);
		}
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
		iscanclick = true;
		if (pos < (float)zimus.Count)
		{
			CioSaying();
		}
		else if (!hundown)
		{
			ashley.Stop();
			hundown = true;
			btn_ringoff.interactable = true;
			btn_ringoff.GetComponent<Animator>().Play("ani_breath");
			txt_zimu2.text = "";
			gameManager.soundManager.Stop();
			gameManager.soundManager.PlaySound(20);
			GetComponent<Animator>().Play("ani_videoHide");
			btn_ringoff.onClick.AddListener(delegate
			{
				gameManager.musicManager.ResumeVol();
				txt_zimu2.text = "";
				gameManager.soundManager.Stop();
				gameManager.soundManager.PlaySound(20);
				gameManager.homeScene.isshowvideo = false;
				GetComponent<Animator>().Play("ani_videoHide");
			});
		}
	}

	private void CioSaying()
	{
		if (!isSaying)
		{
			isSaying = true;
			txt_zimu2.GetComponent<Text>().text = "";
			ashley.SetState(faceLook);
			StopAllCoroutines();
			gameManager.soundManager.Stop();
			float num = 0f;
			if (zimus.Count >= 1)
			{
				num = gameManager.soundManager.PlayFailed(time, (int)pos);
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
				isSaying = false;
			});
		}
		else
		{
			txt_zimu2.DOKill();
			isSaying = false;
			txt_zimu2.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue(zimus[(int)pos].Trim()), gameManager.player.playerdata.nickname));
			pos += 1f;
		}
	}

	private IEnumerator AudioPlayFinished(float time)
	{
		yield return new WaitForSeconds(time);
		ashley.SetState(0);
	}

	public void HideVideoDialog()
	{
		gameManager.CanShowSetting(-1);
		gameManager.homeScene.computerButtonBox.iscanclick = true;
		gameManager.homeScene.isshowvideo = false;
		Object.Instantiate(Resources.Load<GameObject>("Dialog/taskFailedPanel"), gameManager.homeScene.middle).GetComponent<TaskFailed>().Init(type, gameManager);
		gameManager.musicManager.ResumeVol();
		Object.Destroy(base.gameObject);
	}
}
