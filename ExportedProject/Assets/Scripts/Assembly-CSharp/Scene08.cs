using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene08 : MonoBehaviour
{
	public List<Sprite> deng1;

	public List<Sprite> deng2;

	public ParticleSystem fog;

	private Image d1;

	private Image d2;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		d1 = base.transform.Find("img_d1").GetComponent<Image>();
		d2 = base.transform.Find("img_d2").GetComponent<Image>();
		StartCoroutine(Light(d1, deng1));
		StartCoroutine(Light(d2, deng2));
		StartCoroutine(FogChange());
		StartCoroutine(ShowLabel());
		gameManager.Esc.GetComponent<HoldEsc>().sceneName = base.transform.parent.name;
		gameManager.soundManager.PlaySoundLoop(15);
		GetComponent<Canvas>().worldCamera = Camera.main;
	}

	private IEnumerator Light(Image deng, List<Sprite> dengList)
	{
		for (int i = 0; i < dengList.Count; i++)
		{
			yield return new WaitForSeconds(0.2f);
			deng.sprite = dengList[i];
		}
		StartCoroutine(Light(deng, dengList));
	}

	private IEnumerator FogChange()
	{
		yield return new WaitForSeconds(5f);
		if (!gameManager.holdEsc)
		{
			gameManager.startAniManager.ChangeScene("Canvas09");
			gameManager.soundManager.Stop();
		}
	}

	private IEnumerator ShowLabel()
	{
		GameObject txt = base.transform.parent.Find("Canvas/zimu_cn").gameObject;
		yield return new WaitForSeconds(1f);
		GameObject.Find("GameManager").GetComponent<GameManager>().ShowLabel(txt);
		yield return new WaitForSeconds(2.5f);
		GameObject.Find("GameManager").GetComponent<GameManager>().HideLabel(txt);
	}
}
