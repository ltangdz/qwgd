using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardSpawn : MonoBehaviour
{
	public Animator animator;

	public List<GameObject> points = new List<GameObject>();

	private List<Vector3> point_vector3s = new List<Vector3>();

	private void Start()
	{
		for (int i = 0; i < points.Count; i++)
		{
			point_vector3s.Add(points[i].transform.localPosition);
		}
		StartCoroutine(StartAnimation());
	}

	private IEnumerator StartAnimation()
	{
		yield return new WaitForSeconds(0f);
		animator.enabled = true;
		Object.Destroy(base.gameObject, 15f);
		base.transform.DOLocalPath(point_vector3s.ToArray(), 1.2f, PathType.CatmullRom).SetEase(Ease.OutQuad);
	}

	private void FixedUpdate()
	{
	}
}
