using UnityEngine;

namespace DLC7.DDOS
{
	public class DDosMonoBehaviour : MonoBehaviour
	{
		private DDOSManager _ddosManager;

		private DDOSEventManager _ddosEventManager;

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

		public DDOSEventManager DdosEventManager
		{
			get
			{
				if (_ddosEventManager == null)
				{
					_ddosEventManager = DDOSEventManager.Instance;
				}
				return _ddosEventManager;
			}
		}
	}
}
