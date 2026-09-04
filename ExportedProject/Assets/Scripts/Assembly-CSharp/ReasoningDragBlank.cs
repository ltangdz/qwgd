using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ReasoningDragBlank : MonoBehaviour
{
	public Text txt_name;

	public Image img_role;

	public Sprite blanksprite;

	public Image img_red;

	public Image img_redframe;

	public int answer;

	public ReasoningDragRole reasoningDragRole;

	public ReasoningMiddle4003 reasoningMiddle4003;

	public bool isfirst = true;

	public bool isdragline = true;

	public bool isred;

	private void Start()
	{
	}

	public void ResetBlank()
	{
		reasoningDragRole = null;
		answer = 0;
		txt_name.text = "???";
		img_role.sprite = blanksprite;
		if (isdragline)
		{
			GetComponent<DragLine>().avatarname = "";
		}
		isred = false;
	}

	public void SetRole(ReasoningDragRole reasoningDragRole)
	{
		if (this.reasoningDragRole != null)
		{
			this.reasoningDragRole.ResetRole();
		}
		answer = reasoningDragRole.id;
		txt_name.text = I18N.instance.getValue(reasoningDragRole.key);
		img_role.sprite = reasoningDragRole.img_role.sprite;
		this.reasoningDragRole = reasoningDragRole;
		if (isdragline)
		{
			GetComponent<DragLine>().avatarname = "b" + answer;
		}
		if (isfirst)
		{
			reasoningMiddle4003.CheckStep01();
		}
		else
		{
			reasoningMiddle4003.CheckStep02();
		}
	}

	public void SetWrong()
	{
		isred = true;
		img_red.gameObject.SetActive(value: true);
		img_redframe.gameObject.SetActive(value: true);
		img_redframe.DOFade(0.2f, 0.3f).SetLoops(3).OnComplete(delegate
		{
			img_red.gameObject.SetActive(value: false);
			img_redframe.gameObject.SetActive(value: false);
		});
	}
}
