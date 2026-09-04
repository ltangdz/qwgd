using DLC7.Reasoning;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnDragItem : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	private Vector2 originPos;

	public void OnBeginDrag(PointerEventData eventData)
	{
		ReasoningPage1ResultItem component = GetComponent<ReasoningPage1ResultItem>();
		DragManager.instance.reasoningPage1ResultItem.SetContent(component.id, component.showText.text);
		originPos = DragManager.instance.reasoningPage1ResultItem.transform.localPosition;
		DragManager.instance.reasoningPage1ResultItem.transform.localPosition = GetUIPos(DragManager.instance.uiCanvas.gameObject);
		DragManager.instance.reasoningPage1ResultItem.gameObject.SetActive(value: true);
	}

	public void OnDrag(PointerEventData eventData)
	{
		DragManager.instance.reasoningPage1ResultItem.transform.localPosition = GetUIPos(DragManager.instance.uiCanvas.gameObject);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		DragManager.instance.reasoningPage1ResultItem.gameObject.SetActive(value: false);
		if (ReasoningPage1UI.selectedReasoningPage1LeftItemJudgeUI != null)
		{
			ReasoningPage1UI.selectedReasoningPage1LeftItemJudgeUI.JudgeResult(DragManager.instance.reasoningPage1ResultItem.id);
			ReasoningPage1UI.selectedReasoningPage1LeftItemJudgeUI = null;
		}
		else
		{
			DLC7.Reasoning.ReasoningManager.Instance.ResetResult(DragManager.instance.reasoningPage1ResultItem.id);
		}
		DragManager.instance.reasoningPage1ResultItem.EmptyContent();
	}

	public static Vector2 GetUIPos(GameObject go)
	{
		Vector2 sizeDelta = go.GetComponent<RectTransform>().sizeDelta;
		Vector2 vector = Input.mousePosition;
		Vector2 vector2 = default(Vector2);
		vector2.x = vector.x - (float)(Screen.width / 2);
		vector2.y = vector.y - (float)(Screen.height / 2);
		Vector2 result = default(Vector2);
		result.x = vector2.x * (sizeDelta.x / (float)Screen.width);
		result.y = vector2.y * (sizeDelta.y / (float)Screen.height);
		return result;
	}
}
