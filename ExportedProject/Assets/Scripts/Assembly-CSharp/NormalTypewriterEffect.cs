using Honeti;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NormalTypewriterEffect : MonoBehaviour
{
	public float charsPerSecond = 0.005f;

	public string words;

	public bool isActive;

	private float timer;

	public TextMeshProUGUI txt_question;

	private int currentPos;

	public string question;

	public ContentSizeFitter sizeFitter;

	private bool isbk;

	private void Start()
	{
		timer = 0f;
		txt_question.text = "";
	}

	private void Update()
	{
		OnStartWriter();
	}

	public void StartEffect(string questionkey, bool isbkk = false)
	{
		if (isbkk)
		{
			isbk = isbkk;
			sizeFitter = txt_question.GetComponent<ContentSizeFitter>();
		}
		question = questionkey;
		words = question;
		isActive = true;
	}

	private void OnStartWriter()
	{
		if (!isActive)
		{
			return;
		}
		timer += Time.deltaTime;
		if (timer >= charsPerSecond)
		{
			timer = 0f;
			currentPos++;
			txt_question.GetComponent<I18NText>().updateTranslation2(words.Substring(0, currentPos));
			if (isbk)
			{
				FreshBk();
			}
			if (currentPos >= words.Length)
			{
				OnFinish();
			}
		}
	}

	private void FreshBk()
	{
		int num = 300;
		if (txt_question.preferredWidth > (float)num)
		{
			txt_question.rectTransform.sizeDelta = new Vector2(num, txt_question.rectTransform.sizeDelta.y);
			sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
		}
		else
		{
			txt_question.rectTransform.sizeDelta = new Vector2(txt_question.preferredWidth, txt_question.rectTransform.sizeDelta.y);
			sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			sizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
		}
	}

	private void OnFinish()
	{
		isActive = false;
		timer = 0f;
		currentPos = 0;
		txt_question.GetComponent<I18NText>().updateTranslation2(words);
	}
}
