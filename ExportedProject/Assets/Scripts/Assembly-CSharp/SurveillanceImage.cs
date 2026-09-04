using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class SurveillanceImage : MonoBehaviour
{
	public UILineRenderer lineRenderer;

	private List<Vector2> vecs = new List<Vector2>();

	public Transform mapGroup;

	private void Start()
	{
		Init();
	}

	private void Update()
	{
	}

	public void Init()
	{
		vecs = new List<Vector2>();
		for (int i = 0; i < 5; i++)
		{
			int num = 0;
			int num2 = 0;
			switch (i)
			{
			case 0:
				num = Random.Range(-431, -280);
				break;
			case 1:
				num = Random.Range(-223, -106);
				break;
			case 2:
				num = Random.Range(-43, 103);
				break;
			case 3:
				num = Random.Range(156, 267);
				break;
			case 4:
				num = Random.Range(314, 425);
				break;
			}
			int num3 = 1;
			num2 = Random.Range(-158 + num3 * 60, -158 + num3 * 60 + 60);
			vecs.Add(new Vector2(num, num2));
		}
		for (int j = 0; j < vecs.Count; j++)
		{
			AddNewDot2(vecs[j]);
		}
		DrawLine();
	}

	private void AddNewDot2(Vector2 vector2)
	{
		((GameObject)Object.Instantiate(Resources.Load("surveillance_dot"), mapGroup)).GetComponent<SurveillanceDot>().InitImage(1, "^hospital_label28", vector2);
	}

	public void DrawLine()
	{
		lineRenderer.Points = vecs.ToArray();
	}
}
