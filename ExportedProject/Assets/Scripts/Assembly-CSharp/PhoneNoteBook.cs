using Honeti;
using UnityEngine;

public class PhoneNoteBook : MonoBehaviour
{
	public GameObject content;

	public GameObject imgcontent;

	private GameManager gameManager;

	public void AddInfo(string fileID, GameManager gm)
	{
		gameManager = gm;
		Transform transform = Object.Instantiate(Resources.Load<Transform>("Link/phone_notebox"), content.transform);
		string time = gameManager.dataManager.dic35[fileID].time;
		transform.Find("time").GetComponent<I18NText>().updateTranslation2(time);
		string[] array = gameManager.dataManager.dic35[fileID].fileinfo.Split(';');
		gameManager.dataManager.dic35[fileID].highlight.Substring(1).Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			Object.Instantiate(Resources.Load<Transform>("Link/phone_noteinfo"), transform.transform).GetComponent<I18NText>().updateTranslation2(array[i]);
		}
	}
}
