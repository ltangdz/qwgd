using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class RunPassword : MonoBehaviour
{
	public Text[] password;

	public Text txtLoading;

	private int startIndex;

	private string[] passwordVal = new string[36]
	{
		"A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
		"K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
		"U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3",
		"4", "5", "6", "7", "8", "9"
	};

	private void Update()
	{
		for (int i = startIndex; i < password.Length; i++)
		{
			password[i].text = passwordVal[Random.Range(0, passwordVal.Length)];
		}
	}

	public void SetPassword(List<string> val)
	{
		StartCoroutine(StartSetPassword(val));
	}

	private IEnumerator StartSetPassword(List<string> val)
	{
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < password.Length; i++)
		{
			yield return new WaitForSeconds(0.3f);
			password[i].GetComponent<I18NText>().updateTranslation2(val[i]);
			startIndex = i + 1;
		}
		txtLoading.GetComponent<I18NText>().updateTranslation2("^invade01_label02");
	}
}
