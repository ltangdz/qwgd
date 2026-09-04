using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class TextHighLight : MonoBehaviour
{
	public Text textComp;

	public Canvas canvas;

	public Text text;

	private GameManager gameManager;

	public bool iscanclick;

	public bool isusee;

	public bool iscancancle;

	public void ChangeColor(Color c)
	{
		textComp.color = c;
	}

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
	}

	private void Start()
	{
	}

	public void CancelClick()
	{
	}

	public void Init(string bigcontent, string c)
	{
		iscanclick = true;
		textComp.GetComponent<I18NText>().updateTranslation2(bigcontent);
		StartCoroutine(LerpMove(c));
	}

	public Vector3 GetPosAtText(Canvas canvas, Text text, string strFragment)
	{
		int num = text.text.IndexOf(strFragment);
		Vector3 zero = Vector3.zero;
		if (num > -1)
		{
			Vector3 posAtText = GetPosAtText(canvas, text, num + 1);
			Vector3 posAtText2 = GetPosAtText(canvas, text, num + strFragment.Length);
			return (posAtText + posAtText2) * 0.5f;
		}
		return GetPosAtText(canvas, text, num);
	}

	public Vector3 GetPosAtText(Canvas canvas, Text text, int charIndex)
	{
		string text2 = text.text;
		Vector3 position = Vector3.zero;
		if (charIndex <= text2.Length && charIndex > 0)
		{
			TextGenerator textGenerator = new TextGenerator(text2.Length);
			Vector2 size = text.gameObject.GetComponent<RectTransform>().rect.size;
			textGenerator.Populate(text2, text.GetGenerationSettings(size));
			int num = text2.Substring(0, charIndex).Split('\n').Length - 1;
			int num2 = charIndex * 4 + num * 4 - 4;
			if (num2 < textGenerator.vertexCount)
			{
				position = (textGenerator.verts[num2].position + textGenerator.verts[num2 + 1].position + textGenerator.verts[num2 + 2].position + textGenerator.verts[num2 + 3].position) / 4f;
			}
		}
		position /= canvas.scaleFactor;
		return text.transform.TransformPoint(position);
	}

	private void Update()
	{
	}

	private IEnumerator LerpMove(string content)
	{
		text.GetComponent<I18NText>().updateTranslation2(content);
		yield return new WaitForSeconds(0.5f);
		text.rectTransform.position = GetPosAtText(canvas, textComp, content);
		yield return new WaitForSeconds(0.3f);
		text.gameObject.SetActive(value: false);
	}
}
