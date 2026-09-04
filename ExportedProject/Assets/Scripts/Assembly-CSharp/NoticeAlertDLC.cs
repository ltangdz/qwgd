using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class NoticeAlertDLC : MonoBehaviour
{
	public Button sureBtn;

	public Image _tip;

	private Sequence _sequence;

	public Button closeButton;

	private GameManager gameManager;

	public GameObject startConfirm;

	public Button btnRight;

	public Button btnLeft;

	public Text _buttonText;

	public bool isBuy;

	public Text confirmTitle;

	public void InitInfo()
	{
		GetComponent<Animator>().Play("dlc6_noticeAlert");
	}

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void OnEnable()
	{
		if (gameManager.issteam)
		{
			_buttonText.text = I18N.instance.getValue(gameManager.isBuySweetDlc ? "^0E894778-92AC-5C40-18E1-DFDD719FCCD0" : "^406821ED-731E-88E6-6CD7-E93DB027C832");
			Debug.Log("gameManager.issteam:");
		}
		else
		{
			_buttonText.text = I18N.instance.getValue("^0E894778-92AC-5C40-18E1-DFDD719FCCD0");
		}
	}

	private void Start()
	{
		closeButton.onClick.AddListener(delegate
		{
			CloseAlert();
		});
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.IsBuySweetDLC();
		btnLeft.onClick.AddListener(delegate
		{
			startConfirm.GetComponent<Animator>().Play("Exit Panel Out");
		});
		btnRight.onClick.AddListener(delegate
		{
			startConfirm.GetComponent<Animator>().Play("Exit Panel Out");
			CloseAlert();
			if (!gameManager.IsBuyDLC(DLCEnum.HELLO_WORLD))
			{
				gameManager.ValidDLC(8);
			}
			else
			{
				gameManager.PlayDlc(DLCEnum.HELLO_WORLD);
			}
		});
		sureBtn.onClick.AddListener(delegate
		{
			if (!gameManager.isBuyHelloWorldDlc)
			{
				gameManager.ValidDLC(8);
			}
			else
			{
				startConfirm.SetActive(value: true);
				startConfirm.GetComponent<Animator>().Play("Exit Panel In");
				confirmTitle.text = I18N.instance.getValue("^110008_common_89");
			}
		});
		if (gameManager.issteam)
		{
			_buttonText.text = I18N.instance.getValue(gameManager.isBuySweetDlc ? "^0E894778-92AC-5C40-18E1-DFDD719FCCD0" : "^406821ED-731E-88E6-6CD7-E93DB027C832");
			Debug.Log("gameManager.issteam:");
		}
		else
		{
			_buttonText.text = I18N.instance.getValue("^0E894778-92AC-5C40-18E1-DFDD719FCCD0");
		}
	}

	private void ShowTip()
	{
		CanvasGroup component = _tip.GetComponent<CanvasGroup>();
		if (_sequence != null)
		{
			_sequence.Kill();
		}
		_sequence = DOTween.Sequence();
		_sequence.Append(component.DOFade(1f, 0.3f));
		_sequence.AppendInterval(2f);
		_sequence.Append(component.DOFade(0f, 0.3f));
		_sequence.Play();
	}

	private void CloseAlert()
	{
		gameManager.isshowexplainalert = false;
		Object.Destroy(base.gameObject);
	}
}
