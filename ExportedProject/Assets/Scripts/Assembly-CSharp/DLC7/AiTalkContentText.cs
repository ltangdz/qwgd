using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7
{
	public class AiTalkContentText : MonoBehaviour
	{
		public Text contentText;

		public void Say(string content, float time)
		{
			contentText.text = "";
			contentText.DOText(content, time).SetEase(Ease.Linear);
		}
	}
}
