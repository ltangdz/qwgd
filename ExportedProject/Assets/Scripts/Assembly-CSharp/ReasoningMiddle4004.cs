using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ReasoningMiddle4004 : ReasoningMiddle
{
	public bool isallright;

	public Image img_code01;

	public GameObject img_title01;

	public GameObject img_title02;

	public Image img_code02;

	public Image img_code03;

	public List<GameObject> inputbox = new List<GameObject>();

	public Toggle toggle0_1;

	public Toggle toggle0_2;

	public Toggle toggle0_3;

	public Toggle toggle0_4;

	public GameObject toggleGroup01;

	public Toggle toggle1_1;

	public Toggle toggle1_2;

	public Toggle toggle1_3;

	public Toggle toggle1;

	public Toggle toggle2;

	public Toggle toggle3;

	public Toggle toggle4;

	public bool iscaninput;

	public ReasoningPanel reasoningPanel;

	[SerializeField]
	private ScrollRect scrollRect;

	[SerializeField]
	private GameObject img_title0;

	[SerializeField]
	private GameObject img_title1;

	[SerializeField]
	private GameObject img_codetitle1;

	[SerializeField]
	private GameObject img_codetitle2;

	[SerializeField]
	private GameObject img_title03;

	[SerializeField]
	private Button btn_select01;

	[SerializeField]
	private Button btn_select02;

	[SerializeField]
	private Button btn_select03;

	private string codes = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

	public string result = "";

	public string answer = "WKMOGTCE";

	public Sprite[] sprites;

	private List<LetterItem> letterItems = new List<LetterItem>();

	[SerializeField]
	private Transform btn_reset;

	[SerializeField]
	private List<LetterItem> allletterItems = new List<LetterItem>();

	private bool iscanclick = true;

	public Text txt_code;

	private void Start()
	{
		btn_select01.onClick.AddListener(delegate
		{
			ChangeValue0();
		});
		btn_select02.onClick.AddListener(delegate
		{
			ChangeValue1();
		});
		btn_select03.onClick.AddListener(delegate
		{
			ChangeValue();
		});
		Init0();
	}

	private void Init0()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Join(img_title0.transform.DOScaleY(1f, 0.3f)).Join(toggle0_1.GetComponent<CanvasGroup>().DOFade(1f, 0.3f)).Join(toggle0_2.GetComponent<CanvasGroup>().DOFade(1f, 0.3f))
			.Join(toggle0_3.GetComponent<CanvasGroup>().DOFade(1f, 0.3f))
			.Join(toggle0_4.GetComponent<CanvasGroup>().DOFade(1f, 0.3f).OnComplete(delegate
			{
				btn_select01.gameObject.SetActive(value: true);
				btn_select01.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
				Sequence sequence2 = DOTween.Sequence();
				sequence2.Append(btn_select01.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 1f));
				sequence2.Append(btn_select01.transform.DOScale(new Vector3(1f, 1f, 1f), 1f));
				sequence2.Play().SetLoops(-1);
			}));
		sequence.Play();
	}

	public bool Click(LetterItem letterItem)
	{
		if (!iscanclick || result.Length >= inputbox.Count)
		{
			return false;
		}
		iscanclick = false;
		result += letterItem.letter.ToString();
		letterItem.transform.DOMove(inputbox[result.Length - 1].transform.position, 0.5f).OnComplete(delegate
		{
			letterItem.transform.DORotate(new Vector3(0f, 90f, 0f), 0.2f).OnComplete(delegate
			{
				letterItem.gameObject.SetActive(value: false);
				iscanclick = true;
			});
			letterItems.Add(letterItem);
			inputbox[result.Length - 1].transform.GetChild(0).GetComponent<Text>().text = letterItem.letter;
			if (result.Length == 8)
			{
				Check();
			}
		});
		return true;
	}

	public void ClearLetter()
	{
		if (iscanclick)
		{
			for (int i = 0; i < inputbox.Count; i++)
			{
				inputbox[i].transform.GetChild(0).GetComponent<Text>().text = "";
			}
			for (int j = 0; j < letterItems.Count; j++)
			{
				letterItems[j].ResetPosition();
			}
			result = "";
			letterItems.Clear();
			iscanclick = true;
		}
	}

	public void Check()
	{
		isallright = result.Equals(answer);
		if (isallright)
		{
			iscaninput = false;
			Sequence sequence = DOTween.Sequence();
			txt_code.gameObject.SetActive(value: true);
			btn_reset.gameObject.SetActive(value: false);
			for (int i = 0; i < inputbox.Count; i++)
			{
				sequence.Join(inputbox[i].transform.DOLocalMoveX(0f, 1f));
			}
			sequence.AppendCallback(delegate
			{
				for (int j = 0; j < inputbox.Count; j++)
				{
					inputbox[j].SetActive(value: false);
				}
			});
			sequence.Append(txt_code.DOText(answer, 1f).OnComplete(delegate
			{
				Debug.Log("Over");
				reasoningPanel.GetResult();
			}));
			sequence.Play();
		}
		else
		{
			StartCoroutine(ShowWrong());
		}
	}

	private IEnumerator ShowWrong()
	{
		int i;
		for (i = 0; i < inputbox.Count; i++)
		{
			if (inputbox[i].transform.childCount >= 2)
			{
				inputbox[i].transform.GetChild(1).gameObject.SetActive(value: true);
				Sequence sequence = DOTween.Sequence();
				sequence.Append(inputbox[i].transform.GetChild(1).GetComponent<Image>().DOFade(0.2f, 0.3f)).Append(inputbox[i].transform.GetChild(1).GetComponent<Image>().DOFade(1f, 0.3f)).Append(inputbox[i].transform.GetChild(1).GetComponent<Image>().DOFade(0.2f, 0.3f))
					.Append(inputbox[i].transform.GetChild(1).GetComponent<Image>().DOFade(1f, 0.3f))
					.Append(inputbox[i].transform.GetChild(1).GetComponent<Image>().DOFade(0f, 0.3f))
					.AppendCallback(delegate
					{
						inputbox[i].transform.GetChild(1).gameObject.SetActive(value: false);
					});
				sequence.Play();
			}
		}
		yield return new WaitForSeconds(2f);
		iscaninput = true;
	}

	public override bool IsAllRight()
	{
		return isallright;
	}

	public void ChangeValue()
	{
		if (toggle1.isOn)
		{
			btn_select03.gameObject.SetActive(value: false);
			Gotonext();
		}
		else
		{
			StartCoroutine(StartToggleWrong());
		}
	}

	public void ChangeValue0()
	{
		if (toggle0_1.isOn)
		{
			btn_select01.gameObject.SetActive(value: false);
			Gotonext0();
		}
		else
		{
			StartCoroutine(StartToggleWrong0());
		}
	}

	public void ChangeValue1()
	{
		if (toggle1_1.isOn)
		{
			btn_select02.gameObject.SetActive(value: false);
			Gotonext1();
		}
		else
		{
			StartCoroutine(StartToggleWrong1());
		}
	}

	private IEnumerator StartToggleWrong1()
	{
		reasoningPanel.gameManager.homeScene.eventsystem.SetActive(value: false);
		if (toggle1_1.isOn)
		{
			toggle1.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		if (toggle1_2.isOn)
		{
			toggle1_2.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		if (toggle1_3.isOn)
		{
			toggle1_3.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		yield return new WaitForSeconds(1f);
		toggle1_1.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		toggle1_2.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		toggle1_3.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		toggle1_1.isOn = false;
		toggle1_2.isOn = false;
		toggle1_3.isOn = false;
		reasoningPanel.gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	private IEnumerator StartToggleWrong0()
	{
		reasoningPanel.gameManager.homeScene.eventsystem.SetActive(value: false);
		if (toggle0_1.isOn)
		{
			toggle0_1.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		if (toggle0_2.isOn)
		{
			toggle0_2.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		if (toggle0_3.isOn)
		{
			toggle0_3.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		if (toggle0_4.isOn)
		{
			toggle0_4.transform.GetChild(1).GetComponent<Text>().DOColor(Color.red, 0.3f);
		}
		yield return new WaitForSeconds(1f);
		toggle0_1.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		toggle0_2.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		toggle0_3.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		toggle0_4.transform.GetChild(1).GetComponent<Text>().DOColor(new Color32(71, 74, 82, byte.MaxValue), 0.3f);
		toggle0_1.isOn = false;
		toggle0_2.isOn = false;
		toggle0_3.isOn = false;
		toggle0_4.isOn = false;
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

	public void Gotonext()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Join(toggle2.GetComponent<CanvasGroup>().DOFade(0f, 0.3f)).Join(toggle2.transform.DOBlendableLocalMoveBy(new Vector3(0f, 15f, 0f), 0.5f).SetEase(Ease.InCubic)).Join(toggle3.GetComponent<CanvasGroup>().DOFade(0f, 0.3f))
			.Join(toggle3.transform.DOBlendableLocalMoveBy(new Vector3(0f, 15f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Join(toggle4.GetComponent<CanvasGroup>().DOFade(0f, 0.3f))
			.Join(toggle4.transform.DOBlendableLocalMoveBy(new Vector3(0f, 15f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Join(toggle1.transform.DOBlendableLocalMoveBy(new Vector3(0f, 70f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Append(img_codetitle2.transform.DOScaleY(1f, 0.5f))
			.Join(img_code02.transform.DOScale(Vector2.one, 0.5f))
			.Join(img_code02.GetComponent<Image>().DOFade(1f, 0.3f))
			.Join(img_code03.transform.DOScale(Vector2.one, 0.5f))
			.Join(img_code03.GetComponent<Image>().DOFade(1f, 0.3f))
			.Append(img_title03.transform.DOScaleY(1f, 0.5f));
		for (int i = 0; i < inputbox.Count; i++)
		{
			sequence.Join(inputbox[i].transform.DOScale(Vector3.one, 0.7f));
		}
		sequence.Join(btn_reset.DOScale(Vector3.one, 0.7f));
		sequence.Play().OnComplete(delegate
		{
			scrollRect.content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 1550f);
			DOTween.To(() => scrollRect.content.localPosition, delegate(Vector3 x)
			{
				scrollRect.content.localPosition = x;
			}, new Vector3(0f, 727f, 0f), 0.29f).OnComplete(delegate
			{
				iscaninput = true;
				for (int j = 0; j < allletterItems.Count; j++)
				{
					allletterItems[j].Init();
				}
			});
		});
	}

	public void Gotonext0()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Append(toggle0_2.GetComponent<CanvasGroup>().DOFade(0f, 0.3f)).Join(toggle0_2.transform.DOBlendableLocalMoveBy(new Vector3(0f, 15f, 0f), 0.5f).SetEase(Ease.InCubic)).Join(toggle0_3.GetComponent<CanvasGroup>().DOFade(0f, 0.3f))
			.Join(toggle0_3.transform.DOBlendableLocalMoveBy(new Vector3(0f, 15f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Join(toggle0_4.GetComponent<CanvasGroup>().DOFade(0f, 0.3f))
			.Join(toggle0_4.transform.DOBlendableLocalMoveBy(new Vector3(0f, 15f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Join(toggle0_1.transform.DOBlendableLocalMoveBy(new Vector3(0f, 90f, 0f), 0.5f).SetEase(Ease.InCubic));
		sequence.Play();
		toggleGroup01.SetActive(value: true);
		Sequence sequence2 = DOTween.Sequence();
		sequence2.AppendInterval(2f).Append(img_title01.transform.DOScaleY(1f, 0.3f)).Join(toggle1_1.GetComponent<CanvasGroup>().DOFade(1f, 0.3f))
			.Join(toggle1_2.GetComponent<CanvasGroup>().DOFade(1f, 0.3f))
			.Join(toggle1_3.GetComponent<CanvasGroup>().DOFade(1f, 0.3f).OnComplete(delegate
			{
				btn_select02.gameObject.SetActive(value: true);
				btn_select02.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
				Sequence sequence3 = DOTween.Sequence();
				sequence3.Append(btn_select02.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 1f));
				sequence3.Append(btn_select02.transform.DOScale(new Vector3(1f, 1f, 1f), 1f));
				sequence3.Play().SetLoops(-1);
			}));
		sequence2.Play();
	}

	public void Gotonext1()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Append(toggle1_2.GetComponent<CanvasGroup>().DOFade(0f, 0.3f)).Join(toggle1_2.transform.DOBlendableLocalMoveBy(new Vector3(0f, 15f, 0f), 0.5f).SetEase(Ease.InCubic)).Join(toggle1_3.GetComponent<CanvasGroup>().DOFade(0f, 0.3f))
			.Join(toggle1_3.transform.DOBlendableLocalMoveBy(new Vector3(0f, 15f, 0f), 0.5f).SetEase(Ease.InCubic))
			.Join(toggle1_1.transform.DOBlendableLocalMoveBy(new Vector3(0f, 90f, 0f), 0.5f).SetEase(Ease.InCubic));
		sequence.Play();
		Sequence sequence2 = DOTween.Sequence();
		sequence2.AppendInterval(2f).Append(img_codetitle1.transform.DOScaleY(1f, 0.3f)).Append(img_code01.DOFade(1f, 0.3f))
			.Join(img_code01.transform.DOScale(Vector3.one, 0.3f))
			.Append(img_title02.transform.DOScaleY(1f, 0.3f))
			.Join(toggle1.GetComponent<CanvasGroup>().DOFade(1f, 0.3f))
			.Join(toggle2.GetComponent<CanvasGroup>().DOFade(1f, 0.3f))
			.Join(toggle3.GetComponent<CanvasGroup>().DOFade(1f, 0.3f))
			.Join(toggle4.GetComponent<CanvasGroup>().DOFade(1f, 0.3f));
		sequence2.Play().OnComplete(delegate
		{
			scrollRect.content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 1000f);
			DOTween.To(() => scrollRect.content.localPosition, delegate(Vector3 x)
			{
				scrollRect.content.localPosition = x;
			}, new Vector3(0f, 200f, 0f), 0.29f).OnComplete(delegate
			{
				btn_select03.gameObject.SetActive(value: true);
				btn_select03.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
				Sequence sequence3 = DOTween.Sequence();
				sequence3.Append(btn_select03.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 1f));
				sequence3.Append(btn_select03.transform.DOScale(new Vector3(1f, 1f, 1f), 1f));
				sequence3.Play().SetLoops(-1);
			});
		});
	}
}
