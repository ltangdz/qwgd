using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class PhoneNumDialog : CustomDialog
{
	public List<InputField> inputList;

	public Button btnFocus;

	public InvadePhoneDialog invadePhoneDialog;

	public Button btnSubmit;

	public GameObject warningTip;

	public string tiplabel;

	public Text tipObj;

	public Text txt_title;

	[SerializeField]
	private string pwkey;

	private int crtInput;

	private bool isSubmit;

	private string[] inputval = new string[4] { "-1", "-1", "-1", "-1" };

	public void Init(string pw, string tip, string titlekey)
	{
		if (titlekey.StartsWith("^"))
		{
			txt_title.text = I18N.instance.getValue(titlekey);
		}
		pwkey = pw;
		tiplabel = tip;
	}

	private void Start()
	{
		tipObj.GetComponent<I18NText>().updateTranslation2(tiplabel);
		btnFocus.onClick.AddListener(delegate
		{
			inputList[crtInput].ActivateInputField();
		});
		inputList[crtInput].ActivateInputField();
		for (int num = 0; num < inputList.Count; num++)
		{
			inputList[num].onValueChanged.AddListener(delegate
			{
				if (inputList[crtInput].text.Replace(" ", "") != "")
				{
					inputList[crtInput].DeactivateInputField();
					inputval[crtInput] = inputList[crtInput].text;
					if (crtInput < 3)
					{
						crtInput++;
						inputList[crtInput].ActivateInputField();
					}
				}
			});
		}
		btnSubmit.onClick.AddListener(Submit);
	}

	private void Submit()
	{
		string text = "";
		for (int i = 0; i < inputList.Count; i++)
		{
			text += inputList[i].text;
		}
		if (text == pwkey)
		{
			invadePhoneDialog.ShowUnlock();
			Object.Destroy(base.gameObject);
			return;
		}
		isSubmit = false;
		warningTip.SetActive(value: true);
		CancelInvoke("HideWarning");
		Invoke("HideWarning", 3f);
	}

	private void HideWarning()
	{
		warningTip.SetActive(value: false);
	}

	private void Update()
	{
		if ((Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return)) && !isSubmit)
		{
			isSubmit = true;
			Submit();
		}
		if (Input.GetKeyUp(KeyCode.Backspace) || Input.GetKeyUp(KeyCode.Delete))
		{
			if (crtInput == 3 && inputval[crtInput] != "-1")
			{
				inputList[crtInput].ActivateInputField();
				inputval[crtInput] = "-1";
			}
			else if (crtInput > 0 && inputval[crtInput] == "-1")
			{
				inputList[crtInput].DeactivateInputField();
				crtInput--;
				inputList[crtInput].ActivateInputField();
				inputList[crtInput].text = "";
				inputval[crtInput] = "-1";
			}
		}
	}

	public override void AfterShowSize()
	{
	}

	public override void BeforeShowSize()
	{
	}
}
