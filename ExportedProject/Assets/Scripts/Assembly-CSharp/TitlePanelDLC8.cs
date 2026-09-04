using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TitlePanelDLC8 : MonoBehaviour
{
	public Text txt_title;

	private GameManager gameManager;

	public Image img_mouse;

	private bool iscanclick;

	private bool startgo = true;

	private UnityAction _callback;

	private IEnumerator SetContent(string str_title)
	{
		gameManager.CanShowSetting(1);
		yield return new WaitForSeconds(2f);
		txt_title.GetComponent<TypewriterEffect>().StartSlowEffect(I18N.instance.getValue(str_title), 0.4f, issound: true);
		yield return new WaitForSeconds((float)I18N.instance.getValue(str_title).Length * 0.4f + 0.2f);
		iscanclick = true;
		img_mouse.gameObject.SetActive(value: true);
	}

	public void Show(string title, UnityAction callback)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		base.gameObject.SetActive(value: true);
		_callback = callback;
		StartCoroutine(SetContent(title));
	}

	private void Update()
	{
		if (Input.anyKey && iscanclick)
		{
			Go();
		}
	}

	public void Go()
	{
		if (startgo)
		{
			startgo = false;
			gameManager.CanShowSetting(-1);
			_callback?.Invoke();
			img_mouse.gameObject.SetActive(value: false);
		}
	}

	public void Hide()
	{
		GetComponent<CanvasGroup>().DOFade(0f, 1f).SetEase(Ease.Linear).OnComplete(delegate
		{
			base.gameObject.SetActive(value: false);
		});
	}
}
