using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using tnt_deploy;

public class TabBrowser : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public Image img_bk;

	public Button btn_close;

	public Text txt_tab;

	public Sprite[] sprites;

	public GameManager gameManager;

	public GameObject browserPanel;

	public string str_tab;

	public string str_https;

	private bool isactive;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btn_close.onClick.AddListener(Close);
	}

	public void InitButton(DATA2 data2, GameObject panel)
	{
		browserPanel = panel;
		string key = ((data2.tab.Length > 8) ? (data2.tab.Substring(0, 8) + "...") : data2.tab);
		txt_tab.GetComponent<I18NText>().updateTranslation5(key);
		str_tab = data2.tab;
		str_https = ReHttp(I18N.instance.getValue(data2.URL));
	}

	public void InitButton(string tabcontent, string strhttp, GameObject panel)
	{
		browserPanel = panel;
		string key = ((tabcontent.Length > 8) ? (tabcontent.Substring(0, 8) + "...") : tabcontent);
		txt_tab.GetComponent<I18NText>().updateTranslation5(key);
		str_tab = tabcontent;
		str_https = ReHttp(strhttp);
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

	public void SetShow(bool ia)
	{
		isactive = ia;
		img_bk.sprite = sprites[isactive ? 1 : 0];
	}

	public void Close(bool isopenlast = true)
	{
		gameManager.homeScene.newbrowserDialog.tablist.Remove(this);
		Object.Destroy(browserPanel);
		Object.Destroy(base.gameObject);
		if (isopenlast)
		{
			gameManager.homeScene.newbrowserDialog.tablist[gameManager.homeScene.newbrowserDialog.tablist.Count - 1].ClickPanel();
		}
	}

	public void Close()
	{
		gameManager.homeScene.newbrowserDialog.tablist.Remove(this);
		Object.Destroy(browserPanel);
		Object.Destroy(base.gameObject);
		gameManager.homeScene.newbrowserDialog.tablist[gameManager.homeScene.newbrowserDialog.tablist.Count - 1].ClickPanel();
		gameManager.musicManager.ResumeVol();
	}

	public void Hide()
	{
		isactive = false;
		img_bk.sprite = sprites[isactive ? 1 : 0];
		browserPanel.SetActive(value: false);
	}

	public void ClickPanel()
	{
		if (gameManager.homeScene.newbrowserDialog.currenttab != null)
		{
			gameManager.homeScene.newbrowserDialog.currenttab.Hide();
		}
		gameManager.homeScene.newbrowserDialog.transform.SetAsLastSibling();
		gameManager.homeScene.newbrowserDialog.currenttab = this;
		gameManager.homeScene.newbrowserDialog.txt_http.text = str_https;
		browserPanel.SetActive(value: true);
		SetShow(ia: true);
		if (base.name.IndexOf("searchFailed") != -1)
		{
			browserPanel.GetComponent<SearchLoading>().Init();
		}
		if (str_tab == "GrooMusic")
		{
			browserPanel.GetComponent<GrooMusicBrowser>().Init();
		}
		else
		{
			gameManager.musicManager.ResumeVol();
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		ClickPanel();
	}
}
