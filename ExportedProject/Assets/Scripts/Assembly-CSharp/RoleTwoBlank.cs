using UnityEngine;
using UnityEngine.UI;

public class RoleTwoBlank : MonoBehaviour
{
	[SerializeField]
	private GameObject img_blank1;

	[SerializeField]
	private GameObject img_blank2;

	[SerializeField]
	private Text txt_blank1;

	[SerializeField]
	private Text txt_blank2;

	public GameObject blank_answer1;

	public GameObject blank_answer2;

	public int count;

	public int id;

	public void ResetPos()
	{
		blank_answer1 = null;
		blank_answer2 = null;
		txt_blank1.text = "";
		txt_blank2.text = "";
		count = 0;
	}

	public bool SetBlank(DragLetterItem answeritem)
	{
		if (count >= 2)
		{
			return false;
		}
		if (blank_answer1 == null)
		{
			answeritem.SetGray();
			answeritem.roleTwoBlank = this;
			answeritem.pos = 0;
			blank_answer1 = answeritem.gameObject;
			txt_blank1.text = answeritem.str_simple;
			count++;
		}
		else if (blank_answer2 == null)
		{
			answeritem.SetGray();
			answeritem.roleTwoBlank = this;
			answeritem.pos = 1;
			blank_answer2 = answeritem.gameObject;
			txt_blank2.text = answeritem.str_simple;
			count++;
		}
		return true;
	}
}
