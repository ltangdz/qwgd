using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class BrowserDialog : CustomDialog
{
	public Transform contentPanel;

	public GameObject homepanel;

	public GameObject searchPanel;

	public GameObject currentPanel;

	public Image webLoadLine;

	public Button btn_favourite;

	public GameObject favouritePanel;

	public float maxWidth;

	public GameObject loadScene;

	public InputField textHttp;

	private bool hasAdmin;

	public Transform tabGroup;

	public List<GameObject> tabList = new List<GameObject>();

	public List<GameObject> tabList2 = new List<GameObject>();

	private void Start()
	{
		textHttp.text = "https://www.gogo.com";
		homepanel.GetComponent<Link>().webLink = "https://www.gogo.com";
		btn_favourite.onClick.AddListener(delegate
		{
			textHttp.text = "https://www.gogo.com/favorites";
			OpenPanel("favouritePanel", "https://www.gogo.com/favorites");
		});
	}

	public void AddCanvas()
	{
		StartCoroutine(StartAddCanvas());
	}

	private IEnumerator StartAddCanvas()
	{
		yield return new WaitForSeconds(0.2f);
		if (bk.gameObject.GetComponent<Canvas>() == null)
		{
			bk.gameObject.AddComponent<Canvas>().overrideSorting = true;
			bk.gameObject.GetComponent<Canvas>().sortingOrder = 3;
		}
	}

	public void ReopenPanel(string name, ButtonBrowser btnBrowser, string urlLink = "")
	{
		WebLoad();
		if (name.Equals("toothbook_login"))
		{
			OpenPanel(name, urlLink, isnew: true);
		}
	}

	public void OpenNewsPanel(string name, string url)
	{
		url = AddHttp(url);
		if (IsHasTab("web_news" + url))
		{
			currentPanel.SetActive(value: false);
			GameObject gameObject;
			if (name.Equals("1300011"))
			{
				gameObject = (currentPanel = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetWebNewsName()), contentPanel));
				DATA13 d = gameManager.dataManager.dic13[name];
				gameObject.gameObject.SetActive(value: true);
				gameObject.GetComponent<WebNews>().gameManager = gameManager;
				gameObject.GetComponent<WebNews>().Init(d);
				AddButton("web_news" + url, gameObject);
			}
			else
			{
				gameObject = (GameObject)Object.Instantiate(Resources.Load("Browser/" + name), contentPanel);
				AddButton("web_news" + url, gameObject);
			}
			currentPanel = gameObject;
			textHttp.text = url;
			gameObject.GetComponent<Link>().webLink = url;
		}
	}

	public void OpenPanel(string name, string urlLink = "", bool isnew = false, string title = "")
	{
		WebLoad();
		urlLink = AddHttp(urlLink);
		if (!isnew)
		{
			if (name.Equals("searchPanel") || name.Equals("404"))
			{
				currentPanel.SetActive(value: false);
				searchPanel.SetActive(value: true);
				currentPanel = searchPanel;
				if (name.Equals("404"))
				{
					searchPanel.transform.Find("404").gameObject.SetActive(value: true);
					searchPanel.transform.Find("search").gameObject.SetActive(value: false);
				}
				else
				{
					searchPanel.transform.Find("404").gameObject.SetActive(value: false);
					searchPanel.transform.Find("search").gameObject.SetActive(value: true);
				}
				searchPanel.GetComponent<Link>().webLink = urlLink;
				textHttp.text = urlLink;
				AddButton("searchPanel", searchPanel);
			}
			if (name.Equals("favouritePanel"))
			{
				currentPanel.SetActive(value: false);
				favouritePanel.SetActive(value: true);
				currentPanel = favouritePanel;
				AddButton("favourite", favouritePanel);
			}
			if (name.Equals("toothbook_login"))
			{
				Object.Destroy(currentPanel);
				GameObject gameObject = (currentPanel = (GameObject)Object.Instantiate(Resources.Load("Browser/" + name), contentPanel));
				if (name == "toothbook_login")
				{
					gameObject.GetComponent<Login>().bd = GetComponent<BrowserDialog>();
				}
				gameObject.GetComponent<Link>().webLink = urlLink;
				textHttp.text = urlLink;
			}
			if (name.Equals("imeet"))
			{
				Debug.Log("imeet");
				Object.Destroy(currentPanel);
				(currentPanel = (GameObject)Object.Instantiate(Resources.Load("Browser/imeethome"), contentPanel)).GetComponent<Link>().webLink = urlLink;
				textHttp.text = urlLink;
			}
			return;
		}
		currentPanel.SetActive(value: false);
		int num = -1;
		for (int i = 0; i < tabList.Count; i++)
		{
			if (tabList[i].name == name)
			{
				num = i;
			}
		}
		Debug.Log(name);
		GameObject gameObject2 = ((num != -1) ? tabList[num] : ((GameObject)Object.Instantiate(Resources.Load("Browser/" + name), contentPanel)));
		if (name == "searchFiled")
		{
			name = title;
		}
		if (name == "toothbook_login")
		{
			gameObject2 = ((contentPanel.Find("toothbook_login(Clone)") == null) ? ((GameObject)Object.Instantiate(Resources.Load("Browser/" + name), contentPanel)) : contentPanel.Find("toothbook_login(Clone)").gameObject);
			gameObject2.GetComponent<Login>().bd = GetComponent<BrowserDialog>();
			gameObject2.transform.Find("login_box").gameObject.SetActive(value: true);
			gameObject2.transform.Find("forget_password").gameObject.SetActive(value: false);
			gameObject2.transform.Find("mail_change").gameObject.SetActive(value: false);
			gameObject2.transform.Find("mail_stopSecondSend").gameObject.SetActive(value: false);
		}
		currentPanel = gameObject2;
		AddButton(name, gameObject2);
		if (gameObject2.GetComponent<Link>() != null)
		{
			gameObject2.GetComponent<Link>().webLink = urlLink;
		}
		textHttp.text = urlLink;
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
		if (gameManager.player.playerdata.isCourse01 == 0)
		{
			homepanel.GetComponent<HomeBrowser>().Course01();
		}
	}

	public void AddSocialPanel(string socialid, bool isadmin, string url = "", int count = 0)
	{
		bool num = IsHasTab(I18N.instance.getValue("^toothbook") + "/id:" + socialid + isadmin);
		if (isadmin && !hasAdmin)
		{
			hasAdmin = true;
		}
		if (num)
		{
			WebLoad();
			currentPanel.SetActive(value: false);
			GameObject gameObject = Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetBrowserSocialName()) as GameObject, contentPanel.transform);
			gameObject.GetComponent<Link>().webLink = url;
			if (isadmin)
			{
				gameObject.GetComponent<SocialBrowser>().StartWelcome(gameManager.dataManager.dic14[socialid], isadmin);
			}
			else
			{
				gameObject.GetComponent<SocialBrowser>().Init(gameManager.dataManager.dic14[socialid], isadmin);
			}
			currentPanel = gameObject;
			ButtonBrowser buttonBrowser = AddButton(I18N.instance.getValue("^toothbook") + "/id:" + socialid + isadmin, gameObject, isadmin);
			if (buttonBrowser != null)
			{
				gameObject.GetComponent<SocialBrowser>().buttonBrowser = buttonBrowser;
			}
			textHttp.text = ((url.IndexOf("^") > -1) ? I18N.instance.getValue(url) : url);
		}
	}

	public void WebLoad()
	{
		StopAllCoroutines();
		StartCoroutine(WebLoadLine());
	}

	public void StopLoad()
	{
		StopAllCoroutines();
		webLoadLine.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 4f);
		loadScene.SetActive(value: false);
	}

	private IEnumerator WebLoadLine()
	{
		loadScene.SetActive(value: true);
		webLoadLine.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 4f);
		StopCoroutine("SetLineWidth");
		while (webLoadLine.GetComponent<RectTransform>().rect.width < maxWidth)
		{
			float length;
			if ((double)webLoadLine.GetComponent<RectTransform>().rect.width <= (double)maxWidth * 0.4)
			{
				length = Random.Range(10f, maxWidth * 0.4f);
			}
			else
			{
				loadScene.SetActive(value: false);
				length = Random.Range(10f, maxWidth * 0.5f);
			}
			StartCoroutine(SetLineWidth(webLoadLine.GetComponent<RectTransform>(), length));
			yield return new WaitForSeconds(0.5f);
		}
		webLoadLine.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 4f);
	}

	private IEnumerator SetLineWidth(RectTransform line, float length)
	{
		int i = 0;
		while (i < 50)
		{
			i++;
			float num = webLoadLine.GetComponent<RectTransform>().rect.width;
			if (num + length < maxWidth)
			{
				line.sizeDelta = new Vector2(num + length / 50f, 4f);
				yield return new WaitForSeconds(0.01f);
			}
			else
			{
				line.sizeDelta = new Vector2(maxWidth, 4f);
				loadScene.SetActive(value: false);
				i = 100;
			}
		}
	}

	public void SearchItem(string searchcontent)
	{
		textHttp.text = "https://www.gogo.com/?wd=" + searchcontent;
		searchPanel.GetComponent<SearchBrowser>().StartSearchResult(searchcontent);
		gameManager.player.playerdata.UseSocialMethod(0);
	}

	private ButtonBrowser AddButton(string content, GameObject panel, bool isadmin = false, bool hasAdmin = false)
	{
		bool flag = true;
		content = ((content.IndexOf(">") > -1) ? content.Split('>')[1] : content);
		for (int i = 0; i < tabGroup.childCount; i++)
		{
			if (tabGroup.GetChild(i).GetComponent<ButtonBrowser>().contentLabel == content && !isadmin)
			{
				flag = false;
				tabGroup.GetChild(i).GetComponent<ButtonBrowser>().ClickPanel();
			}
		}
		if (flag && !hasAdmin)
		{
			if (tabGroup.childCount >= 6)
			{
				Debug.Log("tabgroup");
				tabGroup.GetChild(5).GetComponent<ButtonBrowser>().Close();
			}
			CloseAllButton();
			gameManager.soundManager.PlaySound(14);
			GameObject gameObject = Object.Instantiate(Resources.Load("Browser/tab_browser") as GameObject, tabGroup);
			gameObject.GetComponent<ButtonBrowser>().InitButton(content, panel);
			gameObject.name = "tab" + tabList.Count;
			gameObject.GetComponent<ButtonBrowser>().SetShow(ia: true);
			tabList2.Add(gameObject);
			tabList.Add(panel);
			return gameObject.GetComponent<ButtonBrowser>();
		}
		return null;
	}

	private bool IsHasTab(string content)
	{
		bool result = true;
		for (int i = 0; i < tabGroup.childCount; i++)
		{
			if (tabGroup.GetChild(i).GetComponent<ButtonBrowser>().contentLabel == content)
			{
				result = false;
				tabGroup.GetChild(i).GetComponent<ButtonBrowser>().ClickPanel();
			}
		}
		return result;
	}

	public void CloseAllButton()
	{
		for (int i = 0; i < tabList2.Count; i++)
		{
			tabList2[i].GetComponent<ButtonBrowser>().SetShow(ia: false);
		}
	}

	public void CloseBrowser(GameObject browserPanel, GameObject btnbrowser)
	{
		if (tabList2.Count <= 1)
		{
			Close();
		}
		else
		{
			if (btnbrowser.GetComponent<ButtonBrowser>().contentLabel.Contains("Home"))
			{
				return;
			}
			bool num = currentPanel == browserPanel;
			if (btnbrowser.GetComponent<ButtonBrowser>().contentLabel.Contains("searchPane") || btnbrowser.GetComponent<ButtonBrowser>().contentLabel.Equals("favouritePanel"))
			{
				tabList.Remove(browserPanel);
				browserPanel.SetActive(value: false);
				tabList2.Remove(btnbrowser);
				Object.Destroy(btnbrowser);
			}
			else
			{
				tabList.Remove(browserPanel);
				Object.Destroy(browserPanel);
				tabList2.Remove(btnbrowser);
				Object.Destroy(btnbrowser);
			}
			if (num)
			{
				currentPanel = tabList[tabList.Count - 1];
				if (currentPanel != null)
				{
					currentPanel.SetActive(value: true);
					textHttp.text = AddHttp(currentPanel.GetComponent<Link>().webLink);
					Debug.Log(currentPanel.GetComponent<Link>().webLink);
				}
				tabList2[tabList.Count - 1].GetComponent<ButtonBrowser>().SetShow(ia: true);
			}
		}
	}

	public void ChangeBrowserPanel(GameObject panel, bool newWeb = true)
	{
		if (newWeb)
		{
			WebLoad();
		}
		if (currentPanel != null)
		{
			currentPanel.SetActive(value: false);
		}
		panel.SetActive(value: true);
		Debug.Log(panel.name.IndexOf("searchFailed"));
		textHttp.text = AddHttp(panel.GetComponent<Link>().webLink);
		currentPanel = panel;
	}

	public new void SetFront()
	{
		base.transform.SetAsLastSibling();
	}

	public string AddHttp(string urlLink)
	{
		if (urlLink.Trim() != "")
		{
			urlLink = ((urlLink.IndexOf("^") > -1) ? I18N.instance.getValue(urlLink) : urlLink);
			urlLink = urlLink.Replace("：", ":");
			urlLink = urlLink.Replace("http://", "");
			urlLink = urlLink.Replace("https://", "");
			urlLink = urlLink.Replace("www.", "");
			urlLink = "https://www." + urlLink;
		}
		return urlLink;
	}
}
