using System.Collections;
using System.Collections.Generic;
using DLC7.DDOS;
using Honeti;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.Titan
{
	public class TitanReportDialog : MonoBehaviour
	{
		public GameObject content;

		public Text titleText;

		public Button closeButton;

		private Dictionary<string, string> _data;

		public List<ContentSizeFitter> contentSizeFitters;

		public Dictionary<string, string> Data
		{
			get
			{
				if (_data == null)
				{
					_data = new Dictionary<string, string>();
					_data.Add("Monitoring Report No.014", "[{\"type\":\"right\",\"key\":\"^110008_report_1700\"},{\"type\":\"daihao\",\"key\":\"^110008_report_1701\"},{\"type\":\"right\",\"key\":\"^110008_report_1702\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1703\"},{\"type\":\"left\",\"key\":\"^110008_report_1704\"},{\"type\":\"left\",\"key\":\"^110008_report_1705\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"biaoti\",\"key\":\"^110008_report_1706\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1707\"},{\"type\":\"left\",\"key\":\"^110008_report_1708\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1709\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1710\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_italic_bold\",\"key\":\"^110008_report_1711\"},{\"type\":\"right_italic_bold\",\"key\":\"^110008_report_1712\"}]");
					_data.Add("ANKH No.079", "[{\"type\":\"right\",\"key\":\"^110008_report_1713\"},{\"type\":\"daihao\",\"key\":\"^110008_report_1714\"},{\"type\":\"right\",\"key\":\"^110008_report_1702\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1716\"},{\"type\":\"left\",\"key\":\"^110008_report_1717\"},{\"type\":\"left\",\"key\":\"^110008_report_1718\"},{\"type\":\"left\",\"key\":\"^110008_report_1719\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"biaoti\",\"key\":\"^110008_report_1720\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1721\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1722\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1723\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_italic_bold\",\"key\":\"^110008_report_1724\"},{\"type\":\"right_italic_bold\",\"key\":\"^110008_report_1725\"}]");
					_data.Add("Test Record No.087", "[{\"type\":\"right\",\"key\":\"^110008_report_1726\"},{\"type\":\"daihao\",\"key\":\"^110008_report_1727\"},{\"type\":\"right\",\"key\":\"^110008_report_1728\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"biaoti\",\"key\":\"^110008_report_1729\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_bold\",\"key\":\"^110008_report_1730\"},{\"type\":\"left\",\"key\":\"^110008_report_1731\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_bold\",\"key\":\"^110008_report_1732\"},{\"type\":\"left\",\"key\":\"^110008_report_1733\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_bold\",\"key\":\"^110008_report_1734\"},{\"type\":\"left\",\"key\":\"^110008_report_1735\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_bold\",\"key\":\"^110008_report_1736\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1737\"},{\"type\":\"left\",\"key\":\"^110008_report_1738\"},{\"type\":\"left\",\"key\":\"^110008_report_1739\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1740\"},{\"type\":\"left\",\"key\":\"^110008_report_1741\"},{\"type\":\"left\",\"key\":\"^110008_report_1742\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1743\"},{\"type\":\"left\",\"key\":\"^110008_report_1744\"},{\"type\":\"left\",\"key\":\"^110008_report_1745\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_italic_bold\",\"key\":\"^110008_report_1746\"},{\"type\":\"right_italic_bold\",\"key\":\"^110008_report_1747\"}]");
					_data.Add("Action Record No.235", "");
					_data.Add("Inspection Report No.001", "[{\"type\":\"right\",\"key\":\"^110008_report_1767\"},{\"type\":\"daihao\",\"key\":\"^110008_report_1768\"},{\"type\":\"right\",\"key\":\"^110008_report_1728\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_bold\",\"key\":\"^110008_report_1770\"},{\"type\":\"left\",\"key\":\"^110008_report_1771\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_bold\",\"key\":\"^110008_report_1772\"},{\"type\":\"left\",\"key\":\"^110008_report_1773\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_bold\",\"key\":\"^110008_report_1774\"},{\"type\":\"left\",\"key\":\"^110008_report_1775\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_bold\",\"key\":\"^110008_report_1776\"},{\"type\":\"left\",\"key\":\"^110008_report_1777\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_bold\",\"key\":\"^110008_report_1778\"},{\"type\":\"left\",\"key\":\"^110008_report_1779\"},{\"type\":\"left\",\"key\":\"^110008_report_1780\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_bold\",\"key\":\"^110008_report_1781\"},{\"type\":\"left\",\"key\":\"^110008_report_1782\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1783\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"right_bold\",\"key\":\"^110008_report_1784\"},{\"type\":\"right_bold\",\"key\":\"^110008_report_1785\"}]");
					_data.Add("Project “Brain” No.017", "[{\"type\":\"right\",\"key\":\"^110008_report_1820\"},{\"type\":\"daihao\",\"key\":\"^110008_report_1821\"},{\"type\":\"right\",\"key\":\"^110008_report_1788\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"biaoti\",\"key\":\"^110008_report_1823\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1824\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1825\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1826\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1827\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1828\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"\",\"key\":\"^110008_report_1829\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"right_bold\",\"key\":\"^110008_report_1784\"},{\"type\":\"right_bold\",\"key\":\"^110008_report_1831\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_italic_bold\",\"key\":\"^110008_report_1832\"},{\"type\":\"right_italic_bold\",\"key\":\"^110008_report_1819\"}]");
					_data.Add("Inspection Report No.076", "[{\"type\":\"right\",\"key\":\"^110008_report_1786\"},{\"type\":\"daihao\",\"key\":\"^110008_report_1787\"},{\"type\":\"right\",\"key\":\"^110008_report_1788\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_bold\",\"key\":\"^110008_report_1770\"},{\"type\":\"left\",\"key\":\"^110008_report_1790\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_bold\",\"key\":\"^110008_report_1772\"},{\"type\":\"left\",\"key\":\"^110008_report_1792\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_bold\",\"key\":\"^110008_report_1793\"},{\"type\":\"left\",\"key\":\"^110008_report_1794\"},{\"type\":\"left\",\"key\":\"^110008_report_1795\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1796\"},{\"type\":\"left\",\"key\":\"^110008_report_1797\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1798\"},{\"type\":\"left\",\"key\":\"^110008_report_1799\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1800\"},{\"type\":\"left\",\"key\":\"^110008_report_1801\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1802\"},{\"type\":\"left\",\"key\":\"^110008_report_1803\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1804\"},{\"type\":\"left\",\"key\":\"^110008_report_1805\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_bold\",\"key\":\"^110008_report_1781\"},{\"type\":\"left\",\"key\":\"^110008_report_1807\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1808\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1809\"},{\"type\":\"left\",\"key\":\"^110008_report_1810\"},{\"type\":\"left\",\"key\":\"^110008_report_1811\"},{\"type\":\"left\",\"key\":\"^110008_report_1812\"},{\"type\":\"left\",\"key\":\"^110008_report_1813\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"right_bold\",\"key\":\"^110008_report_1784\"},{\"type\":\"right_bold\",\"key\":\"^110008_report_1815\"},{\"type\":\"right_bold\",\"key\":\"^110008_report_1816\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left_italic\",\"key\":\"^110008_report_1817\"},{\"type\":\"left_italic_bold\",\"key\":\"^110008_report_1818\"},{\"type\":\"right_italic_bold\",\"key\":\"^110008_report_1819\"}]");
					_data.Add("Internal Minutes No.034", "");
					_data.Add("Advanced Instruction No.014", "[{\"type\":\"right\",\"key\":\"^110008_report_1842\"},{\"type\":\"daihao\",\"key\":\"^110008_report_1843\"},{\"type\":\"right\",\"key\":\"^110008_report_1844\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"biaoti\",\"key\":\"^110008_report_1845\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1846\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1847\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1848\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1849\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"right_bold\",\"key\":\"^110008_report_1850\"}]");
					_data.Add("Internal Report No.007", "[{\"type\":\"right\",\"key\":\"^110008_report_1851\"},{\"type\":\"daihao\",\"key\":\"^110008_report_1852\"},{\"type\":\"right\",\"key\":\"^110008_report_1844\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"biaoti\",\"key\":\"^110008_report_1854\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1855\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1856\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1857\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1858\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"right_bold\",\"key\":\"^110008_report_1859\"}]");
					_data.Add("Internal Report No.001", "[{\"type\":\"right\",\"key\":\"^110008_report_1860\"},{\"type\":\"daihao\",\"key\":\"^110008_report_1852\"},{\"type\":\"right\",\"key\":\"^110008_report_1844\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"biaoti\",\"key\":\"^110008_report_1863\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1864\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1865\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1866\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1867\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1868\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"left\",\"key\":\"^110008_report_1869\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"right_bold\",\"key\":\"^110008_report_1870\"}]");
					_data.Add("X", "[{\"type\":\"right\",\"key\":\"^110008_report_1871\"},{\"type\":\"daihao\",\"key\":\"^110008_report_1872\"},{\"type\":\"right\",\"key\":\"^110008_report_1873\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"center_bold\",\"key\":\"^110008_report_1874\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"center\",\"key\":\"^110008_report_1875\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"center\",\"key\":\"^110008_report_1876\"},{\"type\":\"center\",\"key\":\"^110008_report_1877\"},{\"type\":\"center\",\"key\":\"^110008_report_1878\"},{\"type\":\"center\",\"key\":\"^110008_report_1879\"},{\"type\":\"center\",\"key\":\"^110008_report_1880\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"center\",\"key\":\"^110008_report_1881\"},{\"type\":\"center\",\"key\":\"^110008_report_1882\"},{\"type\":\"center\",\"key\":\"^110008_report_1883\"},{\"type\":\"center\",\"key\":\"^110008_report_1884\"},{\"type\":\"center\",\"key\":\"^110008_report_1885\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"center\",\"key\":\"^110008_report_1886\"},{\"type\":\"center\",\"key\":\"^110008_report_1887\"},{\"type\":\"center\",\"key\":\"^110008_report_1888\"},{\"type\":\"center\",\"key\":\"^110008_report_1889\"},{\"type\":\"center\",\"key\":\"^110008_report_1890\"},{\"type\":\"center\",\"key\":\"^110008_report_1891\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"center\",\"key\":\"^110008_report_1892\"},{\"type\":\"center\",\"key\":\"^110008_report_1893\"},{\"type\":\"\",\"key\":\"\"},{\"type\":\"center_bold\",\"key\":\"^110008_report_1894\"}]");
				}
				return _data;
			}
		}

		private void Start()
		{
			closeButton.onClick.AddListener(Close);
		}

		private void Close()
		{
			TitanEventManager.Instance.NoticeShowReport(titleText.text);
			Object.Destroy(base.gameObject);
		}

		public void InitData(string numberStr)
		{
			titleText.text = numberStr;
			if (!Data.ContainsKey(numberStr))
			{
				Debug.LogError("报告没有Key:" + numberStr);
				return;
			}
			List<Dictionary<string, string>> list = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(Data[numberStr]);
			GameObject gameObject = null;
			Text[] array = null;
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, string> dictionary = list[i];
				if (dictionary.Count == 0)
				{
					Object.Instantiate(Resources.Load<Text>("_DLC7/prefabs/Report/ReportText"), content.transform).text = "";
					continue;
				}
				string text = dictionary["type"];
				string text2 = dictionary["key"];
				Debug.Log("type:" + text + "key:" + text2 + "-------------:" + I18N.instance.getValue(text2));
				if (text.StartsWith("h_"))
				{
					if (gameObject == null)
					{
						gameObject = Object.Instantiate(Resources.Load<GameObject>("_DLC7/prefabs/Report/TitanReportHorizontal"), content.transform);
						array = gameObject.GetComponentsInChildren<Text>();
						array[0].text = I18N.instance.getValue(text2);
					}
					else
					{
						array[1].text = I18N.instance.getValue(text2);
						gameObject = null;
						array = null;
					}
					continue;
				}
				Text text3 = Object.Instantiate(Resources.Load<Text>("_DLC7/prefabs/Report/ReportText"), content.transform);
				RectTransform component = text3.GetComponent<RectTransform>();
				text3.text = ((string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2)) ? "" : I18N.instance.getValue(text2));
				if (text == "daihao" || text == "biaoti")
				{
					component.sizeDelta = new Vector2(component.sizeDelta.x, 40f);
					text3.fontSize = ((text == "daihao") ? 24 : 20);
					text3.alignment = ((text == "daihao") ? TextAnchor.MiddleRight : TextAnchor.MiddleCenter);
					text3.fontStyle = FontStyle.Bold;
				}
				else if (text.StartsWith("right"))
				{
					text3.alignment = TextAnchor.MiddleRight;
				}
				else if (text.StartsWith("center"))
				{
					text3.alignment = TextAnchor.MiddleCenter;
				}
				else if (text.StartsWith("left"))
				{
					text3.alignment = TextAnchor.MiddleLeft;
				}
				else if ((string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2)) && !text.Contains("line"))
				{
					component.sizeDelta = new Vector2(component.sizeDelta.x, 30f);
				}
				if (text.Contains("bold"))
				{
					text3.fontStyle = FontStyle.Bold;
				}
				else if (text.Contains("italic"))
				{
					text3.fontStyle = FontStyle.Italic;
				}
				else if (text.Contains("italic") && text.Contains("bold"))
				{
					text3.fontStyle = FontStyle.BoldAndItalic;
				}
			}
			StartCoroutine(Reset());
			if (numberStr == "X")
			{
				GameObject.Find("GameManager").GetComponent<GameManager>().UnlockAchievements("root");
			}
		}

		private IEnumerator Reset()
		{
			for (int i = 0; i < contentSizeFitters.Count; i++)
			{
				contentSizeFitters[i].enabled = false;
			}
			yield return new WaitForEndOfFrame();
			for (int j = 0; j < contentSizeFitters.Count; j++)
			{
				contentSizeFitters[j].enabled = true;
			}
		}
	}
}
