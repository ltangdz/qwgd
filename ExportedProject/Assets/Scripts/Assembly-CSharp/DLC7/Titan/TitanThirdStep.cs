using System.Collections.Generic;
using UnityEngine;

namespace DLC7.Titan
{
	public class TitanThirdStep : MonoBehaviour
	{
		public GameObject content;

		private List<GameObject> _objs;

		public List<GameObject> Objs
		{
			get
			{
				if (_objs == null)
				{
					_objs = new List<GameObject>();
				}
				return _objs;
			}
		}

		public void Refresh(List<string> reports)
		{
			for (int i = 0; i < Objs.Count; i++)
			{
				Object.Destroy(Objs[i]);
			}
			Objs.Clear();
			if (reports != null)
			{
				for (int j = 0; j < reports.Count; j++)
				{
					TitanReportListItem2 titanReportListItem = Object.Instantiate(Resources.Load<TitanReportListItem2>("_DLC7/prefabs/Report/TitanReportListItem2"), content.transform);
					_objs.Add(titanReportListItem.gameObject);
					titanReportListItem.InitData(reports[j], base.transform);
				}
			}
		}
	}
}
