using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;

public class CombatInvoke : MonoBehaviour
{
	public CombatVan combatVan;

	private float time = 15f;

	public void Init(string lastTime)
	{
		time = int.Parse(lastTime);
		GetComponent<I18NText>().updateTranslation2(time + ":00");
		StartCoroutine(Interval());
	}

	private IEnumerator Interval()
	{
		yield return new WaitForSeconds(1f);
		combatVan.percent = 60f;
		combatVan.combatLoad.SetPercent(60f, time + 2f);
		float a = time;
		DOTween.To(() => a, delegate(float x)
		{
			a = x;
		}, 0f, time).OnUpdate(delegate
		{
			string text = a.ToString("f2");
			string text2 = ((text.Split('.')[0].Length >= 2) ? text.Split('.')[0] : ("0" + text.Split('.')[0]));
			string text3 = text.Split('.')[1];
			GetComponent<I18NText>().updateTranslation2(text2 + ":" + text3);
		}).SetEase(Ease.Linear);
		GetComponent<I18NText>().updateTranslation2("00:00");
		yield return new WaitForSeconds(time);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(GetComponent<CanvasGroup>().DOFade(0.2f, 0.1f));
		sequence.Append(GetComponent<CanvasGroup>().DOFade(1f, 0.1f));
		sequence.Play().SetLoops(2);
		yield return new WaitForSeconds(2f);
		combatVan.TimeOut();
	}
}
