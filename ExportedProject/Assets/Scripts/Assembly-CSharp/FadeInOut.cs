using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
	public bool isBlack;

	public float fadeSpeed = 0.7f;

	public string hideColor = "black";

	private RawImage rawImage;

	private void Start()
	{
		GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
		rawImage = GetComponent<RawImage>();
		rawImage.color = Color.clear;
	}

	private void Update()
	{
		if (rawImage.color.a > 0.001f && !isBlack)
		{
			rawImage.color = Color.Lerp(rawImage.color, Color.clear, Time.deltaTime * fadeSpeed * 0.4f);
			hideColor = "black";
		}
		if (rawImage.color.a <= 0.999f && isBlack)
		{
			rawImage.color = Color.Lerp(rawImage.color, Color.black, Time.deltaTime * fadeSpeed);
		}
	}
}
