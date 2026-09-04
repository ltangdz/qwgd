using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class SurSearchingDialog : MonoBehaviour
{
	public Animator img_light;

	public SliderUI sliderUI;

	public Text txt_status;

	public Text txt_compelete;

	private bool iscorrect;

	public SurveillanceDialog surveillanceDialog;

	private int pos;

	public Image img_black;

	private bool iscanclose;

	private void OnEnable()
	{
		img_black.fillAmount = 0f;
		img_black.DOFillAmount(1f, 0.5f);
	}

	private void OnDestroy()
	{
		Debug.Log("OnDestroy");
		img_black.DOFillAmount(0f, 0.5f);
	}

	private void Start()
	{
		OnEnable();
	}

	public void StartSearch(int pos, bool iscorrect, SurveillanceDialog surveillanceDialog)
	{
		this.pos = pos;
		this.surveillanceDialog = surveillanceDialog;
		this.iscorrect = iscorrect;
		sliderUI.FreshAcc(99f);
		img_light.Play("ani_rotate2");
	}

	public void SearchOver()
	{
		img_light.Play("Empty");
		if (iscorrect)
		{
			txt_status.GetComponent<I18NText>().updateTranslation2("^surveillance10");
		}
		else
		{
			txt_status.color = Color.red;
			txt_status.GetComponent<I18NText>().updateTranslation2("^surveillance09");
		}
		iscanclose = true;
	}

	private void Update()
	{
		if (Input.anyKey && iscanclose)
		{
			if (iscorrect)
			{
				surveillanceDialog.ChangeDot(pos);
			}
			else
			{
				surveillanceDialog.RemoveDot(pos, ishide: true);
			}
			Object.Destroy(base.gameObject);
		}
	}
}
