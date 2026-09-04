using System;
using Dlc.Catch.model;
using UnityEngine;

public class CatchEvent
{
	private static CatchEvent _instance;

	public static CatchEvent Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new CatchEvent();
			}
			return _instance;
		}
	}

	public event Action<PoliceAI> onClickPolice;

	public event Action<Vector2, int> onClickPath;

	public event Action<WayPoint, CarBase> noticeCarPosition;

	public event Action onNoticeStart;

	public event Action onNoticeEnemyShow;

	public event Action onNoticePoliceShow;

	public event Action onNoticeShowSearch;

	public event Action<CatchEventEnum> onNoticeNextEvent;

	public event Action<CatchLoadingStep> onNoticeLoading;

	public event Action<CatchSpeakRole, float> OnNoticeSpeak;

	public void NoticeLoading(CatchLoadingStep step)
	{
		if (this.onNoticeLoading != null)
		{
			this.onNoticeLoading(step);
		}
	}

	public void NoticeSpeak(CatchSpeakRole role, float time)
	{
		if (this.OnNoticeSpeak != null)
		{
			this.OnNoticeSpeak(role, time);
		}
	}

	public void NoticeEnemyShow()
	{
		if (this.onNoticeEnemyShow != null)
		{
			this.onNoticeEnemyShow();
		}
	}

	public void NoticeNextEvent(CatchEventEnum nextEvent)
	{
		if (this.onNoticeNextEvent != null)
		{
			this.onNoticeNextEvent(nextEvent);
		}
	}

	public void NoticeShowSearch()
	{
		if (this.onNoticeShowSearch != null)
		{
			this.onNoticeShowSearch();
		}
	}

	public void NoticePoliceShow()
	{
		if (this.onNoticePoliceShow != null)
		{
			this.onNoticePoliceShow();
		}
	}

	public void NoticeStart()
	{
		if (this.onNoticeStart != null)
		{
			this.onNoticeStart();
		}
	}

	public void ClickPolice(PoliceAI d)
	{
		if (this.onClickPolice != null)
		{
			this.onClickPolice(d);
		}
	}

	public void NoticeCarPosition(WayPoint d, CarBase carBase)
	{
		if (this.noticeCarPosition != null)
		{
			this.noticeCarPosition(d, carBase);
		}
	}

	public void ClickPath(Vector2 p, int pathIndex)
	{
		if (this.onClickPath != null)
		{
			this.onClickPath(p, pathIndex);
		}
	}
}
