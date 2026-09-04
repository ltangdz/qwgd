using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class FishDialog1 : CustomDialog
{
	public Button btn_go;

	public FishDialog2 fishDialog2;

	public TypewriterEffect txt_title;

	public TypewriterEffect[] txt_dragtexts;

	private void Start()
	{
		btn_go.onClick.AddListener(delegate
		{
			fishDialog2.Show();
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
		txt_title.StartEffect(I18N.instance.getValue("^txt_fishdialog1"));
		yield return new WaitForSeconds(0.1f);
		GetComponent<Animator>().Play("ani_fishdialog1");
	}

	public void SetFishDragText(int pos)
	{
		txt_dragtexts[pos].StartEffect(I18N.instance.getValue("^txt_fishdialog3"));
	}
}
