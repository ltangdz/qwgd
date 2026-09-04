using UnityEngine;

public class EffectMove : MonoBehaviour
{
	public float moveSpeed;

	private float timeVal;

	private int randomYPos;

	private void Start()
	{
		Object.Destroy(base.gameObject, 10f);
	}

	private void Update()
	{
		base.transform.Translate(-base.transform.right * moveSpeed * Time.deltaTime);
		if (timeVal >= 1f)
		{
			timeVal = 0f;
			randomYPos = Random.Range(-1, 2);
		}
		else
		{
			base.transform.Translate(base.transform.up * randomYPos * moveSpeed * Time.deltaTime / 5f);
			timeVal += Time.deltaTime;
		}
	}
}
