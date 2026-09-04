using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;
using Honeti;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.TitanWeb
{
	public class Sql3Dlc7 : MonoBehaviour
	{
		[Header("通用文案")]
		public Text contentText;

		[Header("成功或失败")]
		public Text resultText;

		[Header("成功请等待 失败内容")]
		public Text waitText;

		[Header("成功结果")]
		public Text successText;

		public Button closeButton;

		public Button lastButton;

		public Button homeButton;

		public Transform content;

		private List<List<string>> _matchingNames;

		private string _itemID = "";

		private bool _canClick;

		private GameObject _sqlObj;

		private GameManager _gameManager;

		public GameManager GameManager
		{
			get
			{
				if (_gameManager == null)
				{
					_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
				}
				return _gameManager;
			}
		}

		public List<List<string>> MatchingNames
		{
			get
			{
				if (_matchingNames == null)
				{
					_matchingNames = new List<List<string>>
					{
						new List<string>
						{
							"lucrezia borgia",
							"simon cavendish",
							Lucrezia(),
							"11395"
						},
						new List<string>
						{
							"lucrezia borgia",
							"vincent valen",
							I18N.instance.getValue("^110008_other_276")
						},
						new List<string>
						{
							"vincent valen",
							"shirley payten",
							I18N.instance.getValue("^110008_other_277")
						}
					};
				}
				return _matchingNames;
			}
		}

		private void Start()
		{
			content.DOScale(1f, 0.38f);
			closeButton.onClick.AddListener(Close);
			lastButton.onClick.AddListener(ShowLast);
			homeButton.onClick.AddListener(Home);
		}

		private void Hide()
		{
			content.DOScale(0f, 0.5f).OnComplete(delegate
			{
				Object.Destroy(base.gameObject);
			});
		}

		private void Home()
		{
			if (_canClick)
			{
				Object.Destroy(_sqlObj);
				GameManager.homeScene.computerButtonBox.OpenTool(9);
				Hide();
			}
		}

		private void ShowLast()
		{
			if (_canClick)
			{
				_sqlObj.SetActive(value: true);
				Hide();
			}
		}

		private void Close()
		{
			if (_canClick)
			{
				Object.Destroy(_sqlObj);
				Hide();
			}
		}

		private void Achievement(string source, string target)
		{
			if (string.IsNullOrEmpty(source) || source == target)
			{
				return;
			}
			source = source.Trim().ToLower();
			target = target.Trim().ToLower();
			string[] array = new string[5] { "Lucrezia Borgia", "Simon Cavendish", "Tom Blanco", "Vincent Valen Park", "Cloud Shawn" };
			if (!array.Contains(source) || !array.Contains(target))
			{
				return;
			}
			if (GameManager.player.playerdata.sqlCompareNames == null)
			{
				GameManager.player.playerdata.sqlCompareNames = new List<string>();
			}
			if (!GameManager.player.playerdata.sqlCompareNames.Contains(source) || !GameManager.player.playerdata.sqlCompareNames.Contains(target))
			{
				GameManager.player.playerdata.sqlCompareNames.Add($"{source}_{target}");
			}
			for (int i = 0; i < array.Length; i++)
			{
				string item = array[i];
				for (int j = 0; j < array.Length; j++)
				{
					if (i != j)
					{
						string item2 = array[j];
						if (!GameManager.player.playerdata.sqlCompareNames.Contains(item) || !GameManager.player.playerdata.sqlCompareNames.Contains(item2))
						{
							return;
						}
					}
				}
			}
		}

		public void Init(string sourceName, string targetName, Dictionary<string, string> data, GameObject obj)
		{
			Achievement(sourceName, targetName);
			_sqlObj = obj;
			bool flag = false;
			bool flag2 = false;
			Debug.Log(data.Keys.ToString());
			Debug.Log("sourceName:" + sourceName + "---targetName:" + targetName);
			if (data.ContainsKey(sourceName) && GameManager.player.playerdata.sqlFinishedNames.Contains(sourceName))
			{
				flag = true;
			}
			if (data.ContainsKey(targetName) && GameManager.player.playerdata.sqlFinishedNames.Contains(targetName))
			{
				flag2 = true;
			}
			Debug.Log("hasSource:" + flag + "--hasTarget:" + flag2);
			if (!flag || !flag2)
			{
				if (!flag && !flag2)
				{
					Fail("");
					return;
				}
				string text = (flag ? sourceName : targetName);
				string s = CommonInfo(text, text, data);
				Fail(s);
				return;
			}
			string s2 = CommonInfo(sourceName, targetName, data);
			if (sourceName == targetName)
			{
				Fail(s2);
				return;
			}
			string text2 = "";
			for (int i = 0; i < MatchingNames.Count; i++)
			{
				List<string> list = MatchingNames[i];
				if (list.Contains(sourceName.ToLower()) && list.Contains(targetName.ToLower()))
				{
					text2 = list[2];
					if (list.Count == 4)
					{
						_itemID = list[3];
					}
					break;
				}
			}
			if (string.IsNullOrEmpty(text2))
			{
				Fail(s2);
			}
			else
			{
				Success(s2, text2);
			}
		}

		private string BaseInfo(string dataStr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Dictionary<string, string>> list = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(dataStr);
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, string> dictionary = list[i];
				for (int j = 0; j < dictionary.Values.Count; j++)
				{
					stringBuilder.Append(dictionary.ElementAt(j).Value);
					stringBuilder.Append(" ");
				}
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}

		private string CommonInfo(string sourceName, string targetName, Dictionary<string, string> data)
		{
			if (sourceName == targetName)
			{
				return BaseInfo(data[sourceName]);
			}
			string dataStr = data[sourceName];
			string dataStr2 = data[targetName];
			return BaseInfo(dataStr) + BaseInfo(dataStr2);
		}

		private void Fail(string s)
		{
			Sequence sequence = DOTween.Sequence();
			resultText.color = new Color(83f / 85f, 0.3019608f, 0.3019608f);
			sequence.Append(contentText.DOText(s, 1.5f).OnComplete(delegate
			{
				resultText.gameObject.SetActive(value: true);
			}).SetEase(Ease.Linear));
			sequence.Append(resultText.DOText(I18N.instance.getValue("^110008_other_278"), 0.3f).OnComplete(delegate
			{
				waitText.gameObject.SetActive(value: true);
				_canClick = true;
			}).SetEase(Ease.Linear));
			sequence.Append(waitText.DOText(I18N.instance.getValue("^110008_other_279"), 0.5f)).SetEase(Ease.Linear).OnComplete(delegate
			{
				_canClick = true;
			});
			sequence.Play();
		}

		private void Success(string s, string success)
		{
			Sequence sequence = DOTween.Sequence();
			resultText.color = new Color(0.23529412f, 46f / 51f, 32f / 51f);
			sequence.Append(contentText.DOText(s, 1.5f).SetEase(Ease.Linear).OnComplete(delegate
			{
				resultText.gameObject.SetActive(value: true);
			}));
			sequence.Append(resultText.DOText(I18N.instance.getValue("^110008_other_280"), 0.3f).OnComplete(delegate
			{
				waitText.gameObject.SetActive(value: true);
			}).SetEase(Ease.Linear));
			sequence.Append(waitText.DOText(I18N.instance.getValue("^110008_other_281"), 0.3f)).OnComplete(delegate
			{
				successText.gameObject.SetActive(value: true);
			}).SetEase(Ease.Linear);
			sequence.Append(waitText.DOFade(1f, 0f).OnComplete(delegate
			{
				successText.gameObject.SetActive(value: true);
			}));
			sequence.AppendInterval(0.8f);
			sequence.Append(waitText.DOText(success, 0.3f)).SetEase(Ease.Linear).OnComplete(delegate
			{
				if (_itemID != "")
				{
					GameManager.homeScene.notebook.AddNewItem(_itemID);
				}
				_canClick = true;
			});
			sequence.Play();
		}

		private string Lucrezia()
		{
			return string.Format("{0}\n{1}\n{2}", I18N.instance.getValue("^110008_other_273"), I18N.instance.getValue("^110008_other_274"), I18N.instance.getValue("^110008_other_275"));
		}
	}
}
