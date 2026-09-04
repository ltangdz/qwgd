using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class YulunBtnControlDialog : CustomDialog
{
	public YulunDialog yulunDialog;

	private void Start()
	{
		Invoke("CanClick", 6f);
	}

	private void CanClick()
	{
		GetComponent<Button>().onClick.AddListener(delegate
		{
			gameManager.soundManager.Stop();
			gameManager.soundManager.PlaySound(37);
			yulunDialog.addPenziList.Clear();
			yulunDialog.yulunNewsControlBox.gameObject.SetActive(value: true);
			Debug.Log("点击初始化：1");
			yulunDialog.yulunNewsControlBox.Init();
			Debug.Log("点击初始化：2");
			StartCoroutine(ShowCourse());
		});
	}

	private IEnumerator ShowCourse()
	{
		yield return new WaitForSeconds(0.5f);
		if (gameManager.player.playerdata.isYulunCourse04 != 1)
		{
			yulunDialog.yulunCourseManager.gameObject.SetActive(value: true);
			yulunDialog.yulunCourseManager.ShowCoursePanel(0, gameManager);
		}
	}

	public override void AfterShowSize()
	{
	}

	public override void BeforeShowSize()
	{
	}
}
