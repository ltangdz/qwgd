using System.Collections;
using UnityEngine;

public class Scene07 : MonoBehaviour
{
	private GameManager gameManager;

	public GameObject txt;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Invoke("Change", 5f);
		StartCoroutine(ShowLabel());
		gameManager.Esc.GetComponent<HoldEsc>().sceneName = base.transform.parent.name;
	}

	public void Change()
	{
		if (!gameManager.holdEsc)
		{
			gameManager.startAniManager.ChangeScene("Canvas08");
		}
	}

	private IEnumerator ShowLabel()
	{
		yield return new WaitForSeconds(1f);
		GameObject.Find("GameManager").GetComponent<GameManager>().ShowLabel(txt);
		yield return new WaitForSeconds(2.5f);
		GameObject.Find("GameManager").GetComponent<GameManager>().HideLabel(txt);
	}
}
