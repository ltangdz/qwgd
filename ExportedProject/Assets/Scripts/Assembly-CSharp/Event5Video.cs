using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Event5Video : MonoBehaviour
{
	public GameObject black;

	public GameObject page01;

	public GameObject page02;

	public GameObject page03;

	public GameObject page04;

	private GameManager gameManager;

	[SerializeField]
	private RectTransform img_leg;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
		black.GetComponent<Image>().DOFade(0f, 2f).SetEase(Ease.Linear)
			.OnComplete(delegate
			{
				StartCoroutine(StartAni());
			});
	}

	private IEnumerator StartAni()
	{
		gameManager.musicManager.Stop();
		yield return new WaitForSeconds(2f);
		gameManager.soundManager.PlaySound(40);
		yield return new WaitForSeconds(1f);
		StartCoroutine(ChangeToPage(page01, page02));
		yield return new WaitForSeconds(6f);
		StartCoroutine(ChangeToPage(page02, page03));
		yield return new WaitForSeconds(5f);
		StartCoroutine(ChangeToPage(page03, page04));
		Sequence sequence = DOTween.Sequence();
		sequence.AppendInterval(1f);
		sequence.Append(img_leg.DOLocalMove(new Vector3(-107f, 135f, 0f), 3f));
		sequence.Append(img_leg.DOLocalMove(new Vector3(-516f, 1012f, 0f), 0.5f).SetEase(Ease.InQuint));
		sequence.Play();
		yield return new WaitForSeconds(5f);
		black.SetActive(value: true);
		black.GetComponent<Image>().DOFade(1f, 2.5f);
		gameManager.CanShowSetting(-1);
	}

	public void ChangePage1()
	{
		StartCoroutine(ChangeToPage(page01, page02));
	}

	public void ChangePage2()
	{
		StartCoroutine(ChangeToPage(page02, page03));
	}

	private IEnumerator ChangeToPage(GameObject hideObj, GameObject showObj)
	{
		black.GetComponent<Image>().DOFade(1f, 2f);
		yield return new WaitForSeconds(2f);
		hideObj.SetActive(value: false);
		showObj.SetActive(value: true);
		black.GetComponent<Image>().DOFade(0f, 2f);
		yield return new WaitForSeconds(2f);
		showObj.GetComponent<Animator>().enabled = true;
	}
}
