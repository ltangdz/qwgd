using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reason4014Step4 : MonoBehaviour
{
	private List<GameObject> _rightItemList = new List<GameObject>();

	[SerializeField]
	private List<GameObject> _leftItemList = new List<GameObject>();

	private List<Reason4014StepModel> _rightItemKeyList = new List<Reason4014StepModel>();

	public Transform _rightPanel;

	public Transform _leftPanel;

	private string _groupKey = "4014Step4";

	private int _validStatus = -1;

	private bool _canValid = true;

	private void Start()
	{
		List<string> original = new List<string>(new string[4] { "^C23374A7-F2BD-2CBC-A1B3-7945A4ED8F09", "^34C1953A-235A-557E-1E21-AAC0669A2DE7", "^C35A9857-2857-FD66-035F-EA7E929A6C3C", "^881A5627-0916-9555-2151-9E7CFC4D150F" });
		List<string> original2 = new List<string>(new string[5] { "touxiang/_dlc6_Harris", "^touxiang/_dlc6_Herbert", "touxiang/_dlc6_Lisa", "touxiang/_dlc6_Teressa", "touxiang/_dlc6_Claudia" });
		AlubaTools.RandomList(original);
		AlubaTools.RandomList(original2);
	}

	public bool Valid()
	{
		ReasonOptionGroup component = _leftPanel.GetComponent<ReasonOptionGroup>();
		ReasonOptionGroup component2 = _rightPanel.GetComponent<ReasonOptionGroup>();
		bool num = component.ValidResult();
		bool flag = component2.ValidResult();
		if (!num || !flag)
		{
			return false;
		}
		return true;
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
