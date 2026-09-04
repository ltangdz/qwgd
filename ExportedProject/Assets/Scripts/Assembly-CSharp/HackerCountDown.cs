using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class HackerCountDown : MonoBehaviour
{
	[SerializeField]
	private Text txt_countdown;

	[SerializeField]
	private Text txt_title;

	[SerializeField]
	private Text txt_content;

	[SerializeField]
	private Image img_filled;

	[SerializeField]
	private Image img_slider;

	[SerializeField]
	private Image img_bk;

	[SerializeField]
	private int countdown;

	private bool isstop;

	[SerializeField]
	private List<Sprite> sprites = new List<Sprite>();

	private GameManager gameManager;

	private Sequence sq;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		base.transform.DOScale(Vector3.one, 0.2f);
		GetComponent<CanvasGroup>().DOFade(1f, 0.2f).OnComplete(delegate
		{
			img_filled.DOFillAmount(0f, countdown).SetEase(Ease.Linear).OnUpdate(delegate
			{
				if (countdown == 30)
				{
					sq = DOTween.Sequence();
					sq.Append(txt_countdown.DOFade(0.2f, 0.2f));
					sq.Append(txt_countdown.DOFade(1f, 0.2f));
					sq.Play().SetLoops(-1);
				}
				if (!isstop)
				{
					txt_countdown.text = (countdown / 60).ToString("00") + ":" + (countdown % 60).ToString("00");
				}
			})
				.OnComplete(delegate
				{
					StartCoroutine(GameOver());
					Debug.Log("时间到");
					gameManager.homeScene.hackerBk.Stop();
				});
			DOTween.To(() => countdown, delegate(int x)
			{
				countdown = x;
			}, 0, countdown).SetEase(Ease.Linear);
		});
	}

	private IEnumerator GameOver()
	{
		yield return new WaitForSeconds(0.3f);
		gameManager.homeScene.cameraFilterPack_fx_Glitch1.enabled = true;
		yield return new WaitForSeconds(0.3f);
		gameManager.homeScene.cameraFilterPack_fx_Glitch1.enabled = false;
		yield return new WaitForSeconds(0.3f);
		gameManager.homeScene.cameraFilterPack_fx_Glitch1.enabled = true;
		yield return new WaitForSeconds(1f);
		gameManager.homeScene.cameraFilterPack_fx_Glitch1.enabled = false;
		Object.Instantiate(Resources.Load("Dialog/Hacker/hackgameover") as GameObject, base.transform.parent);
	}

	public void Hide()
	{
		GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
	}

	public void Show()
	{
		GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
	}

	public void DestroyObject()
	{
		base.transform.DOScale(Vector3.zero, 0.2f);
		GetComponent<CanvasGroup>().DOFade(0f, 0.2f).OnComplete(delegate
		{
			Object.Destroy(base.gameObject);
		});
	}

	public void StopTime()
	{
		txt_title.GetComponent<I18NText>().updateTranslation2("^hacker28");
		img_filled.DOKill();
		isstop = true;
		sq.Kill();
		gameManager.homeScene.hackerBk.Stop();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(GetComponent<CanvasGroup>().DOFade(0.3f, 0.2f));
		sequence.Append(GetComponent<CanvasGroup>().DOFade(1f, 0.2f));
		sequence.Play().SetLoops(3).OnComplete(delegate
		{
			txt_countdown.color = Color.white;
			txt_title.color = Color.white;
			txt_content.color = Color.white;
			img_bk.sprite = sprites[0];
			img_slider.sprite = sprites[1];
			img_filled.sprite = sprites[2];
		});
	}
}
