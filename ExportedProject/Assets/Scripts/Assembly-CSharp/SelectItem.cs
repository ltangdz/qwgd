using UnityEngine;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour
{
	public Image img_frame;

	public Image img_status;

	public Sprite[] sprites;

	public Text txt_content;

	public int answerid;

	public int correntid;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetRight()
	{
		img_frame.sprite = sprites[4];
		img_status.sprite = sprites[5];
	}

	public void SetWrong()
	{
		img_frame.sprite = sprites[2];
		img_status.sprite = sprites[3];
	}

	public bool IsRight()
	{
		return answerid == correntid;
	}

	public void Refresh(int id, string content)
	{
		answerid = id;
		txt_content.text = content;
	}
}
