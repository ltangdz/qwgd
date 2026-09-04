using DG.Tweening;
using DLC7.DDOS;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DLC7.Titan
{
	public class TitanThirdStepLeftButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
	{
		public int index = -1;

		private Image _image;

		public Image Image
		{
			get
			{
				if (_image == null)
				{
					_image = GetComponentInChildren<Image>();
				}
				return _image;
			}
		}

		private void Start()
		{
			Image.DOFade(0f, 0f);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			Image.DOFade(0.3f, 0.1f);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			Image.DOFade(0f, 0.1f);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			TitanEventManager.Instance.NoticeClickLeftPanel(index);
		}
	}
}
