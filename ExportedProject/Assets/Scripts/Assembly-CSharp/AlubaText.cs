using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlubaText : AlubaUIBase
{
	public Text _text;

	[Header("焦点颜色，设置就会使用 16进制 例如#A1CAF5")]
	public string _focusColor;

	[Header("焦点颜色，设置就会使用 16进制 例如#A1CAF5")]
	public string _loseFocusColor;

	protected override void PointerEnter(PointerEventData eventData)
	{
		if (!string.IsNullOrEmpty(_focusColor))
		{
			ColorUtility.TryParseHtmlString(_focusColor, out var color);
			_text.color = color;
		}
	}

	protected override void PointerExit(PointerEventData eventData)
	{
		if (!string.IsNullOrEmpty(_focusColor))
		{
			ColorUtility.TryParseHtmlString(_loseFocusColor, out var color);
			_text.color = color;
		}
	}

	protected override void TouchDown(PointerEventData eventData)
	{
	}

	protected override void TouchUp(PointerEventData eventData)
	{
		AlubaUIEvent.Instance.ClickText(base.GroupName, base.CurIndex, _text.text);
	}

	protected override void OnClick(PointerEventData eventData)
	{
	}
}
