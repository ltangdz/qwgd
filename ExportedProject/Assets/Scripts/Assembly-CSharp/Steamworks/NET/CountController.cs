using Aluba;
using UnityEngine;

namespace Steamworks.NET
{
	public class CountController
	{
		public ulong GetSteamIdNumber()
		{
			if (!CanUseSteamManager())
			{
				return 0uL;
			}
			return SteamUser.GetSteamID().m_SteamID;
		}

		public static uint GetTimeStamp()
		{
			if (!CanUseSteamManager())
			{
				return (uint)AlubaUtils.TimeStampSeconds();
			}
			return SteamUtils.GetServerRealTime();
		}

		private static void timer1_Tick(object state)
		{
			SteamAPI.RunCallbacks();
		}

		private static bool CanUseSteamManager()
		{
			bool initialized = SteamManager.Initialized;
			if (!initialized)
			{
				Debug.LogError("SteamManager初始化失败");
			}
			return initialized;
		}
	}
}
