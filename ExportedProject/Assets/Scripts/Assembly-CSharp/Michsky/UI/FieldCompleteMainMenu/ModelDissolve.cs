using UnityEngine;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class ModelDissolve : MonoBehaviour
	{
		[Header("VARIABLES")]
		public Material dissolveMaterial;

		[Range(0f, 1f)]
		public float dissolveValue = 1f;

		[Range(0.1f, 2.5f)]
		public float animationSpeed = 0.5f;

		[Header("SETTINGS")]
		public bool playAtStart;

		private bool playing;

		private ParticleSystem ps;

		private void Start()
		{
			ps = GetComponentInChildren<ParticleSystem>();
			if (playAtStart)
			{
				dissolveValue = 1f;
				Dissolve();
			}
			else
			{
				dissolveValue = 1f;
			}
		}

		public void Disable()
		{
			if (playing)
			{
				animationSpeed -= 0.4f;
			}
			playing = false;
		}

		public void Dissolve()
		{
			if (!playing)
			{
				animationSpeed += 0.4f;
			}
			playing = true;
		}

		private void Update()
		{
			if (playing)
			{
				if (dissolveValue == 0f || dissolveValue >= 0f)
				{
					dissolveValue -= Time.deltaTime / animationSpeed;
					ps.Play();
				}
			}
			else if (dissolveValue == 1f || dissolveValue <= 1f)
			{
				dissolveValue += Time.deltaTime / animationSpeed;
			}
			dissolveMaterial.SetFloat("_cutoff", dissolveValue);
		}
	}
}
