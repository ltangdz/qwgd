using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class AlubaLoading1 : MonoBehaviour
{
	public delegate void Finished();

	public List<GameObject> loadList;

	public float waitTime = 0.5f;

	public Text _loadingText;

	public Text progressText;

	public string _loadingString = "";

	public bool isAutoLoad = true;

	private int _progress;

	private bool _isLoading;

	private Finished _finishedCallback;

	private void Start()
	{
		if (isAutoLoad)
		{
			BeginLoad();
		}
	}

	private IEnumerator TextLoading()
	{
		WaitForSeconds _loadingWaitInterval = new WaitForSeconds(0.3f);
		while (_loadingText != null)
		{
			for (int i = 0; i < 4; i++)
			{
				StringBuilder stringBuilder = new StringBuilder(I18N.instance.getValue(_loadingString));
				for (int j = 0; j < i; j++)
				{
					stringBuilder.Append(".");
				}
				_loadingText.text = stringBuilder.ToString();
				yield return _loadingWaitInterval;
			}
		}
	}

	public void HideLoading()
	{
		StopCoroutine("TextLoading");
	}

	public void BeginLoad()
	{
		StartCoroutine("StartLoad");
		StartCoroutine("TextLoading");
	}

	private IEnumerator StartLoad()
	{
		_isLoading = true;
		string value = I18N.instance.getValue(_loadingString);
		if ((bool)_loadingText)
		{
			_loadingText.DOText(value, 0f);
		}
		Debug.Log(value);
		loadList = new List<GameObject>(GameObject.FindGameObjectsWithTag("ruqinloading"));
		loadList.Sort((GameObject a, GameObject b) => int.Parse(a.name).CompareTo(int.Parse(b.name)));
		int count = loadList.Count;
		float onceOff = 1f / (float)count * 100f;
		float curProgress = 0f;
		for (int i = 0; i < loadList.Count; i++)
		{
			Image component = loadList[i].GetComponent<Image>();
			Sequence s = DOTween.Sequence();
			s.Append(component.DOFade(1f, waitTime / 3f));
			s.Append(component.DOFade(0.6f, waitTime / 3f));
			s.Append(component.DOFade(0.9f, waitTime / 3f));
			curProgress += onceOff;
			int num = Mathf.FloorToInt(curProgress);
			if (num > 100)
			{
				num = 100;
			}
			DOTween.To(() => _progress, delegate(int x)
			{
				_progress = x;
			}, num, waitTime).SetEase(Ease.Linear);
			yield return new WaitForSeconds(waitTime);
		}
		yield return new WaitForSeconds(0.5f);
		if (_finishedCallback != null)
		{
			_finishedCallback();
		}
	}

	public void FixedUpdate()
	{
		if (_isLoading && !(progressText == null))
		{
			if (_progress >= 100)
			{
				_isLoading = false;
			}
			progressText.text = $"{_progress}%";
		}
	}

	public void AddCallback(Finished callback)
	{
		_finishedCallback = callback;
	}
}
