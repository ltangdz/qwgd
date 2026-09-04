using UnityEngine;

public class StreetPerson : MonoBehaviour
{
	private float showTime = 0.8f;

	private float stayTime = 1f;

	private float a;

	private bool showPerson;

	private bool hidePerson;

	private float showAlpha = 0.85f;

	private int index;

	private int showTimes;

	private void Start()
	{
	}

	private void Init()
	{
		showPerson = true;
		hidePerson = false;
	}

	public void Show(int i)
	{
		showTimes = i;
		base.gameObject.SetActive(value: true);
		if (i == 1)
		{
			showPerson = false;
			GetComponent<CanvasGroup>().alpha = showAlpha;
			Invoke("FirstShow", stayTime);
		}
		else
		{
			Init();
		}
	}

	private void FirstShow()
	{
		hidePerson = true;
		a = 0.7f;
	}

	private void Update()
	{
		if (showPerson)
		{
			a += Time.deltaTime * showTime;
			if (a <= showAlpha)
			{
				GetComponent<CanvasGroup>().alpha = a;
			}
			else
			{
				showPerson = false;
				Invoke("HidePerson", stayTime);
			}
		}
		if (hidePerson)
		{
			a -= Time.deltaTime * showTime;
			if (a >= 0f)
			{
				GetComponent<CanvasGroup>().alpha = a;
				return;
			}
			hidePerson = false;
			base.gameObject.SetActive(value: false);
		}
	}

	private void HidePerson()
	{
		hidePerson = true;
	}
}
