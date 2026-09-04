using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ReasonOptionGroup : QuestionGroup
{
	[Header("必填，RadioText  RadioImage")]
	public string _groupType;

	public Text _nameText;

	public Transform _containerTransform;

	private List<GameObject> _objects = new List<GameObject>();

	private void Start()
	{
		InitUI(_groupType, base.OptionKeys, base.Answers, base.GroupKey);
	}

	public void InitUI(string groupType, List<string> optionKeys, List<string> answers, string groupKey)
	{
		base.GroupKey = groupKey;
		base.OptionKeys = optionKeys;
		base.Answers = answers;
		_groupType = groupType;
		foreach (GameObject @object in _objects)
		{
			Object.Destroy(@object);
		}
		for (int i = 0; i < base.OptionKeys.Count; i++)
		{
			GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("_DLC/Prefabs/reason/" + _groupType), _containerTransform);
			if (_groupType == "RadioText" || _groupType == "step4RadioText")
			{
				gameObject.GetComponent<ReasonOptionText>().Init(_groupKey, base.OptionKeys[i]);
			}
			else if (_groupType == "RadioImage")
			{
				gameObject.GetComponent<ReasonOptionImage>().Init(_groupKey, base.OptionKeys[i]);
			}
			_objects.Add(gameObject);
		}
		_nameText.text = I18N.instance.getValue(base.GroupKey);
	}
}
