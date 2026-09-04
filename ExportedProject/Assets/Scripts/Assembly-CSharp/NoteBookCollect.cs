using System.Collections.Generic;
using Honeti;
using UnityEngine;

public class NoteBookCollect : MonoBehaviour
{
	public List<MultiplyText> objList;

	public List<string> collectID;

	public List<string> commentLabel;

	public List<string> collectLabel;

	private void Start()
	{
		for (int i = 0; i < objList.Count; i++)
		{
			objList[i].SetContent2(commentLabel[i], collectID[i], I18N.instance.getValue(collectLabel[i]));
		}
	}
}
