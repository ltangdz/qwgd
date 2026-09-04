using System;
using UnityEngine;
using UnityEngine.Events;

namespace Steamworks.NET
{
	public class SteamStats
	{
		private CallResult<GlobalStatsReceived_t> m_statsReceived = new CallResult<GlobalStatsReceived_t>();

		private UnityAction<long> _callback;

		private string _name;

		private int _score;

		public void UpdateState(string name, int score)
		{
			_score = score;
			_name = name;
			SteamUserStats.RequestCurrentStats();
			SteamUserStats.SetStat(name, score);
			SteamUserStats.StoreStats();
		}

		private void OnUpdateStates(GlobalStatsReceived_t param, bool biofailure)
		{
			SteamUserStats.SetStat(_name, _score);
		}

		public void Init(string name, UnityAction<long> callback)
		{
			_callback = callback;
			_name = name;
			try
			{
				if (SteamUserStats.RequestCurrentStats())
				{
					SteamAPICall_t hAPICall = SteamUserStats.RequestGlobalStats(60);
					m_statsReceived.Set(hAPICall, OnReceivedStates);
				}
			}
			catch (Exception message)
			{
				Debug.Log(message);
			}
		}

		private void OnReceivedStates(GlobalStatsReceived_t pCallback, bool failure)
		{
			if (pCallback.m_eResult == EResult.k_EResultOK && _callback != null)
			{
				SteamUserStats.GetGlobalStat(_name, out long pData);
				_callback(pData);
			}
		}
	}
}
