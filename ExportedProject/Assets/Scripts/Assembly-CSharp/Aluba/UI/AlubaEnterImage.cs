using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Aluba.UI
{
	public class AlubaEnterImage : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		private Image _image;

		public Sprite[] sprites;

		private void Awake()
		{
			TryGetComponent<Image>(out var component);
			_image = component;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (!(_image == null) && sprites.Length > 1)
			{
				_image.sprite = sprites[1];
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (!(_image == null) && sprites.Length != 0)
			{
				_image.sprite = sprites[0];
			}
		}
	}
}
