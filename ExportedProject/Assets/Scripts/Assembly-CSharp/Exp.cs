using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Exp : MonoBehaviour
{
	public List<Sprite> expImg;

	public GameObject expImgParent;

	public MissionResult miss;

	public Text attentionVal;

	private float crtVal;

	private void Start()
	{
		StartCoroutine(ShowExp());
		StartCoroutine(UpHotVal());
		crtVal = float.Parse(attentionVal.text);
	}

	public void Replay()
	{
		StartCoroutine(ShowExp());
		attentionVal.GetComponent<I18NText>().updateTranslation2(crtVal.ToString());
		StartCoroutine(UpHotVal());
	}

	private IEnumerator UpHotVal()
	{
		while (miss.Play)
		{
			float addVal = Random.Range(5, 15);
			yield return new WaitForSeconds(0.1f);
			attentionVal.GetComponent<I18NText>().updateTranslation2((float.Parse(attentionVal.text) + addVal).ToString());
		}
		attentionVal.GetComponent<I18NText>().updateTranslation2((crtVal + 4000f).ToString());
	}

	private IEnumerator ShowExp()
	{
		while (miss.Play)
		{
			float seconds = Random.Range(8f, 1.8f) * 0.1f;
			yield return new WaitForSeconds(seconds);
			GameObject obj = Resources.Load<GameObject>("News/exp_img");
			int index = Random.Range(0, expImg.Count);
			obj.GetComponent<Image>().sprite = expImg[index];
			int num = Random.Range(5, 70);
			GameObject gameObject = Object.Instantiate(obj, expImgParent.transform);
			gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(num, 0f, 0f);
			gameObject.transform.DOLocalMoveY(100f, 5f);
			StartCoroutine(HideExp(gameObject.gameObject));
		}
	}

	private IEnumerator HideExp(GameObject expObj)
	{
		float a = expObj.GetComponent<CanvasGroup>().alpha;
		while ((double)a > 0.001)
		{
			a -= 0.01f;
			expObj.GetComponent<CanvasGroup>().alpha = a;
			yield return new WaitForSeconds(0.02f);
		}
		Object.Destroy(expObj);
	}
}
