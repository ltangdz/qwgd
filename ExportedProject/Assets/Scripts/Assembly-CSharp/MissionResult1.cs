using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class MissionResult1 : CustomDialog
{
	public GameObject cont;

	public GameObject videoBox;

	public List<Sprite> body;

	public List<Sprite> eye;

	public List<Sprite> mouth;

	public Sprite greenBak;

	public Transform playBtn;

	public GameObject rightBox;

	public GameObject leftBox;

	public Exp exp;

	public GameObject loadingLine;

	public bool showSettle;

	private DataManager dataManager;

	private long maskTime;

	private float maxHotVal;

	private bool useFormData;

	private bool play = true;

	public MissionResultValueBox missionResultValueBox;

	public GameObject holdjump;

	public Transform newsInfo;

	public Text txt_anykey;

	public bool isend;

	public bool Play => play;

	private void NewsComment(int videoID)
	{
		DATA21 dATA = dataManager.dic21[videoID.ToString()];
		int person = dATA.person;
		Transform transform = videoBox.transform.Find("img_person");
		transform.GetComponent<Image>().sprite = body[person];
		transform.GetComponent<Image>().SetNativeSize();
		transform.Find("img_biyan").GetComponent<Image>().sprite = eye[person];
		transform.Find("img_mouth").GetComponent<Image>().sprite = mouth[person];
		if (person == 1)
		{
			transform.Find("img_biyan").GetComponent<RectTransform>().localPosition = new Vector3(0f, 248f, 0f);
			transform.Find("img_mouth").GetComponent<RectTransform>().localPosition = new Vector3(2.6f, 74f, 0f);
		}
		transform.Find("img_biyan").GetComponent<Image>().SetNativeSize();
		transform.Find("img_mouth").GetComponent<Image>().SetNativeSize();
		newsInfo.Find("news_title").GetComponent<I18NText>().updateTranslation2(dATA.title);
		newsInfo.Find("news_info").GetComponent<I18NText>().updateTranslation2(dATA.content);
		StartCoroutine(MoveLabel(newsInfo));
		Transform transform2 = videoBox.transform.Find("img_newsImgBox");
		string newsImg = dATA.newsImg;
		UnityEngine.Object.Instantiate(Resources.Load("News/" + newsImg, typeof(Transform)) as Transform, transform2.Find("news_imgBox"));
		transform2.Find("img_littleTitle/txt_titleInfo").GetComponent<I18NText>().updateTranslation2(dATA.imgTitle);
		Transform newsComment = videoBox.transform.Find("news_comment");
		string[] commentName = dATA.commentName.Split(';');
		string[] commentInfo = dATA.commentInfo.Split(';');
		StartCoroutine(ShowComment(commentName, commentInfo, newsComment));
		videoBox.transform.Find("img_newsImgBox").DOScaleX(1f, 0.3f);
	}

	private IEnumerator MoveLabel(Transform newsInfo)
	{
		if (play)
		{
			txt_anykey.gameObject.SetActive(value: false);
			holdjump.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(0.5f);
			float num = newsInfo.Find("news_info").GetComponent<RectTransform>().rect.width;
			newsInfo.Find("news_info").DOLocalMoveX(0f - (num + 500f), (num + 500f) / 90f).SetEase(Ease.Linear);
			loadingLine.transform.DOScaleX(1f, (num + 500f) / 90f).SetEase(Ease.Linear);
			missionResultValueBox.Init((num + 500f) / 90f - 1f);
			yield return new WaitForSeconds((num + 500f) / 90f);
			StopToResult(isend: false);
		}
	}

	public void StopToResult(bool isend)
	{
		playBtn.gameObject.SetActive(value: true);
		playBtn.Find("play_again").GetComponent<Button>().onClick.RemoveAllListeners();
		playBtn.Find("play_again").GetComponent<Button>().onClick.AddListener(delegate
		{
			Replay();
		});
		StopVideo();
		if (isend)
		{
			NewsEnd();
			return;
		}
		this.isend = true;
		txt_anykey.gameObject.SetActive(value: true);
		holdjump.gameObject.SetActive(value: false);
	}

	public void NewsEnd()
	{
		if (gameManager.player.playerdata.isovertask)
		{
			rightBox.GetComponent<NewsResultInfo>().ComepleteDialog();
		}
		else
		{
			rightBox.GetComponent<NewsResultInfo>().NotCompleteDialog();
		}
		txt_anykey.gameObject.SetActive(value: false);
		holdjump.gameObject.SetActive(value: false);
		isend = false;
	}

	public void IsEnd()
	{
		if (isend)
		{
			NewsEnd();
		}
	}

	private void StopVideo()
	{
		Transform obj = videoBox.transform.Find("img_person");
		play = false;
		StopAllCoroutines();
		videoBox.transform.Find("img_newsInfoBox/news_info").GetComponent<RectTransform>().localPosition = new Vector3(500f, -24.8f, 0f);
		obj.Find("img_mouth").gameObject.SetActive(value: false);
		obj.Find("img_biyan").gameObject.SetActive(value: false);
		loadingLine.GetComponent<RectTransform>().localScale = new Vector3(0f, 1f, 1f);
		holdjump.SetActive(value: true);
	}

	private void Replay()
	{
		playBtn.gameObject.SetActive(value: false);
		play = true;
		exp.Replay();
	}

	private void PersonAni()
	{
		StartCoroutine(Wink());
		StartCoroutine(Say());
	}

	private void LoadVideo(string videoId)
	{
	}

	private void ComPrize()
	{
		float num = gameManager.player.playerdata.endTime;
		Debug.Log(StampToDateTime(890287f.ToString()).ToString("HH'h'mm'm'ss's'"));
	}

	public string DateTimeToStamp(DateTime now)
	{
		DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
		return ((long)(now - dateTime).TotalMilliseconds).ToString();
	}

	public DateTime StampToDateTime(string timeStamp)
	{
		DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
		long ticks = long.Parse(timeStamp + "0000");
		TimeSpan value = new TimeSpan(ticks);
		return dateTime.Add(value);
	}

	public string JudgeDayType(string timeStamp)
	{
		long num = long.Parse(timeStamp) - maskTime;
		string text = "";
		if (num <= 86400000)
		{
			return "sameDay";
		}
		return "otherDay";
	}

	private IEnumerator ChangeWidth(Transform obj, float val)
	{
		float objWidth = obj.GetComponent<RectTransform>().rect.width;
		while (objWidth < val)
		{
			objWidth += 10f;
			obj.GetComponent<RectTransform>().sizeDelta = new Vector2(objWidth, 15f);
			yield return new WaitForSeconds(0.02f);
		}
	}

	private IEnumerator ShowComment(string[] commentName, string[] commentInfo, Transform newsComment)
	{
		Transform contentBox = newsComment.Find("Content");
		int i = -1;
		while (play)
		{
			i++;
			if (i > commentInfo.Length - 1)
			{
				i = 0;
			}
			float a = UnityEngine.Random.Range(0, 10);
			yield return new WaitForSeconds(2f);
			Transform commentBox = Resources.Load("News/comment_box", typeof(Transform)) as Transform;
			commentBox.Find("comment_info").GetComponent<I18NText>().updateTranslation2(commentInfo[i]);
			commentBox = UnityEngine.Object.Instantiate(commentBox, contentBox);
			if (a <= 5f)
			{
				commentBox.GetComponent<Image>().sprite = greenBak;
			}
			yield return new WaitForSeconds(0.5f);
			float num = commentBox.GetComponent<RectTransform>().rect.height;
			if (contentBox.childCount <= 6)
			{
				float y = contentBox.GetComponent<RectTransform>().localPosition.y;
				contentBox.DOLocalMoveY(y + num + 20f, 0.3f);
				yield return new WaitForSeconds(0.3f);
				continue;
			}
			float y2 = contentBox.GetComponent<RectTransform>().localPosition.y;
			contentBox.DOLocalMoveY(y2 + num + 20f, 0.3f);
			yield return new WaitForSeconds(0.3f);
			float y3 = contentBox.GetComponent<RectTransform>().localPosition.y;
			float num2 = contentBox.GetChild(0).GetComponent<RectTransform>().rect.height;
			UnityEngine.Object.Destroy(contentBox.GetChild(0).gameObject);
			contentBox.GetComponent<RectTransform>().localPosition = new Vector2(0f, y3 - num2 - 20f);
		}
	}

	private IEnumerator Wink()
	{
		Transform eye = videoBox.transform.Find("img_person/img_biyan");
		while (play)
		{
			yield return new WaitForSeconds(3f);
			eye.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(0.15f);
			eye.gameObject.SetActive(value: false);
		}
	}

	private IEnumerator Say()
	{
		Transform mouth = videoBox.transform.Find("img_person/img_mouth");
		while (play)
		{
			yield return new WaitForSeconds(0.2f);
			mouth.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(0.2f);
			mouth.gameObject.SetActive(value: false);
		}
	}

	public override void BeforeShowSize()
	{
	}

	public override void AfterShowSize()
	{
	}
}
