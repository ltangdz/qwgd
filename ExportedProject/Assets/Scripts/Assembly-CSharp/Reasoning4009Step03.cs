using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4009Step03 : MonoBehaviour
{
	[SerializeField]
	private GameObject step03;

	[SerializeField]
	private GameObject step04;

	[SerializeField]
	private GameObject txt_tip;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Text txt_summry;

	[SerializeField]
	private TimePanel timePanel1;

	[SerializeField]
	private TimePanel timePanel2;

	public bool iscanclick;

	private bool iscankeyboard;

	private void Start()
	{
		iscanclick = true;
		btn_continue.onClick.AddListener(delegate
		{
			Check();
		});
	}

	private void Check()
	{
		bool flag = true;
		if (timePanel1.current != 11 || timePanel2.current != 13)
		{
			flag = false;
		}
		if (flag)
		{
			txt_tip.SetActive(value: false);
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			txt_summry.gameObject.SetActive(value: true);
			txt_summry.DOText(I18N.instance.getValue("^tuili0925"), 3f).OnComplete(delegate
			{
				iscankeyboard = true;
			});
		}
		else
		{
			timePanel1.SetRed();
		}
	}

	private void Update()
	{
		if (iscankeyboard && Input.anyKey)
		{
			txt_summry.fontSize = 16;
			txt_summry.fontStyle = FontStyle.Normal;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(step03.GetComponent<CanvasGroup>().DOFade(0f, 0.3f));
			sequence.Append(txt_summry.transform.DOLocalMoveY(149f, 1f));
			sequence.OnComplete(delegate
			{
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
