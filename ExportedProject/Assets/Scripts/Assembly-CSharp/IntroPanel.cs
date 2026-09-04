using System.Collections;
using UnityEngine;

public class IntroPanel : MonoBehaviour
{
	private GameManager gameManager;

	public void NextPanel()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Debug.Log("shownextpanel");
		StartCoroutine(ShowNextPanel());
	}

	private IEnumerator ShowNextPanel()
	{
		yield return new WaitForSeconds(3f);
		gameManager.ShowFloatBox();
		yield return new WaitForSeconds(2f);
		base.transform.parent.parent.GetComponent<LoginCanvas>().beginCanvas.SetActive(value: true);
		base.gameObject.SetActive(value: false);
	}
}
