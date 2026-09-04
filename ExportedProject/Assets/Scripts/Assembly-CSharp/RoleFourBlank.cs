using UnityEngine;

public class RoleFourBlank : MonoBehaviour
{
	[SerializeField]
	private GameObject img_blank1;

	[SerializeField]
	private GameObject img_blank2;

	[SerializeField]
	private GameObject img_blank3;

	[SerializeField]
	private GameObject img_blank4;

	public GameObject blank_answer1;

	public GameObject blank_answer2;

	public GameObject blank_answer3;

	public GameObject blank_answer4;

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
		count = 0;
	}

	public bool SetBlank(DragAnswer answeritem)
	{
		if (count >= 4)
		{
			return false;
		}
		if (blank_answer1 == null)
		{
			answeritem.transform.localPosition = img_blank1.transform.localPosition;
			answeritem.roleFourBlank = this;
			answeritem.pos = 0;
			blank_answer1 = answeritem.gameObject;
			count++;
		}
		else if (blank_answer2 == null)
		{
			answeritem.transform.localPosition = img_blank2.transform.localPosition;
			answeritem.roleFourBlank = this;
			answeritem.pos = 1;
			blank_answer2 = answeritem.gameObject;
			count++;
		}
		else if (blank_answer3 == null)
		{
			answeritem.transform.localPosition = img_blank3.transform.localPosition;
			answeritem.roleFourBlank = this;
			answeritem.pos = 2;
			blank_answer3 = answeritem.gameObject;
			count++;
		}
		else if (blank_answer4 == null)
		{
			answeritem.transform.localPosition = img_blank4.transform.localPosition;
			answeritem.roleFourBlank = this;
			answeritem.pos = 3;
			blank_answer4 = answeritem.gameObject;
			count++;
		}
		return true;
	}
}
