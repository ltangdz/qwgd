using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DuikangDialog : MonoBehaviour
{
	public GameObject resultBox;

	public GameObject failedPanel;

	[SerializeField]
	private Transform timebox;

	[SerializeField]
	private Text txt_lefttime;

	[SerializeField]
	private int countdown;

	[SerializeField]
	private int count;

	[SerializeField]
	private List<Text> group1texts = new List<Text>();

	[SerializeField]
	private List<Text> group2texts = new List<Text>();

	public bool iscansendhack = true;

	[SerializeField]
	private List<string> ciostartlist = new List<string>();

	[SerializeField]
	private List<string> ciolastlist = new List<string>();

	[SerializeField]
	private Transform ciocontent;

	[SerializeField]
	private Transform cioBox;

	[SerializeField]
	private ScrollRect scrollRect;

	[SerializeField]
	private Image gridline;

	[SerializeField]
	private Image zhezhao;

	[SerializeField]
	private List<int> leftpoints = new List<int>();

	[SerializeField]
	private int currentKeypoint;

	[SerializeField]
	private List<DuikangPoint> duikangPoints = new List<DuikangPoint>();

	private string[] attacklabel02 = new string[20]
	{
		"2021-12-25         235.64.187.73         Driord        14279.5Mbps       SYN", "2021-12-25         181.57.201.2          Dreg          14229.2Mbps       UDP&ICMP", "2021-12-25         173.71.217.36         Aridru        14263.7Mbps       SYN", "2021-12-25         218.36.113.32         Brybgar       14121.4Mbps       SYN", "2021-12-25         6.62.155.137          Slutiarm      14036.7Mbps       ACK", "2021-12-25         57.142.42.126         Uyagh         13999.2Mbps       ACK", "2021-12-25         18.47.169.55          Phax          13295Mbps         TCP", "2021-12-25         110.100.45.236        Driord        14279.6Mbps       TCP", "2021-12-25         174.146.253.204       Dreg          14229.3Mbps       UDP&ICMP", "2021-12-25         130.15.200.203        Aridru        14263.8Mbps       SYN",
		"2021-12-25         48.197.204.60         Brybgar       14121.5Mbps       TCP", "2021-12-25         111.96.45.152         Slutiarm      14036.8Mbps       SYN", "2021-12-25         200.25.230.249        Uyagh         13999.3Mbps       ACK", "2021-12-25         22.146.145.174        Phax          13296Mbps         SYN", "2021-12-25         243.47.63.13          Driord        14279.7Mbps       UDP&ICMP", "2021-12-25         72.185.155.235        Dreg          14229.4Mbps       SYN", "2021-12-25         242.102.253.222       Aridru        14263.9Mbps       SYN", "2021-12-25         153.191.20.177        Brybgar       14121.6Mbps       ACK", "2021-12-25         112.120.128.71        Slutiarm      14036.9Mbps       UDP&ICMP", "2021-12-25         130.225.229.113       Uyagh         13999.4Mbps       ACK"
	};

	private string[] attacklabel01 = new string[7] { "^attack_label01", "^attack_label02", "^attack_label03", "^attack_label04", "^attack_label05", "^attack_label06", "^attack_label07" };

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.homeScene.duikangDialog = this;
		gameManager.homeScene.ShowVideoTip("3700084");
		Init5points();
	}

	public void StartSaySomething()
	{
		StartCoroutine(ReadyCio());
	}

	private void Init5points()
	{
		for (int i = 0; i < 5; i++)
		{
			int index = Random.Range(0, leftpoints.Count);
			duikangPoints[leftpoints[index]].gameObject.SetActive(value: true);
			leftpoints.RemoveAt(index);
		}
		int index2 = Random.Range(0, leftpoints.Count);
		currentKeypoint = leftpoints[index2];
	}

	private void Init2points()
	{
		if (leftpoints.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			if (leftpoints.Count > 0)
			{
				int index = Random.Range(0, leftpoints.Count);
				duikangPoints[leftpoints[index]].gameObject.SetActive(value: true);
				leftpoints.RemoveAt(index);
			}
		}
	}

	public void Check(int point)
	{
		bool flag = true;
		if (currentKeypoint != point)
		{
			flag = false;
			Init2points();
		}
		if (flag)
		{
			iscansendhack = false;
			Debug.LogError("成功");
			StopAllCoroutines();
			resultBox.SetActive(value: true);
			StartCoroutine(LastCIO());
		}
	}

	public void Check2()
	{
		bool flag = true;
		for (int i = 0; i < duikangPoints.Count; i++)
		{
			if (!duikangPoints[i].isblue)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			iscansendhack = false;
			Debug.LogError("成功");
			StopAllCoroutines();
			resultBox.SetActive(value: true);
			StartCoroutine(LastCIO());
		}
	}

	private IEnumerator ReadyCio()
	{
		yield return new WaitForSeconds(1f);
		cioBox.DOScaleY(1f, 0.2f);
		yield return new WaitForSeconds(0.2f);
		StartCoroutine(StartCIO());
	}

	private void StartTime()
	{
		count = countdown;
		DOTween.To(() => count, delegate(int x)
		{
			count = x;
		}, 0, countdown).SetEase(Ease.Linear).OnUpdate(delegate
		{
			if (iscansendhack)
			{
				txt_lefttime.text = count.ToString();
			}
		})
			.OnComplete(delegate
			{
				if (iscansendhack)
				{
					Debug.LogError("结束");
					StopAllCoroutines();
					resultBox.SetActive(value: true);
					StartCoroutine(GameFailed());
				}
			});
	}

	private IEnumerator GameFailed()
	{
		resultBox.transform.Find("failed").DOScaleX(1f, 0.3f);
		yield return new WaitForSeconds(2.3f);
		resultBox.transform.Find("failed").DOScaleX(0f, 0.3f);
		yield return new WaitForSeconds(0.6f);
		gameManager.ShowFloatBox();
		yield return new WaitForSeconds(2f);
		failedPanel.SetActive(value: true);
		failedPanel.transform.Find("bakgame").GetComponent<Button>().onClick.AddListener(delegate
		{
			StartCoroutine(ReplayGame());
		});
		failedPanel.transform.Find("bakmain").GetComponent<Button>().onClick.AddListener(delegate
		{
			SceneManager.LoadScene("mainScene");
		});
	}

	private IEnumerator ReplayGame()
	{
		gameManager.ShowFloatBox();
		yield return new WaitForSeconds(2f);
		Object.Instantiate(Resources.Load<GameObject>("Duikang/duikangDialog"), gameManager.homeScene.middle);
		Object.Destroy(base.gameObject);
	}

	private IEnumerator StartCIO()
	{
		for (int i = 0; i < ciostartlist.Count; i++)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("Duikang/txt_cio"), ciocontent);
			float num = gameManager.CalculateLengthOfText(I18N.instance.getValue(ciostartlist[i]), gameObject.GetComponent<Text>());
			gameObject.GetComponent<Text>().DOText(I18N.instance.getValue(ciostartlist[i]), num * 0.002f).SetEase(Ease.Linear);
			yield return new WaitForSeconds(num * 0.002f);
		}
		zhezhao.DOFade(0f, 1f);
		yield return new WaitForSeconds(1f);
		zhezhao.gameObject.SetActive(value: false);
		timebox.DOScaleY(1f, 0.2f);
		yield return new WaitForSeconds(0.2f);
		StartTime();
		StartCoroutine(ShowList01());
		StartCoroutine(ShowList02());
		gridline.DOFillAmount(1f, 0.5f);
	}

	private IEnumerator LastCIO()
	{
		duikangPoints[currentKeypoint].ShowVanSql();
		resultBox.transform.Find("success").DOScaleX(1f, 0.3f);
		yield return new WaitForSeconds(2.3f);
		resultBox.transform.Find("success").DOScaleX(0f, 0.3f).OnComplete(delegate
		{
			resultBox.SetActive(value: false);
		});
		yield return new WaitForSeconds(0.3f);
		for (int i = 0; i < ciolastlist.Count; i++)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("Duikang/txt_cio"), ciocontent);
			float num = gameManager.CalculateLengthOfText(I18N.instance.getValue(ciolastlist[i]), gameObject.GetComponent<Text>());
			gameObject.GetComponent<Text>().DOText(I18N.instance.getValue(ciolastlist[i]), num * 0.005f).SetEase(Ease.Linear);
			float lineNum = Mathf.Ceil(num / 19f);
			for (int j = 0; (float)j < lineNum; j++)
			{
				Canvas.ForceUpdateCanvases();
				scrollRect.normalizedPosition = Vector3.zero;
				Canvas.ForceUpdateCanvases();
				yield return new WaitForSeconds(0.095f);
			}
		}
		duikangPoints[currentKeypoint].CanClick();
	}

	private IEnumerator ShowList01()
	{
		while (true)
		{
			for (int i = 0; i < group1texts.Count; i++)
			{
				int num = Random.Range(0, attacklabel01.Length - 1);
				string key = attacklabel01[num];
				group1texts[i].GetComponent<I18NText>().updateTranslation2(key);
			}
			yield return new WaitForSeconds(0.2f);
		}
	}

	private IEnumerator ShowList02()
	{
		while (true)
		{
			for (int i = 0; i < group2texts.Count; i++)
			{
				int num = Random.Range(0, attacklabel02.Length - 1);
				string key = attacklabel02[num];
				group2texts[i].GetComponent<I18NText>().updateTranslation2(key);
			}
			yield return new WaitForSeconds(0.2f);
		}
	}
}
