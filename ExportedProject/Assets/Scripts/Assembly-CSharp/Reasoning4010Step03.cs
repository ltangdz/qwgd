using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4010Step03 : MonoBehaviour
{
	[SerializeField]
	private GameObject step03;

	[SerializeField]
	private GameObject step04;

	[SerializeField]
	private GameObject txt_summry1;

	[SerializeField]
	private GameObject txt_summry2;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Text txt_summry;

	public bool iscanclick;

	[SerializeField]
	private List<Toggle> toggles = new List<Toggle>();

	[SerializeField]
	private Sprite graysprite;

	[SerializeField]
	private Sprite redsprite;

	private bool iscankeyboard;

	private void Start()
	{
		iscanclick = true;
		btn_continue.interactable = true;
		btn_continue.gameObject.SetActive(value: true);
		btn_continue.onClick.AddListener(delegate
		{
			Check();
		});
	}

	private void Check()
	{
		bool flag = true;
		if (toggles[0].isOn ? true : false)
		{
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			txt_summry.gameObject.SetActive(value: true);
			for (int i = 0; i < toggles.Count; i++)
			{
				if (i != 0)
				{
					toggles[i].GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
					continue;
				}
				toggles[i].GetComponent<ButtonScale>().enabled = false;
				toggles[i].GetComponent<RectTransform>().DOLocalMove(new Vector3(0f, -60f, 0f), 0.3f);
				toggles[i].GetComponent<RectTransform>().DOScale(new Vector3(1.5f, 1.5f, 1f), 0.3f);
			}
			txt_summry.DOText(I18N.instance.getValue("^tuili1016"), 3f).OnComplete(delegate
			{
				iscankeyboard = true;
			});
		}
		else
		{
			iscanclick = true;
			for (int num = 0; num < toggles.Count; num++)
			{
				SetRed(toggles[num]);
			}
		}
	}

	public void SetRed(Toggle tog)
	{
		tog.GetComponent<Image>().sprite = redsprite;
		Sequence sequence = DOTween.Sequence();
		sequence.Append(tog.GetComponent<Image>().DOFade(0.2f, 0.2f));
		sequence.Append(tog.GetComponent<Image>().DOFade(1f, 0.2f));
		sequence.Play().SetLoops(3).OnComplete(delegate
		{
			tog.GetComponent<Image>().sprite = graysprite;
		});
	}

	private void Update()
	{
		if (iscankeyboard && Input.anyKey)
		{
			txt_summry.fontSize = 16;
			txt_summry.fontStyle = FontStyle.Normal;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(step03.GetComponent<CanvasGroup>().DOFade(0f, 0.3f));
			sequence.OnComplete(delegate
			{
				txt_summry1.SetActive(value: false);
				txt_summry2.SetActive(value: false);
				txt_summry.gameObject.SetActive(value: false);
				step04.SetActive(value: true);
				base.gameObject.SetActive(value: false);
			});
		}
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
