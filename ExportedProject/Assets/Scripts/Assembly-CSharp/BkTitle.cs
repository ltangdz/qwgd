using System;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class BkTitle : MonoBehaviour
{
	public Text txtCrim;

	public Text txtTime;

	private string txtCrimVal;

	private void Start()
	{
		txtCrimVal = "100";
		string key = "^text_crim";
		txtCrim.GetComponent<I18NText>().updateTranslation3(key, txtCrimVal);
	}

	private void Update()
	{
		txtTime.GetComponent<I18NText>().updateTranslation2(DateTime.Now.ToString("G"));
	}

	public void SystemBtnClick()
	{
	}
}
