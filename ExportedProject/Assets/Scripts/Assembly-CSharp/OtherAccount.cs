using UnityEngine;

public class OtherAccount : MonoBehaviour
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
