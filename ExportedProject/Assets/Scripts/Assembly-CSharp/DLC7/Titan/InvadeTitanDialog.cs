using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DLC7.Titan
{
	public class InvadeTitanDialog : MonoBehaviour
	{
		public AlubaLoading1 step1;

		public Image[] bgImage;

		public Image[] iconImage;

		public Color[] colors;

		public Sprite[] bgSprites;

		public Sprite[] iconSprites;

		public Text[] step1Texts;

		public CanvasGroup stepCanvasGroup;

		public GameObject step2;

		public Button button;

		public List<Image> loadList;

		private void Start()
		{
			button.onClick.AddListener(Invade);
			for (int i = 0; i < step1Texts.Length; i++)
			{
				Color color = colors[0];
				step1Texts[i].color = color;
			}
			for (int j = 0; j < step1.loadList.Count; j++)
			{
				Color color2 = colors[0];
				if (j > 1)
				{
					color2.a = 0f;
				}
				step1.loadList[j].GetComponent<Image>().color = color2;
			}
			step1.AddCallback(delegate
			{
				Warning();
			});
			Invoke("Loading", 1f);
		}

		private void Loading()
		{
			step1.BeginLoad();
		}

		private void Warning()
		{
			for (int i = 0; i < step1Texts.Length; i++)
			{
				step1Texts[i].color = colors[1];
			}
			bgImage[0].sprite = bgSprites[1];
			iconImage[0].sprite = iconSprites[1];
			for (int j = 0; j < loadList.Count; j++)
			{
				loadList[j].color = colors[1];
			}
			Sequence sequence = DOTween.Sequence();
			sequence.Append(stepCanvasGroup.DOFade(0.5f, 0.5f).SetEase(Ease.Linear));
			sequence.Append(stepCanvasGroup.DOFade(1f, 0.5f).SetEase(Ease.Linear));
			sequence.OnComplete(Step2);
			sequence.SetLoops(2);
			sequence.Play();
		}

		private void Step2()
		{
			step1.transform.DOScale(0f, 0.5f);
			step2.transform.DOScale(1f, 0.5f);
		}

		private void Invade()
		{
			GameManager component = GameObject.Find("GameManager").GetComponent<GameManager>();
			component.player.playerdata.dlc7Invades[2] = 1;
			component.musicManager.Stop();
			SceneManager.LoadSceneAsync("DDOS");
		}
	}
}
