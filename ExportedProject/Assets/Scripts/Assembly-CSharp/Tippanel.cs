using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Tippanel : MonoBehaviour
{
	public Animator animator;

	public Text txt_tip;

	public Image img_redframe;

	private void Start()
	{
	}

	public void SetTip(string tip)
	{
		txt_tip.GetComponent<I18NText>().updateTranslation2(tip);
		StartCoroutine(StartShow());
	}

	private IEnumerator StartShow()
	{
		animator.Play("ani_redtipshow");
		yield return new WaitForSeconds(3f);
		animator.Play("ani_redtiphide");
	}

	public void SetLongTip(string tip, bool isdanger)
	{
		txt_tip.GetComponent<I18NText>().updateTranslation2(tip);
		animator.Play("ani_redtipshow");
		if (isdanger)
		{
			SetDanger();
		}
	}

	public void HideTip()
	{
		animator.Play("ani_redtiphide");
		img_redframe.gameObject.SetActive(value: false);
	}

	public void SetDanger()
	{
		StartCoroutine(StartDanger());
	}

	private IEnumerator StartDanger()
	{
		yield return new WaitForSeconds(1f);
		img_redframe.gameObject.SetActive(value: true);
	}
}
