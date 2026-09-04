using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class CatchInfoPanel : MonoBehaviour
{
	public CodeRun1 _codeRun1;

	public GameObject _logoGroup;

	public GameObject _titleGroup;

	public GameObject _timeGroup;

	public Image _avatartGroup;

	public GameObject _pttGroup;

	public GameObject _voiceGroup;

	public GameObject _progressGroup;

	public Button _answerButton;

	public Text _logoText;

	public Text _logoTextEn;

	public Text _titleText;

	public Text _nameText;

	public Text _timeText1;

	public Text _timeText2;

	public Text _timeText3;

	public Text _timerText;

	public Text _pttNameText;

	public Text _pttTimeText;

	private List<Image> soundsImage = new List<Image>();

	private Dictionary<string, Vector3> _objectPositionDic;

	private bool _isSmall;

	private Text _btnText;

	public Image[] _avatars;

	private CatchSpeakRole _speakRole = CatchSpeakRole.NO;

	private float _speakTotalTime;

	private bool _isSpeak;

	private int hour;

	private int minute;

	private int second;

	private bool isGameOver;

	private bool isStart;

	private float timeSpend;

	private GameManager gameManager;

	private void Start()
	{
	}

	private void ChangeSound()
	{
		for (int i = 0; i < soundsImage.Count; i++)
		{
			float endValue = (_isSpeak ? Random.Range(0.2f, 0.8f) : Random.Range(0.1f, 0.13f));
			soundsImage[i].transform.DOScaleY(endValue, 0.2f).SetEase(Ease.OutBounce);
		}
	}

	public void Show()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_answerButton.GetComponent<Button>().interactable = false;
		_btnText = _answerButton.GetComponentInChildren<Text>();
		_btnText.color = Color.gray;
		Image[] componentsInChildren = _voiceGroup.GetComponentsInChildren<Image>();
		soundsImage = new List<Image>(componentsInChildren);
		for (int i = 0; i < soundsImage.Count; i++)
		{
			soundsImage[i].transform.DOScaleY(0.1f, 0f);
		}
		base.transform.GetComponent<CanvasGroup>().DOFade(1f, 1f).OnComplete(delegate
		{
			StartCoroutine(ShowChildren());
		});
		_answerButton.onClick.AddListener(delegate
		{
			_answerButton.GetComponent<Button>().interactable = false;
			gameManager.soundManager.audiosource.Stop();
			gameManager.soundManager.audiosourceloop.Stop();
			_btnText.color = Color.gray;
			DOTween.Kill("CatchInfoCallSequence");
			CatchEvent.Instance.NoticeNextEvent(CatchEventEnum.SHOW_STEP2);
		});
	}

	private IEnumerator ShowChildren()
	{
		float speed = 0.6f;
		_logoGroup.transform.GetComponent<RectTransform>().DOAnchorPos(_objectPositionDic["logo"], 0.15f).OnComplete(delegate
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(_logoText.DOText(I18N.instance.getValue("^F0C18AAB-5E34-08EA-3F38-371835DB2702"), speed));
			sequence.Append(_logoTextEn.DOText("ALLIVIA NATIONAL SECURITY AGENCY", speed));
			sequence.Play();
		});
		yield return new WaitForSeconds(0.15f);
		_titleGroup.transform.GetComponent<RectTransform>().DOAnchorPos(_objectPositionDic["title"], 0.15f).SetEase(Ease.OutBack)
			.OnComplete(delegate
			{
				_titleText.DOText(I18N.instance.getValue("^7909D877-53FF-DA8F-0A5B-297F9039F549"), speed);
			});
		yield return new WaitForSeconds(0.15f);
		_timeGroup.transform.GetComponent<RectTransform>().DOAnchorPos(_objectPositionDic["time"], 0.15f).OnComplete(delegate
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(_timeText1.DOText(I18N.instance.getValue("^BF583E35-3297-39D8-B83F-D5C96719211A"), speed));
			sequence.Append(_timeText2.DOText("10  1.01", speed));
			sequence.Append(_timeText3.DOText("", speed));
			sequence.Append(_timerText.DOText("00:00:00", speed));
			sequence.Play();
		});
		yield return new WaitForSeconds(0.08f);
		_avatartGroup.material.DOFloat(0f, "_FadeAmount", 1f);
		yield return new WaitForSeconds(0.08f);
		_pttGroup.transform.GetComponent<RectTransform>().DOAnchorPos(_objectPositionDic["ptt"], 0.15f).OnComplete(delegate
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(_pttNameText.DOText(I18N.instance.getValue("^0A5E421D-3433-7C36-7B65-0CA24F68AC47"), speed));
			sequence.Append(_pttTimeText.DOText("10  1.01", speed));
			sequence.Play();
		});
		yield return new WaitForSeconds(0.08f);
		_voiceGroup.transform.GetComponent<RectTransform>().DOAnchorPos(_objectPositionDic["voice"], 0.15f).OnComplete(delegate
		{
			InvokeRepeating("ChangeSound", 1f, 0.2f);
		});
		yield return new WaitForSeconds(0.08f);
		_answerButton.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
		yield return new WaitForSeconds(0.08f);
		_progressGroup.GetComponent<RectTransform>().DOAnchorPos(_objectPositionDic["progress"], 0.15f);
		yield return new WaitForSeconds(0.08f);
		_codeRun1.transform.GetComponent<RectTransform>().DOAnchorPos(_objectPositionDic["code_run"], 0.08f);
		yield return new WaitForSeconds(1f);
		_codeRun1.StartRun();
		CatchEvent.Instance.NoticeShowSearch();
	}

	private void Awake()
	{
		_objectPositionDic = new Dictionary<string, Vector3>();
		_objectPositionDic.Add("logo", new Vector2(-115f, -47.5f));
		_objectPositionDic.Add("title", new Vector2(0f, 391f));
		_objectPositionDic.Add("avatar", new Vector2(-208.8f, -302f));
		_objectPositionDic.Add("time", new Vector2(86.87492f, 239f));
		_objectPositionDic.Add("ptt", new Vector2(0f, 107f));
		_objectPositionDic.Add("voice", new Vector2(0f, 529f));
		_objectPositionDic.Add("answer_btn", new Vector2(0f, -56f));
		_objectPositionDic.Add("code_run", new Vector2(0f, -340f));
		_objectPositionDic.Add("progress", new Vector2(10f, -145.3f));
	}

	private void Update()
	{
		if (!isGameOver && isStart)
		{
			timeSpend += Time.deltaTime;
			hour = (int)timeSpend / 3600;
			minute = ((int)timeSpend - hour * 3600) / 60;
			second = (int)timeSpend - hour * 3600 - minute * 60;
			_timerText.text = $"{hour:D2}:{minute:D2}:{second:D2}";
		}
	}

	private void NoticeSpeak(CatchSpeakRole arg1, float arg2)
	{
		_isSpeak = true;
		Image image = _avatars[0];
		Image image2 = _avatars[1];
		Material material = image.material;
		Material material2 = image2.material;
		if (_speakRole != arg1)
		{
			if (arg1 == CatchSpeakRole.LISA)
			{
				image.DOFade(1f, 0f);
				image2.DOFade(0f, 1f);
				material.DOFloat(0f, "_FadeAmount", 0.5f);
				material2.DOFloat(1f, "_FadeAmount", 0f);
			}
			else
			{
				image2.DOFade(1f, 0f);
				image.DOFade(0f, 1f);
				material2.DOFloat(0f, "_FadeAmount", 0.5f);
				material.DOFloat(1f, "_FadeAmount", 0f);
			}
		}
		_speakRole = arg1;
		Invoke("Stop", arg2);
	}

	private void Stop()
	{
		_isSpeak = false;
	}

	private void OnEnable()
	{
		CatchEvent.Instance.onNoticeStart += NoticeStart;
		CatchEvent.Instance.onNoticeNextEvent += NoticeNextEvent;
		CatchEvent.Instance.OnNoticeSpeak += NoticeSpeak;
	}

	private void NoticeNextEvent(CatchEventEnum obj)
	{
		if (obj == CatchEventEnum.SHOW_CALL)
		{
			gameManager.soundManager.PlaySoundLoop(49);
			ShowCall();
		}
		if (obj == CatchEventEnum.GAME_SUCCESS)
		{
			isGameOver = true;
			if (timeSpend <= 180f)
			{
				gameManager.UnlockAchievements("catchharris");
			}
		}
	}

	private void OnDisable()
	{
		CatchEvent.Instance.onNoticeStart -= NoticeStart;
		CatchEvent.Instance.onNoticeNextEvent -= NoticeNextEvent;
		CatchEvent.Instance.OnNoticeSpeak -= NoticeSpeak;
	}

	private void NoticeStart()
	{
		isStart = true;
		DOTween.Kill("CatchInfoCallSequence");
	}

	public void Win()
	{
		ShowCall();
	}

	public void ShowCall()
	{
		_answerButton.interactable = true;
		_answerButton.GetComponentInChildren<Text>().color = Color.white;
		Sequence sequence = DOTween.Sequence();
		sequence.SetId("CatchInfoCallSequence");
		sequence.Append(_answerButton.transform.DOScale(1.05f, 0.6f));
		sequence.Append(_answerButton.transform.DOScale(1f, 0.6f));
		sequence.SetLoops(-1);
		sequence.Play();
	}
}
