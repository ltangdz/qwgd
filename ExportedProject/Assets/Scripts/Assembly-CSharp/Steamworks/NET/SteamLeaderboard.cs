using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using _DLC8.Main.Rank;

namespace Steamworks.NET
{
	public class SteamLeaderboard
	{
		private const ELeaderboardUploadScoreMethod s_leaderboardMethod = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest;

		private SteamLeaderboard_t s_currentLeaderboard;

		private bool s_initialized;

		private CallResult<LeaderboardFindResult_t> m_findResult = new CallResult<LeaderboardFindResult_t>();

		private CallResult<LeaderboardScoreUploaded_t> m_uploadResult = new CallResult<LeaderboardScoreUploaded_t>();

		private CallResult<LeaderboardScoresDownloaded_t> m_downloadResult = new CallResult<LeaderboardScoresDownloaded_t>();

		private UnityAction<List<LaborerRankData>> _downloadCallback;

		private UnityAction<int> _updateCallback;

		private UnityAction<bool> _initCallback;

		private string s_leaderboardName = "";

		private bool _isTime;

		public bool Init(string leaderboardName, bool isTime, UnityAction<bool> callback)
		{
			_initCallback = callback;
			_isTime = isTime;
			s_leaderboardName = leaderboardName;
			try
			{
				SteamAPICall_t hAPICall = SteamUserStats.FindOrCreateLeaderboard(s_leaderboardName, isTime ? ELeaderboardSortMethod.k_ELeaderboardSortMethodAscending : ELeaderboardSortMethod.k_ELeaderboardSortMethodDescending, (!_isTime) ? ELeaderboardDisplayType.k_ELeaderboardDisplayTypeNumeric : ELeaderboardDisplayType.k_ELeaderboardDisplayTypeTimeSeconds);
				m_findResult.Set(hAPICall, OnLeaderboardFindResult);
				return true;
			}
			catch (Exception)
			{
				Debug.LogError("SteamLeaderboard初始化错误");
			}
			return false;
		}

		public bool DownloadUser(UnityAction<List<LaborerRankData>> downloadCallback)
		{
			if (!s_initialized)
			{
				Debug.Log("Can't DownloadUser to the leaderboard because isn't init yet");
				return false;
			}
			try
			{
				_downloadCallback = downloadCallback;
				SteamAPICall_t hAPICall = SteamUserStats.DownloadLeaderboardEntriesForUsers(s_currentLeaderboard, new CSteamID[1] { SteamUser.GetSteamID() }, 1);
				m_downloadResult.Set(hAPICall, OnLeaderboardDownloadResult);
			}
			catch (Exception)
			{
				Debug.LogError("DownloadUser错误");
			}
			return true;
		}

		public bool DownloadRank(int count, UnityAction<List<LaborerRankData>> downloadCallback)
		{
			if (!s_initialized)
			{
				Debug.Log("Can't DownloadRank to the leaderboard because isn't init yet");
				return false;
			}
			try
			{
				_downloadCallback = downloadCallback;
				SteamAPICall_t hAPICall = SteamUserStats.DownloadLeaderboardEntries(s_currentLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 0, count);
				m_downloadResult.Set(hAPICall, OnLeaderboardDownloadResult);
			}
			catch (Exception)
			{
				Debug.LogError("DownloadUser错误");
			}
			return true;
		}

		public bool UpdateScore(int score, UnityAction<int> updateCallback)
		{
			if (score < 1)
			{
				return false;
			}
			if (!s_initialized)
			{
				Debug.Log("Can't upload to the leaderboard because isn't loadded yet");
				return false;
			}
			try
			{
				_updateCallback = updateCallback;
				Debug.Log("uploading score(" + score + ") to steam leaderboard(" + s_leaderboardName + ")");
				SteamAPICall_t hAPICall = SteamUserStats.UploadLeaderboardScore(s_currentLeaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest, score, null, 0);
				m_uploadResult.Set(hAPICall, OnLeaderboardUploadResult);
			}
			catch (Exception)
			{
				Debug.LogError("UpdateScore错误");
			}
			return true;
		}

		private void OnLeaderboardFindResult(LeaderboardFindResult_t pCallback, bool failure)
		{
			Debug.Log("STEAM LEADERBOARDS: Found - " + pCallback.m_bLeaderboardFound + " leaderboardID - " + pCallback.m_hSteamLeaderboard.m_SteamLeaderboard);
			s_currentLeaderboard = pCallback.m_hSteamLeaderboard;
			s_initialized = true;
			if (_initCallback != null)
			{
				_initCallback(pCallback.m_bLeaderboardFound == 1);
			}
		}

		private void OnLeaderboardUploadResult(LeaderboardScoreUploaded_t pCallback, bool failure)
		{
			Debug.Log("STEAM LEADERBOARDS: failure - " + failure.ToString() + " Completed - " + pCallback.m_bSuccess + " NewRank: " + pCallback.m_nGlobalRankNew + " Score " + pCallback.m_nScore + " HasChanged - " + pCallback.m_bScoreChanged);
			if (_updateCallback != null)
			{
				_updateCallback(pCallback.m_nGlobalRankNew);
			}
		}

		private void OnLeaderboardDownloadResult(LeaderboardScoresDownloaded_t pCallback, bool failure)
		{
			Debug.LogError("OnLeaderboardDownloadResult failure:" + failure + "pCallback" + pCallback);
			if (!failure)
			{
				int cEntryCount = pCallback.m_cEntryCount;
				List<LaborerRankData> list = new List<LaborerRankData>();
				for (int i = 0; i < cEntryCount; i++)
				{
					SteamUserStats.GetDownloadedLeaderboardEntry(pCallback.m_hSteamLeaderboardEntries, i, out var pLeaderboardEntry, null, 0);
					string friendPersonaName = SteamFriends.GetFriendPersonaName(pLeaderboardEntry.m_steamIDUser);
					int nGlobalRank = pLeaderboardEntry.m_nGlobalRank;
					int nScore = pLeaderboardEntry.m_nScore;
					LaborerRankData laborerRankData = new LaborerRankData();
					laborerRankData.Init(nGlobalRank, friendPersonaName, nScore, _isTime);
					list.Add(laborerRankData);
				}
				if (_downloadCallback != null)
				{
					_downloadCallback(list);
				}
			}
		}

		public void RunCallback()
		{
			try
			{
				SteamAPI.RunCallbacks();
			}
			catch (Exception)
			{
				Debug.Log("RunCallback错误");
			}
		}
	}
}
