using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class MissionBaseItem : MonoBehaviour
{
	public TypewriterEffect txt_title;

	public TypewriterEffect txt_content;

	public Image img_avatar;

	public bool isImage;

	public DATA20 data20;

	private void Start()
	{
	}

	public void InitContent(DATA20 data20)
	{
		this.data20 = data20;
		if (data20.pos == 9 || data20.pos == 0)
		{
			txt_title.StartEffect(I18N.instance.getValue(data20.title) + " :");
			txt_content.StartEffect(I18N.instance.getValue((data20.pos == 0) ? data20.content : "^mission_content10"));
		}
		else
		{
			_ = data20.pos;
			_ = 8;
		}
	}

	public void CompleteBaseMission()
	{
		if (data20.pos == 9)
		{
			txt_content.StartEffect(I18N.instance.getValue(data20.content));
		}
		else if (data20.pos == 8)
		{
			img_avatar.sprite = Resources.Load<Sprite>(data20.content);
			if (base.transform.Find("img_line") != null)
			{
				base.transform.Find("img_line").gameObject.SetActive(value: false);
			}
			if (base.transform.Find("img_frame") != null)
			{
				base.transform.Find("img_frame").gameObject.SetActive(value: false);
			}
			if (base.transform.Find("img_avatarline") != null)
			{
				base.transform.Find("img_avatarline").gameObject.SetActive(value: false);
			}
			if (base.transform.Find("img_scanline") != null)
			{
				base.transform.Find("img_scanline").gameObject.SetActive(value: false);
			}
		}
	}
}
