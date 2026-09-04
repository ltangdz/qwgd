using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ErrorDialog : MonoBehaviour
{
	public Image redbk;

	private void Start()
	{
		StartCoroutine(AutoHide(2f));
	}

	private IEnumerator AutoHide(float s)
	{
		yield return new WaitForSeconds(s);
		GetComponent<Animator>().Play("ani_hidedialog");
		yield return new WaitForSeconds(1.5f);
		Object.Destroy(base.gameObject);
	}
}
