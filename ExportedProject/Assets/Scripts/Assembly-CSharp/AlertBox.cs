using UnityEngine;

public class AlertBox : MonoBehaviour
{
	private void Start()
	{
		if (base.transform.parent.Find("dialog") == null)
		{
			Invoke("Hide", 4f);
			base.name = "dialog";
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void Hide()
	{
		Object.Destroy(base.gameObject);
	}
}
