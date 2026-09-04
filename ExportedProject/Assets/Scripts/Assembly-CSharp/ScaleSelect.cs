using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScaleSelect : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	private string selfID;

	public int pos;

	private bool changeing;

	private string crtType;

	public string SelfID
	{
		get
		{
			return selfID;
		}
		set
		{
			selfID = value;
		}
	}

	private void Start()
	{
		Invoke("CanClick", 0.5f);
	}

	private void CanClick()
	{
		GetComponent<Button>().interactable = true;
	}

	private void Update()
	{
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		StopAllCoroutines();
		crtType = "small";
		changeing = true;
		StartCoroutine(Biger());
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		StopAllCoroutines();
		crtType = "big";
		changeing = true;
		StartCoroutine(Smaller());
	}

	private IEnumerator Biger()
	{
		if (!(crtType == "small"))
		{
			yield break;
		}
		float a = GetComponent<RectTransform>().localScale.x;
		while (changeing)
		{
			if (a <= 1.2f)
			{
				GetComponent<RectTransform>().localScale = new Vector3(a, a, 1f);
				a += 0.02f;
				yield return new WaitForSeconds(0.01f);
			}
			else
			{
				changeing = false;
			}
		}
	}

	private IEnumerator Smaller()
	{
		if (!(crtType == "big"))
		{
			yield break;
		}
		float a = GetComponent<RectTransform>().localScale.x;
		while (changeing)
		{
			if (a >= 1f)
			{
				GetComponent<RectTransform>().localScale = new Vector3(a, a, 1f);
				a -= 0.02f;
				yield return new WaitForSeconds(0.01f);
			}
			else
			{
				changeing = false;
			}
		}
	}
}
