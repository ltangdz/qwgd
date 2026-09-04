using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.Titan
{
	public class TitanWarningText : MonoBehaviour
	{
		public Image bgImage;

		public Text text;

		public AudioClip audioClip;

		public bool hasText;

		private void Start()
		{
			_ = new string[3] { "^110008_game_128", "^110008_other_283", "^110008_other_225" };
			GameObject.Find("GameManager").GetComponent<GameManager>().soundManager.audiosource.PlayOneShot(audioClip);
			text = GetComponentInChildren<Text>();
			bgImage = GetComponent<Image>();
			Random.Range(0, 3);
			StartCoroutine(ResetContentSizeFit());
			if (hasText)
			{
				text.text = I18N.instance.getValue("^110008_common_112");
			}
			bgImage.transform.DOScale(1f, 0.4f);
		}

		private IEnumerator ResetContentSizeFit()
		{
			text.GetComponent<ContentSizeFitter>().enabled = false;
			yield return new WaitForEndOfFrame();
			text.GetComponent<ContentSizeFitter>().enabled = true;
			yield return new WaitForEndOfFrame();
			bgImage.GetComponent<ContentSizeFitter>().enabled = false;
			yield return new WaitForEndOfFrame();
			bgImage.GetComponent<ContentSizeFitter>().enabled = true;
		}
	}
}
