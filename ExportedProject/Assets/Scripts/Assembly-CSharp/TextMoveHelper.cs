using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class TextMoveHelper : MonoBehaviour
{
	public Text textComp;

	public Canvas canvas;

	public Text text;

	public Image img_highlight;

	public GameObject buttons;

	public Button btn_add;

	public Button btn_sign;

	public string itemid;

	private GameManager gameManager;

	public Image img_notclick;

	public Color[] colors;

	public string oricontent;

	public Sprite[] sprites;

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

	public void SetItemid(string itemid)
	{
		this.itemid = itemid;
		if (gameManager.player.playerdata.itemlist.Contains(itemid))
		{
			btn_add.interactable = false;
			btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^getitem");
		}
		else
		{
			btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^highlighttip01");
		}
		if (btn_sign != null)
		{
			btn_sign.onClick.RemoveAllListeners();
		}
	}

	private void Start()
	{
		btn_add.onClick.AddListener(delegate
		{
			btn_add.interactable = false;
			btn_add.transform.Find("Text").GetComponent<I18NText>().updateTranslation2("^getitem");
			img_notclick.gameObject.SetActive(value: false);
			gameManager.homeScene.notebook.gameObject.SetActive(value: true);
			gameManager.homeScene.notebook.AddNewItem(itemid);
			CancelClick();
			InvokeRepeating("CloseButton", 0.1f, 0.01f);
		});
	}

	public void CancelClick()
	{
		if (iscancancle)
		{
			img_notclick.gameObject.SetActive(value: false);
			iscanclick = true;
			InvokeRepeating("CloseButton", 0.1f, 0.01f);
			textComp.GetComponent<I18NText>().updateTranslation2(oricontent);
			img_highlight.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
			img_highlight.fillAmount = 0f;
		}
	}

	public void Init(string bigcontent, string c)
	{
		iscanclick = true;
		oricontent = bigcontent;
		textComp.GetComponent<I18NText>().updateTranslation2(bigcontent);
		StartCoroutine(LerpMove(c));
	}

	public void Click(bool isshowbutton = true)
	{
		if (!iscanclick)
		{
			return;
		}
		img_notclick.gameObject.SetActive(value: true);
		SetItemid(itemid);
		img_highlight.gameObject.SetActive(value: true);
		img_highlight.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, text.rectTransform.sizeDelta.y);
		buttons.GetComponent<RectTransform>().anchoredPosition = new Vector2(img_highlight.rectTransform.sizeDelta.x, 0f - img_highlight.rectTransform.sizeDelta.y);
		img_highlight.color = Color.white;
		if (isshowbutton)
		{
			InvokeRepeating("StartRed", 0.1f, 0.03f);
			return;
		}
		img_highlight.fillAmount = 1f;
		if (colors.Length != 0)
		{
			textComp.GetComponent<I18NText>().updateTranslation2(textComp.text.Replace(text.text, "<color=#" + ColorUtility.ToHtmlStringRGB(colors[0]) + ">" + text.text + "</color>"));
		}
	}

	public void Click()
	{
		if (iscanclick)
		{
			img_notclick.gameObject.SetActive(value: true);
			SetItemid(itemid);
			img_highlight.gameObject.SetActive(value: true);
			img_highlight.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, text.rectTransform.sizeDelta.y);
			buttons.GetComponent<RectTransform>().anchoredPosition = new Vector2(img_highlight.rectTransform.sizeDelta.x, 0f - img_highlight.rectTransform.sizeDelta.y);
			img_highlight.color = Color.white;
			InvokeRepeating("StartRed", 0.1f, 0.03f);
		}
	}

	public void Sign()
	{
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
		img_highlight.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, text.rectTransform.sizeDelta.y);
		img_highlight.rectTransform.position = GetPosAtText(canvas, textComp, content);
		yield return new WaitForSeconds(0.3f);
		text.gameObject.SetActive(value: false);
	}

	private void StartRed()
	{
		img_highlight.fillAmount += 0.1f;
		if (img_highlight.fillAmount >= 1f)
		{
			CancelInvoke();
			buttons.SetActive(value: true);
			if (colors.Length != 0)
			{
				textComp.text = "<color=#" + ColorUtility.ToHtmlStringRGB(colors[0]) + ">" + text.text + "</color>";
			}
			img_notclick.gameObject.SetActive(value: true);
			InvokeRepeating("StartButton", 0.1f, 0.01f);
		}
	}

	private void StartButton()
	{
		Vector3 localScale = buttons.GetComponent<RectTransform>().localScale;
		if (localScale.x >= 1f)
		{
			iscancancle = true;
			CancelInvoke();
		}
		else
		{
			buttons.GetComponent<RectTransform>().localScale = new Vector3(localScale.x + 0.1f, localScale.y + 0.1f, 1f);
		}
	}

	private void CloseButton()
	{
		Vector3 localScale = buttons.GetComponent<RectTransform>().localScale;
		if (localScale.x <= 0f)
		{
			CancelInvoke();
			iscancancle = false;
			buttons.SetActive(value: false);
		}
		else
		{
			buttons.GetComponent<RectTransform>().localScale = new Vector3(localScale.x - 0.1f, localScale.y - 0.1f, 1f);
		}
	}

	public void SetClick(bool canClick)
	{
		iscanclick = canClick;
		img_highlight.raycastTarget = canClick;
	}
}
