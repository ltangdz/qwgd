using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DragDialog : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	[Header("是否精准拖拽")]
	public bool m_isPrecision;

	private Vector3 m_offset;

	public RectTransform m_rt;

	public bool iscandrag = true;

	public bool isParTop = true;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (SceneManager.GetActiveScene().name == "homego" || SceneManager.GetActiveScene().name == "homeDLC" || SceneManager.GetActiveScene().name == "homeDLC7")
		{
			base.gameObject.SetActive(value: true);
		}
		else if (gameManager.player.playerdata.isCourse04 == 1 && gameManager.homeScene.newbrowserDialog != null && gameManager.homeScene.newbrowserDialog.name == m_rt.name)
		{
			base.gameObject.SetActive(value: true);
		}
		else if (gameManager.player.playerdata.isCourse04 == 1 && gameManager.homeScene.pictureDialog != null && gameManager.homeScene.pictureDialog.name == m_rt.name)
		{
			base.gameObject.SetActive(value: true);
		}
		else if (gameManager.player.playerdata.isCourse12 == 1 && gameManager.homeScene.sqlDialog != null && gameManager.homeScene.sqlDialog.name == m_rt.name)
		{
			base.gameObject.SetActive(value: true);
		}
		else if (gameManager.player.playerdata.isCourse06 == 1 && gameManager.homeScene.passworddialog1 != null && gameManager.homeScene.passworddialog1.name == m_rt.name)
		{
			base.gameObject.SetActive(value: true);
		}
		else if (gameManager.player.playerdata.isCourse13 == 1 && gameManager.homeScene.passworddialog2 != null && gameManager.homeScene.passworddialog2.name == m_rt.name)
		{
			base.gameObject.SetActive(value: true);
		}
		else if (gameManager.player.playerdata.isCourse15 == 1 && gameManager.homeScene.browserMail != null && gameManager.homeScene.browserMail.name == m_rt.name)
		{
			base.gameObject.SetActive(value: true);
		}
		else if (gameManager.player.playerdata.isCourse10 == 1 && gameManager.homeScene.weizhuang != null && gameManager.homeScene.weizhuang.name == m_rt.name)
		{
			base.gameObject.SetActive(value: true);
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (isParTop && m_rt.transform.parent.gameObject.name.Equals("otherdialogpanel") && !gameManager.homeScene.Iscanopentool())
		{
			m_rt.transform.parent.SetAsLastSibling();
		}
		m_rt.transform.SetAsLastSibling();
		if (iscandrag)
		{
			if (m_isPrecision)
			{
				RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out var worldPoint);
				m_offset = m_rt.position - worldPoint;
			}
			else
			{
				m_offset = Vector3.zero;
			}
			SetDraggedPosition(eventData);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (iscandrag)
		{
			SetDraggedPosition(eventData);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (iscandrag)
		{
			SetDraggedPosition(eventData);
		}
	}

	private GameObject IsPointerOverUIObject(PointerEventData eventDataCurrentPosition)
	{
		if (!iscandrag)
		{
			return null;
		}
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, list);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].gameObject.tag == "img_drag")
			{
				return list[i].gameObject;
			}
		}
		return null;
	}

	private void SetDraggedPosition(PointerEventData eventData)
	{
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out var worldPoint))
		{
			m_rt.position = worldPoint + m_offset;
		}
	}
}
