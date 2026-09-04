using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class HoutaiNumDialog : MonoBehaviour
{
	public Button btn_close;

	public Button btnSubmit;

	public Text warningTip;

	[SerializeField]
	private string pwkey;

	public int crtInput;

	private bool isSubmit;

	public Color[] colors;

	private GameManager gameManager;

	public FolderItem folderItem;

	public InputField inputField;

	public int type;

	public bool isusername;

	public HoutaiPanel houtaiPanel;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btnSubmit.onClick.AddListener(Submit);
		btn_close.onClick.AddListener(delegate
		{
			base.gameObject.SetActive(value: false);
			houtaiPanel.showPasswordPanel = false;
		});
		if (isusername)
		{
			pwkey = gameManager.player.playerdata.nickname;
		}
	}

	private void Submit()
	{
		if (inputField.text.Trim().ToLower().Equals(pwkey.ToLower()))
		{
			houtaiPanel.showPasswordPanel = false;
			warningTip.gameObject.SetActive(value: true);
			warningTip.text = I18N.instance.getValue("^houtai118");
			warningTip.color = colors[0];
			Object.Destroy(base.gameObject);
			if (type == 0)
			{
				gameManager.player.playerdata.isopenfolder3 = true;
			}
			else if (type == 1)
			{
				gameManager.player.playerdata.isopenfolder4 = true;
			}
			else if (type == 2)
			{
				gameManager.player.playerdata.isopenfolder2 = true;
			}
			if (folderItem != null)
			{
				folderItem.Refresh();
			}
			gameManager.saveManager.SavePlayerData();
		}
		else
		{
			isSubmit = false;
			warningTip.gameObject.SetActive(value: true);
			warningTip.text = I18N.instance.getValue("^invade_phone0204");
			warningTip.color = colors[1];
			CancelInvoke("HideWarning");
			Invoke("HideWarning", 3f);
		}
	}

	private void HideWarning()
	{
		warningTip.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		if ((Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return)) && !isSubmit)
		{
			isSubmit = true;
			Submit();
		}
	}
}
