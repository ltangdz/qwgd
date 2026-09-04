using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BrowserBox : MonoBehaviour
{
	public Button btn_home;

	public Transform contentPanel;

	public GameObject homepanel;

	public GameObject newspanel;

	public GameObject searchPanel;

	public GameObject socialPanel;

	public GameManager gameManager;

	public GameObject currentPanel;

	public RectTransform webLoadLine;

	public Transform btnGroup;

	private List<GameObject> btnList = new List<GameObject>();

	private List<GameObject> btnList2 = new List<GameObject>();

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.homeScene.browserBox = this;
		btn_home.onClick.AddListener(delegate
		{
			CloseAllButton();
			ChangeBrowserPanel(homepanel);
		});
	}

	public void OpenPanel(string name, bool isnew = false)
	{
		WebLoad();
		if (!isnew)
		{
			if (name.Equals("searchPanel"))
			{
				currentPanel.SetActive(value: false);
				searchPanel.SetActive(value: true);
				currentPanel = searchPanel;
			}
			else if (name.Equals("socialPanel"))
			{
				currentPanel.SetActive(value: false);
				socialPanel.SetActive(value: true);
				currentPanel = socialPanel;
			}
		}
		else
		{
			currentPanel.SetActive(value: false);
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("Browser/" + name), contentPanel);
			currentPanel = gameObject;
		}
	}

	public void SearchItem(string search, string searchcontent)
	{
	}

	public void AddSocialPanel(string socialid, bool isadmin)
	{
		WebLoad();
		currentPanel.SetActive(value: false);
		GameObject gameObject = Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetBrowserSocialName()) as GameObject, newspanel.transform);
		Debug.Log(gameObject);
		gameObject.GetComponent<SocialBrowser>().Init(gameManager.dataManager.dic14[socialid], isadmin);
		currentPanel = gameObject;
		AddButton("^toothbook", gameObject);
	}

	public void AddNewsPanel(string newsid)
	{
		WebLoad();
		newsid = "1300001";
		currentPanel.SetActive(value: false);
		GameObject gameObject = Object.Instantiate(Resources.Load("Browser/browser_news" + newsid) as GameObject, newspanel.transform);
		gameObject.GetComponent<NewsBrowser01>().newsid = newsid;
		currentPanel = gameObject;
		AddButton("^new_txt", gameObject);
	}

	public void AddToothBookPanel()
	{
		WebLoad();
		currentPanel.SetActive(value: false);
		AddButton("^toothbook", currentPanel = Object.Instantiate(Resources.Load("Browser/toothbook_login") as GameObject, contentPanel.transform));
	}

	public void AddEmailPanel()
	{
		WebLoad();
		currentPanel.SetActive(value: false);
		AddButton("^btn_mail", currentPanel = Object.Instantiate(Resources.Load("Browser/browser_mail") as GameObject, contentPanel.transform));
	}

	private void AddButton(string content, GameObject panel)
	{
		CloseAllButton();
		GameObject gameObject = Object.Instantiate(Resources.Load("Browser/btn_browser") as GameObject, btnGroup);
		gameObject.GetComponent<ButtonBrowser>().InitButton(content, panel);
		gameObject.name = "btn" + btnList.Count;
		gameObject.GetComponent<ButtonBrowser>().SetShow(ia: true);
		btnList2.Add(gameObject);
		btnList.Add(panel);
	}

	public void CloseBrowser(GameObject browserPanel, GameObject btnbrowser)
	{
		btnList.Remove(browserPanel);
		Object.Destroy(browserPanel);
		btnList2.Remove(btnbrowser);
		Object.Destroy(btnbrowser);
		WebLoad();
		if (btnList.Count > 0)
		{
			currentPanel = btnList[btnList.Count - 1];
			currentPanel.SetActive(value: true);
		}
		else
		{
			currentPanel = homepanel;
			currentPanel.SetActive(value: true);
		}
	}

	public void CloseAllButton()
	{
		for (int i = 0; i < btnList2.Count; i++)
		{
			btnList2[i].GetComponent<ButtonBrowser>().SetShow(ia: false);
		}
	}

	public void ChangeBrowserPanel(GameObject panel)
	{
		WebLoad();
		currentPanel.SetActive(value: false);
		panel.SetActive(value: true);
		if (panel.GetComponent<BrowserMail>() != null)
		{
			panel.GetComponent<BrowserMail>().Refresh();
		}
		currentPanel = panel;
	}

	public void WebLoad()
	{
		StopCoroutine("WebLoadLine");
		webLoadLine.gameObject.SetActive(value: false);
		webLoadLine.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 0f, 1f);
		StartCoroutine(WebLoadLine());
	}

	private IEnumerator WebLoadLine()
	{
		yield return new WaitForSeconds(0.25f);
		webLoadLine.gameObject.SetActive(value: true);
		webLoadLine.localScale = new Vector3(1f, 0f, 1f);
		for (int i = 0; i <= 10; i += 2)
		{
			float num = ((i >= 4) ? Random.Range(0.1f, 0.3f) : Random.Range(0.5f, 1f));
			webLoadLine.DOScaleY((float)i * 0.1f, num);
			yield return new WaitForSeconds(num);
		}
		yield return new WaitForSeconds(0.5f);
		webLoadLine.gameObject.SetActive(value: false);
	}
}
