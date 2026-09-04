using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class VideoDialog : MonoBehaviour
{
	public Text txt_name;

	public TypewriterEffect txt_zimu;

	public Text txt_zimu2;

	public string[] zimus;

	public string[] answers;

	public SelectGroup selectGroup;

	public Image img_black;

	public Image img_news1;

	public Image img_news2;

	public Image img_news3;

	public Image img_news4;

	public HomeScene homeScene;

	public Button btn_ringoff;

	public GameObject women;

	public GameObject womensmile;

	public Animator zhangzui;

	public Animator zhangzuismile;

	public float currentzimupos;

	public bool iscanclick = true;

	public Image img_click;

	public bool isstart;

	public Image img_mouse;

	private bool isSheOk = true;

	public string mailid;

	private GameManager gameManager;

	public int currentpos;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		homeScene = gameManager.homeScene;
		Invoke("StartVideo", 1f);
		btn_ringoff.onClick.AddListener(delegate
		{
			txt_zimu2.text = "";
			GetComponent<Animator>().Play("ani_videoHide");
			homeScene.GetTask(mailid);
		});
	}

	private IEnumerator StartZhuangzuiAnimation()
	{
		img_mouse.gameObject.SetActive(value: false);
		if (zhangzui.gameObject.activeSelf)
		{
			zhangzui.Play("ani_openMouth");
		}
		if (zhangzuismile.gameObject.activeSelf)
		{
			zhangzuismile.Play("ani_openMouth");
		}
		yield return new WaitForSeconds(2f);
		if (zhangzui.gameObject.activeSelf)
		{
			zhangzui.Play("Empty");
		}
		if (zhangzuismile.gameObject.activeSelf)
		{
			zhangzuismile.Play("Empty");
		}
		zhangzui.GetComponent<MeshRenderer>().enabled = false;
		yield return new WaitForSeconds(1f);
		if (zhangzui.gameObject.activeSelf)
		{
			zhangzui.Play("ani_openMouth");
		}
		if (zhangzuismile.gameObject.activeSelf)
		{
			zhangzuismile.Play("ani_openMouth");
		}
		zhangzui.GetComponent<MeshRenderer>().enabled = false;
		yield return new WaitForSeconds(2f);
		if (zhangzui.gameObject.activeSelf)
		{
			zhangzui.Play("Empty");
		}
		if (zhangzuismile.gameObject.activeSelf)
		{
			zhangzuismile.Play("Empty");
		}
		zhangzui.GetComponent<MeshRenderer>().enabled = false;
		img_mouse.gameObject.SetActive(value: true);
		iscanclick = true;
	}

	private void StartVideo()
	{
		isstart = true;
		Invoke("ClickZimu", 1f);
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.V))
		{
			selectGroup.HideSelect();
			txt_zimu2.text = "";
			btn_ringoff.interactable = true;
			women.SetActive(value: false);
			womensmile.SetActive(value: true);
			GetComponent<Animator>().Play("ani_smil");
			btn_ringoff.GetComponent<Animator>().Play("ani_breath");
			img_click.gameObject.SetActive(value: false);
			img_mouse.gameObject.SetActive(value: false);
			StartCoroutine(StartEnd());
		}
	}

	public void ClickZimu()
	{
		if (!zhangzui.gameObject.activeInHierarchy)
		{
			zhangzui.gameObject.SetActive(value: true);
		}
		if (!isstart || currentzimupos >= (float)zimus.Length || !iscanclick)
		{
			return;
		}
		if (currentzimupos == 0f || currentzimupos == 26f || currentzimupos == 28f)
		{
			StartCoroutine(StartZhuangzuiAnimation());
			txt_zimu2.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue(zimus[(int)currentzimupos].Trim()), gameManager.player.playerdata.nickname));
			currentzimupos += 1f;
			return;
		}
		if (currentzimupos == 3f)
		{
			StartCoroutine(StartZhuangzuiAnimation());
			txt_zimu2.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue(zimus[(int)currentzimupos].Trim()), gameManager.player.playerdata.nickname));
			currentzimupos = 30f;
			currentzimupos += 1f;
			return;
		}
		if (currentzimupos == 29f)
		{
			StartCoroutine(StartZhuangzuiAnimation());
			txt_zimu2.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue(zimus[(int)currentzimupos].Trim()), gameManager.player.playerdata.nickname));
			currentzimupos = 6f;
			currentzimupos += 1f;
			return;
		}
		if (currentzimupos == 30f)
		{
			img_black.gameObject.SetActive(value: true);
			InvokeRepeating("BlackShow", 0.1f, 0.1f);
			txt_zimu2.GetComponent<I18NText>().updateTranslation2(zimus[(int)currentzimupos].Trim());
			img_black.transform.DOScale(Vector3.one, 0.5f);
			img_news1.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
			currentzimupos = 13f;
			currentzimupos += 1f;
			return;
		}
		if (currentzimupos == 32f)
		{
			StartCoroutine(StartZhuangzuiAnimation());
			txt_zimu2.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue(zimus[(int)currentzimupos].Trim()), gameManager.player.playerdata.nickname));
			currentzimupos = 3f;
			currentzimupos += 1f;
			return;
		}
		StartCoroutine(StartZhuangzuiAnimation());
		txt_zimu2.GetComponent<I18NText>().updateTranslation2(zimus[(int)currentzimupos].Trim());
		if (currentzimupos == 5f)
		{
			currentzimupos += 0.5f;
		}
		else if (currentzimupos == 5.5f)
		{
			iscanclick = false;
			SetSelect(0, 2);
			currentzimupos += 0.5f;
		}
		else if (currentzimupos == 11f)
		{
			currentzimupos += 0.5f;
		}
		else if (currentzimupos == 11.5f)
		{
			currentzimupos += 0.5f;
			SetSelect(2, 4);
		}
		else if (currentzimupos == 13f)
		{
			img_black.gameObject.SetActive(value: true);
			InvokeRepeating("BlackShow", 0.1f, 0.1f);
			img_black.transform.DOScale(Vector3.one, 0.5f);
			img_news1.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
			currentzimupos += 1f;
		}
		else if (currentzimupos == 14f)
		{
			img_news1.transform.DOScale(Vector3.one, 0.5f);
			img_news1.transform.DOMove(new Vector3(-433f, 248f, 0f), 0.5f);
			currentzimupos += 1f;
		}
		else if (currentzimupos == 15f)
		{
			img_news2.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
			currentzimupos += 1f;
		}
		else if (currentzimupos == 16f)
		{
			img_news2.transform.DOScale(Vector3.one, 0.5f);
			img_news2.transform.DOMove(new Vector3(534f, 236f, 0f), 0.5f);
			currentzimupos += 1f;
		}
		else if (currentzimupos == 17f)
		{
			img_news3.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
			currentzimupos += 1f;
		}
		else if (currentzimupos == 18f)
		{
			img_news3.transform.DOScale(Vector3.one, 0.5f);
			img_news3.transform.DOMove(new Vector3(-444f, -212f, 0f), 0.5f);
			currentzimupos += 0.5f;
		}
		else if (currentzimupos == 18.5f)
		{
			img_news4.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
			currentzimupos += 0.5f;
		}
		else if (currentzimupos == 19f)
		{
			iscanclick = false;
			StartCoroutine(showNews1());
			currentzimupos += 1f;
		}
		else if (currentzimupos == 20f)
		{
			iscanclick = false;
			StartCoroutine(showNews2());
			currentzimupos += 1f;
		}
		else if (currentzimupos == 21f)
		{
			if (isSheOk)
			{
				currentzimupos += 1f;
			}
			else
			{
				currentzimupos += 6.5f;
			}
		}
		else if (currentzimupos == 22f)
		{
			currentzimupos += 0.5f;
		}
		else if (currentzimupos == 22.5f)
		{
			iscanclick = false;
			SetSelect(4, 6);
			currentzimupos += 0.5f;
		}
		else if (currentzimupos == 27f)
		{
			currentzimupos += 0.5f;
		}
		else if (currentzimupos == 27.5f)
		{
			iscanclick = false;
			SetSelect(6, 8);
			currentzimupos = 33f;
		}
		else
		{
			currentzimupos += 1f;
		}
	}

	public void HideVideoDialog()
	{
		Object.Destroy(base.gameObject);
	}

	private IEnumerator StartNews(int zimubegin, int zimuend, int selectbegin, int selectmuend)
	{
		txt_zimu.StartEffect(string.Format(I18N.instance.getValue(zimus[0].Trim()), gameManager.player.playerdata.nickname));
		yield return new WaitForSeconds(5f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[1].Trim()));
		yield return new WaitForSeconds(5f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[2].Trim()));
		yield return new WaitForSeconds(3f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[3].Trim()));
		yield return new WaitForSeconds(3f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[4].Trim()));
		yield return new WaitForSeconds(3f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[5].Trim()));
		yield return new WaitForSeconds(3f);
		SetSelect(selectbegin, selectmuend);
	}

	private void BlackShow()
	{
		img_black.GetComponent<CanvasGroup>().alpha += 0.2f;
		if (img_black.GetComponent<CanvasGroup>().alpha >= 1f)
		{
			CancelInvoke();
		}
	}

	private IEnumerator showNews1()
	{
		img_news4.transform.DOScale(Vector3.one, 0.5f);
		img_news4.transform.DOMove(new Vector3(297f, -144f, 0f), 0.5f);
		yield return new WaitForSeconds(1f);
		iscanclick = true;
	}

	private IEnumerator showNews2()
	{
		img_news1.transform.DOScale(Vector3.zero, 0.5f);
		img_news1.transform.DOMove(new Vector3(0f, 0f, 0f), 0.5f);
		img_news2.transform.DOScale(Vector3.zero, 1f);
		img_news2.transform.DOMove(new Vector3(0f, 0f, 0f), 0.5f);
		img_news3.transform.DOScale(Vector3.zero, 1f);
		img_news3.transform.DOMove(new Vector3(0f, 0f, 0f), 0.5f);
		img_news4.transform.DOScale(Vector3.zero, 1f);
		img_news4.transform.DOMove(new Vector3(0f, 0f, 0f), 0.5f);
		yield return new WaitForSeconds(0.5f);
		img_black.transform.DOScale(Vector3.zero, 0.5f);
		img_black.gameObject.SetActive(value: false);
		iscanclick = true;
	}

	private IEnumerator StartNews2()
	{
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[6].Trim()));
		yield return new WaitForSeconds(5f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[7].Trim()));
		yield return new WaitForSeconds(5f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[8].Trim()));
		yield return new WaitForSeconds(4f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[9].Trim()));
		yield return new WaitForSeconds(5f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[10].Trim()));
		yield return new WaitForSeconds(4f);
		img_black.gameObject.SetActive(value: true);
		InvokeRepeating("BlackShow", 0.1f, 0.1f);
		img_black.transform.DOScale(Vector3.one, 0.5f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[11].Trim()));
		yield return new WaitForSeconds(5f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[12].Trim()));
		img_news1.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f);
		yield return new WaitForSeconds(3f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[13].Trim()));
		img_news1.transform.DOScale(Vector3.one, 1f);
		img_news1.transform.DOMove(new Vector3(-433f, 248f, 0f), 1f);
		yield return new WaitForSeconds(4f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[14].Trim()));
		img_news2.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f);
		yield return new WaitForSeconds(4f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[15].Trim()));
		img_news2.transform.DOScale(Vector3.one, 1f);
		img_news2.transform.DOMove(new Vector3(534f, 236f, 0f), 1f);
		yield return new WaitForSeconds(4f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[16].Trim()));
		img_news3.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f);
		yield return new WaitForSeconds(5f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[17].Trim()));
		img_news3.transform.DOScale(Vector3.one, 1f);
		img_news3.transform.DOMove(new Vector3(-444f, -212f, 0f), 1f);
		yield return new WaitForSeconds(4f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[18].Trim()));
		img_news4.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f);
		yield return new WaitForSeconds(5f);
		img_news4.transform.DOScale(Vector3.one, 1f);
		img_news4.transform.DOMove(new Vector3(297f, -144f, 0f), 1f);
		yield return new WaitForSeconds(3f);
		img_news1.transform.DOScale(Vector3.zero, 0.5f);
		img_news1.transform.DOMove(new Vector3(0f, 0f, 0f), 0.5f);
		img_news2.transform.DOScale(Vector3.zero, 1f);
		img_news2.transform.DOMove(new Vector3(0f, 0f, 0f), 0.5f);
		img_news3.transform.DOScale(Vector3.zero, 1f);
		img_news3.transform.DOMove(new Vector3(0f, 0f, 0f), 0.5f);
		img_news4.transform.DOScale(Vector3.zero, 1f);
		img_news4.transform.DOMove(new Vector3(0f, 0f, 0f), 0.5f);
		yield return new WaitForSeconds(2f);
		img_black.transform.DOScale(Vector3.zero, 0.5f);
		img_black.gameObject.SetActive(value: false);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[19].Trim()));
		yield return new WaitForSeconds(3f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[20].Trim()));
		yield return new WaitForSeconds(3f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[21].Trim()));
		yield return new WaitForSeconds(3f);
		SetSelect(2, 4);
	}

	private IEnumerator StartNews3()
	{
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[22].Trim()));
		yield return new WaitForSeconds(4f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[23].Trim()));
		yield return new WaitForSeconds(4f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[24].Trim()));
		yield return new WaitForSeconds(4f);
		txt_zimu.StartEffect(string.Format(I18N.instance.getValue(zimus[25].Trim()), gameManager.player.playerdata.nickname));
		yield return new WaitForSeconds(4f);
		txt_zimu.StartEffect(I18N.instance.getValue(zimus[26].Trim()));
		yield return new WaitForSeconds(4f);
		SetSelect(4, 5);
	}

	private void SetSelect(int begin, int end)
	{
		StartCoroutine(StartSetSelect(begin, end));
	}

	private IEnumerator StartSetSelect(int begin, int end)
	{
		yield return new WaitForSeconds(0f);
		if (begin < 0)
		{
			yield return new WaitForSeconds(1f);
		}
		string[] array = new string[end - begin];
		for (int i = begin; i < end; i++)
		{
			array[i - begin] = answers[i];
		}
		selectGroup.gameObject.SetActive(value: true);
		selectGroup.SetSelect(array, ClickSelect);
		if (zhangzui.gameObject.activeSelf)
		{
			zhangzui.Play("Empty");
		}
		if (zhangzuismile.gameObject.activeSelf)
		{
			zhangzuismile.Play("Empty");
		}
		zhangzui.gameObject.SetActive(value: false);
		zhangzuismile.gameObject.SetActive(value: false);
	}

	public void ClickSelect(int poss)
	{
		if (!selectGroup.iscanclick)
		{
			return;
		}
		iscanclick = true;
		switch (currentpos)
		{
		case 0:
			switch (poss)
			{
			case 0:
				ClickZimu();
				break;
			case 1:
				currentzimupos = 28f;
				ClickZimu();
				break;
			}
			selectGroup.HideSelect();
			zhangzui.gameObject.SetActive(value: true);
			zhangzuismile.gameObject.SetActive(value: true);
			break;
		case 1:
			selectGroup.HideSelect();
			switch (poss)
			{
			case 0:
				ClickZimu();
				break;
			case 1:
				currentzimupos = 12f;
				ClickZimu();
				break;
			}
			zhangzui.gameObject.SetActive(value: true);
			zhangzuismile.gameObject.SetActive(value: true);
			break;
		case 2:
			selectGroup.HideSelect();
			switch (poss)
			{
			case 0:
				ClickZimu();
				break;
			case 1:
				currentzimupos = 23f;
				ClickZimu();
				break;
			}
			break;
		case 3:
			selectGroup.HideSelect();
			switch (poss)
			{
			case 0:
				btn_ringoff.interactable = true;
				women.SetActive(value: false);
				womensmile.SetActive(value: true);
				GetComponent<Animator>().Play("ani_smil");
				btn_ringoff.GetComponent<Animator>().Play("ani_breath");
				img_click.gameObject.SetActive(value: false);
				StartCoroutine(StartEnd());
				break;
			case 1:
				currentzimupos = 13f;
				currentpos = 1;
				currentpos++;
				isSheOk = false;
				Debug.Log("afadsfasdfadsfadsfasdf");
				ClickZimu();
				break;
			}
			break;
		}
		currentpos++;
	}

	private IEnumerator StartEnd()
	{
		yield return new WaitForSeconds(5f);
		img_mouse.gameObject.SetActive(value: false);
		txt_zimu2.GetComponent<I18NText>().updateTranslation2("^vd35");
	}
}
