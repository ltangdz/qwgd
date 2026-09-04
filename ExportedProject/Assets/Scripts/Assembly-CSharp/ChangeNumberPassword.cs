using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNumberPassword : CustomDialog
{
	public Text _titleText;

	public Text _tipText;

	public string _password;

	public string _titleString;

	public string _normalColor = "#A7A7A7";

	public string _wrongColor = "#cd2728";

	public int _itemCount = 3;

	public Transform _centerTransform;

	public Button _confirmButton;

	public Button _cancelButton;

	private List<NumberChangeUI> _changeObjs = new List<NumberChangeUI>();

	public string _wrongTipString = "^twodrive_label06";

	public string _tipString = "^3F68AE70-E7A0-144C-5FDC-139ED5FA5D16";

	private void Start()
	{
		_titleText.text = GetText(_titleString);
		_confirmButton.onClick.AddListener(Confirm);
		for (int i = 0; i < _itemCount; i++)
		{
			NumberChangeUI component = ((GameObject)Object.Instantiate(Resources.Load("_DLC/Prefabs/HomeTools/number_change"), _centerTransform)).GetComponent<NumberChangeUI>();
			component.AddCallback(delegate
			{
				SetTip(isWrong: false);
			});
			_changeObjs.Add(component);
		}
		SetTip(isWrong: false);
	}

	public void InitData(string password, string tipString, string titleString)
	{
		_password = password;
		_titleString = titleString;
		_tipString = tipString;
		SetTip(isWrong: false);
	}

	private void InitData()
	{
	}

	private void SetTip(bool isWrong)
	{
		if (isWrong)
		{
			ColorUtility.TryParseHtmlString(_wrongColor, out var color);
			_tipText.text = GetText(_wrongTipString);
			_tipText.color = color;
		}
		else
		{
			_tipText.text = GetText(_tipString);
			ColorUtility.TryParseHtmlString(_normalColor, out var color2);
			_tipText.color = color2;
		}
	}

	private void Confirm()
	{
		string text = "";
		for (int i = 0; i < _changeObjs.Count; i++)
		{
			NumberChangeUI numberChangeUI = _changeObjs[i];
			text += numberChangeUI._curIndex;
		}
		if (text.Equals(_password))
		{
			InvadeEvent.Instance.NoticePasswordSuccess();
			Close();
		}
		else
		{
			SetTip(isWrong: true);
		}
	}

	private string GetText(string text)
	{
		if (text.StartsWith("^"))
		{
			return I18N.instance.getValue(text);
		}
		return text;
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
