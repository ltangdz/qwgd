using UnityEngine;
using UnityEngine.UI;

public class FavouriteBrowser : MonoBehaviour
{
	public Image img_light;

	public TypewriterEffect txt_searchcontent;

	public string searchitem;

	public Button btn_search;

	public Button btn_del;

	public GameObject searchPanel;

	public BrowserBox browserBox;

	public Button btn_email;

	public Button btn_toothbook;

	public InputField inputField;

	private bool isadd = true;

	private void Start()
	{
		browserBox = base.transform.parent.parent.GetComponent<BrowserBox>();
		InvokeRepeating("StartLightAnimation", 0.1f, 0.05f);
		btn_search.onClick.AddListener(delegate
		{
			browserBox.OpenPanel("searchPanel");
			browserBox.SearchItem(searchitem, txt_searchcontent.GetComponent<Text>().text);
		});
		btn_del.onClick.AddListener(delegate
		{
			txt_searchcontent.StartDeleteEffect();
			searchitem = "";
		});
		btn_email.onClick.AddListener(delegate
		{
			browserBox.AddEmailPanel();
		});
		btn_toothbook.onClick.AddListener(delegate
		{
			browserBox.AddToothBookPanel();
		});
	}

	public void SetSearchContent(string content, string itemid)
	{
		if (txt_searchcontent.GetComponent<Text>().text.Equals(""))
		{
			txt_searchcontent.StartEffect(content);
			searchitem = itemid;
		}
		else if (txt_searchcontent.GetComponent<Text>().text.Split('+').Length < 3)
		{
			txt_searchcontent.StartEffect("+" + content, txt_searchcontent.GetComponent<Text>().text);
			searchitem = searchitem + "+" + itemid;
		}
	}

	private void Update()
	{
	}

	private void StartLightAnimation()
	{
		if (isadd)
		{
			img_light.GetComponent<CanvasGroup>().alpha += 0.1f;
		}
		else
		{
			img_light.GetComponent<CanvasGroup>().alpha -= 0.2f;
		}
		if (img_light.GetComponent<CanvasGroup>().alpha >= 1f)
		{
			isadd = false;
		}
		if (img_light.GetComponent<CanvasGroup>().alpha <= 0f)
		{
			isadd = true;
		}
	}
}
