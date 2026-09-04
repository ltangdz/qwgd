using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CatchStep1 : MonoBehaviour
{
	public GameObject panel;

	public Text _nameText;

	public Text _numberText;

	public Button _sureButton;

	public Text _tipText;

	public Button _nameButton;

	public Button _numberButton;

	public GameObject _nameDropDown;

	public GameObject _numberDropDown;

	private bool _isAnimation;

	private bool _isShowing;

	private int _nameIndex;

	private int _numberIndex;

	private int _okIndex = 3;

	private void Start()
	{
		_numberButton.onClick.AddListener(delegate
		{
			if (!_isAnimation)
			{
				_isAnimation = true;
				if (_isShowing)
				{
					HideDrop();
				}
				else
				{
					ShowDrop();
				}
			}
		});
		_sureButton.onClick.AddListener(delegate
		{
			if (_okIndex == _numberIndex)
			{
				Success();
			}
			else
			{
				Fail();
			}
		});
	}

	private void Fail()
	{
		ColorUtility.TryParseHtmlString("#f48185", out var color);
		_tipText.color = color;
	}

	private void Success()
	{
		ColorUtility.TryParseHtmlString("#f48185", out var color);
		_tipText.color = color;
		InvadeEvent.Instance.NoticeStepFinished(2, isSuccess: true);
	}

	private void HideDrop()
	{
		if (_isAnimation)
		{
			_numberDropDown.transform.DOScaleY(0f, 0.3f).OnComplete(delegate
			{
				_isShowing = false;
				_isAnimation = false;
			});
		}
	}

	private void ShowDrop()
	{
		if (_isAnimation)
		{
			_numberDropDown.transform.DOScaleY(1f, 0.3f).OnComplete(delegate
			{
				_isAnimation = false;
				_isShowing = true;
			});
		}
	}

	private void ClickText(string arg1, int arg2, string arg3)
	{
		_numberIndex = arg2;
		_numberText.text = arg3;
		if (!_isAnimation)
		{
			_isAnimation = true;
			HideDrop();
		}
	}

	private void OnEnable()
	{
		AlubaUIEvent.Instance.onClickText += ClickText;
	}

	private void OnDisable()
	{
		AlubaUIEvent.Instance.onClickText -= ClickText;
	}
}
