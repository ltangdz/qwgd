using UnityEngine;

public class TaskOverVideoBox : MonoBehaviour
{
	public TaskOver taskOver;

	private void Start()
	{
	}

	public void ShowButton()
	{
		taskOver.ShowButton();
	}

	public void HideButton()
	{
		taskOver.HideButton();
	}
}
