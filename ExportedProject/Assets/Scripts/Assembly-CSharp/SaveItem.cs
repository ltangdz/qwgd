using System;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SaveItem : MonoBehaviour
{
	private GameManager gameManager;

	[SerializeField]
	private Text txt_name;

	[SerializeField]
	private Text txt_gametime;

	[SerializeField]
	private Text txt_level;

	[SerializeField]
	private Text txt_date;

	[SerializeField]
	private Text txt_type;

	[SerializeField]
	private Text txt_download;

	[SerializeField]
	private Image img_green;

	public int type;

	public string path = "";

	[SerializeField]
	private List<Sprite> sprites;

	[SerializeField]
	private GameObject levelgroup;

	[SerializeField]
	private List<Transform> levellist = new List<Transform>();

	[SerializeField]
	private GameObject img_star;

	private bool isupdate;

	public void Init(string pt, PlayerData playerData, int tp = 0)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.dataManager.dic11.ContainsKey(gameManager.player.GetEventId(playerData)))
		{
			path = pt;
			type = tp;
			txt_name.text = I18N.instance.getValue("^username") + ":[" + playerData.nickname + "]";
			int num = 0;
			if (playerData.alllevelinfo.ContainsKey(2) && !playerData.alllevelinfo[2].Equals("0"))
			{
				num++;
				levellist[0].gameObject.SetActive(value: true);
			}
			if (playerData.alllevelinfo.ContainsKey(3) && !playerData.alllevelinfo[3].Equals("0"))
			{
				num++;
				levellist[1].gameObject.SetActive(value: true);
			}
			if (playerData.alllevelinfo.ContainsKey(4) && !playerData.alllevelinfo[4].Equals("0"))
			{
				num++;
				levellist[2].gameObject.SetActive(value: true);
			}
			if (playerData.alllevelinfo.ContainsKey(5) && !playerData.alllevelinfo[5].Equals("0"))
			{
				num++;
				levellist[3].gameObject.SetActive(value: true);
			}
			if (playerData.alllevelinfo.ContainsKey(6) && !playerData.alllevelinfo[6].Equals("0"))
			{
				num++;
				levellist[4].gameObject.SetActive(value: true);
			}
			img_star.SetActive(num == levellist.Count);
			txt_level.text = I18N.instance.getValue(gameManager.dataManager.dic11[gameManager.player.GetEventId(playerData)].event_title);
			string text = Mathf.Ceil((float)playerData.accountTime / 60f).ToString();
			txt_gametime.text = I18N.instance.getValue("^home01") + text + " min";
			txt_type.text = I18N.instance.getValue((type == 0) ? "^home02" : "^home03");
			txt_date.text = ChangeTime(playerData.savetime);
			GetComponent<Button>().onClick.AddListener(Click);
		}
	}

	public void Init(PlayerData playerData)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		txt_name.text = I18N.instance.getValue("^username") + ":[" + playerData.nickname + "]";
		txt_level.text = I18N.instance.getValue(gameManager.dataManager.dic11[gameManager.player.GetEventId(playerData)].event_title);
		string text = Mathf.Ceil((float)playerData.accountTime / 60f).ToString();
		txt_gametime.text = I18N.instance.getValue("^home01") + text + " min";
		txt_type.text = I18N.instance.getValue((type == 0) ? "^home02" : "^home03");
		txt_date.text = ChangeTime(playerData.savetime);
		int num = 0;
		if (playerData.alllevelinfo.ContainsKey(2) && !playerData.alllevelinfo[2].Equals("0"))
		{
			num++;
			levellist[0].gameObject.SetActive(value: true);
		}
		else if (playerData.alllevelinfo.ContainsKey(2) && playerData.alllevelinfo[2].Equals("0"))
		{
			levellist[0].gameObject.SetActive(value: false);
		}
		else if (!playerData.alllevelinfo.ContainsKey(2))
		{
			levellist[0].gameObject.SetActive(value: false);
		}
		if (playerData.alllevelinfo.ContainsKey(3) && !playerData.alllevelinfo[3].Equals("0"))
		{
			num++;
			levellist[1].gameObject.SetActive(value: true);
		}
		else if (playerData.alllevelinfo.ContainsKey(3) && playerData.alllevelinfo[3].Equals("0"))
		{
			levellist[1].gameObject.SetActive(value: false);
		}
		else if (!playerData.alllevelinfo.ContainsKey(3))
		{
			levellist[1].gameObject.SetActive(value: false);
		}
		if (playerData.alllevelinfo.ContainsKey(4) && !playerData.alllevelinfo[4].Equals("0"))
		{
			num++;
			levellist[2].gameObject.SetActive(value: true);
		}
		else if (playerData.alllevelinfo.ContainsKey(4) && playerData.alllevelinfo[4].Equals("0"))
		{
			levellist[2].gameObject.SetActive(value: false);
		}
		else if (!playerData.alllevelinfo.ContainsKey(4))
		{
			levellist[2].gameObject.SetActive(value: false);
		}
		if (playerData.alllevelinfo.ContainsKey(5) && !playerData.alllevelinfo[5].Equals("0"))
		{
			num++;
			levellist[3].gameObject.SetActive(value: true);
		}
		else if (playerData.alllevelinfo.ContainsKey(5) && playerData.alllevelinfo[5].Equals("0"))
		{
			levellist[3].gameObject.SetActive(value: false);
		}
		else if (!playerData.alllevelinfo.ContainsKey(5))
		{
			levellist[3].gameObject.SetActive(value: false);
		}
		if (playerData.alllevelinfo.ContainsKey(6) && !playerData.alllevelinfo[6].Equals("0"))
		{
			num++;
			levellist[4].gameObject.SetActive(value: true);
		}
		else if (playerData.alllevelinfo.ContainsKey(6) && playerData.alllevelinfo[6].Equals("0"))
		{
			levellist[4].gameObject.SetActive(value: false);
		}
		else if (!playerData.alllevelinfo.ContainsKey(6))
		{
			levellist[4].gameObject.SetActive(value: false);
		}
		img_star.SetActive(num == levellist.Count);
		GetComponent<Button>().onClick.AddListener(Click);
	}

	private string ChangeTime(long time)
	{
		return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(time).ToString("yyyy/MM/dd HH:mm:ss");
	}

	private void Click()
	{
		gameManager.saveManager.SetSavePanelItem(this);
	}

	public void ShowReadSave()
	{
		txt_download.gameObject.SetActive(value: true);
		HideInfor(ishide: false);
		img_green.sprite = sprites[0];
		isupdate = true;
		img_green.DOFillAmount(1f, 2f).SetEase(Ease.InOutCirc).OnComplete(delegate
		{
			isupdate = false;
			gameManager.saveManager.LoginSystem();
			gameManager.saveManager.savePanel.btn_sure.interactable = true;
			gameManager.saveManager.savePanel.btn_delete.interactable = true;
			gameManager.saveManager.savePanel.btn_back.interactable = true;
		});
	}

	private void HideInfor(bool ishide)
	{
		txt_name.gameObject.SetActive(ishide);
		txt_gametime.gameObject.SetActive(ishide);
		txt_level.gameObject.SetActive(ishide);
		txt_date.gameObject.SetActive(ishide);
		txt_type.gameObject.SetActive(ishide);
		levelgroup.SetActive(ishide);
		if (!ishide)
		{
			img_star.SetActive(ishide);
			levellist[0].gameObject.SetActive(ishide);
			levellist[1].gameObject.SetActive(ishide);
			levellist[2].gameObject.SetActive(ishide);
			levellist[3].gameObject.SetActive(ishide);
			levellist[4].gameObject.SetActive(ishide);
		}
	}

	public void ShowUploadSave(UnityAction callback)
	{
		img_green.gameObject.SetActive(value: true);
		img_green.fillAmount = 0f;
		txt_download.gameObject.SetActive(value: true);
		HideInfor(ishide: false);
		img_green.sprite = sprites[1];
		isupdate = true;
		img_green.DOFillAmount(1f, 2f).SetEase(Ease.InOutCirc).OnComplete(delegate
		{
			isupdate = false;
			Init(gameManager.player.playerdata);
			img_green.gameObject.SetActive(value: false);
			txt_download.gameObject.SetActive(value: false);
			HideInfor(ishide: true);
			base.transform.SetSiblingIndex(1);
			gameManager.saveManager.savePanel.btn_sure.interactable = true;
			gameManager.saveManager.savePanel.btn_delete.interactable = true;
			gameManager.saveManager.savePanel.btn_back.interactable = true;
			gameManager.saveManager.savePanel.Totop();
			if (callback != null)
			{
				callback();
			}
		});
	}

	private void Update()
	{
		if (isupdate)
		{
			txt_download.text = string.Format(I18N.instance.getValue((gameManager.saveManager.GetSavePanelType() == 0) ? "^home05" : "^home06"), (int)(img_green.fillAmount * 100f) + "%");
		}
	}
}
