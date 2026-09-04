using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class PishCmp : CustomDialog
{
	public Text wCmp;

	public Button bakBtn;

	public GameObject warning;

	private string userID;

	private List<GameObject> fileBoxList = new List<GameObject>();

	private void Start()
	{
		StartCoroutine(WardingMsg());
	}

	public void Init(string nameID, string[] fileArray)
	{
		userID = nameID;
		string key = gameManager.dataManager.dic33[nameID].name;
		wCmp.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue(key) + I18N.instance.getValue("^w_cmp"));
		SetFileInfo(fileArray);
	}

	private void SetFileInfo(string[] fileArray)
	{
		GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Link/file_box"), content);
		fileBoxList.Add(gameObject);
		if (fileBoxList.Count > 1)
		{
			bakBtn.gameObject.SetActive(value: true);
			bakBtn.onClick.RemoveAllListeners();
			bakBtn.onClick.AddListener(BakFile);
		}
		else
		{
			bakBtn.gameObject.SetActive(value: false);
		}
		for (int i = 0; i < fileArray.Length; i++)
		{
			int filetype = gameManager.dataManager.dic35[fileArray[i]].filetype;
			DATA35 fileData = gameManager.dataManager.dic35[fileArray[i]];
			switch (filetype)
			{
			case 0:
			{
				GameObject obj2 = Object.Instantiate(Resources.Load<GameObject>("Link/file_list"), gameObject.transform);
				obj2.transform.Find("Text").GetComponent<I18NText>().updateTranslation2(fileData.filename);
				obj2.GetComponent<Button>().onClick.AddListener(delegate
				{
					OpenFile(fileData);
				});
				break;
			}
			case 2:
			{
				GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Link/word_list"), gameObject.transform);
				obj.transform.Find("Text").GetComponent<I18NText>().updateTranslation2(fileData.filename);
				obj.GetComponent<Button>().onClick.AddListener(delegate
				{
					OpenWord(fileData);
				});
				break;
			}
			case 3:
			{
				GameObject gameObject2 = Object.Instantiate(Resources.Load<GameObject>("Link/img_list"), gameObject.transform);
				gameObject2.transform.Find("Text").GetComponent<I18NText>().updateTranslation2(fileData.filename);
				GameObject gameObject3 = Object.Instantiate(Resources.Load<GameObject>("Link/" + fileData.fileinfo), gameObject2.transform.Find("outline"));
				int num = gameManager.ObjSizeType(gameObject3);
				float num2 = gameObject3.GetComponent<RectTransform>().rect.width;
				float num3 = gameObject3.GetComponent<RectTransform>().rect.height;
				gameObject3.GetComponent<RectTransform>().sizeDelta = ((num == 0) ? new Vector2(84f, num3 / (num2 / 84f)) : new Vector2(num2 / (num3 / 56f), 56f));
				gameObject2.GetComponent<Button>().onClick.AddListener(delegate
				{
					OpenImg(fileData);
				});
				break;
			}
			}
		}
	}

	private void OpenFile(DATA35 fileData)
	{
		string fileinfo = fileData.fileinfo;
		string[] fileInfo = new string[0];
		if (fileinfo != "" && fileinfo != " ")
		{
			fileInfo = fileinfo.Substring(1).Split(';');
			SetFileInfo(fileInfo);
		}
		else
		{
			SetFileInfo(fileInfo);
		}
	}

	private void OpenWord(DATA35 fileData)
	{
	}

	private void OpenImg(DATA35 fileData)
	{
		GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Dialog/pic"), content);
		Object.Instantiate(Resources.Load<GameObject>("Link/" + fileData.fileinfo), gameObject.GetComponent<EnlargeImg>().group.transform);
	}

	private void BakFile()
	{
		Object.Destroy(fileBoxList[fileBoxList.Count - 1]);
		fileBoxList.Remove(fileBoxList[fileBoxList.Count - 1]);
		if (fileBoxList.Count <= 1)
		{
			bakBtn.gameObject.SetActive(value: false);
		}
	}

	private IEnumerator WardingMsg()
	{
		yield return new WaitForSeconds(2f);
		warning.transform.Find("warning_bak/Text").GetComponent<I18NText>().updateTranslation2("^link_type02");
		yield return new WaitForSeconds(6f);
		warning.transform.Find("warning_bak/Text").GetComponent<I18NText>().updateTranslation2("^link_type03");
		StartCoroutine(Lighting(1f));
		yield return new WaitForSeconds(2f);
		StopCoroutine(Lighting(1f));
		StartCoroutine(Lighting(0.3f));
		warning.transform.Find("warning_bak/Text").GetComponent<I18NText>().updateTranslation2("^link_type04");
		yield return new WaitForSeconds(2f);
		GameObject alert = Object.Instantiate(Resources.Load<GameObject>("Dialog/ruqin_alert"), content);
		alert.GetComponent<LinkAlert>().Reset("^close_link", "^sure");
		alert.GetComponent<LinkAlert>().sure.onClick.AddListener(delegate
		{
			StartCoroutine(CloseScene(alert));
		});
	}

	private IEnumerator CloseScene(GameObject alert)
	{
		warning.SetActive(value: false);
		alert.SetActive(value: false);
		yield return new WaitForSeconds(0.3f);
		Hide();
	}

	private IEnumerator Lighting(float t)
	{
		while (true)
		{
			warning.GetComponent<CanvasGroup>().DOFade(0.5f, t);
			yield return new WaitForSeconds(t);
			warning.GetComponent<CanvasGroup>().DOFade(1f, t);
			yield return new WaitForSeconds(t);
		}
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
