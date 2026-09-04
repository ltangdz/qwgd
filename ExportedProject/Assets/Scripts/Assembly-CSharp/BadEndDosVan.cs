using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class BadEndDosVan : MonoBehaviour
{
	public List<string> labelList;

	public List<string> yLabel;

	public List<string> nLabel;

	public List<GameObject> codeRunObj;

	public VideoDialog3700077 videoDialog;

	private Coroutine shape;

	private bool canInput;

	private void Start()
	{
		GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f);
		StartCoroutine(Run());
	}

	private IEnumerator Run()
	{
		int i;
		for (i = 0; i < labelList.Count; i++)
		{
			codeRunObj[i].transform.Find("Image").gameObject.SetActive(value: true);
			if (shape != null)
			{
				StopCoroutine(shape);
			}
			shape = StartCoroutine(RunLight(codeRunObj[i].transform.Find("Image").GetComponent<Image>()));
			codeRunObj[i].GetComponent<Text>().DOText(I18N.instance.getValue(labelList[i]), 1f).SetEase(Ease.Linear)
				.OnComplete(delegate
				{
					codeRunObj[i].transform.Find("Image").gameObject.SetActive(value: false);
				});
			yield return new WaitForSeconds(1f);
		}
		codeRunObj[3].transform.Find("Image").gameObject.SetActive(value: true);
		codeRunObj[3].GetComponent<I18NText>().updateTranslation2(">");
		shape = StartCoroutine(RunLight(codeRunObj[3].transform.Find("Image").GetComponent<Image>()));
		canInput = true;
	}

	private IEnumerator RunLight(Image obj)
	{
		while (true)
		{
			obj.color = new Color(1f, 1f, 1f, 1f);
			yield return new WaitForSeconds(0.1f);
			obj.color = new Color(1f, 1f, 1f, 0.3f);
			yield return new WaitForSeconds(0.1f);
		}
	}

	private IEnumerator ChoiceRun(List<string> label)
	{
		int i;
		for (i = 0; i < label.Count; i++)
		{
			codeRunObj[i + labelList.Count + 1].transform.Find("Image").gameObject.SetActive(value: true);
			if (shape != null)
			{
				StopCoroutine(shape);
			}
			shape = StartCoroutine(RunLight(codeRunObj[i + labelList.Count + 1].transform.Find("Image").GetComponent<Image>()));
			codeRunObj[i + labelList.Count + 1].GetComponent<Text>().DOText(I18N.instance.getValue(label[i]), 2f).SetEase(Ease.Linear)
				.OnComplete(delegate
				{
					codeRunObj[i + labelList.Count + 1].transform.Find("Image").gameObject.SetActive(value: false);
				});
			yield return new WaitForSeconds(2.5f);
		}
		yield return new WaitForSeconds(1f);
		GetComponent<RectTransform>().DOScale(Vector3.zero, 0.2f).OnComplete(delegate
		{
			videoDialog.canPass = true;
			Object.Destroy(base.gameObject);
		});
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Y) && canInput)
		{
			canInput = false;
			codeRunObj[3].GetComponent<I18NText>().updateTranslation2(">Y");
			StartCoroutine(ChoiceRun(yLabel));
			codeRunObj[3].transform.Find("Image").gameObject.SetActive(value: false);
			videoDialog.ChoiceYes();
		}
		else if (Input.GetKeyUp(KeyCode.N) && canInput)
		{
			canInput = false;
			codeRunObj[3].GetComponent<I18NText>().updateTranslation2(">N");
			StartCoroutine(ChoiceRun(nLabel));
			codeRunObj[3].transform.Find("Image").gameObject.SetActive(value: false);
			videoDialog.ChoiceNo();
		}
	}
}
