using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Dlc.Catch.model;
using UnityEngine;

public class CatchEnemy : CarBase
{
	private enum GameOver
	{
		GAMEING = 0,
		SUCCESS = 1,
		FAIL = 2
	}

	private enum EnemyStatus
	{
		RANDOM = 0,
		EXIT = 1,
		ESCAPE = 2
	}

	private List<PoliceAI> _polices = new List<PoliceAI>();

	private GameOver _gameOverType;

	private EnemyStatus _enemyStatus;

	private int _moveNextCount;

	private int _maxMoveNextCount = 8;

	private long _randomStartTime;

	private int _exitSecond = 600;

	private float _minSafeDistance;

	private PoliceAI _curAvoidPolice;

	private bool _isStart;

	private List<PoliceAI> _dangerPoliceAis = new List<PoliceAI>();

	private int _crashCount = 3;

	private bool isCrashing;

	private float _safeTime = 10f;

	private bool isCanExit;

	private bool isChange;

	public List<WayPoint> startWay;

	private GameManager _gameManager;

	private long lastTime;

	private void SetEnemyStatus(EnemyStatus status)
	{
		_enemyStatus = status;
		if (_enemyStatus == EnemyStatus.EXIT)
		{
			Debug.Log(" EnemyStatus.EXIT");
			FindExitAI();
		}
		else if (_enemyStatus == EnemyStatus.RANDOM)
		{
			Debug.Log(" EnemyStatus.RANDOM");
			if (isCanExit)
			{
				FindExitAI();
			}
			else
			{
				RandomPathAI();
			}
		}
	}

	private void Start()
	{
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_polices = new List<PoliceAI>();
		GameObject[] array = GameObject.FindGameObjectsWithTag("Police");
		foreach (GameObject gameObject in array)
		{
			_polices.Add(gameObject.GetComponent<PoliceAI>());
		}
	}

	protected override void InitData()
	{
		base.CarType = CatchCarType.EMENY;
		string[] array = new string[1] { "wall1 (222)" };
		string value = array[UnityEngine.Random.Range(0, array.Length)];
		for (int i = 0; i < base.WayPoints.Count; i++)
		{
			WayPoint wayPoint = base.WayPoints[i];
			if (wayPoint.name.Equals(value))
			{
				base.CurWayPoint = wayPoint;
				base.RT.anchoredPosition = base.CurWayPoint.RandomPosition();
				break;
			}
		}
	}

	private IEnumerator SetIsCrashing()
	{
		isCrashing = true;
		yield return new WaitForSeconds(5f);
		isCrashing = false;
	}

	protected override void MovedToFinalPosition()
	{
		_ = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		if (base.CurWayPoint.IsExit)
		{
			Debug.Log("到达终点");
			CatchEvent.Instance.NoticeNextEvent(CatchEventEnum.GAME_FAIL);
			return;
		}
		List<WayPoint> belongPaths = base.CurWayPoint.BelongPaths;
		for (int i = 0; i < belongPaths.Count; i++)
		{
			if (belongPaths[i].IsExit)
			{
				Debug.Log("到达终点");
				CatchEvent.Instance.NoticeNextEvent(CatchEventEnum.GAME_FAIL);
				return;
			}
		}
		WayPoint wayPoint = NextWayPoint();
		if (wayPoint != null)
		{
			base.FinalTargetWayPoint = wayPoint;
			if (!FindPath(wayPoint.RandomPosition()))
			{
				if (_crashCount > 0)
				{
					SetIsCrashing();
					RandomPathAI();
				}
				Debug.Log("没有路了222");
			}
		}
		else
		{
			if (_crashCount > 0)
			{
				StartCoroutine(SetIsCrashing());
				RandomPathAI();
			}
			Debug.Log("没有路了");
		}
	}

	private void RandomNoEludeWayPoints()
	{
	}

	private IEnumerator StartGame()
	{
		yield return new WaitForSeconds(0.5f);
		GameObject.Find("GameManager").GetComponent<GameManager>().soundManager.PlayCatchSoundLoop(0);
		_isStart = true;
		int index = UnityEngine.Random.Range(0, startWay.Count);
		base.FinalTargetWayPoint = startWay[index];
		FindPath(base.FinalTargetWayPoint.RandomPosition());
		InvokeRepeating("Alert", 0.1f, 0.2f);
		_randomStartTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000000;
	}

	private void Alert()
	{
		_eludeWayPoints.Clear();
		if (isCrashing || isCanExit)
		{
			return;
		}
		for (int i = 0; i < _dangerPoliceAis.Count; i++)
		{
			List<WayPoint> aStarPaths = _dangerPoliceAis[i]._aStarPaths;
			if (aStarPaths.Count <= 0)
			{
				continue;
			}
			if (aStarPaths.Count == 1)
			{
				WayPoint wayPoint = aStarPaths[0];
				if (wayPoint == base.CurWayPoint || base.CurWayPoint.BelongPaths.Contains(wayPoint))
				{
					_eludeWayPoints.Add(wayPoint);
				}
			}
			else if (aStarPaths.Count > 1)
			{
				WayPoint wayPoint2 = aStarPaths[1];
				if (wayPoint2 == base.CurWayPoint || base.CurWayPoint.BelongPaths.Contains(wayPoint2))
				{
					_eludeWayPoints.Add(wayPoint2);
				}
			}
		}
		for (int j = 0; j < _polices.Count; j++)
		{
			_eludeWayPoints.Add(_polices[j].CurWayPoint);
		}
		for (int k = 0; k < _aStarPaths.Count; k++)
		{
			WayPoint item = _aStarPaths[k];
			if (_eludeWayPoints.Contains(item))
			{
				if (base.FinalMovedPoints.Count > 2)
				{
					MovedToFinalPosition();
				}
				break;
			}
		}
	}

	private IEnumerator StartGuard()
	{
		yield return new WaitForSeconds(0.5f);
		Guard(1);
	}

	private void DangerPolice()
	{
		List<WayPoint> belongPaths = base.CurWayPoint._belongPaths;
		for (int i = 0; i < belongPaths.Count; i++)
		{
		}
		_ = base.CurWayPoint.PointType;
	}

	private WayPoint NextWayPoint()
	{
		_ = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		if (_randomStartTime != 0L && (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000000 - _randomStartTime > _exitSecond)
		{
			CatchEvent.Instance.NoticeNextEvent(CatchEventEnum.SHOW_EXIT);
			isCanExit = true;
			_eludeWayPoints.Clear();
			SetEnemyStatus(EnemyStatus.EXIT);
			return null;
		}
		int pointType = base.CurWayPoint.PointType;
		List<WayPoint> belongPaths = base.CurWayPoint.BelongPaths;
		int num = Mathf.Min(_wayPointHistoryIndexList.Count, 25);
		Mathf.Min(_wayPointHistoryIndexList.Count, 2);
		List<int> range = _wayPointHistoryIndexList.GetRange(_wayPointHistoryIndexList.Count - num, num);
		List<WayPoint> list = new List<WayPoint>();
		List<WayPoint> list2 = new List<WayPoint>();
		List<WayPoint> list3 = new List<WayPoint>();
		List<WayPoint> list4 = new List<WayPoint>();
		for (int i = 0; i < belongPaths.Count; i++)
		{
			WayPoint wayPoint = belongPaths[i];
			if (!isCanExit && wayPoint.isExitPath)
			{
				continue;
			}
			if (wayPoint.IsExit)
			{
				list4.Add(wayPoint);
			}
			int index = wayPoint.Index;
			if (_randomStartTime != 0L && (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000000 - _randomStartTime < _exitSecond && wayPoint.IsExit)
			{
				list4.Add(wayPoint);
				continue;
			}
			if (pointType == 0)
			{
				if (HavePolice(wayPoint.Index, isSingle: false) && !isCrashing)
				{
					list4.Add(wayPoint);
					continue;
				}
			}
			else
			{
				bool flag = false;
				for (int j = 0; j < _polices.Count; j++)
				{
					PoliceAI policeAI = _polices[j];
					if (Vector2.Distance(policeAI.RT.anchoredPosition, wayPoint._centerPosition) < policeAI.RT.sizeDelta.x)
					{
						flag = true;
					}
				}
				if (flag && !isCrashing)
				{
					list4.Add(wayPoint);
					continue;
				}
				int num2 = 0;
				List<WayPoint> belongPaths2 = wayPoint.BelongPaths;
				List<int> list5 = new List<int>();
				bool flag2 = false;
				for (int k = 0; k < belongPaths2.Count; k++)
				{
					WayPoint wayPoint2 = belongPaths2[k];
					if (wayPoint2.name == base.CurWayPoint.name)
					{
						continue;
					}
					float num3 = Mathf.Abs(Vector2.Distance(base.RT.anchoredPosition, wayPoint.StartPoints[0]));
					float num4 = -1f;
					for (int l = 0; l < _polices.Count; l++)
					{
						PoliceAI policeAI2 = _polices[l];
						if (policeAI2.CurWayPoint.name == wayPoint2.name)
						{
							float num5 = Mathf.Abs(Vector2.Distance(policeAI2.RT.anchoredPosition, wayPoint.StartPoints[0]));
							if (num4 == -1f || num5 < num4)
							{
								num4 = num5;
							}
						}
					}
					if (num4 > -1f && num4 < num3)
					{
						flag2 = true;
						break;
					}
					if (num2 == 0 || wayPoint2._belongPaths.Count > num2)
					{
						num2 = wayPoint2._belongPaths.Count;
					}
				}
				if (flag2)
				{
					list5.Add(wayPoint.Index);
					continue;
				}
			}
			list3.Add(wayPoint);
			if (range.Contains(index))
			{
				list2.Add(wayPoint);
			}
			else if (base.CurWayPoint.name != wayPoint.name && wayPoint.SingleWays.Count > 0 && !wayPoint.SingleWays.Contains(base.CurWayPoint))
			{
				list2.Add(wayPoint);
			}
			else
			{
				list.Add(wayPoint);
			}
		}
		int num6 = -1;
		if (list.Count == 0 && list2.Count == 0)
		{
			return null;
		}
		if (_dangerPoliceAis != null && _dangerPoliceAis.Count > 0 && num6 != -1)
		{
			int index2 = -1;
			int[] array = new int[list3.Count];
			for (int m = 0; m < list3.Count; m++)
			{
				WayPoint nextWay = list3[m];
				int num7 = 0;
				for (int n = 0; n < _dangerPoliceAis.Count; n++)
				{
					PoliceAI policeAI3 = _dangerPoliceAis[n];
					if (nextWayToPoliceDistance(policeAI3, nextWay) > policeAI3.EnemyDistance)
					{
						num7++;
					}
				}
				array[m] = num7;
			}
			int num8 = array.Max();
			for (int num9 = 0; num9 < array.Length; num9++)
			{
				if (num8 == array[num9])
				{
					_ = list3[index2];
				}
			}
		}
		if (list.Count > 0)
		{
			WayPoint wayPoint3 = list[UnityEngine.Random.Range(0, list.Count)];
			if (wayPoint3.SingleWays.Count > 0 && !wayPoint3.SingleWays.Contains(base.CurWayPoint))
			{
				WayPoint pathPoint = wayPoint3.SingleWays[wayPoint3.SingleWays.Count - 1];
				return FindNextPoint(pathPoint);
			}
			return FindNextPoint(wayPoint3);
		}
		if (list2.Count > 0)
		{
			List<WayPoint> list6 = SortPriority(list2, num);
			if (_wayPointHistoryIndexList.Count > 0)
			{
				_ = _wayPointHistoryIndexList[_wayPointHistoryIndexList.Count - 1];
			}
			WayPoint wayPoint4 = list6[0];
			if (wayPoint4.SingleWays.Count > 0 && !wayPoint4.SingleWays.Contains(base.CurWayPoint))
			{
				wayPoint4 = wayPoint4.SingleWays[wayPoint4.SingleWays.Count - 1];
			}
			return FindNextPoint(wayPoint4);
		}
		return null;
	}

	private WayPoint FindNextPoint(WayPoint pathPoint)
	{
		WayPoint result = null;
		if (pathPoint.PointType == 1)
		{
			List<WayPoint> belongPaths = pathPoint.BelongPaths;
			for (int i = 0; i < belongPaths.Count; i++)
			{
				WayPoint wayPoint = belongPaths[i];
				if (!pathPoint.SingleWays.Contains(wayPoint) && base.CurWayPoint.PointType == 0 && wayPoint.name != base.CurWayPoint.name)
				{
					result = wayPoint;
					break;
				}
				result = pathPoint;
			}
			return result;
		}
		return pathPoint;
	}

	private bool HavePolice(int index, bool isSingle)
	{
		WayPoint wayPoint = base.WayPoints[index];
		List<WayPoint> list = ((!isSingle) ? new List<WayPoint>() : wayPoint.SingleWays);
		if (list.Count == 0)
		{
			list.Add(wayPoint);
		}
		bool flag = false;
		for (int i = 0; i < _polices.Count; i++)
		{
			WayPoint curWayPoint = _polices[i].CurWayPoint;
			PoliceAI policeAI = _polices[i];
			Vector2 anchoredPosition = policeAI.RT.anchoredPosition;
			Vector2 sizeDelta = policeAI.RT.sizeDelta;
			new Rect(anchoredPosition.x - sizeDelta.x / 2f, anchoredPosition.y - sizeDelta.y / 2f, sizeDelta.x, sizeDelta.y);
			for (int j = 0; j < list.Count; j++)
			{
				WayPoint wayPoint2 = list[j];
				if (curWayPoint.name == wayPoint2.name)
				{
					flag = true;
				}
			}
		}
		if (flag)
		{
			return true;
		}
		return false;
	}

	private List<WayPoint> SortPriority(List<WayPoint> wayPoints, int count)
	{
		List<WayPoint> list = new List<WayPoint>();
		if (wayPoints == null)
		{
			return list;
		}
		int num = Mathf.Min(_wayPointHistoryIndexList.Count, count);
		List<int> range = _wayPointHistoryIndexList.GetRange(_wayPointHistoryIndexList.Count - num, num);
		range.Reverse();
		Dictionary<WayPoint, int> dictionary = new Dictionary<WayPoint, int>();
		for (int i = 0; i < wayPoints.Count; i++)
		{
			WayPoint wayPoint = wayPoints[i];
			int num2 = 0;
			for (int j = 0; j < range.Count; j++)
			{
				if (range.Contains(wayPoint.Index))
				{
					num2++;
				}
			}
			dictionary[wayPoint] = num2;
		}
		foreach (KeyValuePair<WayPoint, int> item in dictionary.OrderBy(delegate(KeyValuePair<WayPoint, int> item)
		{
			KeyValuePair<WayPoint, int> keyValuePair = item;
			return keyValuePair.Value;
		}))
		{
			Console.WriteLine(item.Key.name.ToString() + " " + item.Value);
			list.Add(item.Key);
		}
		return list;
	}

	private void FindExitAI()
	{
		_moveNextCount = 0;
		Debug.Log("寻找出口");
		List<WayPoint> exitPoints = base.ExitPoints;
		int index = UnityEngine.Random.Range(0, exitPoints.Count);
		WayPoint wayPoint = exitPoints[index];
		Vector2 targetPoint = wayPoint.StartPoints[0];
		base.FinalTargetWayPoint = wayPoint;
		if (!FindPath(targetPoint))
		{
			FindPath(targetPoint);
		}
	}

	private void FindEludeWayPoint(bool isBanDirection)
	{
		_eludeWayPoints.Clear();
		for (int i = 0; i < _polices.Count; i++)
		{
			WayPoint curWayPoint = _polices[i].CurWayPoint;
			if (curWayPoint != null)
			{
				_eludeWayPoints.Add(curWayPoint);
			}
		}
	}

	private void RandomPathAI()
	{
		if (isCanExit)
		{
			FindExitAI();
			return;
		}
		_ = new string[16]
		{
			"wall1 (19)", "wall1 (43)", "wall1 (188)", "waypoint (173)", "wall1 (44)", "wall1 (189)", "waypoint (171)", "wall1 (19)", "waypoint (175)", "wall1 (35)",
			"wall1 (154)", "waypoint (73)", "waypoint (7)", "wall1 (135)", "waypoint (47)", "waypoint (48)"
		};
		long num = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		float num2 = 0f;
		int num3 = 0;
		int num4 = 0;
		float num5 = 0f;
		for (int i = 0; i < base.WayPoints.Count; i++)
		{
			float num6 = 0f;
			WayPoint wayPoint = base.WayPoints[i];
			if (_eludeWayPoints.Contains(wayPoint) || wayPoint.IsExit)
			{
				continue;
			}
			bool flag = false;
			if (!isCanExit && wayPoint.isExitPath)
			{
				continue;
			}
			for (int j = 0; j < _polices.Count; j++)
			{
				PoliceAI policeAI = _polices[j];
				if (policeAI.CurWayPoint.name == wayPoint.name)
				{
					flag = true;
					break;
				}
				num6 += Mathf.Abs(Vector2.Distance(policeAI.RT.anchoredPosition, wayPoint.StartPoints[0]));
			}
			if (!flag)
			{
				float num7 = Mathf.Abs(Vector2.Distance(base.RT.anchoredPosition, wayPoint.StartPoints[0]));
				if (i == 0 || num6 >= num2)
				{
					num2 = num6;
					num3 = i;
				}
				if (i == 0 || num7 >= num5)
				{
					num5 = num7;
					num4 = i;
				}
			}
		}
		int num8 = UnityEngine.Random.Range(0, 2);
		base.FinalTargetWayPoint = base.WayPoints[(num8 == 0) ? num3 : num4];
		FindPath(base.FinalTargetWayPoint.RandomPosition());
		long num9 = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		Debug.Log("randomAI" + (num9 - num));
	}

	private bool NoWay()
	{
		List<PoliceAI> list = new List<PoliceAI>();
		List<PoliceAI> list2 = new List<PoliceAI>();
		for (int i = 0; i < _polices.Count; i++)
		{
			PoliceAI policeAI = _polices[i];
			WayPoint curWayPoint = policeAI.CurWayPoint;
			Vector2 anchoredPosition = policeAI.RT.anchoredPosition;
			Vector2 sizeDelta = policeAI.RT.sizeDelta;
			new Rect(anchoredPosition.x - sizeDelta.x / 2f, anchoredPosition.y - sizeDelta.y / 2f, sizeDelta.x, sizeDelta.y);
			float num = Mathf.Abs(Vector2.Distance(anchoredPosition, base.RT.anchoredPosition));
			if (base.CurWayPoint.name == curWayPoint.name && num < sizeDelta.x / 3f)
			{
				return true;
			}
			for (int j = 0; j < base.RoundWayPoints.Count; j++)
			{
				WayPoint wayPoint = base.RoundWayPoints[j];
				if (curWayPoint.name == wayPoint.name)
				{
					if (wayPoint.name == base.CurWayPoint.name)
					{
						list.Add(policeAI);
					}
					list2.Add(policeAI);
				}
			}
		}
		List<WayPoint> list3 = new List<WayPoint>();
		Vector2 anchoredPosition2 = base.RT.anchoredPosition;
		if (list.Count > 0)
		{
			if (list.Count >= 2)
			{
				bool flag = false;
				bool flag2 = false;
				for (int k = 0; k < list.Count; k++)
				{
					Vector2 anchoredPosition3 = list[k].RT.anchoredPosition;
					if (base.CurWayPoint.Direction == WayPointDirection.HORIZONTAL)
					{
						if (anchoredPosition3.x > anchoredPosition2.x)
						{
							flag = true;
						}
						if (anchoredPosition3.x < anchoredPosition2.x)
						{
							flag2 = true;
						}
					}
					else
					{
						if (anchoredPosition3.y > anchoredPosition2.y)
						{
							flag = true;
						}
						if (anchoredPosition3.y < anchoredPosition2.y)
						{
							flag2 = true;
						}
					}
				}
				if (flag && flag2)
				{
					return true;
				}
			}
			PoliceAI policeAI2 = list[0];
			WayPoint wayPoint2 = null;
			Vector2 vector = policeAI2.RT.anchoredPosition - base.RT.anchoredPosition;
			wayPoint2 = ((base.CurWayPoint.Direction == WayPointDirection.VERTICAL) ? ((!(vector.x < 0f)) ? base.CurWayPoint.DownPoint : base.CurWayPoint.UpPoint) : ((!(vector.y < 0f)) ? base.CurWayPoint.LeftPoint : base.CurWayPoint.RightPoint));
			List<WayPoint> belongPaths = wayPoint2.BelongPaths;
			for (int l = 0; l < belongPaths.Count; l++)
			{
				if (!(belongPaths[l].name == base.CurWayPoint.name))
				{
					list3.Add(belongPaths[l]);
				}
			}
		}
		else
		{
			list3 = base.RoundWayPoints;
		}
		if (list3.Count == 0)
		{
			return false;
		}
		for (int m = 0; m < list3.Count; m++)
		{
			WayPoint wayPoint3 = list3[m];
			bool flag3 = false;
			for (int n = 0; n < _polices.Count; n++)
			{
				if (_polices[n].CurWayPoint.name == wayPoint3.name)
				{
					flag3 = true;
					break;
				}
			}
			if (!flag3)
			{
				return false;
			}
		}
		return true;
	}

	private void TOExitGuard()
	{
		if (_enemyStatus != EnemyStatus.EXIT)
		{
			return;
		}
		List<Vector2> finalMovedPoints = base.FinalMovedPoints;
		if (finalMovedPoints.Count <= 0)
		{
			return;
		}
		Vector2 point = finalMovedPoints[0];
		WayPoint wayPoint = null;
		for (int i = 0; i < base.RoundWayPoints.Count; i++)
		{
			WayPoint wayPoint2 = base.RoundWayPoints[i];
			if (wayPoint2.ItemRect.Contains(point))
			{
				if (wayPoint2.PointType == 0)
				{
					wayPoint = wayPoint2;
					break;
				}
				wayPoint = wayPoint2;
			}
		}
		if (!(wayPoint == null) && HavePolice(wayPoint.Index, isSingle: false))
		{
			base.FinalMovedPoints.Clear();
			SetEnemyStatus(EnemyStatus.RANDOM);
		}
	}

	private void Guard(int guardGrid)
	{
		Debug.Log("Guard");
		if (_enemyStatus == EnemyStatus.EXIT)
		{
			FindPath(base.CurTargetPoint);
		}
	}

	private void GuardRandom(int guardGrid)
	{
	}

	private float nextWayToPoliceDistance(PoliceAI ai, WayPoint nextWay)
	{
		_ = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		float num = 0f;
		List<WayPoint> list = _aStarManager.FindPath(nextWay._centerPosition, ai.RT.anchoredPosition, nextWay, ai.CurWayPoint, null);
		if (list.Count == 0)
		{
			return 0f;
		}
		if (list.Count == 1)
		{
			return Mathf.Abs(Vector2.Distance(ai.RT.anchoredPosition, nextWay._centerPosition));
		}
		Vector2 centerPosition = nextWay._centerPosition;
		for (int i = 1; i < list.Count; i++)
		{
			if (i > 1)
			{
				centerPosition = list[i - 1]._centerPosition;
			}
			Vector2 b = list[i]._centerPosition;
			if (i == list.Count - 1)
			{
				b = ai.RT.anchoredPosition;
			}
			num += Mathf.Abs(Vector2.Distance(centerPosition, b));
		}
		_ = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		return num;
	}

	private float PoliceDistance(PoliceAI ai)
	{
		_ = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		float num = 0f;
		List<WayPoint> list = _aStarManager.FindPath(base.RT.anchoredPosition, ai.RT.anchoredPosition, base.CurWayPoint, ai.CurWayPoint, null);
		if (list.Count == 0)
		{
			return 0f;
		}
		if (list.Count == 1)
		{
			return Mathf.Abs(Vector2.Distance(ai.RT.anchoredPosition, base.RT.anchoredPosition));
		}
		Vector2 a = base.RT.anchoredPosition;
		for (int i = 1; i < list.Count; i++)
		{
			if (i > 1)
			{
				a = list[i - 1]._centerPosition;
			}
			Vector2 b = list[i]._centerPosition;
			if (i == list.Count - 1)
			{
				b = ai.RT.anchoredPosition;
			}
			num += Mathf.Abs(Vector2.Distance(a, b));
		}
		_ = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		return num;
	}

	protected override void FixedUpdate()
	{
		if (_crashCount == 3)
		{
			base.Speed = 150;
		}
		else if (_crashCount == 2)
		{
			base.Speed = 200;
		}
		else if (_crashCount == 1)
		{
			base.Speed = 250;
		}
		else
		{
			base.Speed = 0;
		}
		if (_gameOverType == GameOver.SUCCESS || _gameOverType == GameOver.FAIL)
		{
			return;
		}
		base.FixedUpdate();
		if (!_isStart)
		{
			return;
		}
		_dangerPoliceAis.Clear();
		for (int i = 0; i < _polices.Count; i++)
		{
			PoliceAI policeAI = _polices[i];
			if (Mathf.Abs(Vector2.Distance(policeAI.RT.anchoredPosition, base.RT.anchoredPosition)) < (float)(_speed + policeAI.Speed))
			{
				float num = (policeAI.EnemyDistance = PoliceDistance(policeAI));
				if (!_dangerPoliceAis.Contains(policeAI) && num < (float)(_speed + policeAI.Speed))
				{
					_dangerPoliceAis.Add(policeAI);
				}
				if (num < (float)(_speed + policeAI.Speed) && !isChange)
				{
					isChange = true;
					Invoke("ChangeIsChange", 1f);
					if (!isCanExit)
					{
						InitCurWayPoint();
						MovedToFinalPosition();
					}
					else
					{
						_dangerPoliceAis.Clear();
					}
					break;
				}
				if (_safeTime == 10f && num < base.RT.sizeDelta.x / 4f)
				{
					if (_crashCount > -10)
					{
						Debug.Log("受伤：" + _crashCount);
					}
					_crashCount--;
					if (_crashCount >= 0)
					{
						if (_crashCount == 2)
						{
							_gameManager.soundManager.PlayCatchLoop(5);
						}
						_gameManager.soundManager.PlaySound(46);
						StartCoroutine("PlayGunSound");
						if (!isCanExit)
						{
							_aStarPaths.Clear();
							base.FinalMovedPoints.Clear();
							InitCurWayPoint();
						}
						Invoke("ResetLastTime", _safeTime);
						CatchEvent.Instance.NoticeNextEvent(CatchEventEnum.CATCH_HIT);
						isCrashing = true;
						_safeTime = 4.9f;
						if (_crashCount == 0)
						{
							CatchEvent.Instance.NoticeNextEvent(CatchEventEnum.GAME_SUCCESS);
						}
					}
				}
				lastTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000000;
			}
			else if (_dangerPoliceAis.Contains(policeAI))
			{
				_dangerPoliceAis.Remove(policeAI);
			}
		}
	}

	private IEnumerator PlayGunSound()
	{
		_gameManager.soundManager.PlaySound(48);
		yield return new WaitForSeconds(0.1f);
		_gameManager.soundManager.PlaySound(48);
		yield return new WaitForSeconds(0.1f);
		_gameManager.soundManager.PlaySound(48);
	}

	private void ChangeIsChange()
	{
		isChange = false;
	}

	private void ResetLastTime()
	{
		_safeTime = 10f;
		isCrashing = false;
		CatchEvent.Instance.NoticeNextEvent(CatchEventEnum.CATCH_HIT_FINISHED);
	}

	private void NoticeCarPosition(WayPoint arg1, CarBase carBase)
	{
	}

	private void OnEnable()
	{
		CatchEvent.Instance.noticeCarPosition += NoticeCarPosition;
		CatchEvent.Instance.onNoticeStart += NoticeStart;
		CatchEvent.Instance.onNoticeEnemyShow += NoticeEnemyShow;
	}

	private void OnDisable()
	{
		CatchEvent.Instance.noticeCarPosition -= NoticeCarPosition;
		CatchEvent.Instance.onNoticeStart -= NoticeStart;
		CatchEvent.Instance.onNoticeEnemyShow -= NoticeEnemyShow;
	}

	private void NoticeEnemyShow()
	{
		GetComponent<CanvasGroup>().DOFade(1f, 1f).OnComplete(delegate
		{
			CatchEvent.Instance.NoticePoliceShow();
		});
	}

	private void NoticeStart()
	{
		Vector2 sizeDelta = _polices[0].GetComponent<RectTransform>().sizeDelta;
		float num = Mathf.Max(sizeDelta.x, sizeDelta.y);
		Vector2 sizeDelta2 = base.RT.sizeDelta;
		float num2 = Mathf.Max(sizeDelta2.x, sizeDelta2.y);
		_minSafeDistance = num + num2;
		StartCoroutine(StartGame());
	}

	private void TriggerSound()
	{
		_ = new string[3][][]
		{
			new string[5][]
			{
				new string[3] { "", "2", "2" },
				new string[4] { "^vdev1008", "2", "2", "1" },
				new string[4] { "^vdev1010", "2", "2", "1" },
				new string[4] { "", "2", "2", "1" },
				new string[4] { "^vdev1011", "2", "2", "1" }
			},
			new string[11][]
			{
				new string[4] { "^vdev1012", "2", "2", "1" },
				new string[4] { "^vdev1013", "2", "2", "1" },
				new string[4] { "^vdev1014", "2", "2", "1" },
				new string[3] { "^vdev1015", "2", "2" },
				new string[4] { "^vdev1016", "2", "2", "1" },
				new string[4] { "^vdev1017", "2", "2", "1" },
				new string[4] { "^vdev1018", "2", "2", "1" },
				new string[4] { "^vdev1019", "2", "2", "1" },
				new string[4] { "^vdev1020", "2", "2", "1" },
				new string[4] { "^vdev1021", "2", "2", "1" },
				new string[4] { "^vdev1022", "2", "2", "1" }
			},
			new string[2][]
			{
				new string[4] { "^vdev1023", "2", "2", "1" },
				new string[4] { "", "2", "2", "1" }
			}
		};
	}
}
