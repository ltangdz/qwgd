using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class ItemNormal : MonoBehaviour
{
	public TypewriterEffect txt_content;

	public Text txt_title;

	public Image img_icon;

	public bool issetsize;

	public int form;

	public Sprite[] bksprites;

	public Sprite[] iconsprites;

	public string itemid;

	private void Start()
	{
	}

	public void SetContent(DATA1 data1)
	{
		itemid = data1.ID.ToString();
		img_icon.gameObject.SetActive(data1.form == 3);
		img_icon.sprite = iconsprites[data1.sign - 1];
		GetComponent<Image>().sprite = bksprites[data1.sign - 1];
		txt_content.StartEffect(I18N.instance.getValue(data1.title) + ":" + I18N.instance.getValue(data1.message));
		issetsize = true;
	}
}
