using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavePanel : MonoBehaviour
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

	public Button btn_back;

	public Button btn_sure;

	public Button btn_delete;

	[SerializeField]
	private Text txt_sure;

	[SerializeField]
	private Button btn_newitem;

	public SaveItem currentsaveitem;

	public int type;

	[SerializeField]
	public Animator deleteWindow;

	private bool alertCanClick = true;

	public bool isnewitem;

	public bool isOver;

	public bool isOver3;

	private GameManager gameManager;

	private bool isShow;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btn_back.onClick.AddListener(delegate
		{
			gameManager.soundManager.PlaySound(16);
			BakBtn();
		});
		btn_newitem.onClick.AddListener(delegate
		{
			gameManager.soundManager.PlaySound(16);
			isnewitem = true;
		});
		btn_delete.onClick.AddListener(delegate
		{
			gameManager.soundManager.PlaySound(16);
			if (currentsaveitem != null && currentsaveitem.type == 0)
			{
				gameManager.soundManager.PlaySound(16);
				deleteWindow.gameObject.SetActive(value: true);
				StartCoroutine(StartDelete());
			}
		});
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
		GameObject obj = (GameObject)Object.Instantiate(Resources.Load("saveitem"), scrollRect.content);
		obj.GetComponent<SaveItem>().Init(pt, saveManager.gameManager.player.playerdata);
		obj.transform.SetSiblingIndex(1);
		obj.GetComponent<SaveItem>().ShowUploadSave(delegate
		{
			SelectedNewSaveItem(isSelected: true);
		});
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
				GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Caidan/caidan"), gameManager.homeScene.middle);
				obj.SetActive(value: true);
				obj.GetComponent<CanvasGroup>().DOFade(1f, 2f);
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
			else if (isOver && gameManager.homeScene.middle.Find("tobecontinue") == null)
			{
				Cursor.visible = true;
				gameManager.musicManager.Stop();
				gameManager.musicManager.GetComponent<AudioSource>().Stop();
				gameManager.soundManager.PlaySound(16);
				if (gameManager.saveManager.IsHasAutoSave())
				{
					Debug.Log("自动存档文件存在");
					isOver = false;
					type = 0;
					gameManager.istaohuashow = false;
					gameManager.iscancollect = true;
					gameManager.txt_studio.SetActive(value: false);
					SceneManager.LoadScene(gameManager.GetHomeSceneName());
				}
				else
				{
					Debug.Log("自动存档文件不存在");
				}
				isOver = false;
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
		btn_sure.onClick.RemoveAllListeners();
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
				((GameObject)Object.Instantiate(Resources.Load("saveitem"), scrollRect.content)).GetComponent<SaveItem>().Init("AutoSaveData.es3", saveManager.GetAutoSaveItem(), 1);
			}
			txt_sure.text = I18N.instance.getValue("^home08");
		}
		else
		{
			txt_title.text = I18N.instance.getValue("^home_btntsave");
			newitem.SetActive(value: true);
			txt_sure.text = I18N.instance.getValue("^home10");
		}
		List<PlayerData> allSaveList = saveManager.GetAllSaveList();
		List<string> allSavePathList = saveManager.GetAllSavePathList();
		for (int j = 0; j < allSaveList.Count; j++)
		{
			((GameObject)Object.Instantiate(Resources.Load("saveitem"), scrollRect.content)).GetComponent<SaveItem>().Init(allSavePathList[j], allSaveList[j]);
		}
		btn_sure.onClick.AddListener(delegate
		{
			if (isnewitem)
			{
				CreateNewSave();
			}
			else
			{
				Debug.Log(type + ":::::" + currentsaveitem);
				if (type == 0 && currentsaveitem != null)
				{
					btn_sure.interactable = false;
					btn_delete.interactable = false;
					btn_back.interactable = false;
					PlayerData playerData = saveManager.LoadData(currentsaveitem.path);
					if (playerData != null)
					{
						if (playerData.Eventid > 6 && !gameManager.isBuyDLC(playerData.Eventid))
						{
							gameManager.ValidDLC(playerData.Eventid);
							btn_sure.interactable = true;
							btn_delete.interactable = true;
							btn_back.interactable = true;
							return;
						}
						gameManager.IsDlc = playerData.Eventid == 7;
						playerData.isDLC = gameManager.IsDlc;
						saveManager.gameManager.player.playerdata = playerData;
						saveManager.SavePlayerData();
						currentsaveitem.ShowReadSave();
					}
				}
				else if (type == 1 && currentsaveitem != null)
				{
					btn_sure.interactable = false;
					btn_delete.interactable = false;
					btn_back.interactable = false;
					saveManager.SaveManualPlayerData(currentsaveitem.path);
					currentsaveitem.ShowUploadSave(delegate
					{
						SelectedNewSaveItem(isSelected: true);
					});
				}
				else if (type == 2 && currentsaveitem != null)
				{
					btn_sure.interactable = false;
					btn_delete.interactable = false;
					btn_back.interactable = false;
					saveManager.SaveManualPlayerData(currentsaveitem.path);
					currentsaveitem.ShowUploadSave(delegate
					{
						SelectedNewSaveItem(isSelected: true);
					});
				}
				else if (type == 3 && currentsaveitem != null)
				{
					btn_sure.interactable = false;
					btn_delete.interactable = false;
					btn_back.interactable = false;
					saveManager.SaveManualPlayerData(currentsaveitem.path);
					currentsaveitem.ShowUploadSave(delegate
					{
						SelectedNewSaveItem(isSelected: true);
					});
				}
			}
			gameManager.soundManager.PlaySound(16);
		});
		SelectedNewSaveItem(isSelected: true);
	}

	private void SelectedNewSaveItem(bool isSelected)
	{
		isnewitem = isSelected;
		newitem.GetComponent<Button>().Select();
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
