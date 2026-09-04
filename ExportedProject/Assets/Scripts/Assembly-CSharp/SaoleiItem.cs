using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SaoleiItem : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image img_bk;

	public Image img_icon;

	public Text txt_level;

	public int level;

	public Sprite[] sprites;

	public bool isprotect;

	public bool isopen;

	public SaoleiDialog saoleiDialog;

	private void Start()
	{
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			ButtonLeftClick();
		}
		else if (eventData.button == PointerEventData.InputButton.Right)
		{
			ButtonRightClick();
		}
	}

	private void ButtonLeftClick()
	{
		if (!isprotect && !isopen && saoleiDialog.iscanclick)
		{
			isopen = true;
			if (level == -1)
			{
				img_bk.sprite = sprites[2];
				img_icon.sprite = sprites[3];
				img_icon.gameObject.SetActive(value: true);
				saoleiDialog.ShowFail();
			}
			else
			{
				img_bk.sprite = sprites[6];
				txt_level.text = level.ToString();
				txt_level.gameObject.SetActive(value: true);
			}
			Debug.Log("Button Left Click");
		}
	}

	private void ButtonRightClick()
	{
		if (isopen || !saoleiDialog.iscanclick)
		{
			return;
		}
		if (!isprotect)
		{
			if (saoleiDialog.leftcount == 0)
			{
				return;
			}
			img_bk.sprite = sprites[4];
			img_icon.sprite = sprites[5];
			img_icon.gameObject.SetActive(value: true);
			isprotect = true;
			saoleiDialog.MinusCount(level == -1);
		}
		else
		{
			img_icon.gameObject.SetActive(value: false);
			img_bk.sprite = sprites[0];
			isprotect = false;
			saoleiDialog.AddCount(level == -1);
		}
		Debug.Log("Button Right Click");
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!isprotect && !isopen)
		{
			img_bk.sprite = sprites[1];
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!isprotect && !isopen)
		{
			img_bk.sprite = sprites[0];
		}
	}
}
