using System.Collections.Generic;
using DG.Tweening;
using DLC7.Titan;
using Honeti;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using _DLC8;

public class ZhadanDialog : MonoBehaviour
{
	public List<ZhadanGroup> objHaveDoor;

	public float mapNum;

	public bool isGameOver;

	public Text imgTip;

	public Button playAgain;

	public Button getOut;

	public GameObject overPanel;

	public GameObject hand;

	public GameObject easyGame;

	public bool isDlc8;

	private bool isLoading;

	private GameManager gameManager;

	private bool isRestart;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		playAgain.onClick.AddListener(delegate
		{
			if (!isGameOver)
			{
				Restart();
			}
		});
	}

	public void SetRestartButton(Button button, UnityAction callback)
	{
		playAgain = button;
		playAgain.onClick.AddListener(delegate
		{
			if (!isGameOver)
			{
				Restart();
				callback?.Invoke();
			}
		});
	}

	public void ReStartGame()
	{
		if (isRestart)
		{
			return;
		}
		isRestart = true;
		if (isDlc8)
		{
			ResetDLc8();
			return;
		}
		if (base.name == "ZhadanGameGroup1" || base.name == "ZhadanGameGroup2")
		{
			ResetDLc();
			return;
		}
		if (easyGame != null)
		{
			gameManager.homeScene.zhadanInvade.failedTime++;
			if (gameManager.homeScene.zhadanInvade.failedTime == 5)
			{
				Object.Instantiate(Resources.Load<GameObject>("zhadan/zhadanConfirm"), base.transform.parent).GetComponent<ZhadanConfirm>().Init(this);
				return;
			}
		}
		Restart();
	}

	private void ResetDLc8()
	{
		Object.Instantiate(Resources.Load<GameObject>(string.Format("_DLC8/Virus/{0}", base.name.Replace("(Clone)", ""))), base.transform.parent);
		Object.Destroy(base.gameObject);
	}

	private void ResetDLc()
	{
		Object.Instantiate(Resources.Load<GameObject>(string.Format("_DLC7/prefabs/{0}", base.name.Replace("(Clone)", ""))), base.transform.parent);
		Object.Destroy(base.gameObject);
	}

	public void Restart(int obj = 0)
	{
		Debug.Log("加载");
		if (isLoading)
		{
			return;
		}
		isLoading = true;
		Debug.Log(base.name);
		if (isDlc8)
		{
			ResetDLc8();
			return;
		}
		if (base.name == "ZhadanGameGroup1" || base.name == "ZhadanGameGroup2(Clone)")
		{
			ResetDLc();
			return;
		}
		Debug.Log("加载2");
		if (gameManager.homeScene.zhadanInvade != null)
		{
			Image white = gameManager.homeScene.zhadanInvade.white;
			white.gameObject.SetActive(value: true);
			white.GetComponent<RectTransform>().DOScale(Vector3.one, 1f).OnComplete(delegate
			{
				string text2 = base.gameObject.name.Replace("(Clone)", "");
				base.gameObject.SetActive(value: false);
				Transform transform2 = null;
				if (obj != 0)
				{
					Debug.Log("加载1");
					transform2 = Object.Instantiate(easyGame.transform, base.transform.parent);
				}
				else
				{
					transform2 = Object.Instantiate(Resources.Load<Transform>("zhadan/" + text2), base.transform.parent);
				}
				transform2.SetSiblingIndex(0);
				white.DOFade(0f, 0.6f).OnComplete(delegate
				{
					white.GetComponent<RectTransform>().localScale = Vector3.zero;
					white.color = new Color(1f, 1f, 1f, 1f);
					white.gameObject.SetActive(value: false);
					Object.Destroy(base.gameObject);
				});
			});
		}
		else
		{
			string text = base.gameObject.name.Replace("(Clone)", "");
			base.gameObject.SetActive(value: false);
			Transform transform = null;
			if (obj != 0)
			{
				Debug.Log("加载1");
				transform = Object.Instantiate(easyGame.transform, base.transform.parent);
			}
			else
			{
				transform = Object.Instantiate(Resources.Load<Transform>("zhadan/" + text), base.transform.parent);
			}
			transform.SetSiblingIndex(0);
			Object.Destroy(base.gameObject);
		}
	}

	public void ChangeDoor()
	{
		for (int i = 0; i < objHaveDoor.Count; i++)
		{
			objHaveDoor[i].ChangeType();
		}
		if (hand != null)
		{
			Object.Destroy(hand);
		}
	}

	public void LoadNext()
	{
		Object.Instantiate(Resources.Load<GameObject>("_DLC7/prefabs/ZhadanGameGroup2"), base.transform.parent);
		Object.Destroy(base.gameObject);
	}

	public void MoveEnd()
	{
		if ((mapNum -= 1f) != 0f)
		{
			return;
		}
		if (isDlc8)
		{
			DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.FINISH_GAMME, 0);
			Debug.LogError("dlc8成功结束了");
			return;
		}
		if (base.name == "ZhadanGameGroup1" || base.name == "ZhadanGameGroup2(Clone)")
		{
			isGameOver = true;
			if (base.name == "ZhadanGameGroup1")
			{
				Invoke("LoadNext", 1.5f);
			}
			if (base.name == "ZhadanGameGroup2(Clone)")
			{
				GetComponentInParent<TitanVirusDialog>().ShowLoading();
			}
			return;
		}
		gameManager.homeScene.zhadanInvade.codeRunBox.TaskOver();
		isGameOver = true;
		if (gameManager.homeScene.zhadanInvade.userid != "3300010" && gameManager.homeScene.zhadanInvade.userid != "3300011")
		{
			Object.Instantiate(Resources.Load<GameObject>("zhadan/zhadansucceload"), base.transform).GetComponent<ZhadanSucceLoad>().zhadanDialog = this;
		}
		else if (gameManager.homeScene.zhadanInvade.userid == "3300010")
		{
			overPanel.SetActive(value: true);
			overPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.3f);
			Invoke("HideOverPanel", 2f);
			gameManager.homeScene.zhadanInvade.GameOver(isOver: false);
		}
		else if (gameManager.homeScene.zhadanInvade.userid == "3300011")
		{
			overPanel.SetActive(value: true);
			overPanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.3f);
			gameManager.homeScene.zhadanInvoke.StopInterval();
			Invoke("ShowVan", 2f);
		}
	}

	private void HideOverPanel()
	{
		overPanel.SetActive(value: false);
		Object.Instantiate(Resources.Load<GameObject>("zhadan/zhadanload1"), base.transform).GetComponent<ZhadanLoad1>().zhadanDialog = this;
	}

	private void ShowVan()
	{
		if (gameManager.player.playerdata.completeHideGame)
		{
			gameManager.homeScene.ShowVideoTip("3700069");
		}
		else
		{
			gameManager.homeScene.ShowVideoTip("3700070");
		}
	}

	public void Trigger()
	{
		imgTip.color = new Color(1f, 0.14f, 0.14f);
		imgTip.GetComponent<I18NText>().updateTranslation2("^zhadan_label12");
	}
}
