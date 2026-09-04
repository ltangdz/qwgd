using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class HackerUploadDialog : CustomDialog
{
	[SerializeField]
	private GameObject codedialog;

	[SerializeField]
	private GameObject img_red;

	[SerializeField]
	private Image img_fill;

	[SerializeField]
	private Text txt_title;

	[SerializeField]
	private Button btn_pause;

	private bool iscanclick = true;

	private bool isupdate;

	[SerializeField]
	private GameObject uploaddialog02;

	[SerializeField]
	private ScrollRect scrollRect;

	private bool is40;

	private string[] upload2string = new string[21]
	{
		"admin @wks80:~$ grep root etc/crypto", "grep: /etc/crypto: Permission Denied", "ablkcipher_done_fast(walk)", "[sudo] password for admin: **********", "admin @wks80:~$", "scatterwalk_done(&amp;walk-&gt;in, 0, nbytes)", "pico ablkcipher.c", "ALIGN(sizeof(struct ablkcipher_buffer), alignmask + 1)", "p-&gt;len = bsize", "struct list_head entry;",
		"struct scatter_walk dst;", "unsigned int len;", "void* data;", "scatterwalk_copychunks(src, &amp;walk-&gt;in, bsize, 0)", "enum {", "ABLKCIPHER_WALK_SLOW = 1 & lt;&lt; 0,", "};", "ablkcipher_get_spot(iv, bs) + aligned_bs", "ablkcipher_get_spot(iv, ivsize)", "scatterwalk_copychunks(p-&gt; data, &amp;p-&gt;dst, p-&gt;len, 1);",
		"run"
	};

	private int pos;

	public override void AfterShowSize()
	{
	}

	public override void BeforeShowSize()
	{
	}

	private void Init()
	{
		isupdate = true;
		img_fill.DOFillAmount(0.4f, 20f).SetEase(Ease.Linear).OnComplete(delegate
		{
			iscanclick = false;
			StartCoroutine(StartAddCode());
			img_fill.DOFillAmount(0.85f, 3f).SetEase(Ease.Linear).OnComplete(delegate
			{
				isupdate = false;
				Over();
			});
		});
		uploaddialog02.transform.DOScale(Vector3.one, 0.3f);
		uploaddialog02.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		btn_pause.onClick.AddListener(StopUpload);
		StartCoroutine(StartAddCode());
	}

	private IEnumerator StartAddCode()
	{
		for (int i = pos; i < upload2string.Length; i++)
		{
			Object.Instantiate(Resources.Load("Hacker/txt_code2") as GameObject, scrollRect.content).GetComponent<Text>().DOText(upload2string[i], (float)upload2string[i].Length * 0.05f);
			pos++;
			yield return new WaitForSeconds((float)upload2string[i].Length * 0.05f);
			scrollRect.normalizedPosition = Vector3.zero;
		}
	}

	private void StopUpload()
	{
		if (iscanclick)
		{
			is40 = true;
			iscanclick = false;
			img_fill.DOKill();
			StopAllCoroutines();
			isupdate = false;
			Object.Instantiate(Resources.Load("Dialog/Hacker/hackervideoDialog02") as GameObject, gameManager.homeScene.middle).GetComponent<HackerVideoDialog02>().hackerUploadDialog = this;
			btn_pause.interactable = false;
			btn_pause.transform.GetChild(0).GetComponent<Text>().color = Color.gray;
		}
	}

	private void Start()
	{
		Init();
	}

	public void GoOn()
	{
		isupdate = true;
		StartCoroutine(StartAddCode());
		img_fill.DOFillAmount(0.85f, 3f).SetEase(Ease.Linear).OnComplete(delegate
		{
			isupdate = false;
			StopAllCoroutines();
			Over();
		});
	}

	private void Over()
	{
		img_red.SetActive(value: true);
		txt_title.text = I18N.instance.getValue("^hacker04") + "... (85%)";
		btn_pause.interactable = false;
		btn_pause.transform.GetChild(0).GetComponent<Text>().color = Color.gray;
		gameManager.soundManager.PlayHackerSoundLoop(7);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(img_red.GetComponent<Image>().DOFade(0.2f, 0.2f));
		sequence.Append(img_red.GetComponent<Image>().DOFade(1f, 0.2f));
		sequence.Play().SetLoops(4).OnComplete(delegate
		{
			gameManager.homeScene.cameraFilterPack_Noise_TV_2.enabled = true;
			Sequence sequence2 = DOTween.Sequence();
			sequence2.Append(img_red.GetComponent<Image>().DOFade(0.2f, 0.2f));
			sequence2.Append(img_red.GetComponent<Image>().DOFade(1f, 0.2f));
			sequence2.Play().SetLoops(2).OnComplete(delegate
			{
				Object.Instantiate(Resources.Load("Dialog/Hacker/hackervideoDialog03") as GameObject, gameManager.homeScene.middle).GetComponent<HackerVideoDialog03>().zimus[0] = ((!is40) ? "^hackervd0616" : "^hackervd0301");
				Hide();
			});
		});
	}

	private void Update()
	{
		if (isupdate)
		{
			txt_title.text = I18N.instance.getValue("^hacker04") + "... (" + (int)(img_fill.fillAmount * 100f) + "%)";
		}
	}
}
