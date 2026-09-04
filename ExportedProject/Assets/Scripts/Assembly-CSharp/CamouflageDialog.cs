using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class CamouflageDialog : CustomDialog
{
	public bool isstranger;

	public Dropdown[] dropdowns;

	public Button btn_sure;

	public Button btn_reset;

	public Text[] txt_dropdowns;

	public TypewriterEffect txt_name;

	public Image mask;

	public PicTurnOver9 picTurnOver9;

	public TypewriterEffect txt_title;

	public void ChangeIsStranger(int isstranger)
	{
		this.isstranger = isstranger == 1;
		for (int i = 0; i < dropdowns.Length; i++)
		{
			dropdowns[i].interactable = this.isstranger;
		}
	}

	private void ShowAvatar()
	{
		picTurnOver9.StartShowPic();
	}

	public void ChangeDropdown(int pos)
	{
		txt_dropdowns[pos].GetComponent<I18NText>().updateTranslation2(dropdowns[pos].captionText.text);
	}

	private void Start()
	{
		btn_sure.onClick.AddListener(delegate
		{
			txt_name.StartEffect("Franklin");
			mask.GetComponent<Mask>().enabled = true;
			InvokeRepeating("ShowAvatar", 0.1f, 0.02f);
		});
	}

	private void Init()
	{
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
		txt_title.StartEffect(I18N.instance.getValue("^txt_camouflagedialog01"));
		yield return new WaitForSeconds(0.6f);
		GetComponent<Animator>().Play("ani_camouflagedialog1");
	}
}
