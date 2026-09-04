using System;
using System.Collections.Generic;
using System.Linq;
using Dlc.Catch.model;
using UnityEngine;

public abstract class CarBase : MonoBehaviour
{
	private List<WayPoint> _wayPoints;

	public List<WayPoint> _exitPoints = new List<WayPoint>();

	private WayPoint _finalTargetWayPoint;

	private Vector2 _curTargetPoint;

	private WayPoint _curWayPoint;

	private Rect _anchoredRect;

	[SerializeField]
	private List<Vector2> _finalMovedPoints = new List<Vector2>();

	protected int _speed;

	protected CatchCarType _carType;

	private Vector2 _lastPosition;

	private List<WayPoint> _roundWayPoints = new List<WayPoint>();

	private CatchUtils _catchUtils;

	private CarDirection _dircetion;

	public Animator _animator;

	private RectTransform _rt;

	protected List<WayPoint> _eludeWayPoints = new List<WayPoint>();

	protected List<int> _wayHistoryIndexList = new List<int>();

	protected List<int> _wayPointHistoryIndexList = new List<int>();

	private Vector2 _initPosition;

	protected AStarManager _aStarManager;

	public List<WayPoint> _aStarPaths;

	private List<GameObject> objs1 = new List<GameObject>();

	private List<GameObject> objs2 = new List<GameObject>();

	public RectTransform RT => _rt;

	public List<WayPoint> WayPoints
	{
		get
		{
			return _wayPoints;
		}
		set
		{
			_wayPoints = value;
		}
	}

	public WayPoint FinalTargetWayPoint
	{
		get
		{
			return _finalTargetWayPoint;
		}
		set
		{
			_finalTargetWayPoint = value;
		}
	}

	public WayPoint CurWayPoint
	{
		get
		{
			return _curWayPoint;
		}
		set
		{
			_ = _curWayPoint;
			_curWayPoint = value;
		}
	}

	public List<WayPoint> RoundWayPoints
	{
		get
		{
			return _roundWayPoints;
		}
		set
		{
			_roundWayPoints = value;
		}
	}

	public Rect AnchoredRect
	{
		get
		{
			return _anchoredRect;
		}
		set
		{
			_anchoredRect = value;
		}
	}

	public List<Vector2> FinalMovedPoints
	{
		get
		{
			return _finalMovedPoints;
		}
		set
		{
			_finalMovedPoints = value;
		}
	}

	public int Speed
	{
		get
		{
			return _speed;
		}
		set
		{
			_speed = value;
		}
	}

	public CatchCarType CarType
	{
		get
		{
			return _carType;
		}
		set
		{
			_carType = value;
		}
	}

	public Vector2 LastPosition
	{
		get
		{
			return _lastPosition;
		}
		set
		{
			_lastPosition = value;
		}
	}

	public Vector2 CurTargetPoint
	{
		get
		{
			return _curTargetPoint;
		}
		set
		{
			_curTargetPoint = value;
		}
	}

	public CatchUtils CatchUtils
	{
		get
		{
			return _catchUtils;
		}
		set
		{
			_catchUtils = value;
		}
	}

	public Animator Animator
	{
		get
		{
			return _animator;
		}
		set
		{
			_animator = value;
		}
	}

	public List<WayPoint> EludeWayPoints
	{
		get
		{
			return _eludeWayPoints;
		}
		set
		{
			_eludeWayPoints = value;
		}
	}

	public CarDirection Dircetion
	{
		get
		{
			return _dircetion;
		}
		set
		{
			_dircetion = value;
		}
	}

	public List<int> WayHistoryIndexList => _wayHistoryIndexList;

	public List<WayPoint> ExitPoints => _exitPoints;

	public Vector2 InitPosition
	{
		get
		{
			return _initPosition;
		}
		set
		{
			_initPosition = value;
		}
	}

	protected abstract void InitData();

	protected abstract void MovedToFinalPosition();

	public void Init(List<WayPoint> wayPoints)
	{
		_wayPoints = wayPoints;
		_catchUtils = new CatchUtils();
		_rt = base.transform as RectTransform;
		_initPosition = _rt.anchoredPosition;
		InitData();
		InitCurWayPoint();
		InitRound();
		_aStarManager = new AStarManager();
		_aStarManager.Init(_wayPoints);
	}

	protected void InitRound()
	{
		if (_curWayPoint == null)
		{
			return;
		}
		_roundWayPoints.Clear();
		List<WayPoint> belongPaths = CurWayPoint.BelongPaths;
		for (int i = 0; i < belongPaths.Count; i++)
		{
			WayPoint wayPoint = belongPaths[i];
			if (wayPoint.name == CurWayPoint.name)
			{
				continue;
			}
			_roundWayPoints.Add(wayPoint);
			List<WayPoint> belongPaths2 = wayPoint.BelongPaths;
			for (int j = 0; j < belongPaths2.Count; j++)
			{
				WayPoint wayPoint2 = belongPaths2[j];
				if (!(wayPoint2.name == CurWayPoint.name))
				{
					_roundWayPoints.Add(wayPoint2);
				}
			}
		}
		_roundWayPoints = _roundWayPoints.Distinct(new ListComparer<WayPoint>((WayPoint p1, WayPoint p2) => p1.name == p2.name)).ToList();
	}

	public void InitCurWayPoint()
	{
		WayPoint curWayPoint = _curWayPoint;
		bool flag = _roundWayPoints.Count == 0;
		if (!flag)
		{
			bool flag2 = false;
			for (int i = 0; i < _roundWayPoints.Count; i++)
			{
				WayPoint wayPoint = _roundWayPoints[i];
				RectTransform rectTransform = wayPoint.transform as RectTransform;
				if (new Rect(rectTransform.anchoredPosition, rectTransform.sizeDelta).Contains(_rt.anchoredPosition))
				{
					flag2 = true;
					_curWayPoint = wayPoint;
					if (wayPoint.PointType == 0)
					{
						break;
					}
				}
			}
			flag = flag2;
		}
		if (flag)
		{
			for (int j = 0; j < _wayPoints.Count; j++)
			{
				WayPoint wayPoint2 = _wayPoints[j];
				RectTransform rectTransform2 = wayPoint2.transform as RectTransform;
				if (new Rect(rectTransform2.anchoredPosition, rectTransform2.sizeDelta).Contains(_rt.anchoredPosition))
				{
					_curWayPoint = wayPoint2;
					if (wayPoint2.PointType == 0)
					{
						break;
					}
				}
			}
		}
		if (curWayPoint != null && curWayPoint != _curWayPoint)
		{
			if (curWayPoint.PointType == 1)
			{
				_wayHistoryIndexList.Add(curWayPoint.Index);
			}
			_wayPointHistoryIndexList.Add(curWayPoint.Index);
			InitRound();
		}
	}

	protected bool FindPath2(Vector2 targetPoint)
	{
		_finalMovedPoints.Clear();
		GameObject[] array = GameObject.FindGameObjectsWithTag("Police");
		List<PoliceAI> list = new List<PoliceAI>();
		foreach (GameObject gameObject in array)
		{
			list.Add(gameObject.GetComponent<PoliceAI>());
		}
		_ = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		List<WayPoint> list2 = new List<WayPoint>();
		if (_carType != CatchCarType.POLICE)
		{
			for (int j = 0; j < list.Count; j++)
			{
				list2.Add(list[j].CurWayPoint);
			}
		}
		_aStarManager.FindPath(_rt.anchoredPosition, targetPoint, CurWayPoint, _finalTargetWayPoint, list2);
		_ = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		long num = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		WayPoint curWayPoint = _curWayPoint;
		WayPoint finalTargetWayPoint = _finalTargetWayPoint;
		List<WayPoint> list3 = new List<WayPoint>();
		List<WayPoint> list4 = new List<WayPoint>();
		int num2 = 0;
		list3.Add(curWayPoint);
		curWayPoint.LastWayPoint = null;
		WayPoint wayPoint = null;
		while (list3.Count > 0)
		{
			num2++;
			List<WayPoint> list5 = new List<WayPoint>();
			for (int k = 0; k < list3.Count; k++)
			{
				WayPoint wayPoint2 = list3[k];
				if (wayPoint2.LastWayPoint == null)
				{
					wayPoint2.WayIndexQuene = new List<int>(new int[1] { wayPoint2.Index });
				}
				if (list4.Contains(wayPoint2))
				{
					continue;
				}
				if (wayPoint2.name == finalTargetWayPoint.name)
				{
					wayPoint = wayPoint2;
					list3.Clear();
					list5.Clear();
				}
				else
				{
					if (_eludeWayPoints.Contains(wayPoint2))
					{
						if (list3.Count <= 1)
						{
							list3.Clear();
							Debug.Log("找不到路了");
							return false;
						}
						continue;
					}
					List<WayPoint> belongPaths = wayPoint2.BelongPaths;
					for (int l = 0; l < belongPaths.Count; l++)
					{
						WayPoint wayPoint3 = belongPaths[l];
						wayPoint3.LastWayPoint = wayPoint2;
						List<int> list6 = new List<int>(wayPoint2.WayIndexQuene.ToArray());
						list6.Add(wayPoint3.Index);
						wayPoint3.WayIndexQuene = list6;
						list5.Add(wayPoint3);
					}
				}
				list3.Remove(wayPoint2);
				list4.Add(wayPoint2);
			}
			list3 = list3.Union(list5).ToList();
		}
		if (wayPoint == null)
		{
			return false;
		}
		List<int> wayIndexQuene = wayPoint.WayIndexQuene;
		for (int m = 0; m < wayIndexQuene.Count; m++)
		{
			WayPoint wayPoint4 = _wayPoints[wayIndexQuene[m]];
			if (wayPoint4.PointType != 1)
			{
				_finalMovedPoints.Add(wayPoint4.StartPoints[0]);
			}
		}
		_finalMovedPoints.Add(targetPoint);
		long num3 = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		Debug.Log(num3 - num);
		if (_carType == CatchCarType.POLICE)
		{
			Debug.Log("广度:" + (num3 - num) + "-----count:" + _finalMovedPoints.Count);
		}
		return true;
	}

	protected bool FindPath(Vector2 targetPoint)
	{
		for (int i = 0; i < objs1.Count; i++)
		{
			UnityEngine.Object.Destroy(objs1[i].gameObject, 0f);
		}
		for (int j = 0; j < objs2.Count; j++)
		{
			UnityEngine.Object.Destroy(objs2[j].gameObject, 0f);
		}
		objs1.Clear();
		objs2.Clear();
		_ = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		List<WayPoint> list = (_aStarPaths = _aStarManager.FindPath(_rt.anchoredPosition, targetPoint, CurWayPoint, _finalTargetWayPoint, (_carType == CatchCarType.POLICE) ? null : _eludeWayPoints));
		_finalMovedPoints.Clear();
		if (list.Count == 0)
		{
			Debug.Log("没路");
			return false;
		}
		_finalMovedPoints.Add(_rt.anchoredPosition);
		if (list.Count == 1)
		{
			_finalMovedPoints.Add(targetPoint);
		}
		else
		{
			for (int k = 0; k < list.Count; k++)
			{
				WayPoint wayPoint = list[k];
				if (k == 0)
				{
					_finalMovedPoints.Add(RT.anchoredPosition);
				}
				else if (k == list.Count - 1)
				{
					_finalMovedPoints.Add(targetPoint);
				}
				else
				{
					_finalMovedPoints.Add(wayPoint._centerPosition);
				}
			}
		}
		_ = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000;
		return true;
	}

	protected virtual void FixedUpdate()
	{
		if (_finalMovedPoints.Count <= 0)
		{
			return;
		}
		Vector2 anchoredPosition = _rt.anchoredPosition;
		_curTargetPoint = _finalMovedPoints[0];
		bool num = Math.Round(_curTargetPoint.x, 2) == Math.Round(anchoredPosition.x, 2) && Math.Round(_curTargetPoint.y, 2) == Math.Round(anchoredPosition.y, 2);
		Vector2 anchoredPosition2 = _catchUtils.FinalPosistion(_curTargetPoint, anchoredPosition, _speed);
		_rt.anchoredPosition = anchoredPosition2;
		InitCurWayPoint();
		if (!num)
		{
			return;
		}
		if (_finalMovedPoints.Count > 1)
		{
			Vector2 p = _finalMovedPoints[1];
			float unityDirection = AlubaTools.GetUnityDirection(_curTargetPoint, p);
			if (unityDirection == 0f)
			{
				_dircetion = CarDirection.RIGHT;
			}
			else if (unityDirection == 90f)
			{
				_dircetion = CarDirection.UP;
			}
			else if (unityDirection == 180f)
			{
				_dircetion = CarDirection.LEFT;
			}
			else
			{
				_dircetion = CarDirection.DOWN;
			}
		}
		if (_aStarPaths.Count > 0 && _aStarPaths[0].ItemRect.Contains(_curTargetPoint))
		{
			_aStarPaths.RemoveAt(0);
		}
		_finalMovedPoints.Remove(_curTargetPoint);
		if (_finalMovedPoints.Count == 0)
		{
			MovedToFinalPosition();
		}
	}
}
