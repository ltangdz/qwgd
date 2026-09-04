using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Game.DDOS
{
	public class EnemyProgressDLC8 : MonoBehaviour
	{
		[Header("组件")]
		public Image progress;

		public Text progressText;

		public RectTransform enemyIconGroup;

		[Header("基础属性")]
		private int _deadCount;

		private int _maxCount = 100;

		private RectTransform _rt;

		private int _maxDouble = 100;

		private int _doubleDeadCount;

		private bool _isDouble;

		private int _level;

		public RectTransform RT
		{
			get
			{
				if (_rt == null)
				{
					_rt = GetComponent<RectTransform>();
				}
				return _rt;
			}
		}

		private void Awake()
		{
			DDOSEventManagerDLC8.Instance.onNoticeGameWaves += NoticeGameWaves;
		}

		private void NoticeGameWaves(GameWavesType arg1, int arg2)
		{
			switch (arg1)
			{
			case GameWavesType.START:
				_isDouble = false;
				_maxCount = arg2;
				_deadCount = 0;
				SetProgressText();
				break;
			case GameWavesType.DOUBLE:
				_isDouble = true;
				_maxDouble = arg2;
				_doubleDeadCount = 0;
				SetProgressText();
				break;
			case GameWavesType.ENEMY_DEAD:
				if (_isDouble)
				{
					_doubleDeadCount++;
				}
				else
				{
					_deadCount++;
				}
				SetProgressText();
				break;
			case GameWavesType.BOSS:
				break;
			}
		}

		public void SetProgressText()
		{
			if (_isDouble)
			{
				if (_doubleDeadCount == _maxDouble)
				{
					DDOSEventManagerDLC8.Instance.NoticeGameWaves(GameWavesType.FINISH_DOUBLE, 0);
				}
				return;
			}
			float num = (float)_deadCount * 1f / (float)_maxCount;
			if (num > 1f)
			{
				num = 1f;
			}
			progressText.text = $"{Mathf.FloorToInt(num * 100f)}%";
			progress.DOFillAmount(num, 0.1f).SetEase(Ease.Linear);
			enemyIconGroup.DOAnchorPosX(RT.sizeDelta.x * num - 23f, 0f).SetEase(Ease.Linear);
			if (_deadCount == _maxCount)
			{
				DDOSEventManagerDLC8.Instance.NoticeGameWaves(GameWavesType.FINISH_NORAML, 0);
			}
		}

		private void OnDestroy()
		{
			DDOSEventManagerDLC8.Instance.onNoticeGameWaves -= NoticeGameWaves;
		}

		private void Start()
		{
			Invoke("InitEnemyHp", 1.5f);
		}
	}
}
