using UnityEngine;

namespace Honeti
{
	public class I18NSprite : MonoBehaviour
	{
		private bool _initialized;

		private SpriteRenderer _spriteRenderer;

		[SerializeField]
		private Sprite _defaultSprite;

		[SerializeField]
		private I18NSprites[] _sprites;

		private void OnEnable()
		{
			if (!_initialized)
			{
				_init();
			}
			_updateTranslation(I18N.instance.gameLang);
		}

		private void OnDestroy()
		{
			if (_initialized)
			{
				I18N.OnLanguageChanged -= _onLanguageChanged;
			}
		}

		private void _init()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			if (_defaultSprite == null)
			{
				_defaultSprite = _spriteRenderer.sprite;
			}
			_initialized = true;
			I18N.OnLanguageChanged += _onLanguageChanged;
		}

		private void _onLanguageChanged(LanguageCode newLang)
		{
			_updateTranslation(newLang);
		}

		private void _updateTranslation(LanguageCode newLang)
		{
			if (_sprites == null || (_sprites != null && _sprites.Length == 0))
			{
				return;
			}
			Sprite sprite = _defaultSprite;
			for (int i = 0; i < _sprites.Length; i++)
			{
				if (_sprites[i].language == newLang)
				{
					sprite = _sprites[i].image;
					break;
				}
			}
			_spriteRenderer.sprite = sprite;
		}
	}
}
