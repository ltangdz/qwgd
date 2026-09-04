using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PasteInputField : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	public InputField inputfield;

	public Button btn_paste;

	public float zoom = 1f;

	public bool isshow;

	private void Start()
	{
	}

	public void Click()
	{
		if (!GUIUtility.systemCopyBuffer.Equals("") && GUIUtility.systemCopyBuffer != null && inputfield != null)
		{
			inputfield.text = GUIUtility.systemCopyBuffer;
		}
		CloseButton();
	}

	public void ShowPaste()
	{
		btn_paste.GetComponent<CanvasGroup>().alpha = 1f;
	}

	public void HidePaste()
	{
		btn_paste.GetComponent<CanvasGroup>().alpha = 0f;
	}

	private void Update()
	{
		if (inputfield.isFocused && inputfield.text.Length == 0 && !isshow)
		{
			if (!GUIUtility.systemCopyBuffer.Equals("") && GUIUtility.systemCopyBuffer != null)
			{
				ShowButton();
			}
		}
		else if (inputfield.isFocused && inputfield.text.Length > 0 && isshow && btn_paste.transform.localScale == Vector3.one)
		{
			Invoke("CloseButton", 0.1f);
		}
		else if (!inputfield.isFocused && btn_paste.transform.localScale == Vector3.one)
		{
			Invoke("CloseButton", 0.1f);
		}
	}

	private void CloseButton()
	{
		if (isshow)
		{
			isshow = false;
			btn_paste.transform.DOKill();
			btn_paste.transform.DOScale(Vector3.zero, 0.3f);
		}
	}

	private void ShowButton()
	{
		if (!isshow)
		{
			isshow = true;
			ShowPaste();
			btn_paste.transform.DOKill();
			btn_paste.transform.DOScale(Vector3.one * zoom, 0.3f);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Click();
	}
}
