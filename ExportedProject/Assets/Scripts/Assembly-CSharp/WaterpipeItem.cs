using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WaterpipeItem : MonoBehaviour
{
	public WaterpipeManager waterpipeManager;

	public bool isstart;

	public Color greencolor;

	public Sprite greenbk;

	public Sprite graybk;

	public int r;

	public Transform lineGroup;

	public bool ishasup;

	public bool ishasbottom;

	public bool ishasleft;

	public bool ishasright;

	public bool isup;

	public bool isbottom;

	public bool isleft;

	public bool isright;

	public WaterpipeItem upitem;

	public WaterpipeItem bottomitem;

	public WaterpipeItem leftitem;

	public WaterpipeItem rightitem;

	public bool isgreen;

	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(RotateItem);
	}

	private void RotateItem()
	{
		if (waterpipeManager.iscanclick)
		{
			ResetVec();
			if (r == 0)
			{
				lineGroup.DOLocalRotate(new Vector3(0f, 0f, -90f), 0.1f);
				r = 1;
				isright = ishasup;
				isbottom = ishasright;
				isleft = ishasbottom;
				isup = ishasleft;
			}
			else if (r == 1)
			{
				lineGroup.DOLocalRotate(new Vector3(0f, 0f, -180f), 0.1f);
				r = 2;
				isbottom = ishasup;
				isup = ishasbottom;
				isleft = ishasright;
				isright = ishasleft;
			}
			else if (r == 2)
			{
				lineGroup.DOLocalRotate(new Vector3(0f, 0f, -270f), 0.1f);
				r = 3;
				isleft = ishasup;
				isup = ishasright;
				isright = ishasbottom;
				isbottom = ishasleft;
			}
			else if (r == 3)
			{
				lineGroup.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.1f);
				r = 0;
				isleft = ishasleft;
				isup = ishasup;
				isright = ishasright;
				isbottom = ishasbottom;
			}
			waterpipeManager.Check();
		}
	}

	public void Next(bool isgreen)
	{
		if (!this.isgreen || isstart)
		{
			SetGreen(isgreen);
			if (upitem != null && isup && upitem.isbottom)
			{
				upitem.Next(isup);
			}
			if (bottomitem != null && isbottom && bottomitem.isup)
			{
				bottomitem.Next(isbottom);
			}
			if (leftitem != null && isleft && leftitem.isright)
			{
				leftitem.Next(isleft);
			}
			if (rightitem != null && isright && rightitem.isleft)
			{
				rightitem.Next(isright);
			}
		}
	}

	public void SetGreen(bool isgreen)
	{
		this.isgreen = isgreen;
		if (isgreen)
		{
			for (int i = 0; i < lineGroup.childCount; i++)
			{
				if (lineGroup.GetChild(i).name.Equals("img_line"))
				{
					lineGroup.GetChild(i).GetComponent<Image>().color = greencolor;
				}
			}
			GetComponent<Image>().sprite = greenbk;
			return;
		}
		for (int j = 0; j < lineGroup.childCount; j++)
		{
			if (lineGroup.GetChild(j).name.Equals("img_line"))
			{
				lineGroup.GetChild(j).GetComponent<Image>().color = Color.white;
			}
		}
		GetComponent<Image>().sprite = graybk;
	}

	private void ResetVec()
	{
		isup = false;
		isbottom = false;
		isleft = false;
		isright = false;
	}
}
