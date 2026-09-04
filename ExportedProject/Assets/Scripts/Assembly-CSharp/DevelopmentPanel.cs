using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class DevelopmentPanel : MonoBehaviour
{
	public Transform middlepanel;

	public GameObject developmentpanel;

	public GameObject totalpanel;

	public GameObject folderpanel;

	public Button btn_singlepersonback;

	[SerializeField]
	private Button btn_sure;

	[SerializeField]
	private Text txt_tip;

	[SerializeField]
	private Image img_voice;

	[SerializeField]
	private Image img_voicelight;

	public List<Sprite> sprites = new List<Sprite>();

	public int anstwer;

	public int correct = 4;

	private bool iscanclick = true;

	public List<GameObject> reportitems = new List<GameObject>();

	public bool isopen;

	public GameObject cioitem;

	public GameObject tomitem;

	public GameObject img_bk;

	public GameObject result01;

	public GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		isopen = gameManager.player.playerdata.isvoiceopen;
		btn_singlepersonback.onClick.AddListener(delegate
		{
			if (result01.activeSelf)
			{
				folderpanel.SetActive(value: true);
				result01.SetActive(value: false);
			}
			else if (folderpanel.activeSelf)
			{
				totalpanel.SetActive(value: true);
				developmentpanel.SetActive(value: false);
			}
			else
			{
				totalpanel.SetActive(value: true);
				developmentpanel.SetActive(value: false);
			}
		});
		btn_sure.onClick.AddListener(Sure);
		RefreshPanel();
	}

	private void OnEnable()
	{
		RefreshPanel();
	}

	private void RefreshPanel()
	{
		txt_tip.color = Color.white;
		img_bk.SetActive(!isopen);
		folderpanel.SetActive(isopen);
		cioitem.SetActive(totalpanel.GetComponent<TotalPanel>().ishascio);
		tomitem.SetActive(totalpanel.GetComponent<TotalPanel>().ishastom);
	}

	private void Sure()
	{
		if (anstwer == -1)
		{
			return;
		}
		iscanclick = false;
		img_voicelight.gameObject.SetActive(value: true);
		img_voicelight.transform.localScale = new Vector3(-1f, 1f, 1f);
		Sequence s = DOTween.Sequence();
		s.Append(img_voicelight.transform.DOLocalMoveX(525f, 1.2f));
		s.Append(img_voicelight.transform.DOScaleX(1f, 0.1f));
		s.Append(img_voicelight.transform.DOLocalMoveX(-441f, 1f));
		s.Append(img_voicelight.transform.DOScaleX(-1f, 0.1f));
		s.Append(img_voicelight.transform.DOLocalMoveX(525f, 1.2f));
		s.Append(img_voicelight.transform.DOScaleX(1f, 0.1f));
		s.Append(img_voicelight.transform.DOLocalMoveX(-441f, 1f)).OnComplete(delegate
		{
			Debug.LogError("jieshu");
			img_voicelight.gameObject.SetActive(value: false);
			iscanclick = true;
			if (anstwer == correct)
			{
				isopen = true;
				totalpanel.GetComponent<TotalPanel>().gameManager.player.playerdata.isvoiceopen = true;
				totalpanel.GetComponent<TotalPanel>().gameManager.saveManager.SavePlayerData();
				RefreshPanel();
			}
			else
			{
				txt_tip.text = I18N.instance.getValue("^houtai93");
				txt_tip.color = Color.red;
				img_voice.gameObject.SetActive(value: false);
				anstwer = -1;
			}
		});
	}

	public void SelectVoice(int spritepos)
	{
		if (iscanclick)
		{
			anstwer = spritepos;
			img_voice.sprite = sprites[spritepos];
			img_voice.gameObject.SetActive(value: true);
		}
	}
}
