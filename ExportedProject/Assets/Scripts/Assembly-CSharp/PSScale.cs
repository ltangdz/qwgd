using UnityEngine;

public class PSScale : MonoBehaviour
{
	private ParticleSystem[] ps;

	public float psScaleFloat = 0.5f;

	private void Start()
	{
		ParticleSystem[] componentsInChildren = base.transform.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem obj in componentsInChildren)
		{
			ParticleSystem.MainModule main = obj.main;
			main.scalingMode = ParticleSystemScalingMode.Local;
			obj.transform.localScale = new Vector3(psScaleFloat, psScaleFloat, psScaleFloat);
		}
	}

	private void Update()
	{
	}
}
