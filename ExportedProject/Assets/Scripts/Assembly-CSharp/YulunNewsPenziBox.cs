using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class YulunNewsPenziBox : MonoBehaviour
{
	public YulunDialog yulunDialog;

	public Image penziIcon;

	public Text penziName;

	public Text addVal;

	public Text title;

	public YulunTipList yulunTipList;

	public void Init(int a, int chufa)
	{
		penziIcon.enabled = true;
		Debug.Log("喷子init打印*****string a val:" + a);
		Debug.Log("喷子init打印*****int a val:" + a);
		if (a >= 0 && a <= 5)
		{
			GetComponent<CanvasGroup>().alpha = 1f;
			Debug.Log("index拿到的值：" + a);
			yulunTipList = yulunDialog.yulunPenziDialog.tipList[a];
			if (yulunTipList.val < 5f)
			{
				penziIcon.sprite = yulunTipList.icon.sprite;
				penziName.GetComponent<I18NText>().updateTranslation2(yulunTipList.tipName.text);
				addVal.GetComponent<I18NText>().updateTranslation2("+1");
				if (chufa == -1)
				{
					title.GetComponent<I18NText>().updateTranslation2("^yulun_label228");
				}
				else
				{
					title.GetComponent<I18NText>().updateTranslation2("^yulun_label227");
				}
			}
		}
		else
		{
			GetComponent<CanvasGroup>().alpha = 0f;
			Clear();
		}
	}

	public void Clear()
	{
		GetComponent<CanvasGroup>().alpha = 0f;
		penziIcon.enabled = false;
		penziName.text = "";
		addVal.text = "";
	}
}
