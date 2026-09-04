using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ReadPinglunList : MonoBehaviour
{
	public ReaditBrowser readitBrowser;

	public Text userName;

	public Text time;

	public Text info;

	public GameObject replyBox;

	public Text replyUserName;

	public Text replyTime;

	public Text replyInfo;

	public MultiplyText txtCollect;

	public Image replyImg;

	public HighLightPic collectImg;

	private GameManager gameManager;

	public void Init(int i, ReaditBrowser obj, GameManager gm)
	{
		readitBrowser = obj;
		gameManager = gm;
		SetInfo(i);
	}

	private void SetInfo(int i)
	{
		string[] array = readitBrowser.pinglun[i].Split(';');
		userName.GetComponent<I18NText>().updateTranslation2(array[0]);
		time.GetComponent<I18NText>().updateTranslation2(array[1]);
		if (readitBrowser.collectID.Count != 0 && readitBrowser.collectID[i] != "")
		{
			txtCollect.gameObject.SetActive(value: true);
			info.gameObject.SetActive(value: false);
			txtCollect.SetContent2(array[2], readitBrowser.collectID[i], I18N.instance.getValue((array.Length == 4) ? array[3] : array[2]));
		}
		else
		{
			txtCollect.gameObject.SetActive(value: false);
			info.gameObject.SetActive(value: true);
			info.GetComponent<I18NText>().updateTranslation2(array[2]);
		}
		if (readitBrowser.reply.Count != 0 && readitBrowser.reply[i] != "")
		{
			replyBox.SetActive(value: true);
			string[] array2 = readitBrowser.pinglun[int.Parse(readitBrowser.reply[i])].Split(';');
			replyUserName.GetComponent<I18NText>().updateTranslation2(array2[0]);
			replyTime.GetComponent<I18NText>().updateTranslation2(array2[1]);
			replyInfo.GetComponent<I18NText>().updateTranslation2(array2[2]);
		}
		if (readitBrowser.replyImg.Count != 0 && readitBrowser.replyImg[i] != "")
		{
			replyImg.gameObject.SetActive(value: true);
			string text = readitBrowser.replyImg[i].Split(';')[0];
			string text2 = readitBrowser.replyImg[i].Split(';')[1];
			string text3 = readitBrowser.replyImg[i].Split(';')[2];
			if (text2 == "0")
			{
				Sprite sprite = Resources.Load<Sprite>("Image/" + text);
				replyImg.sprite = sprite;
			}
			else
			{
				Object.Instantiate(Resources.Load<GameObject>("Image/" + text), replyImg.transform);
			}
			if (text3 != "0")
			{
				collectImg.gameObject.SetActive(value: true);
				collectImg.SetContent(text3);
			}
			replyImg.SetNativeSize();
		}
	}
}
