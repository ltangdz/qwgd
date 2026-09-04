using System.Collections.Generic;
using System.Linq;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Game
{
	public class BaseStationManager : MonoBehaviour
	{
		private ObscuredInt _level = 1;

		public Transform _gameCenterTransform;

		public GridLayoutGroup gridLayoutGroup;

		public InvadeGridItem invadeGridItemPrefab;

		private ObscuredInt _maxCount;

		private ObscuredInt _curCount;

		private List<InvadeGridItem> _gridItems = new List<InvadeGridItem>();

		private ObscuredInt _curGameIndex;

		private ObscuredInt[] _curGameData;

		private GameManager _gameManager;

		private ObscuredFloat _time = 0f;

		private ObscuredInt[][] openList = new ObscuredInt[25][]
		{
			new ObscuredInt[1] { 3 },
			new ObscuredInt[1] { 6 },
			new ObscuredInt[1] { 4 },
			new ObscuredInt[2] { 3, 6 },
			new ObscuredInt[2] { 3, 4 },
			new ObscuredInt[2] { 0, 4 },
			new ObscuredInt[2] { 0, 8 },
			new ObscuredInt[3] { 3, 6, 8 },
			new ObscuredInt[8] { 0, 1, 4, 5, 6, 9, 11, 13 },
			new ObscuredInt[5] { 2, 7, 10, 12, 15 },
			new ObscuredInt[5] { 2, 3, 6, 9, 11 },
			new ObscuredInt[6] { 0, 2, 4, 7, 10, 13 },
			new ObscuredInt[5] { 0, 3, 4, 5, 14 },
			new ObscuredInt[8] { 0, 3, 4, 5, 6, 8, 9, 12 },
			new ObscuredInt[10] { 0, 2, 5, 6, 7, 8, 9, 10, 13, 15 },
			new ObscuredInt[9] { 0, 1, 2, 3, 5, 8, 9, 12, 13 },
			new ObscuredInt[9] { 0, 4, 6, 7, 9, 10, 11, 12, 14 },
			new ObscuredInt[9] { 2, 5, 6, 8, 9, 10, 13, 14, 15 },
			new ObscuredInt[9] { 0, 2, 4, 6, 7, 9, 11, 13, 14 },
			new ObscuredInt[6] { 0, 4, 10, 11, 12, 15 },
			new ObscuredInt[9] { 1, 3, 4, 5, 6, 9, 10, 12, 15 },
			new ObscuredInt[6] { 2, 5, 7, 8, 14, 15 },
			new ObscuredInt[7] { 0, 5, 6, 8, 10, 13, 15 },
			new ObscuredInt[16]
			{
				1, 2, 5, 6, 7, 8, 10, 11, 12, 14,
				16, 18, 19, 22, 23, 24
			},
			new ObscuredInt[16]
			{
				1, 2, 3, 5, 6, 7, 9, 10, 11, 12,
				15, 17, 18, 19, 21, 24
			}
		};

		private void Start()
		{
			_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			ObscuredInt obscuredInt = 9;
			ObscuredInt obscuredInt2 = 3;
			ObscuredFloat obscuredFloat = 375f;
			if ((int)_level > 8 && (int)_level <= 23)
			{
				obscuredInt2 = 4;
				obscuredInt = 16;
				obscuredFloat = 500f;
			}
			else if ((int)_level >= 24)
			{
				obscuredInt2 = 5;
				obscuredInt = 25;
				obscuredFloat = 625f;
			}
			float num = 476f / (float)obscuredFloat;
			float num2 = 113f * num;
			float num3 = 6f * num;
			gridLayoutGroup.cellSize = new Vector2(num2, num2);
			gridLayoutGroup.spacing = new Vector2(num3, num3);
			_curGameIndex = (int)_level - 1;
			_curGameData = openList[(int)_curGameIndex];
			for (ObscuredInt obscuredInt3 = 0; (int)obscuredInt3 < (int)obscuredInt; ++obscuredInt3)
			{
				InvadeGridItem invadeGridItem = Object.Instantiate(invadeGridItemPrefab, _gameCenterTransform);
				bool isOpen = !_curGameData.Contains(obscuredInt3);
				invadeGridItem.InitData(obscuredInt3, "invade_item", obscuredInt2, obscuredInt2, isOpen);
				_gridItems.Add(invadeGridItem);
			}
			InvadeEvent.Instance.NoticeItemCanClick();
		}

		public void Init(int level)
		{
			_level = level;
		}

		public void ResetGame()
		{
			for (ObscuredInt obscuredInt = 0; (int)obscuredInt < _gridItems.Count; ++obscuredInt)
			{
				InvadeGridItem invadeGridItem = _gridItems[obscuredInt];
				bool isOpen = _curGameData.Contains(obscuredInt);
				invadeGridItem.ResetData(isOpen);
			}
		}

		private void NoticeItemChange(List<int> obj)
		{
			_maxCount = obj.Count;
			_curCount = 0;
		}

		private void NoticeItemAnimationFinished(int obj)
		{
			++_curCount;
			if ((int)_curCount != (int)_maxCount || (int)_maxCount == 0)
			{
				return;
			}
			InvadeEvent.Instance.NoticeItemCanClick();
			for (ObscuredInt obscuredInt = 0; (int)obscuredInt < _gridItems.Count; ++obscuredInt)
			{
				if (!_gridItems[obscuredInt].IsOpen)
				{
					return;
				}
			}
			InvadeEvent.Instance.NoticeStepFinished(3, isSuccess: true);
			DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.FINISH_GAMME, 0);
			Debug.Log("过关");
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
			_time = (float)_time + Time.deltaTime;
		}
	}
}
