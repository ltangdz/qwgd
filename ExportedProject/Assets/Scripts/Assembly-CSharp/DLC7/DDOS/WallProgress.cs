using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.DDOS
{
	public class WallProgress : DDosMonoBehaviour
	{
		[Header("组件")]
		public Image progress;

		public Text hpText;

		public bool isEnemy;

		public RectTransform enemyIconGroup;

		[Header("基础属性")]
		private int _hp;

		private int _maxHP;

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
			base.DdosEventManager.onNoticeWallInjured += NoticeWallInjured;
			base.DdosEventManager.onNoticeWallHeal += NoticeWallHeal;
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
				_maxHP = base.DdosManager.Level.enemyHp;
				Hp = _maxHP;
				EnemyInjuredAnimation(1f);
			}
		}

		private void NoticeWallInjured(int damaged, bool _isEnemy, GameObject from)
		{
			if (_isEnemy == isEnemy)
			{
				if (!_isEnemy)
				{
					Debug.Log("我方Hp：" + Hp + "----damaged:" + damaged + "--剩余：" + (Hp - damaged));
				}
				int num = Hp - damaged;
				if (num <= 0)
				{
					num = 0;
				}
				if (num <= 0)
				{
					Debug.Log("NoticeGameResult");
					base.DdosEventManager.NoticeGameResult(_isEnemy ? GameResult.SUCCESS : GameResult.FAIL);
				}
				float num2 = base.DdosManager.CountHpPercentage(num, _maxHP);
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
			if (!isEnemy)
			{
				Debug.Log("SetHPText：" + hp);
			}
			float endValue = base.DdosManager.CountHpPercentage(hp, _maxHP);
			progress.DOFillAmount(endValue, 0f).SetEase(Ease.Linear);
			Hp = hp;
			_maxHP = maxHp;
			if (Hp <= 0)
			{
				base.DdosEventManager.NoticeGameResult(GameResult.FAIL);
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
			base.DdosEventManager.onNoticeWallInjured -= NoticeWallInjured;
			base.DdosEventManager.onNoticeWallHeal -= NoticeWallHeal;
		}
	}
}
