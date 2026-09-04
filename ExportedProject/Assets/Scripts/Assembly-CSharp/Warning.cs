using UnityEngine;

public class Warning : MonoBehaviour
{
	public PasswordDialog1 passwordDialog1;

	public SqlDialog sqlDialog;

	private Animator ani;

	private bool clickAble;

	private void Start()
	{
		ani = base.transform.GetComponent<Animator>();
	}

	public void HideWarning()
	{
		ani.Play("ani_warningHide");
	}

	public void HideWarn()
	{
		if (passwordDialog1 != null)
		{
			passwordDialog1.BeginGame();
		}
		if (sqlDialog != null)
		{
			sqlDialog.BeginGame();
		}
		base.gameObject.SetActive(value: false);
	}

	public void CanClick()
	{
		clickAble = true;
	}
}
