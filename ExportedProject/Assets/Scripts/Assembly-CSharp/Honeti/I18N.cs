using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Honeti
{
	public class I18N : MonoBehaviour
	{
		public delegate void LanguageChange(LanguageCode newLanguage);

		public delegate void FontChange(I18NFonts newFont);

		private static LanguageCode _defaultLang;

		private static I18N _instance;

		private const string GAME_LANG = "game_language";

		private Hashtable _langs;

		[SerializeField]
		private LanguageCode _gameLang;

		private string _noTranslationText = "Translation missing for {0}";

		[SerializeField]
		private TextAsset _languageFile;

		private List<LanguageCode> _availableLangs;

		[SerializeField]
		private bool _useCustomFonts;

		private I18NFonts _currentCustomFont;

		[SerializeField]
		private List<I18NFonts> _langFonts;

		public static I18N instance
		{
			get
			{
				if (!_instance)
				{
					_instance = UnityEngine.Object.FindObjectOfType<I18N>();
					_instance.init();
				}
				return _instance;
			}
		}

		public LanguageCode gameLang => _gameLang;

		public bool useCustomFonts => _useCustomFonts;

		public I18NFonts customFont
		{
			get
			{
				if (_useCustomFonts)
				{
					return _currentCustomFont;
				}
				return null;
			}
		}

		public static event LanguageChange OnLanguageChanged;

		public static event FontChange OnFontChanged;

		public void setLanguage(string langCode)
		{
			setLanguage((LanguageCode)Enum.Parse(typeof(LanguageCode), langCode));
		}

		public void setLanguage(LanguageCode langCode)
		{
			if (_langs.ContainsKey(langCode))
			{
				_gameLang = langCode;
			}
			else
			{
				_gameLang = _defaultLang;
				Debug.LogError($"Language {langCode} not recognized! Using default language.");
			}
			PlayerPrefs.SetString("game_language", _gameLang.ToString());
			if (I18N.OnLanguageChanged != null)
			{
				I18N.OnLanguageChanged(_gameLang);
			}
			if (_useCustomFonts)
			{
				I18NFonts newFont = null;
				_currentCustomFont = null;
				if (_langFonts != null && _langFonts.Count > 0)
				{
					foreach (I18NFonts langFont in _langFonts)
					{
						if (langFont.lang == _gameLang)
						{
							newFont = langFont;
							_currentCustomFont = langFont;
							break;
						}
					}
				}
				if (I18N.OnFontChanged != null)
				{
					I18N.OnFontChanged(newFont);
				}
			}
			else
			{
				_currentCustomFont = null;
			}
		}

		public string getValue(string key)
		{
			return getValue(key, null);
		}

		public string GetValueNoSpacing(string key)
		{
			return getValue(key, null).Replace(" ", "\u00a0");
		}

		public string getValue(string key, string[] parameters)
		{
			if (!((_langs[_gameLang] as Hashtable)[key] is string text) || text.Length == 0)
			{
				if (key == "")
				{
					return "";
				}
				return string.Format(_noTranslationText, key);
			}
			if (parameters != null && parameters.Length != 0)
			{
				return string.Format(text.Replace("\\n", Environment.NewLine), parameters);
			}
			return text.Replace("\\n", Environment.NewLine);
		}

		private void init()
		{
			_availableLangs = new List<LanguageCode>();
			_langs = _parseLanguage(_languageFile);
			string language = null;
			if (!PlayerPrefs.HasKey("game_language"))
			{
				switch (Application.systemLanguage)
				{
				case SystemLanguage.Polish:
					language = "PL";
					break;
				case SystemLanguage.English:
					language = "EN";
					break;
				case SystemLanguage.German:
					language = "DE";
					break;
				case SystemLanguage.French:
					language = "FR";
					break;
				case SystemLanguage.Spanish:
					language = "ES";
					break;
				}
			}
			else
			{
				language = PlayerPrefs.GetString("game_language");
			}
			try
			{
				setLanguage(language);
			}
			catch
			{
				setLanguage(_defaultLang);
			}
		}

		private Hashtable _parseLanguage(TextAsset lang)
		{
			Hashtable hashtable = new Hashtable();
			foreach (object value2 in Enum.GetValues(typeof(LanguageCode)))
			{
				hashtable[value2] = new Hashtable();
			}
			string[] array = lang.text.Split(new string[3] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
			string[] array2 = array[0].Split('\t');
			string[] names = Enum.GetNames(typeof(LanguageCode));
			string[] array3 = array2;
			foreach (string value in array3)
			{
				if (Array.IndexOf(names, value) >= 0)
				{
					_availableLangs.Add((LanguageCode)Enum.Parse(typeof(LanguageCode), value));
				}
			}
			array3 = array;
			foreach (string text in array3)
			{
				if (text.StartsWith("#") || !text.StartsWith("^"))
				{
					continue;
				}
				string[] array4 = text.Split('\t');
				for (int j = 0; j < _availableLangs.Count; j++)
				{
					try
					{
						(hashtable[_availableLangs[j]] as Hashtable).Add(array4[0], (array4[j + 1] != "") ? array4[j + 1] : " ");
					}
					catch (Exception ex)
					{
						Debug.LogError(text + "&&&&&&&&&&&" + ex.ToString());
					}
				}
			}
			return hashtable;
		}
	}
}
