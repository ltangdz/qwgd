using System.Collections;
using DG.Tweening;
using UnityEngine;

public class LiveBroadingChatLogin : CustomDialog
{
	public GameObject logo;

	public GameObject img_logocircle;

	private void Start()
	{
		gameManager.istaohuashow = true;
		btn_close.onClick.AddListener(delegate
		{
			gameManager.istaohuashow = false;
		});
		StartCoroutine(StartLoad());
	}

	private IEnumerator StartLoad()
	{
		yield return new WaitForSeconds(0.5f);
		logo.transform.DOLocalMoveY(0f, 0.5f);
		logo.transform.DOScaleX(1.3f, 0.5f);
		logo.transform.DOScaleY(1.3f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		img_logocircle.transform.DOLocalRotate(new Vector3(0f, 0f, -1440f), 2f).SetEase(Ease.InOutCirc).OnComplete(delegate
		{
			GameObject obj = Object.Instantiate(Resources.Load("Livebroadcasting/livebroadingchatDialog") as GameObject, gameManager.homeScene.middle);
			obj.GetComponent<LiveBroadingChatBox>().Show();
			obj.gameObject.name = "livebroadingchatDialog";
			Hide();
		});
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
