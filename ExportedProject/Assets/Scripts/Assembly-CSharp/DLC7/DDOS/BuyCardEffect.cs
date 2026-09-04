using DG.Tweening;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.DDOS
{
	public class BuyCardEffect : AlubaSpawn
	{
		public Image frameImage;

		public Image contentImage;

		public float speed = 10f;

		private DDOSManager _ddosManager;

		private DDOSEventManager _eventManager;

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

		public void Move(Card card, BagGrid bagGrid)
		{
			DdosManager.InitImage(card.FramePath, frameImage);
			DdosManager.InitImage(card.ContentPath, contentImage);
			Vector3 position = bagGrid.transform.position;
			float duration = Mathf.Abs(Vector2.Distance(base.transform.position, position)) / speed;
			float num = 0.5f;
			base.transform.localScale = new Vector3(num, num, num);
			base.transform.DOScale(1f, duration);
			base.transform.DOMove(position, duration).OnComplete(delegate
			{
				bagGrid.AddCard(card);
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
			return "DDOS";
		}
	}
}
