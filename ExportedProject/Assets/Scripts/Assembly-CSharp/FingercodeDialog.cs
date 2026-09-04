using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FingercodeDialog : CustomDialog
{
	[SerializeField]
	private Image img_lock2;

	public InvadePhoneDialog invadePhoneDialog;

	public LineRendererInfo line;

	public void Init(string password)
	{
		line.Init(password);
	}

	public void OpenLock()
	{
		Debug.Log("破解成功");
		img_lock2.transform.DORotate(new Vector3(0f, 0f, 15f), 0.3f);
		invadePhoneDialog.ShowUnlock();
		Close();
	}

	public override void AfterShowSize()
	{
	}

	public override void BeforeShowSize()
	{
	}
}
