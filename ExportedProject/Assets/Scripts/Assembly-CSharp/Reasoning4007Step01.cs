using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4007Step01 : MonoBehaviour
{
	[SerializeField]
	private GameObject step02;

	[SerializeField]
	private GameObject step01;

	[SerializeField]
	private int correct;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private DragFrame datedragframe;

	[SerializeField]
	private DragFrame timedragframe;

	[SerializeField]
	private GameObject img_title2;

	[SerializeField]
	private Toggle toggle01;

	[SerializeField]
	private Toggle toggle02;

	[SerializeField]
	private Toggle toggle03;

	[SerializeField]
	private Toggle toggle04;

	[SerializeField]
	private Button btn_continue2;

	[SerializeField]
	private Text txt_summry;

	[SerializeField]
	private Text txt_maintitle;

	private bool iscanover;

	private void Start()
	{
		txt_maintitle.DOFade(1f, 0.2f);
		btn_continue.gameObject.SetActive(value: true);
		Sequence sequence = DOTween.Sequence();
		sequence.PrependInterval(3f);
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 2f));
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1f, 1f, 1f), 2f));
		sequence.Play().SetLoops(-1);
		btn_continue.onClick.AddListener(Check1);
		btn_continue2.onClick.AddListener(Check2);
	}

	private void Update()
	{
		if (iscanover && Input.anyKey)
		{
			step01.GetComponent<CanvasGroup>().DOFade(0f, 0.3f).OnComplete(delegate
			{
				iscanover = false;
				step02.gameObject.SetActive(value: true);
				step02.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
				step01.gameObject.SetActive(value: false);
				txt_summry.gameObject.SetActive(value: false);
			});
		}
	}

	private void Check1()
	{
		if (!datedragframe.iscandrag || !timedragframe.iscandrag)
		{
			return;
		}
		if (datedragframe.currentpos == 2 && timedragframe.currentpos == 3)
		{
			datedragframe.iscandrag = false;
			timedragframe.iscandrag = false;
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			img_title2.GetComponent<Image>().DOFade(1f, 0.2f);
			img_title2.transform.DOScaleY(1f, 0.2f).OnComplete(delegate
			{
				toggle01.transform.DOScale(Vector3.one, 0.2f);
				toggle02.transform.DOScale(Vector3.one, 0.2f);
				toggle03.transform.DOScale(Vector3.one, 0.2f);
				toggle04.transform.DOScale(Vector3.one, 0.2f);
				toggle01.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
				toggle02.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
				toggle03.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
				toggle04.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
				btn_continue2.gameObject.SetActive(value: true);
				btn_continue2.GetComponent<Image>().DOFade(1f, 0.2f);
			});
		}
		else
		{
			datedragframe.ShowWrong();
			timedragframe.ShowWrong();
		}
	}

	private void Check2()
	{
		if (toggle04.isOn)
		{
			btn_continue2.interactable = false;
			btn_continue2.gameObject.SetActive(value: false);
			toggle01.interactable = false;
			toggle02.interactable = false;
			toggle03.interactable = false;
			toggle04.interactable = false;
			txt_summry.DOText(I18N.instance.getValue("^tuili0444"), 1.5f).OnComplete(delegate
			{
				iscanover = true;
			});
		}
		else if (toggle01.isOn)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(toggle01.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f));
			sequence.Append(toggle01.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f));
			sequence.Play().SetLoops(3);
		}
		else if (toggle02.isOn)
		{
			Sequence sequence2 = DOTween.Sequence();
			sequence2.Append(toggle02.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f));
			sequence2.Append(toggle02.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f));
			sequence2.Play().SetLoops(3);
		}
		else if (toggle03.isOn)
		{
			Sequence sequence3 = DOTween.Sequence();
			sequence3.Append(toggle03.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f));
			sequence3.Append(toggle03.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f));
			sequence3.Play().SetLoops(3);
		}
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
		btn_continue.GetComponent<Image>().DOFade(1f, 0.2f);
	}
}
