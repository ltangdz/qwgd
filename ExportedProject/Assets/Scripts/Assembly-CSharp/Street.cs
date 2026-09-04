using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Street : MonoBehaviour
{
	public GameObject p1;

	public GameObject p2;

	public GameObject p3;

	private int crtShowPerson;

	private GameObject[] person;

	private float intervalTime = 2f;

	private int showSceneTime = 1;

	private Image objAD1_1;

	private Image objAD1_2;

	private Image objAD2_1;

	private Image objAD2_2;

	private bool changeADImg;

	private float alp;

	private GameManager gameManager;

	public GameObject txt_zimu;

	private void Start()
	{
		alp = 0f;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		changeADImg = false;
		person = new GameObject[3] { p1, p2, p3 };
		StartCoroutine(PersonWalk());
		objAD1_1 = base.transform.Find("img_street/ad_box/img_ad1_1").GetComponent<Image>();
		objAD1_2 = base.transform.Find("img_street/ad_box/img_ad1_2").GetComponent<Image>();
		objAD2_1 = base.transform.Find("img_street/ad_box/img_ad2_1").GetComponent<Image>();
		objAD2_2 = base.transform.Find("img_street/ad_box/img_ad2_2").GetComponent<Image>();
		StartCoroutine(ChangeAD());
		StartCoroutine(ShowLabel());
		gameManager.Esc.GetComponent<HoldEsc>().sceneName = base.transform.parent.name;
	}

	private IEnumerator PersonWalk()
	{
		while (true)
		{
			person[crtShowPerson].GetComponent<StreetPerson>().Show(showSceneTime);
			crtShowPerson++;
			if (crtShowPerson > 2)
			{
				crtShowPerson = 0;
			}
			if (showSceneTime == 1)
			{
				intervalTime = 0.8f;
			}
			else
			{
				intervalTime = 2f;
			}
			showSceneTime++;
			if (showSceneTime == 6 && !gameManager.holdEsc)
			{
				gameManager.startAniManager.ChangeScene("Canvas03");
			}
			yield return new WaitForSeconds(intervalTime);
		}
	}

	private IEnumerator ChangeAD()
	{
		yield return new WaitForSeconds(4f);
		changeADImg = true;
	}

	private void Update()
	{
		if (changeADImg)
		{
			if ((double)alp <= 0.99999)
			{
				alp += 0.01f;
			}
			else
			{
				changeADImg = false;
			}
			objAD1_1.color = new Color(1f, 1f, 1f, 1f - alp);
			objAD2_1.color = new Color(1f, 1f, 1f, 1f - alp);
			objAD1_2.color = new Color(1f, 1f, 1f, alp);
			objAD2_2.color = new Color(1f, 1f, 1f, alp);
		}
	}

	private IEnumerator ShowLabel()
	{
		yield return new WaitForSeconds(1f);
		GameObject.Find("GameManager").GetComponent<GameManager>().ShowLabel(txt_zimu);
		yield return new WaitForSeconds(4f);
		GameObject.Find("GameManager").GetComponent<GameManager>().HideLabel(txt_zimu);
	}
}
