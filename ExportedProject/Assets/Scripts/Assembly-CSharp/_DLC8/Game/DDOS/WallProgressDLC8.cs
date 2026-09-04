using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Game.DDOS
{
	public class WallProgressDLC8 : DDosMonoBehaviourDLC8
	{
		[Header("组件")]
		public Image progress;

		public Text hpText;

		public bool isEnemy;

		public RectTransform enemyIconGroup;

		[Header("基础属性")]
		private ObscuredInt _hp;

		private ObscuredInt _maxHP;

		private RectTransform _rt;

		public int Hp
		{
			get
			{
				return _hp;
			}
			set
			{
				_hp = value;
			}
		}

		public int MAXHp => _maxHP;

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

		private void Start()
		{
			base.DdosEventManagerDlc8.onNoticeWallInjured += NoticeWallInjured;
			base.DdosEventManagerDlc8.onNoticeWallHeal += NoticeWallHeal;
			if (isEnemy)
			{
				Invoke("InitEnemyHp", 1.5f);
			}
		}

		private void NoticeWallHeal(int maxHP)
		{
			if (!isEnemy)
			{
				_maxHP = maxHP;
				SetHPText(_maxHP, _maxHP);
			}
		}

		private void InitEnemyHp()
		{
			if (isEnemy)
			{
				Debug.LogError("InitEnemyHp");
				_maxHP = base.DdosManagerDlc8.LevelDlc8.enemyHp;
				Hp = _maxHP;
				EnemyInjuredAnimation(1f);
			}
		}

		private void NoticeWallInjured(int damaged, bool _isEnemy, GameObject from)
		{
			if (!_isEnemy)
			{
				int num = Hp - damaged;
				if (num <= 0)
				{
					num = 0;
				}
				if (num <= 0)
				{
					Debug.Log("NoticeGameResult");
					base.DdosEventManagerDlc8.NoticeGameResult(_isEnemy ? GameResult.SUCCESS : GameResult.FAIL);
				}
				float num2 = base.DdosManagerDlc8.CountHpPercentage(num, _maxHP);
				if (isEnemy)
				{
					SetHPText(num, _maxHP);
					EnemyInjuredAnimation(num2);
				}
				else
				{
					Hp = num;
					SetHPText(num, _maxHP);
					progress.DOFillAmount(num2, 0f).SetEase(Ease.Linear);
				}
			}
		}

		private void EnemyInjuredAnimation(float percent)
		{
			float num = 1f - percent;
			SetEnemyProgressText(num);
			progress.DOFillAmount(num, 0f).SetEase(Ease.Linear);
			enemyIconGroup.DOAnchorPosX(RT.sizeDelta.x * num - 23f, 0f).SetEase(Ease.Linear).OnComplete(delegate
			{
				_ = percent;
				_ = 0f;
			});
		}

		public void SetHPText(int hp, int maxHp)
		{
			float endValue = base.DdosManagerDlc8.CountHpPercentage(hp, _maxHP);
			progress.DOFillAmount(endValue, 0f).SetEase(Ease.Linear);
			Hp = hp;
			_maxHP = maxHp;
			if (Hp <= 0)
			{
				base.DdosEventManagerDlc8.NoticeGameResult(GameResult.FAIL);
			}
			if (hpText != null)
			{
				hpText.text = $"{Hp}/{_maxHP}";
			}
		}

		public void SetEnemyProgressText(float progress)
		{
			if (hpText != null)
			{
				hpText.text = $"{Mathf.FloorToInt(progress * 100f)}%";
			}
		}

		private void OnDestroy()
		{
			base.DdosEventManagerDlc8.onNoticeWallInjured -= NoticeWallInjured;
			base.DdosEventManagerDlc8.onNoticeWallHeal -= NoticeWallHeal;
		}
	}
}
