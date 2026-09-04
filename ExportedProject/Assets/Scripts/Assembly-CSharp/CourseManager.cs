using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CourseManager : MonoBehaviour
{
	public CoursePanel00 coursepanel00;

	public CoursePanel01 coursepanel01;

	public CoursePanel02 coursepanel02;

	public CoursePanel03 coursepanel03;

	public CoursePanel04 coursepanel04;

	public CoursePanel05 coursepanel05;

	public CoursePanel06 coursepanel06;

	public CoursePanel07 coursepanel07;

	public CoursePanel08 coursepanel08;

	public CoursePanel09 coursepanel09;

	public CoursePanel10 coursepanel10;

	public CoursePanel11 coursepanel11;

	public CoursePanel12 coursepanel12;

	public CoursePanel13 coursepanel13;

	public CoursePanel14 coursepanel14;

	public CoursePanel15 coursepanel15;

	public CoursePanel16 coursepanel16;

	public GameManager gameManager;

	public GameObject[] coursetulis;

	public GameObject btn_browser;

	public GameObject btn_sql;

	public GameObject btn_weizhuang;

	public GameObject btn_email;

	public GameObject btn_pojie;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.player.playerdata.isCourse01 == 1 && gameManager.player.playerdata.isCourseOver == 0)
		{
			btn_pojie.GetComponent<ComputerButton>().enabled = true;
			btn_browser.transform.Find("img_search").GetComponent<Image>().color = Color.white;
			btn_browser.transform.Find("txt_content").GetComponent<Text>().color = Color.white;
		}
		if (gameManager.player.playerdata.isCourse05 == 1 && gameManager.player.playerdata.isCourseOver == 0)
		{
			btn_sql.GetComponent<ComputerButton>().enabled = true;
			btn_sql.transform.Find("img_sql").GetComponent<Image>().color = Color.white;
			btn_sql.transform.Find("txt_content").GetComponent<Text>().color = Color.white;
		}
		if (gameManager.player.playerdata.isCourse06 == 1 && gameManager.player.playerdata.isCourseOver == 0)
		{
			btn_pojie.GetComponent<ComputerButton>().enabled = true;
			btn_pojie.transform.Find("img_pojie").GetComponent<Image>().color = Color.white;
			btn_pojie.transform.Find("txt_content").GetComponent<Text>().color = Color.white;
		}
		if (gameManager.player.playerdata.isCourse07 == 1 && gameManager.player.playerdata.isCourseOver == 0)
		{
			btn_email.GetComponent<ComputerButton>().enabled = true;
			btn_email.transform.Find("img_email").GetComponent<Image>().color = Color.white;
			btn_email.transform.Find("txt_content").GetComponent<Text>().color = Color.white;
		}
		if (gameManager.player.playerdata.isCourse09 == 1 && gameManager.player.playerdata.isCourseOver == 0)
		{
			btn_weizhuang.GetComponent<ComputerButton>().enabled = true;
			btn_weizhuang.transform.Find("img_weizhuang").GetComponent<Image>().color = Color.white;
			btn_weizhuang.transform.Find("txt_content").GetComponent<Text>().color = Color.white;
		}
	}

	public void ShowTuli1()
	{
		if (gameManager.player.playerdata.isTuli01 != 1)
		{
			base.transform.SetAsLastSibling();
			if (coursetulis[0] != null)
			{
				coursetulis[0].SetActive(value: true);
				coursetulis[0].GetComponent<Course01>().Init();
			}
		}
	}

	public void ShowCourse(int index)
	{
		if (index == 1)
		{
			StartCoroutine(StartShowCourse(index, 4f));
		}
	}

	private IEnumerator StartShowCourse(int index, float time)
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		yield return new WaitForSeconds(time);
		switch (index)
		{
		case 1:
			coursepanel01.gameObject.SetActive(value: true);
			coursepanel01.ShowCourse();
			break;
		case 2:
			coursepanel02.gameObject.SetActive(value: true);
			coursepanel02.ShowCourse();
			break;
		case 3:
			coursepanel03.gameObject.SetActive(value: true);
			coursepanel03.ShowCourse();
			break;
		case 4:
			coursepanel04.gameObject.SetActive(value: true);
			coursepanel04.ShowCourse();
			break;
		case 5:
			coursepanel05.gameObject.SetActive(value: true);
			coursepanel05.ShowCourse();
			break;
		case 6:
			coursepanel06.gameObject.SetActive(value: true);
			coursepanel06.ShowCourse();
			break;
		case 7:
			coursepanel07.gameObject.SetActive(value: true);
			coursepanel07.ShowCourse();
			break;
		case 8:
			coursepanel08.gameObject.SetActive(value: true);
			coursepanel08.ShowCourse();
			break;
		case 9:
			coursepanel09.gameObject.SetActive(value: true);
			coursepanel09.ShowCourse();
			break;
		case 10:
			coursepanel10.gameObject.SetActive(value: true);
			coursepanel10.ShowCourse();
			break;
		case 11:
			coursepanel11.gameObject.SetActive(value: true);
			coursepanel11.ShowCourse();
			break;
		}
		yield return new WaitForSeconds(0.5f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	public void ShowTuli(int index, float time)
	{
		StartCoroutine(StartShowTuli(index, time));
	}

	private IEnumerator StartShowTuli(int index, float time)
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		yield return new WaitForSeconds(time);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		base.transform.SetAsLastSibling();
		if (index == 3)
		{
			if (coursetulis[2] != null)
			{
				coursetulis[2].SetActive(value: true);
				coursetulis[2].GetComponent<Course02>().Init();
			}
		}
		else if (coursetulis[index - 1] != null)
		{
			coursetulis[index - 1].SetActive(value: true);
			coursetulis[index - 1].GetComponent<Course01>().Init();
		}
	}

	public void ShowCourse0()
	{
		base.transform.SetAsLastSibling();
		coursepanel00.gameObject.SetActive(value: true);
		coursepanel00.ShowCourse();
	}

	public void ShowCourse1()
	{
		StartCoroutine(StartShowCourse1());
	}

	private IEnumerator StartShowCourse1()
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		yield return new WaitForSeconds(2f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		coursepanel01.gameObject.SetActive(value: true);
		coursepanel01.ShowCourse();
	}

	public void ShowCourse2()
	{
		StartCoroutine(StartShowCourse2());
	}

	private IEnumerator StartShowCourse2()
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		yield return new WaitForSeconds(2f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		coursepanel02.gameObject.SetActive(value: true);
		coursepanel02.ShowCourse();
	}

	public void ShowCourse3()
	{
		if (gameManager.player.playerdata.isCourse02 == 1)
		{
			StartCoroutine(StartShowCourse3());
		}
	}

	private IEnumerator StartShowCourse3()
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		yield return new WaitForSeconds(1f);
		coursepanel03.gameObject.SetActive(value: true);
		coursepanel03.ShowCourse();
		yield return new WaitForSeconds(1f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	public void ShowCourse4()
	{
		coursepanel04.gameObject.SetActive(value: true);
		coursepanel04.ShowCourse();
	}

	public void ShowCourse5()
	{
		StartCoroutine(StartShowCourse5());
	}

	private IEnumerator StartShowCourse5()
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		yield return new WaitForSeconds(4f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		coursepanel05.gameObject.SetActive(value: true);
		coursepanel05.ShowCourse();
	}

	public void ShowCourse6()
	{
		StartCoroutine(StartShowCourse6());
	}

	private IEnumerator StartShowCourse6()
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		yield return new WaitForSeconds(6f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		coursepanel06.gameObject.SetActive(value: true);
		coursepanel06.ShowCourse();
	}

	public void ShowCourse7()
	{
		StartCoroutine(StartShowCourse7());
	}

	private IEnumerator StartShowCourse7()
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		yield return new WaitForSeconds(6f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		coursepanel07.gameObject.SetActive(value: true);
		coursepanel07.ShowCourse();
	}

	public void ShowCourse8()
	{
		StartCoroutine(StartShowCourse8());
	}

	private IEnumerator StartShowCourse8()
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		yield return new WaitForSeconds(6f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		coursepanel08.gameObject.SetActive(value: true);
		coursepanel08.ShowCourse();
	}

	public void ShowCourse9()
	{
		StartCoroutine(StartShowCourse9());
	}

	private IEnumerator StartShowCourse9()
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		yield return new WaitForSeconds(4f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		coursepanel09.gameObject.SetActive(value: true);
		coursepanel09.ShowCourse();
	}

	public void ShowCourse10()
	{
		StartCoroutine(StartShowCourse10());
	}

	private IEnumerator StartShowCourse10()
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		yield return new WaitForSeconds(2f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		coursepanel10.gameObject.SetActive(value: true);
		coursepanel10.ShowCourse();
	}

	public void ShowCourse11()
	{
		StartCoroutine(StartShowCourse11());
	}

	private IEnumerator StartShowCourse11()
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		yield return new WaitForSeconds(4f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		coursepanel11.gameObject.SetActive(value: true);
		coursepanel11.ShowCourse();
	}

	public void ShowCourse12()
	{
		coursepanel12.gameObject.SetActive(value: true);
		coursepanel12.ShowCourse();
	}

	public void ShowCourse13()
	{
		coursepanel13.gameObject.SetActive(value: true);
		coursepanel13.ShowCourse();
	}

	public void ShowCourse14()
	{
		coursepanel14.gameObject.SetActive(value: true);
		coursepanel14.ShowCourse();
	}

	public void ShowCourse15()
	{
		coursepanel15.gameObject.SetActive(value: true);
		coursepanel15.ShowCourse();
	}

	public void ShowCourse16()
	{
		coursepanel16.gameObject.SetActive(value: true);
		coursepanel16.ShowCourse();
	}

	public void ShowTuli2()
	{
		if (gameManager.player.playerdata.isTuli02 != 1)
		{
			base.transform.SetAsLastSibling();
			if (coursetulis[1] != null)
			{
				coursetulis[1].SetActive(value: true);
				coursetulis[1].GetComponent<Course01>().Init();
			}
		}
	}

	public void ShowTuli3()
	{
		if (gameManager.player.playerdata.isTuli03 != 1)
		{
			gameManager.homeScene.eventsystem.SetActive(value: false);
			StartCoroutine(StartShowTuli3());
		}
	}

	private IEnumerator StartShowTuli3()
	{
		yield return new WaitForSeconds(4f);
		base.transform.SetAsLastSibling();
		if (coursetulis[2] != null)
		{
			coursetulis[2].SetActive(value: true);
			coursetulis[2].GetComponent<Course02>().Init();
		}
	}

	public void ShowTuli4()
	{
		if (gameManager.player.playerdata.isTuli04 != 1)
		{
			gameManager.homeScene.eventsystem.SetActive(value: false);
			StartCoroutine(StartShowTuli4());
		}
	}

	private IEnumerator StartShowTuli4()
	{
		yield return new WaitForSeconds(2f);
		base.transform.SetAsLastSibling();
		if (coursetulis[3] != null)
		{
			coursetulis[3].SetActive(value: true);
			coursetulis[3].GetComponent<Course01>().Init();
		}
	}

	public void ShowTuli5()
	{
		if (gameManager.player.playerdata.isTuli05 != 1)
		{
			gameManager.homeScene.eventsystem.SetActive(value: false);
			StartCoroutine(StartShowTuli5());
		}
	}

	private IEnumerator StartShowTuli5()
	{
		yield return new WaitForSeconds(2f);
		base.transform.SetAsLastSibling();
		if (coursetulis[4] != null)
		{
			coursetulis[4].SetActive(value: true);
			coursetulis[4].GetComponent<Course01>().Init();
		}
	}

	public void ShowTuli6()
	{
		if (gameManager.player.playerdata.isTuli06 != 1)
		{
			gameManager.homeScene.eventsystem.SetActive(value: false);
			StartCoroutine(StartShowTuli6());
		}
	}

	private IEnumerator StartShowTuli6()
	{
		yield return new WaitForSeconds(2f);
		base.transform.SetAsLastSibling();
		if (coursetulis[5] != null)
		{
			coursetulis[5].SetActive(value: true);
			coursetulis[5].GetComponent<Course01>().Init();
		}
	}

	public void ShowTuli7()
	{
		if (gameManager.player.playerdata.isTuli07 != 1)
		{
			gameManager.homeScene.eventsystem.SetActive(value: false);
			StartCoroutine(StartShowTuli7());
		}
	}

	private IEnumerator StartShowTuli7()
	{
		yield return new WaitForSeconds(2f);
		base.transform.SetAsLastSibling();
		if (coursetulis[6] != null)
		{
			coursetulis[6].SetActive(value: true);
			coursetulis[6].GetComponent<Course01>().Init();
		}
	}

	public void HideAll()
	{
		base.gameObject.SetActive(value: false);
	}

	private IEnumerator StartHide()
	{
		yield return new WaitForSeconds(10f);
		coursepanel00.gameObject.SetActive(value: false);
		coursepanel01.gameObject.SetActive(value: false);
		coursepanel02.gameObject.SetActive(value: false);
		coursepanel03.gameObject.SetActive(value: false);
		coursepanel04.gameObject.SetActive(value: false);
		coursepanel05.gameObject.SetActive(value: false);
		coursepanel06.gameObject.SetActive(value: false);
		coursepanel07.gameObject.SetActive(value: false);
		coursepanel08.gameObject.SetActive(value: false);
		coursepanel09.gameObject.SetActive(value: false);
		coursepanel10.gameObject.SetActive(value: false);
		coursepanel11.gameObject.SetActive(value: false);
		coursepanel12.gameObject.SetActive(value: false);
		coursepanel13.gameObject.SetActive(value: false);
		coursepanel14.gameObject.SetActive(value: false);
		coursepanel15.gameObject.SetActive(value: false);
		for (int i = 0; i < coursetulis.Length; i++)
		{
			coursetulis[i].SetActive(value: false);
		}
	}
}
