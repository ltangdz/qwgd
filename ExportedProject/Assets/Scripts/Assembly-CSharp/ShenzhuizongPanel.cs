using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class ShenzhuizongPanel : MonoBehaviour
{
	private GameManager gameManager;

	[SerializeField]
	private Image img_frame;

	[SerializeField]
	private Image img_circleframe01;

	[SerializeField]
	private Image img_circleframe02;

	[SerializeField]
	private Image img_scanline;

	[SerializeField]
	private List<GameObject> whitedotlist = new List<GameObject>();

	[SerializeField]
	private Image img_circle01;

	[SerializeField]
	private Image img_circle02;

	[SerializeField]
	private Image img_arrow;

	[SerializeField]
	private GameObject codegroup;

	[SerializeField]
	private List<Text> codetextlist = new List<Text>();

	[SerializeField]
	private Button btn_sure;

	[SerializeField]
	private Image img_bigerror;

	[SerializeField]
	private Image img_middleicon;

	[SerializeField]
	private GameObject dotpanel;

	[SerializeField]
	private Text txt_content;

	public int count = 8;

	public string data36id = "3600001";

	private DATA36 data36;

	public ShenzhuizongItem curretnshenzhuizongItem;

	[SerializeField]
	private Animator successwindow;

	public Zhuizongmap zhuizongmap;

	[SerializeField]
	private GameObject img_scan01;

	[SerializeField]
	private GameObject img_scan02;

	[SerializeField]
	private bool iscansure;

	private bool iscanclick = true;

	private void Init()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Append(img_circle01.transform.DOLocalRotate(new Vector3(0f, 0f, -60f), 2f).SetEase(Ease.InOutCirc));
		sequence.Append(img_circle01.transform.DOLocalRotate(new Vector3(0f, 0f, 60f), 4f).SetEase(Ease.InOutCirc));
		sequence.Append(img_circle01.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 2f).SetEase(Ease.InOutCirc)).SetLoops(-1);
		sequence.Play();
		Sequence sequence2 = DOTween.Sequence();
		sequence2.Append(img_circle02.transform.DOLocalRotate(new Vector3(0f, 0f, 30f), 1f).SetEase(Ease.InOutBounce));
		sequence2.Append(img_circle02.transform.DOLocalRotate(new Vector3(0f, 0f, -30f), 2f).SetEase(Ease.InOutBounce));
		sequence2.Append(img_circle02.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 1f).SetEase(Ease.InOutBounce)).SetLoops(-1);
		sequence2.Play();
		Sequence sequence3 = DOTween.Sequence();
		sequence3.Append(img_arrow.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 1f).SetEase(Ease.InOutBounce));
		sequence3.Append(img_arrow.transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f)).SetEase(Ease.InOutBounce).SetLoops(-1);
		sequence3.Play();
	}

	private IEnumerator InitDot()
	{
		dotpanel.GetComponent<HorizontalLayoutGroup>().enabled = true;
		int rightpos = Random.Range(0, count);
		for (int i = 0; i < count; i++)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("shenzhuizong_dot"), dotpanel.transform);
			if (i == rightpos)
			{
				string[] array = data36.itemids.Substring(1).Split(';');
				int num = gameManager.homeScene.computerButtonBox.surveillanceDialog.GetComponent<ZhuizongDialog>().img_map.GetComponent<Zhuizongmap>().count;
				if (num < array.Length)
				{
					gameObject.GetComponent<ShenzhuizongItem>().Init(i == rightpos, gameManager.dataManager.dic1[array[num]].image);
				}
			}
			else
			{
				gameObject.GetComponent<ShenzhuizongItem>().Init(i == rightpos, "");
			}
			gameObject.GetComponent<ShenzhuizongItem>().shenzhuizongPanel = this;
			whitedotlist.Add(gameObject);
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(0.1f);
		dotpanel.GetComponent<HorizontalLayoutGroup>().enabled = false;
		yield return new WaitForSeconds(0.1f);
		for (int j = 0; j < whitedotlist.Count; j++)
		{
			whitedotlist[j].GetComponent<ShenzhuizongItem>().Move();
		}
		yield return new WaitForSeconds(1f);
		iscansure = true;
	}

	public void Sure()
	{
		if (iscansure && curretnshenzhuizongItem != null && iscanclick)
		{
			txt_content.text = I18N.instance.getValue("^zhuizong19");
			StartCoroutine(StartSureAnimation());
			for (int i = 0; i < whitedotlist.Count; i++)
			{
				whitedotlist[i].GetComponent<ShenzhuizongItem>().MovePause();
			}
		}
	}

	private IEnumerator StartSureAnimation()
	{
		iscanclick = false;
		codegroup.SetActive(value: true);
		codegroup.transform.DOLocalMoveY(40f, 1.5f).SetLoops(-1);
		InvokeRepeating("StartCode", 0.1f, 0.2f);
		yield return new WaitForSeconds(3f);
		codegroup.transform.DOKill();
		StopAllCoroutines();
		codegroup.SetActive(value: false);
		if (curretnshenzhuizongItem != null && !curretnshenzhuizongItem.isright)
		{
			txt_content.color = Color.red;
			txt_content.text = I18N.instance.getValue("^zhuizong21");
			img_bigerror.gameObject.SetActive(value: true);
			Debug.Log("cuocuocuo");
			Sequence sequence = DOTween.Sequence();
			sequence.Append(img_bigerror.DOFade(0.2f, 0.5f));
			sequence.Append(img_bigerror.DOFade(1f, 0.5f));
			sequence.Play().SetLoops(3).OnComplete(delegate
			{
				iscanclick = true;
				img_bigerror.gameObject.SetActive(value: false);
				for (int i = 0; i < whitedotlist.Count; i++)
				{
					whitedotlist[i].GetComponent<ShenzhuizongItem>().MoveResume();
				}
				txt_content.color = Color.white;
				txt_content.text = I18N.instance.getValue("^zhuizong17");
			});
		}
		else
		{
			successwindow.gameObject.SetActive(value: true);
			successwindow.Play("Exit Panel In");
			txt_content.text = I18N.instance.getValue("^zhuizong18");
			iscanclick = true;
		}
	}

	public void GoOn()
	{
		successwindow.Play("Exit Panel Out");
		zhuizongmap.GoOn();
		Object.Destroy(base.gameObject);
	}

	private void StartCode()
	{
		for (int i = 0; i < codetextlist.Count; i++)
		{
			float num = Random.Range(1E-28f, 0.1f);
			codetextlist[i].text = num.ToString("f30");
		}
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		data36 = gameManager.dataManager.dic36[data36id];
		img_frame.transform.DOLocalMoveY(514f, 30f).SetEase(Ease.Linear).SetLoops(-1);
		img_scanline.transform.DOLocalMoveX(1284f, 4f).SetEase(Ease.Linear).SetLoops(-1);
		img_circleframe01.transform.DOLocalRotate(new Vector3(0f, 0f, 180f), 10f).SetEase(Ease.Linear).SetLoops(-1);
		img_circleframe02.transform.DOLocalRotate(new Vector3(0f, 0f, -180f), 15f).SetEase(Ease.Linear).SetLoops(-1);
		Init();
		StartCoroutine(InitDot());
		btn_sure.onClick.AddListener(Sure);
		if (data36id.Equals("3600001"))
		{
			img_scan01.SetActive(value: true);
			img_scan02.SetActive(value: false);
		}
		else if (data36id.Equals("3600002"))
		{
			img_scan01.SetActive(value: false);
			img_scan02.SetActive(value: true);
		}
	}
}
