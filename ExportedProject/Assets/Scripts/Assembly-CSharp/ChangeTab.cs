using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTab : MonoBehaviour
{
	public Button _leftButton;

	public Button _rigthButton;

	public GameObject _centerGroup;

	private int _curIndex;

	public int _moveDistance;

	public int _showCount;

	[Header("需要移动的元素")]
	public List<GameObject> _items;

	private void Start()
	{
		_leftButton.onClick.AddListener(LeftClick);
		_rigthButton.onClick.AddListener(RightClick);
		refreshButton();
	}

	private void LeftClick()
	{
		Vector3 localPosition = _centerGroup.transform.localPosition;
		if (_curIndex + 1 <= _items.Count - _showCount)
		{
			_centerGroup.transform.DOLocalMove(new Vector3(localPosition.x - (float)_moveDistance, localPosition.y, localPosition.z), 0.5f);
			_curIndex++;
		}
		refreshButton();
	}

	private void RightClick()
	{
		_ = _rigthButton.GetComponent<Image>().color;
		Vector3 localPosition = _centerGroup.transform.localPosition;
		if (_curIndex - 1 >= 0)
		{
			_centerGroup.transform.DOLocalMove(new Vector3(localPosition.x + (float)_moveDistance, localPosition.y, localPosition.z), 0.5f);
			_curIndex--;
		}
		refreshButton();
	}

	private void refreshButton()
	{
		Color color = _rigthButton.GetComponent<Image>().color;
		Color color2 = _leftButton.GetComponent<Image>().color;
		if (_curIndex == 0)
		{
			_rigthButton.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.5f);
		}
		else
		{
			_rigthButton.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1f);
		}
		if (_curIndex == _items.Count - _showCount)
		{
			_leftButton.GetComponent<Image>().color = new Color(color2.r, color2.g, color2.b, 0.5f);
		}
		else
		{
			_leftButton.GetComponent<Image>().color = new Color(color2.r, color2.g, color2.b, 1f);
		}
	}

	private void Update()
	{
	}
}
