using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Orbit")]
public class OrbitCam : MonoBehaviour
{
	public Transform target;

	public float distance = 10f;

	private float x;

	public float xSpeed = 250f;

	private float y;

	public int yMaxLimit = 80;

	public int yMinLimit = -20;

	public float ySpeed = 120f;

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	public void LateUpdate()
	{
		if (target != null)
		{
			x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
			y = ClampAngle(y, yMinLimit, yMaxLimit);
			Quaternion quaternion = Quaternion.Euler(y, x, 0f);
			Vector3 position = quaternion * new Vector3(0f, 0f, 0f - distance) + target.position;
			base.transform.rotation = quaternion;
			base.transform.position = position;
		}
	}

	public void Start()
	{
		Vector3 eulerAngles = base.transform.eulerAngles;
		x = eulerAngles.y;
		y = eulerAngles.x;
	}
}
