using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class LineRendererInfo : MonoBehaviour
{
	[SerializeField]
	private FingercodeDialog fingercodeDialog;

	public List<Vector2> vecs = new List<Vector2>();

	[SerializeField]
	private UILineRenderer uILineRenderer;

	public int count;

	[SerializeField]
	private string password;

	[SerializeField]
	private Transform img_lock;

	[SerializeField]
	private Text txt_tip;

	public string currentpw;

	[SerializeField]
	private List<fingercodeitem> fingercodeitems = new List<fingercodeitem>();

	[SerializeField]
	private Color redcolor;

	[SerializeField]
	private Color greencolor;

	public void Init(string pw)
	{
		password = pw;
	}

	public void RefreshLine()
	{
		uILineRenderer.Points = vecs.ToArray();
	}

	public void RefreshLine(Vector2 pos)
	{
		if (vecs.Count > 0)
		{
			vecs[vecs.Count - 1] = pos;
		}
		uILineRenderer.Points = vecs.ToArray();
	}

	public void AddDotToLine(Vector2 pos)
	{
		vecs.Add(pos);
		uILineRenderer.Points = vecs.ToArray();
	}

	public void CheckPw()
	{
		if (!currentpw.Equals(password))
		{
			for (int i = 0; i < fingercodeitems.Count; i++)
			{
				fingercodeitems[i].ShowRed();
			}
			uILineRenderer.color = redcolor;
			img_lock.DOShakePosition(0.2f).SetLoops(2);
			txt_tip.GetComponent<I18NText>().updateTranslation2("^invadephone17");
			txt_tip.color = redcolor;
			StartCoroutine(Shake(txt_tip.gameObject));
		}
		else
		{
			fingercodeDialog.OpenLock();
			for (int j = 0; j < fingercodeitems.Count; j++)
			{
				fingercodeitems[j].Lock();
			}
		}
	}

	private IEnumerator Shake(GameObject obj)
	{
		obj.GetComponent<RectTransform>().DOLocalMoveX(-5f, 0.05f);
		yield return new WaitForSeconds(0.05f);
		obj.GetComponent<RectTransform>().DOLocalMoveX(5f, 0.05f);
		yield return new WaitForSeconds(0.05f);
		obj.GetComponent<RectTransform>().DOLocalMoveX(-5f, 0.05f);
		yield return new WaitForSeconds(0.05f);
		obj.GetComponent<RectTransform>().DOLocalMoveX(5f, 0.05f);
		yield return new WaitForSeconds(0.05f);
		obj.GetComponent<RectTransform>().DOLocalMoveX(0f, 0.05f);
	}

	public void ResetLine()
	{
		vecs.Clear();
		uILineRenderer.color = greencolor;
		uILineRenderer.Points = vecs.ToArray();
		count = 0;
		currentpw = "";
		txt_tip.GetComponent<I18NText>().updateTranslation2("^invadephone16");
		txt_tip.color = greencolor;
	}
}
