using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class img_logo : MonoBehaviour
{
	[SerializeField]
	private Image img_logo0;

	[SerializeField]
	private SpriteRenderer sprite_logo;

	[SerializeField]
	private List<Sprite> sprites = new List<Sprite>();

	public void LastSprite()
	{
		sprite_logo.enabled = false;
		if (I18N.instance.gameLang.Equals(LanguageCode.CN))
		{
			img_logo0.sprite = sprites[0];
		}
		else if (I18N.instance.gameLang.Equals(LanguageCode.EN))
		{
			img_logo0.sprite = sprites[1];
		}
		else if (I18N.instance.gameLang.Equals(LanguageCode.TC))
		{
			img_logo0.sprite = sprites[2];
		}
		GetComponent<Animator>().enabled = false;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
