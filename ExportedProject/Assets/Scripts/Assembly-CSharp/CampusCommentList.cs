using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class CampusCommentList : MonoBehaviour
{
	public Text userName;

	public Text commentInfoNoCollect;

	public GameObject commentInfoCollect;

	public Text time;

	public GameObject replyBox;

	public Text replyInfo;

	private BrowserForumList parObj;

	private GameManager gameManager;

	private int i;

	public void Init(BrowserCampusCommentInfo obj, GameManager gm, int index)
	{
		parObj = obj.parobj;
		gameManager = gm;
		i = index;
		SetInfo();
	}

	private void SetInfo()
	{
		userName.GetComponent<I18NText>().updateTranslation2(parObj.commenterName[i]);
		if (parObj.comment[i].Split(':')[1] != "0")
		{
			commentInfoCollect.gameObject.SetActive(value: true);
			commentInfoNoCollect.gameObject.SetActive(value: false);
			commentInfoCollect.GetComponent<MultiplyText>().SetNewWidth(I18N.instance.getValue(parObj.comment[i].Split(':')[0]));
			commentInfoCollect.GetComponent<MultiplyText>().SetContent2(parObj.comment[i].Split(':')[0], parObj.comment[i].Split(':')[1], I18N.instance.getValue(parObj.comment[i].Split(':')[0]));
		}
		else
		{
			commentInfoNoCollect.gameObject.SetActive(value: true);
			commentInfoCollect.gameObject.SetActive(value: false);
			commentInfoNoCollect.GetComponent<I18NText>().updateTranslation2(parObj.comment[i].Split(':')[0]);
		}
		time.GetComponent<I18NText>().updateTranslation2(parObj.commenterTime[i]);
		if (parObj.userComment.Count != 0)
		{
			replyBox.SetActive(value: true);
			replyInfo.GetComponent<I18NText>().updateTranslation2(parObj.userComment[i]);
		}
	}
}
