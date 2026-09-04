using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class CastGroupDlc : MonoBehaviour
{
	public List<Sprite> _castList1;

	public List<Sprite> _castList2;

	public List<Sprite> _castList3;

	public List<Sprite> _castList4;

	public List<Sprite> _castList5;

	public List<Sprite> _castList6;

	public GameObject _cast1;

	public GameObject _cast2;

	public GameObject _cast3;

	public GameObject _cast4;

	public GameObject _cast5;

	public GameObject _cast6;

	public Text _contentText;

	public Image _cast1Image;

	public Image _cast2Image;

	public Image _cast3Image;

	public Image _cast4Image;

	public Image _cast5Image;

	public Image _cast6Image;

	private bool _isAnimation;

	public List<GameObject> _cast1Objs;

	public List<GameObject> _cast2Objs;

	public List<GameObject> _cast3Objs;

	public List<GameObject> _cast4Objs;

	public List<GameObject> _cast5Objs;

	public List<GameObject> _cast6Objs;

	protected int _frame;

	private bool isEnglish;

	private GameManager gameManager;

	private string[] contentStr = new string[6] { "^BD9230BB-CE62-EECA-F43F-5C98E4DB4502", "^3E9E6CD7-FA9F-C398-BCB2-30CA28EEE1F9", "^62E13ABB-A96D-165A-C7BD-33FE3E0E19F3", "^B31A1B65-9B7B-F310-BF3A-4968EDF9A0F7", "^31B3538F-57C1-0EC1-7F60-B1DA3BA0823D", "^7A44E0ED-084B-51A8-E447-B26585B76782" };

	public void Begin()
	{
		GetComponent<Image>().DOFade(1f, 3f);
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
		{
			isEnglish = false;
		}
		else if (I18N.instance.gameLang == LanguageCode.EN)
		{
			isEnglish = true;
		}
		_isAnimation = true;
		gameManager.musicManager.Stop();
		StartCoroutine("PlayAnimation");
		Invoke("PlayMusic", 1f);
		Invoke("ShowCast1", 3f);
	}

	private void PlayMusic()
	{
		gameManager.musicManager.PlayMusic(23);
	}

	private void ShowCast1()
	{
		_cast1.GetComponent<CanvasGroup>().DOFade(1f, 0f);
		Vector2[] posList = new Vector2[2]
		{
			new Vector2(-120f, 235f),
			new Vector2(123.7f, -103.8f)
		};
		_cast1Image.DOFade(0f, 0f).OnComplete(delegate
		{
			_cast1Image.DOFade(1f, 4f);
		});
		gameManager.soundManager.PlayEvent("110006", 97);
		PlayZimu(0, 1f, isEnglish ? 8f : 7.37f, 5f);
		Sequence sequence = DOTween.Sequence();
		sequence.AppendInterval(0.5f);
		sequence.Append(_cast1Objs[0].transform.DOLocalMove(posList[0], 0.3f).OnComplete(delegate
		{
			_cast1Objs[1].transform.DOScaleY(1f, 0.4f);
			_cast1Objs[2].transform.DOScaleY(1f, 0.4f);
			_cast1Objs[3].transform.DOLocalMove(posList[1], 0.2f).OnComplete(delegate
			{
				_cast1Objs[4].transform.DOScaleY(1f, 0.2f);
				_cast1Objs[5].transform.DOScaleY(1f, 0.2f);
			});
		}));
		sequence.AppendInterval(5f);
		sequence.Append(_cast1Objs[4].transform.DOScaleY(1f, 0.2f)).OnComplete(delegate
		{
			_cast1Objs[0].GetComponent<CanvasGroup>().DOFade(0f, 0.4f).OnComplete(delegate
			{
				StartCoroutine(ShowCast2(0f));
			});
			_cast1Objs[3].GetComponent<CanvasGroup>().DOFade(0f, 0.4f);
			_cast1Image.DOFade(0f, 4f);
		});
		sequence.Play();
	}

	private IEnumerator ShowCast2(float time)
	{
		yield return new WaitForSeconds(time);
		_cast2.GetComponent<CanvasGroup>().DOFade(1f, 0f);
		new Vector2(-44f, -25f);
		_cast2Objs[2].GetComponent<Text>().DOFade(0f, 0f);
		_cast2Objs[3].GetComponent<Text>().DOFade(0f, 0f);
		_cast2Objs[4].GetComponent<Text>().DOFade(0f, 0f);
		_cast2Image.DOFade(0f, 0f).OnComplete(delegate
		{
			_cast2Image.DOFade(1f, 4f);
		});
		gameManager.soundManager.PlayEvent("110006", 98);
		PlayZimu(1, 1.5f, isEnglish ? 5.6f : 6.63f, 4.5f);
		DOTween.Sequence();
		yield return new WaitForSeconds(0.5f);
		_cast2Objs[0].transform.DOLocalMove(default(Vector3), 0.3f);
		yield return new WaitForSeconds(0.3f);
		_cast2Objs[1].transform.DOScaleY(1f, 0.4f);
		yield return new WaitForSeconds(0.3f);
		_cast2Objs[2].GetComponent<Text>().DOFade(1f, 0.4f);
		yield return new WaitForSeconds(0.3f);
		_cast2Objs[3].GetComponent<Text>().DOFade(1f, 0.4f);
		yield return new WaitForSeconds(0.3f);
		_cast2Objs[4].GetComponent<Text>().DOFade(1f, 0.4f);
		yield return new WaitForSeconds(5f);
		_cast2Objs[0].GetComponent<Image>().DOFade(0f, 0.3f);
		for (int num = 0; num < _cast2Objs.Count - 1; num++)
		{
			_cast2Objs[num + 1].GetComponent<Text>().DOFade(0f, 0.3f);
		}
		yield return new WaitForSeconds(1f);
		_cast2Image.DOFade(0f, 2f);
		StartCoroutine(ShowCast3(0f));
	}

	private IEnumerator ShowCast3(float time)
	{
		yield return new WaitForSeconds(time);
		Vector2 pos = new Vector2(-62f, -15.5f);
		_cast3.GetComponent<CanvasGroup>().DOFade(1f, 0f);
		_cast3Objs[2].GetComponent<Text>().DOFade(0f, 0f);
		_cast3Objs[3].GetComponent<Text>().DOFade(0f, 0f);
		_cast3Objs[4].GetComponent<Text>().DOFade(0f, 0f);
		_cast3Image.DOFade(0f, 0f).OnComplete(delegate
		{
			_cast3Image.DOFade(1f, 4f);
		});
		gameManager.soundManager.PlayEvent("110006", 99);
		PlayZimu(2, 1f, isEnglish ? 8.5f : 11f, 6.5f);
		yield return new WaitForSeconds(0.5f);
		_cast3Objs[0].transform.DOLocalMove(pos, 0.3f);
		yield return new WaitForSeconds(0.3f);
		_cast3Objs[1].transform.DOScaleY(1f, 0.4f);
		yield return new WaitForSeconds(0.3f);
		_cast3Objs[2].GetComponent<Text>().DOFade(1f, 0.4f);
		yield return new WaitForSeconds(0.3f);
		_cast3Objs[3].GetComponent<Text>().DOFade(1f, 0.4f);
		yield return new WaitForSeconds(0.3f);
		_cast3Objs[4].GetComponent<Text>().DOFade(1f, 0.4f);
		yield return new WaitForSeconds(0.3f);
		_cast3Objs[5].GetComponent<Text>().DOFade(1f, 0.4f);
		yield return new WaitForSeconds(10f);
		_cast3Objs[0].GetComponent<Image>().DOFade(0f, 0.3f);
		for (int num = 0; num < _cast3Objs.Count - 1; num++)
		{
			_cast3Objs[num + 1].GetComponent<Text>().DOFade(0f, 0.3f);
		}
		yield return new WaitForSeconds(1f);
		_cast3Image.DOFade(0f, 3f);
		StartCoroutine(ShowCast4(0f));
	}

	private IEnumerator ShowCast4(float time)
	{
		yield return new WaitForSeconds(time);
		Vector2 pos = new Vector2(-50f, -3f);
		_cast4.GetComponent<CanvasGroup>().DOFade(1f, 0f);
		_cast4Image.DOFade(1f, 4f);
		gameManager.soundManager.PlayEvent("110006", 100);
		PlayZimu(3, 1f, isEnglish ? 7.5f : 7f, 5.5f);
		yield return new WaitForSeconds(0.5f);
		_cast4Objs[0].transform.DOLocalMove(pos, 0.3f);
		yield return new WaitForSeconds(0.3f);
		_cast4Objs[1].transform.DOScaleY(1f, 0.4f);
		yield return new WaitForSeconds(0.3f);
		_cast4Objs[2].GetComponent<Text>().DOFade(1f, 0.4f);
		yield return new WaitForSeconds(0.3f);
		_cast4Objs[3].GetComponent<Text>().DOFade(1f, 0.4f);
		yield return new WaitForSeconds(0.3f);
		_cast4Objs[4].GetComponent<Text>().DOFade(1f, 0.4f);
		yield return new WaitForSeconds(0.3f);
		_cast4Objs[5].GetComponent<Text>().DOFade(1f, 0.4f);
		yield return new WaitForSeconds(6f);
		_cast4Objs[0].GetComponent<Image>().DOFade(0f, 0.3f);
		for (int i = 0; i < _cast3Objs.Count - 1; i++)
		{
			_cast4Objs[i + 1].GetComponent<Text>().DOFade(0f, 0.3f);
		}
		yield return new WaitForSeconds(1f);
		_cast4Image.DOFade(0f, 3f);
		StartCoroutine(ShowCast5(0f));
	}

	private IEnumerator ShowCast5(float time)
	{
		yield return new WaitForSeconds(time);
		Vector2 pos = new Vector2(40f, -1.5f);
		_cast5.GetComponent<CanvasGroup>().DOFade(1f, 0f);
		_cast5Image.DOFade(1f, 4f);
		gameManager.soundManager.PlayEvent("110006", 101);
		PlayZimu(4, 1f, isEnglish ? 7.6f : 10f, 5.5f);
		yield return new WaitForSeconds(0.5f);
		_cast5Objs[0].transform.DOLocalMove(pos, 0.3f);
		yield return new WaitForSeconds(0.3f);
		_cast5Objs[1].transform.DOScaleY(1f, 0.4f);
		_cast5Objs[2].transform.DOScaleY(1f, 0.4f);
		yield return new WaitForSeconds(0.3f);
		_cast5Objs[2].GetComponent<Text>().DOFade(1f, 0.4f);
		yield return new WaitForSeconds(0.3f);
		yield return new WaitForSeconds(0.3f);
		_cast5Objs[3].GetComponent<Text>().DOFade(1f, 0.4f);
		yield return new WaitForSeconds(0.3f);
		_cast5Objs[4].GetComponent<Text>().DOFade(1f, 0.4f);
		yield return new WaitForSeconds(8f);
		_cast5Objs[0].GetComponent<Image>().DOFade(0f, 0.3f);
		for (int i = 0; i < _cast5Objs.Count - 1; i++)
		{
			_cast5Objs[i + 1].GetComponent<Text>().DOFade(0f, 0.3f);
		}
		yield return new WaitForSeconds(1f);
		_cast5Image.DOFade(0f, 3f);
		StartCoroutine(ShowCast6(0f));
	}

	private IEnumerator ShowCast6(float time)
	{
		List<GameObject> clodiaTxts = new List<GameObject>(GameObject.FindGameObjectsWithTag("ClaudiaText"));
		yield return new WaitForSeconds(time);
		_cast6.GetComponent<CanvasGroup>().DOFade(1f, 0f);
		_cast6Image.DOFade(1f, 4f);
		gameManager.soundManager.PlayEvent("110006", 102);
		PlayZimu(5, 0.5f, isEnglish ? 15.2f : 14f, 14f);
		while (clodiaTxts.Count > 0)
		{
			int index = Random.Range(0, clodiaTxts.Count);
			clodiaTxts[index].GetComponent<CanvasGroup>().DOFade(1f, 0.8f);
			clodiaTxts.RemoveAt(index);
			yield return new WaitForSeconds(0.8f);
		}
		yield return new WaitForSeconds(3f);
		CatchEvent.Instance.NoticeNextEvent(CatchEventEnum.SHOW_END_START);
	}

	private void PlayZimu(int index, float delayTime, float voiceTime, float finishedTime)
	{
		Sequence sequence = DOTween.Sequence();
		sequence.AppendInterval(delayTime);
		sequence.Append(_contentText.DOText(I18N.instance.getValue(contentStr[index]), finishedTime));
		sequence.AppendInterval(voiceTime - finishedTime - delayTime);
		sequence.Append(_contentText.DOText("", finishedTime));
		sequence.Play();
	}

	private IEnumerator PlayAnimation()
	{
		while (true)
		{
			_cast1Image.sprite = _castList1[0];
			_cast2Image.sprite = _castList2[0];
			_cast3Image.sprite = _castList3[0];
			_cast4Image.sprite = _castList4[0];
			_cast5Image.sprite = _castList5[0];
			_cast6Image.sprite = _castList6[0];
			yield return new WaitForSeconds(0.13f);
			_cast1Image.sprite = _castList1[1];
			_cast2Image.sprite = _castList2[1];
			_cast3Image.sprite = _castList3[1];
			_cast4Image.sprite = _castList4[1];
			_cast5Image.sprite = _castList5[1];
			_cast6Image.sprite = _castList6[1];
			yield return new WaitForSeconds(0.13f);
			_cast1Image.sprite = _castList1[2];
			_cast2Image.sprite = _castList2[2];
			_cast3Image.sprite = _castList3[2];
			_cast4Image.sprite = _castList4[2];
			_cast5Image.sprite = _castList5[2];
			_cast6Image.sprite = _castList6[2];
			yield return new WaitForSeconds(0.13f);
			_cast1Image.sprite = _castList1[3];
			_cast2Image.sprite = _castList2[3];
			_cast3Image.sprite = _castList3[3];
			_cast4Image.sprite = _castList4[3];
			_cast5Image.sprite = _castList5[3];
			_cast6Image.sprite = _castList6[3];
			yield return new WaitForSeconds(0.13f);
		}
	}
}
