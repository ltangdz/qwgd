using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reason4014Step3 : MonoBehaviour
{
	private List<GameObject> _rightItemList = new List<GameObject>();

	[SerializeField]
	private List<GameObject> _leftItemList = new List<GameObject>();

	private List<Reason4014StepModel> _rightItemKeyList = new List<Reason4014StepModel>();

	public Transform _rightPanel;

	private string _groupKey = "4014Step3";

	private int _validStatus = -1;

	private bool _canValid = true;

	private void Start()
	{
		List<string> list = new List<string>(new string[7] { "^152693F5-6BB0-F50B-E8BC-CC659F7465DF", "^6D35A26E-0F13-F17E-3A4B-563080790667", "^F3A52D5C-FE26-B270-DD3D-5EC89CBFFE5E", "^C8F53B19-2643-0B60-27C4-12D9CBE00676", "^2D86ED50-B275-F629-8781-6C27F6F68DBD", "^7C0476CC-6A27-B57D-12B8-D8A420580A74", "^32809BCF-6195-6EBB-418C-02A59B6E77BE" });
		_rightItemKeyList.Clear();
		for (int i = 0; i < list.Count; i++)
		{
			string titleKey = list[i];
			string messageKey = "";
			string sampleKey = "";
			Reason4014StepModel item = new Reason4014StepModel(titleKey, messageKey, sampleKey, _groupKey, i);
			_rightItemKeyList.Add(item);
		}
		_rightItemKeyList = AlubaTools.RandomList(_rightItemKeyList);
		foreach (Reason4014StepModel rightItemKey in _rightItemKeyList)
		{
			GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("_DLC/Prefabs/reason/step3_source"), _rightPanel);
			gameObject.GetComponent<ReasonStep3Source>().Init(rightItemKey, rightItemKey.GroupKey, rightItemKey.TitleKey);
			_rightItemList.Add(gameObject);
		}
	}

	public bool Valid()
	{
		if (!_canValid)
		{
			return false;
		}
		_canValid = false;
		_validStatus = 0;
		for (int i = 0; i < _leftItemList.Count; i++)
		{
			if (!_leftItemList[i].GetComponent<ReasonStep3Target>().ValidData() || _validStatus == 1)
			{
				_validStatus = 1;
			}
			else
			{
				_validStatus = 0;
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
			_leftItemList[i].GetComponent<ReasonStep3Target>().ClearData();
			_rightItemList[i].GetComponent<ReasonStep3Source>().ResetData();
		}
		_canValid = true;
	}
}
