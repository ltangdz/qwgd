using UnityEngine;

public class Close : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void CloseAni()
	{
		base.gameObject.SetActive(value: false);
	}
}
