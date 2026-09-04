using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class NewBrowserDialog : CustomDialog
{
	public Transform tabGroup;

	public Transform browserPanel;

	public Text txt_http;

	public RectTransform webLoadLine;

	public List<TabBrowser> tablist;

	public TabBrowser currenttab;

	public RectTransform loadScene;

	public GameObject tab_load;

	public Button btn_min;

	public bool isminimize;

	public HomeBrowser homepanel;

	public GameObject imgDragArea;

	private void Start()
	{
		gameManager.homeScene.newbrowserDialog = this;
		btn_min.onClick.AddListener(Minimize);
		btn_close.onClick.AddListener(delegate
		{
			gameManager.musicManager.ResumeVol();
		});
	}

	private void Minimize()
	{
		isminimize = true;
		base.transform.DOScale(Vector3.zero, 0.3f);
		base.transform.DOMove(gameManager.homeScene.computerButtonBox.btn_search.transform.position, 0.3f);
	}

	public void ResumeMinimize()
	{
		base.transform.DOScale(Vector3.one, 0.3f);
		base.transform.DOLocalMove(Vector3.zero, 0.3f);
		isminimize = false;
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

	public void AddImgSearchItem(List<string> searchID, Sprite imgUrl)
	{
		txt_http.text = "https://www.gogo.com/?wd=image" + imgUrl.name;
		if (!IsHasTab("searchimg"))
		{
			loadScene.gameObject.SetActive(value: true);
			loadScene.localScale = Vector3.one;
			webLoadLine.sizeDelta = new Vector2(0f, 4f);
			webLoadLine.DOKill();
			tab_load.transform.SetAsLastSibling();
			tab_load.SetActive(value: true);
			gameManager.homeScene.eventsystem.SetActive(value: false);
			webLoadLine.DOSizeDelta(new Vector2(1040f, 4f), 1f).SetEase(Ease.InOutCirc).OnComplete(delegate
			{
				loadScene.DOScaleY(0f, 0.2f);
				webLoadLine.sizeDelta = new Vector2(0f, 4f);
				GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetBrowserSearchName()), browserPanel);
				gameObject.GetComponent<SearchBrowser>().StartSearchImg(searchID, imgUrl);
				AddCustomTab("searchimg", "https://www.gogo.com/?wd=image" + imgUrl, gameObject);
				Invoke("CanClick", 1f);
			});
		}
		else
		{
			tabGroup.Find("searchimg").GetComponent<TabBrowser>().ClickPanel();
			tabGroup.Find("searchimg").GetComponent<TabBrowser>().browserPanel.GetComponent<SearchBrowser>().StartSearchImg(searchID, imgUrl);
		}
	}

	private void CanClick()
	{
		gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	public void AddSearchItem(string searchcontent)
	{
		if (!IsHasTab("searchresult"))
		{
			loadScene.gameObject.SetActive(value: true);
			loadScene.localScale = Vector3.one;
			webLoadLine.sizeDelta = new Vector2(0f, 4f);
			webLoadLine.DOKill();
			tab_load.transform.SetAsLastSibling();
			tab_load.SetActive(value: true);
			gameManager.homeScene.eventsystem.SetActive(value: false);
			webLoadLine.DOSizeDelta(new Vector2(1040f, 4f), 1f).SetEase(Ease.InOutCirc).OnComplete(delegate
			{
				loadScene.DOScaleY(0f, 0.2f);
				webLoadLine.sizeDelta = new Vector2(0f, 4f);
				GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetBrowserSearchName()), browserPanel);
				gameObject.GetComponent<SearchBrowser>().StartSearchResult(searchcontent);
				AddCustomTab("searchresult", "https://www.gogo.com/?wd=" + searchcontent, gameObject);
				Invoke("CanClick", 1f);
			});
		}
		else
		{
			tabGroup.Find("searchresult").GetComponent<TabBrowser>().str_https = "https://www.gogo.com/?wd=" + searchcontent;
			tabGroup.Find("searchresult").GetComponent<TabBrowser>().ClickPanel();
			tabGroup.Find("searchresult").GetComponent<TabBrowser>().browserPanel.GetComponent<SearchBrowser>().StartSearchResult(searchcontent);
		}
		txt_http.text = "https://www.gogo.com/?wd=" + searchcontent;
	}

	public void AddNewPanel(DATA2 data2, bool isadmin = false)
	{
		TabBrowser tabBrowser = IsHasTab(data2.tab);
		if (tabBrowser != null)
		{
			tabBrowser.Close(isopenlast: false);
		}
		AddBrowserPanel(data2, isadmin);
		txt_http.text = ReHttp(I18N.instance.getValue(data2.URL));
	}

	public string ReHttp(string htp)
	{
		string result = htp;
		if (htp.IndexOf("https") == -1 || htp.IndexOf("http") == -1)
		{
			result = "https://" + htp;
		}
		return result;
	}

	private void AddBrowserPanel(DATA2 data2, bool isadmin)
	{
		new DATA35();
		loadScene.gameObject.SetActive(value: true);
		loadScene.localScale = Vector3.one;
		webLoadLine.sizeDelta = new Vector2(0f, 4f);
		webLoadLine.DOKill();
		tab_load.transform.SetAsLastSibling();
		tab_load.SetActive(value: true);
		gameManager.homeScene.eventsystem.SetActive(value: false);
		webLoadLine.DOSizeDelta(new Vector2(1040f, 4f), 1f).SetEase(Ease.InOutCirc).OnComplete(delegate
		{
			loadScene.DOScaleY(0f, 0.2f);
			webLoadLine.sizeDelta = new Vector2(0f, 4f);
			switch (data2.type)
			{
			case 0:
			{
				GameObject panel = (GameObject)Object.Instantiate(Resources.Load("Browser/" + data2.Jump.Substring(1)), browserPanel);
				AddTab(data2, panel);
				break;
			}
			case 1:
			{
				GameObject gameObject2 = Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetBrowserSocialName()) as GameObject, browserPanel);
				if (isadmin)
				{
					gameObject2.GetComponent<SocialBrowser>().StartWelcome(gameManager.dataManager.dic14[data2.Jump.Substring(1)], isadmin);
				}
				else
				{
					gameObject2.GetComponent<SocialBrowser>().Init(gameManager.dataManager.dic14[data2.Jump.Substring(1)], isadmin);
				}
				AddTab(data2, gameObject2);
				break;
			}
			case 2:
			{
				GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetWebNewsName()), browserPanel);
				DATA13 d = gameManager.dataManager.dic13[data2.Jump.Substring(1)];
				gameObject.gameObject.SetActive(value: true);
				gameObject.GetComponent<WebNews>().gameManager = gameManager;
				gameObject.GetComponent<WebNews>().Init(d);
				AddTab(data2, gameObject);
				break;
			}
			}
			Invoke("CanClick", 1f);
		});
	}

	public void AddNewAnWangPanel(string prefabname, string tab, string http, AnwangList anwangList)
	{
		if (!IsHasTab(tab))
		{
			loadScene.gameObject.SetActive(value: true);
			loadScene.localScale = Vector3.one;
			webLoadLine.sizeDelta = new Vector2(0f, 4f);
			webLoadLine.DOKill();
			tab_load.transform.SetAsLastSibling();
			tab_load.SetActive(value: true);
			gameManager.homeScene.eventsystem.SetActive(value: false);
			webLoadLine.DOSizeDelta(new Vector2(1040f, 4f), 1f).SetEase(Ease.InOutCirc).OnComplete(delegate
			{
				loadScene.DOScaleY(0f, 0.2f);
				webLoadLine.sizeDelta = new Vector2(0f, 4f);
				GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Browser/" + prefabname), browserPanel);
				gameObject.GetComponent<AnwangInfo>().Info(anwangList, gameManager);
				AddCustomTab(tab, http, gameObject);
				Invoke("CanClick", 1f);
			});
		}
		else
		{
			tabGroup.Find(tab).GetComponent<TabBrowser>().ClickPanel();
			tabGroup.Find(tab).GetComponent<TabBrowser>().browserPanel.GetComponent<AnwangInfo>().Info(anwangList, gameManager);
		}
		txt_http.text = ReHttp(http);
	}

	public void AddNewPanel(string prefabname, string tab, string http, bool isAdmin = false)
	{
		if (!IsHasTab(tab))
		{
			loadScene.gameObject.SetActive(value: true);
			loadScene.localScale = Vector3.one;
			webLoadLine.sizeDelta = new Vector2(0f, 4f);
			webLoadLine.DOKill();
			tab_load.transform.SetAsLastSibling();
			tab_load.SetActive(value: true);
			gameManager.homeScene.eventsystem.SetActive(value: false);
			webLoadLine.DOSizeDelta(new Vector2(1040f, 4f), 1f).SetEase(Ease.InOutCirc).OnComplete(delegate
			{
				loadScene.DOScaleY(0f, 0.2f);
				webLoadLine.sizeDelta = new Vector2(0f, 4f);
				Debug.Log("havenoprefab");
				GameObject panel = (GameObject)Object.Instantiate(Resources.Load("Browser/" + prefabname), browserPanel);
				AddCustomTab(tab, http, panel);
				Invoke("CanClick", 1f);
			});
		}
		else
		{
			tabGroup.Find(tab).GetComponent<TabBrowser>().ClickPanel();
		}
		txt_http.text = ReHttp(http);
	}

	public void RefreshHttp(string http)
	{
		txt_http.text = ReHttp(http);
	}

	public void RefreshTab(string tabcontent, string strhttp, GameObject panel)
	{
		TabBrowser tabBrowser = IsHasTab(tabcontent);
		if (tabBrowser != null)
		{
			tabBrowser.InitButton(tabcontent, strhttp, panel);
		}
	}

	private void AddTab(DATA2 data2, GameObject panel)
	{
		if (tablist.Count >= 6)
		{
			tablist[1].Close(isopenlast: false);
		}
		tab_load.SetActive(value: false);
		GameObject gameObject = Object.Instantiate(Resources.Load("Browser/tab_browser") as GameObject, tabGroup);
		gameObject.GetComponent<TabBrowser>().InitButton(data2, panel);
		tablist.Add(gameObject.GetComponent<TabBrowser>());
		gameObject.name = data2.tab;
		if (currenttab != null)
		{
			currenttab.Hide();
		}
		currenttab = gameObject.GetComponent<TabBrowser>();
		tab_load.transform.SetAsLastSibling();
	}

	private void AddCustomTab(string tabcontent, string strhttp, GameObject panel)
	{
		if (tablist.Count >= 6)
		{
			tablist[1].Close(isopenlast: false);
		}
		tab_load.SetActive(value: false);
		GameObject gameObject = Object.Instantiate(Resources.Load("Browser/tab_browser") as GameObject, tabGroup);
		tablist.Add(gameObject.GetComponent<TabBrowser>());
		gameObject.GetComponent<TabBrowser>().InitButton(tabcontent, strhttp, panel);
		gameObject.name = tabcontent;
		if (currenttab != null)
		{
			currenttab.Hide();
		}
		currenttab = gameObject.GetComponent<TabBrowser>();
		tab_load.transform.SetAsLastSibling();
	}

	private TabBrowser IsHasTab(string str_tab)
	{
		for (int i = 0; i < tablist.Count; i++)
		{
			if (tablist[i].str_tab.ToLower().Equals(str_tab.ToLower()))
			{
				return tablist[i];
			}
		}
		return null;
	}

	public override void BeforeShowSize()
	{
		currenttab.InitButton("Home", "https://www.gogo.com", homepanel.gameObject);
	}

	public override void AfterShowSize()
	{
		if (gameManager.player.playerdata.isCourse01 == 0)
		{
			homepanel.Course01();
		}
	}
}
