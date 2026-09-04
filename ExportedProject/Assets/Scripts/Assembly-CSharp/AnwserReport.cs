using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class AnwserReport : MonoBehaviour
{
	public Transform listBox;

	public List<string> QAlist;

	public List<int> anwser;

	public bool isAdmin;

	public Transform choiceBox;

	public Transform choiceList;

	public GameObject alert;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		GetAnwser();
		SetAnwserInfo();
	}

	private void GetAnwser()
	{
		isAdmin = false;
		for (int i = 0; i < anwser.Count; i++)
		{
			if (gameManager.player.playerdata.cioAnwser.ContainsKey(QAlist[i].Split(':')[0]))
			{
				anwser[i] = gameManager.player.playerdata.cioAnwser[QAlist[i].Split(':')[0]];
			}
		}
	}

	private void SetAnwserInfo()
	{
		for (int i = 0; i < QAlist.Count; i++)
		{
			Transform transform = ((i == 0) ? listBox : Object.Instantiate(listBox, listBox.parent));
			string key = QAlist[i].Split(':')[0];
			string[] array = QAlist[i].Split(':')[1].Split(',');
			transform.Find("txt_num").GetComponent<I18NText>().updateTranslation2("(" + (i + 1) + ")");
			transform.Find("txt_title").GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^houtai134") + I18N.instance.getValue(key));
			transform.Find("txt_anwser").GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^houtai135") + "<color=#6587ab>" + I18N.instance.getValue(array[anwser[i]]) + "</color>");
			int s = i;
			Transform obj = transform;
			obj.GetComponent<Button>().onClick.AddListener(delegate
			{
				ShowAnwser(obj, s);
			});
		}
	}

	private void ShowAnwser(Transform newObj, int a)
	{
		if (choiceBox.gameObject.activeInHierarchy)
		{
			return;
		}
		choiceBox.gameObject.SetActive(value: true);
		choiceBox.SetAsLastSibling();
		for (int i = 0; i < choiceList.parent.childCount; i++)
		{
			if (i != 0 && i != 1)
			{
				Object.Destroy(choiceList.parent.GetChild(i).gameObject);
			}
		}
		string[] anwserInfo = QAlist[a].Split(':')[1].Split(',');
		for (int j = 0; j < anwserInfo.Length; j++)
		{
			Transform obj = ((j == 0) ? choiceList : Object.Instantiate(choiceList, choiceList.parent));
			obj.Find("Text").GetComponent<I18NText>().updateTranslation2(anwserInfo[j]);
			int s = j;
			obj.GetComponent<Button>().onClick.AddListener(delegate
			{
				ChangeAnwser(newObj, anwserInfo[s]);
			});
		}
		choiceBox.GetComponent<CanvasGroup>().DOKill();
		choiceBox.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
		choiceBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(626f, newObj.GetComponent<RectTransform>().anchoredPosition.y);
	}

	public void HideChoiceBox()
	{
		choiceBox.GetComponent<CanvasGroup>().DOKill();
		choiceBox.GetComponent<CanvasGroup>().DOFade(0f, 0.2f).OnComplete(delegate
		{
			choiceBox.gameObject.SetActive(value: false);
		});
	}

	private void ChangeAnwser(Transform newObj, string label)
	{
		Debug.Log(newObj.name + " " + label);
		if (!isAdmin)
		{
			alert.gameObject.SetActive(value: true);
			alert.GetComponent<Animator>().Play("Exit Panel In");
		}
		else
		{
			newObj.Find("txt_anwser").GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^houtai135") + "<color=#6587ab>" + I18N.instance.getValue(label) + "</color>");
			HideChoiceBox();
		}
	}

	public void HideAlert()
	{
		alert.GetComponent<Animator>().Play("Exit Panel Out");
		Invoke("HideAlertPanel", 1.3f);
		HideChoiceBox();
	}

	private void HideAlertPanel()
	{
		alert.gameObject.SetActive(value: false);
	}
}
