using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ClueMethod : MonoBehaviour
{
	public Sprite[] sprites;

	public Image img_icon;

	public Text txt_name;

	private string[] names = new string[8] { "^tab_search", "^tab_social", "^tab_camouflage", "^tab_crack", "^tab_taohua", "^tab_picAls", "^tab_jiankong", "^tab_fishing" };

	private void Start()
	{
	}

	public void Init(string name)
	{
		txt_name.GetComponent<I18NText>().updateTranslation2(name);
		if (name.Equals(names[0]))
		{
			img_icon.sprite = sprites[0];
		}
		else if (name.Equals(names[1]))
		{
			img_icon.sprite = sprites[1];
		}
		else if (name.Equals(names[2]))
		{
			img_icon.sprite = sprites[2];
		}
		else if (name.Equals(names[3]))
		{
			img_icon.sprite = sprites[3];
		}
		else if (name.Equals(names[4]))
		{
			img_icon.sprite = sprites[4];
		}
		else if (name.Equals(names[5]))
		{
			img_icon.sprite = sprites[5];
		}
		else if (name.Equals(names[6]))
		{
			img_icon.sprite = sprites[6];
		}
		else if (name.Equals(names[7]))
		{
			img_icon.sprite = sprites[7];
		}
		img_icon.SetNativeSize();
	}
}
