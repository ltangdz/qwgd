using UnityEngine;
using UnityEngine.UI;

public class NumberChangeUI : MonoBehaviour
{
	public delegate void NumberChange();

	public Button _lessButton;

	public Button _addButton;

	public Text _text;

	public int _curIndex;

	public int _minIndex;

	public int _maxIndex = 9;

	private NumberChange _callback;

	private void Start()
	{
		_lessButton.onClick.AddListener(LessNumber);
		_addButton.onClick.AddListener(AddNumber);
		_text.text = string.Concat(_curIndex);
	}

	public void AddCallback(NumberChange d)
	{
		_callback = d;
	}

	private void LessNumber()
	{
		_curIndex--;
		if (_curIndex < _minIndex)
		{
			_curIndex = _maxIndex;
		}
		_text.text = string.Concat(_curIndex);
		if (_callback != null)
		{
			_callback();
		}
	}

	private void AddNumber()
	{
		_curIndex++;
		if (_curIndex > _maxIndex)
		{
			_curIndex = _minIndex;
		}
		_text.text = string.Concat(_curIndex);
		if (_callback != null)
		{
			_callback();
		}
	}
}
