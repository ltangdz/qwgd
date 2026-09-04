using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Step2DragTargetItem : DragItemTarget<Reason4014StepModel>
{
	public Image _titleBg;

	public Text _titleText;

	public Text _messageText;

	public Image _line1Img;

	public Image _line2Img;

	public Image _messageGroup;

	public List<Sprite> _images;

	protected override void InitUI()
	{
		ResetUI();
	}

	protected override void ClearUI()
	{
		ResetUI();
	}

	protected override void ResetUI()
	{
		_titleText.text = "";
		_line1Img.transform.DOScaleX(0f, 0f);
		_line2Img.transform.DOScaleY(0f, 0f);
		_messageGroup.transform.DOScaleY(0f, 0f);
		_titleBg.sprite = _images[0];
	}

	protected override void IsEnterUI()
	{
		if (base.IsEnter)
		{
			_titleBg.sprite = _images[1];
		}
		else
		{
			_titleBg.sprite = _images[0];
		}
	}

	protected override void DragOk()
	{
		_messageText.text = I18N.instance.getValue(base.DataItem.MessageKey);
		_titleText.text = I18N.instance.getValue(base.DataItem.TitleKey);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(_line1Img.transform.DOScale(1f, 0.1f));
		sequence.Append(_line2Img.transform.DOScale(1f, 0.1f));
		sequence.Append(_messageGroup.transform.DOScale(1f, 0.5f));
		sequence.Play();
	}

	protected override void OnDragEnd()
	{
	}

	public override bool ValidResult()
	{
		if (base.DataItem == null || base.DataItem.Sort != base.Index)
		{
			return false;
		}
		return true;
	}
}
