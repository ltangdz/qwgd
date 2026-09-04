using System.Collections.Generic;
using DG.Tweening;
using Dlc.Catch.model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PoliceAI : CarBase, IPointerDownHandler, IEventSystemHandler
{
	private Vector3 _targetPoint;

	private PoliceAI _curMovePolice;

	public Image _carImage;

	private float _enemyDistance;

	private bool _isMoved;

	private bool _isAuto;

	public Image numberImage;

	public Sprite[] sprites;

	public Text _numberText;

	public int intNumber;

	private bool isOver;

	private List<GameObject> points = new List<GameObject>();

	private bool _isStart;

	public Vector3 TargetPoint
	{
		get
		{
			return _targetPoint;
		}
		set
		{
			_targetPoint = value;
		}
	}

	public PoliceAI CurMovePolice
	{
		get
		{
			return _curMovePolice;
		}
		set
		{
			_curMovePolice = value;
		}
	}

	public Image CarImage
	{
		get
		{
			return _carImage;
		}
		set
		{
			_carImage = value;
		}
	}

	public float EnemyDistance
	{
		get
		{
			return _enemyDistance;
		}
		set
		{
			_enemyDistance = value;
		}
	}

	public List<GameObject> Points
	{
		get
		{
			return points;
		}
		set
		{
			points = value;
		}
	}

	public bool IsStart
	{
		get
		{
			return _isStart;
		}
		set
		{
			_isStart = value;
		}
	}

	public int Speed1
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

	public CatchCarType CarType1
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

	public Animator Animator1
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

	public List<WayPoint> EludeWayPoints1
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

	public List<int> WayHistoryIndexList1
	{
		get
		{
			return _wayHistoryIndexList;
		}
		set
		{
			_wayHistoryIndexList = value;
		}
	}

	public List<int> WayPointHistoryIndexList
	{
		get
		{
			return _wayPointHistoryIndexList;
		}
		set
		{
			_wayPointHistoryIndexList = value;
		}
	}

	public AStarManager AStarManager
	{
		get
		{
			return _aStarManager;
		}
		set
		{
			_aStarManager = value;
		}
	}

	public List<WayPoint> AStarPaths
	{
		get
		{
			return _aStarPaths;
		}
		set
		{
			_aStarPaths = value;
		}
	}

	private void Start()
	{
		_animator.Play("PoliceIdle");
		_numberText.text = string.Concat(intNumber);
	}

	private void Catch()
	{
		if (_isStart && _isAuto)
		{
			CatchEnemy component = GameObject.FindGameObjectWithTag("Enemy").GetComponent<CatchEnemy>();
			base.FinalTargetWayPoint = component.CurWayPoint;
			FindPath(component.RT.anchoredPosition);
		}
	}

	public void ShowCar()
	{
		Material m = _carImage.material;
		Sequence sequence = DOTween.Sequence();
		sequence.AppendInterval(Random.Range(0.5f, 1f));
		sequence.Append(m.DOFade(1f, 1f).OnComplete(delegate
		{
			m.shaderKeywords = new string[0];
			numberImage.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
		}));
		sequence.Play();
	}

	protected override void InitData()
	{
		base.Speed = 150;
		base.CarType = CatchCarType.POLICE;
		_animator.Play("PoliceIdle");
	}

	protected override void MovedToFinalPosition()
	{
	}

	private void ClickPolice(PoliceAI ai)
	{
		if (_isStart && !isOver)
		{
			_curMovePolice = ai;
			if (_curMovePolice == this)
			{
				RemovedMovePoint();
				_isAuto = false;
				base.FinalMovedPoints.Clear();
				_animator.Play("PoliceSelected");
				numberImage.sprite = sprites[1];
				numberImage.GetComponentInChildren<Text>().color = Color.black;
			}
			else
			{
				numberImage.sprite = sprites[0];
				ColorUtility.TryParseHtmlString("#01F1F1", out var color);
				numberImage.GetComponentInChildren<Text>().color = color;
				_animator.Play("PoliceIdle");
			}
		}
	}

	public void AddMovePoint(Vector2 point)
	{
		GameObject gameObject = GameObject.FindWithTag("PathPanel");
		GameObject gameObject2 = Object.Instantiate(Resources.Load<GameObject>("_DLC/Prefabs/PathPoint1"), gameObject.transform);
		gameObject2.GetComponent<RectTransform>().anchoredPosition = point - gameObject2.GetComponent<RectTransform>().sizeDelta / 2f;
		gameObject2.GetComponentInChildren<Text>().text = string.Concat(intNumber);
		points.Add(gameObject2);
	}

	public void RemovedMovePoint()
	{
		for (int i = 0; i < points.Count; i++)
		{
			Object.Destroy(points[i].gameObject);
		}
		points.Clear();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		CatchEvent.Instance.ClickPolice(this);
	}

	private WayPoint NextWayPoint()
	{
		List<WayPoint> belongPaths = base.CurWayPoint.BelongPaths;
		List<int> wayPointHistoryIndexList = WayPointHistoryIndexList;
		int index;
		while (true)
		{
			index = Random.Range(0, belongPaths.Count);
			if (wayPointHistoryIndexList.Count >= 3)
			{
				for (int i = 0; i < 2; i++)
				{
					_ = wayPointHistoryIndexList[wayPointHistoryIndexList.Count - i - 1];
				}
				int num = wayPointHistoryIndexList[1];
				if (belongPaths[index].Index != num)
				{
					break;
				}
			}
		}
		return belongPaths[index];
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
		for (int i = 0; i < range.Count; i++)
		{
			int num2 = range[i];
			for (int j = 0; j < wayPoints.Count; j++)
			{
				WayPoint wayPoint = wayPoints[j];
				if (wayPoint.Index == num2 && !list.Contains(wayPoint))
				{
					list.Add(wayPoint);
					break;
				}
			}
		}
		list.Reverse();
		return list;
	}

	private void ClickPath(Vector2 arg1, int arg2)
	{
		if (!isOver && _curMovePolice == this)
		{
			RemovedMovePoint();
			base.FinalTargetWayPoint = base.WayPoints[arg2];
			AddMovePoint(arg1);
			FindPath(arg1);
			_isMoved = true;
			_isAuto = false;
		}
	}

	private void Update()
	{
		int num = 0;
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			num = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			num = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			num = 3;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			num = 4;
		}
		if (num > 0 && num < 5 && intNumber == num)
		{
			CatchEvent.Instance.ClickPolice(this);
		}
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (_isMoved && base.FinalMovedPoints.Count == 0)
		{
			_isAuto = true;
		}
	}

	private void OnEnable()
	{
		CatchEvent.Instance.onClickPolice += ClickPolice;
		CatchEvent.Instance.onClickPath += ClickPath;
		CatchEvent.Instance.onNoticePoliceShow += ShowCar;
		CatchEvent.Instance.onNoticeStart += NoticeStart;
		CatchEvent.Instance.onNoticeNextEvent += NoticeNextEvent;
	}

	private void NoticeNextEvent(CatchEventEnum obj)
	{
		if (obj == CatchEventEnum.GAME_SUCCESS)
		{
			isOver = true;
			CatchEnemy component = GameObject.FindGameObjectWithTag("Enemy").GetComponent<CatchEnemy>();
			base.FinalTargetWayPoint = component.CurWayPoint;
			FindPath(component.RT.anchoredPosition);
			RemovedMovePoint();
		}
	}

	private void OnDisable()
	{
		CatchEvent.Instance.onClickPolice -= ClickPolice;
		CatchEvent.Instance.onClickPath -= ClickPath;
		CatchEvent.Instance.onNoticePoliceShow -= ShowCar;
		CatchEvent.Instance.onNoticeStart -= NoticeStart;
		CatchEvent.Instance.onNoticeNextEvent -= NoticeNextEvent;
	}

	private void NoticeStart()
	{
		_isStart = true;
	}
}
