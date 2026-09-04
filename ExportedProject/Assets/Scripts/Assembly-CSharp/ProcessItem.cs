using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ProcessItem : MonoBehaviour
{
	public Image img_arrow;

	public Transform selectpanel;

	public InputField inputField;

	public Image img_circle;

	public Image img_line;

	public Transform contentpanel;

	public Animator animator;

	public Text txt_content;

	public Text txt_question;

	public int answerpos = -1;

	public int correctanswer;

	public Color[] colors;

	public Sprite[] sprites;

	public Image img_icon;

	public Image img_gray;

	public Image img_line2;

	public Image img_inputbk;

	public Image img_inputline;

	public GameObject resultpanel;

	public string resultkey;

	public Image img_resultline1;

	public Image img_resultline2;

	public Text txt_result;

	public Animator iconanimator;

	public string str_iconani;

	private void Start()
	{
	}

	public void ShowIconAni()
	{
		iconanimator.Play(str_iconani);
	}

	public void ShowRepeatIconAni()
	{
		iconanimator.Play(str_iconani + "loop", 0, 0f);
	}

	public void StopIconAni()
	{
		iconanimator.Play("Empty");
	}

	public void ShowResult()
	{
		inputField.interactable = false;
		resultpanel.SetActive(value: true);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(img_resultline1.DOFillAmount(1f, 0.5f));
		sequence.Append(img_resultline2.DOFillAmount(1f, 0.2f));
		sequence.Append(txt_result.DOText(I18N.instance.getValue(resultkey), 1f));
		sequence.Play();
	}

	private void SetSelect(bool isselected)
	{
		img_circle.sprite = sprites[isselected ? 1 : 0];
		img_icon.sprite = sprites[(!isselected) ? 2 : 5];
		img_gray.sprite = sprites[(!isselected) ? 3 : 6];
		img_line2.sprite = sprites[(!isselected) ? 4 : 7];
		img_inputbk.sprite = sprites[(!isselected) ? 8 : 10];
		img_inputline.sprite = sprites[(!isselected) ? 9 : 11];
		txt_question.color = colors[isselected ? 2 : 0];
	}

	public void ClickSelectItem(string key)
	{
		string[] array = key.Split(';');
		txt_content.text = "";
		txt_content.color = new Color(0.38f, 0.42f, 0.52f, 1f);
		txt_content.DOText(I18N.instance.getValue(array[0]), 1f);
		answerpos = int.Parse(array[1]);
	}

	public bool IsRight()
	{
		return answerpos == correctanswer;
	}

	public void SetWrong()
	{
		txt_question.color = colors[1];
	}

	public void SetRight()
	{
		txt_question.color = colors[0];
	}

	public void Init()
	{
		Debug.Log("name:" + base.gameObject.name);
		animator.Play("ani_processitem");
	}

	private void Update()
	{
		if (inputField.isFocused && img_arrow.transform.localScale.x == 0f)
		{
			SetSelect(isselected: true);
			ShowRepeatIconAni();
			img_arrow.transform.DOScaleX(1f, 0.3f).OnComplete(delegate
			{
				selectpanel.DOScale(Vector3.one, 0.3f);
			});
		}
		if (!inputField.isFocused && img_arrow.transform.localScale.x == 1f && !Input.GetMouseButton(0))
		{
			SetSelect(isselected: false);
			StopIconAni();
			Invoke("DelayScale", 0.3f);
		}
	}

	private void DelayScale()
	{
		selectpanel.DOScale(Vector3.zero, 0.3f);
		img_arrow.transform.DOScaleX(0f, 0.3f);
	}
}
