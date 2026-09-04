using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class SocialWorker : CustomDialog
{
	public GameObject systemLogin;

	public GameObject systemIntro;

	public GameObject systemIntroInfo;

	public GameObject socialBox;

	public GameObject socialRun;

	public InputField sqlInput;

	public ScrollRect scrollRect;

	public GameObject sqlList;

	private SQLManager sqlManager;

	private string[] route = new string[8] { "D:\\PERSON\\mysql-13.7.12\\BLD_INFO\\package.psc", "D:\\PERSON\\mysql-13.7.12\\BLD_INFO\\ALL_BUILD.psc", "D:\\PERSON\\mysql-13.7.12\\BLD_INFO\\test_service_sql_api\\test_x_sessions_init.vcxp", "D:\\PERSON\\mysql-13.7.12\\plugin\\test_service_sql_api\\test_x_sessions_init.cc<500>:warning", "D:\\PERSON\\mysql-13.7.12\\plugin\\test_service_sql_api\\test_x_sessiongs_init.vcxproj", "D:\\PERSON\\mysql-13.7.12\\PERSON_BIRTH\\sessions.init", "D:\\PERSON\\mysql-13.7.12\\PERSON_BIRTH\\session_count.init", "D:\\PERSON\\mysql-13.7.12\\PERSON_BIRTH\\sql_api.init" };

	private string[] search = new string[20]
	{
		"root@Person:/tmp# ls - la /tmp/", ".font-unix/", ".ICE-unix/", "mysql_hookandroot_lib.c", "mysql_hookandroot_lib.so", "ssh-c0e9lnDXoUgw/", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/name", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/gender", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/IDnumber", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/birth",
		"systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/email", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/tel", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/job", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/address", "systemd-private-e2d3d4b20e89345f3344ea92dca378c-person.service-z0iMbg/hobby", "Test-unix/", "tracker-extract-files.0/", "VirtualBox-Dropped-Files/", ".X11-unix/", ".XTM-unix/"
	};

	private string[] result = new string[2] { "^search_success", "^search_failed" };

	private bool haveResult;

	private List<string> sqlKey = new List<string> { "name", "gender", "IDnumber", "birth", "email", "tel", "job", "address", "hobby" };

	private string[] resulttables = new string[3] { "+--------------+------------------+--------------+--------------------------+----------------------------------------+---------------------------------+--------------+--------------+--------------+", "| name        | birth               | gender      | tel                            | address                                    | email                                 | ID number      | job      | hobby      |", "| name        | birth               | gender      | tel                            | address                                    | email                                | IDnumber| job            | hobby       |" };

	private List<string> dos = new List<string>();

	private string crtEventId;

	private int crtDos;

	public Color[] colors;

	private void Start()
	{
		ResetType();
		sqlManager = gameManager.sqlManager;
		Invoke("LoadComplete", 3f);
		if (crtEventId != gameManager.player.GetEventId())
		{
			dos.Clear();
			crtEventId = gameManager.player.GetEventId();
		}
	}

	private void LoadComplete()
	{
		if (!gameManager.player.playerdata.getSql)
		{
			systemIntro.SetActive(value: true);
			systemLogin.SetActive(value: false);
			systemIntro.transform.Find("create_btn").GetComponent<Button>().onClick.AddListener(ReadInfoIntro);
		}
		else
		{
			StartUsing();
		}
	}

	private void StartSearch()
	{
		if (!sqlInput.text.Trim().Equals(""))
		{
			gameManager.player.playerdata.UseSocialMethod(1);
			string text = sqlInput.text.Trim();
			dos.Add(text);
			crtDos = dos.Count;
			sqlInput.text = "";
			socialBox.SetActive(value: false);
			socialRun.SetActive(value: true);
			StartCoroutine(LoadImg());
			StartCoroutine(ChangeRoute());
			StartCoroutine(Search(text, text));
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
		for (int i = 0; i < search.Length; i++)
		{
			AddWriterText(search[i]);
			yield return new WaitForSeconds(0.5f);
			if (i >= search.Length - 1)
			{
				Result(inputVal, otherVal);
				haveResult = true;
			}
		}
	}

	private void Result(string inputVal, string otherVal)
	{
		List<string[]> list = new List<string[]>();
		if (FormatTrue(inputVal))
		{
			list = sqlManager.SelectWherePersonTable(inputVal, otherVal);
			Debug.Log(list.Count);
		}
		if (list.Count != 0)
		{
			AddNewResultText(resulttables[0], 0);
			AddNewResultText(resulttables[2], 1);
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
		Object.Instantiate(Resources.Load("sqlitem") as GameObject, sqlList.transform).GetComponent<SqlItem>().InitContent(results);
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

	public override void BeforeShowSize()
	{
	}

	private void ReadInfoIntro()
	{
		systemIntroInfo.SetActive(value: true);
		systemIntro.SetActive(value: false);
		systemIntroInfo.transform.Find("start_Btn").GetComponent<Button>().onClick.AddListener(StartUsing);
	}

	private void ResetType()
	{
		systemLogin.SetActive(value: true);
		systemIntroInfo.SetActive(value: false);
		systemIntro.SetActive(value: false);
		socialBox.SetActive(value: false);
		socialRun.SetActive(value: false);
	}

	private void StartUsing()
	{
		socialBox.SetActive(value: true);
		systemIntroInfo.SetActive(value: false);
		sqlInput.ActivateInputField();
		socialBox.transform.Find("sql_searchBox").GetComponent<Button>().onClick.RemoveAllListeners();
		socialBox.transform.Find("sql_searchBox").GetComponent<Button>().onClick.AddListener(StartSearch);
	}

	public override void AfterShowSize()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			base.transform.GetSiblingIndex();
			_ = base.transform.parent.childCount;
			StartSearch();
		}
		if (Input.GetKeyDown(KeyCode.Tab) && haveResult)
		{
			Research();
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) && crtDos > 0)
		{
			crtDos--;
			sqlInput.GetComponent<I18NText>().updateTranslation2(dos[crtDos]);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) && crtDos < dos.Count - 1)
		{
			crtDos++;
			sqlInput.GetComponent<I18NText>().updateTranslation2(dos[crtDos]);
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
		int num = inputVal.IndexOf("and");
		if (inputVal.IndexOf(";") > -1)
		{
			if (num > -1)
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
				int num2 = array[i].IndexOf("=");
				int num3 = array[i].IndexOf("'");
				if (num2 > -1 && num3 > -1)
				{
					string[] array2 = array[i].Split('=');
					flag = sqlKey.Contains(array2[0].ToString().Trim());
				}
				else
				{
					flag = false;
				}
			}
		}
		else
		{
			flag = false;
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
}
