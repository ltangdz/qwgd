using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Reasoning4007Step02 : MonoBehaviour
{
	[SerializeField]
	private GameObject step02;

	[SerializeField]
	private GameObject step03;

	[SerializeField]
	private int correct;

	[SerializeField]
	private TimePanel timepanel;

	[SerializeField]
	private Button btn_continue;

	[SerializeField]
	private Text txt_summry;

	[SerializeField]
	private Text txt_maintitle;

	[SerializeField]
	private List<ArrowItem> arrowItems = new List<ArrowItem>();

	[SerializeField]
	private List<GameObject> cubeItems = new List<GameObject>();

	[SerializeField]
	private Image img_dunpaiwhite;

	[SerializeField]
	private Image img_dunpai;

	public int isover;

	private int arrowid;

	private void Start()
	{
		txt_maintitle.DOFade(1f, 0.2f);
		btn_continue.onClick.AddListener(delegate
		{
			btn_continue.interactable = false;
			btn_continue.gameObject.SetActive(value: false);
			for (int i = 0; i < cubeItems.Count; i++)
			{
				cubeItems[i].SetActive(value: false);
			}
			txt_summry.DOText(I18N.instance.getValue("^tuili0462"), 1.5f);
			isover = 1;
		});
	}

	private void Update()
	{
		if (isover == 1 && Input.anyKey)
		{
			isover = 2;
			txt_summry.gameObject.SetActive(value: false);
			step03.gameObject.SetActive(value: true);
			step03.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
			step02.gameObject.SetActive(value: false);
		}
	}

	public bool Check1(ArrowItem arrowItem)
	{
		if (arrowid == arrowItem.id)
		{
			arrowid++;
			if (arrowid == 5)
			{
				btn_continue.GetComponent<Image>().DOFade(1f, 0.2f);
			}
			return true;
		}
		for (int i = 0; i < arrowItems.Count; i++)
		{
			arrowItems[i].Resetpos();
		}
		arrowid = 0;
		img_dunpaiwhite.gameObject.SetActive(value: true);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(img_dunpaiwhite.DOFade(1f, 0.2f));
		sequence.Join(img_dunpaiwhite.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f));
		sequence.Append(img_dunpaiwhite.DOFade(0f, 0.2f));
		sequence.Join(img_dunpaiwhite.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.2f)).OnComplete(delegate
		{
			img_dunpai.transform.DOShakePosition(1f, new Vector3(5f, 5f, 0f));
		});
		sequence.Play();
		return false;
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}
}
