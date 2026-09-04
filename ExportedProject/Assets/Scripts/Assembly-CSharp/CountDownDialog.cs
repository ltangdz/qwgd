using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class CountDownDialog : MonoBehaviour
{
	public delegate void CallBak();

	public Text txt_content;

	public Text txt_time;

	public bool isstart;

	public int count_down = 10;

	public GameObject bk;

	public CallBak callBak;

	private void Start()
	{
	}

	public void SetTime(int t)
	{
		count_down = t;
		txt_time.text = $"{count_down / 3600:D2}:{count_down / 60:D2}:{count_down % 60:D2}";
		txt_time.GetComponent<I18NText>().updateTranslation2($"{count_down / 3600:D2}:{count_down / 60:D2}:{count_down % 60:D2}");
		InvokeRepeating("Time_count", 2f, 1f);
	}

	public void PauseTime()
	{
		CancelInvoke();
	}

	public void RestartTime()
	{
		InvokeRepeating("Time_count", 2f, 1f);
	}

	private void Time_count()
	{
		if (count_down > 0)
		{
			count_down--;
			if (count_down <= 9)
			{
				bk.GetComponent<Animator>().enabled = true;
			}
			txt_time.text = $"{count_down / 3600:D2}:{count_down / 60:D2}:{count_down % 60:D2}";
			txt_time.GetComponent<I18NText>().updateTranslation2($"{count_down / 3600:D2}:{count_down / 60:D2}:{count_down % 60:D2}");
		}
		else
		{
			CancelInvoke();
			if (callBak != null)
			{
				callBak();
			}
			Object.Destroy(base.gameObject);
		}
	}
}
