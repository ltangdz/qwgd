using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Event1Video : MonoBehaviour
{
	public GameObject blackFloat;

	public GameObject whiteFloat;

	public GameObject scene01;

	public GameObject scene02;

	public GameObject scene01Drop1;

	public GameObject scene01Drop2;

	public GameObject scene02Drop;

	public GameObject photoDrop;

	public GameObject photoPore;

	public GameObject modi;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
		StartCoroutine(Run());
	}

	private IEnumerator Run()
	{
		gameManager.musicManager.Stop();
		blackFloat.GetComponent<Image>().DOFade(0f, 2f);
		yield return new WaitForSeconds(3f);
		gameManager.musicManager.PlayAnimationSound(0);
		scene01Drop1.GetComponent<RectTransform>().DOLocalMoveX(-325f, 0.3f);
		scene01Drop2.GetComponent<RectTransform>().DOLocalMoveX(-325f, 0.3f);
		yield return new WaitForSeconds(2f);
		scene01Drop1.GetComponent<Image>().DOFade(0f, 1.2f).SetEase(Ease.Linear);
		yield return new WaitForSeconds(0.4f);
		scene01Drop2.GetComponent<Image>().DOFade(1f, 1.2f).SetEase(Ease.Linear);
		yield return new WaitForSeconds(4f);
		blackFloat.GetComponent<Image>().DOFade(1f, 2f);
		yield return new WaitForSeconds(2f);
		scene01.SetActive(value: false);
		scene02.SetActive(value: true);
		blackFloat.GetComponent<Image>().DOFade(0f, 2f);
		yield return new WaitForSeconds(2f);
		scene02Drop.GetComponent<RectTransform>().DOScale(new Vector3(1f, 1f, 1f), 0.5f);
		scene02Drop.GetComponent<Image>().DOFade(1f, 0.5f);
		yield return new WaitForSeconds(1f);
		photoDrop.GetComponent<RectTransform>().DOLocalMoveY(-211f, 5f);
		yield return new WaitForSeconds(2f);
		whiteFloat.GetComponent<Image>().DOFade(1f, 1f);
		yield return new WaitForSeconds(1.5f);
		photoDrop.SetActive(value: false);
		photoPore.SetActive(value: true);
		whiteFloat.GetComponent<Image>().DOFade(0f, 2f);
		yield return new WaitForSeconds(1f);
		modi.GetComponent<Image>().DOFade(1f, 1f);
		yield return new WaitForSeconds(0.8f);
		modi.GetComponent<RectTransform>().DOLocalMoveY(-600f, 2f).SetEase(Ease.Linear);
	}
}
