using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class SearchItem : MonoBehaviour
{
	public GameManager gameManager;

	public string searchid;

	public Text txt_title;

	public Text txt_link;

	public Text txt_content;

	public Image searchImg;

	public GameObject img_line;

	public DATA2 data;

	public string content;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		GetComponent<Button>().onClick.AddListener(delegate
		{
			OpenLink();
		});
	}

	public void OpenLink()
	{
		if (!(gameManager.homeScene.newbrowserDialog != null))
		{
			return;
		}
		switch (data.type)
		{
		case 0:
			if (!data.Jump.Equals("#0") && data.Jump != null)
			{
				gameManager.homeScene.newbrowserDialog.AddNewPanel(data);
			}
			else
			{
				gameManager.homeScene.newbrowserDialog.AddNewPanel("searchFailed", "searchFailed", "https://www.gogo.com/?wd=null");
			}
			break;
		case 1:
			if (gameManager.player.playerdata.isCourse02 == 0)
			{
				StartCoroutine(DeleteCanvas());
				if (gameManager.homeScene.courseManager.coursepanel02.gameObject.activeInHierarchy)
				{
					gameManager.homeScene.courseManager.coursepanel02.HideCourse();
				}
			}
			gameManager.homeScene.newbrowserDialog.AddNewPanel(data);
			break;
		case 2:
			gameManager.homeScene.newbrowserDialog.AddNewPanel(data);
			break;
		}
		if (data.ID.ToString() == "20196")
		{
			gameManager.UnlockAchievements("negativeratings");
		}
	}

	private IEnumerator DeleteCanvas()
	{
		yield return new WaitForSeconds(0.2f);
		Object.Destroy(base.gameObject.GetComponent<GraphicRaycaster>());
		Object.Destroy(base.gameObject.GetComponent<Canvas>());
	}

	public void SetContent(DATA2 data2)
	{
		data = data2;
		searchid = data2.ID.ToString();
		txt_title.GetComponent<I18NText>().updateTranslation2(data2.title);
		GameManager.SetTextWithEllipsis(txt_link, I18N.instance.getValue(data2.URL));
		txt_content.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue(data2.word));
		if (!data2.missionID.Equals("") && data2.missionID != null)
		{
			gameManager.homeScene.goalDialog.CompleteItem(data2.missionID.Substring(1));
		}
		SetURLUnderLine();
	}

	public void SetContentImg(DATA2 data2)
	{
		data = data2;
		searchid = data2.ID.ToString();
		txt_title.GetComponent<I18NText>().updateTranslation2(data2.title);
		Debug.Log(data2.pic);
		Sprite sprite = Resources.Load<Sprite>("Social/" + data2.pic.Substring(1));
		if (sprite.rect.width < sprite.rect.height)
		{
			searchImg.GetComponent<LayoutElement>().preferredWidth = 350f;
			searchImg.GetComponent<LayoutElement>().preferredHeight = sprite.rect.height / sprite.rect.width * 350f;
		}
		searchImg.sprite = sprite;
		if (!data2.missionID.Equals("") && data2.missionID != null)
		{
			gameManager.homeScene.goalDialog.CompleteItem(data2.missionID.Substring(1));
		}
	}

	private void SetURLUnderLine()
	{
		float preferredWidth = txt_title.preferredWidth;
		Vector2 sizeDelta = img_line.GetComponent<RectTransform>().sizeDelta;
		img_line.GetComponent<RectTransform>().sizeDelta = new Vector2(preferredWidth, sizeDelta.y);
	}
}
