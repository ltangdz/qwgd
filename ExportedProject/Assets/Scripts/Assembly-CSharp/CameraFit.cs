using UnityEngine;

public class CameraFit : MonoBehaviour
{
	public Camera camera;

	private float designWidth = 1920f;

	private float designHeight = 1080f;

	private float designOrthographicSize = 5.4f;

	private float designScale;

	private float scaleRate;

	private void Start()
	{
		designOrthographicSize = 5.4f;
		designScale = designWidth / designHeight;
		scaleRate = (float)Screen.width / (float)Screen.height;
	}

	private void Update()
	{
		if (scaleRate < designScale)
		{
			float num = scaleRate / designScale;
			camera.orthographicSize = designOrthographicSize / num;
		}
		else
		{
			camera.orthographicSize = designOrthographicSize;
		}
	}
}
