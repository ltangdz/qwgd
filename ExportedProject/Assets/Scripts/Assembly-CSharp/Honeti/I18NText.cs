using UnityEngine;
using UnityEngine.UI;

namespace Honeti
{
	public class I18NText : MonoBehaviour
	{
		private string _key = "";

		private Text _text;

		private bool _initialized;

		private bool _isValidKey;

		private Font _defaultFont;

		private float _defaultLineSpacing;

		private int _defaultFontSize;

		private TextAnchor _defaultAlignment;

		private readonly string no_breaking_space = "\u00a0";

		public bool isNoBreakingSpace;

		[SerializeField]
		private bool _dontOverwrite;

		[SerializeField]
		private string[] _params;

		private void OnEnable()
		{
			if (!_initialized)
			{
				_init();
				updateTranslation();
			}
		}

		private void OnDestroy()
		{
			if (_initialized)
			{
				I18N.OnLanguageChanged -= _onLanguageChanged;
				I18N.OnFontChanged -= _onFontChanged;
			}
		}

		private void _updateTranslation()
		{
			if (!_initialized)
			{
				_init(isgetkey: false);
			}
			if (!_text)
			{
				return;
			}
			_isValidKey = _key.StartsWith("^");
			if (_isValidKey)
			{
				string value = I18N.instance.getValue(_key.Trim(), _params);
				if (isNoBreakingSpace)
				{
					value.Replace(" ", no_breaking_space);
				}
				_text.text = value;
			}
			else
			{
				_text.text = _key.Trim();
			}
			if (_text.GetComponent<NonBreakingSpaceTextComponent>() != null)
			{
				_text.GetComponent<NonBreakingSpaceTextComponent>().Refresh();
			}
		}

		private void _updateTranslation2(string value)
		{
			if (!_initialized)
			{
				_init(isgetkey: false);
			}
			if ((bool)_text)
			{
				_isValidKey = _key.StartsWith("^");
				if (_isValidKey)
				{
					_text.text = I18N.instance.getValue(_key.Trim(), _params) + value;
					return;
				}
				Text text = _text;
				string format = _key.Trim();
				object[] args = _params;
				text.text = string.Format(format, args);
			}
		}

		public void updateTranslation(bool invalidateKey = false)
		{
			if (invalidateKey)
			{
				_isValidKey = false;
			}
			_updateTranslation();
		}

		public void updateTranslation2(string key)
		{
			_key = key;
			updateTranslation();
		}

		public void updateTranslationAddBlank(string key)
		{
			if (!_initialized)
			{
				_init(isgetkey: false);
			}
			_text.text = "\u3000\u3000" + I18N.instance.getValue(key.Trim());
		}

		public void updateTranslation5(string key)
		{
			if (!_initialized)
			{
				_init(isgetkey: false);
			}
			_text.text = key.Trim();
		}

		public void updateTranslation6(string key)
		{
			if (!_initialized)
			{
				_init(isgetkey: false);
			}
			_text.text = key;
		}

		public void updateTranslation3(string key, string value)
		{
			_key = key;
			_updateTranslation2(value);
		}

		public void updateTranslation4(string key, string param)
		{
			_key = key;
			_params = new string[1] { param };
			_updateTranslation();
		}

		private void _init(bool isgetkey = true)
		{
			_text = GetComponent<Text>();
			_defaultFont = _text.font;
			_defaultLineSpacing = _text.lineSpacing;
			_defaultFontSize = _text.fontSize;
			_defaultAlignment = _text.alignment;
			if (!_text.text.Trim().Equals("") && isgetkey)
			{
				_key = _text.text.Trim();
			}
			_initialized = true;
			if (I18N.instance.useCustomFonts)
			{
				_changeFont(I18N.instance.customFont);
			}
			I18N.OnLanguageChanged += _onLanguageChanged;
			I18N.OnFontChanged += _onFontChanged;
			if (!_key.StartsWith("^"))
			{
				_isValidKey = false;
			}
			else
			{
				_isValidKey = true;
			}
			_ = (bool)_text;
		}

		private void _onLanguageChanged(LanguageCode newLang)
		{
			_updateTranslation();
		}

		private void _onFontChanged(I18NFonts newFont)
		{
			_changeFont(newFont);
		}

		private void _changeFont(I18NFonts f)
		{
			if (_dontOverwrite)
			{
				return;
			}
			if (f != null)
			{
				if ((bool)f.font)
				{
					_text.font = f.font;
				}
				else
				{
					_text.font = _defaultFont;
				}
				if (f.customLineSpacing)
				{
					_text.lineSpacing = f.lineSpacing;
				}
				if (f.customFontSizeOffset)
				{
					_text.fontSize = _defaultFontSize + _defaultFontSize * f.fontSizeOffsetPercent / 100;
				}
				if (f.customAlignment)
				{
					_text.alignment = _getAnchorFromAlignment(f.alignment);
				}
			}
			else
			{
				_text.font = _defaultFont;
				_text.lineSpacing = _defaultLineSpacing;
				_text.fontSize = _defaultFontSize;
				_text.alignment = _defaultAlignment;
			}
		}

		private TextAnchor _getAnchorFromAlignment(TextAlignment alignment)
		{
			switch (_defaultAlignment)
			{
			case TextAnchor.UpperLeft:
			case TextAnchor.UpperRight:
				switch (alignment)
				{
				case TextAlignment.Left:
					return TextAnchor.UpperLeft;
				case TextAlignment.Right:
					return TextAnchor.UpperRight;
				}
				break;
			case TextAnchor.MiddleLeft:
			case TextAnchor.MiddleRight:
				switch (alignment)
				{
				case TextAlignment.Left:
					return TextAnchor.MiddleLeft;
				case TextAlignment.Right:
					return TextAnchor.MiddleRight;
				}
				break;
			case TextAnchor.LowerLeft:
			case TextAnchor.LowerRight:
				switch (alignment)
				{
				case TextAlignment.Left:
					return TextAnchor.LowerLeft;
				case TextAlignment.Right:
					return TextAnchor.LowerRight;
				}
				break;
			}
			return _defaultAlignment;
		}
	}
}
