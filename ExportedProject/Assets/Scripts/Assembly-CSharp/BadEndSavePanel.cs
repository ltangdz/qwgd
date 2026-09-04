using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BadEndSavePanel : MonoBehaviour
{
	[SerializeField]
	private Text txt_title;

	[SerializeField]
	private Text txt_path;

	[SerializeField]
	private SaveManager saveManager;

	[SerializeField]
	private DataManager dataManager;

	[SerializeField]
	private ScrollRect scrollRect;

	[SerializeField]
	private GameObject newitem;

	public SaveItem currentsaveitem;

	public AudioClip saveDel;

	public int type;

	[SerializeField]
	public Animator deleteWindow;

	private bool alertCanClick = true;

	public bool isnewitem;

	public bool isOver;

	public bool isOver3;

	private GameManager gameManager;

	private bool isShow;

	private List<GameObject> saveitemList = new List<GameObject>();

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		saveManager = gameManager.saveManager;
		dataManager = gameManager.dataManager;
		Init(0);
		StartCoroutine(DelSaveItemList());
	}

	private IEnumerator DelSaveItemList()
	{
		for (int i = 0; i < saveitemList.Count; i++)
		{
			saveitemList[i].GetComponent<BadEndSaveItem>().Del();
			gameManager.soundManager.GetComponent<AudioSource>().PlayOneShot(saveDel);
			yield return new WaitForSeconds(13f / (float)saveitemList.Count);
		}
	}

	private void OnDisable()
	{
		for (int i = 0; i < saveManager.playerdatapanel.transform.childCount; i++)
		{
			Object.Destroy(saveManager.playerdatapanel.transform.GetChild(i).gameObject);
		}
	}

	public void CreateNewSave()
	{
		string pt = saveManager.CreateNewSave(saveManager.gameManager.player.playerdata.nickname);
		GameObject obj = (GameObject)Object.Instantiate(Resources.Load("Dialog/badend/badendsaveitem"), scrollRect.content);
		obj.GetComponent<BadEndSaveItem>().Init(pt, saveManager.gameManager.player.playerdata);
		obj.transform.SetSiblingIndex(1);
		obj.GetComponent<BadEndSaveItem>().ShowUploadSave();
		Totop();
	}

	private IEnumerator StartDelete()
	{
		deleteWindow.Play("Exit Panel In");
		yield return new WaitForSeconds(1.2f);
		alertCanClick = true;
	}

	public void Cancle()
	{
		if (alertCanClick)
		{
			saveManager.gameManager.soundManager.PlaySound(16);
			alertCanClick = false;
			StartCoroutine(StopDelete());
		}
	}

	private IEnumerator StopDelete()
	{
		deleteWindow.Play("Exit Panel Out");
		yield return new WaitForSeconds(1.2f);
		alertCanClick = true;
	}

	public void DeleteItem()
	{
		saveManager.DeleteSave(currentsaveitem.path);
		Object.Destroy(currentsaveitem.gameObject);
		currentsaveitem = null;
		deleteWindow.Play("Exit Panel Out");
	}

	private void HideSetting()
	{
		base.gameObject.SetActive(value: false);
	}

	public void BakBtn()
	{
		if (!isShow)
		{
			return;
		}
		isShow = false;
		if (gameManager.homeScene != null)
		{
			if (type == 3)
			{
				Object.Instantiate(Resources.Load<GameObject>("Dialog/endPanel"), base.transform.parent);
				gameManager.soundManager.PlaySound(16);
				isOver = false;
				type = 0;
			}
			else if (isOver3 && gameManager.homeScene.middle.Find("end") == null)
			{
				if (I18N.instance.gameLang == LanguageCode.CN)
				{
					Object.Instantiate(Resources.Load<GameObject>("Dialog/EAendVideoCN"), gameManager.homeScene.middle).name = "end";
				}
				else if (I18N.instance.gameLang == LanguageCode.TC)
				{
					Object.Instantiate(Resources.Load<GameObject>("Dialog/EAendVideoTC"), gameManager.homeScene.middle).name = "end";
				}
				else if (I18N.instance.gameLang == LanguageCode.EN)
				{
					Object.Instantiate(Resources.Load<GameObject>("Dialog/EAendVideoEN"), gameManager.homeScene.middle).name = "end";
				}
				gameManager.soundManager.PlaySound(16);
				isOver3 = false;
			}
		}
		if (type == 2)
		{
			isOver = false;
			type = 0;
			gameManager.istaohuashow = false;
			gameManager.iscancollect = true;
			StartCoroutine(ChangeScene(gameManager.GetHomeSceneName()));
		}
		GetComponent<Animator>().SetBool("closeSetting", value: true);
		Invoke("HideSetting", 1f);
	}

	private IEnumerator ChangeScene(string sceneName)
	{
		yield return new WaitForSeconds(0.5f);
		gameManager.CanShowSetting(-1);
		SceneManager.LoadScene(sceneName);
	}

	public void Init(int tp)
	{
		type = tp;
		txt_path.text = ((type == 1) ? "/fileupload8" : "/filedownload8");
		for (int i = 0; i < scrollRect.content.childCount; i++)
		{
			if (i != 0)
			{
				Object.Destroy(scrollRect.content.GetChild(i).gameObject);
			}
		}
		if (type == 0)
		{
			txt_title.text = I18N.instance.getValue("^home_btnread");
			newitem.SetActive(value: false);
			if (saveManager.IsHasAutoSave() && saveManager.GetAutoSaveItem() != null)
			{
				GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("Dialog/badend/badendsaveitem"), scrollRect.content);
				gameObject.GetComponent<BadEndSaveItem>().Init("AutoSaveData.es3", saveManager.GetAutoSaveItem(), 1);
				saveitemList.Add(gameObject);
			}
		}
		else
		{
			txt_title.text = I18N.instance.getValue("^home_btntsave");
			newitem.SetActive(value: true);
		}
		List<PlayerData> allSaveList = saveManager.GetAllSaveList();
		List<string> allSavePathList = saveManager.GetAllSavePathList();
		for (int j = 0; j < allSaveList.Count; j++)
		{
			GameObject gameObject2 = (GameObject)Object.Instantiate(Resources.Load("Dialog/badend/badendsaveitem"), scrollRect.content);
			gameObject2.GetComponent<BadEndSaveItem>().Init(allSavePathList[j], allSaveList[j]);
			saveitemList.Add(gameObject2);
		}
	}

	public void Totop()
	{
		DOTween.To(() => scrollRect.content.localPosition, delegate(Vector3 x)
		{
			scrollRect.content.localPosition = x;
		}, Vector3.zero, 0.3f);
	}

	public void IsShow()
	{
		isShow = true;
	}
}
