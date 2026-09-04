using Honeti;
using UnityEngine;
using UnityEngine.UI;

namespace Aluba.UI
{
	public class AlubaI18NImage : MonoBehaviour
	{
		private Image _image;

		public Sprite[] sprites;

		private void Awake()
		{
			TryGetComponent<Image>(out var component);
			_image = component;
		}

		private void OnEnable()
		{
			if (!(_image == null))
			{
				int gameLang = (int)I18N.instance.gameLang;
				if (sprites.Length > gameLang)
				{
					_image.sprite = sprites[gameLang];
				}
				_image.SetNativeSize();
			}
		}
	}
}
