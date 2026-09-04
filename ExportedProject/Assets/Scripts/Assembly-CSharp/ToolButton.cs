using UnityEngine;
using UnityEngine.UI;

public class ToolButton : MonoBehaviour
{
	public Image img_bk;

	public Image img_icon;

	public Color[] colors;

	public Text txt_name;

	public Sprite[] sprites;

	public ButtonBox buttonbox;

	private bool isselected;

	private void Start()
	{
		buttonbox = base.transform.parent.parent.GetComponent<ButtonBox>();
	}

	public void SelectTool(int tool)
	{
		if (!isselected)
		{
			img_bk.sprite = sprites[(!isselected) ? 1u : 0u];
			img_icon.sprite = sprites[isselected ? 2 : 3];
			txt_name.color = colors[(!isselected) ? 1u : 0u];
			isselected = !isselected;
			buttonbox.OpenTool(tool);
		}
	}

	public void CloseTool()
	{
		isselected = false;
		img_bk.sprite = sprites[0];
		img_icon.sprite = sprites[2];
		txt_name.color = colors[0];
	}
}
