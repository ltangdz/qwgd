using UnityEngine;

public class DefaultAccount : MonoBehaviour
{
	private string id;

	public string GetId
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
		}
	}

	private void Start()
	{
	}

	public void Reset(string accountID)
	{
		accountID = id;
	}
}
