using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DLC7.Titan
{
	public class TitanReportListItem2 : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
	{
		public List<Text> texts;

		public Image bg;

		public List<Sprite> sprites;

		private string _numberStr;

		private Transform _parent;

		public void InitData(string dataStr, Transform parent)
		{
			_parent = parent;
			string[] array = Regex.Split(dataStr, "&&");
			_numberStr = array[0];
			texts[0].text = array[0];
			texts[1].text = array[3];
		}

		private void ShowReport()
		{
			if (_numberStr == "Internal Minutes No.034")
			{
				Object.Instantiate(Resources.Load<GameObject>("_DLC7/prefabs/Report/TitanReport034"), _parent.GetComponentInParent<TiTanDlc7>().transform);
			}
			else if (_numberStr == "Action Record No.235")
			{
				Object.Instantiate(Resources.Load<GameObject>("_DLC7/prefabs/Report/TitanReport235"), _parent.GetComponentInParent<TiTanDlc7>().transform);
			}
			else
			{
				Object.Instantiate(Resources.Load<TitanReportDialog>("_DLC7/prefabs/Report/TitanReport"), _parent.GetComponentInParent<TiTanDlc7>().transform).InitData(_numberStr);
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			bg.sprite = sprites[1];
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			bg.sprite = sprites[0];
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			ShowReport();
		}
	}
}
