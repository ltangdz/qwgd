using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using Honeti;
using Spine.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CatchEnd : MonoBehaviour
{
	public Text _contentText;

	public Image _topImage;

	public Image _bottomImage;

	public Button _selectButton;

	public Text _selectText;

	public GameObject[] _spine1Renders;

	public GameObject _spine1;

	public GameObject _spine2;

	public GameObject _spine3;

	public GameObject[] _hiddenObjs;

	public GameObject _castGroup;

	public List<GameObject> _casts;

	public GameObject _endTipGroup;

	public Text _endTipText1;

	public Text _endTipText2;

	public CastGroupDlc _castGroupDlc;

	private bool _isStart;

	private int _policeSpeakIndex;

	private bool isEnglish;

	private string[][] curSpeakDatas;

	private int curStep;

	private string[][] videoStr1 = new string[5][]
	{
		new string[5] { "", "1.0", "0.0", "0.0", "" },
		new string[5] { "^vdev1024", "4.071", "0.928", "2.0", "" },
		new string[5] { "^vdev1025", "0.901", "0.549", "1.0", "" },
		new string[5] { "^vdev1026", "1.946", "1.923", "2.0", "" },
		new string[5] { "^vdev1027", "1.259", "9.597", "2.5", "" }
	};

	private string[][] videoStr0 = new string[2][]
	{
		new string[5] { "^F9EF25D8-A747-D131-DEAD-2CBAD5139733", "3.0", "1.0", "1.0", "3" },
		new string[5] { "^9203011B-EDF6-B641-4D9B-757B2576384C", "6.0", "1.0", "3.0", "4" }
	};

	private GameManager gameManager;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
		{
			isEnglish = false;
		}
		else if (I18N.instance.gameLang == LanguageCode.EN)
		{
			isEnglish = true;
			videoStr1 = new string[7][]
			{
				new string[5] { "", "0.915", "0.0", "0.0", "" },
				new string[5] { "^vdev1024", "3.883", "0.687", "2.0", "" },
				new string[5] { "^vdev1025", "1.831", "0.096", "1.0", "" },
				new string[5] { "^vdev1026", "1.816", "0.0", "2.0", "" },
				new string[5] { "", "2.85", "0.0", "0.0", "" },
				new string[5] { "^vdev1027", "1.255", "0.0", "2.5", "" },
				new string[5] { "", "8.66", "0.0", "0.0", "" }
			};
			videoStr0 = new string[2][]
			{
				new string[5] { "^F9EF25D8-A747-D131-DEAD-2CBAD5139733", "3.0", "1.0", "1.0", "3" },
				new string[5] { "^9203011B-EDF6-B641-4D9B-757B2576384C", "6.0", "1.0", "3.0", "4" }
			};
		}
	}

	public void Begin()
	{
		if (_isStart)
		{
			return;
		}
		_isStart = true;
		Sequence t = DOTween.Sequence();
		GetComponent<CanvasGroup>().DOFade(1f, 0.5f).OnComplete(delegate
		{
			_topImage.GetComponent<RectTransform>().DOAnchorPosY(0f, 0.5f);
			_bottomImage.GetComponent<RectTransform>().DOAnchorPosY(0f, 0.5f).OnComplete(delegate
			{
				FirstSpeak();
			});
		});
		t.Play();
	}

	private IEnumerator PoliceCall()
	{
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		_selectButton.onClick.AddListener(ShowPoliceDialog);
		for (int i = 0; i < videoStr0.Length; i++)
		{
			Debug.Log("PoliceCall");
			string[] array = videoStr0[i];
			string obj = array[1];
			string text = array[4];
			Debug.Log(obj + ":" + text);
			float num = Convert.ToSingle(obj, CultureInfo.InvariantCulture);
			_contentText.DOText("", 0f);
			gameManager.soundManager.PlayEvent("110006", Convert.ToInt32(text, CultureInfo.InvariantCulture));
			CatchEvent.Instance.NoticeSpeak(CatchSpeakRole.POLICE, num);
			_contentText.DOText(I18N.instance.getValue(array[0]), num / 2f);
			yield return new WaitForSeconds(num);
		}
		ShowPoliceDialog();
		_selectButton.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
		_selectButton.GetComponent<RectTransform>().DOAnchorPosY(-345f, 0.5f).OnComplete(delegate
		{
			_selectButton.interactable = true;
		});
	}

	private void ShowPoliceDialog()
	{
		string[] array = new string[3]
		{
			"......",
			"...... ......",
			I18N.instance.getValue("^6A5CAE05-C583-876F-239D-221308B7A9A2")
		};
		if (_policeSpeakIndex == array.Length)
		{
			_selectButton.interactable = false;
			_selectButton.GetComponent<CanvasGroup>().DOFade(0f, 0f);
			_selectButton.GetComponent<RectTransform>().DOAnchorPosY(-611f, 0f);
			Invoke("ShowStep2", 1f);
		}
		else
		{
			_selectText.text = array[_policeSpeakIndex];
		}
		_policeSpeakIndex++;
	}

	private void FirstSpeak()
	{
		Debug.Log("firstSpeak");
		gameManager.musicManager.Stop();
		gameManager.soundManager.audiosource.Stop();
		gameManager.soundManager.audiosourceloop.Stop();
		gameManager.soundManager.catchsourceloop.Stop();
		StopAllSound();
		gameManager.soundManager.PlayCatch(isEnglish ? 9 : 3);
		curSpeakDatas = videoStr1;
		curStep = 1;
		StartCoroutine(SpeakIEnumerator());
	}

	private void StopAllSound()
	{
		gameManager.soundManager.audiosource.Stop();
		gameManager.soundManager.audiosourceloop.Stop();
	}

	private IEnumerator SpeakIEnumerator()
	{
		Debug.Log("speak" + curStep);
		int spine = -1;
		if (curStep == 3)
		{
			spine = 1;
		}
		else if (curStep == 4)
		{
			spine = 2;
		}
		else if (curStep == 5)
		{
			spine = 3;
		}
		for (int i = 0; i < curSpeakDatas.Length; i++)
		{
			string[] array = curSpeakDatas[i];
			int num = i;
			string text = array[1];
			string text2 = array[2];
			string text3 = array[3];
			string text4 = array[4];
			Debug.Log(text + ":" + text2 + ":" + text3 + ":" + text4);
			float time = Convert.ToSingle(text, CultureInfo.InvariantCulture);
			int num2 = -1;
			if (!string.IsNullOrEmpty(text4))
			{
				num2 = Convert.ToInt32(text4, CultureInfo.InvariantCulture);
			}
			float num3 = Convert.ToSingle(text, CultureInfo.InvariantCulture);
			float interval = Convert.ToSingle(text2, CultureInfo.InvariantCulture);
			float num4 = Convert.ToSingle(text3, CultureInfo.InvariantCulture);
			Debug.Log(num3 + ":" + interval + ":" + num4 + ":" + num2);
			string endValue = "";
			if (array[0].StartsWith("^"))
			{
				endValue = I18N.instance.getValue(array[0]);
			}
			_contentText.DOText("", 0f);
			if (num2 != -1)
			{
				StopAllSound();
				gameManager.soundManager.PlayEvent(gameManager.player.GetEventId(), num2);
			}
			if (spine == -1)
			{
				CatchEvent.Instance.NoticeSpeak(CatchSpeakRole.LISA, time);
			}
			else
			{
				if (spine == 2 && num == curSpeakDatas.Length - 2)
				{
					Invoke("ChangeMusic", isEnglish ? 5.75f : 6.45f);
				}
				if (spine == 2 && num == curSpeakDatas.Length - 1)
				{
					Debug.Log(222);
					SkeletonGraphic component = _spine2.GetComponent<SkeletonGraphic>();
					component.AnimationState.ClearTracks();
					component.AnimationState.SetAnimation(0, "2man2-introduce2-all", loop: false);
				}
				else if (spine == 3)
				{
					if (num != 0)
					{
						StartSpeakAnimation(spine);
					}
				}
				else
				{
					StartSpeakAnimation(spine);
				}
			}
			_contentText.DOText(endValue, num4).SetEase(Ease.Linear);
			yield return new WaitForSeconds(num3);
			_contentText.DOText("", 0f);
			if (spine != -1)
			{
				StopSpeakAnimation(spine);
			}
			yield return new WaitForSeconds(interval);
		}
		_contentText.DOText("", 0f);
		if (curStep == 5)
		{
			SkeletonAnimation component2 = _spine3.GetComponent<SkeletonAnimation>();
			component2.AnimationState.TimeScale = 1f;
			component2.AnimationState.ClearTracks();
			component2.AnimationState.SetAnimation(0, "3then-breath-all", loop: true);
			step5Finish();
		}
		else
		{
			StepFinished(curStep);
		}
	}

	private void Speak(string[][] dataArray, int step)
	{
		Debug.Log("speak" + step);
		Sequence sequence = DOTween.Sequence();
		int spine = -1;
		if (step == 3)
		{
			spine = 1;
		}
		else if (step == 4)
		{
			spine = 2;
		}
		else if (step == 5)
		{
			spine = 3;
		}
		for (int i = 0; i < dataArray.Length; i++)
		{
			string[] array = dataArray[i];
			int i2 = i;
			float time = float.Parse(array[1]);
			int index = -1;
			if (!string.IsNullOrEmpty(array[4]))
			{
				index = int.Parse(array[4]);
			}
			float num = float.Parse(array[1]);
			float interval = float.Parse(array[2]);
			float num2 = float.Parse(array[3]);
			string endValue = "";
			if (array[0].StartsWith("^"))
			{
				endValue = I18N.instance.getValue(array[0]);
			}
			sequence.Append(_contentText.DOText("", 0f).OnComplete(delegate
			{
				if (index != -1)
				{
					StopAllSound();
					gameManager.soundManager.PlayEvent(gameManager.player.GetEventId(), index);
				}
				if (spine == -1)
				{
					CatchEvent.Instance.NoticeSpeak(CatchSpeakRole.LISA, time);
				}
				else
				{
					if (spine == 2 && i2 == dataArray.Length - 2)
					{
						Invoke("ChangeMusic", isEnglish ? 5.75f : 6.45f);
					}
					if (spine == 2 && i2 == dataArray.Length - 1)
					{
						Debug.Log(222);
						SkeletonGraphic component = _spine2.GetComponent<SkeletonGraphic>();
						component.AnimationState.ClearTracks();
						component.AnimationState.SetAnimation(0, "2man2-introduce2-all", loop: false);
					}
					else if (spine == 3)
					{
						if (i2 != 0)
						{
							StartSpeakAnimation(spine);
						}
					}
					else
					{
						StartSpeakAnimation(spine);
					}
				}
			}));
			sequence.Append(_contentText.DOText(endValue, num2).SetEase(Ease.Linear));
			sequence.AppendInterval(num - num2);
			sequence.Append(_contentText.DOText("", 0f).OnComplete(delegate
			{
				if (spine != -1)
				{
					StopSpeakAnimation(spine);
				}
			}));
			sequence.AppendInterval(interval);
		}
		sequence.Append(_contentText.DOText("", 0f).OnComplete(delegate
		{
			if (step == 5)
			{
				SkeletonAnimation component = _spine3.GetComponent<SkeletonAnimation>();
				component.AnimationState.TimeScale = 1f;
				component.AnimationState.ClearTracks();
				component.AnimationState.SetAnimation(0, "3then-breath-all", loop: true);
				step5Finish();
			}
			else
			{
				StepFinished(step);
			}
		}));
		sequence.Play();
	}

	private void ChangeMusic()
	{
		gameManager.musicManager.PlayMusic(21);
	}

	private void step5Finish()
	{
		SkeletonAnimation component = _spine3.GetComponent<SkeletonAnimation>();
		component.AnimationState.TimeScale = 1f;
		component.AnimationState.ClearTracks();
		component.AnimationState.SetAnimation(0, "4end-smile-all", loop: false);
		Sequence sequence = DOTween.Sequence();
		sequence.AppendInterval(2f);
		sequence.OnComplete(delegate
		{
			Vector2 sizeDelta = _topImage.GetComponent<RectTransform>().sizeDelta;
			_topImage.GetComponent<RectTransform>().DOSizeDelta(new Vector2(sizeDelta.x, 960f), 0.4f);
			_bottomImage.GetComponent<RectTransform>().DOSizeDelta(new Vector2(sizeDelta.x, 960f), 0.4f).OnComplete(delegate
			{
				StepFinished(5);
			});
		});
		sequence.Play();
	}

	private void StartSpeakAnimation(int spine)
	{
		switch (spine)
		{
		case 2:
		{
			SkeletonGraphic component2 = _spine2.GetComponent<SkeletonGraphic>();
			component2.AnimationState.ClearTracks();
			component2.AnimationState.SetAnimation(0, "1speak-all", loop: true);
			break;
		}
		case 3:
		{
			SkeletonAnimation component = _spine3.GetComponent<SkeletonAnimation>();
			component.AnimationState.ClearTracks();
			component.AnimationState.SetAnimation(0, "2then-talk-all", loop: true);
			break;
		}
		}
	}

	private void StopSpeakAnimation(int spine)
	{
		if (spine == 2)
		{
			_spine2.GetComponent<SkeletonGraphic>().AnimationState.ClearTracks();
		}
		if (spine == 3)
		{
			SkeletonAnimation component = _spine3.GetComponent<SkeletonAnimation>();
			component.AnimationState.ClearTracks();
			component.AnimationState.SetAnimation(0, "3then-breath-and—wink", loop: true);
		}
	}

	private void StepFinished(int step)
	{
		Debug.Log(step + "完成了");
		if (step == 1)
		{
			GetComponent<CanvasGroup>().blocksRaycasts = false;
			CatchEvent.Instance.NoticeSpeak(CatchSpeakRole.POLICE, 0f);
			CatchEvent.Instance.NoticeNextEvent(CatchEventEnum.SHOW_CALL);
		}
		if (step == 2)
		{
			GetComponent<Image>().DOFade(1f, 1.5f).OnComplete(delegate
			{
				gameManager.soundManager.PlaySound(51);
				Invoke("ShowStep3", 3f);
			});
		}
		if (step == 3)
		{
			GetComponent<Image>().DOFade(0f, 0f).OnComplete(ShowStep4);
		}
		if (step == 4)
		{
			ShowStep5();
		}
		if (step == 5)
		{
			Invoke("ShowStep6", 2f);
		}
	}

	private void ShowStep2()
	{
		string[][] array = ((!isEnglish) ? new string[20][]
		{
			new string[5] { "", "1.0", "0.0", "0.0", "" },
			new string[5] { "^B4E86940-BDB2-D7CE-B489-A65DEAABDDA7", "1.63", "0.974", "1.0", "" },
			new string[5] { "^B547D24C-4608-862E-7F8F-F271B8790430", "1.499", "0.684", "1.0", "" },
			new string[5] { "^B29F0CEB-1024-5F9C-7DA5-D5307C0A7B39", "2.34", "0.947", "1.0", "" },
			new string[5] { "^3C74B7A6-C2FC-D5A5-B712-727A78240082", "6.467", "1.24", "1.0", "" },
			new string[5] { "^D6893DF6-26AE-16E5-428F-A17E88491FB7", "6.996", "1.973", "1.0", "" },
			new string[5] { "^18DD6CD8-1BB7-08C6-D237-C06E6B5CD97B", "3.814", "0.21", "1.0", "" },
			new string[5] { "^DC275488-195A-E243-1097-289DA5DD21BC", "3.874", "0.387", "1.0", "" },
			new string[5] { "^44C3C76F-7E12-9ED9-602E-BEAB14A37B22", "5.655", "1.104", "1.0", "" },
			new string[5] { "^FA79562F-1CCB-7CFA-4D04-39AD585880CF", "3.175", "0.56", "1.0", "" },
			new string[5] { "^D9762C0B-449B-1804-A2BA-1D099F58F5D1", "7.181", "1.367", "1.0", "" },
			new string[5] { "^37747B72-0C75-3067-570B-CC58345A7CD0", "3.078", "0.184", "1.0", "" },
			new string[5] { "^C33E739D-6045-EA72-3121-0B091CC07940", "4.555", "0.657", "1.0", "" },
			new string[5] { "^C76A3AA4-8ACA-B057-8772-D43315B7F733", "4.547", "0.898", "1.0", "" },
			new string[5] { "^44DAE2A2-EDB1-0093-DDB4-D96E28DC52A9", "2.42", "9.09", "1.0", "" },
			new string[5] { "^DD3D04AB-4156-7577-D32F-23B3CFE6DE39", "1.641", "0.71", "1.0", "" },
			new string[5] { "^9E1D694C-0930-ADD6-351D-04229174233A", "2.025", "3.577", "1.0", "" },
			new string[5] { "", "3.683", "0.341", "1.0", "" },
			new string[5] { "^6482C9B2-9E17-FF3B-CEA1-86ABEAEE8ABE", "0.605", "0.88", "1.0", "" },
			new string[5] { "", "3.0", "0.88", "1.0", "" }
		} : new string[19][]
		{
			new string[5] { "", "1.191", "0.0", "0.0", "" },
			new string[5] { "^B29F0CEB-1024-5F9C-7DA5-D5307C0A7B39", "3.062", "1.078", "3.0", "" },
			new string[5] { "^3C74B7A6-C2FC-D5A5-B712-727A78240082", "6.58", "0.0", "6.0", "" },
			new string[5] { "", "0.893", "0.0", "0.0", "" },
			new string[5] { "^D6893DF6-26AE-16E5-428F-A17E88491FB7", "6.466", "0.766", "6.0", "" },
			new string[5] { "^18DD6CD8-1BB7-08C6-D237-C06E6B5CD97B", "4.792", "0.199", "4.0", "" },
			new string[5] { "^DC275488-195A-E243-1097-289DA5DD21BC", "4.325", "0.042", "4.0", "" },
			new string[5] { "^44C3C76F-7E12-9ED9-602E-BEAB14A37B22", "6.112", "0.0", "6.0", "" },
			new string[5] { "^FA79562F-1CCB-7CFA-4D04-39AD585880CF", "1.957", "0.0", "1.0", "" },
			new string[5] { "^D9762C0B-449B-1804-A2BA-1D099F58F5D1", "3.204", "0.0", "2.0", "" },
			new string[5] { "^37747B72-0C75-3067-570B-CC58345A7CD0", "4.951", "0.0", "4.0", "" },
			new string[5] { "^C33E739D-6045-EA72-3121-0B091CC07940", "4.197", "0.0", "3.9", "" },
			new string[5] { "^C76A3AA4-8ACA-B057-8772-D43315B7F733", "3.515", "0.0", "3.0", "" },
			new string[5] { "^44DAE2A2-EDB1-0093-DDB4-D96E28DC52A9", "1.389", "0.0", "1.0", "" },
			new string[5] { "^DD3D04AB-4156-7577-D32F-23B3CFE6DE39", "4.069", "2.278", "2.0", "" },
			new string[5] { "^9E1D694C-0930-ADD6-351D-04229174233A", "3.516", "0.539", "2.0", "" },
			new string[5] { "", "3.63", "0.298", "1.0", "" },
			new string[5] { "^6482C9B2-9E17-FF3B-CEA1-86ABEAEE8ABE", "1.39", "0.0", "1.0", "" },
			new string[5] { "", "0.964", "0.0", "1.0", "" }
		});
		StopAllSound();
		gameManager.soundManager.PlayCatch(isEnglish ? 10 : 4);
		curSpeakDatas = array;
		curStep = 2;
		StartCoroutine(SpeakIEnumerator());
	}

	private void ShowStep3()
	{
		StopAllSound();
		gameManager.soundManager.catchsourceloop.clip = gameManager.soundManager.sounds[51];
		gameManager.soundManager.catchsourceloop.Play();
		for (int i = 0; i < _hiddenObjs.Length; i++)
		{
			_hiddenObjs[i].SetActive(value: false);
		}
		SkeletonDataAsset skeletonDataAsset = Resources.Load("_DLC/spine/I_conference_animation_chip_SkeletonData", typeof(SkeletonDataAsset)) as SkeletonDataAsset;
		_spine1.GetComponent<SkeletonGraphic>().skeletonDataAsset = skeletonDataAsset;
		_spine1.GetComponent<SkeletonGraphic>().Initialize(overwrite: true);
		_spine1.GetComponent<SkeletonGraphic>().initialSkinName = "defaultSkin";
		SkeletonDataAsset skeletonDataAsset2 = Resources.Load("_DLC/spine/I_introduce_animation_chip_SkeletonData", typeof(SkeletonDataAsset)) as SkeletonDataAsset;
		_spine2.GetComponent<SkeletonGraphic>().skeletonDataAsset = skeletonDataAsset2;
		_spine2.GetComponent<SkeletonGraphic>().Initialize(overwrite: true);
		_spine2.GetComponent<SkeletonGraphic>().initialSkinName = "defaultSkin";
		SkeletonDataAsset skeletonDataAsset3 = Resources.Load("_DLC/spine/I_talk_animation_chip_SkeletonData", typeof(SkeletonDataAsset)) as SkeletonDataAsset;
		_spine3.GetComponent<SkeletonAnimation>().skeletonDataAsset = skeletonDataAsset3;
		_spine3.GetComponent<SkeletonAnimation>().Initialize(overwrite: true);
		_spine3.GetComponent<SkeletonAnimation>().initialSkinName = "defaultSkin";
		_spine1.SetActive(value: true);
		_spine2.SetActive(value: false);
		_spine3.SetActive(value: false);
		_castGroupDlc.gameObject.SetActive(value: true);
		_endTipGroup.gameObject.SetActive(value: true);
		string[][] array = ((!isEnglish) ? new string[5][]
		{
			new string[5] { "^19B2FFF0-F6AE-5905-FB75-36EF50A8A63E", "5.2", "0.5", "2.5", "14" },
			new string[5] { "^57C90BAE-F1D6-3A1E-ACB7-92E460A4DBF0", "4.0", "1.0", "2.0", "15" },
			new string[5] { "^2A7D36C5-6C59-46CC-B34E-41976409DD02", "5.0", "3.0", "3.0", "16" },
			new string[5] { "^D7FC6AF8-C168-E037-B4A5-302104BEE61F", "8.2", "1.5", "4.0", "17" },
			new string[5] { "^9B97427E-5A4B-2F7C-68BE-696353DEFB53", "7.5", "1.0", "4.0", "18" }
		} : new string[5][]
		{
			new string[5] { "^19B2FFF0-F6AE-5905-FB75-36EF50A8A63E", "5.2", "0.5", "2.5", "14" },
			new string[5] { "^57C90BAE-F1D6-3A1E-ACB7-92E460A4DBF0", "7.0", "0.5", "3.0", "15" },
			new string[5] { "^2A7D36C5-6C59-46CC-B34E-41976409DD02", "10", "0.5", "5.0", "16" },
			new string[5] { "^D7FC6AF8-C168-E037-B4A5-302104BEE61F", "9.5", "0.3", "5.0", "17" },
			new string[5] { "^9B97427E-5A4B-2F7C-68BE-696353DEFB53", "7.5", "0.7", "4.0", "18" }
		});
		float endValue = (float)Screen.width * 0.04f * -1f;
		Debug.Log(Screen.width);
		_spine1Renders[0].transform.DOMoveX(endValue, 20f).SetEase(Ease.Linear);
		_spine1Renders[1].transform.DOMoveX(endValue, 20f).SetEase(Ease.Linear);
		_spine1Renders[2].transform.DOScale(1.05f, 39f);
		_spine1Renders[3].transform.DOScale(1.05f, 39f);
		curSpeakDatas = array;
		curStep = 3;
		StartCoroutine(SpeakIEnumerator());
		StartCoroutine("ShowPicture");
	}

	private IEnumerator ShowPicture()
	{
		TakePictureAnimation(1, 1);
		int count = 4;
		while (count > 0)
		{
			count--;
			yield return new WaitForSeconds(2f);
			TakePictureAnimation(1, 1);
			yield return new WaitForSeconds(2f);
			TakePictureAnimation(1, 1);
			yield return new WaitForSeconds(UnityEngine.Random.Range(4, 7));
		}
	}

	private void TakePictureAnimation(int spineNumber, int times)
	{
		if (spineNumber == 1)
		{
			Sequence sequence = DOTween.Sequence();
			Material material = _spine1.GetComponent<SkeletonGraphic>().material;
			sequence.Append(material.DOFloat(0.4f, "_Glow", UnityEngine.Random.Range(0.2f, 0.3f)));
			sequence.Append(material.DOFloat(0f, "_Glow", 0.1f));
			sequence.Append(material.DOFloat(0.3f, "_Glow", UnityEngine.Random.Range(0.1f, 0.15f)));
			sequence.Append(material.DOFloat(0f, "_Glow", 0.05f));
			sequence.Append(material.DOFloat(0.3f, "_Glow", UnityEngine.Random.Range(0.05f, 0.1f)));
			sequence.Append(material.DOFloat(0f, "_Glow", 0.05f));
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				sequence.Append(material.DOFloat(0.3f, "_Glow", UnityEngine.Random.Range(0.05f, 0.1f)));
				sequence.Append(material.DOFloat(0f, "_Glow", 0.05f));
				sequence.Append(material.DOFloat(0.3f, "_Glow", UnityEngine.Random.Range(0.05f, 0.1f)));
				sequence.Append(material.DOFloat(0f, "_Glow", 0.05f));
			}
			sequence.Play();
		}
	}

	private void ShowStep4()
	{
		gameManager.soundManager.catchsourceloop.Stop();
		_spine1.SetActive(value: false);
		_spine2.SetActive(value: true);
		_spine3.SetActive(value: false);
		string[][] array = ((!isEnglish) ? new string[3][]
		{
			new string[5] { "^32BF98D4-D372-24EA-90DE-A214B51F1232", "7.8", "0.2", "4.0", "19" },
			new string[5] { "^F4CE0F97-F6F0-956A-9C99-31F3792573B4", "7.9", "0.2", "4.0", "20" },
			new string[5] { "^C8E0EAE2-B504-0FEE-25FA-50303785B6BE", "3.0", "0.0", "2.0", "21" }
		} : new string[3][]
		{
			new string[5] { "^32BF98D4-D372-24EA-90DE-A214B51F1232", "7.9", "0.2", "4.0", "19" },
			new string[5] { "^F4CE0F97-F6F0-956A-9C99-31F3792573B4", "7.9", "0.0", "4.0", "20" },
			new string[5] { "^C8E0EAE2-B504-0FEE-25FA-50303785B6BE", "2.3", "0.0", "2.0", "21" }
		});
		_ = Screen.width;
		curSpeakDatas = array;
		curStep = 4;
		StartCoroutine(SpeakIEnumerator());
	}

	private void ShowStep5()
	{
		_spine1.SetActive(value: false);
		_spine2.SetActive(value: false);
		_spine3.SetActive(value: true);
		_spine3.transform.DOMoveY(-20.46f, 34.4f).SetEase(Ease.Linear);
		Camera.main.DOOrthoSize(5.9f, 34.4f).SetEase(Ease.Linear);
		_contentText.DOText("", 0f);
		gameManager.soundManager.PlayEvent(gameManager.player.GetEventId(), 22);
		Invoke("Speak5Step", 1.9f);
	}

	private void ShowStep6()
	{
		_castGroupDlc.Begin();
	}

	private IEnumerator ShowEndTip()
	{
		_endTipGroup.GetComponent<Image>().DOFade(1f, 3f);
		yield return new WaitForSeconds(3f);
		_endTipText1.DOFade(0f, 0f);
		_endTipText2.DOFade(0f, 0f);
		_endTipText1.text = I18N.instance.getValue("^EE517C56-8313-CFEA-3FA6-E488532459AD");
		_endTipText2.text = I18N.instance.getValue("^8B97F605-65E0-527C-1C74-EFB509C1671B");
		yield return new WaitForSeconds(2f);
		_endTipText1.DOFade(1f, 2f);
		_endTipText2.DOFade(1f, 2f);
		yield return new WaitForSeconds(12f);
		_endTipText1.DOFade(0f, 2f);
		_endTipText2.DOFade(0f, 2f);
		yield return new WaitForSeconds(3f);
		Cursor.visible = true;
		gameManager.musicManager.Stop();
		if (gameManager.Esc != null)
		{
			gameManager.Esc.SetActive(value: false);
		}
		gameManager.musicManager.PlayMusicLoop(8);
		gameManager.txt_studio.SetActive(value: true);
		SceneManager.LoadScene("home");
	}

	private IEnumerator SetContent(string str_date, string str_title)
	{
		yield return new WaitForSeconds(1f);
		_endTipText1.GetComponent<TypewriterEffect>().StartSlowEffect(I18N.instance.getValue(str_date), 0.3f, issound: true);
		yield return new WaitForSeconds((float)I18N.instance.getValue(str_date).Length * 0.3f + 0.2f);
		_endTipText2.GetComponent<TypewriterEffect>().StartSlowEffect(I18N.instance.getValue(str_title), 0.3f, issound: true);
		yield return new WaitForSeconds((float)I18N.instance.getValue(str_title).Length * 0.3f + 0.2f);
		UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Dialog/endPanel"), base.transform.parent);
	}

	private void Speak5Step()
	{
		SkeletonAnimation component = _spine3.GetComponent<SkeletonAnimation>();
		component.AnimationState.TimeScale = 1f;
		component.AnimationState.ClearTracks();
		string[][] array = ((!isEnglish) ? new string[3][]
		{
			new string[5] { "^27BB0273-2ABB-D0C7-CBE8-A3FA18FF5CA8", "8.5", "5.0", "4.0", "0" },
			new string[5] { "^2CA8C04E-7269-8B43-0B48-7A0F9184D437", "6.3", "11.3", "3.5", "1" },
			new string[5] { "^CF024E4E-D720-6D76-129D-CB7E50FCCF87", "3.0", "0.6", "0.0", "2" }
		} : new string[3][]
		{
			new string[5] { "^27BB0273-2ABB-D0C7-CBE8-A3FA18FF5CA8", "6.8", "6.7", "4.0", "0" },
			new string[5] { "^2CA8C04E-7269-8B43-0B48-7A0F9184D437", "5.7", "11.3", "3.5", "1" },
			new string[5] { "^CF024E4E-D720-6D76-129D-CB7E50FCCF87", "3.6", "0.6", "0.0", "2" }
		});
		component.AnimationState.SetAnimation(0, "2then-talk-all", loop: true);
		curSpeakDatas = array;
		curStep = 5;
		StartCoroutine(SpeakIEnumerator());
	}

	private void NoticeNextEvent(CatchEventEnum obj)
	{
		if (obj == CatchEventEnum.SHOW_STEP2)
		{
			Debug.Log("PoliceCall");
			StartCoroutine(PoliceCall());
		}
		if (obj == CatchEventEnum.SHOW_END_START)
		{
			StartCoroutine(ShowEndTip());
		}
	}

	private void OnEnable()
	{
		CatchEvent.Instance.onNoticeNextEvent += NoticeNextEvent;
	}

	private void OnDisable()
	{
		CatchEvent.Instance.onNoticeNextEvent -= NoticeNextEvent;
	}
}
