using UnityEngine;

public class FadeInOutControl : MonoBehaviour
{
	private FadeInOut fadeInOut;

	public GameObject rawImage;

	public string hideColor;

	private void Start()
	{
		hideColor = "black";
		rawImage.SetActive(value: true);
		fadeInOut = rawImage.GetComponent<FadeInOut>();
	}

	public void BackGroundControl(bool b)
	{
		fadeInOut.hideColor = hideColor;
		if (b)
		{
			fadeInOut.isBlack = true;
		}
		else
		{
			fadeInOut.isBlack = false;
		}
	}
}
