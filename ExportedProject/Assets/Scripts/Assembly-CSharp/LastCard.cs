using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastCard : MonoBehaviour
{
	public Transform page5;

	private GameManager gameManager;

	public void Bigger()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Append(page5.DOScale(new Vector3(3f, 3f, 3f), 4f).SetEase(Ease.InCubic));
		sequence.OnComplete(delegate
		{
			StartCoroutine(Black());
		});
		sequence.Play();
	}

	private IEnumerator Black()
	{
		yield return new WaitForSeconds(4f);
		gameManager.ShowFloatBox();
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene("mainScene");
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
}
