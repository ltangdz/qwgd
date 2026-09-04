using UnityEngine;
using UnityEngine.UI;

public class Favourite : MonoBehaviour
{
	public Button toothbook;

	public Button imiss;

	public Button shopping;

	public Button happy;

	public GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		toothbook.onClick.AddListener(Toothbook);
		imiss.onClick.AddListener(Imiss);
		shopping.onClick.AddListener(Shopping);
		happy.onClick.AddListener(Happy);
	}

	public void ResetPosition()
	{
		toothbook.transform.Find("img_tip").gameObject.SetActive(value: false);
		toothbook.transform.Find("img_tip").GetComponent<CanvasGroup>().alpha = 0f;
		toothbook.transform.Find("img_tip").GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
		imiss.transform.Find("img_tip").gameObject.SetActive(value: false);
		imiss.transform.Find("img_tip").GetComponent<CanvasGroup>().alpha = 0f;
		imiss.transform.Find("img_tip").GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
		shopping.transform.Find("img_tip").gameObject.SetActive(value: false);
		shopping.transform.Find("img_tip").GetComponent<CanvasGroup>().alpha = 0f;
		shopping.transform.Find("img_tip").GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
		happy.transform.Find("img_tip").gameObject.SetActive(value: false);
		happy.transform.Find("img_tip").GetComponent<CanvasGroup>().alpha = 0f;
		happy.transform.Find("img_tip").GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
	}

	private void Toothbook()
	{
		gameManager.homeScene.newbrowserDialog.AddNewPanel("toothbook_login", "toothbook_login", "https://www.toothbook.com/login");
	}

	private void Email()
	{
	}

	private void Imiss()
	{
		gameManager.homeScene.newbrowserDialog.AddNewPanel("imeethome", "imeet", "https://www.imeet.com");
	}

	private void Shopping()
	{
	}

	private void Happy()
	{
	}
}
