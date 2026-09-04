using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceAccount : MonoBehaviour
{
	private void Start()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			int s = i;
			base.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate
			{
				Choice(base.transform.GetChild(s), s);
			});
		}
	}

	public void Choice(Transform accBtn, int s)
	{
		if (s != 0)
		{
			_ = accBtn.GetComponent<OtherAccount>().GetId;
		}
		else
		{
			_ = accBtn.GetComponent<DefaultAccount>().GetId;
		}
	}

	public void Focus()
	{
		base.transform.DOScaleY(1f, 0.2f);
	}

	public void Blur()
	{
		base.transform.DOScaleY(0f, 0.2f);
	}
}
