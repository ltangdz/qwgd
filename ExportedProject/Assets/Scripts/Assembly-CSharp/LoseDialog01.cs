using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class LoseDialog01 : MonoBehaviour
{
	public Image img_red;

	public Text txt_time;

	public GameObject timePanel;

	public GameObject warningPanel;

	public Animator animator;

	public Transform resultPanel;

	public GameObject bluePanel;

	public GameObject blackPanel;

	public Image img_resultbk;

	public Text txt_second;

	private string lastsecond = "The system is collecting some error info and will restart them after {0:S} seconds.";

	private float CountDownTime = 600f;

	private float GameTime;

	private float timer;

	private bool isstart;

	private bool isshowblue;

	private void Start()
	{
		ShowLose();
	}

	private void Update()
	{
		int num = (int)(GameTime / 60f);
		float num2 = GameTime % 60f;
		if (!isstart)
		{
			return;
		}
		timer += Time.deltaTime;
		if (timer >= 1f / 60f)
		{
			timer = 0f;
			GameTime -= 1f;
			txt_time.GetComponent<I18NText>().updateTranslation2("00:" + $"{num:00}" + ":" + $"{num2:00}");
			if (GameTime <= 0f)
			{
				txt_time.GetComponent<I18NText>().updateTranslation2("00:00:00");
				isstart = false;
				ShowBlue();
			}
		}
	}

	private void ShowLose()
	{
		GameTime = CountDownTime;
		isstart = true;
		animator.Play("ani_losedialogred");
		StartCoroutine(StartShow());
	}

	private IEnumerator StartShow()
	{
		yield return new WaitForSeconds(2f);
		timePanel.transform.DOScale(Vector3.one, 0.3f);
		yield return new WaitForSeconds(0.2f);
		warningPanel.transform.DOScale(Vector3.one, 0.3f);
	}

	private void ShowBlue()
	{
		if (!isshowblue)
		{
			isshowblue = true;
			StartCoroutine(StartHideWarning());
		}
	}

	private IEnumerator StartHideWarning()
	{
		timePanel.transform.DOScale(Vector3.zero, 0.3f);
		yield return new WaitForSeconds(0.2f);
		warningPanel.transform.DOScale(Vector3.zero, 0.3f);
		yield return new WaitForSeconds(0.3f);
		timePanel.gameObject.SetActive(value: false);
		warningPanel.gameObject.SetActive(value: false);
		img_red.gameObject.SetActive(value: false);
		blackPanel.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(1f);
		bluePanel.gameObject.SetActive(value: true);
		blackPanel.gameObject.SetActive(value: false);
		txt_second.GetComponent<I18NText>().updateTranslation2(string.Format(lastsecond, "5"));
		yield return new WaitForSeconds(1f);
		txt_second.GetComponent<I18NText>().updateTranslation2(string.Format(lastsecond, "4"));
		yield return new WaitForSeconds(1f);
		txt_second.GetComponent<I18NText>().updateTranslation2(string.Format(lastsecond, "3"));
		yield return new WaitForSeconds(1f);
		txt_second.GetComponent<I18NText>().updateTranslation2(string.Format(lastsecond, "2"));
		yield return new WaitForSeconds(1f);
		txt_second.GetComponent<I18NText>().updateTranslation2(string.Format(lastsecond, "1"));
		yield return new WaitForSeconds(1f);
		txt_second.GetComponent<I18NText>().updateTranslation2(string.Format(lastsecond, "0"));
		resultPanel.gameObject.SetActive(value: true);
		bluePanel.gameObject.SetActive(value: false);
		img_resultbk.DOColor(Color.white, 1f);
		yield return new WaitForSeconds(1f);
		ShowResult();
	}

	private void ShowResult()
	{
		Object.Instantiate(Resources.Load("Dialog/taskOver") as GameObject, resultPanel);
	}
}
