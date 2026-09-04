using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scene06 : MonoBehaviour
{
	private Animator ani;

	public GameObject parent;

	public List<GameObject> police;

	public List<GameObject> person;

	private float mohuVal = 6f;

	private GameManager gameManager;

	public GameObject txt;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		ani = GetComponent<Animator>();
		StartCoroutine(ShowLabel());
		gameManager.Esc.GetComponent<HoldEsc>().sceneName = base.transform.parent.name;
		gameManager.soundManager.PlaySoundLoop(15);
	}

	public void OpenDoor()
	{
		ani.SetBool("openDoor", value: true);
	}

	public void Vague()
	{
		for (int i = 0; i < person.Count; i++)
		{
			person[i].transform.Find("Image").GetComponent<CanvasGroup>().DOFade(1f, 1f);
		}
	}

	public void ShowPolice()
	{
		for (int i = 0; i < police.Count; i++)
		{
			police[i].transform.Find("Image").GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
		}
	}

	public void Change(string scene)
	{
		if (!gameManager.holdEsc)
		{
			gameManager.startAniManager.ChangeScene(scene);
			gameManager.soundManager.Stop();
		}
	}

	private IEnumerator ShowLabel()
	{
		yield return new WaitForSeconds(1f);
		GameObject.Find("GameManager").GetComponent<GameManager>().ShowLabel(txt);
		yield return new WaitForSeconds(2f);
		GameObject.Find("GameManager").GetComponent<GameManager>().HideLabel(txt);
	}
}
