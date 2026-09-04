using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ReasonStep3Target : DragItemTarget<Reason4014StepModel>
{
	public Text _titleText;

	public Image _lineImg;

	public Image _titleBg;

	private List<Sprite> _lineImages;

	private List<Sprite> _textImages;

	private void Start()
	{
		base.GroupKey = "4014Step3";
		base.DragInType = DragInType.GAMEOBJECT;
	}

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
		_titleBg.color = Color.white;
		Image component = base.OverlapTransform.GetComponent<Image>();
		component.sprite = Resources.Load<Sprite>("_DLC/UI/tuili/tuili_30");
		base.OverlapTransform.gameObject.SetActive(value: true);
		_titleBg.gameObject.SetActive(value: false);
		component.SetNativeSize();
		LineStatus(0);
	}

	protected override void IsEnterUI()
	{
		if (string.IsNullOrEmpty(base.Sourcekey))
		{
			Image component = base.OverlapTransform.GetComponent<Image>();
			if (base.IsEnter)
			{
				LineStatus(1);
				component.sprite = Resources.Load<Sprite>("_DLC/UI/tuili/tuili_33");
			}
			else
			{
				LineStatus(0);
				component.sprite = Resources.Load<Sprite>("_DLC/UI/tuili/tuili_32");
			}
			component.SetNativeSize();
		}
	}

	protected override void DragOk()
	{
		_titleText.text = I18N.instance.getValue(base.DataItem.TitleKey);
		_titleBg.gameObject.SetActive(value: true);
		base.OverlapTransform.gameObject.SetActive(value: false);
		LineStatus(0);
	}

	private void LineStatus(int status)
	{
		switch (status)
		{
		case 0:
			if (_lineImg.sprite.name == "tuili_25" || _lineImg.sprite.name == "tuili_26")
			{
				_lineImg.sprite = Resources.Load<Sprite>("_DLC/UI/tuili/tuili_24");
			}
			else if (_lineImg.sprite.name == "tuili_28" || _lineImg.sprite.name == "tuili_29")
			{
				_lineImg.sprite = Resources.Load<Sprite>("_DLC/UI/tuili/tuili_27");
			}
			break;
		case 1:
			if (_lineImg.sprite.name == "tuili_24" || _lineImg.sprite.name == "tuili_26")
			{
				_lineImg.sprite = Resources.Load<Sprite>("_DLC/UI/tuili/tuili_25");
			}
			else if (_lineImg.sprite.name == "tuili_27" || _lineImg.sprite.name == "tuili_29")
			{
				_lineImg.sprite = Resources.Load<Sprite>("_DLC/UI/tuili/tuili_28");
			}
			break;
		default:
			if (_lineImg.sprite.name == "tuili_24" || _lineImg.sprite.name == "tuili_25")
			{
				_lineImg.sprite = Resources.Load<Sprite>("_DLC/UI/tuili/tuili_26");
			}
			else if (_lineImg.sprite.name == "tuili_27" || _lineImg.sprite.name == "tuili_28")
			{
				_lineImg.sprite = Resources.Load<Sprite>("_DLC/UI/tuili/tuili_29");
			}
			break;
		}
	}

	protected override void OnDragEnd()
	{
		Debug.Log("OnDragEnd");
		if (string.IsNullOrEmpty(base.Sourcekey))
		{
			Image component = base.OverlapTransform.GetComponent<Image>();
			component.sprite = Resources.Load<Sprite>("_DLC/UI/tuili/tuili_30");
			component.SetNativeSize();
		}
	}

	public override bool ValidResult()
	{
		if (base.DataItem == null || base.DataItem.Sort != base.Index)
		{
			LineStatus(2);
			ColorUtility.TryParseHtmlString("#E03D3D", out var color);
			_titleBg.color = color;
			return false;
		}
		return true;
	}
}
