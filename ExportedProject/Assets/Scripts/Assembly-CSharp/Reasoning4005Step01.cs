using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4005Step01 : MonoBehaviour
{
	[SerializeField]
	private GameObject step02;

	[SerializeField]
	private List<LineItem> lineitems = new List<LineItem>();

	[SerializeField]
	private string correct = "";

	[SerializeField]
	private Button btn_continue;

	public bool iscandrag = true;

	public void ResetAllGray()
	{
		for (int i = 0; i < lineitems.Count; i++)
		{
			if (lineitems[i].currentfaceitem == null)
			{
				lineitems[i].SetGray();
			}
		}
	}

	private void Start()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 2f));
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1f, 1f, 1f), 2f));
		sequence.Play().SetLoops(-1);
		btn_continue.onClick.AddListener(Check);
	}

	private void Check()
	{
		string text = "";
		for (int i = 0; i < lineitems.Count; i++)
		{
			if (lineitems[i].currentfaceitem != null)
			{
				text += lineitems[i].currentfaceitem.id;
			}
		}
		Debug.Log("result:" + text);
		if (text.Equals(correct))
		{
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			StartCoroutine(Over());
		}
		else
		{
			for (int j = 0; j < lineitems.Count; j++)
			{
				lineitems[j].SetRed();
			}
		}
	}

	private IEnumerator Over()
	{
		GetComponent<CanvasGroup>().DOFade(0f, 1f);
		yield return new WaitForSeconds(1f);
		step02.SetActive(value: true);
		base.gameObject.SetActive(value: false);
	}

	public void ShowAllLingxing()
	{
		for (int i = 0; i < lineitems.Count; i++)
		{
			lineitems[i].StartLingXingAnimation();
		}
	}

	public void HideAllLingxing()
	{
		for (int i = 0; i < lineitems.Count; i++)
		{
			lineitems[i].StopLingXingAnimation();
		}
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
