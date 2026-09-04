using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reason4014Step2 : MonoBehaviour
{
	private List<GameObject> _rightItemList = new List<GameObject>();

	private List<GameObject> _leftItemList = new List<GameObject>();

	private List<Reason4014StepModel> _rightItemKeyList = new List<Reason4014StepModel>();

	public Transform _rightPanel;

	public Transform _leftPanel;

	[Header("圆点外部")]
	public List<Image> _circleImages1;

	[Header("圆点内部")]
	public List<Image> _circleImages2;

	[Header("圆圈外框 0普通 1焦点 2错误")]
	public List<Sprite> _circleStatusImages1;

	[Header("圆圈内部点 0普通 1焦点 2错误")]
	public List<Sprite> _circleStatusImages2;

	private string _groupKey = "4014Step2";

	private int _validStatus = -1;

	private bool _canValid = true;

	private void Start()
	{
		List<string> list = new List<string>(new string[5] { "^509C6FDE-32A4-19AC-1731-EEB5C794B063", "^239F792F-A24F-B3D0-7773-9F1F11FD76BE", "^4DD73F96-9E67-A6FD-ADD1-F8291463001C", "^B926E51B-AD97-1B89-C99A-CC9DCE542A07", "^6BCAFEFE-12C6-6FEC-C29F-AF68D50AACB4" });
		List<string> list2 = new List<string>(new string[5] { "^0B38E8C1-3C02-77F4-3D62-A545FE3E6D33", "^AAF8EF6A-465C-A9B0-F732-9A9D2CE30720", "^CE3C3D51-A9EB-109D-3C93-A78893589A08", "^0948AF95-0BFF-BE1F-0D85-8C8F019B75F4", "^EDB46B5D-A60A-AF4F-1F91-A91A2896EAB6" });
		List<string> list3 = new List<string>(new string[5] { "^EB215BF1-D08F-2317-0AA4-C8E273757BB8", "^B7DFC102-1E73-6D47-A7D9-AEC036691703", "^2AE063FF-1877-8EA7-A48C-57C8FB97196E", "^89FA861F-2286-48D6-4F81-4AB19FA013D3", "^3D089CEA-EF98-F4F3-826E-F185C121D4F2" });
		_rightItemKeyList.Clear();
		for (int i = 0; i < list.Count; i++)
		{
			string titleKey = list[i];
			string messageKey = list2[i];
			string sampleKey = list3[i];
			Reason4014StepModel item = new Reason4014StepModel(titleKey, messageKey, sampleKey, _groupKey, i);
			_rightItemKeyList.Add(item);
			GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("_DLC/Prefabs/reason/step2_left"), _leftPanel);
			gameObject.GetComponent<Step2DragTargetItem>().Init(_groupKey, i, DragInType.GAMEOBJECT);
			_leftItemList.Add(gameObject);
		}
		_rightItemKeyList = AlubaTools.RandomList(_rightItemKeyList);
		foreach (Reason4014StepModel rightItemKey in _rightItemKeyList)
		{
			GameObject gameObject2 = Object.Instantiate(Resources.Load<GameObject>("_DLC/Prefabs/reason/step2_right"), _rightPanel);
			gameObject2.GetComponent<ReasonStep2RightItemSource>().Init(rightItemKey, rightItemKey.GroupKey, rightItemKey.TitleKey);
			_rightItemList.Add(gameObject2);
		}
	}

	public bool Valid()
	{
		if (!_canValid)
		{
			return false;
		}
		_canValid = false;
		_validStatus = -1;
		for (int i = 0; i < _leftItemList.Count; i++)
		{
			if (!_leftItemList[i].GetComponent<Step2DragTargetItem>().ValidData() || _validStatus == 1)
			{
				_circleImages1[i].sprite = _circleStatusImages1[2];
				_circleImages2[i].sprite = _circleStatusImages2[2];
				_validStatus = 1;
			}
			else
			{
				_validStatus = 0;
				_circleImages1[i].sprite = _circleStatusImages1[0];
				_circleImages2[i].sprite = _circleStatusImages2[0];
			}
		}
		if (_validStatus == 0)
		{
			return true;
		}
		if (_validStatus == 1)
		{
			StartCoroutine(ResetUI());
		}
		return false;
	}

	private IEnumerator ResetUI()
	{
		yield return new WaitForSeconds(5f);
		for (int i = 0; i < _leftItemList.Count; i++)
		{
			_leftItemList[i].GetComponent<Step2DragTargetItem>().ResetData();
			_rightItemList[i].GetComponent<ReasonStep2RightItemSource>().ResetData();
			_circleImages1[i].sprite = _circleStatusImages1[0];
			_circleImages2[i].sprite = _circleStatusImages2[0];
		}
		_canValid = true;
	}
}
