using System.Collections.Generic;
using UnityEngine;

public class YulunCourseManager : MonoBehaviour
{
	public List<YulunCourse> courseList;

	public List<YulunCoursePanel> coursePanelList;

	private GameManager gameManager;

	public void ShowCourse(int index, GameManager gm = null)
	{
		if (gm != null)
		{
			gameManager = gm;
		}
		if (index < courseList.Count)
		{
			courseList[index].gameObject.SetActive(value: true);
			courseList[index].Init();
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
	}

	public void ShowCoursePanel(int index, GameManager gm = null)
	{
		if (gm != null)
		{
			gameManager = gm;
		}
		if (index < coursePanelList.Count)
		{
			coursePanelList[index].gameObject.SetActive(value: true);
			if (index == 0)
			{
				coursePanelList[index].ShowCourse(0.2f);
			}
			else if (index == coursePanelList.Count - 1)
			{
				coursePanelList[index].ShowCourse(0f, lastPanel: true);
			}
			else
			{
				coursePanelList[index].ShowCourse();
			}
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
