using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class PhoneItemInfo : MonoBehaviour
{
	public List<Image> icon;

	public MultiplyText talkInfo;

	public Text talkInfo0;

	public Text userName;

	private PhoneInfo parObj;

	private GameManager gameManager;

	private int soundid;

	private string soundDlc;

	public void Init(DATA38 data38, PhoneInfo par, GameManager gm, string itemid, int a = 0, int sound = -1)
	{
		soundid = sound;
		soundDlc = data38.ID.ToString();
		parObj = par;
		gameManager = gm;
		string key = gameManager.dataManager.dic37[par.getID].name;
		userName.GetComponent<I18NText>().updateTranslation2(key);
		if (a == 0)
		{
			if (itemid.Equals("0"))
			{
				if (talkInfo0 != null)
				{
					talkInfo.gameObject.SetActive(value: false);
					talkInfo0.gameObject.SetActive(value: true);
					StartCoroutine(SetNormalLevel(data38.frdreply));
				}
				else
				{
					StartCoroutine(SetLabel(data38.frdreply, "0", "*"));
				}
			}
			else
			{
				_ = gameManager.dataManager.dic1[itemid];
				StartCoroutine(SetLabel(data38.frdreply, itemid, I18N.instance.getValue(data38.highlight)));
			}
			return;
		}
		icon[0].gameObject.SetActive(value: false);
		icon[1].gameObject.SetActive(value: false);
		icon[2].gameObject.SetActive(value: false);
		if (itemid.Equals("0"))
		{
			if (talkInfo0 != null)
			{
				talkInfo.gameObject.SetActive(value: false);
				talkInfo0.gameObject.SetActive(value: true);
				talkInfo0.GetComponent<I18NText>().updateTranslation2(data38.frdreply);
				parObj.ToBottom();
			}
			else
			{
				talkInfo.SetContent2(data38.frdreply, "0", "*");
				SetNewWidth(data38.frdreply);
			}
		}
		else
		{
			DATA1 dATA = gameManager.dataManager.dic1[itemid];
			Debug.Log("talkinfo:" + data38.frdreply + "::" + itemid + "::" + I18N.instance.getValue(dATA.message));
			talkInfo.SetContent2(data38.frdreply, itemid, I18N.instance.getValue(data38.highlight));
			SetNewWidth(data38.frdreply);
			parObj.multiplytextlist.Add(talkInfo);
		}
	}

	private void SetNewWidth(string frdReply)
	{
		if (CalculateLengthOfText(I18N.instance.getValue(frdReply)) <= 250f)
		{
			talkInfo.SetNewWidth(I18N.instance.getValue(frdReply));
		}
	}

	private float CalculateLengthOfText(string message)
	{
		float num = 0f;
		Font font = talkInfo.textBkg.text.font;
		font.RequestCharactersInTexture(message, talkInfo.textBkg.text.fontSize, talkInfo.textBkg.text.fontStyle);
		CharacterInfo info = default(CharacterInfo);
		char[] array = message.ToCharArray();
		foreach (char ch in array)
		{
			font.GetCharacterInfo(ch, out info, talkInfo.textBkg.text.fontSize);
			num += (float)info.advance;
		}
		return num;
	}

	private IEnumerator SetNormalLevel(string frdReply)
	{
		parObj.LineToBottom();
		for (int i = 0; i < 6; i++)
		{
			int index = ((i < 3) ? i : (i % 3));
			if (i == 3)
			{
				icon[0].GetComponent<CanvasGroup>().alpha = 0f;
				icon[1].GetComponent<CanvasGroup>().alpha = 0f;
				icon[2].GetComponent<CanvasGroup>().alpha = 0f;
			}
			icon[index].GetComponent<CanvasGroup>().alpha = 1f;
			yield return new WaitForSeconds(0.2f);
		}
		if (gameManager.Is_Dlc7())
		{
			gameManager.soundManager.PlayDLCEventSound(gameManager.player.GetEventId(), parObj.getID, soundDlc);
		}
		else
		{
			gameManager.soundManager.PlayEvent(gameManager.player.GetEventId(), soundid);
		}
		talkInfo0.GetComponent<I18NText>().updateTranslation2(frdReply);
		yield return new WaitForSeconds(0.12f);
		parObj.LineToBottom();
	}

	private IEnumerator SetLabel(string frdReply, string itemid, string strFrament)
	{
		parObj.LineToBottom();
		for (int i = 0; i < 6; i++)
		{
			int index = ((i < 3) ? i : (i % 3));
			if (i == 3)
			{
				icon[0].GetComponent<CanvasGroup>().alpha = 0f;
				icon[1].GetComponent<CanvasGroup>().alpha = 0f;
				icon[2].GetComponent<CanvasGroup>().alpha = 0f;
			}
			icon[index].GetComponent<CanvasGroup>().alpha = 1f;
			yield return new WaitForSeconds(0.2f);
		}
		if (gameManager.Is_Dlc7())
		{
			gameManager.soundManager.PlayDLCEventSound(gameManager.player.GetEventId(), parObj.getID, soundDlc);
		}
		else
		{
			gameManager.soundManager.PlayEvent(gameManager.player.GetEventId(), soundid);
		}
		talkInfo.SetContent2(frdReply, itemid, strFrament);
		SetNewWidth(frdReply);
		parObj.multiplytextlist.Add(talkInfo);
		parObj.LineToBottom();
	}
}
