using System;
using System.Collections;
using System.Globalization;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CatchLoading : MonoBehaviour
{
	public Image _codeRunImage;

	public Image _loadingProgress;

	public Text _contentText;

	public Image _maskImage;

	private bool isEnglish;

	private string[][] contentDataArray = new string[9][]
	{
		new string[5] { "^vdev1001", "4.0", "2.0", "4.5", "5" },
		new string[5] { "^vdev1002", "2.8", "1.4", "1.5", "6" },
		new string[5] { "^vdev1003", "1.7", "0.9", "4.0", "7" },
		new string[5] { "^vdev1004", "3.9", "2.0", "3.0", "8" },
		new string[5] { "^vdev1005", "9.0", "4.5", "1.5", "9" },
		new string[5] { "^vdev1007", "3.6", "1.8", "1.5", "10" },
		new string[5] { "^vdev1006", "5.0", "2.5", "1.5", "11" },
		new string[5] { "^615F7A72-65A1-EA99-497B-09D1E677A3CC", "3.0", "1.5", "3.0", "12" },
		new string[5] { "^1C1FAC13-97A0-1D6E-EB7C-586471F66790", "4.5", "2.3", "1.5", "13" }
	};

	[SerializeField]
	private GameManager gameManager;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_ = _loadingProgress.material;
		if (I18N.instance.gameLang == LanguageCode.EN)
		{
			isEnglish = true;
			contentDataArray = new string[9][]
			{
				new string[5] { "^vdev1001", "4.0", "2.0", "4.5", "5" },
				new string[5] { "^vdev1002", "2.8", "1.4", "1.5", "6" },
				new string[5] { "^vdev1003", "1.7", "0.9", "4.0", "7" },
				new string[5] { "^vdev1004", "3.9", "2.0", "3.0", "8" },
				new string[5] { "^vdev1005", "14.5", "10.5", "1.5", "9" },
				new string[5] { "^vdev1007", "3.6", "1.8", "1.5", "10" },
				new string[5] { "^vdev1006", "7.9", "4.5", "1.5", "11" },
				new string[5] { "^615F7A72-65A1-EA99-497B-09D1E677A3CC", "3.0", "1.5", "3.0", "12" },
				new string[5] { "^1C1FAC13-97A0-1D6E-EB7C-586471F66790", "4.5", "2.3", "1.5", "13" }
			};
		}
	}

	public void Begin()
	{
		gameManager.musicManager.LowerVol();
		_loadingProgress.GetComponent<RectTransform>().DOScaleX(0f, 0f);
		_loadingProgress.material.EnableKeyword("_ClipUvRight");
		_loadingProgress.transform.DOScaleX(0.2f, 4.5f).OnComplete(delegate
		{
			_codeRunImage.GetComponent<CanvasGroup>().DOFade(1f, 0f);
			CatchEvent.Instance.NoticeLoading(CatchLoadingStep.STEP1);
		});
		StartCoroutine("Speak");
	}

	private IEnumerator Speak()
	{
		for (int i = 0; i < contentDataArray.Length; i++)
		{
			string[] array = contentDataArray[i];
			_ = i;
			string text = array[1];
			string text2 = array[2];
			string text3 = array[3];
			string text4 = array[4];
			Debug.Log("----------------------------------");
			Debug.Log(text + ":" + text2 + ":" + text3 + ":" + text4);
			int num = Convert.ToInt32(text4, CultureInfo.InvariantCulture);
			_contentText.DOText("", 0f);
			gameManager.soundManager.PlayEvent(gameManager.player.GetEventId(), num);
			float num2 = Convert.ToSingle(array[1], CultureInfo.InvariantCulture);
			float interval = Convert.ToSingle(array[3], CultureInfo.InvariantCulture);
			float num3 = Convert.ToSingle(array[2], CultureInfo.InvariantCulture);
			Debug.Log(num2 + ":" + num3 + ":" + interval + ":" + num);
			string value = I18N.instance.getValue(array[0]);
			_contentText.DOText(value, num3).SetEase(Ease.Linear);
			yield return new WaitForSeconds(num2);
			_contentText.DOText("", 0f);
			yield return new WaitForSeconds(interval);
		}
	}

	private void NoticeLoading(CatchLoadingStep obj)
	{
		_ = _loadingProgress.material;
		CanvasGroup codeCanvasGroup = _codeRunImage.GetComponent<CanvasGroup>();
		switch (obj)
		{
		case CatchLoadingStep.STEP1_FINISHED:
			codeCanvasGroup.DOFade(0f, 0f);
			_loadingProgress.transform.DOScaleX(0.4f, 8f).OnComplete(delegate
			{
				codeCanvasGroup.DOFade(1f, 0f);
				CatchEvent.Instance.NoticeLoading(CatchLoadingStep.STEP2);
			});
			break;
		case CatchLoadingStep.STEP2_FINISHED:
			codeCanvasGroup.DOFade(0f, 0f);
			_loadingProgress.transform.DOScaleX(0.6f, isEnglish ? 40 : 32).OnComplete(delegate
			{
				codeCanvasGroup.DOFade(1f, 0f);
				CatchEvent.Instance.NoticeLoading(CatchLoadingStep.STEP3);
			});
			break;
		}
		if (obj != CatchLoadingStep.STEP3_FINISHED)
		{
			return;
		}
		codeCanvasGroup.DOFade(0f, 0f);
		_loadingProgress.transform.DOScaleX(1f, 7.5f).OnComplete(delegate
		{
			_maskImage.DOFade(1f, 0.5f).OnComplete(delegate
			{
				SceneManager.LoadScene("CatchScene");
			});
		});
	}

	private void OnEnable()
	{
		CatchEvent.Instance.onNoticeLoading += NoticeLoading;
	}

	private void OnDisable()
	{
		CatchEvent.Instance.onNoticeLoading -= NoticeLoading;
	}
}
