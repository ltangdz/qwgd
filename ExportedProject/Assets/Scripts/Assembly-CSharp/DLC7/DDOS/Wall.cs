using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.DDOS
{
	public class Wall : DDosMonoBehaviour
	{
		public bool isEnemy;

		private bool _isCanShowHit;

		public Image frameImage;

		private void Awake()
		{
			_isCanShowHit = true;
			base.DdosEventManager.onNoticeWallInjured += NoticeWallInjured;
			base.DdosEventManager.onNoticeGameResult += NoticeGameResult;
		}

		private void NoticeWallInjured(int damaged, bool isEnemy, GameObject from)
		{
			if (this.isEnemy == isEnemy && !this.isEnemy && _isCanShowHit)
			{
				from.GetComponent<Enemy>();
				Debug.Log("from:" + from.name + "----damaged:" + damaged);
				base.DdosEventManager.NoticeSound(DdosSound.OUR_HURT);
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
				other.GetComponent<Enemy>().AttackWall();
			}
		}

		private void OnDestroy()
		{
			base.DdosEventManager.onNoticeWallInjured -= NoticeWallInjured;
			base.DdosEventManager.onNoticeGameResult -= NoticeGameResult;
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
