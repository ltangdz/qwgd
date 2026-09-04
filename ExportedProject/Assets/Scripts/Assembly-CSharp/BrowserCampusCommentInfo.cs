using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class BrowserCampusCommentInfo : MonoBehaviour
{
	public Text timeDay;

	public Text timeMonth;

	public Text title;

	public Text sender;

	public Transform txtInfo;

	public Image infoImg;

	public Transform infoComment;

	public Button btnBak;

	private GameManager gameManager;

	public BrowserForumList parobj;

	public void Init(BrowserForumList obj, GameManager gm)
	{
		parobj = obj;
		gameManager = gm;
		SetInfo();
	}

	public void SetInfo()
	{
		timeDay.GetComponent<I18NText>().updateTranslation2(parobj.timeDay);
		timeMonth.GetComponent<I18NText>().updateTranslation2(parobj.timeMonth);
		title.GetComponent<I18NText>().updateTranslation2(parobj.title);
		sender.GetComponent<I18NText>().updateTranslation2(parobj.sender);
		btnBak.onClick.AddListener(delegate
		{
			parobj.parObj.bbs.Focus();
		});
		string[] array = parobj.info.Split(';');
		for (int num = 0; num < txtInfo.childCount; num++)
		{
			Object.Destroy(txtInfo.GetChild(num).gameObject);
		}
		for (int num2 = 0; num2 < txtInfo.transform.parent.childCount; num2++)
		{
			if (txtInfo.transform.parent.GetChild(num2).name != txtInfo.name && txtInfo.transform.parent.GetChild(num2).name != "Image" && txtInfo.transform.parent.GetChild(num2).name != "bottomline")
			{
				Object.Destroy(txtInfo.transform.parent.GetChild(num2).gameObject);
			}
		}
		for (int num3 = 0; num3 < infoComment.childCount; num3++)
		{
			Object.Destroy(infoComment.GetChild(num3).gameObject);
		}
		for (int num4 = 0; num4 < array.Length; num4++)
		{
			if (array[num4].Split(':')[1] == "0")
			{
				Object.Instantiate(Resources.Load<GameObject>("Browser/campusForumNoCollect"), txtInfo).GetComponent<I18NText>().updateTranslation2(array[num4].Split(':')[0]);
				continue;
			}
			GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Browser/campusForumCollect"), txtInfo);
			gameObject.GetComponent<MultiplyText>().SetNewWidth(I18N.instance.getValue(array[num4].Split(':')[0]));
			gameObject.GetComponent<MultiplyText>().SetContent2(array[num4].Split(':')[0], array[num4].Split(':')[1], I18N.instance.getValue(array[num4].Split(':')[0]));
		}
		if (parobj.img != "")
		{
			if (parobj.img.Split(':')[1] != "0")
			{
				infoImg.gameObject.SetActive(value: false);
				Object.Instantiate(Resources.Load<GameObject>("Image/" + parobj.img.Split(':')[0]), infoImg.transform.parent);
			}
			else
			{
				infoImg.gameObject.SetActive(value: true);
				infoImg.sprite = Resources.Load<Sprite>("Image/" + parobj.img.Split(':')[0]);
			}
		}
		else
		{
			infoImg.gameObject.SetActive(value: false);
			infoImg.sprite = null;
		}
		for (int num5 = 0; num5 < parobj.comment.Count; num5++)
		{
			Object.Instantiate(Resources.Load<GameObject>("Browser/campusCommentList"), infoComment).GetComponent<CampusCommentList>().Init(this, gameManager, num5);
		}
	}
}
