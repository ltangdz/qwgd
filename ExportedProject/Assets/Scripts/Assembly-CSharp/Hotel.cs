using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Hotel : MonoBehaviour
{
	public MultiplyText txt_managername;

	public MultiplyText txt_managerphone;

	public GameManager gameManager;

	public Button btnOrder;

	public GameObject hotelAlert;

	public Button closeAlert;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		txt_managername.SetContent2("^jiudian_label04", "10209", I18N.instance.getValue("^message_event0226"));
		txt_managerphone.SetContent2("^jiudian_label06", "10240", I18N.instance.getValue("^message_event0351"));
		btnOrder.onClick.AddListener(ShowAlert);
		closeAlert.onClick.AddListener(delegate
		{
			StartCoroutine(HideAlert());
		});
	}

	private void ShowAlert()
	{
		GetComponent<ScrollRect>().enabled = false;
		hotelAlert.SetActive(value: true);
		hotelAlert.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
	}

	private IEnumerator HideAlert()
	{
		GetComponent<ScrollRect>().enabled = true;
		hotelAlert.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		hotelAlert.SetActive(value: false);
	}

	private IEnumerator StartShowVideo()
	{
		yield return new WaitForSeconds(2f);
		gameManager.homeScene.ShowVideoTip("3700021");
	}
}
