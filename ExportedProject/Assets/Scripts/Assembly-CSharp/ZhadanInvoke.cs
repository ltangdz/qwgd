using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ZhadanInvoke : MonoBehaviour
{
	public float lasttime;

	public Text txtTime;

	public Sprite succSprite;

	public GameObject invadeBox;

	public GameObject successBox;

	public bool gameover;

	public GameObject txtTip;

	private float twinkleTime = 1f;

	private GameManager gameManager;

	public float totalTime;

	public Image zhadanRed;

	private Sprite sprite;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Debug.Log("invoke初始化");
		gameManager.homeScene.zhadanInvoke = this;
		sprite = GetComponent<Image>().sprite;
	}

	public void Init(float lastTime, string mailid, bool inter = true)
	{
		twinkleTime = 1f;
		totalTime = lastTime;
		txtTip.SetActive(value: true);
		lasttime = lastTime;
		Debug.Log("打开的邮件：" + mailid);
		ClearItems(mailid);
		txtTime.GetComponent<I18NText>().updateTranslation2(lasttime + "s");
		InvokeRepeating("SetInterval", 1f, 1f);
		zhadanRed = Object.Instantiate(Resources.Load<Image>("zhadan/zhadanred"), gameManager.homeScene.middle);
		StartCoroutine(ShowAni());
		gameManager.homeScene.zhadanInvoke1 = this;
	}

	private IEnumerator ShowAni()
	{
		while (zhadanRed != null)
		{
			float alpha = 1f - lasttime / totalTime;
			if (zhadanRed != null)
			{
				zhadanRed.GetComponent<CanvasGroup>().alpha = alpha;
				zhadanRed.DOFade(0.7f, twinkleTime);
			}
			GetComponent<Image>().DOFade(0.7f, twinkleTime);
			yield return new WaitForSeconds(twinkleTime);
			if (zhadanRed != null)
			{
				zhadanRed.DOFade(1f, twinkleTime);
			}
			GetComponent<Image>().DOFade(1f, twinkleTime);
			yield return new WaitForSeconds(twinkleTime);
		}
	}

	private void SetInterval()
	{
		if (lasttime > 0f)
		{
			lasttime -= 1f;
			gameManager.player.playerdata.boomLastTime = lasttime;
			txtTime.GetComponent<I18NText>().updateTranslation2(lasttime + "s");
			twinkleTime = ((twinkleTime > 0.1f) ? (lasttime * 0.003f) : 0.1f);
			return;
		}
		gameManager.homeScene.newZhadanDialog.btnSearch.interactable = false;
		gameover = true;
		StopAllCoroutines();
		CancelInvoke("SetInterval");
		if (gameManager.homeScene.zhadanInvade != null)
		{
			Object.Destroy(gameManager.homeScene.zhadanInvade.gameObject);
		}
		if (gameManager.homeScene.zhadanInvade1 != null)
		{
			Object.Destroy(gameManager.homeScene.zhadanInvade1.gameObject);
		}
		if (gameManager.homeScene.zhadanCodeRun != null)
		{
			Object.Destroy(gameManager.homeScene.zhadanCodeRun.gameObject);
		}
		gameManager.soundManager.Stop();
		gameManager.musicManager.ResumeVol();
		gameManager.homeScene.ShowVideoTip("3700075");
	}

	public void CrackSucc()
	{
		if (gameManager.homeScene.zhadanInvade.userid == "3300007")
		{
			CancelInvoke("SetInterval");
			GetComponent<Image>().sprite = succSprite;
			invadeBox.SetActive(value: false);
			successBox.SetActive(value: true);
			gameManager.homeScene.zhadanInvoke1 = null;
			Invoke("Fresh", 5f);
			StopAllCoroutines();
		}
		else if (gameManager.homeScene.zhadanInvade.userid == "3300009")
		{
			StopInterval();
		}
	}

	private void Fresh()
	{
		gameManager.homeScene.newZhadanDialog.FreshType();
	}

	public void FreshType()
	{
		CancelInvoke("Fresh");
		CancelInvoke("SetInterval");
		GetComponent<Image>().sprite = sprite;
		invadeBox.SetActive(value: true);
		successBox.SetActive(value: false);
		txtTime.text = "";
		txtTip.SetActive(value: false);
		gameManager.homeScene.zhadanInvoke1 = null;
		if (zhadanRed != null)
		{
			Object.Destroy(zhadanRed.gameObject);
		}
	}

	public void StopInterval()
	{
		CancelInvoke("SetInterval");
		StopAllCoroutines();
		gameManager.homeScene.newZhadanDialog.btnSearch.interactable = false;
	}

	public void ResetInterval()
	{
		StartCoroutine(ShowAni());
		InvokeRepeating("SetInterval", 1f, 1f);
	}

	public void Success()
	{
		if (!gameManager.player.playerdata.OpenedMail.Contains("1500088"))
		{
			gameManager.player.playerdata.OpenedMail.Add("1500088");
		}
		gameManager.homeScene.AddMail("1500089");
		PojieSuccess();
	}

	public void PojieSuccess()
	{
		CancelInvoke("SetInterval");
		GetComponent<Image>().sprite = succSprite;
		invadeBox.SetActive(value: false);
		successBox.SetActive(value: true);
		StopAllCoroutines();
		gameManager.homeScene.zhadanInvoke1 = null;
		Invoke("Fresh", 5f);
		gameManager.saveManager.SavePlayerData();
	}

	public void Failed()
	{
		CancelInvoke("SetInterval");
		StopAllCoroutines();
		int a = (int)lasttime;
		int num = (int)(lasttime * 0.1f);
		num = ((num > 4) ? 4 : ((num < 1) ? 1 : num));
		DOTween.To(() => a, delegate(int x)
		{
			a = x;
		}, 0, num).SetEase(Ease.Linear).OnUpdate(delegate
		{
			txtTime.GetComponent<I18NText>().updateTranslation2(a + "s");
		})
			.OnComplete(delegate
			{
				txtTime.GetComponent<I18NText>().updateTranslation2("0s");
				gameManager.homeScene.ShowVideoTip("3700066");
				gameManager.homeScene.newZhadanDialog.btnSearch.interactable = false;
			});
	}

	private void ClearItems(string sendMail)
	{
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
			string[] array2 = new string[6] { "10605", "10624", "10623", "10633", "10626", "10628" };
			gameManager.homeScene.zhibojiannotebook.DeleteSpecialItem(array2);
			if (gameManager.player.playerdata.camChatInfo.ContainsKey("2300095"))
			{
				gameManager.player.playerdata.camChatInfo.Remove("2300095");
			}
			DelItemList(array2);
		}
		if (sendMail == "1500088")
		{
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
	}

	private void DelItemList(string[] idList)
	{
	}
}
