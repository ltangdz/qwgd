using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class FishPhoneThought : MonoBehaviour
{
	public List<GameObject> choiceObj;

	public Image imgTop;

	public Image imgBottom;

	public Button btnStart;

	public GameObject choiceBox;

	public GameObject codeRun01;

	public GameObject codeRun02;

	public GameObject imgMask;

	public GameObject scrollBar;

	public RectTransform runImg;

	public string ObjID;

	public DATA33 data33;

	private GameManager gameManager;

	private string[] selImgUrl;

	private string trueSelName;

	private FishChoiceObj choiced;

	private bool startThought;

	public void Init(string id, GameManager gm)
	{
		ObjID = id;
		gameManager = gm;
		data33 = gameManager.dataManager.dic33[id];
		selImgUrl = data33.imgselect.Split(';');
		gameManager.CanShowSetting(1);
		StartCoroutine(InitSet());
		StartCoroutine(Ani());
		if (!gameManager.player.playerdata.videotiplist.Contains("3700050") && gameManager.player.GetEventId() == "110002")
		{
			gameManager.homeScene.ShowVideoTip("3700050");
		}
	}

	private IEnumerator Ani()
	{
		while (true)
		{
			runImg.sizeDelta = new Vector2(700f, 17f);
			runImg.DOSizeDelta(new Vector2(651f, 17f), 0.2f);
			yield return new WaitForSeconds(0.2f);
		}
	}

	private IEnumerator InitSet()
	{
		GetComponent<CanvasGroup>().DOFade(1f, 2f);
		yield return new WaitForSeconds(1f);
		imgTop.transform.DOLocalMoveY(480f, 2f);
		imgBottom.transform.DOLocalMoveY(-480f, 2f);
		yield return new WaitForSeconds(1.5f);
		choiceBox.GetComponent<CanvasGroup>().DOFade(1f, 1f);
		btnStart.GetComponent<CanvasGroup>().DOFade(1f, 1f);
		scrollBar.GetComponent<CanvasGroup>().DOFade(1f, 1f);
		for (int i = 0; i < selImgUrl.Length; i++)
		{
			Sprite sprite = Resources.Load<Sprite>("Image/" + selImgUrl[i].Split(':')[0]);
			Sprite sprite2 = Resources.Load<Sprite>("Image/" + selImgUrl[i].Split(':')[0] + "-1");
			choiceObj[i].GetComponent<FishChoiceObj>().imgSelBk.GetComponent<Image>().sprite = sprite;
			choiceObj[i].GetComponent<FishChoiceObj>().vagueImg.GetComponent<Image>().sprite = sprite2;
			if (selImgUrl[i].Split(':')[1] == "1")
			{
				choiceObj[i].GetComponent<FishChoiceObj>().isTrue = true;
			}
		}
		btnStart.onClick.AddListener(delegate
		{
			StartCoroutine(CodeRun());
		});
	}

	private IEnumerator CodeRun()
	{
		if (choiced != null && !startThought)
		{
			imgMask.SetActive(value: true);
			codeRun01.SetActive(value: true);
			yield return new WaitForSeconds(3.8f);
			codeRun02.SetActive(value: true);
			yield return new WaitForSeconds(3f);
			codeRun01.GetComponent<InvadeThoughtCodeRun>().Hide();
			codeRun02.GetComponent<InvadeThoughtCodeRun>().Hide();
			yield return new WaitForSeconds(0.8f);
			startThought = true;
			StartCoroutine(ResultRun());
		}
	}

	private IEnumerator ResultRun()
	{
		if (gameManager.GameType == GameTypeEnum.DLC6)
		{
			GetComponent<Animator>().Play("ani_fishHide_dlc");
		}
		else
		{
			GetComponent<Animator>().Play("ani_fishHide");
		}
		yield return new WaitForSeconds(1.5f);
		GameObject fishPhoneInvadeDialog = Object.Instantiate(Resources.Load<GameObject>(DLCNameUtil.Instance.GetFishPhoneInvadeDialogName()), base.transform.parent);
		base.transform.SetAsLastSibling();
		fishPhoneInvadeDialog.GetComponent<FishPhoneInvadeDialog>().Show();
		fishPhoneInvadeDialog.GetComponent<FishPhoneInvadeDialog>().Init(ObjID, choiced.isTrue);
		yield return new WaitForSeconds(1.6f);
		gameManager.CanShowSetting(-1);
		Object.Destroy(base.gameObject);
		fishPhoneInvadeDialog.transform.SetAsLastSibling();
		gameManager.homeScene.notebook.transform.SetAsLastSibling();
	}

	public void ObjFoces(GameObject objName)
	{
		if (!btnStart.interactable)
		{
			btnStart.interactable = true;
		}
		for (int i = 0; i < choiceObj.Count; i++)
		{
			if (choiceObj[i].name != objName.name)
			{
				choiced = objName.GetComponent<FishChoiceObj>();
				choiceObj[i].GetComponent<FishChoiceObj>().Blur();
			}
		}
	}

	private void Update()
	{
	}
}
