using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class DNAStep01 : MonoBehaviour
{
	[SerializeField]
	private GameObject step01;

	[SerializeField]
	private GameObject step02;

	[SerializeField]
	private GameObject runpanel;

	[SerializeField]
	private GameObject selecepanel;

	[SerializeField]
	private Button btn_start;

	[SerializeField]
	private InputField input_date;

	[SerializeField]
	private InputField input_hospital;

	[SerializeField]
	private Toggle toggle_blood;

	[SerializeField]
	private Toggle toggle_zhifa;

	[SerializeField]
	private Toggle toggle_shuangyanpi;

	private string[] search = new string[17]
	{
		"admin@wks05:~$ grep root etc/crypto", "grep: /etc/crypto: Permission Denied", "sudo -i", "admin@wks05:~$ grep root etc/crypto", "pico ablkcipher.c", "ssh-c0e9lnDXoUgw/", "systemd-private-person.info-sefMebG/name", "systemd-private-person.info-sefMebG/gender", "systemd-private-person.info-sefMebG/birth", "systemd-private-person.info-sefMebG/idnumber",
		"systemd-private-person.info-sefMebG/tel", "systemd-private-person.info-sefMebG/other", "Test-unix/", "tracker-extract-files.0/", "VirtualBox-Dropped-Files/", ".X11-unix/", ".XTM-unix/"
	};

	private bool isright = true;

	private void Start()
	{
		btn_start.onClick.AddListener(delegate
		{
			if (toggle_blood.isOn && toggle_zhifa.isOn && input_date.text.Trim().ToLower().Equals(I18N.instance.getValue("^livename29").ToLower()) && input_hospital.text.Trim().ToLower().Equals(I18N.instance.getValue("^livename58").ToLower()))
			{
				isright = true;
			}
			else
			{
				isright = false;
			}
			runpanel.gameObject.SetActive(value: true);
			selecepanel.gameObject.SetActive(value: false);
			for (int num = runpanel.transform.childCount - 1; num >= 0; num--)
			{
				Object.Destroy(runpanel.transform.GetChild(num));
			}
			StartCoroutine(RunCode());
		});
	}

	private IEnumerator RunCode()
	{
		for (int i = 0; i < search.Length; i++)
		{
			AddWriterText(search[i], isred: false);
			yield return new WaitForSeconds(0.3f);
		}
		if (isright)
		{
			step01.SetActive(value: false);
			step02.SetActive(value: true);
			yield break;
		}
		AddWriterText(I18N.instance.getValue("^surveillance14"), isred: true);
		yield return new WaitForSeconds(2f);
		for (int num = runpanel.transform.childCount - 1; num >= 0; num--)
		{
			Object.Destroy(runpanel.transform.GetChild(num).gameObject);
		}
		runpanel.gameObject.SetActive(value: false);
		selecepanel.gameObject.SetActive(value: true);
	}

	private void AddWriterText(string s, bool isred)
	{
		GameObject gameObject = Object.Instantiate(Resources.Load("txt_sqlwriter") as GameObject, runpanel.transform);
		gameObject.GetComponent<TypewriterEffect>().StartEffect(s);
		if (isred)
		{
			gameObject.GetComponent<Text>().fontSize = 20;
			gameObject.GetComponent<Text>().color = Color.red;
		}
	}
}
