using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIEraserTexture : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	public Image image;

	public int brushScale = 4;

	private Texture2D texRender;

	private RectTransform mRectTransform;

	private int remainArea;

	private int totalArea;

	private bool[][] colorArr;

	private bool isMove;

	private Vector2 pos = Vector2.zero;

	public void Hide(UnityAction action)
	{
		((Tween)DOTween.To(() => image.color, delegate(Color x)
		{
			image.color = x;
		}, new Color(1f, 1f, 1f, 0f), 0.5f)).OnComplete((TweenCallback)delegate
		{
			base.gameObject.SetActive(value: false);
			action?.Invoke();
		});
	}

	private void Awake()
	{
		mRectTransform = GetComponent<RectTransform>();
	}

	public bool CanSee()
	{
		return (float)remainArea <= (float)totalArea * 0.7f;
	}

	private void Start()
	{
		texRender = new Texture2D((int)image.GetComponent<RectTransform>().rect.size.x, (int)image.GetComponent<RectTransform>().rect.size.y, TextureFormat.ARGB32, mipChain: true);
		remainArea = texRender.width * texRender.height;
		totalArea = remainArea;
		colorArr = new bool[texRender.height][];
		Debug.Log(texRender.width + " + " + texRender.height);
		for (int i = 0; i < colorArr.Length; i++)
		{
			colorArr[i] = new bool[texRender.width];
		}
		Reset();
	}

	public void OnPointerDown(PointerEventData data)
	{
		if (!((float)remainArea <= (float)totalArea * 0.8f))
		{
			Debug.Log("OnPointerDown..." + data.position);
			pos = ConvertSceneToUI(data.position);
			isMove = true;
		}
	}

	public void OnPointerUp(PointerEventData data)
	{
		isMove = false;
		Debug.Log("OnPointerUp..." + data.position);
		OnMouseMove(data.position);
		pos = Vector2.zero;
		if ((float)remainArea <= (float)totalArea * 0.8f)
		{
			Debug.LogError("EnterNext");
		}
	}

	private void Update()
	{
		if (isMove && (float)remainArea > (float)totalArea * 0.2f)
		{
			OnMouseMove(Input.mousePosition);
		}
	}

	private Vector2 ConvertSceneToUI(Vector3 posi)
	{
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mRectTransform, posi, Camera.main, out var localPoint))
		{
			return localPoint;
		}
		return Vector2.zero;
	}

	private void OnMouseMove(Vector2 position)
	{
		pos = ConvertSceneToUI(position);
		Draw(new Rect(pos.x + (float)(texRender.width / 2) - (float)brushScale * 0.5f, pos.y + (float)(texRender.height / 2) - (float)brushScale * 0.5f, brushScale, brushScale));
	}

	private void Reset()
	{
		for (int i = 0; i < texRender.width; i++)
		{
			for (int j = 0; j < texRender.height; j++)
			{
				Color pixel = texRender.GetPixel(i, j);
				pixel.a = 1f;
				texRender.SetPixel(i, j, pixel);
			}
		}
		texRender.Apply();
		image.material.SetTexture("_RendTex", texRender);
	}

	private void Draw(Rect rect)
	{
		bool flag = false;
		for (int i = (int)rect.xMin; i < (int)rect.xMax; i++)
		{
			for (int j = (int)rect.yMin; j < (int)rect.yMax; j++)
			{
				if (i < 0 || i >= texRender.width || j < 0 || j >= texRender.height)
				{
					return;
				}
				if (!colorArr[j][i])
				{
					flag = true;
					colorArr[j][i] = true;
					Color pixel = texRender.GetPixel(i, j);
					remainArea--;
					pixel.a = 0f;
					texRender.SetPixel(i, j, pixel);
				}
			}
		}
		if (flag)
		{
			texRender.Apply();
			image.material.SetTexture("_RendTex", texRender);
		}
	}
}
