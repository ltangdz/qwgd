using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class FishDialog3 : CustomDialog
{
	public Image fill;

	public Image img_light;

	public Image img_dots;

	public Image img_line1;

	public Image img_line2;

	public bool isstart;

	public Text txt_content;

	public Text txt_percent;

	public Color[] colors;

	public Sprite[] lightsps;

	public Sprite[] progresssps;

	public bool issuccess;

	public Button btn_checkfile;

	public Button btn_reset;

	private void Start()
	{
	}

	public void SetSuccess(bool issuccess)
	{
		this.issuccess = issuccess;
		isstart = true;
	}

	private void Update()
	{
		if (isstart)
		{
			fill.fillAmount += 0.001f;
			txt_percent.GetComponent<I18NText>().updateTranslation2((int)(fill.fillAmount * 100f) + "%");
		}
		if ((fill.fillAmount >= 1f) & isstart)
		{
			isstart = false;
			fill.fillAmount = 1f;
			txt_percent.GetComponent<I18NText>().updateTranslation2("100%");
			img_light.gameObject.SetActive(value: true);
			fill.transform.GetChild(0).GetComponent<Animator>().enabled = false;
			if (issuccess)
			{
				img_light.GetComponent<Animator>().enabled = false;
				fill.sprite = progresssps[0];
				img_light.sprite = lightsps[0];
				txt_content.color = colors[0];
				txt_content.GetComponent<I18NText>().updateTranslation2("^txt_fishdialog9");
				img_dots.sprite = progresssps[3];
				img_line1.sprite = progresssps[5];
				img_line2.sprite = progresssps[5];
				txt_content.GetComponent<Animator>().enabled = false;
				btn_checkfile.gameObject.SetActive(value: true);
				btn_reset.gameObject.SetActive(value: false);
			}
			else
			{
				img_light.GetComponent<Animator>().enabled = true;
				fill.sprite = progresssps[1];
				img_light.sprite = lightsps[1];
				txt_content.color = colors[1];
				img_line1.sprite = progresssps[2];
				img_line2.sprite = progresssps[2];
				img_dots.sprite = progresssps[4];
				txt_content.GetComponent<I18NText>().updateTranslation2("^txt_fishdialog10");
				txt_content.GetComponent<Animator>().enabled = true;
				btn_checkfile.gameObject.SetActive(value: false);
				btn_reset.gameObject.SetActive(value: true);
			}
		}
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
