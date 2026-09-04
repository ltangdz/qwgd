using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadeOpenLock : MonoBehaviour
{
	public GameObject bk;

	public Image avatar;

	public Text userName;

	public GameObject suokai;

	public GameObject btnOpen;

	public GameObject btnOpenIcon;

	public GameObject suoLight;

	public GameObject txtSuo;

	private void Start()
	{
		InvokeRepeating("Light", 2f, 3f);
	}

	public void Init(Sprite avatarUrl, string txtName)
	{
		avatar.sprite = avatarUrl;
		userName.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue(txtName) + I18N.instance.getValue("^w_phone"));
	}

	private void Light()
	{
		suoLight.GetComponent<RectTransform>().localPosition = new Vector3(-70f, 0f, 0f);
		suoLight.GetComponent<RectTransform>().DOLocalMoveX(70f, 0.8f);
	}

	public void OpenSce()
	{
		btnOpen.SetActive(value: false);
		btnOpenIcon.SetActive(value: false);
		suokai.SetActive(value: true);
		StartCoroutine(Open());
	}

	private IEnumerator Open()
	{
		yield return new WaitForSeconds(1f);
		bk.GetComponent<RectTransform>().DOLocalMoveY(560f, 0.3f);
		bk.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		Object.Destroy(base.gameObject);
	}
}
