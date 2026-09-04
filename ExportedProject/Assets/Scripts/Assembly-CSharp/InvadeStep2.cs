using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InvadeStep2 : MonoBehaviour
{
	public Text _text;

	public Button _startButton;

	public Image _drapDown;

	public Image _downImage;

	public Button _drapButton;

	public Sprite[] _failSprites;

	public Sprite[] _normalSprites;

	private int _index;

	private int _okIndex = 2;

	private bool _isShowing;

	private bool _isAnimation;

	public Text Text
	{
		get
		{
			return _text;
		}
		set
		{
			_text = value;
		}
	}

	public Button StartButton
	{
		get
		{
			return _startButton;
		}
		set
		{
			_startButton = value;
		}
	}

	public Image DrapDown
	{
		get
		{
			return _drapDown;
		}
		set
		{
			_drapDown = value;
		}
	}

	public Button DrapButton
	{
		get
		{
			return _drapButton;
		}
		set
		{
			_drapButton = value;
		}
	}

	public int Index
	{
		get
		{
			return _index;
		}
		set
		{
			_index = value;
		}
	}

	public int OkIndex
	{
		get
		{
			return _okIndex;
		}
		set
		{
			_okIndex = value;
		}
	}

	private void Start()
	{
		_drapButton.onClick.AddListener(delegate
		{
			ColorUtility.TryParseHtmlString("#a1caf5", out var color);
			_text.color = color;
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
				_downImage.sprite = _normalSprites[1];
				_drapButton.image.sprite = _normalSprites[0];
			}
		});
		_startButton.onClick.AddListener(delegate
		{
			if (_okIndex == _index)
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
		_text.color = color;
		_downImage.sprite = _failSprites[1];
		_drapButton.image.sprite = _failSprites[0];
		InvadeEvent.Instance.NoticeStepFinished(2, isSuccess: false);
	}

	private void Success()
	{
		InvadeEvent.Instance.NoticeStepFinished(2, isSuccess: true);
	}

	private void HideDrop()
	{
		if (_isAnimation)
		{
			_drapDown.transform.DOScaleY(0f, 0.3f).OnComplete(delegate
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
			_drapDown.transform.DOScaleY(1f, 0.3f).OnComplete(delegate
			{
				_isAnimation = false;
				_isShowing = true;
			});
		}
	}

	private void ClickText(string arg1, int arg2, string arg3)
	{
		_index = arg2;
		_text.text = arg3;
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
