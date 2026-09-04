using Steamworks;
using UnityEngine;

public class SteamAchi : MonoBehaviour
{
	public void UnlockAchievements(string achiname)
	{
		if (SteamManager.Initialized)
		{
			SteamUserStats.SetAchievement(achiname);
			SteamUserStats.StoreStats();
		}
	}

	public bool GetAchievement(string achiname)
	{
		if (!SteamManager.Initialized)
		{
			return false;
		}
		bool pbAchieved = false;
		SteamUserStats.GetAchievement(achiname, out pbAchieved);
		return pbAchieved;
	}

	public int GetStat(string globalname)
	{
		if (!SteamManager.Initialized)
		{
			return -1;
		}
		int pData = -1;
		SteamUserStats.GetStat(globalname, out pData);
		return pData;
	}

	public void SetGlobalStat(string globalname, int stat, string achiname)
	{
		if (SteamManager.Initialized)
		{
			SteamUserStats.SetStat(globalname, stat);
			SteamUserStats.StoreStats();
		}
	}

	public void CheckHopeAchi()
	{
		if (SteamManager.Initialized && GetStat("stat_hope0") == 1 && GetStat("stat_hope1") == 1 && GetStat("stat_hope2") == 1 && GetStat("stat_hope4") == 1 && GetStat("stat_hope5") == 1 && GetStat("stat_hope8") == 1 && GetStat("stat_hope10") == 1)
		{
			UnlockAchievements("allknow");
		}
	}
}
