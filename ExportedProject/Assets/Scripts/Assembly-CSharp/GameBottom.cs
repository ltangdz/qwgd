using UnityEngine;

public class GameBottom : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log("开始接触");
		Debug.Log(collider.name);
		Object.Destroy(collider.gameObject);
	}
}
