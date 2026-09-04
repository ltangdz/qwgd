using PathologicalGames;
using UnityEngine;

namespace DLC7.DDOS
{
	public abstract class AlubaSpawn : MonoBehaviour
	{
		private SpawnPool _pool;

		private string poolName = "pool";

		public SpawnPool Pool
		{
			get
			{
				if (_pool == null)
				{
					_pool = PoolManager.Pools[poolName];
				}
				return _pool;
			}
		}

		protected abstract void OnSpawnedCallback(SpawnPool pool);

		protected abstract void OnDespawnedCallback(SpawnPool pool);

		public abstract string PoolName();

		private void OnSpawned(SpawnPool pool)
		{
			PoolName();
			_pool = pool;
			OnSpawnedCallback(pool);
		}

		private void OnDespawned(SpawnPool pool)
		{
			OnDespawnedCallback(pool);
		}
	}
}
