using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ZhadanSucceLoad : MonoBehaviour
{
	public List<Image> loadList;

	public Text percent;

	public Text txttip;

	public ZhadanDialog zhadanDialog;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		base.transform.DOScale(Vector3.one, 0.3f);
		GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		StartCoroutine(StartLoad(0.06f));
	}

	public void Init(string label)
	{
		txttip.GetComponent<I18NText>().updateTranslation2(label);
	}

	private IEnumerator StartLoad(float time)
	{
		yield return new WaitForSeconds(0.3f);
		float l = loadList.Count;
		for (int i = 0; (float)i < l; i++)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(loadList[i].DOFade(1f, time));
			sequence.Append(loadList[i].DOFade(0.6f, time));
			sequence.Play().SetLoops(2);
			percent.GetComponent<I18NText>().updateTranslation2(Mathf.Round((float)i / l * 100f) + "%");
			loadList[i].DOFade(0.9f, time);
			yield return new WaitForSeconds(0.1f);
		}
		percent.GetComponent<I18NText>().updateTranslation2("100%");
		if (gameManager.homeScene.zhadanInvade.userid != "3300010")
		{
			gameManager.homeScene.zhadanInvoke.StopInterval();
		}
		yield return new WaitForSeconds(0.5f);
		base.transform.DOScale(Vector3.zero, 0.3f);
		GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("zhadan/zhadanSucc"), zhadanDialog.transform);
		if (gameManager.homeScene.zhadanInvade.userid == "3300010")
		{
			gameObject.GetComponent<ZhadanSuccConfirm>().Init("^zhadan_label10");
		}
		Object.Destroy(base.gameObject);
	}
}
