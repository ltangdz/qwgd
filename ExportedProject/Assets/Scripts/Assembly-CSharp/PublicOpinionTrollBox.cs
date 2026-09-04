using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class PublicOpinionTrollBox : MonoBehaviour
{
	public YulunPenziDialog trollDialog;

	public Image penziIcon;

	public Text penziName;

	public Text addVal;

	public Text title;

	private YulunTipList _yulunTipList;

	public void Init(int a, int chufa)
	{
		penziIcon.enabled = true;
		Debug.Log("喷子init打印*****string a val:" + a);
		Debug.Log("喷子init打印*****int a val:" + a);
		if (a >= 0 && a <= 5)
		{
			GetComponent<CanvasGroup>().alpha = 1f;
			Debug.Log("index拿到的值：" + a);
			_yulunTipList = trollDialog.tipList[a];
			if (_yulunTipList.val <= 5f)
			{
				penziIcon.sprite = _yulunTipList.icon.sprite;
				penziName.GetComponent<I18NText>().updateTranslation2(_yulunTipList.tipName.text);
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
