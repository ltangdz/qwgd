using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class FansItem : MonoBehaviour
{
	[SerializeField]
	private Text txt_content;

	[SerializeField]
	private Image img_avatar;

	[SerializeField]
	private RectTransform movetransform;

	[SerializeField]
	private HorizontalLayoutGroup horizontalLayoutGroup;

	public void MoveLeft()
	{
		horizontalLayoutGroup.enabled = false;
		GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
		movetransform.DOLocalMoveX(-500f, 0.3f).OnComplete(delegate
		{
			Object.Destroy(base.gameObject);
		});
	}

	public void Init(string key, string str_avatar)
	{
		img_avatar.sprite = Resources.Load<Sprite>("touxiang/" + str_avatar);
		float num = CalculateLengthOfText(I18N.instance.getValue(key));
		if (num >= 253f)
		{
			num = 253f;
		}
		txt_content.GetComponent<RectTransform>().sizeDelta = new Vector2(num, txt_content.GetComponent<RectTransform>().sizeDelta.y);
		txt_content.text = I18N.instance.getValue(key);
		LayoutRebuilder.ForceRebuildLayoutImmediate(movetransform);
		GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
	}

	private float CalculateLengthOfText(string message)
	{
		TextGenerationSettings generationSettings = txt_content.GetGenerationSettings(Vector2.zero);
		generationSettings.scaleFactor = 1f;
		return txt_content.cachedTextGeneratorForLayout.GetPreferredWidth(message, generationSettings);
	}
}
