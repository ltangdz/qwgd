using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InvadePhoneRecord : MonoBehaviour
{
	public Image bk;

	public Button btnClose;

	public int soundID;

	private GameManager gameManager;

	public void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		float time = gameManager.soundManager.PlayEventFinished(gameManager.player.GetEventId(), soundID);
		gameManager.musicManager.LowerVol();
		StopAllCoroutines();
		StartCoroutine(MusicResum(time));
		bk.GetComponent<RectTransform>().DOScale(new Vector3(1f, 1f, 1f), 0.3f);
		bk.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		btnClose.onClick.AddListener(delegate
		{
			bk.GetComponent<RectTransform>().DOScale(new Vector3(0f, 0f, 0f), 0.3f);
			gameManager.musicManager.ResumeVol();
			gameManager.soundManager.Stop();
			bk.GetComponent<CanvasGroup>().DOFade(0f, 0.3f).OnComplete(delegate
			{
				Object.Destroy(base.gameObject);
			});
		});
	}

	private IEnumerator MusicResum(float time)
	{
		yield return new WaitForSeconds(time);
		gameManager.musicManager.ResumeVol();
	}
}
