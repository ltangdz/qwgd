using DG.Tweening;
using UnityEngine;

public class testnoise : MonoBehaviour
{
	public Material mmm;

	[SerializeField]
	[Range(0f, 1f)]
	private float _scanLineJitter;

	[SerializeField]
	[Range(0f, 1f)]
	private float _verticalJump;

	[SerializeField]
	[Range(0f, 1f)]
	private float _horizontalShake;

	[SerializeField]
	[Range(0f, 1f)]
	private float _colorDrift;

	private float _verticalJumpTime;

	public float scanLineJitter
	{
		get
		{
			return _scanLineJitter;
		}
		set
		{
			_scanLineJitter = value;
		}
	}

	public float verticalJump
	{
		get
		{
			return _verticalJump;
		}
		set
		{
			_verticalJump = value;
		}
	}

	public float horizontalShake
	{
		get
		{
			return _horizontalShake;
		}
		set
		{
			_horizontalShake = value;
		}
	}

	public float colorDrift
	{
		get
		{
			return _colorDrift;
		}
		set
		{
			_colorDrift = value;
		}
	}

	private void Awake()
	{
		DOTween.To(() => _scanLineJitter, delegate(float x)
		{
			_scanLineJitter = x;
		}, 1f, 0.6f).SetLoops(-1, LoopType.Yoyo);
		DOTween.To(() => _colorDrift, delegate(float x)
		{
			_colorDrift = x;
		}, 0.1f, 1f).SetLoops(-1, LoopType.Yoyo);
	}

	public void Update()
	{
		_verticalJumpTime += Time.deltaTime * _verticalJump * 11.3f;
		float y = Mathf.Clamp01(1f - _scanLineJitter * 1.2f);
		float x = 0.002f + Mathf.Pow(_scanLineJitter, 3f) * 0.05f;
		mmm.SetVector("_ScanLineJitter", new Vector2(x, y));
		Vector2 vector = new Vector2(_verticalJump, _verticalJumpTime);
		mmm.SetVector("_VerticalJump", vector);
		mmm.SetFloat("_HorizontalShake", _horizontalShake * 0.2f);
		Vector2 vector2 = new Vector2(_colorDrift * 0.04f, Time.time * 606.11f);
		mmm.SetVector("_ColorDrift", vector2);
	}
}
