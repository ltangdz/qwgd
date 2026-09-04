using UnityEngine;

public class ChaseGame : MonoBehaviour
{
	public EdgeCollider2D _edgeCollider2D;

	private void Start()
	{
	}

	private void OnMouseEnter()
	{
		Debug.Log("12321");
	}

	private void OnMouseDown()
	{
		Debug.Log(_edgeCollider2D.points);
	}

	private void OnMouseDrag()
	{
		Debug.Log("OnMouseDrag");
	}

	private void Update()
	{
	}
}
