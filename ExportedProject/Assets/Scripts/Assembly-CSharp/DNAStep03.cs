using System.Collections;
using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class DNAStep03 : MonoBehaviour
{
	private GameManager gameManager;

	[SerializeField]
	private GameObject dnapanel;

	[SerializeField]
	private Text txt_title;

	[SerializeField]
	private Button btn_close;

	private string[] str_nums = new string[17]
	{
		"AC-GENE-DNA     DNA2328901NADF189203", "BN-GENE-DNA     DNA1233901NADF035683", "SE-GENE-DNA     DNA2328901NADF234688", "UJ-GENE-DNA     DNA2354601NAD7787543", "TH-GENE-DNA     DNA6758901NADF235566", "3W-GENE-DNA     DNA2328901NADF875443", "AC-GENE-DNA     DNA2328901NADF865432", "W3-GENE-DNA     DNA2328901NADF665343", "SD-GENE-DNA     DNA2364345NADF533498", "TT-GENE-DNA     DNA2312341NADF854332",
		"AS-GENE-DNA     DNA2328901NAD1379654", "WS-GENE-DNA     DNA7768901NADF986543", "QW-GENE-DNA     DNA1235761NADF097654", "NB-GENE-DNA     DNA0866321NADF236899", "IO-GENE-DNA     DNA1235678NADF236478", "PO-GENE-DNA     DNA0984231NADF234654", "ER-GENE-DNA     DNA6799732NADF632356"
	};

	[SerializeField]
	private List<Text> txt_linenums = new List<Text>();

	[SerializeField]
	private DNADialog dnadialog;

	private int currentnumpos;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		txt_title.text = string.Format(I18N.instance.getValue("^dna13"), I18N.instance.getValue("^livename40"), I18N.instance.getValue("^name_ht0519"));
		btn_close.onClick.RemoveAllListeners();
		InvokeRepeating("SetNums", 0.1f, 0.3f);
		StartCoroutine(Over());
	}

	private IEnumerator Over()
	{
		yield return new WaitForSeconds(5f);
		CancelInvoke();
		dnapanel.SetActive(value: true);
		yield return new WaitForSeconds(3f);
		if (!gameManager.player.playerdata.temporaryhopelist.Contains("10592"))
		{
			gameManager.homeScene.zhibojiannotebook.AddNewItem("10592");
			yield return new WaitForSeconds(3f);
		}
		dnadialog.Hide();
	}

	private void SetNums()
	{
		for (int i = 0; i < 5; i++)
		{
			int index = Random.Range(0, txt_linenums.Count);
			int num = Random.Range(0, str_nums.Length);
			txt_linenums[index].text = str_nums[num];
		}
	}
}
