using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.Titan
{
	public class TitanDocumentDialog : MonoBehaviour
	{
		public Text titleText;

		public Button backButton;

		public GameObject content;

		private TotalPanelDlc7 _totalPanelDlc7;

		private void Start()
		{
			backButton.onClick.AddListener(Back);
		}

		private void Back()
		{
			_totalPanelDlc7.gameObject.SetActive(value: true);
			Object.Destroy(base.gameObject);
		}

		public void InitData(string title, List<string> dataStrList, TotalPanelDlc7 totalPanelDlc7)
		{
			titleText.text = title;
			_totalPanelDlc7 = totalPanelDlc7;
			for (int i = 0; i < dataStrList.Count; i++)
			{
				Object.Instantiate(Resources.Load<TitanReportListItem>("_DLC7/prefabs/Report/TitanReportListItem"), content.transform).InitData(dataStrList[i], i, _totalPanelDlc7);
			}
		}
	}
}
