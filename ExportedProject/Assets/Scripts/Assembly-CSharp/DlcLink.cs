using UnityEngine;
using UnityEngine.UI;

public class DlcLink : MonoBehaviour
{
	public string link;

	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(delegate
		{
		});
	}
}
