using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TabLineGroup : MonoBehaviour
{
	public GameObject line;

	public List<TablineScale> tablineScales;

	private void Start()
	{
	}

	public void SelectTab(int pos)
	{
		switch (pos)
		{
		case 0:
			line.transform.DOLocalMoveY(217.6f, 0.3f);
			break;
		case 1:
			line.transform.DOLocalMoveY(133.2f, 0.3f);
			break;
		case 2:
			line.transform.DOLocalMoveY(44.6f, 0.3f);
			break;
		case 3:
			line.transform.DOLocalMoveY(-44.2f, 0.3f);
			break;
		case 4:
			line.transform.DOLocalMoveY(-131.1f, 0.3f);
			break;
		case 5:
			line.transform.DOLocalMoveY(-218.1f, 0.3f);
			break;
		}
		for (int i = 0; i < tablineScales.Count; i++)
		{
			if (i != pos)
			{
				tablineScales[i].SetStatus(0);
			}
			else
			{
				tablineScales[i].SetStatus(1);
			}
		}
	}

	private void Update()
	{
	}
}
