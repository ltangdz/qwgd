using UnityEngine;

public class CaiDan05 : MonoBehaviour
{
	public GameObject zimu;

	private void ShowZimu()
	{
		zimu.SetActive(value: true);
	}

	public void Init()
	{
		Invoke("ShowZimu", 2.5f);
	}
}
