using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ReasonStep3Source : DragItemSource<Reason4014StepModel>
{
	[SerializeField]
	private Text _contentText;

	private Color _color;

	protected override void InitUI()
	{
		_contentText.text = I18N.instance.getValue(base.DataItem.TitleKey);
	}

	protected override void ResetUI()
	{
		GetComponent<Image>().color = Color.white;
	}

	protected override void DragOk(Reason4014StepModel data)
	{
		_color.a = 0.5f;
		GetComponent<Image>().color = _color;
		Debug.Log("成功");
	}

	protected override void StartDrag()
	{
		Debug.Log("StartDrag");
		_color = Color.white;
		_color.a = 0.5f;
		GetComponent<Image>().color = _color;
	}
}
