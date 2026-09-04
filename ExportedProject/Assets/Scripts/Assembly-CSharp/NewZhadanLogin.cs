using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class NewZhadanLogin : MonoBehaviour
{
	public Transform box;

	public List<Transform> pointList;

	public List<Image> loadList;

	public Text tishi;

	public Text percent;

	private IEnumerator pointie;

	private GameManager gameManager;

	private string zhadanID;

	public void Init(bool isSucce, string id = "")
	{
		if (gameManager == null)
		{
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		}
		zhadanID = id;
		pointie = PointRun();
		StartCoroutine(Login(isSucce));
	}

	private IEnumerator Login(bool isSucce)
	{
		box.DOScale(Vector3.one, 0.3f);
		box.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		StartCoroutine(pointie);
		StartCoroutine(StartLoading(isSucce));
	}

	private IEnumerator PointRun()
	{
		while (true)
		{
			for (int i = 0; i < pointList.Count; i++)
			{
				pointList[i].Find("Image").gameObject.SetActive(value: false);
			}
			for (int j = 0; j < pointList.Count; j++)
			{
				pointList[j].Find("Image").gameObject.SetActive(value: true);
				yield return new WaitForSeconds(0.2f);
			}
		}
	}

	private IEnumerator StartLoading(bool isSucce)
	{
		float count = ((!isSucce) ? Mathf.Floor(Random.Range((float)loadList.Count * 0.16f, (float)loadList.Count * 0.36f)) : ((float)loadList.Count));
		for (int i = 0; (float)i < count; i++)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(loadList[i].DOFade(1f, 0.06f));
			sequence.Append(loadList[i].DOFade(0.6f, 0.06f));
			sequence.Play().SetLoops(2);
			sequence.Append(loadList[i].DOFade(0.9f, 0.06f));
			float num = Mathf.Floor((float)i * 100f / ((float)loadList.Count * 100f) * 100f);
			percent.GetComponent<I18NText>().updateTranslation2(num + "%");
			yield return new WaitForSeconds(3f / (float)loadList.Count);
		}
		if (isSucce)
		{
			percent.GetComponent<I18NText>().updateTranslation2("100%");
			GameObject obj = Object.Instantiate(Resources.Load<GameObject>("zhadan/zhadanInvade"), gameManager.homeScene.middle);
			obj.GetComponent<ZhadanInvade>().Show();
			obj.GetComponent<ZhadanInvade>().userid = zhadanID;
			obj.GetComponent<ZhadanInvade>().PojieSuccess();
			yield return new WaitForSeconds(0.3f);
			Object.Destroy(base.gameObject);
			yield break;
		}
		for (int j = 0; (float)j < count; j++)
		{
			loadList[j].transform.Find("Image").gameObject.SetActive(value: true);
		}
		tishi.color = new Color(0.8f, 0.09f, 0.09f);
		percent.color = new Color(0.8f, 0.09f, 0.09f);
		StopCoroutine(pointie);
		tishi.GetComponent<I18NText>().updateTranslation2("^zhadan_label38");
		yield return new WaitForSeconds(2f);
		box.DOScale(Vector3.zero, 0.3f);
		box.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		Object.Destroy(base.gameObject);
	}
}
