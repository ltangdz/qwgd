using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickItem : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public bool isselect;

	[SerializeField]
	private Image img_bk;

	[SerializeField]
	private Text txt_content;

	[SerializeField]
	private Sprite[] sprites;

	[SerializeField]
	private Color[] colors;

	public Reasoning4009Step01 reasoning4009Step01;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (reasoning4009Step01.iscanclick)
		{
			if (!isselect)
			{
				img_bk.sprite = sprites[1];
				txt_content.color = colors[1];
				isselect = true;
			}
			else
			{
				Cancel();
			}
		}
	}

	public void Cancel()
	{
		isselect = false;
		img_bk.sprite = sprites[0];
		txt_content.color = colors[0];
	}

	public void StartRed()
	{
		if (isselect)
		{
			StartCoroutine(StartRedAni());
		}
	}

	private IEnumerator StartRedAni()
	{
		img_bk.sprite = sprites[2];
		txt_content.color = colors[2];
		yield return new WaitForSeconds(0.2f);
		img_bk.sprite = sprites[0];
		txt_content.color = colors[0];
		yield return new WaitForSeconds(0.2f);
		img_bk.sprite = sprites[2];
		txt_content.color = colors[2];
		yield return new WaitForSeconds(0.2f);
		Cancel();
		yield return new WaitForSeconds(0.2f);
		reasoning4009Step01.iscanclick = true;
	}
}
