using System;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class YulunTimeDialog : CustomDialog
{
	public YulunDialog yulunDialog;

	public int timeDay;

	public Text txtTime;

	public Text txtDay;

	private long timeStamp;

	private long currentTime = 57600L;

	private string needChange = "^yulun_label229";

	private string crtDay = "7";

	private void Start()
	{
		timeStamp = timeDay * 24 * 60 * 60;
		LastDay();
	}

	public void StartCountDown()
	{
		yulunDialog.yulunDataDialog.ChangeVal();
		DOTween.To(() => currentTime, delegate(long x)
		{
			currentTime = x;
		}, currentTime + 28800, yulunDialog.changeTime).SetEase(Ease.Linear).OnUpdate(delegate
		{
			DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(currentTime);
			txtTime.GetComponent<I18NText>().updateTranslation2(dateTime.ToShortTimeString().ToLower().Replace("am", "")
				.Replace("pm", "")
				.ToString());
		})
			.OnComplete(delegate
			{
				LastDay();
				yulunDialog.gameRunning = false;
			});
	}

	private void LastDay()
	{
		float num = Mathf.Ceil((currentTime - 57600) / 60 / 60 / 24);
		string key = string.Format(I18N.instance.getValue(needChange), ((float)timeDay - num).ToString());
		txtDay.GetComponent<I18NText>().updateTranslation2(key);
		crtDay = ((float)timeDay - num).ToString();
		if ((float)timeDay - num <= 0f)
		{
			yulunDialog.gameOver = true;
			yulunDialog.GameResult();
		}
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
