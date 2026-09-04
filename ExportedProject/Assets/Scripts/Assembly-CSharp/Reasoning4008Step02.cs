using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4008Step02 : MonoBehaviour
{
	[SerializeField]
	private GameObject step02;

	[SerializeField]
	private GameObject step03;

	[SerializeField]
	private GameObject txt_tip;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Button btn_continue2;

	[SerializeField]
	private Text txt_summry;

	[SerializeField]
	private List<DragAnswer> dragAnswers = new List<DragAnswer>();

	[SerializeField]
	private RoleFourBlank img_left;

	[SerializeField]
	private RoleFourBlank img_right;

	public bool iscandrag = true;

	public Transform img_title2;

	public InputField inputfield;

	public Sprite[] inputsprites;

	public Color[] colors;

	private bool iscankeyboard;

	private void Start()
	{
		btn_continue.gameObject.SetActive(value: true);
		Sequence sequence = DOTween.Sequence();
		sequence.PrependInterval(3f);
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 2f));
		sequence.Append(btn_continue.transform.DOScale(new Vector3(1f, 1f, 1f), 2f));
		sequence.Play().SetLoops(-1);
		btn_continue.onClick.AddListener(Check);
		btn_continue2.onClick.AddListener(Check2);
	}

	private void Check()
	{
		bool flag = true;
		for (int i = 0; i < dragAnswers.Count; i++)
		{
			if (!dragAnswers[i].IsRight())
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			txt_tip.SetActive(value: false);
			iscandrag = false;
			txt_summry.DOText(I18N.instance.getValue("^tuili0485"), 3f).OnComplete(delegate
			{
				img_title2.DOScaleY(1f, 0.2f);
				inputfield.transform.DOScaleY(1f, 0.2f);
				btn_continue2.gameObject.SetActive(value: true);
				btn_continue2.image.DOFade(1f, 0.2f);
				iscankeyboard = true;
			});
		}
		else
		{
			for (int num = 0; num < dragAnswers.Count; num++)
			{
				dragAnswers[num].ResetPos();
			}
			img_left.ResetPos();
			img_right.ResetPos();
		}
	}

	private void Update()
	{
		if (iscankeyboard && Input.GetKeyDown(KeyCode.Return))
		{
			Check2();
		}
	}

	private void Check2()
	{
		if (inputfield.text.ToLower().Equals("van"))
		{
			iscankeyboard = false;
			txt_summry.gameObject.SetActive(value: false);
			step03.SetActive(value: true);
			step03.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
			step02.GetComponent<CanvasGroup>().DOFade(0f, 0.3f).OnComplete(delegate
			{
				step02.SetActive(value: false);
			});
		}
		else
		{
			StartCoroutine(ShowInputFieldRed());
		}
	}

	private IEnumerator ShowInputFieldRed()
	{
		iscankeyboard = false;
		inputfield.textComponent.color = colors[0];
		inputfield.interactable = false;
		inputfield.image.sprite = inputsprites[0];
		yield return new WaitForSeconds(0.4f);
		inputfield.image.sprite = inputsprites[1];
		yield return new WaitForSeconds(0.4f);
		inputfield.image.sprite = inputsprites[0];
		yield return new WaitForSeconds(0.4f);
		inputfield.image.sprite = inputsprites[1];
		inputfield.textComponent.color = colors[1];
		inputfield.text = "";
		inputfield.interactable = true;
		iscankeyboard = true;
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
