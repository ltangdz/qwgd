using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class AddLoadingPoint : MonoBehaviour
{
	private string label;

	private void Start()
	{
		label = GetComponent<Text>().text;
		StartCoroutine(Loading());
	}

	private IEnumerator Loading()
	{
		int a = 0;
		string la = label;
		while (true)
		{
			if (a < 3)
			{
				a++;
				la += ".";
			}
			else
			{
				a = 0;
				la = label;
			}
			GetComponent<I18NText>().updateTranslation2(la);
			yield return new WaitForSeconds(0.3f);
		}
	}
}
