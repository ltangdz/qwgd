using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class NewsBox : MonoBehaviour
{
	public string newsid;

	public Text txt_title;

	public Image img_content;

	public Text txt_content;

	public GameManager gameManager;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Start()
	{
		string[] array = gameManager.dataManager.dic11[gameManager.player.GetEventId()].newsid2.Substring(1).Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			Init(array[i]);
		}
	}

	public void Init(string nid)
	{
		newsid = nid;
		DATA13 dATA = gameManager.dataManager.dic13[newsid];
		txt_title.GetComponent<I18NText>().updateTranslation2(dATA.title);
		txt_content.GetComponent<I18NText>().updateTranslation2(dATA.arrowid);
		img_content.sprite = Resources.Load<Sprite>("News/" + dATA.picname.Substring(1));
		img_content.SetNativeSize();
	}

	public void ShowNews()
	{
		gameManager.homeScene.browserBox.AddNewsPanel(newsid);
	}
}
