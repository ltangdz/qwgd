using UnityEngine;
using UnityEngine.UI;

public class AnimationImage : MonoBehaviour
{
	private SpriteRenderer sp;

	private Image img;

	private void Start()
	{
		sp = GetComponent<SpriteRenderer>();
		img = GetComponent<Image>();
	}

	private void Update()
	{
		img.sprite = sp.sprite;
	}
}
