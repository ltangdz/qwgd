using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class NoteItemOver : MonoBehaviour
{
	[SerializeField]
	private Image img_fx;

	[SerializeField]
	private Image img_ok;

	[SerializeField]
	private Text txt_content;

	private void Start()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Append(img_fx.DOFillAmount(1f, 0.5f));
		sequence.Append(img_ok.DOFade(1f, 0.5f));
		sequence.Append(txt_content.DOText(I18N.instance.getValue("^Select_Chapter02"), 0.5f));
		sequence.Play();
	}
}
