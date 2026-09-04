using System.Collections.Generic;
using UnityEngine;

public class QuestionGroup : MonoBehaviour
{
	public string _groupKey;

	[SerializeField]
	private List<string> _curSelectedNames = new List<string>();

	[SerializeField]
	private List<string> _answers;

	[SerializeField]
	private List<string> _optionKeys;

	public List<string> OptionKeys
	{
		get
		{
			return _optionKeys;
		}
		set
		{
			_optionKeys = value;
		}
	}

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

	public List<string> CurSelectedNames
	{
		get
		{
			return _curSelectedNames;
		}
		set
		{
			_curSelectedNames = value;
		}
	}

	public List<string> Answers
	{
		get
		{
			return _answers;
		}
		set
		{
			_answers = value;
		}
	}

	private void Click(string groupKey, string itemKey)
	{
		if (groupKey != _groupKey)
		{
			return;
		}
		if (_answers.Count == 1)
		{
			if (!_curSelectedNames.Contains(itemKey))
			{
				_curSelectedNames.Clear();
				_curSelectedNames.Add(itemKey);
			}
		}
		else if (_answers.Count > 1)
		{
			if (_curSelectedNames.Contains(itemKey))
			{
				_curSelectedNames.Remove(itemKey);
			}
			else
			{
				_curSelectedNames.Add(itemKey);
			}
		}
		QuestionOptionEvent.Instance.Selected(_groupKey, _curSelectedNames);
	}

	public bool ValidResult()
	{
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		for (int i = 0; i < _curSelectedNames.Count; i++)
		{
			string item = _curSelectedNames[i];
			if (_answers.Contains(item))
			{
				list.Add(item);
			}
			else
			{
				list2.Add(item);
			}
		}
		bool flag = ((list2.Count <= 0 && list.Count != 0 && list.Count == _answers.Count) ? true : false);
		QuestionOptionEvent.Instance.NoticeValidResult(_groupKey, list, list2, flag);
		return flag;
	}

	private void Valid(string groupKey)
	{
		if (!(_groupKey != groupKey))
		{
			ValidResult();
		}
	}

	private void OnEnable()
	{
		QuestionOptionEvent.Instance.onClick += Click;
		QuestionOptionEvent.Instance.onValid += Valid;
	}

	private void OnDisable()
	{
		QuestionOptionEvent.Instance.onClick -= Click;
		QuestionOptionEvent.Instance.onValid -= Valid;
	}
}
