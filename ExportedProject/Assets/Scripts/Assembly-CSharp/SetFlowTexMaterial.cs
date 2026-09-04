using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SetFlowTexMaterial : MonoBehaviour
{
	private float widthRate = 1f;

	private float heightRate = 1f;

	private float xOffsetRate;

	private float yOffsetRate;

	public Shader shader;

	public Color color = Color.yellow;

	public float power = 0.55f;

	public float speed = 5f;

	public float largeWidth = 0.003f;

	public float littleWidth = 0.0003f;

	public float length = 0.1f;

	public float skewRadio = 0.2f;

	public float moveTime;

	private float endMoveTime;

	private Image maskableGraphic;

	private Image image;

	public Material imageMat;

	private void Awake()
	{
		maskableGraphic = GetComponent<Image>();
		if ((bool)maskableGraphic)
		{
			image = maskableGraphic;
			if ((bool)image)
			{
				Debug.LogError("image");
				imageMat = new Material(shader);
				widthRate = image.sprite.textureRect.width * 1f / (float)image.sprite.texture.width;
				heightRate = image.sprite.textureRect.height * 1f / (float)image.sprite.texture.height;
				xOffsetRate = image.sprite.textureRect.xMin * 1f / (float)image.sprite.texture.width;
				yOffsetRate = image.sprite.textureRect.yMin * 1f / (float)image.sprite.texture.height;
			}
		}
		image.material = null;
		OnWaitAnim(1000f);
	}

	public void OnWaitAnim(float time)
	{
		Debug.Log(time);
		StopCoroutine("SlowLight");
		endMoveTime = time;
		StartCoroutine("SlowLight");
	}

	private IEnumerator SlowLight()
	{
		if ((bool)image)
		{
			image.material = imageMat;
		}
		moveTime = 0f;
		while (moveTime < endMoveTime)
		{
			moveTime += Time.deltaTime;
			SetShader();
			yield return null;
		}
	}

	private void OnDisable()
	{
		if ((bool)image)
		{
			image.material = null;
		}
		StopCoroutine("SlowLight");
	}

	private void Start()
	{
		SetShader();
	}

	private void Update()
	{
	}

	public void SetShader()
	{
		skewRadio = Mathf.Clamp(skewRadio, 0f, 1f);
		length = Mathf.Clamp(length, 0f, 0.5f);
		imageMat.SetColor("_FlowlightColor", color);
		imageMat.SetFloat("_Power", power);
		imageMat.SetFloat("_MoveSpeed", speed);
		imageMat.SetFloat("_LargeWidth", largeWidth);
		imageMat.SetFloat("_LittleWidth", littleWidth);
		imageMat.SetFloat("_SkewRadio", skewRadio);
		imageMat.SetFloat("_Lengthlitandlar", length);
		imageMat.SetFloat("_MoveTime", moveTime);
		imageMat.SetFloat("_WidthRate", widthRate);
		imageMat.SetFloat("_HeightRate", heightRate);
		imageMat.SetFloat("_XOffset", xOffsetRate);
		imageMat.SetFloat("_YOffset", yOffsetRate);
	}
}
