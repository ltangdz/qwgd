using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SqlDialogDLC : SqlDialog, IPointerClickHandler, IEventSystemHandler
{
	public override IEnumerator StartMap()
	{
		yield return new WaitForSeconds(1.25f);
		if (crtEventId != gameManager.player.GetEventId())
		{
			dos.Clear();
			crtEventId = gameManager.player.GetEventId();
		}
		map.SetActive(value: false);
		Show();
	}

	public override void Show()
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		rectTransform.DOSizeDelta(new Vector2(width, height), 0f).OnComplete(delegate
		{
			img_drag.enabled = true;
			content.gameObject.SetActive(value: true);
			LoadComplete();
			gameManager.homeScene.eventsystem.SetActive(value: true);
		});
	}

	private void LoadComplete()
	{
		if (!gameManager.player.playerdata.getSql)
		{
			systemLogin.SetActive(value: false);
			ReadInfoIntro();
		}
		else
		{
			StartUsing();
		}
	}

	private void StartSearch()
	{
		if (!sqlInput.text.Trim().Equals("") && !sqlInput2.text.Trim().Equals(""))
		{
			gameManager.player.playerdata.UseSocialMethod(1);
			string text = sqlInput.text.ToLower().Trim();
			string otherVal = sqlInput2.text.ToLower().Trim();
			dos.Add(text);
			crtDos = dos.Count;
			sqlInput.text = "";
			sqlInput2.text = "";
			socialBox.SetActive(value: false);
			socialRun.SetActive(value: true);
			StartCoroutine(LoadImg());
			StartCoroutine(ChangeRoute());
			StartCoroutine(Search(text, otherVal));
			ScrollBottom();
		}
	}

	private IEnumerator LoadImg()
	{
		float rotation = 0f;
		while (true)
		{
			rotation = ((!(rotation > 360f)) ? (rotation + -10f) : 0f);
			socialRun.transform.Find("loading_img").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, rotation);
			yield return new WaitForSeconds(0.02f);
		}
	}

	private IEnumerator ChangeRoute()
	{
		int a = 0;
		while (!haveResult)
		{
			socialRun.transform.Find("search_label").GetComponent<I18NText>().updateTranslation2(route[a]);
			a++;
			if (a > route.Length - 1)
			{
				a = 0;
			}
			yield return new WaitForSeconds(0.06f);
		}
	}

	private IEnumerator Search(string inputVal, string otherVal)
	{
		List<string[]> resultList = Result(inputVal, otherVal);
		float listLength = ((resultList.Count == 0) ? 6f : ((float)search.Length));
		for (int i = 0; (float)i < listLength; i++)
		{
			AddWriterText(search[i]);
			yield return new WaitForSeconds(0.3f);
			if ((float)i >= listLength - 1f)
			{
				SetSearchVal(resultList);
				haveResult = true;
			}
		}
	}

	private List<string[]> Result(string inputVal, string otherVal)
	{
		new List<string[]>();
		return gameManager.sqlManager.SelectWherePersonTable(inputVal, otherVal);
	}

	private void SetSearchVal(List<string[]> list)
	{
		if (list.Count != 0)
		{
			AddNewResultText(resulttables[0], 0);
			AddNewResultText(resulttables[1], 1);
			AddNewResultText(resulttables[0], 0);
			for (int i = 0; i < list.Count; i++)
			{
				AddSqlItem(list[i]);
			}
			socialRun.transform.Find("search_label").GetComponent<I18NText>().updateTranslation2(result[0]);
		}
		else
		{
			socialRun.transform.Find("search_label").GetComponent<I18NText>().updateTranslation2(result[1]);
			AddWriterText("Failed");
		}
		socialRun.transform.Find("loading_img").gameObject.SetActive(value: false);
	}

	private void AddWriterText(string s)
	{
		Object.Instantiate(Resources.Load("txt_sqlwriter") as GameObject, sqlList.transform).GetComponent<TypewriterEffect>().StartEffect(s);
		ScrollBottom();
	}

	private void AddSqlItem(string[] results)
	{
		GameObject gameObject = Object.Instantiate(Resources.Load("sqlitem") as GameObject, sqlList.transform);
		gameObject.GetComponent<SqlItem>().InitContent(results);
		if (gameManager.player.playerdata.isCourse12 == 0)
		{
			gameManager.homeScene.courseManager.coursepanel12.sqlitem = gameObject;
			gameManager.homeScene.courseManager.ShowCourse12();
		}
		ScrollBottom();
	}

	private void Research()
	{
		socialRun.transform.Find("loading_img").gameObject.SetActive(value: true);
		socialRun.transform.Find("search_label").GetComponent<Text>().text = "";
		for (int i = 0; i < sqlList.transform.childCount; i++)
		{
			Object.Destroy(sqlList.transform.GetChild(i).gameObject);
		}
		socialRun.SetActive(value: false);
		socialBox.SetActive(value: true);
		haveResult = false;
		sqlInput.ActivateInputField();
	}

	private void ReadInfoIntro()
	{
		btn_close.gameObject.SetActive(value: true);
		systemIntroInfo.SetActive(value: true);
		systemIntro.SetActive(value: false);
		systemIntroInfo.transform.Find("start_Btn").GetComponent<Button>().onClick.AddListener(StartUsing);
	}

	protected override void ResetType()
	{
		systemLogin.SetActive(value: false);
		systemIntroInfo.SetActive(value: false);
		systemIntro.SetActive(value: false);
		socialBox.SetActive(value: false);
		socialRun.SetActive(value: false);
		img_drag.enabled = false;
	}

	private void StartUsing()
	{
		socialBox.SetActive(value: true);
		systemIntroInfo.SetActive(value: false);
		sqlInput.ActivateInputField();
		socialBox.transform.Find("sql_searchBox").GetComponent<Button>().onClick.RemoveAllListeners();
		socialBox.transform.Find("sql_searchBox").GetComponent<Button>().onClick.AddListener(StartSearch);
		if (gameManager.player.playerdata.isCourse06 == 0)
		{
			gameManager.homeScene.courseManager.ShowTuli4();
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			float num = base.transform.GetSiblingIndex();
			float num2 = base.transform.parent.childCount;
			if (num == num2 - 1f && socialBox.activeInHierarchy)
			{
				Debug.Log("event01");
				StartSearch();
			}
		}
		if (Input.GetKeyDown(KeyCode.Tab) && (!(gameManager.homeScene.courseManager.coursepanel12 != null) || !gameManager.homeScene.courseManager.coursepanel12.gameObject.activeSelf) && haveResult && socialRun.activeInHierarchy)
		{
			Research();
		}
	}

	private void ScrollBottom()
	{
		Canvas.ForceUpdateCanvases();
		scrollRect.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
	}

	private bool FormatTrue(string inputVal)
	{
		bool flag = true;
		string[] array = new string[0];
		if (inputVal.IndexOf("and") > -1)
		{
			inputVal = inputVal.Replace(";", "").Replace("and", "@");
			array = inputVal.Split('@');
		}
		else
		{
			array = new string[1] { inputVal.Replace(";", "") };
		}
		for (int i = 0; i < array.Length; i++)
		{
			int num = array[i].IndexOf("=");
			int num2 = array[i].IndexOf("'");
			if (num > -1 && num2 > -1)
			{
				string[] array2 = array[i].Split('=');
				flag = sqlKey.Contains(array2[0].ToString().Trim());
			}
			else
			{
				flag = false;
			}
		}
		return flag;
	}

	private void AddNewResultText(string s, int c)
	{
		GameObject obj = Object.Instantiate(Resources.Load("txt_sql") as GameObject, sqlList.transform);
		obj.GetComponent<I18NText>().updateTranslation2(s);
		obj.GetComponent<Text>().color = colors[c];
		ScrollBottom();
	}

	public new void SetFront()
	{
		base.transform.SetAsLastSibling();
	}

	private IEnumerator ChangeEvent02()
	{
		socialBox.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		socialBox.SetActive(value: false);
		sqlEvent02Box.SetActive(value: true);
		sqlEvent02Box.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
	}

	public new void OnPointerClick(PointerEventData eventData)
	{
		base.transform.SetAsLastSibling();
	}
}
