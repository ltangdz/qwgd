using System.Collections;
using UnityEngine;

public class Host : MonoBehaviour
{
	public GameObject eye;

	public GameObject mouth;

	private void Start()
	{
		StartCoroutine(CloseEye());
		StartCoroutine(OpenMouth());
	}

	private IEnumerator CloseEye()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.5f);
			mouth.SetActive(value: true);
			yield return new WaitForSeconds(0.2f);
			mouth.SetActive(value: false);
			for (int i = 0; i < 4; i++)
			{
				yield return new WaitForSeconds(0.2f);
				mouth.SetActive(value: true);
				yield return new WaitForSeconds(0.2f);
				mouth.SetActive(value: false);
			}
		}
	}

	private IEnumerator OpenMouth()
	{
		while (true)
		{
			yield return new WaitForSeconds(3f);
			eye.SetActive(value: true);
			yield return new WaitForSeconds(0.1f);
			eye.SetActive(value: false);
			yield return new WaitForSeconds(2f);
			eye.SetActive(value: true);
			yield return new WaitForSeconds(0.1f);
			eye.SetActive(value: false);
			yield return new WaitForSeconds(4f);
			eye.SetActive(value: true);
			yield return new WaitForSeconds(0.1f);
			eye.SetActive(value: false);
		}
	}
}
