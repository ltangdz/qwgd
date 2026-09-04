using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZhadanGameOver : MonoBehaviour
{
	[SerializeField]
	private Transform img_whiteblank;

	[SerializeField]
	private Text txt_btn1;

	[SerializeField]
	private Text txt_btn2;

	[SerializeField]
	private Shadow shadow_btn1;

	[SerializeField]
	private Shadow shadow_btn2;

	[SerializeField]
	private Color bluecolor;

	[SerializeField]
	private Color blackcolor;

	[SerializeField]
	private Color blueshadowcolor;

	[SerializeField]
	private Color blackshadowcolor;

	private int pos;

	private GameManager gameManager;

	[SerializeField]
	private GameObject img_light;

	[SerializeField]
	private GameObject img_bk;

	public GameObject window;

	private bool windowOpend;

	private string sendMail = "";

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Transform middle = gameManager.homeScene.middle;
		for (int i = 0; i < middle.childCount; i++)
		{
			if (middle.GetChild(i).name != base.transform.name && middle.GetChild(i).name.IndexOf("zhadandialog") == -1)
			{
				Object.Destroy(middle.GetChild(i).gameObject);
			}
		}
		Sequence sequence = DOTween.Sequence();
		sequence.Append(img_light.transform.DOScale(10f, 2f).OnComplete(delegate
		{
		}));
		sequence.Append(img_bk.GetComponent<CanvasGroup>().DOFade(1f, 0.3f));
		sequence.Play();
	}

	private void DelItemList(string[] idList)
	{
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1))
		{
			if (img_whiteblank.localPosition.y == -58f)
			{
				img_whiteblank.DOLocalMoveY(15f, 0.1f);
				txt_btn2.color = bluecolor;
				shadow_btn2.effectColor = blueshadowcolor;
				txt_btn1.color = blackcolor;
				shadow_btn1.effectColor = blackshadowcolor;
				pos = 0;
			}
		}
		else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Keypad2))
		{
			if (img_whiteblank.localPosition.y == 15f)
			{
				img_whiteblank.DOLocalMoveY(-58f, 0.1f);
				txt_btn1.color = bluecolor;
				shadow_btn1.effectColor = blueshadowcolor;
				txt_btn2.color = blackcolor;
				shadow_btn2.effectColor = blackshadowcolor;
				pos = 1;
			}
		}
		else
		{
			if (!Input.GetKeyUp(KeyCode.KeypadEnter) && !Input.GetKeyUp(KeyCode.Return))
			{
				return;
			}
			if (windowOpend)
			{
				BakMain();
				return;
			}
			switch (pos)
			{
			case 0:
			{
				BakGame();
				Cursor.visible = true;
				gameManager.txt_studio.SetActive(value: false);
				gameManager.istaohuashow = false;
				gameManager.iscancollect = true;
				string[] boomList = gameManager.player.playerdata.boomList;
				if (boomList[boomList.Length - 1] != "0")
				{
					gameManager.homeScene.SendMail(sendMail);
					gameManager.saveManager.SavePlayerData();
				}
				else if (gameManager.player.playerdata.completeHideGame)
				{
					gameManager.homeScene.ShowVideoTip("3700069");
				}
				else
				{
					gameManager.homeScene.ShowVideoTip("3700070");
				}
				if (gameManager.homeScene.browserMail != null)
				{
					gameManager.homeScene.browserMail.Hide();
				}
				Object.Destroy(base.gameObject);
				break;
			}
			case 1:
				windowOpend = true;
				window.SetActive(value: true);
				window.GetComponent<Animator>().Play("Exit Panel In");
				break;
			}
		}
	}

	private void BakGame()
	{
		gameManager.homeScene.newZhadanDialog.FreshType();
		string[] boomList = gameManager.player.playerdata.boomList;
		if (boomList[boomList.Length - 1] != "0")
		{
			sendMail = gameManager.player.playerdata.OpenMail[gameManager.player.playerdata.OpenMail.Count - 1];
			gameManager.player.RemoveMail("admin", sendMail);
			gameManager.player.playerdata.OpenMail.Remove(sendMail);
			if (sendMail == "1500086")
			{
				gameManager.player.playerdata.boomList[0] = "3300007";
				string[] array = new string[4] { "10602", "10620", "10621", "10622" };
				gameManager.homeScene.zhibojiannotebook.DeleteSpecialItem(array);
				DelItemList(array);
			}
			if (sendMail == "1500087")
			{
				gameManager.player.playerdata.boomList[1] = "3300008";
				gameManager.player.playerdata.boomList[2] = "3300009";
				string[] array2 = new string[6] { "10605", "10624", "10606", "10623", "10627", "10628" };
				gameManager.homeScene.zhibojiannotebook.DeleteSpecialItem(array2);
				if (gameManager.player.playerdata.camChatInfo.ContainsKey("2300095"))
				{
					gameManager.player.playerdata.camChatInfo.Remove("2300095");
				}
				DelItemList(array2);
			}
			if (sendMail == "1500088")
			{
				Debug.Log("删除这封邮件内容");
				string[] array3 = new string[1] { "10607" };
				gameManager.homeScene.zhibojiannotebook.DeleteSpecialItem(array3);
				DelItemList(array3);
			}
			if (sendMail == "1500089")
			{
				gameManager.player.playerdata.boomList[3] = "3300010";
				gameManager.player.playerdata.boomList[4] = "3300011";
				gameManager.player.playerdata.completeHideGame = false;
				string[] array4 = new string[9] { "10606", "10631", "10630", "10585", "10632", "10626", "10634", "10635", "10636" };
				gameManager.homeScene.zhibojiannotebook.DeleteSpecialItem(array4);
				gameManager.player.playerdata.videotiplist.Remove("3700067");
				DelItemList(array4);
			}
			gameManager.saveManager.SavePlayerData();
			if (gameManager.homeScene.zhadanInvade != null)
			{
				Object.Destroy(gameManager.homeScene.zhadanInvade.gameObject);
			}
		}
		else if (gameManager.homeScene.zhadanInvade1 != null)
		{
			Object.Destroy(gameManager.homeScene.zhadanInvade1.gameObject);
		}
	}

	public void Cancle()
	{
		window.GetComponent<Animator>().Play("Exit Panel Out");
		Invoke("HideWindow", 1f);
		windowOpend = false;
	}

	public void BakMain()
	{
		Cancle();
		gameManager.saveManager.SavePlayerData();
		gameManager.txt_studio.SetActive(value: true);
		SceneManager.LoadScene("mainScene");
		gameManager.soundManager.Stop();
		gameManager.musicManager.PlayMusicLoop(8);
	}

	private void HideWindow()
	{
		window.SetActive(value: false);
	}
}
