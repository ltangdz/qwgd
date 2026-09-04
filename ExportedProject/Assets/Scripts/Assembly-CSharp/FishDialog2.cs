using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class FishDialog2 : CustomDialog
{
	public Button btn_send;

	public Button btn_reset;

	public Text txt_name;

	public Text txt_linkname;

	public FishDialog3 fishDialog3;

	public TypewriterEffect txt_title;

	private void Start()
	{
		btn_send.onClick.AddListener(delegate
		{
			fishDialog3.Show();
			fishDialog3.SetSuccess(issuccess: true);
		});
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
		StartCoroutine(StartAnimation());
	}

	public IEnumerator StartAnimation()
	{
		yield return new WaitForSeconds(0.1f);
		txt_title.StartEffect(I18N.instance.getValue("^txt_fishdialog4"));
		yield return new WaitForSeconds(0.1f);
		GetComponent<Animator>().Play("ani_fishdialog2");
	}
}
