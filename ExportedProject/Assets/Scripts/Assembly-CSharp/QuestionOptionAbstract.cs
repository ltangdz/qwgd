using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class QuestionOptionAbstract : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	private string _groupKey;

	private string _curKey;

	private bool _isSelected;

	public string GroupKey
	{
		get
		{
			return _groupKey;
		}
		set
		{
			_groupKey = value;
		}
	}

	public string CurKey
	{
		get
		{
			return _curKey;
		}
		set
		{
			_curKey = value;
		}
	}

	protected abstract void SelectedUI();

	protected abstract void UnSelectedUI();

	protected abstract void InitUI();

	protected abstract void SuccessUI();

	protected abstract void FailUI();

	protected abstract void resetUI();

	public void Init(string groupKey, string key)
	{
		_groupKey = groupKey;
		_curKey = key;
		InitUI();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		QuestionOptionEvent.Instance.Click(_groupKey, _curKey);
	}

	private void NoticeValidResult(string groupKey, List<string> successList, List<string> failList, bool success)
	{
		if (groupKey != _groupKey)
		{
			return;
		}
		if (!success && groupKey == "^95DEBD19-49A3-263A-38A3-E21C63F47D96")
		{
			FailUI();
			return;
		}
		if (successList.Contains(_curKey))
		{
			SuccessUI();
		}
		if (failList.Contains(_curKey))
		{
			FailUI();
		}
	}

	private void SelectedAction(string groupKey, List<string> selectedList)
	{
		if (groupKey != _groupKey)
		{
			return;
		}
		resetUI();
		if (selectedList.Contains(_curKey))
		{
			if (!_isSelected)
			{
				_isSelected = true;
				SelectedUI();
			}
		}
		else if (_isSelected)
		{
			_isSelected = false;
			UnSelectedUI();
		}
	}

	private void OnEnable()
	{
		QuestionOptionEvent.Instance.onSelectedList += SelectedAction;
		QuestionOptionEvent.Instance.onNoticeValid += NoticeValidResult;
	}

	private void OnDisable()
	{
		QuestionOptionEvent.Instance.onSelectedList -= SelectedAction;
		QuestionOptionEvent.Instance.onNoticeValid -= NoticeValidResult;
	}
}
