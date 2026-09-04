using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanel : MonoBehaviour
{
	public int id;

	public Text txt_answer;

	public Text txt_date;

	[SerializeField]
	private Image img_masaike;

	[SerializeField]
	private Image img_circle;

	[SerializeField]
	private Image img_line;

	[SerializeField]
	private List<Sprite> sprites = new List<Sprite>();

	[SerializeField]
	private QuestionPanel nextquestionpanel;

	[SerializeField]
	private ScrollRect scrollRect;

	[SerializeField]
	private Image img_blank;

	public bool isneedinit;

	public GameObject laststep;

	public bool isok;

	public bool iscanclick;

	private void Start()
	{
		if (isneedinit)
		{
			Init();
		}
	}

	public void Init()
	{
		StartCoroutine(StartAni());
	}

	private IEnumerator StartAni()
	{
		img_line.GetComponent<RectTransform>().DOSizeDelta(new Vector2(2f, 88f), 0.8f);
		yield return new WaitForSeconds(0.8f);
		img_circle.color = Color.white;
		txt_date.gameObject.SetActive(value: true);
		img_masaike.DOFade(1f, 1f);
		yield return new WaitForSeconds(1f);
		iscanclick = true;
	}

	public void Right()
	{
		isok = true;
		img_masaike.DOFade(0f, 1f).OnComplete(delegate
		{
			txt_answer.gameObject.SetActive(value: true);
			txt_answer.DOFade(1f, 0.3f);
			if (nextquestionpanel != null)
			{
				nextquestionpanel.gameObject.SetActive(value: true);
				nextquestionpanel.Init();
				DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
				{
					scrollRect.normalizedPosition = x;
				}, Vector2.zero, 1f);
			}
			else
			{
				if (img_blank != null)
				{
					img_blank.gameObject.SetActive(value: false);
				}
				if (laststep != null)
				{
					laststep.SetActive(value: true);
				}
				DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
				{
					scrollRect.normalizedPosition = x;
				}, Vector2.zero, 1f);
			}
		});
	}

	public void Wrong()
	{
		StartCoroutine(StartRed());
	}

	private IEnumerator StartRed()
	{
		img_masaike.sprite = sprites[1];
		yield return new WaitForSeconds(0.2f);
		Sequence s = DOTween.Sequence();
		s.Append(img_masaike.DOFade(0.2f, 0.2f));
		s.Append(img_masaike.DOFade(1f, 0.2f));
		s.Append(img_masaike.DOFade(0.2f, 0.2f));
		s.Append(img_masaike.DOFade(1f, 0.2f));
		yield return new WaitForSeconds(0.8f);
		img_masaike.sprite = sprites[0];
	}
}
