using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class SearchLoading : MonoBehaviour
{
	public GameObject searchIcon;

	public GameObject wrongLeft;

	public GameObject wrongRight;

	public Text persent;

	public Text text;

	public GameManager gameManager;

	public Animator ani;

	private void OnEnable()
	{
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		text.GetComponent<I18NText>().updateTranslation2("^search_web");
		ani = base.transform.GetComponent<Animator>();
		Invoke("StartLoad", 0.5f);
	}

	public void Init()
	{
		GetComponent<Animator>().enabled = false;
		base.transform.Find("search_loading/circle_point").gameObject.SetActive(value: false);
		base.transform.Find("search_loading/search_icon").gameObject.SetActive(value: false);
		base.transform.Find("search_loading/search_point").gameObject.SetActive(value: false);
		persent.gameObject.SetActive(value: false);
		wrongLeft.gameObject.SetActive(value: true);
		wrongRight.gameObject.SetActive(value: true);
		wrongLeft.GetComponent<RectTransform>().sizeDelta = new Vector2(7f, 75f);
		wrongRight.GetComponent<RectTransform>().sizeDelta = new Vector2(7f, 75f);
		text.GetComponent<I18NText>().updateTranslation2("^search_fail");
	}

	public void StartLoad()
	{
		ani.Play("ani_search");
	}

	public void CircleLoad()
	{
		ani.Play("ani_search2");
		StartCoroutine(Load());
	}

	private void SearchResult()
	{
		ani.Play("ani_search3");
	}

	public void Result()
	{
		searchIcon.SetActive(value: false);
		searchIcon.SetActive(value: false);
		searchIcon.SetActive(value: false);
		persent.gameObject.SetActive(value: false);
		text.GetComponent<I18NText>().updateTranslation2("^search_fail");
	}

	private IEnumerator Load()
	{
		int a = 0;
		while (a < 100)
		{
			yield return new WaitForSeconds(0.01f);
			a++;
			persent.GetComponent<I18NText>().updateTranslation2(a + "%");
		}
		SearchResult();
	}
}
