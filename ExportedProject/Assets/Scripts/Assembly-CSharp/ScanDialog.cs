using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ScanDialog : CustomDialog
{
	public Image img_drag;

	public GameObject dragcontent;

	public GameObject scancontent;

	public TypewriterEffect txt_title1;

	public string itemid;

	public Image img_red;

	public AccuracyUI accuracyUI;

	public Transform scanpanel;

	public ScanPicDialog scanPicDialog;

	public TypewriterEffect txt_drag1;

	public TypewriterEffect txt_drag2;

	public GameObject dragPanel;

	public GameObject doingPanel;

	public TypewriterEffect txt_drag3;

	private string[] typewirterstrings = new string[8] { "^analy_label", "^analy_sharing", "^analy_opening", "^analy_going", "^analy_searching", "^analy_searchinfo", "^analy_faile", "^analy_success" };

	private void Start()
	{
		toolid = 4;
		txt_drag3.StartEffect(I18N.instance.getValue(typewirterstrings[0]));
		txt_drag1.StartEffect(I18N.instance.getValue(typewirterstrings[0]));
		txt_drag2.StartEffect(I18N.instance.getValue(typewirterstrings[1]));
	}

	public void StartScan(string id)
	{
		itemid = id;
		base.transform.SetAsLastSibling();
		StartCoroutine(StartAcc());
	}

	private IEnumerator StartAcc()
	{
		gameManager.soundManager.PlaySoundLoop(4);
		dragPanel.SetActive(value: false);
		doingPanel.SetActive(value: true);
		for (int i = 0; i < scanpanel.childCount; i++)
		{
			Object.Destroy(scanpanel.GetChild(i).gameObject);
		}
		txt_drag1.StartEffect(I18N.instance.getValue(typewirterstrings[0]));
		txt_drag2.StartEffect(I18N.instance.getValue(typewirterstrings[3]));
		accuracyUI.Restart(0f);
		float loadWidth = 0f;
		for (int j = 1; j <= 3; j++)
		{
			float num = (float)Random.Range(15, 30) * 0.1f;
			loadWidth += num * 10f;
			accuracyUI.FreshAcc(loadWidth);
			yield return new WaitForSeconds(num * 1.1f);
		}
		accuracyUI.FreshAcc(100f);
		yield return new WaitForSeconds(1f);
		ChangeContent();
		txt_drag1.StartEffect(I18N.instance.getValue(typewirterstrings[3]));
		gameManager.soundManager.Stop();
	}

	public void ShowScan(bool iscorrect)
	{
		StartCoroutine(StartAcc2(iscorrect));
	}

	private IEnumerator StartAcc2(bool iscorrect)
	{
		gameManager.soundManager.PlaySoundLoop(4);
		accuracyUI.Restart(0f);
		txt_drag1.StartEffect(I18N.instance.getValue(typewirterstrings[3]));
		txt_drag2.StartEffect(I18N.instance.getValue(typewirterstrings[3]));
		accuracyUI.gameObject.SetActive(value: true);
		img_red.gameObject.SetActive(value: false);
		float loadWidth = 0f;
		for (int i = 1; i <= 3; i++)
		{
			float num = (float)Random.Range(15, 30) * 0.1f;
			loadWidth += num * 10f;
			accuracyUI.FreshAcc(loadWidth);
			yield return new WaitForSeconds(num * 1.1f);
		}
		accuracyUI.FreshAcc(100f);
		yield return new WaitForSeconds(1f);
		gameManager.soundManager.Stop();
		if (!iscorrect)
		{
			accuracyUI.gameObject.SetActive(value: false);
			img_red.gameObject.SetActive(value: true);
			txt_drag1.StartEffect(I18N.instance.getValue(typewirterstrings[3]));
			scanPicDialog.ShowNoClub();
		}
		else
		{
			txt_drag1.StartEffect(I18N.instance.getValue(typewirterstrings[3]));
		}
		if (scancontent != null)
		{
			scancontent.GetComponent<ScanPicDialog>().ShowPoint();
		}
	}

	private void ChangeContent()
	{
		scancontent = (GameObject)Object.Instantiate(Resources.Load("Scan/scancontent" + itemid), scanpanel);
		scancontent.SetActive(value: true);
		scancontent.GetComponent<ScanPicDialog>().scanDialog = this;
		scancontent.GetComponent<ScanPicDialog>().ChangeContent();
		scanPicDialog = scancontent.GetComponent<ScanPicDialog>();
	}

	public void ClosePic()
	{
		if (scanPicDialog.gameObject != null)
		{
			Object.Destroy(scanPicDialog.gameObject);
		}
		doingPanel.SetActive(value: false);
		dragPanel.SetActive(value: true);
		StopAllCoroutines();
		Invoke("ResetFenxi", 0.5f);
	}

	private void ResetFenxi()
	{
		accuracyUI.Restart(0f);
		Debug.Log("restart");
		txt_drag3.StartEffect(I18N.instance.getValue("^scandialog02"));
		txt_drag1.StartEffect(I18N.instance.getValue("^scandialog01"));
		txt_drag2.StartEffect(I18N.instance.getValue("^scandialog02"));
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
		StartCoroutine(StartAnimation());
	}

	public IEnumerator StartAnimation()
	{
		yield return new WaitForSeconds(0.1f);
		txt_title1.StartEffect("PICTURE ANALYSIS");
		txt_drag3.StartEffect(I18N.instance.getValue("^scandialog02"));
		txt_drag1.StartEffect(I18N.instance.getValue("^scandialog01"));
		txt_drag2.StartEffect(I18N.instance.getValue("^scandialog02"));
	}
}
