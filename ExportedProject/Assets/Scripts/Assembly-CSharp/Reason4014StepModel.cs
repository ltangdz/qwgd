public class Reason4014StepModel
{
	private string _titleKey;

	private string _sampleKey;

	private string _messageKey;

	private string _groupKey;

	private int _sort;

	public string SampleKey
	{
		get
		{
			return _sampleKey;
		}
		set
		{
			_sampleKey = value;
		}
	}

	public string TitleKey
	{
		get
		{
			return _titleKey;
		}
		set
		{
			_titleKey = value;
		}
	}

	public string MessageKey
	{
		get
		{
			return _messageKey;
		}
		set
		{
			_messageKey = value;
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

	public int Sort
	{
		get
		{
			return _sort;
		}
		set
		{
			_sort = value;
		}
	}

	public Reason4014StepModel(string titleKey, string messageKey, string sampleKey, string groupKey, int sort)
	{
		_sampleKey = sampleKey;
		_titleKey = titleKey;
		_messageKey = messageKey;
		_groupKey = groupKey;
		_sort = sort;
	}
}
