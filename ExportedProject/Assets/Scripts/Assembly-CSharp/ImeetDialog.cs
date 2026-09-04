using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ImeetDialog : MonoBehaviour
{
	public GameObject indexPage;

	public InputField searchBox;

	public Button searchBtn;

	public GameObject noResult;

	public Button btnBakIndex;

	public MultiplyText txt_name;

	public MultiplyText txt_email;

	private GameManager gameManager;

	public bool isneedinit;

	public string txt_emailKey;

	public string txt_hitalkKey;

	public string txt_emailID;

	public string txt_hitalkID;

	private string eventID;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		eventID = gameManager.player.GetEventId();
		searchBtn.onClick.AddListener(Search);
		btnBakIndex.onClick.AddListener(BakIndex);
		InitInfor();
	}

	private void Search()
	{
		string text = searchBox.text;
		if (!(text.Trim() != ""))
		{
			return;
		}
		if (text.ToLower().Trim().Replace(" ", "")
			.Equals("lisasnyder"))
		{
			string text2 = "imeet";
			if (!eventID.Equals("110001"))
			{
				text2 += "_no";
			}
			if (eventID.Equals("110006"))
			{
				text2 = "imeet_dlc6_lisa";
			}
			GameObject panel = Object.Instantiate(Resources.Load<GameObject>("Browser/" + text2), base.transform.parent);
			gameManager.homeScene.newbrowserDialog.RefreshTab("imeet", "https://www.imeet.com/id=LisaSnyder", panel);
			Object.Destroy(base.gameObject);
		}
		else if (text.ToLower().Trim().Replace(" ", "")
			.Equals("kapilmodi"))
		{
			GameObject panel2 = Object.Instantiate(Resources.Load<GameObject>("Browser/imeet03"), base.transform.parent);
			gameManager.homeScene.newbrowserDialog.RefreshTab("imeet", "https://www.imeet.com/id=KapilModi", panel2);
			Object.Destroy(base.gameObject);
		}
		else if (text.ToLower().Trim().Replace(" ", "")
			.Equals("herbertlee"))
		{
			GameObject panel3 = Object.Instantiate(Resources.Load<GameObject>("Browser/imeet_herbert"), base.transform.parent);
			gameManager.homeScene.newbrowserDialog.RefreshTab("imeet", "https://www.imeet.com/id=HerbertLee", panel3);
			Object.Destroy(base.gameObject);
		}
		else if (text.ToLower().Trim().Replace(" ", "")
			.Equals("theresameadows"))
		{
			GameObject panel4 = Object.Instantiate(Resources.Load<GameObject>("Browser/imeet_theresa"), base.transform.parent);
			gameManager.homeScene.newbrowserDialog.RefreshTab("imeet", "https://www.imeet.com/id=TheresaMeadows", panel4);
			Object.Destroy(base.gameObject);
		}
		else if (text.ToLower().Trim().Replace(" ", "")
			.Equals("masontoney"))
		{
			string text3 = "imeet02";
			if (eventID.Equals("110003"))
			{
				text3 += "_no";
				GameObject panel5 = Object.Instantiate(Resources.Load<GameObject>("Browser/" + text3), base.transform.parent);
				gameManager.homeScene.newbrowserDialog.RefreshTab("imeet", "https://www.imeet.com/id=MasonToney", panel5);
				Object.Destroy(base.gameObject);
			}
			else if (eventID.Equals("110002"))
			{
				GameObject panel6 = Object.Instantiate(Resources.Load<GameObject>("Browser/" + text3), base.transform.parent);
				gameManager.homeScene.newbrowserDialog.RefreshTab("imeet", "https://www.imeet.com/id=MasonToney", panel6);
				Object.Destroy(base.gameObject);
			}
			else
			{
				StopCoroutine(Noresult());
				noResult.GetComponent<CanvasGroup>().alpha = 0f;
				StartCoroutine(Noresult());
			}
		}
		else
		{
			StopCoroutine(Noresult());
			noResult.GetComponent<CanvasGroup>().alpha = 0f;
			StartCoroutine(Noresult());
		}
	}

	private IEnumerator Noresult()
	{
		noResult.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
		yield return new WaitForSeconds(3f);
		noResult.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
	}

	public void BakIndex()
	{
		GameObject panel = Object.Instantiate(Resources.Load<GameObject>("Browser/imeethome"), base.transform.parent);
		gameManager.homeScene.newbrowserDialog.RefreshTab("imeet", "https://www.imeet.com", panel);
		Object.Destroy(base.gameObject);
	}

	private void InitInfor()
	{
		if (isneedinit)
		{
			if (txt_name != null)
			{
				string value = I18N.instance.getValue(txt_emailKey);
				txt_name.SetNewWidth(value);
				txt_name.SetContent2(txt_emailKey, txt_emailID, I18N.instance.getValue(txt_emailKey));
			}
			if (txt_email != null)
			{
				string value2 = I18N.instance.getValue(txt_hitalkKey);
				txt_email.SetNewWidth(value2);
				txt_email.SetContent2(txt_hitalkKey, txt_hitalkID, I18N.instance.getValue(txt_hitalkKey));
			}
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			float num = gameManager.homeScene.newbrowserDialog.transform.GetSiblingIndex();
			float num2 = gameManager.homeScene.newbrowserDialog.transform.parent.childCount;
			if (num == num2 - 1f)
			{
				Search();
			}
		}
	}
}
