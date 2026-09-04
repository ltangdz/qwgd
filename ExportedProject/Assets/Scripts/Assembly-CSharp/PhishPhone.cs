using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class PhishPhone : CustomDialog
{
	public Image phoneSignal;

	public Sprite[] signal;

	public Color[] titleColor;

	public Sprite[] closeSprite;

	public Text introPhone;

	public GameObject iconGroup;

	public GameObject warning;

	public GameObject titleInfoBox;

	[HideInInspector]
	public int sceneCount;

	private string[] collectIDList;

	private string userID;

	private void Start()
	{
		StartCoroutine(WardingMsg());
		btn_close.onClick.AddListener(IfSuccess);
	}

	private void SetVir()
	{
		GameObject alert = Object.Instantiate(Resources.Load<GameObject>("Dialog/ruqin_alert"), content);
		alert.GetComponent<LinkAlert>().Reset("^close_link", "^txt_yes", "^txt_no");
		alert.GetComponent<LinkAlert>().sure.onClick.AddListener(delegate
		{
			Object.Instantiate(Resources.Load<GameObject>("Link/virLink"), content);
			Object.Destroy(alert.gameObject);
		});
	}

	public void Init(string nameID, string[] appArray)
	{
		userID = nameID;
		string key = gameManager.dataManager.dic33[nameID].name;
		introPhone.GetComponent<I18NText>().updateTranslation6(I18N.instance.getValue(key) + I18N.instance.getValue("^w_phone"));
		SetIconList(appArray);
		collectIDList = gameManager.dataManager.dic33[nameID].collect.Substring(1).Split(';');
	}

	private void SetIconList(string[] appArray)
	{
		for (int i = 0; i < appArray.Length; i++)
		{
			int s = i;
			DATA35 data = gameManager.dataManager.dic35[appArray[s]];
			GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Link/icon"), iconGroup.transform);
			Sprite sprite = Resources.Load<Sprite>("Link/" + data.icon);
			obj.GetComponent<Image>().sprite = sprite;
			obj.GetComponent<Button>().onClick.AddListener(delegate
			{
				OpenApp(appArray[s], data.fileinfo.Substring(1).Split(';'));
			});
		}
	}

	private void OpenApp(string data35ID, string[] fileInfo)
	{
		DATA35 dATA = gameManager.dataManager.dic35[data35ID];
		GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Link/phone_appfunbox"), content);
		Debug.Log("data的type:" + dATA.ID + " " + dATA.filetype);
		switch (dATA.filetype)
		{
		case 5:
		{
			gameObject.GetComponent<AppFunBox>().Reset(this, "^call_record");
			GameObject gameObject4 = Object.Instantiate(Resources.Load<GameObject>("Link/phone_numlist"), gameObject.transform);
			for (int j = 0; j < fileInfo.Length; j++)
			{
				string avatar = gameManager.dataManager.dic35[fileInfo[j]].avatar;
				string key = gameManager.dataManager.dic35[fileInfo[j]].phone.Split('.')[0];
				string time = gameManager.dataManager.dic35[fileInfo[j]].time;
				Transform transform = Object.Instantiate(Resources.Load<Transform>("Link/num_list"), gameObject4.transform);
				if (avatar != "" && avatar != " ")
				{
					transform.Find("avatar").GetComponent<Image>().sprite = Resources.Load<Sprite>("Link/" + avatar);
				}
				transform.Find("num").GetComponent<I18NText>().updateTranslation2(key);
				transform.Find("time").GetComponent<I18NText>().updateTranslation2(time);
			}
			break;
		}
		case 3:
		{
			gameObject.GetComponent<AppFunBox>().Reset(this, "^img");
			GameObject gameObject5 = Object.Instantiate(Resources.Load<GameObject>("Link/img_listbox"), gameObject.transform);
			for (int k = 0; k < fileInfo.Length; k++)
			{
				string fileinfo = gameManager.dataManager.dic35[fileInfo[k]].fileinfo;
				Transform transform2 = Object.Instantiate(Resources.Load<Transform>("Link/img_info"), gameObject5.transform);
				Sprite imgSprite = Resources.Load<Sprite>("Link/" + fileinfo);
				transform2.Find("Image").GetComponent<Image>().sprite = imgSprite;
				transform2.Find("Image").GetComponent<Image>().SetNativeSize();
				int num = gameManager.ObjSizeType(transform2.Find("Image").gameObject);
				float num2 = transform2.Find("Image").GetComponent<RectTransform>().rect.width;
				float num3 = transform2.Find("Image").GetComponent<RectTransform>().rect.height;
				float x = ((num == 0) ? (num2 / num3 * 146f) : 146f);
				float y = ((num == 0) ? 146f : (146f / (num2 / num3)));
				transform2.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);
				transform2.GetComponent<Button>().onClick.AddListener(delegate
				{
					OpenImg(imgSprite);
				});
			}
			break;
		}
		case 6:
		{
			gameObject.GetComponent<AppFunBox>().Reset(this, "^file_name05");
			GameObject gameObject3 = Object.Instantiate(Resources.Load<GameObject>("Link/phone_notebook"), gameObject.transform);
			if (dATA.highlight != "" || dATA.highlight != "#0")
			{
				gameObject3.GetComponent<PhoneNoteBook>().imgcontent.gameObject.SetActive(value: true);
				gameObject3.GetComponent<PhoneNoteBook>().imgcontent.GetComponent<HighLightPic>().itemid = dATA.highlight.Substring(1);
			}
			else
			{
				gameObject3.GetComponent<PhoneNoteBook>().imgcontent.gameObject.SetActive(value: false);
			}
			for (int i = 0; i < fileInfo.Length; i++)
			{
				gameObject3.GetComponent<PhoneNoteBook>().AddInfo(fileInfo[i], gameManager);
			}
			break;
		}
		case 7:
		{
			Debug.Log("打开浏览器");
			gameObject.GetComponent<AppFunBox>().Reset(this, "^btntool09");
			GameObject gameObject2 = Object.Instantiate(Resources.Load<GameObject>("fishPhoneWeb"), gameObject.transform);
			string[] filelist = gameManager.dataManager.dic35[data35ID].fileinfo.Substring(1).Split(';');
			gameObject2.GetComponent<FishPhoneWeb>().Init(filelist, gameManager);
			break;
		}
		case 4:
			break;
		}
	}

	private void OpenImg(Sprite imgUrl)
	{
		GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Link/phone_appfunbox"), content);
		gameObject.GetComponent<AppFunBox>().Reset(this, "^img");
		float num = gameObject.GetComponent<RectTransform>().rect.width;
		float num2 = gameObject.GetComponent<RectTransform>().rect.height;
		Transform transform = Object.Instantiate(Resources.Load<Transform>("Link/img_listsearch"), gameObject.transform);
		transform.Find("Image").GetComponent<Image>().sprite = imgUrl;
		transform.Find("Image").GetComponent<Image>().SetNativeSize();
		int num3 = gameManager.ObjSizeType(transform.Find("Image").gameObject);
		float num4 = transform.Find("Image").GetComponent<RectTransform>().rect.width;
		float num5 = transform.Find("Image").GetComponent<RectTransform>().rect.height;
		float x = ((num3 == 0) ? (num - 20f) : (num4 / num5 * num2 - 20f));
		float y = ((num3 == 0) ? (num / (num4 / num5) - 20f) : (num2 - 20f));
		transform.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);
	}

	private IEnumerator WardingMsg()
	{
		yield return new WaitForSeconds(3f);
		warning.transform.Find("warning_bak/Text").GetComponent<I18NText>().updateTranslation2("^link_type02");
		yield return new WaitForSeconds(12f);
		warning.transform.Find("warning_bak/Text").GetComponent<I18NText>().updateTranslation2("^link_type03");
		StartCoroutine(Lighting(1f));
		yield return new WaitForSeconds(5f);
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
		yield return new WaitForSeconds(3f);
		StartCoroutine(CloseScene(alert));
	}

	private IEnumerator CloseScene(GameObject alert)
	{
		warning.SetActive(value: false);
		alert.SetActive(value: false);
		IfSuccess();
		yield return new WaitForSeconds(0.3f);
		Hide();
	}

	private void IfSuccess()
	{
		for (int i = 0; i < collectIDList.Length; i++)
		{
			if (!gameManager.player.playerdata.itemlist.Contains(collectIDList[i]))
			{
				gameManager.homeScene.StartVideoDialog("videoDialogtaskfailed", "invade");
				return;
			}
		}
		gameManager.player.playerdata.fishLink[gameManager.dataManager.dic33[userID].name] = 1;
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
