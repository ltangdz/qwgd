using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using tnt_deploy;

public class NoteDragItem : MonoBehaviour
{
	public Text txt_content;

	private RectTransform m_rt;

	private DATA1 data1;

	private Vector3 screenPos;

	private void Start()
	{
		screenPos = Camera.main.WorldToScreenPoint(base.transform.position);
		m_rt = base.gameObject.GetComponent<RectTransform>();
	}

	private void OnEnable()
	{
		NoteDragManager.Instance.onDragStart += OnDragStart;
		NoteDragManager.Instance.onDraging += OnDraging;
		NoteDragManager.Instance.onDragEnd += OnDragEnd;
	}

	public void SetContent(DATA1 data1)
	{
		SetFront();
		this.data1 = data1;
		txt_content.text = I18N.instance.getValue(data1.title) + ":" + I18N.instance.getValue(data1.message);
	}

	private void OnDisable()
	{
		NoteDragManager.Instance.onDragStart -= OnDragStart;
		NoteDragManager.Instance.onDraging -= OnDraging;
		NoteDragManager.Instance.onDragEnd -= OnDragEnd;
	}

	private void OnDragStart(PointerEventData eventData, DATA1 data)
	{
		SetContent(data);
	}

	private void OnDraging(PointerEventData eventData, DATA1 data)
	{
		base.transform.GetComponent<Image>().raycastTarget = false;
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = screenPos.z;
		Vector3 position = Camera.main.ScreenToWorldPoint(mousePosition);
		base.transform.position = position;
	}

	private void OnDragEnd(PointerEventData eventData, DATA1 data)
	{
		HideDialog();
	}

	private void SetFront()
	{
		base.transform.SetAsLastSibling();
	}

	public void HideDialog()
	{
		base.transform.position = new Vector3(1287f, -140f, 0f);
	}
}
