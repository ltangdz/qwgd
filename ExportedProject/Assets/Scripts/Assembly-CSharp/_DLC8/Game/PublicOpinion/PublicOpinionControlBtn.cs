using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Game.PublicOpinion
{
	public class PublicOpinionControlBtn : CustomDialog
	{
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
				Debug.Log("点击初始化：2");
				StartCoroutine(ShowCourse());
			});
		}

		private IEnumerator ShowCourse()
		{
			yield return new WaitForSeconds(0.5f);
			_ = gameManager.player.playerdata.isYulunCourse04;
		}

		public override void AfterShowSize()
		{
		}

		public override void BeforeShowSize()
		{
		}
	}
}
