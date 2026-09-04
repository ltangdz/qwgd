using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReasoningPage1ResultItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public int id;

	public Text showText;

	public Image backgroundImage;

	public bool isEnable = true;

	private bool selected;

	private bool isUsed;

	public void SetContent(int id, string text)
	{
		this.id = id;
		showText.text = text;
	}

	public void EmptyContent()
	{
		id = -1;
		showText.text = "";
	}

	public void SetUsed()
	{
		isUsed = true;
		showText.color = new Color(showText.color.r, showText.color.g, showText.color.b, 0.5f);
		Object.Destroy(GetComponent<OnDragItem>());
	}

	private void Update()
	{
		if (isEnable && DragManager.instance.reasoningPage1ResultItem.id != -1 && !isUsed)
		{
			if (DragManager.instance.reasoningPage1ResultItem.id == id)
			{
				showText.color = new Color(showText.color.r, showText.color.g, showText.color.b, 0.5f);
			}
			else
			{
				showText.color = new Color(showText.color.r, showText.color.g, showText.color.b, 1f);
			}
		}
	}

	public void Reset()
	{
		SetSelected(s: false);
		if (!isUsed)
		{
			showText.color = new Color(showText.color.r, showText.color.g, showText.color.b, 1f);
		}
	}

	public void SetSelected(bool s)
	{
		selected = s;
		backgroundImage.gameObject.SetActive(s);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (DragManager.instance.reasoningPage1ResultItem.id == -1 && !isUsed)
		{
			SetSelected(s: true);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (DragManager.instance.reasoningPage1ResultItem.id == -1 && !isUsed)
		{
			SetSelected(s: false);
		}
	}
}
