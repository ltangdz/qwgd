using UnityEngine;

namespace _DLC8.Game.DDOS
{
	public class DDosMonoBehaviourDLC8 : MonoBehaviour
	{
		private DDOSManagerDLC8 _ddosManagerDlc8;

		private DDOSEventManagerDLC8 _ddosEventManagerDlc8;

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

		public DDOSEventManagerDLC8 DdosEventManagerDlc8
		{
			get
			{
				if (_ddosEventManagerDlc8 == null)
				{
					_ddosEventManagerDlc8 = DDOSEventManagerDLC8.Instance;
				}
				return _ddosEventManagerDlc8;
			}
		}
	}
}
