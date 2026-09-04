using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ZhadanLoad1 : MonoBehaviour
{
	public List<Image> loadList;

	public Text percent;

	public Text txttip;

	public ZhadanDialog zhadanDialog;

	private int crtIndex;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		StartCoroutine(StartLoad(0.06f, showVan: true, 0.55f));
	}

	private IEnumerator StartLoad(float time, bool showVan, float num)
	{
		float l = loadList.Count;
		int loadlength = (int)Mathf.Round(l * num);
		for (int i = crtIndex; i < loadlength; i++)
		{
			crtIndex = i;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(loadList[i].DOFade(1f, time));
			sequence.Append(loadList[i].DOFade(0.6f, time));
			sequence.Append(loadList[i].DOFade(0.9f, time));
			sequence.Play().SetLoops(2);
			percent.GetComponent<I18NText>().updateTranslation2(Mathf.Round((float)i / l * 100f) + "%");
			yield return new WaitForSeconds(time * 6f);
		}
		if (showVan)
		{
			Object.Instantiate(Resources.Load<GameObject>("Dialog/videoDialog3700068"), gameManager.homeScene.middle).GetComponent<VideoDialog3700068>().zhadanLoad1 = this;
			yield break;
		}
		gameManager.homeScene.zhadanInvade.GameOver();
		percent.GetComponent<I18NText>().updateTranslation2("100%");
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

	public void EndCrack()
	{
		StartCoroutine(StartLoad(0.02f, showVan: false, 1f));
	}
}
