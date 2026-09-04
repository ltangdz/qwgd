using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyProgress : MonoBehaviour
{
	public List<Sprite> progressSprites;

	public Image bgImage;

	public Image progressImage;

	public Image haloImage;

	public Text progressText;

	private int _count;

	private void Start()
	{
		Normal();
	}

	private void Hit()
	{
		if (_count < 3)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.SetId("EnemyProgressHaloSeq");
			ColorUtility.TryParseHtmlString("#F13536", out var color);
			progressText.color = color;
			int[] array = new int[3] { 20, 75, 100 };
			DOTween.To(delegate(float value)
			{
				progressText.text = Mathf.Floor(value) + "%";
			}, 0f, array[_count], 1f);
			progressImage.DOFillAmount((float)array[_count] / 100f, 1f).OnComplete(delegate
			{
			});
			_count++;
			sequence.Append(haloImage.DOFade(0.3f, 0.5f));
			sequence.Append(haloImage.DOFade(1f, 0.5f));
			sequence.SetLoops(-1);
			progressImage.sprite = progressSprites[1];
			bgImage.sprite = progressSprites[0];
			sequence.Play();
		}
	}

	private void HitFinished()
	{
		DOTween.Kill("EnemyProgressHaloSeq");
		if (_count < 3)
		{
			Normal();
		}
	}

	private void Normal()
	{
		progressText.color = Color.white;
		haloImage.DOFade(0f, 0f);
		bgImage.sprite = progressSprites[2];
		progressImage.sprite = progressSprites[3];
	}

	private void OnEnable()
	{
		CatchEvent.Instance.onNoticeNextEvent += NoticeNextEvent;
	}

	private void OnDisable()
	{
		CatchEvent.Instance.onNoticeNextEvent -= NoticeNextEvent;
	}

	private void NoticeNextEvent(CatchEventEnum obj)
	{
		if (obj == CatchEventEnum.CATCH_HIT)
		{
			Hit();
		}
		if (obj == CatchEventEnum.CATCH_HIT_FINISHED)
		{
			HitFinished();
		}
	}
}
