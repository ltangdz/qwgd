using System.Collections.Generic;
using UnityEngine;

public class YulunPenziDialog : CustomDialog
{
	public List<YulunTipList> tipList;

	public void AddVal(List<int> penziList)
	{
		for (int i = 0; i < penziList.Count; i++)
		{
			Debug.Log("添加喷子：" + penziList[i]);
			tipList[penziList[i]].AddVal();
		}
	}

	public override void AfterShowSize()
	{
	}

	public override void BeforeShowSize()
	{
	}
}
