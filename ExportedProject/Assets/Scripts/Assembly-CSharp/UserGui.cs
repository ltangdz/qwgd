using UnityEngine;

public class UserGui : MonoBehaviour
{
	private ParticleSystem particleSystem;

	private ParticleSystem.ForceOverLifetimeModule forceMode;

	private void Start()
	{
		particleSystem = GetComponent<ParticleSystem>();
		forceMode = particleSystem.forceOverLifetime;
	}

	private void Update()
	{
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 30f, 50f, 30f), "left"))
		{
			ParticleSystem.MinMaxCurve x = forceMode.x;
			x.constantMax -= 0.5f;
			forceMode.x = x;
		}
		if (GUI.Button(new Rect(10f, 70f, 50f, 30f), "right"))
		{
			ParticleSystem.MinMaxCurve x2 = forceMode.x;
			x2.constantMax += 0.5f;
			forceMode.x = x2;
		}
		if (GUI.Button(new Rect(10f, 110f, 50f, 30f), "big"))
		{
			particleSystem.startSize *= 1.11f;
			particleSystem.startLifetime *= 1.11f;
		}
		if (GUI.Button(new Rect(10f, 150f, 50f, 30f), "small"))
		{
			particleSystem.startSize *= 0.9f;
			particleSystem.startLifetime *= 0.9f;
		}
	}
}
