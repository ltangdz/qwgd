using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadingPanelDLC8 : MonoBehaviour
{
	public Image img_loading;

	public Text txt_loading;

	public Text txt_username;

	private string dotstring = "......";

	private GameManager gameManager;

	private string username;

	private UnityAction _callback;

	private bool _isStartLoading;

	private int pos = 5;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void StartLoading(string name, UnityAction callback)
	{
		if (!_isStartLoading)
		{
			base.gameObject.SetActive(value: true);
			_isStartLoading = true;
			_callback = callback;
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			username = name;
			txt_username.GetComponent<I18NText>().updateTranslation2(username);
			InvokeRepeating("StartLoadingTextAni", 0.1f, 0.5f);
			StartCoroutine(StartLogin());
			StartLoadingAni();
		}
	}

	private IEnumerator StartLogin()
	{
		yield return new WaitForSeconds(3f);
		gameManager.musicManager.Stop();
		gameManager.ShowFloatBox();
		yield return new WaitForSeconds(2f);
		base.gameObject.SetActive(value: false);
		gameManager.txt_studio.SetActive(value: false);
		_isStartLoading = false;
		_callback?.Invoke();
	}

	private void StartLoadingAni()
	{
		img_loading.transform.DOLocalRotate(new Vector3(0f, 720f, 0f), 5f, RotateMode.LocalAxisAdd).SetLoops(-1);
	}

	private void StartLoadingTextAni()
	{
		txt_loading.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue("^loading_01"), username) + dotstring.Substring(pos));
		pos--;
		if (pos == 0)
		{
			pos = 5;
		}
	}
}
