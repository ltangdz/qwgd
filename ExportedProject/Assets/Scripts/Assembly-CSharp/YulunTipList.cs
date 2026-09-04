using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class YulunTipList : MonoBehaviour
{
	public Image icon;

	public Text tipName;

	public Text txtVal;

	public float val;

	public void AddVal()
	{
		if (val < 5f)
		{
			val += 1f;
		}
		else
		{
			val = 5f;
		}
		Invoke("ShowShuijun", 2f);
	}

	private void ShowShuijun()
	{
		if (!txtVal.isActiveAndEnabled)
		{
			txtVal.gameObject.SetActive(value: true);
		}
		txtVal.GetComponent<I18NText>().updateTranslation2("+" + val);
	}
}
