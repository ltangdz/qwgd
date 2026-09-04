using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.DDOS
{
	public class Enemy : AlubaSpawn
	{
		public CanvasGroup enemyCanvasGroup;

		public Image frameImage;

		public Image iconImage;

		public Image hpProgress;

		public Image attackImage;

		public CanvasGroup hpCanvasGroup;

		private float _maxSpeed;

		private float _maxATK;

		private int _maxHp;

		private float _curSpeed;

		private int _curHp;

		private float _ATK;

		private int _lv;

		private EnemyType _enemyType;

		[SerializeField]
		private AbnormalStatus _abnormalStatus;

		private bool _isSafe;

		private Wall _targetWall;

		private DDOSEventManager _eventManager;

		[SerializeField]
		protected bool _isBoss;

		protected Dictionary<string, string> _enemyDic;

		protected Transform _areaRt;

		private FrameAnimation2D _attackAnimation;

		private RectTransform _rt;

		private bool _isAttacking;

		private DDOSManager _ddosManager;

		public List<Sprite> frameSprite;

		public List<Material> _materialList;

		private bool _isWin;

		public AbnormalStatus AbnormalStatus
		{
			get
			{
				return _abnormalStatus;
			}
			set
			{
				_abnormalStatus = value;
			}
		}

		public float MAXAtk
		{
			get
			{
				return _maxATK;
			}
			set
			{
				_maxATK = value;
			}
		}

		public float Atk
		{
			get
			{
				return _ATK;
			}
			set
			{
				_ATK = value;
			}
		}

		public Wall TargetWall
		{
			get
			{
				return _targetWall;
			}
			set
			{
				_targetWall = value;
			}
		}

		public float MAXSpeed
		{
			get
			{
				return _maxSpeed;
			}
			set
			{
				_maxSpeed = value;
			}
		}

		public int MAXHp
		{
			get
			{
				return _maxHp;
			}
			set
			{
				_maxHp = value;
			}
		}

		public float CurSpeed
		{
			get
			{
				return _curSpeed;
			}
			set
			{
				_curSpeed = value;
			}
		}

		public int CurHp
		{
			get
			{
				return _curHp;
			}
			set
			{
				_curHp = value;
			}
		}

		public bool IsSafe
		{
			get
			{
				return _isSafe;
			}
			set
			{
				_isSafe = value;
			}
		}

		public int Lv => _lv;

		public EnemyType EnemyType
		{
			get
			{
				return _enemyType;
			}
			set
			{
				_enemyType = value;
			}
		}

		public FrameAnimation2D AttackAnimation
		{
			get
			{
				if (_attackAnimation == null)
				{
					_attackAnimation = attackImage.GetComponent<FrameAnimation2D>();
				}
				return _attackAnimation;
			}
		}

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

		public DDOSEventManager EventManager
		{
			get
			{
				if (_eventManager == null)
				{
					_eventManager = DDOSEventManager.Instance;
				}
				return _eventManager;
			}
		}

		public DDOSManager DdosManager
		{
			get
			{
				if (_ddosManager == null)
				{
					_ddosManager = DDOSManager.Instance;
				}
				return _ddosManager;
			}
		}

		private void ShaderTest()
		{
			if (_isBoss)
			{
				GetShaderByStatus();
			}
			else
			{
				IceOrPalsyShader();
			}
		}

		public void InitData(Dictionary<string, string> enemyDic, bool isBoss, EnemyType enemyType, Transform area)
		{
			CancelInvoke("StartCloaking");
			CancelInvoke("Attack");
			InitStartData(enemyDic, isBoss, enemyType, area);
			InitAttribute();
			GetShaderByStatus();
			ShowPosition();
			MoveAnimation();
		}

		private void InitStartData(Dictionary<string, string> enemyDic, bool isBoss, EnemyType enemyType, Transform area)
		{
			_enemyDic = enemyDic;
			_maxSpeed = (float)Math.Round(71.05262756347656, 2);
			enemyCanvasGroup.alpha = 0f;
			_areaRt = area;
			_lv = Convert.ToInt32(enemyDic["EnemyLevel"]);
			_maxHp = Convert.ToInt32(enemyDic["EnemyHp"]);
			_maxATK = Convert.ToInt32(enemyDic["EnemyAtt"]);
			_curHp = _maxHp;
			_ATK = _maxATK;
			_isBoss = isBoss;
			_enemyType = enemyType;
			_abnormalStatus = AbnormalStatus.NORMAL;
			base.transform.localScale = (isBoss ? (Vector3.one * 1.5f) : Vector3.one);
			iconImage.DOFade(1f, 0f);
			hpCanvasGroup.alpha = 1f;
			hpProgress.fillAmount = 1f;
			frameImage.sprite = frameSprite[UnityEngine.Random.Range(0, 7)];
			if (_enemyType == EnemyType.SAMLL)
			{
				iconImage.sprite = DdosManager.ddosAtlas.GetSprite("dun_icon_06");
			}
			else
			{
				Image image = iconImage;
				Sprite sprite3;
				if (_enemyType != EnemyType.NORMAL)
				{
					Sprite sprite = (iconImage.sprite = DdosManager.ddosAtlas.GetSprite($"dun_icon_0{(int)(EnemyType + 2)}"));
					sprite3 = sprite;
				}
				else
				{
					sprite3 = DdosManager.ddosAtlas.GetSprite($"dun_icon_0{UnityEngine.Random.Range(1, 4)}");
				}
				image.sprite = sprite3;
			}
			if (_enemyType == EnemyType.CLOAKING)
			{
				InvokeRepeating("StartCloaking", 3f, 3f);
			}
		}

		private void InitAttribute()
		{
			if (_isBoss)
			{
				base.transform.DOScale(1.5f, 0f);
			}
			else
			{
				base.transform.DOScale(1f, 0f);
			}
			switch (EnemyType)
			{
			case EnemyType.SAMLL:
				_isBoss = false;
				base.transform.DOScale(0.8f, 0f);
				MAXHp = Mathf.FloorToInt((float)MAXHp * 0.4f * (_isBoss ? 1.5f : 1f));
				MAXAtk *= (_isBoss ? 1.5f : 1f);
				break;
			case EnemyType.NORMAL:
				MAXHp = Mathf.FloorToInt((float)MAXHp * (_isBoss ? 1.5f : 1f));
				MAXAtk *= (_isBoss ? 1.5f : 1f);
				break;
			case EnemyType.TRANSFER:
				MAXHp = Mathf.FloorToInt((float)MAXHp / 2f * (_isBoss ? 1.5f : 1f));
				MAXAtk *= (_isBoss ? 1.5f : 1f);
				break;
			case EnemyType.CLOAKING:
				MAXHp = Mathf.FloorToInt((float)MAXHp * 0.8f * (_isBoss ? 1.5f : 1f));
				MAXAtk *= (_isBoss ? 1.5f : 1f);
				break;
			case EnemyType.SPLIT:
				MAXSpeed /= 2f;
				MAXHp = Mathf.FloorToInt((float)MAXHp * 1.5f * (_isBoss ? 1.5f : 1f));
				MAXAtk *= (_isBoss ? 1.5f : 1f);
				break;
			case EnemyType.HP:
				MAXSpeed *= 0.8f;
				MAXHp = Mathf.FloorToInt((float)MAXHp * 1.25f * (_isBoss ? 1.5f : 1f));
				MAXAtk *= (_isBoss ? 1.5f : 1f);
				break;
			case EnemyType.SPEED:
				MAXSpeed *= 0.8f;
				MAXHp = Mathf.FloorToInt((float)MAXHp * 0.8f * (_isBoss ? 1.5f : 1f));
				MAXAtk = MAXAtk * 1.2f * (_isBoss ? 1.5f : 1f);
				break;
			}
			CurHp = MAXHp;
			Atk = MAXAtk;
		}

		public void InitData(Dictionary<string, string> enemyDic, bool isBoss, EnemyType enemyType, Vector2 from, Vector2 to)
		{
			CancelInvoke("StartCloaking");
			CancelInvoke("Attack");
			InitStartData(enemyDic, isBoss, enemyType, null);
			InitAttribute();
			_isAttacking = false;
			GetShaderByStatus();
			RectTransform component = GetComponent<RectTransform>();
			component.localPosition = from;
			component.DOLocalJump(to, 3f, 2, 0.3f).OnComplete(delegate
			{
				MoveAnimation();
			});
		}

		public void ShowPosition()
		{
			Vector3 localPosition = _areaRt.localPosition;
			float y = localPosition.y;
			Vector2 sizeDelta = _areaRt.gameObject.GetComponent<RectTransform>().sizeDelta;
			float y2 = sizeDelta.y;
			float y3 = UnityEngine.Random.Range(y - y2 / 2f, y + y2 / 2f);
			float x = ((EnemyType == EnemyType.TRANSFER) ? UnityEngine.Random.Range(localPosition.x - sizeDelta.x / 2f, localPosition.x + sizeDelta.x / 2f) : (localPosition.x - sizeDelta.x / 2f + (float)UnityEngine.Random.Range(-10, 10)));
			Vector3 localPosition2 = new Vector3(x, y3, 0f);
			base.transform.localPosition = localPosition2;
		}

		private void MoveAnimation()
		{
			enemyCanvasGroup.DOFade(1f, 1f).OnComplete(delegate
			{
				_curSpeed = _maxSpeed;
				Sequence sequence = DOTween.Sequence();
				sequence.SetId($"EnemyMove{base.name}");
				sequence.Append(frameImage.transform.DOScale(Vector3.one * 1.05f, 0.39f)).SetEase(Ease.Linear);
				sequence.Append(frameImage.transform.DOScale(Vector3.one, 0.39f)).SetEase(Ease.Linear);
				sequence.SetLoops(-1);
				sequence.Play();
			});
		}

		public void AttackWall()
		{
			if (!_isAttacking)
			{
				_isAttacking = true;
				CurSpeed = 0f;
				InvokeRepeating("Attack", 1f, 1f * ((_abnormalStatus == AbnormalStatus.PALSY) ? 0.8f : 1f));
			}
		}

		public void Win()
		{
			if (base.isActiveAndEnabled && _isAttacking)
			{
				CancelInvoke("Attack");
			}
			_isWin = true;
			_isAttacking = false;
			_curSpeed = _maxSpeed;
		}

		private void Attack()
		{
			if (base.isActiveAndEnabled)
			{
				if (!_isAttacking && base.isActiveAndEnabled)
				{
					CancelInvoke("Attack");
					return;
				}
				AttackAnimation.gameObject.SetActive(value: true);
				attackImage.gameObject.SetActive(value: true);
				attackImage.transform.DOLocalMoveX(-20f, 0f);
				attackImage.DOFade(1f, 0f);
				AttackAnimation.Play();
				attackImage.transform.DOLocalMoveX(RT.localPosition.x - 50f, 0.6f);
				int num = Mathf.RoundToInt(_ATK);
				Debug.Log(base.name + ":" + num);
				EventManager.NoticeWallInjured(num, isEnemy: false, base.gameObject);
			}
		}

		public void Injured(BulletType bulletType, int damage)
		{
			ReduceHp(damage);
			Status(bulletType);
		}

		public void ReduceHp(int damage)
		{
			int num = CurHp - damage;
			CurHp = ((num >= 0) ? num : 0);
			float endValue = (float)_curHp / (float)MAXHp;
			hpProgress.DOFillAmount(endValue, 0.3f);
			if (CurHp <= 0)
			{
				Dead();
			}
		}

		private void Status(BulletType bulletType)
		{
			if (AbnormalStatus != AbnormalStatus.DEAD && AbnormalStatus == AbnormalStatus.NORMAL)
			{
				switch (bulletType)
				{
				case BulletType.ICE:
					Freeze();
					break;
				case BulletType.PALSY:
					Palsy();
					break;
				}
			}
		}

		private void Freeze()
		{
			CancelInvoke("Unfreeze");
			AbnormalStatus = AbnormalStatus.ICE;
			_curSpeed = 0f;
			GetShaderByStatus();
			Invoke("Unfreeze", 0.3f);
		}

		private void Unfreeze()
		{
			NormalStatus();
			CurSpeed = _maxSpeed;
		}

		private void NormalStatus()
		{
			AbnormalStatus = AbnormalStatus.NORMAL;
			GetShaderByStatus();
		}

		private void GetShaderByStatus()
		{
			switch (_abnormalStatus)
			{
			case AbnormalStatus.NORMAL:
				RemoveIceOrPalsyShader();
				if (_isBoss)
				{
					Invoke("BossShader", 0.01f);
				}
				break;
			case AbnormalStatus.ICE:
			case AbnormalStatus.PALSY:
				IceOrPalsyShader();
				break;
			case AbnormalStatus.DEAD:
				DeadShader();
				break;
			}
		}

		private void RemoveIceOrPalsyShader()
		{
			frameImage.material.SetFloat("_OutlineAlpha", 0f);
		}

		private void IceOrPalsyShader()
		{
			frameImage.material = _materialList[1];
			frameImage.material.SetFloat("_OutlineAlpha", 1f);
		}

		private void DeadShader()
		{
			frameImage.material = _materialList[2];
			frameImage.material.SetFloat("_OutlineAlpha", 1f);
			frameImage.material.SetFloat("_FadeAmount", -0.1f);
			iconImage.DOFade(0f, 0.5f);
			frameImage.material.DOFloat(1f, "_FadeAmount", 1.5f).SetEase(Ease.Linear).OnComplete(delegate
			{
				base.transform.localPosition = new Vector3(-3000f, 0f, 0f);
				frameImage.material = null;
				if (_isWin)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
				else if (base.Pool.IsSpawned(base.transform))
				{
					base.Pool.Despawn(base.transform);
				}
			});
		}

		private void BossShader()
		{
			frameImage.material = _materialList[0];
			frameImage.material.SetFloat("_OutlineAlpha", 1f);
		}

		private void Palsy()
		{
			CancelInvoke("RemovePalsy");
			AbnormalStatus = AbnormalStatus.PALSY;
			GetShaderByStatus();
			CurSpeed = _maxSpeed * 0.8f;
			_ATK = Mathf.FloorToInt(_maxATK * 0.8f);
			Invoke("RemovePalsy", 1f);
		}

		private void RemovePalsy()
		{
			NormalStatus();
			CurSpeed = _maxSpeed;
		}

		private void Dead()
		{
			DdosManager.Enemies.Remove(this);
			AbnormalStatus = AbnormalStatus.DEAD;
			EventManager.NoticeSound(DdosSound.ENEMY_DEAD);
			_isAttacking = false;
			if (base.isActiveAndEnabled)
			{
				DOTween.Kill($"EnemyMove{base.name}");
				CancelInvoke("Attack");
			}
			if (EnemyType == EnemyType.SPLIT)
			{
				Split();
			}
			CurSpeed = 0f;
			hpCanvasGroup.alpha = 0f;
			float energy = 4f;
			if (DdosManager.Lv == 2)
			{
				energy = 1.5f;
			}
			else if (DdosManager.Lv == 3)
			{
				energy = 1.2f;
			}
			if (DdosManager.isTest)
			{
				energy = 50f;
			}
			EventManager.NoticeChangeEnergy(energy);
			if (UnityEngine.Random.Range(0, 100) < 20)
			{
				int coin = DdosManager.EnemyDrop();
				CoinItem component = DdosManager.SpawnPool.Spawn("CoinItem").GetComponent<CoinItem>();
				component.transform.position = base.transform.position;
				component.InitData(coin, isCard: false);
			}
			DeadShader();
		}

		private void Split()
		{
			for (int i = 0; i < 3; i++)
			{
				Enemy component = DdosManager.SpawnPool.Spawn("DDOSEnemy").GetComponent<Enemy>();
				Vector3 localPosition = GetComponent<RectTransform>().localPosition;
				component.InitData(_enemyDic, isBoss: false, EnemyType.SAMLL, localPosition, localPosition - new Vector3(UnityEngine.Random.Range(130, 160), UnityEngine.Random.Range(-150, 150), 0f));
				DdosManager.Enemies.Add(component);
			}
		}

		private void StartCloaking()
		{
			if (base.isActiveAndEnabled)
			{
				StartCoroutine("Cloaking");
			}
		}

		private IEnumerator Cloaking()
		{
			float interval = 0.25f;
			WaitForSeconds waitForSeconds = new WaitForSeconds(interval);
			_isSafe = true;
			enemyCanvasGroup.DOFade(0.2f, interval).SetEase(Ease.Linear);
			yield return waitForSeconds;
			enemyCanvasGroup.DOFade(1f, interval).SetEase(Ease.Linear);
			yield return waitForSeconds;
			_isSafe = false;
		}

		public void StopCloaking()
		{
			_isSafe = false;
			StopCoroutine("Cloaking");
		}

		private void FixedUpdate()
		{
			if (!_isWin && base.transform.localPosition.x > 80f)
			{
				_curSpeed = 0f;
			}
			if (base.transform.localPosition.x < 0f)
			{
				_isAttacking = false;
			}
			base.transform.localPosition += Vector3.right * _curSpeed * Time.deltaTime;
		}

		public override string PoolName()
		{
			return "DDOS";
		}

		protected override void OnSpawnedCallback(SpawnPool pool)
		{
			_isAttacking = false;
			EventManager.onNoticeGameResult += NoticeGameResult;
		}

		private void NoticeGameResult(GameResult obj)
		{
			if (obj == GameResult.SUCCESS)
			{
				Dead();
			}
			else
			{
				Win();
			}
		}

		protected override void OnDespawnedCallback(SpawnPool pool)
		{
			EventManager.onNoticeGameResult -= NoticeGameResult;
			_isAttacking = false;
			StopCloaking();
			CancelInvoke("Attack");
		}

		private void OnDestroy()
		{
			EventManager.onNoticeGameResult -= NoticeGameResult;
		}
	}
}
