using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InvadeStep3 : MonoBehaviour
{
	private int _maxCount;

	public Button _replayButton;

	private int _curCount;

	private List<InvadeGridItem> _gridItems = new List<InvadeGridItem>();

	public Transform _gameCenterTransform;

	private int _curGameIndex;

	private int[] _curGameData;

	private float _time = 30f;

	private GameManager _gameManager;

	private void Awake()
	{
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		int[][] array = new int[3][]
		{
			new int[14]
			{
				2, 5, 6, 7, 8, 9, 12, 15, 16, 17,
				18, 22, 23, 24
			},
			new int[13]
			{
				1, 2, 3, 7, 10, 11, 13, 14, 16, 18,
				19, 21, 24
			},
			new int[11]
			{
				0, 2, 4, 7, 10, 12, 14, 16, 18, 21,
				23
			}
		};
		_curGameIndex = Random.Range(0, 3);
		_curGameData = array[_curGameIndex];
		for (int i = 0; i < 25; i++)
		{
			InvadeGridItem component = ((GameObject)Object.Instantiate(Resources.Load("_DLC/Prefabs/invade_item"), _gameCenterTransform)).GetComponent<InvadeGridItem>();
			bool flag = _curGameData.Contains(i);
			Debug.Log(flag);
			component.InitData(i, "invade_item", 5, 5, flag);
			_gridItems.Add(component);
		}
		InvadeEvent.Instance.NoticeItemCanClick();
		_replayButton.onClick.AddListener(delegate
		{
			for (int j = 0; j < _gridItems.Count; j++)
			{
				InvadeGridItem invadeGridItem = _gridItems[j];
				bool isOpen = _curGameData.Contains(j);
				invadeGridItem.ResetData(isOpen);
			}
		});
	}

	private void NoticeItemChange(List<int> obj)
	{
		_maxCount = obj.Count;
		_curCount = 0;
	}

	private void NoticeItemAnimationFinished(int obj)
	{
		_curCount++;
		if (_curCount != _maxCount || _maxCount == 0)
		{
			return;
		}
		InvadeEvent.Instance.NoticeItemCanClick();
		for (int i = 0; i < _gridItems.Count; i++)
		{
			if (!_gridItems[i].IsOpen)
			{
				return;
			}
		}
		InvadeEvent.Instance.NoticeStepFinished(3, isSuccess: true);
		Debug.Log("过关");
		if (_time > 0f)
		{
			_gameManager.UnlockAchievements("eagleeyed");
		}
		_replayButton.onClick.RemoveAllListeners();
	}

	private void OnEnable()
	{
		InvadeEvent.Instance.onNoticeItemChange += NoticeItemChange;
		InvadeEvent.Instance.onNoticeItemAnimationFinished += NoticeItemAnimationFinished;
	}

	private void OnDisable()
	{
		InvadeEvent.Instance.onNoticeItemChange -= NoticeItemChange;
		InvadeEvent.Instance.onNoticeItemAnimationFinished -= NoticeItemAnimationFinished;
	}

	private void FixedUpdate()
	{
		_time -= Time.deltaTime;
	}
}
