using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BigCard : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, IEndDragHandler, IBeginDragHandler, IDragHandler
{
	[SerializeField]
	private GameObject card0;

	[SerializeField]
	private GameObject card1;

	[SerializeField]
	private bool isup = true;

	[SerializeField]
	private bool correctisup = true;

	[SerializeField]
	private bool iscanclick = true;

	public int pos;

	public int correntpos;

	private RectTransform m_rt;

	public Vector3 startpos;

	public Vector3 startscale;

	[Header("是否精准拖拽")]
	public bool m_isPrecision;

	private Vector3 m_offset;

	[SerializeField]
	private Sprite card1graysprite1;

	[SerializeField]
	private Sprite card1bluesprite1;

	[SerializeField]
	private Sprite redsprite1;

	[SerializeField]
	private Sprite redsprite2;

	[SerializeField]
	private Image imgcard1_bk1;

	[SerializeField]
	private Image imgcard2_bk1;

	public Reasoning4008Step01 reasoning4008Step01;

	public bool isred;

	private bool iscandrag;

	public bool IsRight()
	{
		if (pos != correntpos)
		{
			return false;
		}
		if (isup != correctisup)
		{
			return false;
		}
		return true;
	}

	private void Start()
	{
		m_rt = base.gameObject.GetComponent<RectTransform>();
		startpos = m_rt.position;
	}

	public void SetRed()
	{
		isred = true;
		if (isup)
		{
			imgcard1_bk1.sprite = redsprite1;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(imgcard1_bk1.DOFade(0.2f, 0.5f));
			sequence.Append(imgcard1_bk1.DOFade(1f, 0.5f));
			sequence.Play().SetLoops(3).OnComplete(delegate
			{
				imgcard1_bk1.sprite = card1graysprite1;
				isred = false;
			});
		}
		else
		{
			imgcard2_bk1.sprite = redsprite2;
			Sequence sequence2 = DOTween.Sequence();
			sequence2.Append(imgcard2_bk1.DOFade(0.2f, 0.5f));
			sequence2.Append(imgcard2_bk1.DOFade(1f, 0.5f));
			sequence2.Play().SetLoops(3).OnComplete(delegate
			{
				imgcard2_bk1.sprite = card1bluesprite1;
				isred = false;
			});
		}
	}

	public void Click()
	{
		if (!iscanclick || isred)
		{
			return;
		}
		iscanclick = false;
		if (isup)
		{
			base.transform.DOLocalRotate(new Vector3(0f, 90f, 0f), 0.1f).OnComplete(delegate
			{
				card0.SetActive(value: true);
				card1.SetActive(value: false);
				base.transform.DOLocalRotate(new Vector3(0f, 180f, 0f), 0.1f).OnComplete(delegate
				{
					iscanclick = true;
				});
			});
			isup = false;
			return;
		}
		base.transform.DOLocalRotate(new Vector3(0f, 90f, 0f), 0.1f).OnComplete(delegate
		{
			card0.SetActive(value: false);
			card1.SetActive(value: true);
			base.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.1f).OnComplete(delegate
			{
				iscanclick = true;
			});
		});
		isup = true;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Click();
	}

	public void Move(Vector3 topos, float time, bool isreset)
	{
		iscanclick = false;
		GetComponent<BigCard>().iscandrag = false;
		base.transform.DOMove(topos, time).OnComplete(delegate
		{
			iscanclick = true;
			iscandrag = false;
			GetComponent<BigCard>().startpos = topos;
			if (isreset && reasoning4008Step01 != null)
			{
				reasoning4008Step01.iscandragcard = true;
			}
		});
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!isred && iscandrag)
		{
			iscanclick = false;
			SetDraggedPosition(eventData);
			GameObject gameObject = IsPointerOverUIObject(eventData);
			if (gameObject != null && !gameObject.transform.parent.GetComponent<BigCard>().isred && gameObject.transform.parent.GetComponent<BigCard>().iscanclick)
			{
				int num = pos;
				Move(gameObject.transform.parent.position, 0.5f, isreset: false);
				gameObject.transform.parent.GetComponent<BigCard>().Move(startpos, 0.6f, isreset: true);
				pos = gameObject.transform.parent.GetComponent<BigCard>().pos;
				gameObject.transform.parent.GetComponent<BigCard>().pos = num;
			}
			else
			{
				Move(startpos, 0.5f, isreset: true);
				base.transform.DOScale(startscale, 0.2f);
			}
		}
	}

	private void Cannotmove()
	{
		if (!(reasoning4008Step01 != null))
		{
			return;
		}
		if (!reasoning4008Step01.iscandragcard)
		{
			Debug.Log("不可点击");
			return;
		}
		Debug.Log("可点击");
		if (reasoning4008Step01.iscandragcard)
		{
			reasoning4008Step01.iscandragcard = false;
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (isred || !iscanclick || iscandrag)
		{
			return;
		}
		startpos = m_rt.position;
		if (reasoning4008Step01 != null)
		{
			if (!reasoning4008Step01.iscandragcard)
			{
				Debug.Log("不可点击");
				return;
			}
			Debug.Log("可点击");
			if (reasoning4008Step01.iscandragcard)
			{
				reasoning4008Step01.iscandragcard = false;
			}
		}
		iscandrag = true;
		iscanclick = false;
		base.transform.SetAsLastSibling();
		if (m_isPrecision)
		{
			RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out var worldPoint);
			m_offset = base.transform.position - worldPoint;
		}
		else
		{
			m_offset = Vector3.zero;
		}
		SetDraggedPosition(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!isred && iscandrag)
		{
			SetDraggedPosition(eventData);
		}
	}

	private void SetDraggedPosition(PointerEventData eventData)
	{
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out var worldPoint))
		{
			Vector3 position = worldPoint + m_offset;
			m_rt.position = position;
		}
	}

	private GameObject IsPointerOverUIObject(PointerEventData eventDataCurrentPosition)
	{
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, list);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].gameObject.tag == "itempanel" && !list[i].gameObject.transform.parent.name.Equals(base.gameObject.name))
			{
				return list[i].gameObject;
			}
		}
		return null;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!isred)
		{
			base.transform.DOScale(new Vector3(startscale.x * 1.05f, startscale.y * 1.05f, startscale.z * 1.05f), 0.2f);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!isred)
		{
			base.transform.DOScale(startscale, 0.2f);
		}
	}
}
