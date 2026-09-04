using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InvadeLoadingDLC : MonoBehaviour
{
	public List<Image> brackets;

	public List<GameObject> loadList;

	public Button btngoon;

	public GameObject tip;

	public GameObject wrongTip;

	private void Start()
	{
		StartCoroutine(StartLoad());
	}

	private IEnumerator StartLoad()
	{
		loadList = new List<GameObject>(GameObject.FindGameObjectsWithTag("ruqinloading"));
		loadList.Sort((GameObject a, GameObject b) => int.Parse(a.name).CompareTo(int.Parse(b.name)));
		for (int i = 0; i < loadList.Count; i++)
		{
			Image component = loadList[i].GetComponent<Image>();
			Sequence s = DOTween.Sequence();
			s.Append(component.DOFade(1f, 0.08f));
			s.Append(component.DOFade(0.6f, 0.08f));
			s.Append(component.DOFade(0.9f, 0.08f));
			yield return new WaitForSeconds(0.18f);
		}
		yield return new WaitForSeconds(0.5f);
		InvadeEvent.Instance.NoticeStepFinished(1, isSuccess: true);
	}
}
