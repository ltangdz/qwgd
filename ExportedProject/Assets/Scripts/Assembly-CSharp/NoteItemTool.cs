using System;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using tnt_deploy;

public class NoteItemTool : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerUpHandler, IPointerClickHandler
{
	public Image img_icon;

	public Text txt_toolname;

	public GameObject img_tip;

	public Sprite[] sprites;

	public int toolid;

	public string imgName;

	public GameManager gameManager;

	public string imgstr;

	public string openImgName;

	private int lastTime;

	private int continuousClick;

	public void Init(GameManager gameManager, int tool)
	{
		this.gameManager = gameManager;
		toolid = tool;
		switch (tool)
		{
		case 0:
			img_icon.sprite = sprites[2];
			txt_toolname.GetComponent<I18NText>().updateTranslation2("^btntool03");
			break;
		case 2:
			img_icon.sprite = sprites[8];
			txt_toolname.GetComponent<I18NText>().updateTranslation2("^btntool09");
			break;
		case 4:
			img_icon.sprite = sprites[7];
			txt_toolname.GetComponent<I18NText>().updateTranslation2("^btntool05");
			break;
		case 5:
			img_icon.sprite = sprites[1];
			txt_toolname.GetComponent<I18NText>().updateTranslation2("^btntool06");
			break;
		case 9:
			img_icon.sprite = sprites[3];
			txt_toolname.GetComponent<I18NText>().updateTranslation2("^btntool01");
			break;
		case 12:
			img_icon.sprite = sprites[4];
			txt_toolname.GetComponent<I18NText>().updateTranslation2("^btntool11");
			break;
		case 13:
			img_icon.sprite = sprites[0];
			txt_toolname.GetComponent<I18NText>().updateTranslation2("^btntool04");
			break;
		case 15:
			img_icon.sprite = sprites[5];
			txt_toolname.GetComponent<I18NText>().updateTranslation2("^btntool12");
			break;
		}
	}

	public virtual void Click()
	{
		if (gameManager.player.playerdata.isCourseOver == 0 || gameManager.homeScene.Iscanopentool() || gameManager.player.playerdata.isCourse01 == 0)
		{
			return;
		}
		if (toolid != 4)
		{
			if (gameManager != null)
			{
				gameManager.homeScene.computerButtonBox.FrontTool(toolid);
				GetCrashInfo();
			}
		}
		else
		{
			if (!(openImgName != imgstr))
			{
				return;
			}
			if (gameManager.homeScene.middle.Find(imgName) == null)
			{
				if (I18N.instance.gameLang.Equals(LanguageCode.EN) && (bool)Resources.Load<GameObject>("Image/" + imgstr + "_en"))
				{
					imgstr += "_en";
				}
				openImgName = imgstr;
				GameObject obj = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Image/" + imgstr), gameManager.homeScene.middle);
				obj.name = imgName;
				obj.GetComponent<PictureDialog>().btn_close.onClick.AddListener(delegate
				{
					openImgName = "";
				});
			}
			else
			{
				gameManager.homeScene.middle.Find(imgName).transform.DOLocalMove(Vector3.zero, 0.3f);
			}
		}
	}

	private void GetCrashInfo()
	{
		if (!gameManager.IsAllDlc())
		{
			return;
		}
		checkAlubaSystem();
		if (toolid != 5)
		{
			return;
		}
		List<string> itemlist = gameManager.player.playerdata.itemlist;
		gameManager._passwordItemList.Clear();
		for (int i = 0; i < itemlist.Count; i++)
		{
			string key = itemlist[i];
			DATA1 dATA = gameManager.dataManager.dic1[key];
			if (dATA.role == "#" + gameManager._selectedPlayerId && dATA.passwordnumber >= 1 && dATA.passwordnumber <= 6)
			{
				gameManager._passwordItemList.Add(dATA);
			}
		}
	}

	private void checkAlubaSystem()
	{
		int millisecond = DateTime.Now.Millisecond;
		int num = millisecond - ((lastTime == 0) ? millisecond : lastTime);
		lastTime = millisecond;
		if (num < 500)
		{
			continuousClick++;
		}
		else
		{
			continuousClick = 0;
		}
		if (continuousClick > 20)
		{
			gameManager.isAlubaSystem = !gameManager.isAlubaSystem;
			continuousClick = 0;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (gameManager.player.playerdata.isCourse01 != 0)
		{
			img_tip.transform.DOKill();
			img_tip.GetComponent<CanvasGroup>().DOKill();
			img_tip.transform.localPosition = new Vector2(img_tip.transform.localPosition.x, 15f);
			img_tip.transform.DOLocalMoveY(24f, 0.05f);
			img_tip.GetComponent<CanvasGroup>().DOFade(1f, 0.05f);
			Debug.Log("enter:show");
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (gameManager.player.playerdata.isCourse01 != 0)
		{
			Invoke("Hide", 0.5f);
		}
	}

	private void Hide()
	{
		img_tip.transform.DOKill();
		img_tip.GetComponent<CanvasGroup>().DOKill();
		img_tip.transform.localPosition = new Vector2(img_tip.transform.localPosition.x, 24f);
		img_tip.transform.DOLocalMoveY(15f, 0.05f);
		img_tip.GetComponent<CanvasGroup>().DOFade(0f, 0.05f);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Click();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}
}
