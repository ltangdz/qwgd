using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InvadeSearchPanel : MonoBehaviour
{
	public InputField input;

	public Button btnSure;

	public GameObject wrongLabel;

	private InvadeListBox parobj;

	private string[] files;

	public void Init(string[] sendfiles, InvadeListBox obj)
	{
		files = sendfiles;
		parobj = obj;
		btnSure.onClick.AddListener(Result);
	}

	private void Result()
	{
		string text = input.text;
		int num = -1;
		for (int i = 0; i < files.Length; i++)
		{
			if (files[i].Split(':')[0].Replace(" ", "").ToLower() == text.Replace(" ", "").ToLower())
			{
				num = i;
			}
		}
		if (num == -1)
		{
			StartCoroutine(Wrong());
			return;
		}
		parobj.ShowFileImg(files[num].Split(':')[1], files);
		Object.Destroy(base.gameObject);
	}

	private IEnumerator Wrong()
	{
		wrongLabel.SetActive(value: true);
		yield return new WaitForSeconds(2f);
		wrongLabel.SetActive(value: false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			Result();
		}
	}
}
