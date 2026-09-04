using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DLC7.DDOS;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.TitanWeb
{
	public class TitanWebController : MonoBehaviour
	{
		public List<GameObject> panelList;

		private bool[] _showArray = new bool[4] { true, false, false, false };

		private GameManager gameManager;

		public ContentSizeFitter page4ContentSizeFitter;

		public LayoutElement row1;

		public LayoutElement row2;

		public LayoutElement row3;

		public GameManager GameManager
		{
			get
			{
				if (gameManager == null)
				{
					gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
				}
				return gameManager;
			}
		}

		private void Start()
		{
			if (I18N.instance.gameLang == LanguageCode.EN)
			{
				row1.minHeight = 220f;
				row2.minHeight = 220f;
				row3.minHeight = 185f;
			}
			StartCoroutine(Show());
		}

		private IEnumerator Show()
		{
			for (int i = 0; i < panelList.Count; i++)
			{
				panelList[i].gameObject.SetActive(value: false);
			}
			page4ContentSizeFitter.enabled = false;
			yield return new WaitForEndOfFrame();
			page4ContentSizeFitter.enabled = true;
			panelList[3].gameObject.SetActive(value: true);
			yield return new WaitForEndOfFrame();
			page4ContentSizeFitter.enabled = false;
			yield return new WaitForEndOfFrame();
			page4ContentSizeFitter.enabled = true;
			panelList[3].gameObject.SetActive(value: true);
			panelList[(base.name == "_dlc7_TitanWeb") ? 1 : 0].gameObject.SetActive(value: true);
		}

		public void ShowTab(int index)
		{
			_showArray[index] = true;
			StartCoroutine(ShowTab());
			if (!_showArray.Contains(value: false))
			{
				if (!GameManager.player.playerdata.aiSpeakGroupIds.Contains("3910010"))
				{
					DLCEventManager.Instance.NoticeAITalk("3910010");
				}
				GameManager.UnlockAchievements("checktitan");
			}
		}

		private IEnumerator ShowTab()
		{
			page4ContentSizeFitter.enabled = false;
			yield return new WaitForEndOfFrame();
			page4ContentSizeFitter.enabled = true;
		}
	}
}
