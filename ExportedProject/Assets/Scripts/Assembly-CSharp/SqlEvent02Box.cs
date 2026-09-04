using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class SqlEvent02Box : MonoBehaviour
{
	public GameObject socialBox;

	public Button btnChangePanel;

	public Button btnSearch;

	public InputField input;

	public SqlDialog parObj;

	public GameObject event2Run;

	public ScrollRect scrollRect;

	public GameObject sqlList;

	private bool haveResult;

	private string[] result = new string[2] { "^search_success", "^search_failed" };

	private string[] route = new string[8] { "C:\\PERSON\\mysql-13.7.12\\BLD_INFO\\package.psc", "D:\\PERSON\\mysql-13.7.12\\BLD_INFO\\ALL_BUILD.psc", "E:\\PERSON\\mysql-13.7.12\\BLD_INFO\\test_service_sql_api\\test_x_sessions_init.vcxp", "D:\\PERSON\\mysql-13.7.12\\plugin\\test_service_sql_api\\test_x_sessions_init.cc<500>:warning", "F:\\PERSON\\mysql-13.7.12\\plugin\\test_service_sql_api\\test_x_sessiongs_init.vcxproj", "D:\\PERSON\\mysql-13.7.12\\PERSON_BIRTH\\sessions.init", "D:\\PERSON\\mysql-13.7.12\\PERSON_BIRTH\\session_count.init", "D:\\PERSON\\mysql-13.7.12\\PERSON_BIRTH\\sql_api.init" };

	private string[] search = new string[20]
	{
		"admin@wks05:~$ grep root etc/crypto", "grep: /etc/crypto: Permission Denied", "sudo -i", "admin@wks05:~$ grep root etc/crypto", "pico ablkcipher.c", "ssh-c0e9lnDXoUgw/", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/name", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/gender", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/IDnumber", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/birth",
		"systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/email", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/tel", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/job", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/address", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/Hitalk ID", "Test-unix/", "tracker-extract-files.0/", "VirtualBox-Dropped-Files/", ".X11-unix/", ".XTM-unix/"
	};

	private string[] resulttables = new string[2] { "+-----------------------------------+--------------+-------------------+-----------------------------------------+---------------------------------------+", "| name                                  | gender       |birth                 | idnumber                                    | tel                                            |" };

	private GameManager gameManager;

	public Color[] colors;

	private void Start()
	{
		btnChangePanel.onClick.AddListener(delegate
		{
			StartCoroutine(ChangeEvent02());
		});
		btnSearch.onClick.AddListener(StartSearch);
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private IEnumerator ChangeEvent02()
	{
		GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		base.gameObject.SetActive(value: false);
		socialBox.SetActive(value: true);
		socialBox.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
	}

	private void StartSearch()
	{
		if (!input.text.Trim().Equals(""))
		{
			gameManager.player.playerdata.UseSocialMethod(1);
			string text = input.text;
			input.text = "";
			base.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
			event2Run.SetActive(value: true);
			StartCoroutine(LoadImg());
			StartCoroutine(ChangeRoute());
			StartCoroutine(Search(text));
			ScrollBottom();
		}
	}

	private IEnumerator LoadImg()
	{
		float rotation = 0f;
		while (true)
		{
			rotation = ((!(rotation > 360f)) ? (rotation + -10f) : 0f);
			event2Run.transform.Find("loading_img").GetComponent<RectTransform>().localRotation = Quaternion.Euler(0f, 0f, rotation);
			yield return new WaitForSeconds(0.02f);
		}
	}

	private IEnumerator ChangeRoute()
	{
		int a = 0;
		while (!haveResult)
		{
			event2Run.transform.Find("search_label").GetComponent<I18NText>().updateTranslation2(route[a]);
			a++;
			if (a > route.Length - 1)
			{
				a = 0;
			}
			yield return new WaitForSeconds(0.06f);
		}
	}

	private IEnumerator Search(string inputVal)
	{
		List<string[]> resultList = Result(inputVal);
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

	private void ScrollBottom()
	{
		Canvas.ForceUpdateCanvases();
		scrollRect.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
	}

	private List<string[]> Result(string inputVal)
	{
		new List<string[]>();
		return gameManager.sqlManager.SelectWherePersonBoatInfo(inputVal);
	}

	private void AddWriterText(string s)
	{
		Object.Instantiate(Resources.Load("txt_sqlwriter") as GameObject, sqlList.transform).GetComponent<TypewriterEffect>().StartEffect(s);
		ScrollBottom();
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
				ScrollBottom();
			}
			event2Run.transform.Find("search_label").GetComponent<I18NText>().updateTranslation2(result[0]);
		}
		else
		{
			event2Run.transform.Find("search_label").GetComponent<I18NText>().updateTranslation2(result[1]);
			AddWriterText("Failed");
		}
		event2Run.transform.Find("loading_img").gameObject.SetActive(value: false);
	}

	private void AddNewResultText(string s, int c)
	{
		GameObject obj = Object.Instantiate(Resources.Load("txt_sql") as GameObject, sqlList.transform);
		obj.GetComponent<I18NText>().updateTranslation2(s);
		obj.GetComponent<Text>().color = colors[c];
		ScrollBottom();
	}

	private void AddSqlItem(string[] results)
	{
		GameObject gameObject = Object.Instantiate(Resources.Load("sqlitemShip") as GameObject, sqlList.transform);
		gameObject.GetComponent<SqlItemShip>().InitContent(results);
		if (gameManager.player.playerdata.isCourse12 == 0)
		{
			gameManager.homeScene.courseManager.coursepanel12.sqlitem = gameObject;
			gameManager.homeScene.courseManager.ShowCourse12();
		}
		ScrollBottom();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			float num = parObj.transform.GetSiblingIndex();
			float num2 = parObj.transform.parent.childCount;
			Debug.Log((num == num2 - 1f) + " " + base.gameObject.activeInHierarchy + " " + (base.gameObject.GetComponent<CanvasGroup>().alpha == 1f));
			if (num == num2 - 1f && base.gameObject.activeInHierarchy && base.gameObject.GetComponent<CanvasGroup>().alpha == 1f)
			{
				Debug.Log("event02");
				StartSearch();
			}
		}
		if (Input.GetKeyDown(KeyCode.Tab) && haveResult && event2Run.activeInHierarchy)
		{
			Research();
		}
	}

	private void Research()
	{
		event2Run.transform.Find("loading_img").gameObject.SetActive(value: true);
		event2Run.transform.Find("search_label").GetComponent<Text>().text = "";
		for (int i = 0; i < sqlList.transform.childCount; i++)
		{
			Object.Destroy(sqlList.transform.GetChild(i).gameObject);
		}
		event2Run.SetActive(value: false);
		base.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
		haveResult = false;
		input.ActivateInputField();
	}
}
