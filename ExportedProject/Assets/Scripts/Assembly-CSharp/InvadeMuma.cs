using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadeMuma : MonoBehaviour
{
	public Image jindu;

	public Button sureBtn;

	public Text title;

	private void Start()
	{
		StartCoroutine(Run());
		sureBtn.onClick.AddListener(delegate
		{
			StartCoroutine(Close());
		});
	}

	private IEnumerator Run()
	{
		GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
		GetComponent<RectTransform>().DOScale(new Vector3(1f, 1f, 1f), 0.2f);
		yield return new WaitForSeconds(0.2f);
		jindu.DOFillAmount(1f, 2f);
		if (GameObject.Find("GameManager").GetComponent<GameManager>().GameType == GameTypeEnum.BASIC)
		{
			yield return new WaitForSeconds(2f);
			title.GetComponent<I18NText>().updateTranslation2("^invade_mumalabel02");
		}
		sureBtn.interactable = true;
	}

	private IEnumerator Close()
	{
		GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
		GetComponent<RectTransform>().DOScale(new Vector3(0f, 0f, 0f), 0.5f);
		yield return new WaitForSeconds(0.2f);
		Object.Destroy(base.gameObject);
	}
}
