using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class VoicePanel : MonoBehaviour
{
	public GameObject voicepanel;

	public VoiceLoadingDialog voiceloadingdialog;

	public GameObject singlevoice;

	public Button btn_voiceback;

	public TotalPanel totalPanel;

	public Button btn_gotovoice;

	public Button btn_gotovoiceitem;

	public Button btn_gototommvoice;

	public Button btn_gototomvoiceitem;

	public GameObject cioitem;

	public Button btn_cioitem;

	public GameObject tomitem;

	public Button btn_tomitem;

	public Text txt_ok;

	public Text txt_tomok;

	public GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btn_voiceback.onClick.AddListener(delegate
		{
			totalPanel.gameObject.SetActive(value: true);
			base.gameObject.SetActive(value: false);
			voiceloadingdialog.gameObject.SetActive(value: false);
		});
		btn_gotovoice.onClick.AddListener(delegate
		{
			ShowLoadingDialog(0);
		});
		btn_gotovoiceitem.onClick.AddListener(delegate
		{
			ShowLoadingDialog(0);
		});
		btn_gototommvoice.onClick.AddListener(delegate
		{
			ShowLoadingDialog(3);
		});
		btn_gototomvoiceitem.onClick.AddListener(delegate
		{
			ShowLoadingDialog(3);
		});
		RefreshCioItem();
	}

	private void OnEnable()
	{
		RefreshCioItem();
	}

	public void RefreshCioItem()
	{
		if (gameManager == null)
		{
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		}
		if (totalPanel.ishascio)
		{
			btn_cioitem.interactable = false;
			txt_ok.text = I18N.instance.getValue("^houtai48");
		}
		if (totalPanel.ishastom)
		{
			btn_tomitem.interactable = false;
			txt_tomok.text = I18N.instance.getValue("^houtai48");
		}
	}

	private void ShowLoadingDialog(int type)
	{
		if ((type != 0 || !totalPanel.ishascio) && (type != 3 || !totalPanel.ishastom))
		{
			voiceloadingdialog.gameObject.SetActive(value: true);
			voiceloadingdialog.Show(type);
		}
	}
}
