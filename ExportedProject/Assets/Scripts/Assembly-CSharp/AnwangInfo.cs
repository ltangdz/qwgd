using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class AnwangInfo : MonoBehaviour
{
	public Text title;

	public Text artName;

	public Text artTime;

	public Transform info;

	public Image infoImgContent;

	public Image infoImgNoContent;

	public Transform pinglunBox;

	public Button link;

	public ScrollRect scrollRect;

	private GameManager gameManager;

	private Image infoImg;

	private AnwangList parObj;

	public void Info(AnwangList listObj, GameManager gm)
	{
		infoImg = infoImgNoContent;
		gameManager = gm;
		parObj = listObj;
		SetMsg();
	}

	private void SetMsg()
	{
		for (int i = 0; i < info.childCount; i++)
		{
			Object.Destroy(info.GetChild(i).gameObject);
		}
		Canvas.ForceUpdateCanvases();
		scrollRect.verticalNormalizedPosition = 1f;
		Canvas.ForceUpdateCanvases();
		title.GetComponent<I18NText>().updateTranslation2(parObj.title);
		artName.GetComponent<I18NText>().updateTranslation2(parObj.artName);
		if (parObj.arttime != null && artTime != null)
		{
			artTime.GetComponent<I18NText>().updateTranslation2("· " + parObj.arttime);
		}
		for (int j = 0; j < parObj.info.Count; j++)
		{
			string text = "";
			string text2 = "";
			string key = "";
			if (parObj.info[j].IndexOf(";") > -1)
			{
				text = parObj.info[j].Split(';')[1];
				text2 = parObj.info[j].Split(';')[0];
				key = parObj.info[j].Split(';')[2];
			}
			else
			{
				text2 = parObj.info[j];
			}
			if (text == "0" || text == "")
			{
				Object.Instantiate(Resources.Load<GameObject>("Dialog/txt_anwanginfo"), info).GetComponent<I18NText>().updateTranslation2(text2);
			}
			else
			{
				Object.Instantiate(Resources.Load<GameObject>("Dialog/txt_anwangshouji"), info).GetComponent<MultiplyText>().SetContent2(text2, text, I18N.instance.getValue(key));
			}
		}
		if (parObj.linkUrl != "")
		{
			link.gameObject.SetActive(value: true);
			link.transform.Find("mail_link").GetComponent<I18NText>().updateTranslation2(parObj.linkUrl);
			Debug.Log("设置此项目");
			link.onClick.AddListener(delegate
			{
				Debug.Log("点击打开项目");
				gameManager.homeScene.newbrowserDialog.AddNewPanel(parObj.linkJump, "twoDriveNews", parObj.linkUrl);
			});
		}
		else
		{
			link.gameObject.SetActive(value: false);
		}
		if (parObj.infoImg != null)
		{
			infoImg.gameObject.SetActive(value: true);
			infoImg.sprite = parObj.infoImg;
			infoImg.SetNativeSize();
			float width = infoImg.GetComponent<RectTransform>().rect.width;
			float height = infoImg.GetComponent<RectTransform>().rect.height;
			Debug.Log("设置高度:" + width / (height / 200f));
			infoImg.GetComponent<RectTransform>().sizeDelta = new Vector2(width / (height / 200f), 200f);
			if (parObj.itemid != "")
			{
				infoImgContent.gameObject.SetActive(value: true);
				infoImgContent.GetComponent<HighLightPic>().itemid = parObj.itemid;
			}
			else
			{
				infoImgContent.gameObject.SetActive(value: false);
				infoImgContent.GetComponent<HighLightPic>().itemid = "";
			}
		}
		else
		{
			infoImg.gameObject.SetActive(value: false);
		}
		for (int num = 0; num < pinglunBox.childCount; num++)
		{
			Object.Destroy(pinglunBox.GetChild(num).gameObject);
		}
		for (int num2 = 0; num2 < parObj.commentArtName.Count; num2++)
		{
			Transform transform = Object.Instantiate(Resources.Load<Transform>("Browser/evileye_list"), pinglunBox);
			string text3 = "";
			string text4 = "";
			string key2 = "";
			if (parObj.comment[num2].IndexOf(";") > -1)
			{
				text3 = parObj.comment[num2].Split(';')[1];
				text4 = parObj.comment[num2].Split(';')[0];
				key2 = parObj.comment[num2].Split(';')[2];
			}
			else
			{
				text4 = parObj.comment[num2];
			}
			transform.Find("txt_name").GetComponent<I18NText>().updateTranslation2(parObj.commentArtName[num2]);
			if (text3 == "0" || text3 == "")
			{
				transform.Find("txt_info").gameObject.SetActive(value: true);
				transform.Find("txt_infocollect").gameObject.SetActive(value: false);
				transform.Find("txt_info").GetComponent<I18NText>().updateTranslation2(text4);
			}
			else
			{
				transform.Find("txt_info").gameObject.SetActive(value: false);
				transform.Find("txt_infocollect").gameObject.SetActive(value: true);
				transform.Find("txt_infocollect").GetComponent<MultiplyText>().SetContent2(text4, text3, I18N.instance.getValue(key2));
			}
			if (parObj.haveVideo.Count > 0 && parObj.haveVideo[num2] == "1")
			{
				transform.Find("img_nocontent").gameObject.SetActive(value: true);
			}
			else if (parObj.haveVideo.Count > 0 && parObj.haveVideo[num2] != "0" && parObj.haveVideo[num2] != "1")
			{
				transform.Find("img_nocontent").gameObject.SetActive(value: false);
				transform.Find("img_content").gameObject.SetActive(value: true);
				transform.Find("img_content").GetComponent<HighLightPic>().itemid = parObj.haveVideo[num2];
			}
			else
			{
				transform.Find("img_nocontent").gameObject.SetActive(value: false);
				transform.Find("img_content").gameObject.SetActive(value: false);
			}
		}
	}
}
