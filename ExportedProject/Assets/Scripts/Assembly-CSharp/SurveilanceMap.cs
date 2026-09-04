using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class SurveilanceMap : MonoBehaviour
{
	public UILineRenderer lineRenderer;

	public Button btnClose;

	public Transform mapGroup;

	private List<Vector2> linePST = new List<Vector2>();

	private List<Vector2> pointPST = new List<Vector2>();

	private GameManager gameManager;

	private void Start()
	{
		btnClose.onClick.AddListener(delegate
		{
			Object.Destroy(base.gameObject);
		});
	}

	public void Init(string id, GameManager gm)
	{
		gameManager = gm;
		linePST = gameManager.player.playerdata.surveillanceRecord[id];
		pointPST = gameManager.player.playerdata.surveillancelist[id];
		if (linePST.Count != 0 && pointPST.Count != 0)
		{
			lineRenderer.Points = linePST.ToArray();
			string[] array = gameManager.dataManager.dic36[id].searchcontent.Split(';');
			for (int i = 0; i < 5; i++)
			{
				AddNewDot2(i + 1, pointPST[i], array[i]);
			}
		}
	}

	private void AddNewDot2(int pos, Vector2 vector2, string key)
	{
		((GameObject)Object.Instantiate(Resources.Load("surveillance_dot"), mapGroup)).GetComponent<SurveillanceDot>().InitImage(pos, key, vector2);
	}
}
