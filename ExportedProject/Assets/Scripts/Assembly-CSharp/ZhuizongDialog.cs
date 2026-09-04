using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class ZhuizongDialog : CustomDialog
{
	[SerializeField]
	private GameObject step1;

	[SerializeField]
	private GameObject step2;

	private List<DATA36> list36;

	public DATA36 currentdata36;

	[Header("step1 界面")]
	[SerializeField]
	private Button btn_sure;

	[SerializeField]
	private List<GameObject> tabs = new List<GameObject>();

	[SerializeField]
	private List<GameObject> zhuizongitems = new List<GameObject>();

	[SerializeField]
	private Sprite[] sprites;

	[SerializeField]
	private Color[] colors;

	[SerializeField]
	private Image img_line0;

	[SerializeField]
	private Image img_line1;

	[SerializeField]
	private Image img_longline;

	[SerializeField]
	private Image contentpanel;

	[SerializeField]
	private GameObject img_target;

	[SerializeField]
	private GameObject img_green;

	[SerializeField]
	private Text txt_title;

	[SerializeField]
	private Text txt_title0;

	private DATA36 data36;

	[Header("step2 界面")]
	[SerializeField]
	private List<Image> littlepics = new List<Image>();

	[SerializeField]
	private Text txt_add;

	[SerializeField]
	private Text txt_city;

	[SerializeField]
	private Text txt_post;

	[SerializeField]
	private Text txt_country;

	[SerializeField]
	private Text txt_time;

	[SerializeField]
	private Image img_jindu;

	[SerializeField]
	private Image img_jindudot;

	[SerializeField]
	private Image img_linestep2_1;

	[SerializeField]
	private Image img_line1_1;

	[SerializeField]
	private Image img_dot;

	[SerializeField]
	private Image img_linestep2;

	[SerializeField]
	private Image img_line_1;

	[SerializeField]
	private Image img_dot2_1;

	[SerializeField]
	private Image img_dot2_2;

	[SerializeField]
	public GameObject img_role;

	[SerializeField]
	public GameObject img_pos;

	[SerializeField]
	public GameObject img_map;

	private IEnumerator StartAnimationStep1()
	{
		img_line0.DOFillAmount(1f, 0.5f).SetEase(Ease.InOutCirc);
		img_line1.DOFillAmount(1f, 0.5f).SetEase(Ease.InOutCirc);
		yield return new WaitForSeconds(0.1f);
		img_target.SetActive(value: true);
		txt_title.DOText(I18N.instance.getValue("^zhuizong01"), 0.7f);
		yield return new WaitForSeconds(0.2f);
		txt_title0.DOText(I18N.instance.getValue("^surveillance01"), 0.5f);
		yield return new WaitForSeconds(0.1f);
		img_green.SetActive(value: true);
		img_longline.gameObject.SetActive(value: true);
		img_longline.DOFillAmount(1f, 1f);
		yield return new WaitForSeconds(0.5f);
		contentpanel.DOFillAmount(1f, 1f).SetEase(Ease.InOutCirc);
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < zhuizongitems.Count; i++)
		{
			zhuizongitems[i].SetActive(value: true);
			zhuizongitems[i].transform.DOScale(Vector3.one, 0.5f);
			yield return new WaitForSeconds(0.3f);
		}
		btn_sure.gameObject.SetActive(value: true);
		btn_sure.transform.DOScale(Vector3.one, 0.3f);
		yield return new WaitForSeconds(0.3f);
		for (int i = 0; i < list36.Count; i++)
		{
			tabs[i].gameObject.SetActive(value: true);
			tabs[i].GetComponent<Image>().DOFillAmount(1f, 0.3f).SetEase(Ease.InOutCirc);
			yield return new WaitForSeconds(0.3f);
		}
		if (!gameManager.player.playerdata.videotiplist.Contains("3700048") && gameManager.player.GetEventId().Equals("110002"))
		{
			gameManager.homeScene.ShowVideoTip("3700048");
		}
	}

	private void InitTabs()
	{
		gameManager.CanShowSetting(1);
		btn_close.onClick.AddListener(delegate
		{
			gameManager.CanShowSetting(-1);
		});
		list36 = gameManager.dataManager.GetShowSurveillanceItems(gameManager.player.GetEventId());
		for (int num = 0; num < list36.Count; num++)
		{
			Debug.Log("tab" + I18N.instance.getValue(list36[num].rolename));
			tabs[num].transform.GetChild(0).GetComponent<I18NText>().updateTranslation2(list36[num].rolename);
			if (num == 0)
			{
				InitStep1(list36[num]);
			}
		}
	}

	private void InitStep1(DATA36 data36)
	{
		currentdata36 = data36;
		string[] array = data36.itemids.Substring(1).Split(';');
		int num = 0;
		for (int i = 0; i < array.Length; i++)
		{
			if (gameManager.player.playerdata.itemlist.Contains(array[i]) || gameManager.isbug)
			{
				Debug.Log("itempis:Image/" + gameManager.dataManager.dic1[array[i]].image);
				Sprite sprite = Resources.Load<Sprite>("Image/" + gameManager.dataManager.dic1[array[i]].image + "_zz");
				zhuizongitems[num].transform.GetChild(0).GetChild(0).GetComponent<Image>()
					.sprite = sprite;
				num++;
			}
			else if ((gameManager.player.playerdata.temporaryhopelist.Contains(array[i]) && gameManager.homeScene.iszhibojian) || gameManager.isbug)
			{
				Sprite sprite2 = Resources.Load<Sprite>("Image/" + gameManager.dataManager.dic1[array[i]].image + "_zz");
				zhuizongitems[num].transform.GetChild(0).GetChild(0).GetComponent<Image>()
					.sprite = sprite2;
				num++;
			}
		}
		if (num < array.Length)
		{
			btn_sure.image.sprite = sprites[0];
			btn_sure.transform.GetChild(0).GetComponent<Text>().color = colors[0];
			btn_sure.interactable = false;
			return;
		}
		btn_sure.onClick.AddListener(delegate
		{
			if (gameManager.player.playerdata.surveillanceRecord.ContainsKey(currentdata36.ID.ToString()))
			{
				btn_close.gameObject.SetActive(value: false);
				GetComponent<Image>().DOFade(0.4f, 0.2f);
			}
			step1.SetActive(value: false);
			step2.SetActive(value: true);
			InitStep2(currentdata36);
			if (gameManager.player.GetEventId().Equals("110002"))
			{
				gameManager.istaohuashow = false;
			}
			StartCoroutine(StartAnimationStep2());
		});
	}

	private void UpdateTime()
	{
		float num = Random.Range(0.2f, 1f);
		txt_time.DOText(I18N.instance.getValue("^zhuizong13") + num + "ms", 0.2f);
	}

	public void UpdateAddress()
	{
		txt_add.DOText(I18N.instance.getValue("^zhuizong09") + I18N.instance.getValue(currentdata36.searchcontent.Split(';')[img_map.GetComponent<Zhuizongmap>().keypos]), 0.3f);
	}

	private void InitStep2(DATA36 data36)
	{
		string[] array = data36.itemids.Substring(1).Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			Sprite sprite = Resources.Load<Sprite>("Image/" + gameManager.dataManager.dic1[array[i]].image + "_zz");
			littlepics[i].transform.GetChild(0).GetComponent<Image>().sprite = sprite;
		}
	}

	private IEnumerator StartAnimationStep2()
	{
		for (int i = 0; i < littlepics.Count; i++)
		{
			littlepics[i].transform.DOScale(Vector3.one, 0.3f);
			yield return new WaitForSeconds(0.15f);
		}
		img_linestep2_1.DOFillAmount(1f, 0.2f);
		img_linestep2.DOFillAmount(1f, 0.2f);
		yield return new WaitForSeconds(0.2f);
		img_linestep2_1.transform.DOLocalMoveY(4.1f, 0.2f);
		img_linestep2.transform.DOLocalMoveY(-217.7f, 0.2f);
		yield return new WaitForSeconds(0.2f);
		img_line1_1.gameObject.SetActive(value: true);
		img_line_1.gameObject.SetActive(value: true);
		img_dot.gameObject.SetActive(value: true);
		img_dot2_1.gameObject.SetActive(value: true);
		img_dot2_2.gameObject.SetActive(value: true);
		img_line_1.DOFillAmount(1f, 0.2f);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(img_dot.transform.DOLocalMoveX(79.7f, 4f).SetEase(Ease.InOutCirc));
		sequence.Append(img_dot.transform.DOLocalMoveX(-61.1f, 4f).SetEase(Ease.InOutCirc));
		sequence.SetLoops(-1);
		sequence.Play();
		Sequence sequence2 = DOTween.Sequence();
		sequence2.Append(img_dot2_1.transform.DOLocalMoveX(79f, 2f).SetEase(Ease.InOutCirc));
		sequence2.Append(img_dot2_1.transform.DOLocalMoveX(-79f, 2f).SetEase(Ease.InOutCirc));
		sequence2.SetLoops(-1);
		sequence2.Play();
		Sequence sequence3 = DOTween.Sequence();
		sequence3.Append(img_dot2_2.transform.DOLocalMoveX(79f, 3f).SetEase(Ease.InOutCirc));
		sequence3.Append(img_dot2_2.transform.DOLocalMoveX(-79f, 3f).SetEase(Ease.InOutCirc));
		sequence3.SetLoops(-1);
		sequence3.Play();
		yield return new WaitForSeconds(0.2f);
		img_role.transform.DOScaleY(1f, 0.3f);
		img_pos.transform.DOScaleX(1f, 0.3f);
		txt_city.DOText(I18N.instance.getValue("^zhuizong10") + I18N.instance.getValue(currentdata36.city), 0.3f);
		txt_post.DOText(I18N.instance.getValue("^zhuizong11") + I18N.instance.getValue(currentdata36.postcode), 0.3f);
		txt_country.DOText(I18N.instance.getValue("^zhuizong12") + I18N.instance.getValue(currentdata36.country), 0.3f);
		InvokeRepeating("UpdateTime", 0.1f, 0.3f);
		txt_time.DOText(I18N.instance.getValue("^zhuizong13"), 0.3f);
		yield return new WaitForSeconds(0.2f);
		img_jindu.DOFillAmount(1f, 0.2f);
		img_jindudot.gameObject.SetActive(value: true);
		Sequence sequence4 = DOTween.Sequence();
		sequence4.Append(img_jindudot.transform.DOLocalMoveX(173f, 4f).SetEase(Ease.InOutCirc));
		sequence4.Append(img_jindudot.transform.DOLocalMoveX(9.2f, 4f).SetEase(Ease.InOutCirc));
		sequence4.SetLoops(-1);
		sequence4.Play();
		img_map.transform.DOScale(Vector3.one, 0.5f);
		img_map.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
	}

	public override void AfterShowSize()
	{
		StartCoroutine(StartAnimationStep1());
		InitTabs();
	}

	public override void BeforeShowSize()
	{
	}
}
