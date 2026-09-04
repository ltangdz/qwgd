using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ZhadanLoad : MonoBehaviour
{
	public List<Image> brackets;

	public List<Image> loadList;

	public Button btngoon;

	public GameObject tip;

	public GameObject wrongTip;

	private void Start()
	{
		StartCoroutine(StartLoad());
		btngoon.onClick.AddListener(GoNext);
	}

	private IEnumerator StartLoad()
	{
		int loadlength = (int)Mathf.Round((float)loadList.Count * 0.3f);
		for (int i = 0; i < loadlength; i++)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(loadList[i].DOFade(1f, 0.08f));
			sequence.Append(loadList[i].DOFade(0.6f, 0.08f));
			sequence.Append(loadList[i].DOFade(0.9f, 0.08f));
			sequence.Play().SetLoops(2);
			yield return new WaitForSeconds(0.48f);
		}
		for (int j = 0; j < brackets.Count; j++)
		{
			brackets[j].transform.Find("Image").gameObject.SetActive(value: true);
		}
		for (int k = 0; k < loadlength; k++)
		{
			loadList[k].color = new Color(1f, 1f, 1f, 0f);
			loadList[k].transform.Find("Image").gameObject.SetActive(value: true);
		}
		tip.SetActive(value: false);
		wrongTip.SetActive(value: true);
		btngoon.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^zhadan_label29");
		btngoon.interactable = true;
	}

	private void GoNext()
	{
		Object.Instantiate(Resources.Load<GameObject>("zhadan/waterpipepanel01"), base.transform.parent);
		Object.Destroy(base.gameObject);
	}
}
