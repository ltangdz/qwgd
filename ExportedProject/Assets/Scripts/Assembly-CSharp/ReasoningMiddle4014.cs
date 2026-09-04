using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ReasoningMiddle4014 : ReasoningMiddle
{
	public ReasoningPanel reasoningPanel;

	public List<GameObject> step1List;

	public List<GameObject> step2List;

	public List<GameObject> step3List;

	public List<GameObject> step4List;

	public List<GameObject> stepList;

	public int _step;

	public bool isallright;

	public Button btn_sure;

	public Button btn_next;

	private GameManager gameManager;

	private void Start()
	{
		btn_next.onClick.AddListener(Valid);
		reasoningPanel.playCioSound = false;
		reasoningPanel.SetTopContent("^8354607A-E7BE-0BEE-F9F6-C77A9FCD49EE", 0);
	}

	private void Valid()
	{
		List<GameObject> list = ((_step == 0) ? step1List : ((_step == 1) ? step2List : ((_step != 2) ? step4List : step3List)));
		bool flag = true;
		if (_step == 0)
		{
			foreach (GameObject item in list)
			{
				if (!item.GetComponent<ReasonOptionGroup>().ValidResult())
				{
					flag = false;
				}
			}
		}
		else if (_step == 1)
		{
			flag = stepList[_step].GetComponent<Reason4014Step2>().Valid();
		}
		else if (_step == 2)
		{
			flag = stepList[_step].GetComponent<Reason4014Step3>().Valid();
		}
		else if (_step == 3)
		{
			flag = stepList[_step].GetComponent<Reason4014Step4>().Valid();
		}
		if (flag)
		{
			if (_step == 3)
			{
				flag = true;
				reasoningPanel.GetResult();
				return;
			}
			float y = reasoningPanel.gameObject.GetComponent<RectTransform>().sizeDelta.y;
			GameObject obj = stepList[_step];
			GameObject gameObject = stepList[_step + 1];
			Vector3 position = obj.transform.position;
			obj.transform.DOMoveY(position.y + y, 1f);
			gameObject.SetActive(value: true);
			Vector3 position2 = gameObject.transform.position;
			gameObject.transform.DOMoveY(position2.y + y, 1f);
			_step++;
		}
	}
}
