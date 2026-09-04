using UnityEngine;

namespace _DLC8.Main
{
	public class AppGroup : MonoBehaviour
	{
		public AppItem appItemPrefab;

		private AppItem[] _appItemList;

		public AppItem[] AppItemList => _appItemList;

		private void Start()
		{
			_appItemList = GetComponentsInChildren<AppItem>();
		}

		public AppItem AppItemByCityGameType(CityGameType type)
		{
			for (int i = 0; i < _appItemList.Length; i++)
			{
				if (_appItemList[i].gameType == type)
				{
					return _appItemList[i];
				}
			}
			return null;
		}
	}
}
