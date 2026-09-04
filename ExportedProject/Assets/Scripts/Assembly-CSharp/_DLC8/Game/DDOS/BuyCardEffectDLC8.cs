using DG.Tweening;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Game.DDOS
{
	public class BuyCardEffectDLC8 : AlubaSpawnDLC8
	{
		public Image frameImage;

		public Image contentImage;

		public float speed = 10f;

		private DDOSManagerDLC8 _ddosManagerDlc8;

		private DDOSEventManagerDLC8 _eventManagerDlc8;

		public DDOSManagerDLC8 DdosManagerDlc8
		{
			get
			{
				if (_ddosManagerDlc8 == null)
				{
					_ddosManagerDlc8 = DDOSManagerDLC8.Instance;
				}
				return _ddosManagerDlc8;
			}
		}

		public DDOSEventManagerDLC8 EventManagerDlc8
		{
			get
			{
				if (_eventManagerDlc8 == null)
				{
					_eventManagerDlc8 = DDOSEventManagerDLC8.Instance;
				}
				return _eventManagerDlc8;
			}
		}

		public void Move(CardDLC8 cardDlc8, BagGridDLC8 bagGridDlc8)
		{
			DdosManagerDlc8.InitImage(cardDlc8.FramePath, frameImage);
			DdosManagerDlc8.InitImage(cardDlc8.ContentPath, contentImage);
			Vector3 position = bagGridDlc8.transform.position;
			float duration = Mathf.Abs(Vector2.Distance(base.transform.position, position)) / speed;
			float num = 0.5f;
			base.transform.localScale = new Vector3(num, num, num);
			base.transform.DOScale(1f, duration);
			base.transform.DOMove(position, duration).OnComplete(delegate
			{
				bagGridDlc8.AddCard(cardDlc8);
				base.Pool.Despawn(base.transform);
			});
		}

		protected override void OnSpawnedCallback(SpawnPool pool)
		{
		}

		protected override void OnDespawnedCallback(SpawnPool pool)
		{
		}

		public override string PoolName()
		{
			return "DDOSDLC8";
		}
	}
}
