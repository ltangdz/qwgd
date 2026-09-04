using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class ListJieSuan : MonoBehaviour
{
	public Text targetLabel;

	public Text question;

	public GameObject infoBox;

	private string id;

	private MissionResult parObj;

	private int index;

	private GameManager gameManager;

	private string[] listInfo;

	private string[] highLight;

	private string[] clue;

	private bool isallok;

	public void Init(DATA20 listID, MissionResult obj, int listIndex, GameManager gm)
	{
		id = listID.ID.ToString();
		parObj = obj;
		index = listIndex;
		gameManager = gm;
		targetLabel.GetComponent<I18NText>().updateTranslation2(obj.target[listIndex]);
		question.GetComponent<I18NText>().updateTranslation2(listID.title);
		listInfo = listID.info.Split(';');
		highLight = listID.highlight.Split(';');
		clue = listID.clue.Split(';');
		bool flag = true;
		if (id.Equals("2000021"))
		{
			for (int i = 0; i < listInfo.Length; i++)
			{
				Text text = Object.Instantiate(Resources.Load<Text>("txt_jiesuaninfo"), infoBox.transform);
				string value = I18N.instance.getValue(highLight[i]);
				string value2 = I18N.instance.getValue(listInfo[i]);
				switch (i)
				{
				case 0:
					if (gameManager.player.playerdata.isenterhoutai)
					{
						value2 = value2.Replace(value, "<color=#fee9a8>" + value + "</color>");
					}
					else
					{
						Debug.Log("无数据信息" + listIndex);
						string text3 = "";
						for (int k = 0; k < value.Length; k++)
						{
							text3 += "█";
						}
						value2 = value2.Replace(value, "<color=#fee9a8>" + text3 + "</color>");
						flag = false;
					}
					text.GetComponent<I18NText>().updateTranslation2(value2);
					break;
				case 1:
					if (gameManager.player.playerdata.isopenreport)
					{
						value2 = value2.Replace(value, "<color=#fee9a8>" + value + "</color>");
					}
					else
					{
						Debug.Log("无数据信息" + listIndex);
						string text2 = "";
						for (int j = 0; j < value.Length; j++)
						{
							text2 += "█";
						}
						value2 = value2.Replace(value, "<color=#fee9a8>" + text2 + "</color>");
						flag = false;
					}
					text.GetComponent<I18NText>().updateTranslation2(value2);
					break;
				}
			}
		}
		else
		{
			for (int l = 0; l < listInfo.Length; l++)
			{
				Text text4 = Object.Instantiate(Resources.Load<Text>("txt_jiesuaninfo"), infoBox.transform);
				string value3 = I18N.instance.getValue(highLight[l]);
				string value4 = I18N.instance.getValue(listInfo[l]);
				if (gameManager.player.playerdata.itemlist.Contains(clue[l]) || clue[l] == "0")
				{
					value4 = value4.Replace(value3, "<color=#fee9a8>" + value3 + "</color>");
				}
				else
				{
					Debug.Log("无数据信息" + listIndex);
					string text5 = "";
					for (int m = 0; m < value3.Length; m++)
					{
						text5 += "█";
					}
					value4 = value4.Replace(value3, "<color=#fee9a8>" + text5 + "</color>");
					flag = false;
				}
				text4.GetComponent<I18NText>().updateTranslation2(value4);
			}
		}
		if (flag)
		{
			parObj.completeEvent++;
		}
	}
}
