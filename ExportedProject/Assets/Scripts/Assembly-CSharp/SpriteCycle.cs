using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteCycle : MonoBehaviour
{
	public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

	[Range(0f, 1f)]
	public float offset;

	private float totalWidth = 1f;

	private float mPosition;

	public float position
	{
		get
		{
			return mPosition;
		}
		set
		{
			float x = base.transform.localScale.x;
			mPosition = value;
			if (x > 0f)
			{
				mPosition /= x;
			}
			Vector3 zero = Vector3.zero;
			totalWidth = 0f;
			for (int i = 0; i < spriteRenderers.Count; i++)
			{
				SpriteRenderer spriteRenderer = spriteRenderers[i];
				if ((bool)spriteRenderer && (bool)spriteRenderer.sprite)
				{
					spriteRenderer.transform.localPosition = zero;
					zero.x += spriteRenderer.sprite.bounds.size.x;
					totalWidth += spriteRenderer.sprite.bounds.size.x;
				}
			}
			float num = mPosition % totalWidth;
			for (int j = 0; j < spriteRenderers.Count; j++)
			{
				SpriteRenderer spriteRenderer2 = spriteRenderers[j];
				if ((bool)spriteRenderer2 && (bool)spriteRenderer2.sprite)
				{
					Vector3 localPosition = spriteRenderer2.transform.localPosition + Vector3.right * num;
					if (localPosition.x <= 0f - spriteRenderer2.sprite.bounds.size.x)
					{
						localPosition.x += totalWidth;
					}
					else if (localPosition.x > totalWidth)
					{
						localPosition.x -= totalWidth;
					}
					localPosition.x -= offset * totalWidth;
					spriteRenderer2.transform.localPosition = localPosition;
				}
			}
		}
	}

	private void Awake()
	{
		position = 0f;
	}

	private void OnValidate()
	{
		position = 0f;
	}
}
