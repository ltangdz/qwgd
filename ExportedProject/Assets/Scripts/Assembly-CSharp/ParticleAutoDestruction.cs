using UnityEngine;

public class ParticleAutoDestruction : MonoBehaviour
{
	private ParticleSystem particleSystems;

	private void Start()
	{
		particleSystems = GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		bool flag = true;
		if (!particleSystems.isStopped)
		{
			flag = false;
		}
		if (flag)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
