using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadePojieBox : MonoBehaviour
{
	public List<GameObject> step;

	public List<GameObject> point;

	public GameObject getPassword;

	public GameObject textInfo;

	public GameObject pojie;

	public Text pojieTxtInfo;

	public string truepassword;

	public GameObject resultBox;

	public Slider slider_hor;

	public Slider slider_ver;

	public RectTransform img_shengbo;

	public InvadeLoading loadBox;

	private Coroutine bkLight;

	private Coroutine pointLight;

	private string[] passwordVal = new string[31]
	{
		"A", "B", "C", "D", "E", "F", "G", "H", "J", "K",
		"M", "N", "P", "Q", "R", "S", "T", "U", "V", "W",
		"X", "Y", "Z", "2", "3", "4", "5", "6", "7", "8",
		"9"
	};

	private InvadeDialog parObj;

	private GameManager _gameManager;

	public GameManager GameManager
	{
		get
		{
			if (_gameManager == null)
			{
				_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			}
			return _gameManager;
		}
	}

	public void Init(int i, GameManager gm, InvadeDialog par = null)
	{
		parObj = par;
		Step(i);
	}

	public void SetInvadeDialog(InvadeDialog invadeDialog)
	{
		parObj = invadeDialog;
	}

	public void InitShow()
	{
		StartCoroutine(ShowIcon());
	}

	private IEnumerator ShowIcon()
	{
		StartCoroutine(Reversal(step[0]));
		yield return new WaitForSeconds(0.3f);
		step[0].transform.Find("Text").GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
		StartCoroutine(Reversal(step[1]));
		StartCoroutine(ShowPoint(point[0].transform));
		yield return new WaitForSeconds(0.3f);
		step[1].transform.Find("Text").GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
		StartCoroutine(Reversal(step[2]));
		StartCoroutine(ShowPoint(point[1].transform));
		yield return new WaitForSeconds(0.3f);
		step[2].transform.Find("Text").GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
	}

	private IEnumerator Reversal(GameObject iconObj)
	{
		iconObj.GetComponent<RectTransform>().localScale = new Vector3(0f, 1f, 1f);
		iconObj.GetComponent<RectTransform>().DOScaleX(-1f, 0.1f);
		yield return new WaitForSeconds(0.1f);
		iconObj.GetComponent<RectTransform>().DOScaleX(1f, 0.2f);
	}

	private IEnumerator ShowPoint(Transform point)
	{
		for (int i = 0; i < point.childCount; i++)
		{
			point.GetChild(i).gameObject.SetActive(value: true);
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void StepLoading(int i, bool sce)
	{
		Debug.Log("StepLoading:" + i + "---sce:" + sce.ToString());
		if (sce)
		{
			step[i - 1].transform.GetChild(0).Find("img_outline").GetComponent<Image>()
				.DOFillAmount(1f, 2f)
				.SetEase(Ease.Linear);
			return;
		}
		float endValue = Random.Range(5f, 8f) * 0.1f;
		step[i - 1].transform.GetChild(0).Find("img_outline").GetComponent<Image>()
			.DOFillAmount(endValue, 1f)
			.SetEase(Ease.Linear)
			.OnComplete(delegate
			{
				step[i - 1].transform.GetChild(0).Find("img_outline").GetComponent<Image>()
					.fillAmount = 0f;
			});
	}

	public void Step(int i, bool pojieSql = true)
	{
		if (i < step.Count)
		{
			if (pointLight != null)
			{
				StopCoroutine(pointLight);
			}
			if (i != 0)
			{
				pointLight = StartCoroutine(PointLight(point[i - 1]));
			}
		}
		if (i == 0)
		{
			return;
		}
		if (pojieSql)
		{
			if (i == 1)
			{
				GetPassword();
			}
			if (i == 2)
			{
				if (GameManager.Is_Dlc7())
				{
					StartCoroutine(StartPojieDlc7());
				}
				else
				{
					StartCoroutine(StartPojie());
				}
			}
		}
		CompleteStep(i);
	}

	private IEnumerator StepLighting(int i)
	{
		while (true)
		{
			step[i].transform.GetChild(0).DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.3f);
			step[i].transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(0.5f, 0.3f);
			yield return new WaitForSeconds(0.3f);
			step[i].transform.GetChild(0).DOScale(new Vector3(1f, 1f, 1f), 0.3f);
			step[i].transform.GetChild(0).GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
			yield return new WaitForSeconds(0.3f);
		}
	}

	private IEnumerator PointLight(GameObject pointPar)
	{
		int i = 0;
		while (true)
		{
			float num = pointPar.transform.childCount;
			if ((float)i < num)
			{
				pointPar.transform.GetChild(i).GetChild(0).gameObject.SetActive(value: true);
				i++;
				yield return new WaitForSeconds(0.2f);
			}
			else if ((float)i == num)
			{
				for (int j = 0; (float)j < num; j++)
				{
					pointPar.transform.GetChild(j).GetChild(0).gameObject.SetActive(value: false);
				}
				i = 0;
				yield return new WaitForSeconds(0.2f);
			}
		}
	}

	private void GetPassword()
	{
		getPassword.SetActive(value: true);
		List<string> list = new List<string>();
		if (GameManager.Is_Dlc7() && GameManager.player.playerdata.dlc7Invades[0] == 1)
		{
			list = new List<string> { "4", "1", "4", "6", "A", "Q", "Q", "D" };
		}
		else
		{
			for (int i = 0; i < 8; i++)
			{
				string text = passwordVal[Random.Range(0, passwordVal.Length)];
				truepassword += text;
				list.Add(text);
			}
		}
		textInfo.GetComponent<RunPassword>().SetPassword(list);
	}

	private IEnumerator StartPojieDlc7()
	{
		getPassword.SetActive(value: false);
		pojie.SetActive(value: true);
		yield return new WaitForSeconds(0.5f);
		string value = I18N.instance.getValue("^110008_invade_1");
		pojieTxtInfo.DOText(value, 0.5f);
		yield return new WaitForSeconds(0.5f);
		pojieTxtInfo.GetComponent<CanvasGroup>().DOFade(0.5f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		pojieTxtInfo.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		yield return new WaitForSeconds(0.6f);
		pojieTxtInfo.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		pojieTxtInfo.text = "";
		pojieTxtInfo.GetComponent<CanvasGroup>().alpha = 1f;
		string value2 = I18N.instance.getValue("^110008_invade_2");
		pojieTxtInfo.DOText(value2, 0.5f);
		yield return new WaitForSeconds(0.5f);
		pojieTxtInfo.GetComponent<CanvasGroup>().DOFade(0.5f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		pojieTxtInfo.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		yield return new WaitForSeconds(0.6f);
		pojieTxtInfo.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		pojieTxtInfo.text = "";
		pojieTxtInfo.GetComponent<CanvasGroup>().alpha = 1f;
		string value3 = I18N.instance.getValue("^invade_label19_1");
		pojieTxtInfo.DOText(value3, 0.5f);
		parObj.ComSucc();
	}

	private IEnumerator StartPojie()
	{
		getPassword.SetActive(value: false);
		pojie.SetActive(value: true);
		yield return new WaitForSeconds(0.5f);
		string endValue = I18N.instance.getValue("^invade_label15") + GameManager.dataManager.dic33[parObj.userid].ip.Substring(1);
		pojieTxtInfo.DOText(endValue, 0.5f);
		yield return new WaitForSeconds(0.5f);
		pojieTxtInfo.GetComponent<CanvasGroup>().DOFade(0.5f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		pojieTxtInfo.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		yield return new WaitForSeconds(0.6f);
		pojieTxtInfo.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		pojieTxtInfo.text = "";
		pojieTxtInfo.GetComponent<CanvasGroup>().alpha = 1f;
		string endValue2 = I18N.instance.getValue("^invade_label17") + GameManager.dataManager.dic33[parObj.userid].port.Substring(1);
		pojieTxtInfo.DOText(endValue2, 0.5f);
		yield return new WaitForSeconds(0.5f);
		pojieTxtInfo.GetComponent<CanvasGroup>().DOFade(0.5f, 0.3f);
		yield return new WaitForSeconds(0.3f);
		pojieTxtInfo.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		yield return new WaitForSeconds(0.6f);
		pojieTxtInfo.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		pojieTxtInfo.text = "";
		pojieTxtInfo.GetComponent<CanvasGroup>().alpha = 1f;
		string value = I18N.instance.getValue("^invade_label19_1");
		pojieTxtInfo.DOText(value, 0.5f);
		parObj.ComSucc();
	}

	public void CompleteStep(int i)
	{
		if (i == 0)
		{
			return;
		}
		if (i == step.Count)
		{
			StopCoroutine(pointLight);
		}
		Transform child = step[i - 1].transform.GetChild(0);
		child.Find("img_light").gameObject.SetActive(value: true);
		child.Find("img_outline").gameObject.SetActive(value: false);
		child.Find("img_outline").GetComponent<Image>().fillAmount = 1f;
		child.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		child.GetComponent<CanvasGroup>().alpha = 1f;
		step[i - 1].transform.GetChild(1).Find("on").gameObject.SetActive(value: true);
		step[i - 1].transform.Find("icon/on").gameObject.SetActive(value: true);
		step[i - 1].transform.Find("Text").GetComponent<Text>().text = "<color=#ffffff>" + step[i - 1].transform.Find("Text").GetComponent<Text>().text + "</color>";
		if (i > 1)
		{
			float num = point[i - 2].transform.childCount;
			for (int j = 0; (float)j < num; j++)
			{
				point[i - 2].transform.GetChild(j).GetChild(0).gameObject.SetActive(value: true);
			}
		}
	}

	public void ChangeHor()
	{
		img_shengbo.localScale = new Vector2(slider_hor.value, img_shengbo.localScale.y);
	}

	public void ChangeVer()
	{
		img_shengbo.localScale = new Vector2(img_shengbo.localScale.x, slider_ver.value);
	}
}
