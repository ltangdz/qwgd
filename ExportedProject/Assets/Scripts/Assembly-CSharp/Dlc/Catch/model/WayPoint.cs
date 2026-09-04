using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dlc.Catch.model
{
	public class WayPoint : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		private Rect _itemRect;

		[SerializeField]
		private WayPoint _leftPoint;

		[SerializeField]
		private WayPoint _upPoint;

		[SerializeField]
		private WayPoint _downPoint;

		[SerializeField]
		private WayPoint _rightPoint;

		private WayPoint _lastWayPoint;

		private List<int> _wayIndexQuene;

		[SerializeField]
		private int _pathCount;

		[SerializeField]
		private int _pointType;

		[SerializeField]
		private bool _isExit;

		[SerializeField]
		private List<WayPoint> _singleWays = new List<WayPoint>();

		private List<PoliceAI> _policeAis = new List<PoliceAI>();

		public WayPoint aParent;

		public float f;

		public float g;

		public float h;

		[SerializeField]
		private WayPointDirection _direction;

		private RectTransform _curTransform;

		[SerializeField]
		public List<WayPoint> _belongPaths = new List<WayPoint>();

		private float _horizontalDistance;

		private float _verticalDistance;

		private int _index;

		public bool isExitPath;

		public Vector2 _centerPosition;

		[SerializeField]
		private List<Vector2> _startPoints = new List<Vector2>();

		public int PointType
		{
			get
			{
				return _pointType;
			}
			set
			{
				_pointType = value;
			}
		}

		public Rect ItemRect
		{
			get
			{
				return _itemRect;
			}
			set
			{
				_itemRect = value;
			}
		}

		public WayPoint LeftPoint
		{
			get
			{
				return _leftPoint;
			}
			set
			{
				_leftPoint = value;
			}
		}

		public WayPoint UpPoint
		{
			get
			{
				return _upPoint;
			}
			set
			{
				_upPoint = value;
			}
		}

		public WayPoint DownPoint
		{
			get
			{
				return _downPoint;
			}
			set
			{
				_downPoint = value;
			}
		}

		public WayPoint RightPoint
		{
			get
			{
				return _rightPoint;
			}
			set
			{
				_rightPoint = value;
			}
		}

		public List<WayPoint> BelongPaths
		{
			get
			{
				return _belongPaths;
			}
			set
			{
				_belongPaths = value;
			}
		}

		public int Index
		{
			get
			{
				return _index;
			}
			set
			{
				_index = value;
			}
		}

		public int PathCount
		{
			get
			{
				return _pathCount;
			}
			set
			{
				_pathCount = value;
			}
		}

		public WayPoint LastWayPoint
		{
			get
			{
				return _lastWayPoint;
			}
			set
			{
				_lastWayPoint = value;
			}
		}

		public WayPointDirection Direction
		{
			get
			{
				return _direction;
			}
			set
			{
				_direction = value;
			}
		}

		public RectTransform CurTransform
		{
			get
			{
				return _curTransform;
			}
			set
			{
				_curTransform = value;
			}
		}

		public float HorizontalDistance
		{
			get
			{
				return _horizontalDistance;
			}
			set
			{
				_horizontalDistance = value;
			}
		}

		public float VerticalDistance
		{
			get
			{
				return _verticalDistance;
			}
			set
			{
				_verticalDistance = value;
			}
		}

		public List<Vector2> StartPoints
		{
			get
			{
				return _startPoints;
			}
			set
			{
				_startPoints = value;
			}
		}

		public List<WayPoint> SingleWays => _singleWays;

		public List<int> WayIndexQuene
		{
			get
			{
				return _wayIndexQuene;
			}
			set
			{
				_wayIndexQuene = value;
			}
		}

		public bool IsExit => _isExit;

		public void ResetData()
		{
			_startPoints.Clear();
			_curTransform = GetComponent<RectTransform>();
			Vector2 anchoredPosition = _curTransform.anchoredPosition;
			float x = _curTransform.sizeDelta.x;
			float y = _curTransform.sizeDelta.y;
			float num = Mathf.Abs(x - y);
			float num2 = Mathf.Min(x, y);
			_centerPosition = new Vector2(anchoredPosition.x + x / 2f, anchoredPosition.y + y / 2f);
			_startPoints.Add(new Vector2(anchoredPosition.x + num2 / 2f, anchoredPosition.y + num2 / 2f));
			_itemRect = new Rect(_curTransform.anchoredPosition, _curTransform.sizeDelta);
			if (x == y || num < num2 / 2f)
			{
				_direction = WayPointDirection.CORNER;
				_horizontalDistance = num2 / 2f;
				_verticalDistance = num2 / 2f;
				_pointType = 0;
				return;
			}
			if (x > num2)
			{
				_horizontalDistance = x - num2;
				_verticalDistance = num2 / 2f;
				_direction = WayPointDirection.HORIZONTAL;
				_startPoints.Add(new Vector2(anchoredPosition.x + x - num2 / 2f, anchoredPosition.y + num2 / 2f));
			}
			else
			{
				_horizontalDistance = num2 / 2f;
				_verticalDistance = y - num2;
				_direction = WayPointDirection.VERTICAL;
				_startPoints.Add(new Vector2(anchoredPosition.x + num2 / 2f, anchoredPosition.y + y - num2 / 2f));
			}
			_pointType = 1;
		}

		public void ResetStar()
		{
			h = 0f;
			f = 0f;
			g = 0f;
		}

		public void ResetRound()
		{
			ResetData();
			for (int i = 0; i < _belongPaths.Count; i++)
			{
				WayPoint wayPoint = _belongPaths[i];
				wayPoint.ResetData();
				Vector2 a = _startPoints[0];
				Vector2 b = wayPoint.StartPoints[0];
				float num = Mathf.Abs(Vector2.Distance(a, b));
				if (_pointType == 0)
				{
					Vector2 b2 = wayPoint.StartPoints[1];
					float num2 = Mathf.Abs(Vector2.Distance(a, b2));
					if (wayPoint.Direction == WayPointDirection.HORIZONTAL)
					{
						if (num > num2)
						{
							_leftPoint = wayPoint;
						}
						else
						{
							_rightPoint = wayPoint;
						}
					}
					else if (wayPoint.Direction == WayPointDirection.VERTICAL)
					{
						if (num > num2)
						{
							_downPoint = wayPoint;
						}
						else
						{
							_upPoint = wayPoint;
						}
					}
					continue;
				}
				float num3 = Mathf.Abs(Vector2.Distance(_startPoints[1], b));
				if (_direction == WayPointDirection.HORIZONTAL)
				{
					if (num < num3)
					{
						_leftPoint = wayPoint;
					}
					else
					{
						_rightPoint = wayPoint;
					}
				}
				else if (num < num3)
				{
					_downPoint = wayPoint;
				}
				else
				{
					_upPoint = wayPoint;
				}
			}
		}

		public Vector2 RandomPosition()
		{
			return _centerPosition;
		}

		public Vector2 RandomLocalPosition()
		{
			return base.transform.GetComponent<RectTransform>().position;
		}

		public Vector2 FindBan(Vector2 curPos, List<Vector2> historyPosList)
		{
			if (_policeAis.Count == 0)
			{
				GameObject[] array = GameObject.FindGameObjectsWithTag("Police");
				for (int i = 0; i < array.Length; i++)
				{
					_policeAis.Add(array[i].GetComponent<PoliceAI>());
				}
			}
			for (int j = 0; j < _policeAis.Count; j++)
			{
				PoliceAI policeAI = _policeAis[j];
				if (_pointType == 0 && _itemRect.Contains(policeAI.RT.anchoredPosition))
				{
					return Vector2.zero;
				}
			}
			return Vector2.zero;
		}

		public void ResetSingleWayPoint()
		{
			List<WayPoint> list = new List<WayPoint>();
			List<WayPoint> list2 = new List<WayPoint>();
			list.Add(this);
			_singleWays.Clear();
			while (list.Count > 0)
			{
				List<WayPoint> list3 = new List<WayPoint>();
				for (int i = 0; i < list.Count; i++)
				{
					WayPoint wayPoint = list[i];
					List<WayPoint> belongPaths = wayPoint.BelongPaths;
					bool flag = false;
					if (wayPoint.PointType == 0)
					{
						if (belongPaths.Count == 2)
						{
							flag = true;
						}
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						_singleWays.Add(wayPoint);
						list2.Add(wayPoint);
						for (int j = 0; j < belongPaths.Count; j++)
						{
							WayPoint item = belongPaths[j];
							if (!list2.Contains(item))
							{
								list3.Add(item);
							}
						}
					}
					list.Remove(wayPoint);
				}
				if (list3.Count > 0)
				{
					list.AddRange(list3);
				}
			}
			_singleWays = _singleWays.Distinct(new ListComparer<WayPoint>((WayPoint p1, WayPoint p2) => p1.name == p2.name)).ToList();
			if (_singleWays.Count == 1)
			{
				_singleWays.Clear();
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform.parent.transform as RectTransform, eventData.position, null, out var localPoint);
			if (Direction == WayPointDirection.HORIZONTAL)
			{
				localPoint.y = _startPoints[0].y;
			}
			else
			{
				localPoint.x = _startPoints[0].x;
			}
			CatchEvent.Instance.ClickPath(localPoint, _index);
		}
	}
}
