using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class SearchBrowser : MonoBehaviour
{
	public GameManager gameManager;

	public Transform panel;

	public Button btn_search;

	public Text txt_searchcontent;

	public InputField input;

	public string searchcontent;

	public GameObject gameobject404;

	public GameObject gameobjectsearch;

	public Button btn_backtohome;

	public Text txt_404;

	public ScrollRect scrollRect;

	public Transform focus;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btn_search.onClick.AddListener(delegate
		{
			searchcontent = txt_searchcontent.text;
			gameManager.homeScene.newbrowserDialog.AddSearchItem(searchcontent);
		});
		btn_backtohome.onClick.AddListener(delegate
		{
			gameobject404.SetActive(value: false);
			gameManager.homeScene.newbrowserDialog.txt_http.text = "https://www.gogo.com";
		});
		if (gameManager.player.playerdata.isCourse02 == 0)
		{
			gameManager.homeScene.courseManager.coursepanel02.scrollRect = scrollRect;
		}
	}

	private void Update()
	{
		if ((Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter)) && gameManager.homeScene.newbrowserDialog.isclick)
		{
			float num = gameManager.homeScene.newbrowserDialog.transform.GetSiblingIndex();
			float num2 = gameManager.homeScene.newbrowserDialog.transform.parent.childCount;
			string text = input.text;
			if (base.gameObject.activeInHierarchy && num == num2 - 1f && text != "" && text != " ")
			{
				searchcontent = txt_searchcontent.text;
				gameManager.homeScene.newbrowserDialog.AddSearchItem(searchcontent);
			}
		}
	}

	private IEnumerator SearchResult(string searchcontent)
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		gameobject404.SetActive(value: false);
		gameobjectsearch.SetActive(value: true);
		for (int i = 0; i < panel.childCount; i++)
		{
			Object.Destroy(panel.GetChild(i).gameObject);
		}
		List<DATA2> lists = gameManager.dataManager.GetSearchResults(gameManager.player.GetEventId(), searchcontent);
		input.text = searchcontent;
		for (int j = 0; j < lists.Count; j++)
		{
			GameObject gameObject;
			if (lists[j].pic != "")
			{
				gameObject = (GameObject)Object.Instantiate(Resources.Load("searchitemimg"), panel);
				gameObject.name = "searchitem" + j;
				gameObject.GetComponent<SearchItem>().SetContentImg(lists[j]);
			}
			else
			{
				gameObject = (GameObject)Object.Instantiate(Resources.Load("searchitem"), panel);
				gameObject.name = "searchitem" + j;
				gameObject.GetComponent<SearchItem>().SetContent(lists[j]);
			}
			if (gameManager.player.playerdata.isCourse02 == 0 && j == 0 && gameManager.homeScene != null)
			{
				gameManager.homeScene.courseManager.coursepanel02.searchitem = gameObject;
			}
			yield return new WaitForSeconds(0.2f);
		}
		if (lists.Count == 0)
		{
			gameobject404.SetActive(value: true);
			txt_404.GetComponent<I18NText>().updateTranslation4("^browser_f06", searchcontent);
			gameobjectsearch.SetActive(value: false);
		}
		if (gameManager.player.playerdata.isCourse02 == 0)
		{
			gameManager.homeScene.newbrowserDialog.AddCanvas();
			gameManager.homeScene.courseManager.coursepanel02.browserscrollview = gameManager.homeScene.newbrowserDialog.bk.gameObject;
			gameManager.homeScene.courseManager.ShowCourse2();
		}
		else
		{
			gameManager.homeScene.eventsystem.SetActive(value: true);
		}
	}

	private IEnumerator SearchImg(List<string> searchID)
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		gameobject404.SetActive(value: false);
		gameobjectsearch.SetActive(value: true);
		for (int i = 0; i < panel.childCount; i++)
		{
			Object.Destroy(panel.GetChild(i).gameObject);
		}
		List<DATA2> lists = new List<DATA2>();
		for (int j = 0; j < searchID.Count; j++)
		{
			DATA2 item = gameManager.dataManager.dic2[searchID[j]];
			lists.Add(item);
		}
		input.text = searchcontent;
		for (int k = 0; k < lists.Count; k++)
		{
			GameObject gameObject;
			if (lists[k].pic != "")
			{
				gameObject = (GameObject)Object.Instantiate(Resources.Load("searchitemimg"), panel);
				gameObject.name = "searchitem" + k;
				gameObject.GetComponent<SearchItem>().SetContentImg(lists[k]);
			}
			else
			{
				gameObject = (GameObject)Object.Instantiate(Resources.Load("searchitem"), panel);
				gameObject.name = "searchitem" + k;
				gameObject.GetComponent<SearchItem>().SetContent(lists[k]);
			}
			if (gameManager.player.playerdata.isCourse02 == 0 && k == 0 && gameManager.homeScene != null)
			{
				gameManager.homeScene.courseManager.coursepanel02.searchitem = gameObject;
			}
			yield return new WaitForSeconds(0.2f);
		}
		if (lists.Count == 0)
		{
			gameobject404.SetActive(value: true);
			txt_404.GetComponent<I18NText>().updateTranslation4("^browser_f06", searchcontent);
			gameobjectsearch.SetActive(value: false);
		}
		if (gameManager.player.playerdata.isCourse02 == 0)
		{
			gameManager.homeScene.newbrowserDialog.AddCanvas();
			gameManager.homeScene.courseManager.coursepanel02.browserscrollview = gameManager.homeScene.newbrowserDialog.bk.gameObject;
			gameManager.homeScene.courseManager.ShowCourse2();
		}
		else
		{
			gameManager.homeScene.eventsystem.SetActive(value: true);
		}
	}

	public void StartSearchResult(string searchcontent)
	{
		StartCoroutine(SearchResult(searchcontent));
	}

	public void StartSearchImg(List<string> searchID, Sprite imgUrl)
	{
		if (panel.parent.Find("img_search(Clone)") != null)
		{
			Object.Destroy(panel.parent.Find("img_search(Clone)").gameObject);
		}
		Transform transform = Object.Instantiate(Resources.Load<Transform>("Browser/img_search"), panel.parent);
		transform.SetAsFirstSibling();
		transform.Find("change_image").GetComponent<Image>().sprite = imgUrl;
		transform.Find("change_image").GetComponent<Image>().SetNativeSize();
		float width = transform.Find("change_image").GetComponent<RectTransform>().rect.width;
		float height = transform.Find("change_image").GetComponent<RectTransform>().rect.height;
		if (width > height)
		{
			transform.Find("change_image").GetComponent<RectTransform>().sizeDelta = new Vector2(width / (width / 300f), height / (width / 300f));
		}
		else
		{
			transform.Find("change_image").GetComponent<RectTransform>().sizeDelta = new Vector2(width / (width / 150f), height / (width / 150f));
		}
		StartCoroutine(SearchImg(searchID));
	}
}
