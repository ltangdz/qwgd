using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Reasoning4003Step02 : MonoBehaviour
{
	public List<DragLine> dragLines = new List<DragLine>();

	public List<GameObject> reasoningLineResults = new List<GameObject>();

	public List<GameObject> drawreasoningLineResults = new List<GameObject>();

	public List<string> answer = new List<string>();

	public Toggle toggle2;

	public Image img_lineV0;

	public Image img_lineH;

	public GameObject img_dot;

	public GameObject img_dot2;

	public GameObject img_frame;

	public GameObject img_title;

	public Toggle toggle1;

	public Toggle toggle3;

	public Toggle toggle4;

	public GameObject reasoning4003Step01;

	public GameObject img_wenshenframe;

	public GameObject img_title2;

	public GameObject toggleGroup2;

	public Toggle multitoggle1;

	public Toggle multitoggle2;

	public Toggle multitoggle3;

	public Toggle multitoggle4;

	public Toggle multitoggle5;

	public GameObject img_title3;

	public GameObject img_roleblank;

	public List<GameObject> img_rolelist = new List<GameObject>();

	public Text txt_title4;

	public ReasoningMiddle4003 reasoningMiddle;

	public ReasoningPanel reasoningPanel;

	public ScrollRect scrollRect;

	[SerializeField]
	private Image img_wenshenblack;

	[SerializeField]
	private Button btn_sure01;

	[SerializeField]
	private Button btn_sure02;

	[SerializeField]
	private int selectpos;

	public void Gotonext()
	{
		img_dot2.SetActive(value: false);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(img_lineH.DOFillAmount(0f, 0.3f)).Append(img_lineV0.DOFillAmount(0f, 0.3f).SetEase(Ease.InCubic)).AppendCallback(delegate
		{
			img_dot.SetActive(value: false);
			scrollRect.content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 1300f);
			DOTween.To(() => scrollRect.content.localPosition, delegate(Vector3 x)
			{
				scrollRect.content.localPosition = x;
			}, new Vector3(0f, 470f, 0f), 0.29f).OnComplete(delegate
			{
				base.gameObject.SetActive(value: true);
			});
		});
		sequence.Play();
		Sequence sequence2 = DOTween.Sequence();
		sequence2.Join(toggle1.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetEase(Ease.InCubic)).Join(toggle1.transform.DOBlendableLocalMoveBy(new Vector3(0f, 50f, 0f), 0.5f).SetEase(Ease.InCubic)).Join(toggle3.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetEase(Ease.InCubic))
			.Join(toggle3.transform.DOBlendableLocalMoveBy(new Vector3(0f, 50f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Join(toggle4.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetEase(Ease.InCubic))
			.Join(toggle4.transform.DOBlendableLocalMoveBy(new Vector3(0f, 50f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Join(toggle2.transform.DOBlendableLocalMoveBy(new Vector3(0f, 40f, 0f), 0.5f).SetEase(Ease.InCubic));
		sequence2.Play();
		toggle2.interactable = false;
		StartCoroutine(Gonext0());
	}

	private IEnumerator Gonext0()
	{
		img_wenshenframe.SetActive(value: true);
		img_wenshenblack.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(2f);
		Sequence sequence = DOTween.Sequence();
		sequence.AppendInterval(1f);
		sequence.PrependInterval(1f).Prepend(img_wenshenframe.transform.DOScale(Vector3.one, 0.1f)).Join(img_wenshenframe.GetComponent<CanvasGroup>().DOFade(1f, 0.1f))
			.Append(img_wenshenblack.transform.DOScale(Vector3.one, 0.1f))
			.Join(img_wenshenblack.transform.DOLocalMove(new Vector2(-237f, -455f), 0.5f))
			.Join(img_wenshenframe.transform.DOLocalMove(new Vector2(237f, -455f), 0.5f))
			.Append(img_wenshenframe.transform.DORotate(new Vector3(0f, -180f, 0f), 0.5f))
			.Append(img_wenshenblack.DOFade(0.5f, 0.2f))
			.Join(img_wenshenframe.GetComponent<CanvasGroup>().DOFade(0.5f, 0.2f))
			.Join(img_wenshenframe.transform.DOLocalMove(new Vector2(0f, -455f), 4f))
			.Join(img_wenshenblack.transform.DOScale(new Vector3(0.79f, 0.79f, 0.79f), 2f))
			.Join(img_wenshenblack.transform.DOLocalMove(new Vector2(22.6f, -472f), 4f))
			.OnComplete(delegate
			{
				img_wenshenframe.GetComponent<CanvasGroup>().alpha = 1f;
				img_wenshenframe.transform.localRotation = Quaternion.identity;
				img_wenshenframe.transform.GetChild(0).GetComponent<Image>().sprite = img_wenshenblack.sprite;
				img_wenshenframe.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(220f, 220f);
				img_wenshenframe.transform.GetChild(0).GetComponent<RectTransform>().localPosition = Vector3.zero;
				img_wenshenblack.gameObject.SetActive(value: false);
				Gotonext1();
			});
		sequence.Play();
	}

	public void Gotonext1()
	{
		img_title2.SetActive(value: true);
		img_title2.transform.DOScaleY(1f, 0.3f);
		toggleGroup2.SetActive(value: true);
		Sequence sequence = DOTween.Sequence();
		sequence.PrependInterval(5f).Join(multitoggle1.GetComponent<CanvasGroup>().DOFade(1f, 0.3f)).Join(multitoggle1.transform.DOBlendableLocalMoveBy(new Vector3(0f, -15f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Join(multitoggle2.GetComponent<CanvasGroup>().DOFade(1f, 0.3f))
			.Join(multitoggle2.transform.DOBlendableLocalMoveBy(new Vector3(0f, -15f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Join(multitoggle3.GetComponent<CanvasGroup>().DOFade(1f, 0.3f))
			.Join(multitoggle3.transform.DOBlendableLocalMoveBy(new Vector3(0f, -15f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Join(multitoggle4.GetComponent<CanvasGroup>().DOFade(1f, 0.3f))
			.Join(multitoggle4.transform.DOBlendableLocalMoveBy(new Vector3(0f, -15f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Join(multitoggle5.GetComponent<CanvasGroup>().DOFade(1f, 0.3f))
			.Join(multitoggle5.transform.DOBlendableLocalMoveBy(new Vector3(0f, -15f, 0f), 0.5f).SetEase(Ease.InCubic).OnComplete(delegate
			{
				selectpos = 1;
				btn_sure02.gameObject.SetActive(value: true);
				Sequence sequence2 = DOTween.Sequence();
				sequence2.Append(btn_sure02.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 1f));
				sequence2.Append(btn_sure02.transform.DOScale(new Vector3(1f, 1f, 1f), 1f));
				sequence2.Play().SetLoops(-1);
			}));
		sequence.Play();
	}

	public void Gotonext2()
	{
		scrollRect.content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 1700f);
		Sequence sequence = DOTween.Sequence();
		sequence.Join(multitoggle1.GetComponent<CanvasGroup>().DOFade(0f, 0.3f)).Join(multitoggle1.transform.DOBlendableLocalMoveBy(new Vector3(0f, 15f, 0f), 0.5f).SetEase(Ease.InCubic)).Join(multitoggle5.GetComponent<CanvasGroup>().DOFade(0f, 0.3f))
			.Join(multitoggle5.transform.DOBlendableLocalMoveBy(new Vector3(0f, 15f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Join(multitoggle3.transform.DOBlendableLocalMoveBy(new Vector3(0f, 50f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Join(multitoggle4.transform.DOBlendableLocalMoveBy(new Vector3(0f, 50f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Join(multitoggle2.transform.DOBlendableLocalMoveBy(new Vector3(0f, 50f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Join(DOTween.To(() => scrollRect.content.localPosition, delegate(Vector3 x)
			{
				scrollRect.content.localPosition = x;
			}, new Vector3(0f, 870f, 0f), 0.29f).OnComplete(delegate
			{
				base.gameObject.SetActive(value: true);
			}));
		sequence.Play();
		img_title3.SetActive(value: true);
		img_roleblank.SetActive(value: true);
		img_title3.transform.DOScaleY(1f, 0.3f);
		img_roleblank.transform.DOScale(Vector3.one, 0.3f);
		for (int num = 0; num < img_rolelist.Count; num++)
		{
			img_rolelist[num].SetActive(value: true);
			img_rolelist[num].transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.3f);
			img_rolelist[num].GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		}
	}

	public void RemoveAni()
	{
		GetComponent<Animator>().enabled = false;
		Object.Destroy(GetComponent<Animator>());
		for (int i = 0; i < dragLines.Count; i++)
		{
			dragLines[i].GetComponent<Image>().raycastTarget = false;
			dragLines[i].GetComponent<DragLine>().enabled = false;
		}
		btn_sure01.gameObject.SetActive(value: true);
		btn_sure01.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(btn_sure01.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 1f));
		sequence.Append(btn_sure01.transform.DOScale(new Vector3(1f, 1f, 1f), 1f));
		sequence.Play().SetLoops(-1);
		selectpos = 0;
	}

	public void ClearDrawLine(int num)
	{
		if (num > -1)
		{
			dragLines[num].ShowDot(isshow: false);
		}
	}

	public void ClearDrawLine(int start, int end)
	{
		if (start > -1)
		{
			Debug.Log("namestart:" + dragLines[start].name);
			dragLines[start].ShowDot(isshow: false);
		}
		if (end > -1)
		{
			Debug.Log("nameend:" + dragLines[end].name);
			dragLines[end].ShowDot(isshow: false);
		}
	}

	public void IsAllRight()
	{
		int num = 0;
		for (int i = 0; i < drawreasoningLineResults.Count; i++)
		{
			string value = drawreasoningLineResults[i].GetComponent<ReasoningLineResult>().startavatarname + ";" + drawreasoningLineResults[i].GetComponent<ReasoningLineResult>().endavatarname;
			for (int j = 0; j < answer.Count; j++)
			{
				if (answer[j].Contains(value))
				{
					num++;
					break;
				}
			}
		}
		if (num == 3)
		{
			GetComponent<Animator>().enabled = true;
		}
		else if (drawreasoningLineResults.Count == 3)
		{
			StartCoroutine(ShowLineWrong());
		}
	}

	private IEnumerator ShowLineWrong()
	{
		for (int i = 0; i < drawreasoningLineResults.Count; i++)
		{
			drawreasoningLineResults[i].GetComponent<UILineRenderer>().DOColor(new Color32(189, 55, 63, byte.MaxValue), 0.3f).SetLoops(3);
		}
		for (int j = 0; j < dragLines.Count; j++)
		{
			dragLines[j].ShowWrong();
		}
		yield return new WaitForSeconds(1.5f);
		for (int k = 0; k < drawreasoningLineResults.Count; k++)
		{
			drawreasoningLineResults[k].GetComponent<UILineRenderer>().DOColor(new Color32(109, 123, 150, byte.MaxValue), 0.3f);
			List<Vector2> list = new List<Vector2>();
			drawreasoningLineResults[k].GetComponent<UILineRenderer>().Points = list.ToArray();
			drawreasoningLineResults[k].GetComponent<ReasoningLineResult>().ClearLine0();
		}
		for (int l = 0; l < drawreasoningLineResults.Count; l++)
		{
			reasoningLineResults.Add(drawreasoningLineResults[l]);
		}
		for (int m = 0; m < dragLines.Count; m++)
		{
			dragLines[m].ShowDot(isshow: false);
		}
		drawreasoningLineResults.Clear();
	}

	public void ChangeToggleValue()
	{
		Debug.Log("google:" + toggle2.isOn);
		if (toggle2.isOn)
		{
			btn_sure01.gameObject.SetActive(value: false);
			Gotonext();
		}
		else
		{
			StartCoroutine(StartToggleWrong());
		}
	}

	public void ChangeMultiToggle()
	{
		if (multitoggle1.isOn || multitoggle5.isOn)
		{
			StartCoroutine(StartMultiToggleWrong());
		}
		else if (!multitoggle1.isOn && multitoggle2.isOn && multitoggle3.isOn && multitoggle4.isOn && !multitoggle5.isOn)
		{
			multitoggle1.gameObject.SetActive(value: false);
			multitoggle5.gameObject.SetActive(value: false);
			btn_sure02.gameObject.SetActive(value: false);
			Gotonext2();
		}
		else
		{
			StartCoroutine(StartMultiToggleWrong());
		}
	}

	private IEnumerator StartMultiToggleWrong()
	{
		reasoningPanel.gameManager.homeScene.eventsystem.SetActive(value: false);
		if (multitoggle1.isOn)
		{
			multitoggle1.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		if (multitoggle2.isOn)
		{
			multitoggle2.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		if (multitoggle3.isOn)
		{
			multitoggle3.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		if (multitoggle4.isOn)
		{
			multitoggle4.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		if (multitoggle5.isOn)
		{
			multitoggle5.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		yield return new WaitForSeconds(1f);
		multitoggle1.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		multitoggle2.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		multitoggle3.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		multitoggle4.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		multitoggle5.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		multitoggle1.isOn = false;
		multitoggle2.isOn = false;
		multitoggle3.isOn = false;
		multitoggle4.isOn = false;
		multitoggle5.isOn = false;
		reasoningPanel.gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	private IEnumerator StartToggleWrong()
	{
		reasoningPanel.gameManager.homeScene.eventsystem.SetActive(value: false);
		if (toggle1.isOn)
		{
			toggle1.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		if (toggle2.isOn)
		{
			toggle2.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		if (toggle3.isOn)
		{
			toggle3.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		if (toggle4.isOn)
		{
			toggle4.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		yield return new WaitForSeconds(1f);
		toggle1.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		toggle2.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		toggle3.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		toggle4.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		toggle1.isOn = false;
		toggle2.isOn = false;
		toggle3.isOn = false;
		toggle4.isOn = false;
		reasoningPanel.gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	public void LastStep()
	{
		txt_title4.gameObject.SetActive(value: true);
		txt_title4.DOText(I18N.instance.getValue("^tuili0427"), 2f);
		for (int i = 0; i < img_rolelist.Count; i++)
		{
			img_rolelist[i].GetComponent<ReasoningDragRole>().ResetRole();
			img_rolelist[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
			img_rolelist[i].GetComponent<CanvasGroup>().interactable = false;
			img_rolelist[i].GetComponent<ReasoningDragRole>().enabled = false;
			if (i != 0)
			{
				Sequence sequence = DOTween.Sequence();
				sequence.Join(img_rolelist[i].GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetEase(Ease.InCubic)).Join(img_rolelist[i].transform.DOBlendableLocalMoveBy(new Vector3(0f, 50f, 0f), 0.5f).SetEase(Ease.InCubic));
				sequence.Play();
			}
			else
			{
				img_rolelist[i].transform.DOScale(Vector3.one, 0.2f);
				img_rolelist[i].transform.DOLocalMoveX(0f, 0.5f);
			}
		}
		reasoningMiddle.isallright = true;
		Invoke("Over", 3f);
	}

	private void Over()
	{
		reasoningPanel.GetResult();
	}

	private void Start()
	{
		btn_sure01.onClick.AddListener(delegate
		{
			ChangeToggleValue();
		});
		btn_sure02.onClick.AddListener(delegate
		{
			ChangeMultiToggle();
		});
	}
}
