using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.Reasoning
{
	public class ToggleItem : MonoBehaviour
	{
		public Image backgroundImage;

		public Image checkImage;

		public Text text;

		public Toggle toggle;

		private void Start()
		{
			if (toggle != null)
			{
				toggle.isOn = false;
			}
		}

		public void Hide()
		{
			Tween tween = DOTween.To(() => text.color, delegate(Color x)
			{
				text.color = x;
			}, new Color(text.color.r, text.color.g, text.color.b, 0f), 0.2f);
			tween.OnComplete(delegate
			{
				LayoutElement layoutElement = GetComponent<LayoutElement>();
				if (layoutElement != null)
				{
					DOTween.To(() => layoutElement.preferredHeight, delegate(float x)
					{
						layoutElement.preferredHeight = x;
					}, 0f, 0.2f);
					tween.OnComplete(delegate
					{
						base.gameObject.SetActive(value: false);
					});
				}
			});
		}

		public void Reset()
		{
			text.color = new Color(0.38039216f, 36f / 85f, 44f / 85f, 1f);
		}
	}
}
