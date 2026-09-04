using Aluba;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _DLC8.Main
{
	public class EmployeeButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		private Text _clickText;

		private EmployeeBook _employeeBook;

		public Image iconImage;

		private int _index;

		private Color[] _colors = new Color[2]
		{
			new Color(0.6392157f, 0.654902f, 0.6745098f, 1f),
			Color.white
		};

		public Text ClickText
		{
			get
			{
				if (_clickText == null)
				{
					_clickText = GetComponent<Text>();
				}
				return _clickText;
			}
		}

		public int Index => _index;

		public void Init(EmployeeBook book, int index, bool isSelected)
		{
			_index = index;
			_employeeBook = book;
			SetSelected(isSelected);
		}

		public void SetSelected(bool isSelected)
		{
			ClickText.color = _colors[isSelected ? 1 : 0];
			iconImage.DOFade(isSelected ? 1f : 0.5f, 0f);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().PlaySound(DLC8SoundType.CLICK_BUTTON);
			_employeeBook.ClickButton(this);
		}
	}
}
