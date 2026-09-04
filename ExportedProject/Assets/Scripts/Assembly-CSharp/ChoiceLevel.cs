using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoiceLevel : MonoBehaviour
{
	public Button btnBak;

	public int eventid;

	public bool isDLC;

	public GameObject startConfirm;

	public Button btnRight;

	public Button btnLeft;

	public List<Sprite> btnSprites;

	public RectTransform content;

	private GameManager gameManager;

	private int allPage;

	private int crtPage;

	public Text titleText;

	[SerializeField]
	private List<Chapter> chapters = new List<Chapter>();

	private void OnEnable()
	{
		for (int i = 0; i < chapters.Count; i++)
		{
			if (chapters[i] != null)
			{
				chapters[i].Refresh();
			}
		}
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		allPage = content.transform.childCount - 4;
		if (allPage == crtPage)
		{
			btnLeft.interactable = false;
			btnRight.interactable = false;
			btnLeft.GetComponent<Image>().sprite = btnSprites[0];
			btnRight.GetComponent<Image>().sprite = btnSprites[2];
		}
		btnBak.onClick.AddListener(BtnBak);
		btnRight.onClick.AddListener(MoveToRight);
		btnLeft.onClick.AddListener(MoveToLeft);
	}

	public void BtnBak()
	{
		gameManager.soundManager.PlaySound(16);
		GetComponent<Animator>().SetBool("closeSetting", value: true);
		Invoke("HideSetting", 1f);
	}

	private void HideSetting()
	{
		base.gameObject.SetActive(value: false);
	}

	public void BtnSure()
	{
		gameManager.soundManager.PlaySound(16);
		startConfirm.GetComponent<Animator>().Play("Exit Panel Out");
		Cursor.visible = true;
		gameManager.musicManager.Stop();
		if (gameManager.saveManager.IsHasAutoSave())
		{
			if (eventid > 6 && !gameManager.isBuyDLC(eventid))
			{
				gameManager.ValidDLC(eventid);
				return;
			}
			if (eventid == 1)
			{
				startConfirm.gameObject.SetActive(value: false);
				gameManager.loginPanel.CreateUser();
				base.gameObject.SetActive(value: false);
				return;
			}
			Debug.Log("自动存档文件存在");
			gameManager.saveManager.LoadAutoData();
			gameManager.player.playerdata.GoToEventID(eventid, isclear: true);
			gameManager.txt_studio.SetActive(value: false);
			gameManager.istaohuashow = false;
			gameManager.iscancollect = true;
			SceneManager.LoadScene(gameManager.GetHomeSceneName());
		}
		else
		{
			if (eventid == 1)
			{
				startConfirm.gameObject.SetActive(value: false);
				gameManager.loginPanel.CreateUser();
				base.gameObject.SetActive(value: false);
				return;
			}
			if (eventid > 6)
			{
				if (!gameManager.isBuyDLC(eventid))
				{
					gameManager.ValidDLC(eventid);
					return;
				}
				gameManager.PlayDlc((eventid != 7) ? DLCEnum.HELLO_WORLD : DLCEnum.SWEET_HOME);
			}
			startConfirm.gameObject.SetActive(value: false);
			Debug.Log("自动存档文件不存在");
		}
		BtnBak();
	}

	public void BtnCancel()
	{
		gameManager.soundManager.PlaySound(16);
		startConfirm.GetComponent<Animator>().Play("Exit Panel Out");
		eventid = 0;
	}

	private void MoveToRight()
	{
		if (!btnLeft.interactable)
		{
			btnLeft.interactable = true;
			btnLeft.GetComponent<Image>().sprite = btnSprites[1];
		}
		crtPage++;
		if (crtPage == allPage)
		{
			btnRight.interactable = false;
			btnRight.GetComponent<Image>().sprite = btnSprites[2];
		}
		float x = content.localPosition.x;
		content.DOLocalMoveX(x - 400f, 0.3f);
	}

	private void MoveToLeft()
	{
		if (!btnRight.interactable)
		{
			btnRight.interactable = true;
			btnRight.GetComponent<Image>().sprite = btnSprites[3];
		}
		crtPage--;
		if (crtPage == 0)
		{
			btnLeft.interactable = false;
			btnLeft.GetComponent<Image>().sprite = btnSprites[0];
		}
		float x = content.localPosition.x;
		content.DOLocalMoveX(x + 400f, 0.3f);
	}

	public void IsShow()
	{
	}
}
