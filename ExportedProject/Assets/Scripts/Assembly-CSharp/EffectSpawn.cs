using UnityEngine;

public class EffectSpawn : MonoBehaviour
{
	public GameObject[] effectGos;

	public Transform canvasTrans;

	private void Start()
	{
		InvokeRepeating("CreateEffectGo", 0f, 2f);
	}

	private void CreateEffectGo()
	{
		int num = Random.Range(0, 2);
		base.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0, 45)));
		Object.Instantiate(effectGos[num], base.transform.position, base.transform.rotation).transform.SetParent(canvasTrans);
	}
}
