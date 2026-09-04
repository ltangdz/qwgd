using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ZhadanInvade1 : CustomDialog
{
	public ZhadanCodeRun codeRunBox;

	public Image invadeImg;

	public Text invadePercent;

	public GameObject zhedang;

	private void Start()
	{
		codeRunBox.Complete();
		StartInterval();
		gameManager.homeScene.zhadanInvade1 = this;
	}

	public void GameOver()
	{
		gameManager.player.playerdata.isZhadanStart = false;
		gameManager.homeScene.ResumeAll();
		if (gameManager.player.playerdata.zhadantime / 60f <= 12f)
		{
			gameManager.UnlockAchievements("aahdefuse");
		}
		gameManager.homeScene.ShowVideoTip("3700078");
		if (gameManager.homeScene.newZhadanDialog != null)
		{
			Object.Destroy(gameManager.homeScene.newZhadanDialog.gameObject);
		}
		gameManager.saveManager.SavePlayerData();
		Object.Destroy(base.gameObject);
	}

	private void StartInterval()
	{
		invadeImg.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0f, 12f), 60f).SetEase(Ease.Linear);
		int a = 100;
		DOTween.To(() => a, delegate(int x)
		{
			a = x;
		}, 0, 60f).SetEase(Ease.Linear).OnUpdate(delegate
		{
			invadePercent.text = a + "%";
		})
			.OnComplete(delegate
			{
				zhedang.SetActive(value: true);
				gameManager.homeScene.ShowVideoTip("3700076");
			});
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
