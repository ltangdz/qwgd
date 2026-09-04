using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DuikangPoint : MonoBehaviour
{
	public int pointid;

	public Button btnPoint;

	public Image pointLight;

	[SerializeField]
	private GameObject ball;

	[SerializeField]
	private List<Transform> points = new List<Transform>();

	public Vector3 startpos;

	private List<Vector3> point_vector3s = new List<Vector3>();

	[SerializeField]
	private Transform redpoint;

	[SerializeField]
	private Sprite bluesprite;

	[SerializeField]
	private DuikangDialog duikangDialog;

	public bool isblue;

	private GameManager gameManager;

	public void Setblue()
	{
		isblue = true;
		duikangDialog.Check(pointid);
	}

	private void Awake()
	{
		startpos = ball.transform.localPosition;
		redpoint.localPosition = startpos;
		for (int i = 0; i < points.Count; i++)
		{
			point_vector3s.Add(points[i].transform.localPosition);
		}
		Init();
	}

	private void Init()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (!duikangDialog.iscansendhack)
		{
			return;
		}
		int num = Random.Range(0, 3);
		Sequence sequence = DOTween.Sequence();
		sequence.AppendInterval(num);
		sequence.Append(ball.transform.DOLocalPath(point_vector3s.ToArray(), 5f, PathType.CatmullRom));
		sequence.OnComplete(delegate
		{
			if (isblue)
			{
				GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("Duikang/bluepoint"), base.transform, instantiateInWorldSpace: false);
				gameObject.transform.localPosition = startpos;
				gameObject.transform.SetAsFirstSibling();
				ball = gameObject;
			}
			else
			{
				GameObject gameObject2 = (GameObject)Object.Instantiate(Resources.Load("Duikang/redpoint"), base.transform, instantiateInWorldSpace: false);
				gameObject2.transform.localPosition = startpos;
				gameObject2.transform.SetAsFirstSibling();
				ball = gameObject2;
			}
			Init();
		});
	}

	public void ShowVanSql()
	{
		btnPoint.gameObject.SetActive(value: true);
		StartCoroutine(Light());
	}

	public void CanClick()
	{
		bool opencombat = false;
		btnPoint.onClick.AddListener(delegate
		{
			if (!opencombat)
			{
				opencombat = true;
				gameManager.ShowFloatBox();
				Invoke("LoadCombat", 2f);
			}
		});
	}

	private void LoadCombat()
	{
		Object.Instantiate(Resources.Load<GameObject>("Duikang/combatvan"), gameManager.homeScene.middle).GetComponent<CombatVan>().duikangDialog = duikangDialog;
	}

	private IEnumerator Light()
	{
		while (true)
		{
			pointLight.DOFade(0.3f, 0.5f);
			yield return new WaitForSeconds(0.5f);
			pointLight.DOFade(1f, 0.5f);
			yield return new WaitForSeconds(0.5f);
		}
	}
}
