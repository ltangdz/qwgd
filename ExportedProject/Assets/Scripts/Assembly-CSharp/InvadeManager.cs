using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class InvadeManager : MonoBehaviour
{
	public GameObject _loadingObj;

	public Image _step2Image;

	public GameObject _step3;

	public GameObject _wave;

	public Text _topText;

	public Text _buttomText;

	private string[] _contents;

	public Image[] _lights;

	public Button _finishedButton;

	private void Start()
	{
		_contents = new string[2] { "^9621F6D0-2AD7-F5C2-267D-B5E8DDDD284F", "^FDC6178D-100A-71F4-C15F-2653B8A3705F" };
		for (int i = 0; i < _lights.Length; i++)
		{
			_lights[i].DOFade(0f, 0f);
		}
		_finishedButton.onClick.AddListener(delegate
		{
			Object.Destroy(base.gameObject);
			InvadeEvent.Instance.NoticeInvadeDecryptSuccess();
		});
		_step2Image.gameObject.SetActive(value: false);
		_finishedButton.gameObject.SetActive(value: false);
		_step3.SetActive(value: false);
		_wave.SetActive(value: false);
	}

	private void NoticeStepFinished(int step, bool isSuccess)
	{
		switch (step)
		{
		case 1:
			_loadingObj.transform.DOScaleY(0f, 0.1f).OnComplete(delegate
			{
				_loadingObj.SetActive(value: false);
				_step2Image.gameObject.SetActive(value: true);
			});
			_buttomText.DOFade(0f, 0f);
			_buttomText.DOText(I18N.instance.getValue(_contents[0]), 0f);
			_buttomText.DOFade(1f, 1f);
			break;
		case 2:
		{
			if (isSuccess)
			{
				_step2Image.transform.DOScaleY(0f, 0.2f).OnComplete(delegate
				{
					_step2Image.gameObject.SetActive(value: false);
					_step3.SetActive(value: true);
				});
				_buttomText.text = "";
				_topText.text = "";
				break;
			}
			string value = I18N.instance.getValue(_contents[1]);
			if (_buttomText.text != value)
			{
				_buttomText.DOFade(0f, 0f);
				_buttomText.DOText(value, 0f);
				_buttomText.DOFade(1f, 1f);
			}
			break;
		}
		case 3:
			_wave.SetActive(value: true);
			ShowWave();
			break;
		}
	}

	public void ShowWave()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Append(_lights[0].DOFade(1f, 0.4f));
		sequence.Append(_lights[1].DOFade(0.6f, 0.4f));
		sequence.Append(_lights[2].DOFade(0.3f, 0.4f));
		sequence.Append(_lights[0].DOFade(0f, 0f));
		sequence.Append(_lights[1].DOFade(0f, 0f));
		sequence.Append(_lights[2].DOFade(0f, 0f));
		sequence.SetLoops(3);
		sequence.Play().OnComplete(delegate
		{
			_finishedButton.gameObject.SetActive(value: true);
			Debug.Log("完成");
		});
	}

	private void OnEnable()
	{
		InvadeEvent.Instance.onNoticeStepFinished += NoticeStepFinished;
	}

	private void OnDisable()
	{
		InvadeEvent.Instance.onNoticeStepFinished -= NoticeStepFinished;
	}
}
