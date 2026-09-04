using System.Collections.Generic;
using Shapes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DLC7.Reasoning
{
	public class LineItem : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		[Header("列")]
		public int col;

		[Header("行")]
		public int row;

		public Image dotImage;

		public Shapes.Line line;

		public List<int> connectPointIndex;

		public Camera camera;

		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		public void OnDrag(PointerEventData eventData)
		{
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.z = camera.WorldToScreenPoint(base.transform.position).z;
			Vector3 worldPoint = camera.ScreenToWorldPoint(mousePosition);
			Vector3 vector = RectTransformUtility.WorldToScreenPoint(camera, worldPoint);
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out var localPoint);
			Debug.Log(vector.ToString());
			line.transform.position = dotImage.transform.position;
			line.End = localPoint - (Vector2)dotImage.GetComponent<RectTransform>().localPosition;
		}

		public Vector3 WorldToUI(Camera camera, Vector3 pos)
		{
			CanvasScaler componentInParent = base.gameObject.GetComponentInParent<CanvasScaler>();
			float x = componentInParent.referenceResolution.x;
			float y = componentInParent.referenceResolution.y;
			Vector3 vector = camera.WorldToViewportPoint(pos);
			return new Vector3(vector.x * x - x * 0.5f, vector.y * y - y * 0.5f, 0f);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
		}
	}
}
