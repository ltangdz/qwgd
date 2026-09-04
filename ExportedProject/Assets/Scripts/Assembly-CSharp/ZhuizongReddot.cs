using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ZhuizongReddot : MonoBehaviour
{
	[SerializeField]
	private Image img_question;

	[SerializeField]
	private Image img_wave1;

	[SerializeField]
	private Image img_wave2;

	private void Start()
	{
		StartCoroutine(ShowWave());
	}

	private IEnumerator ShowWave()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Append(img_question.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f).SetEase(Ease.Linear));
		sequence.Append(img_question.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetEase(Ease.Linear));
		sequence.Play().SetLoops(-1);
		Sequence sequence2 = DOTween.Sequence();
		sequence2.Join(img_wave1.transform.DOScale(new Vector3(1.6f, 1.6f, 1.6f), 5f).SetEase(Ease.Linear));
		sequence2.Join(img_wave1.DOFade(0f, 5f).SetEase(Ease.Linear));
		sequence2.Play().SetLoops(-1);
		yield return new WaitForSeconds(2.5f);
		Sequence sequence3 = DOTween.Sequence();
		sequence3.Join(img_wave2.transform.DOScale(new Vector3(1.6f, 1.6f, 1.6f), 5f).SetEase(Ease.Linear));
		sequence3.Join(img_wave2.DOFade(0f, 5f).SetEase(Ease.Linear));
		sequence3.Play().SetLoops(-1);
	}

	private void OnEnable()
	{
		StartCoroutine(ShowWave());
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}
}
