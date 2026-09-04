using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class DownloadFileBox : MonoBehaviour
{
	public Image percentImg;

	public Sprite failedPercent;

	public Text percentTxt;

	public Text fileName;

	public Image failedImg;

	private string fileID;

	private DownloadDialog parObj;

	public void StartLoad(float loadTime, string loadID, DownloadDialog obj)
	{
		parObj = obj;
		fileID = loadID;
		GetComponent<CanvasGroup>().alpha = 1f;
		base.transform.SetAsLastSibling();
		StartCoroutine(Loading(loadTime + 10f));
	}

	private IEnumerator Loading(float loadTime)
	{
		float.Parse(percentTxt.text.Replace("%", ""));
		percentImg.transform.DOScaleX(1f, loadTime).SetEase(Ease.Linear).OnUpdate(delegate
		{
			float num = percentImg.GetComponent<RectTransform>().localScale.x * 100f;
			percentTxt.GetComponent<I18NText>().updateTranslation2((int)num + "%");
		})
			.OnComplete(delegate
			{
				percentTxt.GetComponent<I18NText>().updateTranslation2("100%");
				parObj.LoadComplete(fileID);
			});
		yield return null;
	}

	public void LoadFailed()
	{
		StopAllCoroutines();
		percentImg.transform.DOKill();
		percentImg.sprite = failedPercent;
		failedImg.gameObject.SetActive(value: true);
		percentTxt.color = parObj.failedColor;
		fileName.color = parObj.failedColor;
	}

	public void LoadSce()
	{
		StopAllCoroutines();
		percentImg.transform.DOKill();
		float num = Random.Range(1, 3);
		Debug.Log(num);
		StartCoroutine(Loading(num));
	}
}
