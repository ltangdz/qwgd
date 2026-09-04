using System.Collections.Generic;
using System.Text.RegularExpressions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DLC7.Titan
{
	public class TitanReportListItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		public List<Text> texts;

		public Image bg;

		public Button button;

		private string _numberStr;

		private TotalPanelDlc7 _totalPanelDlc7;

		public Sprite[] sprites;

		private void Start()
		{
			button.onClick.AddListener(ShowReport);
		}

		public void InitData(string dataStr, int index, TotalPanelDlc7 totalPanelDlc7)
		{
			_totalPanelDlc7 = totalPanelDlc7;
			string[] array = Regex.Split(dataStr, "&&");
			_numberStr = array[0];
			for (int i = 0; i < texts.Count; i++)
			{
				texts[i].text = array[i];
			}
		}

		private void ShowReport()
		{
			if (_numberStr == "Internal Minutes No.034")
			{
				Object.Instantiate(Resources.Load<GameObject>("_DLC7/prefabs/Report/TitanReport034"), _totalPanelDlc7.transform.parent.parent);
			}
			else if (_numberStr == "Action Record No.235")
			{
				Object.Instantiate(Resources.Load<GameObject>("_DLC7/prefabs/Report/TitanReport235"), _totalPanelDlc7.transform.parent.parent);
			}
			else
			{
				Object.Instantiate(Resources.Load<TitanReportDialog>("_DLC7/prefabs/Report/TitanReport"), _totalPanelDlc7.transform.parent.parent).InitData(_numberStr);
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			bg.DOFade(1f, 0f);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			bg.DOFade(0f, 0f);
		}
	}
}
