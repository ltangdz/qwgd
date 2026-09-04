using System.Collections.Generic;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.DDOS
{
	public class DDOSBullet : AlubaSpawn
	{
		private int _damage;

		private BulletType _bulletType;

		private float _speed;

		private Vector3 _direction;

		private float _lieTime = 6f;

		private bool _isMove;

		public BoxCollider2D collider;

		public Rigidbody2D rigidbody;

		public Image bulletImage;

		public List<ParticleSystem> particles;

		private bool _isUsed;

		private ParticleSystem _curParticle;

		private DDOSEventManager _eventManager;

		private DDOSManager _ddosManager;

		private bool _isFlood;

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

		protected override void OnSpawnedCallback(SpawnPool pool)
		{
			pool.Despawn(base.transform, _lieTime);
			bulletImage.DOFade(1f, 0f);
			_isUsed = false;
		}

		protected override void OnDespawnedCallback(SpawnPool pool)
		{
			_isMove = false;
			if ((bool)_curParticle)
			{
				_curParticle.gameObject.SetActive(value: false);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (_isUsed)
			{
				return;
			}
			EventManager.NoticeSound(GetSoundType());
			if (other.gameObject.name == "EnmenyWall")
			{
				EventManager.NoticeWallInjured(_damage, isEnemy: true, null);
			}
			else
			{
				Enemy component = other.gameObject.GetComponent<Enemy>();
				if (component.AbnormalStatus == AbnormalStatus.DEAD || component.IsSafe)
				{
					return;
				}
				component.Injured(_bulletType, _damage);
			}
			_isUsed = true;
			_speed = 0f;
			bulletImage.DOFade(0f, 0f);
			_curParticle.gameObject.SetActive(value: true);
			_curParticle.Play(withChildren: true);
			base.Pool.Despawn(base.transform, 1.5f);
		}

		private DdosSound GetSoundType()
		{
			switch (_bulletType)
			{
			case BulletType.NORMAL:
			case BulletType.FLOOD:
				return DdosSound.BULLET_NORMAL;
			case BulletType.BUG:
				return DdosSound.BULLET_BUG;
			case BulletType.ICE:
				return DdosSound.BULLET_ICE;
			case BulletType.PALSY:
				return DdosSound.BULLET_PALSY;
			default:
				return DdosSound.BULLET_NORMAL;
			}
		}

		private void FixedUpdate()
		{
			if (_isMove)
			{
				base.transform.position += _direction * _speed * Time.deltaTime;
			}
		}

		public void Shoot(BulletType bulletType, Transform target, float speed, int damage, bool isFlood)
		{
			_isFlood = isFlood;
			_bulletType = bulletType;
			float num = 1f;
			if (_bulletType == BulletType.ICE || _bulletType == BulletType.BUG)
			{
				num = 0.9f;
			}
			else if (_bulletType == BulletType.PALSY)
			{
				num = 0.8f;
			}
			_damage = Mathf.RoundToInt((float)damage * num);
			InitUI();
			EventManager.NoticeSound(DdosSound.SHOOT);
			Vector2 vector = (target.transform.position - base.transform.position).normalized;
			float num2 = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			base.transform.rotation = Quaternion.AngleAxis(num2 - 180f, Vector3.forward);
			_direction = vector;
			_speed = speed;
			_isMove = true;
		}

		public void FreeShoot(BulletType bulletType, float speed, bool isFlood)
		{
			_isFlood = isFlood;
			_bulletType = bulletType;
			InitUI();
			EventManager.NoticeSound(DdosSound.SHOOT);
			_direction = Vector3.left;
			_speed = speed;
			_isMove = true;
		}

		private void InitUI()
		{
			int num = (int)_bulletType;
			if (_isFlood)
			{
				num = 0;
			}
			for (int i = 0; i < particles.Count; i++)
			{
				if (num == i)
				{
					_curParticle = particles[num];
					_curParticle.gameObject.SetActive(value: true);
				}
				else
				{
					particles[num].gameObject.SetActive(value: false);
				}
			}
			string text = "rq_30";
			switch (_bulletType)
			{
			case BulletType.ICE:
				text = "rq_31";
				break;
			case BulletType.PALSY:
				text = "rq_39";
				break;
			case BulletType.FLOOD:
				text = "rq_33";
				break;
			case BulletType.BUG:
				text = "rq_32";
				break;
			}
			if (_isFlood)
			{
				text = "rq_33";
			}
			bulletImage.sprite = DdosManager.ddosAtlas.GetSprite(text);
		}

		public override string PoolName()
		{
			return "DDOS";
		}
	}
}
