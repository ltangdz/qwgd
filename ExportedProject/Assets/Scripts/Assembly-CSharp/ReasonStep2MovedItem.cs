using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ReasonStep2MovedItem : DragItem<Reason4014StepModel>
{
	[SerializeField]
	private Text _contentText;

	public override void InitUI(Reason4014StepModel t)
	{
		_contentText.text = I18N.instance.getValue(t.SampleKey);
	}
}
