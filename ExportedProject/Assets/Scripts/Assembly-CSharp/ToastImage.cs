using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ToastImage : MonoBehaviour
{
	public Image _bgImage;

	public Text _contentText;

	private Sequence _sequence;

	public void InitData(Color? bgColor, Color? textColor)
	{
		if (!bgColor.HasValue)
		{
			bgColor = Color.black;
		}
		if (!textColor.HasValue)
		{
			textColor = Color.white;
		}
	}

	public void ShowText(string contentStr)
	{
		_contentText.text = contentStr;
		CanvasGroup component = _bgImage.GetComponent<CanvasGroup>();
		if (_sequence != null)
		{
			_sequence.Kill();
		}
		_sequence = DOTween.Sequence();
		_sequence.Append(component.DOFade(1f, 0.3f));
		_sequence.AppendInterval(2f);
		_sequence.Append(component.DOFade(0f, 0.3f));
		_sequence.Play();
	}
}
