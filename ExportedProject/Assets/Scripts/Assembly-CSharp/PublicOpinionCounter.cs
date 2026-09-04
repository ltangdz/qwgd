using System.Collections.Generic;
using DLC7.DDOS;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using _DLC8.Game.PublicOpinion;

public class PublicOpinionCounter : MonoBehaviour
{
	public Text txtShuijun;

	public Button btnAdd;

	public Button btnResume;

	public Text choiceVal;

	public List<Sprite> addBtnSprite;

	public List<Sprite> resumeBtnSprite;

	private PublicOpinionCardControl _controller;

	public int allShuijun;

	private GameManager gameManager;

	private void OnEnable()
	{
		BagDragManager<PublicOpinionInfo>.Instance.onDragStart += DragStart;
	}

	private void DragStart(string arg1, PointerEventData arg2, PublicOpinionInfo arg3, string arg4)
	{
		InitUI();
	}

	private void OnDisable()
	{
		BagDragManager<PublicOpinionInfo>.Instance.onDragStart -= DragStart;
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btnAdd.onClick.AddListener(AddVal);
		btnResume.onClick.AddListener(ResumeVal);
	}

	public void Init(PublicOpinionCardControl controller)
	{
		_controller = controller;
		allShuijun = _controller.cardInfos.Count * 2;
		InitUI();
	}

	public void InitUI()
	{
		int num = 0;
		txtShuijun.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^yulun_label247") + "(" + _controller.UsedPersonCount() + "/" + allShuijun + ")");
		PublicOpinionInfo curCardInfo = _controller.GetCurCardInfo();
		if (curCardInfo == null)
		{
			choiceVal.GetComponent<I18NText>().updateTranslation2(num.ToString());
			btnAdd.interactable = false;
			btnResume.interactable = false;
			return;
		}
		num = curCardInfo.roleNum;
		bool flag = (int)curCardInfo.roleNum > 0 && _controller.UsedPersonCount() > 0;
		bool flag2 = (int)curCardInfo.roleNum < allShuijun && _controller.UsedPersonCount() < allShuijun;
		choiceVal.GetComponent<I18NText>().updateTranslation2(num.ToString());
		btnAdd.GetComponent<Image>().sprite = addBtnSprite[(!flag2) ? 1 : 0];
		btnAdd.interactable = flag2;
		btnResume.interactable = flag;
		btnResume.GetComponent<Image>().sprite = resumeBtnSprite[(!flag) ? 1 : 0];
	}

	private void AddVal()
	{
		int num = allShuijun - _controller.UsedPersonCount();
		PublicOpinionInfo curCardInfo = _controller.GetCurCardInfo();
		if (curCardInfo != null && num > 0)
		{
			++curCardInfo.roleNum;
			gameManager.soundManager.Stop();
			gameManager.soundManager.PlaySound(34);
			InitUI();
		}
	}

	private void ResumeVal()
	{
		PublicOpinionInfo curCardInfo = _controller.GetCurCardInfo();
		if (curCardInfo != null && (int)curCardInfo.roleNum != 0)
		{
			gameManager.soundManager.Stop();
			gameManager.soundManager.PlaySound(34);
			--curCardInfo.roleNum;
			InitUI();
		}
	}
}
