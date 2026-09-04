using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class AccuracyUI : MonoBehaviour
{
	public Text txt_acc;

	public Image[] accs;

	public float currentacc;

	private bool isgoing;

	public float dengfen;

	private void Start()
	{
		dengfen = 100f / (float)accs.Length;
	}

	private void Update()
	{
	}

	public void FreshAddAcc(float acc)
	{
		if (!isgoing)
		{
			StartCoroutine(StartAnimation(currentacc + acc));
		}
	}

	public void FreshAcc(float acc)
	{
		if (!isgoing)
		{
			StartCoroutine(StartAnimation(acc));
		}
	}

	private IEnumerator StartAnimation(float acc)
	{
		if (acc > 100f)
		{
			acc = 100f;
		}
		isgoing = true;
		int accper = (int)(((acc % 2f == 0f) ? acc : (acc - 1f)) / dengfen);
		int num = ((acc > currentacc) ? 1 : 0);
		int num2 = (int)(((currentacc % 2f == 0f) ? currentacc : (currentacc - 1f)) / dengfen);
		if (num != 0)
		{
			for (int i = num2; i < accs.Length; i++)
			{
				if (i <= accper)
				{
					accs[i].color = Color.white;
					txt_acc.GetComponent<I18NText>().updateTranslation2((((float)i * dengfen > 100f) ? 100f : ((float)i * dengfen)).ToString("f2") + "<size=23>%</size>");
				}
				else
				{
					accs[i].color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
				}
				yield return new WaitForSeconds(0.02f);
			}
		}
		else
		{
			for (int i = num2; i >= 0; i--)
			{
				if (i >= accper)
				{
					if (i < accs.Length)
					{
						accs[i].color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
					}
					txt_acc.GetComponent<I18NText>().updateTranslation2(((i * 2 > 100) ? 100 : (i * 2)).ToString("f2") + "<size=23>%</size>");
				}
				else if (i < accs.Length)
				{
					accs[i].color = Color.white;
				}
				yield return new WaitForSeconds(0.02f);
			}
		}
		currentacc = acc;
		if (currentacc == 100f)
		{
			txt_acc.GetComponent<I18NText>().updateTranslation2(currentacc.ToString("f2") + "<size=23>%</size>");
		}
		isgoing = false;
	}

	public void Restart(float acc)
	{
		int num = (int)(((acc % 2f == 0f) ? acc : (acc - 1f)) / dengfen);
		for (int num2 = (int)(((currentacc % 2f == 0f) ? currentacc : (currentacc - 1f)) / dengfen); num2 >= 0; num2--)
		{
			if (num2 >= num)
			{
				if (num2 < accs.Length)
				{
					accs[num2].color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
				}
				txt_acc.GetComponent<I18NText>().updateTranslation2(((num2 * 2 > 100) ? 100 : (num2 * 2)).ToString("f2") + "<size=23>%</size>");
			}
			else if (num2 < accs.Length)
			{
				accs[num2].color = Color.white;
			}
		}
		currentacc = acc;
		txt_acc.GetComponent<I18NText>().updateTranslation2(currentacc.ToString("f2") + "<size=23>%</size>");
	}
}
