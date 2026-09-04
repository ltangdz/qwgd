using UnityEngine;

namespace Honeti
{
	public class I18NTextMesh : MonoBehaviour
	{
		private string _key = "";

		private TextMesh _text;

		private MeshRenderer _renderer;

		private bool _initialized;

		private bool _isValidKey;

		private Font _defaultFont;

		private float _defaultLineSpacing;

		private int _defaultFontSize;

		private TextAlignment _defaultAlignment;

		[SerializeField]
		private bool _dontOverwrite;

		[SerializeField]
		private string[] _params;

		private void OnEnable()
		{
			if (!_initialized)
			{
				_init();
			}
			updateTranslation();
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
			if (!_text)
			{
				return;
			}
			if (!_isValidKey)
			{
				_key = _text.text;
				if (_key.StartsWith("^"))
				{
					_isValidKey = true;
				}
			}
			if (_isValidKey)
			{
				_text.text = I18N.instance.getValue(_key.Trim(), _params);
				return;
			}
			TextMesh text = _text;
			string format = _key.Trim();
			object[] args = _params;
			text.text = string.Format(format, args);
		}

		private void _updateTranslation2()
		{
			if ((bool)_text)
			{
				_isValidKey = _key.StartsWith("^");
				if (_isValidKey)
				{
					_text.text = I18N.instance.getValue(_key.Trim(), _params);
					return;
				}
				TextMesh text = _text;
				string format = _key.Trim();
				object[] args = _params;
				text.text = string.Format(format, args);
			}
		}

		public void updateTranslation2(string key)
		{
			_key = key;
			_updateTranslation2();
		}

		public void updateTranslation(bool invalidateKey = false)
		{
			if (invalidateKey)
			{
				_isValidKey = false;
			}
			_updateTranslation();
		}

		private void _init()
		{
			_text = GetComponent<TextMesh>();
			_renderer = GetComponent<MeshRenderer>();
			_defaultFont = _text.font;
			_defaultLineSpacing = _text.lineSpacing;
			_defaultFontSize = _text.fontSize;
			_defaultAlignment = _text.alignment;
			_key = _text.text;
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
					_renderer.material = f.font.material;
				}
				else
				{
					_text.font = _defaultFont;
					_renderer.material = _defaultFont.material;
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
					_text.alignment = f.alignment;
				}
			}
			else
			{
				_text.font = _defaultFont;
				_renderer.material = _defaultFont.material;
				_text.lineSpacing = _defaultLineSpacing;
				_text.fontSize = _defaultFontSize;
				_text.alignment = _defaultAlignment;
			}
		}
	}
}
