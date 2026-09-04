using UnityEngine;

public class RoleSixBlank : MonoBehaviour
{
	[SerializeField]
	private GameObject img_blank1;

	[SerializeField]
	private GameObject img_blank2;

	[SerializeField]
	private GameObject img_blank3;

	[SerializeField]
	private GameObject img_blank4;

	[SerializeField]
	private GameObject img_blank5;

	[SerializeField]
	private GameObject img_blank6;

	public GameObject blank_answer1;

	public GameObject blank_answer2;

	public GameObject blank_answer3;

	public GameObject blank_answer4;

	public GameObject blank_answer5;

	public GameObject blank_answer6;

	public int count;

	public int id;

	private void Start()
	{
	}

	public void ResetPos()
	{
		blank_answer1 = null;
		blank_answer2 = null;
		blank_answer3 = null;
		blank_answer4 = null;
		blank_answer5 = null;
		blank_answer6 = null;
		count = 0;
	}

	public bool SetBlank(DragAnswerSix answeritem)
	{
		if (count >= 6)
		{
			return false;
		}
		if (blank_answer1 == null)
		{
			answeritem.transform.localPosition = img_blank1.transform.localPosition;
			answeritem.roleSixBlank = this;
			answeritem.pos = 0;
			blank_answer1 = answeritem.gameObject;
			count++;
		}
		else if (blank_answer2 == null)
		{
			answeritem.transform.localPosition = img_blank2.transform.localPosition;
			answeritem.roleSixBlank = this;
			answeritem.pos = 1;
			blank_answer2 = answeritem.gameObject;
			count++;
		}
		else if (blank_answer3 == null)
		{
			answeritem.transform.localPosition = img_blank3.transform.localPosition;
			answeritem.roleSixBlank = this;
			answeritem.pos = 2;
			blank_answer3 = answeritem.gameObject;
			count++;
		}
		else if (blank_answer4 == null)
		{
			answeritem.transform.localPosition = img_blank4.transform.localPosition;
			answeritem.roleSixBlank = this;
			answeritem.pos = 3;
			blank_answer4 = answeritem.gameObject;
			count++;
		}
		else if (blank_answer5 == null)
		{
			answeritem.transform.localPosition = img_blank5.transform.localPosition;
			answeritem.roleSixBlank = this;
			answeritem.pos = 4;
			blank_answer5 = answeritem.gameObject;
			count++;
		}
		else if (blank_answer6 == null)
		{
			answeritem.transform.localPosition = img_blank6.transform.localPosition;
			answeritem.roleSixBlank = this;
			answeritem.pos = 5;
			blank_answer6 = answeritem.gameObject;
			count++;
		}
		return true;
	}
}
