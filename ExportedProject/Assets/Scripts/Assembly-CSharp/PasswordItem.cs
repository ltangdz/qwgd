using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using tnt_deploy;

public class PasswordItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public InputField inputField;

	[HideInInspector]
	public GameManager gameManager;

	public PasswordDialog1 passwordDialog1;

	public string itemid;

	public int itemType;

	private string eventID;

	private string origInput = "";

	private bool _isEnter;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		inputField.onValueChanged.AddListener(Input_change);
		inputField.onEndEdit.AddListener(Input_End);
		eventID = gameManager.player.GetEventId();
	}

	private void OnEnable()
	{
		NoteDragManager.Instance.onDragStart += OnDragStart;
		NoteDragManager.Instance.onDraging += OnDraging;
		NoteDragManager.Instance.onDragEnd += OnDragEnd;
	}

	private void OnDisable()
	{
		NoteDragManager.Instance.onDragStart -= OnDragStart;
		NoteDragManager.Instance.onDraging -= OnDraging;
		NoteDragManager.Instance.onDragEnd -= OnDragEnd;
	}

	private void OnDragStart(PointerEventData eventData, DATA1 data)
	{
	}

	private void OnDraging(PointerEventData eventData, DATA1 data)
	{
	}

	private void OnDragEnd(PointerEventData eventData, DATA1 data)
	{
		if (_isEnter)
		{
			inputField.text = I18N.instance.getValue(data.message);
			InputEnd();
		}
	}

	private void Input_change(string arg0)
	{
		passwordDialog1.inputType = true;
	}

	private void Input_End(string arg0)
	{
		Invoke("InputEnd", 0.2f);
	}

	public void InputEnd()
	{
		if (gameManager.Is_Dlc7() || gameManager.Is_Dlc6())
		{
			passwordDialog1.InputEnd(itemType);
			return;
		}
		string text = inputField.text;
		Debug.Log(text);
		List<DATA1> allItems = gameManager.dataManager.GetAllItems(eventID);
		bool flag = false;
		for (int i = 0; i < allItems.Count; i++)
		{
			DATA1 dATA = allItems[i];
			if ((gameManager.IsAllDlc() && dATA.role.Substring(1) != gameManager._selectedPlayerId) || !text.ToLower().Trim().Equals(I18N.instance.getValue(dATA.message).ToLower().Trim()))
			{
				continue;
			}
			if (dATA.passwordnumber == itemType)
			{
				if (itemid != dATA.ID.ToString())
				{
					if (!itemid.Equals("0") && !itemid.Equals(""))
					{
						passwordDialog1.MinusItem(gameManager.dataManager.dic1[itemid]);
					}
					itemid = dATA.ID.ToString();
					flag = true;
					passwordDialog1.SetItem(dATA);
				}
				if (!gameManager.Is_Dlc6() || dATA.role.Substring(1) == gameManager._selectedPlayerId)
				{
					return;
				}
			}
			else if (!itemid.Equals("0") && !itemid.Equals(""))
			{
				flag = true;
				passwordDialog1.MinusItem(gameManager.dataManager.dic1[itemid]);
				itemid = "0";
			}
		}
		if (!itemid.Equals("0") && !itemid.Equals(""))
		{
			flag = true;
			passwordDialog1.MinusItem(gameManager.dataManager.dic1[itemid]);
			itemid = "0";
		}
		if (!flag)
		{
			passwordDialog1.inputType = false;
		}
	}

	private void Update()
	{
		string text = inputField.text;
		if (text != origInput && ((text.Trim() != "" && !passwordDialog1.btn_go.interactable) || (text.Trim() == "" && passwordDialog1.btn_go.interactable)))
		{
			origInput = text;
			float val = ((!(text.Trim() == "")) ? 1 : (-1));
			passwordDialog1.OpenBtnFun(itemType, val);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_isEnter = false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_isEnter = true;
	}
}
