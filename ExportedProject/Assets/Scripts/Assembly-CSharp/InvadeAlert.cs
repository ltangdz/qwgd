using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadeAlert : MonoBehaviour
{
	public Text info;

	public Button btnOk;

	private GameObject parObj;

	private GameManager gameManager;

	public void Init(string label, GameObject obj = null)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		parObj = obj;
		gameManager.homeScene.eventsystem.SetActive(value: true);
		info.GetComponent<I18NText>().updateTranslation2(label);
		GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
		btnOk.onClick.AddListener(CloseBox);
	}

	public void CloseBox()
	{
		btnOk.interactable = false;
		GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
		StartCoroutine(HideBox());
	}

	private IEnumerator HideBox()
	{
		yield return new WaitForSeconds(0.3f);
		Object.Destroy(base.gameObject);
		if (parObj != null)
		{
			parObj.SetActive(value: false);
		}
	}
}
