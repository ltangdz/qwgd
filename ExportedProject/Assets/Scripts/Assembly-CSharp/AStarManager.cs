using System.Collections.Generic;
using Dlc.Catch.model;
using UnityEngine;

public class AStarManager
{
	public List<WayPoint> wayPoints;

	public List<WayPoint> openList = new List<WayPoint>();

	public List<WayPoint> closeList = new List<WayPoint>();

	private WayPoint _startWay;

	private WayPoint _endWay;

	private bool isInit;

	public void Init(List<WayPoint> points)
	{
		wayPoints = points;
	}

	public List<WayPoint> FindPath(Vector2 start, Vector2 end, WayPoint startWay, WayPoint endWay, List<WayPoint> banPoints)
	{
		for (int i = 0; i < wayPoints.Count; i++)
		{
			wayPoints[i].ResetData();
		}
		openList.Clear();
		closeList.Clear();
		List<WayPoint> list = new List<WayPoint>();
		if (startWay == endWay)
		{
			list.Add(startWay);
			return list;
		}
		startWay.aParent = null;
		startWay.h = Mathf.Abs(start.x - end.x) + Mathf.Abs(start.y - end.y);
		startWay.f = startWay.h + startWay.g;
		closeList.Add(startWay);
		_startWay = startWay;
		_endWay = endWay;
		while (true)
		{
			List<WayPoint> belongPaths = startWay.BelongPaths;
			for (int j = 0; j < belongPaths.Count; j++)
			{
				WayPoint wayPoint = belongPaths[j];
				if ((banPoints != null && banPoints.Count > 0 && banPoints.Contains(wayPoint)) || closeList.Contains(wayPoint) || closeList.Contains(wayPoint))
				{
					continue;
				}
				if (wayPoint.name == _endWay.name)
				{
					WayPoint wayPoint2 = wayPoint;
					wayPoint.aParent = startWay;
					list.Add(wayPoint);
					while (wayPoint2.aParent != null)
					{
						list.Add(wayPoint2.aParent);
						wayPoint2 = wayPoint2.aParent;
					}
					list.Reverse();
					return list;
				}
				wayPoint.aParent = startWay;
				wayPoint.g = Mathf.Abs(Vector2.Distance(startWay._centerPosition, wayPoint._centerPosition)) + startWay.g;
				wayPoint.h = Mathf.Abs(wayPoint._centerPosition.x - end.x) + Mathf.Abs(wayPoint._centerPosition.y - end.y);
				wayPoint.f = wayPoint.h + wayPoint.g;
				openList.Add(wayPoint);
			}
			if (openList.Count <= 0)
			{
				break;
			}
			openList.Sort(SortOpenList);
			WayPoint wayPoint3 = openList[0];
			startWay = wayPoint3;
			openList.RemoveAt(0);
			closeList.Add(wayPoint3);
		}
		return list;
	}

	private int SortOpenList(WayPoint a, WayPoint b)
	{
		if (a.f > b.f)
		{
			return 1;
		}
		if (a.f == b.f)
		{
			return 1;
		}
		return -1;
	}
}
