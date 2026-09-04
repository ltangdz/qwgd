using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.Titan
{
	public class TitanAIMessageDialog : TitanVirusBaseDialog
	{
		private List<string> _contentList;

		public List<GameObject> textGroup;

		public Text nameText;

		public Button closeButton;

		private int _curIndex;

		private void Awake()
		{
			nameText.text = base.GameManager.player.playerdata.aiNameDlc7;
		}

		public void InitData(int index)
		{
			closeButton.interactable = false;
			closeButton.onClick.AddListener(base.Hidden);
			_curIndex = index;
			closeButton.interactable = false;
		}

		private IEnumerator Talk()
		{
			float seconds = 1f;
			yield return new WaitForSeconds(seconds);
			_contentList = ((_curIndex == 0) ? new List<string> { "^110008_game_113", "^110008_game_114", "^110008_game_115", "^110008_game_116" } : new List<string> { "^110008_game_110", "^110008_game_111", "^110008_game_112" });
			for (int i = 0; i < _contentList.Count; i++)
			{
				string value = I18N.instance.getValue(_contentList[i]);
				GameObject obj = textGroup[i];
				obj.SetActive(value: true);
				Text componentInChildren = obj.GetComponentInChildren<Text>();
				seconds = (float)value.Length / 10f;
				if (seconds < 1f)
				{
					seconds = 1f;
				}
				componentInChildren.DOText(value, seconds).SetEase(Ease.Linear);
				yield return new WaitForSeconds(seconds + Random.Range(1.5f, 2f));
			}
			closeButton.interactable = true;
		}

		protected override void AfterShow()
		{
			StartCoroutine("Talk");
		}

		protected override void AfterHidden()
		{
			for (int i = 0; i < textGroup.Count; i++)
			{
				GameObject obj = textGroup[i];
				obj.GetComponentInChildren<Text>().text = "";
				obj.SetActive(value: false);
			}
			GetComponentInParent<TitanSecondStepDialog>().Finished((_curIndex != 0) ? TitanSecondStep.AI_TALK2 : TitanSecondStep.AI_TALK1);
			base.gameObject.SetActive(value: false);
		}
	}
}
