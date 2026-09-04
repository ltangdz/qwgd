using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LeonKim
{
	public class BaseLoopList : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
	{
		public delegate void CallBackFunc(GameObject cell, int index);

		protected struct CellInfo
		{
			public Vector3 pos;

			public GameObject obj;
		}

		public int m_Row = 1;

		public bool m_IsVertical = true;

		public float m_Spacing;

		public GameObject m_CellGameObject;

		protected CallBackFunc m_CallBackFunc;

		protected object m_Members;

		protected RectTransform rectTrans;

		protected float m_PlaneWidth;

		protected float m_PlaneHeight;

		protected float m_ContentWidth;

		protected float m_ContentHeight;

		protected float m_CellObjectWidth;

		protected float m_CellObjectHeight;

		protected GameObject m_Content;

		protected RectTransform m_ContentRectTrans;

		private bool m_isInited;

		protected CellInfo[] m_CellInfos;

		protected bool m_IsInited;

		protected ScrollRect m_ScrollRect;

		protected int m_MaxCount = -1;

		protected int m_MinIndex = -1;

		protected int m_MaxIndex = -1;

		protected bool m_IsClearList;

		protected Stack<GameObject> poolsObj = new Stack<GameObject>();

		public virtual void Init(CallBackFunc func)
		{
			m_CallBackFunc = func;
			DisposeAll();
			if (!m_isInited)
			{
				m_Content = GetComponent<ScrollRect>().content.gameObject;
				if (m_CellGameObject == null)
				{
					m_CellGameObject = m_Content.transform.GetChild(0).gameObject;
				}
				SetPoolsObj(m_CellGameObject);
				RectTransform component = m_CellGameObject.GetComponent<RectTransform>();
				component.pivot = new Vector2(0f, 1f);
				CheckAnchor(component);
				component.anchoredPosition = Vector2.zero;
				m_CellObjectHeight = component.rect.height;
				m_CellObjectWidth = component.rect.width;
				rectTrans = GetComponent<RectTransform>();
				Rect rect = rectTrans.rect;
				m_PlaneHeight = rect.height;
				m_PlaneWidth = rect.width;
				m_ContentRectTrans = m_Content.GetComponent<RectTransform>();
				Rect rect2 = m_ContentRectTrans.rect;
				m_ContentHeight = rect2.height;
				m_ContentWidth = rect2.width;
				m_ContentRectTrans.pivot = new Vector2(0f, 1f);
				CheckAnchor(m_ContentRectTrans);
				m_ScrollRect = GetComponent<ScrollRect>();
				m_ScrollRect.onValueChanged.RemoveAllListeners();
				m_ScrollRect.onValueChanged.AddListener(delegate(Vector2 value)
				{
					ScrollRectListener(value);
				});
				m_isInited = true;
			}
		}

		private void CheckAnchor(RectTransform rectTrans)
		{
			if (m_IsVertical)
			{
				if ((!(rectTrans.anchorMin == new Vector2(0f, 1f)) || !(rectTrans.anchorMax == new Vector2(0f, 1f))) && (!(rectTrans.anchorMin == new Vector2(0f, 1f)) || !(rectTrans.anchorMax == new Vector2(1f, 1f))))
				{
					rectTrans.anchorMin = new Vector2(0f, 1f);
					rectTrans.anchorMax = new Vector2(1f, 1f);
				}
			}
			else if ((!(rectTrans.anchorMin == new Vector2(0f, 1f)) || !(rectTrans.anchorMax == new Vector2(0f, 1f))) && (!(rectTrans.anchorMin == new Vector2(0f, 0f)) || !(rectTrans.anchorMax == new Vector2(0f, 1f))))
			{
				rectTrans.anchorMin = new Vector2(0f, 0f);
				rectTrans.anchorMax = new Vector2(0f, 1f);
			}
		}

		public virtual void ShowList(string numStr)
		{
		}

		public virtual void ShowList(int num)
		{
			m_MinIndex = -1;
			m_MaxIndex = -1;
			if (m_IsVertical)
			{
				float num2 = (m_ContentHeight = (m_Spacing + m_CellObjectHeight) * (float)Mathf.CeilToInt((float)num / (float)m_Row));
				m_ContentWidth = m_ContentRectTrans.sizeDelta.x;
				num2 = ((num2 < rectTrans.rect.height) ? rectTrans.rect.height : num2);
				m_ContentRectTrans.sizeDelta = new Vector2(m_ContentWidth, num2);
				if (num != m_MaxCount)
				{
					m_ContentRectTrans.anchoredPosition = new Vector2(m_ContentRectTrans.anchoredPosition.x, 0f);
				}
			}
			else
			{
				float num3 = (m_ContentWidth = (m_Spacing + m_CellObjectWidth) * (float)Mathf.CeilToInt((float)num / (float)m_Row));
				m_ContentHeight = m_ContentRectTrans.sizeDelta.x;
				num3 = ((num3 < rectTrans.rect.width) ? rectTrans.rect.width : num3);
				m_ContentRectTrans.sizeDelta = new Vector2(num3, m_ContentHeight);
				if (num != m_MaxCount)
				{
					m_ContentRectTrans.anchoredPosition = new Vector2(0f, m_ContentRectTrans.anchoredPosition.y);
				}
			}
			int num4 = 0;
			if (m_IsInited)
			{
				num4 = ((num - m_MaxCount > 0) ? m_MaxCount : num);
				num4 = ((!m_IsClearList) ? num4 : 0);
				int num5 = (m_IsClearList ? m_CellInfos.Length : m_MaxCount);
				for (int i = num4; i < num5; i++)
				{
					if (m_CellInfos[i].obj != null)
					{
						SetPoolsObj(m_CellInfos[i].obj);
						m_CellInfos[i].obj = null;
					}
				}
			}
			CellInfo[] cellInfos = m_CellInfos;
			m_CellInfos = new CellInfo[num];
			for (int j = 0; j < num; j++)
			{
				if (m_MaxCount != -1 && j < num4)
				{
					CellInfo cellInfo = cellInfos[j];
					float pos = (m_IsVertical ? cellInfo.pos.y : cellInfo.pos.x);
					if (!IsOutRange(pos))
					{
						m_MinIndex = ((m_MinIndex == -1) ? j : m_MinIndex);
						m_MaxIndex = j;
						if (cellInfo.obj == null)
						{
							cellInfo.obj = GetPoolsObj();
						}
						cellInfo.obj.transform.GetComponent<RectTransform>().anchoredPosition = cellInfo.pos;
						cellInfo.obj.name = j.ToString();
						cellInfo.obj.SetActive(value: true);
						Func(cellInfo.obj);
					}
					else
					{
						SetPoolsObj(cellInfo.obj);
						cellInfo.obj = null;
					}
					m_CellInfos[j] = cellInfo;
					continue;
				}
				CellInfo cellInfo2 = default(CellInfo);
				float num6 = 0f;
				float num7 = 0f;
				if (m_IsVertical)
				{
					num6 = m_CellObjectHeight * (float)Mathf.FloorToInt(j / m_Row) + m_Spacing * (float)Mathf.FloorToInt(j / m_Row);
					num7 = m_CellObjectWidth * (float)(j % m_Row) + m_Spacing * (float)(j % m_Row);
					cellInfo2.pos = new Vector3(num7, 0f - num6, 0f);
				}
				else
				{
					num6 = m_CellObjectWidth * (float)Mathf.FloorToInt(j / m_Row) + m_Spacing * (float)Mathf.FloorToInt(j / m_Row);
					num7 = m_CellObjectHeight * (float)(j % m_Row) + m_Spacing * (float)(j % m_Row);
					cellInfo2.pos = new Vector3(num6, 0f - num7, 0f);
				}
				float pos2 = (m_IsVertical ? cellInfo2.pos.y : cellInfo2.pos.x);
				if (IsOutRange(pos2))
				{
					cellInfo2.obj = null;
					m_CellInfos[j] = cellInfo2;
					continue;
				}
				m_MinIndex = ((m_MinIndex == -1) ? j : m_MinIndex);
				m_MaxIndex = j;
				GameObject gameObject = GetPoolsObj();
				gameObject.transform.GetComponent<RectTransform>().anchoredPosition = cellInfo2.pos;
				gameObject.gameObject.name = j.ToString();
				cellInfo2.obj = gameObject;
				m_CellInfos[j] = cellInfo2;
				Func(gameObject);
			}
			m_MaxCount = num;
			m_IsInited = true;
		}

		public virtual void UpdateList()
		{
			int i = 0;
			for (int num = m_CellInfos.Length; i < num; i++)
			{
				CellInfo cellInfo = m_CellInfos[i];
				if (cellInfo.obj != null)
				{
					float pos = (m_IsVertical ? cellInfo.pos.y : cellInfo.pos.x);
					if (!IsOutRange(pos))
					{
						Func(cellInfo.obj);
					}
				}
			}
		}

		public void UpdateCell(int index)
		{
			CellInfo cellInfo = m_CellInfos[index - 1];
			if (cellInfo.obj != null)
			{
				float pos = (m_IsVertical ? cellInfo.pos.y : cellInfo.pos.x);
				if (!IsOutRange(pos))
				{
					Func(cellInfo.obj);
				}
			}
		}

		public void UpdateSize()
		{
			Rect rect = GetComponent<RectTransform>().rect;
			m_PlaneHeight = rect.height;
			m_PlaneWidth = rect.width;
		}

		protected virtual void ScrollRectListener(Vector2 value)
		{
			NormalPerformanceMode();
		}

		private void NormalPerformanceMode()
		{
			if (m_CellInfos == null)
			{
				return;
			}
			int i = 0;
			for (int num = m_CellInfos.Length; i < num; i++)
			{
				CellInfo cellInfo = m_CellInfos[i];
				GameObject obj = cellInfo.obj;
				Vector3 pos = cellInfo.pos;
				float pos2 = (m_IsVertical ? pos.y : pos.x);
				if (IsOutRange(pos2))
				{
					if (obj != null)
					{
						SetPoolsObj(obj);
						m_CellInfos[i].obj = null;
					}
				}
				else if (obj == null)
				{
					GameObject gameObject = GetPoolsObj();
					gameObject.transform.localPosition = pos;
					gameObject.gameObject.name = i.ToString();
					m_CellInfos[i].obj = gameObject;
					Func(gameObject);
				}
			}
		}

		protected bool IsOutRange(float pos)
		{
			Vector3 vector = m_ContentRectTrans.anchoredPosition;
			if (m_IsVertical)
			{
				if (pos + vector.y > m_CellObjectHeight || pos + vector.y < 0f - rectTrans.rect.height)
				{
					return true;
				}
			}
			else if (pos + vector.x < 0f - m_CellObjectWidth || pos + vector.x > rectTrans.rect.width)
			{
				return true;
			}
			return false;
		}

		protected virtual GameObject GetPoolsObj()
		{
			GameObject gameObject = null;
			if (poolsObj.Count > 0)
			{
				gameObject = poolsObj.Pop();
			}
			if (gameObject == null)
			{
				gameObject = Object.Instantiate(m_CellGameObject);
			}
			gameObject.transform.SetParent(m_Content.transform);
			gameObject.transform.localScale = Vector3.one;
			SetActive(gameObject, isShow: true);
			return gameObject;
		}

		protected virtual void SetPoolsObj(GameObject cell)
		{
			if (cell != null)
			{
				poolsObj.Push(cell);
				SetActive(cell, isShow: false);
			}
		}

		protected void Func(GameObject selectObject)
		{
			int index = int.Parse(selectObject.name) + 1;
			if (m_CallBackFunc != null)
			{
				m_CallBackFunc(selectObject, index);
			}
		}

		private void SetActive(GameObject cell, bool isShow)
		{
			cell.SetActive(isShow);
		}

		public void DisposeAll()
		{
		}

		protected void OnDestroy()
		{
			DisposeAll();
		}

		public virtual void OnClickCell(GameObject cell)
		{
		}

		public virtual void OnClickExpand(int index)
		{
		}

		public virtual void SetToPageIndex(int index)
		{
		}

		public virtual void OnBeginDrag(PointerEventData eventData)
		{
		}

		public void OnDrag(PointerEventData eventData)
		{
		}

		public virtual void OnEndDrag(PointerEventData eventData)
		{
		}

		protected void OnDragListener(Vector2 value)
		{
		}
	}
}
