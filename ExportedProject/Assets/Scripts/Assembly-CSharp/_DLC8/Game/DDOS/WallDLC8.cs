using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Game.DDOS
{
	public class WallDLC8 : DDosMonoBehaviourDLC8
	{
		public bool isEnemy;

		private bool _isCanShowHit;

		public Image frameImage;

		private void Awake()
		{
			_isCanShowHit = true;
			base.DdosEventManagerDlc8.onNoticeWallInjured += NoticeWallInjured;
			base.DdosEventManagerDlc8.onNoticeGameResult += NoticeGameResult;
		}

		private void NoticeWallInjured(int damaged, bool isEnemy, GameObject from)
		{
			if (this.isEnemy == isEnemy && !this.isEnemy && _isCanShowHit)
			{
				from.GetComponent<EnemyDLC8>();
				Debug.Log("from:" + from.name + "----damaged:" + damaged);
				base.DdosEventManagerDlc8.NoticeSound(DdosSound.OUR_HURT);
				_isCanShowHit = false;
				frameImage.transform.DOShakePosition(0.5f, new Vector3(Random.Range(0, 10), Random.Range(0, 16), 0f)).OnComplete(delegate
				{
					_isCanShowHit = true;
				});
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!isEnemy)
			{
				other.TryGetComponent<EnemyDLC8>(out var component);
				if ((bool)component)
				{
					component.AttackWall();
				}
			}
		}

		private void OnDestroy()
		{
			base.DdosEventManagerDlc8.onNoticeWallInjured -= NoticeWallInjured;
			base.DdosEventManagerDlc8.onNoticeGameResult -= NoticeGameResult;
		}

		private void NoticeGameResult(GameResult obj)
		{
			if (obj == GameResult.SUCCESS && isEnemy)
			{
				Object.Destroy(base.gameObject);
			}
			if (obj == GameResult.FAIL && !isEnemy)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}
}
