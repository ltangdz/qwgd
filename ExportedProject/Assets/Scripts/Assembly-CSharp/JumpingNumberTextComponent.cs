using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class JumpingNumberTextComponent : MonoBehaviour
{
	[SerializeField]
	[Tooltip("按最高位起始顺序设置每位数字Text（显示组）")]
	private List<Text> _numbers;

	[SerializeField]
	[Tooltip("按最高位起始顺序设置每位数字Text（替换组）")]
	private List<Text> _unactiveNumbers;

	[SerializeField]
	private float _duration = 1.5f;

	[SerializeField]
	private float _rollingDuration = 0.05f;

	private int _speed;

	[SerializeField]
	private float _delay = 0.008f;

	private Vector2 _numberSize;

	private int _curNumber;

	private int _fromNumber;

	private int _toNumber;

	private List<Tweener> _tweener = new List<Tweener>();

	private bool _isJumping;

	public Action OnComplete;

	private float _different;

	public float duration
	{
		get
		{
			return _duration;
		}
		set
		{
			_duration = value;
		}
	}

	public float different => _different;

	public int number
	{
		get
		{
			return _toNumber;
		}
		set
		{
			if (_toNumber != value)
			{
				Change(_curNumber, _toNumber);
			}
		}
	}

	private void Awake()
	{
		if (_numbers.Count != 0 && _unactiveNumbers.Count != 0)
		{
			_numberSize = _numbers[0].rectTransform.sizeDelta;
		}
	}

	public void Change(int from, int to)
	{
		if (!_isJumping || _fromNumber != from || _toNumber != to)
		{
			bool flag = _toNumber == from && ((to - from > 0 && _different > 0f) || (to - from < 0 && _different < 0f));
			if (!(_isJumping && flag))
			{
				_fromNumber = from;
				_curNumber = _fromNumber;
			}
			_toNumber = to;
			_different = _toNumber - _fromNumber;
			_speed = (int)Math.Ceiling(_different / (_duration * (1f / _rollingDuration)));
			_speed = ((_speed != 0) ? _speed : ((_different > 0f) ? 1 : (-1)));
			SetNumber(_curNumber, isTween: false);
			_isJumping = true;
			StopCoroutine("DoJumpNumber");
			StartCoroutine("DoJumpNumber");
		}
	}

	private IEnumerator DoJumpNumber()
	{
		while (true)
		{
			if (_speed > 0)
			{
				_curNumber = Math.Min(_curNumber + _speed, _toNumber);
			}
			else if (_speed < 0)
			{
				_curNumber = Math.Max(_curNumber + _speed, _toNumber);
			}
			SetNumber(_curNumber, isTween: true);
			if (_curNumber == _toNumber)
			{
				StopCoroutine("DoJumpNumber");
				_isJumping = false;
				if (OnComplete != null)
				{
					OnComplete();
				}
				yield return null;
			}
			yield return new WaitForSeconds(_rollingDuration);
		}
	}

	public void SetNumber(int v, bool isTween)
	{
		char[] array = v.ToString().ToCharArray();
		Array.Reverse((Array)array);
		string text = new string(array);
		if (!isTween)
		{
			for (int i = 0; i < _numbers.Count; i++)
			{
				if (i < text.Count())
				{
					_numbers[i].text = text[i].ToString() ?? "";
				}
				else
				{
					_numbers[i].text = "0";
				}
			}
			return;
		}
		while (_tweener.Count > 0)
		{
			_tweener[0].Complete();
			_tweener.RemoveAt(0);
		}
		for (int j = 0; j < _numbers.Count; j++)
		{
			if (j < text.Count())
			{
				_unactiveNumbers[j].text = text[j].ToString() ?? "";
			}
			else
			{
				_unactiveNumbers[j].text = "0";
			}
			_unactiveNumbers[j].rectTransform.anchoredPosition = new Vector2(_unactiveNumbers[j].rectTransform.anchoredPosition.x, (float)((_speed <= 0) ? 1 : (-1)) * _numberSize.y);
			_numbers[j].rectTransform.anchoredPosition = new Vector2(_unactiveNumbers[j].rectTransform.anchoredPosition.x, 0f);
			if (_unactiveNumbers[j].text != _numbers[j].text)
			{
				DoTween(_numbers[j], (float)((_speed > 0) ? 1 : (-1)) * _numberSize.y, _delay * (float)j);
				DoTween(_unactiveNumbers[j], 0f, _delay * (float)j);
				Text value = _numbers[j];
				_numbers[j] = _unactiveNumbers[j];
				_unactiveNumbers[j] = value;
			}
		}
	}

	public void DoTween(Text text, float endValue, float delay)
	{
		Tweener item = DOTween.To(() => text.rectTransform.anchoredPosition, delegate(Vector2 x)
		{
			text.rectTransform.anchoredPosition = x;
		}, new Vector2(text.rectTransform.anchoredPosition.x, endValue), _rollingDuration - delay).SetDelay(delay);
		_tweener.Add(item);
	}

	[ContextMenu("测试数字变化")]
	public void TestChange()
	{
		Change(UnityEngine.Random.Range(1, 1), UnityEngine.Random.Range(1, 100000));
	}
}
