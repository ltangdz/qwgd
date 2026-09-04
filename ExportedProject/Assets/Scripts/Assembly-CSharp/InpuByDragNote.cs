using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using tnt_deploy;

public class InpuByDragNote : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	private bool _isEnter;

	private InputField _inputField;

	private void OnEnable()
	{
		NoteDragManager.Instance.onDragStart += OnDragStart;
		NoteDragManager.Instance.onDraging += OnDraging;
		NoteDragManager.Instance.onDragEnd += OnDragEnd;
	}

	private void OnDisable()
	{
		NoteDragManager.Instance.onDragStart -= OnDragStart;
		NoteDragManager.Instance.onDraging -= OnDraging;
		NoteDragManager.Instance.onDragEnd -= OnDragEnd;
	}

	private void OnDragStart(PointerEventData eventData, DATA1 data)
	{
	}

	private void OnDraging(PointerEventData eventData, DATA1 data)
	{
	}

	private void OnDragEnd(PointerEventData eventData, DATA1 data)
	{
		if (_isEnter)
		{
			_inputField = GetComponent<InputField>();
			_inputField.text = I18N.instance.getValue(data.message);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_isEnter = false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_isEnter = true;
	}
}
