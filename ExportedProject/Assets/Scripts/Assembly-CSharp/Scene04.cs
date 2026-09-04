using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;

public class Scene04 : MonoBehaviour
{
	public float bakSpeed;

	public float webMoveSpeed;

	public Transform webInfo;

	public int maxBox;

	public float minAlpha;

	public float webRateTime;

	public float hideSpeed;

	private float[] xPosi;

	private float[] yPosi;

	private Animator ani;

	private bool subwayMove;

	private float startX;

	private float endX;

	private int crtWebInfo;

	private bool browse = true;

	private GameObject bornPoint;

	private int crtSpriteIndex;

	private int crtBox;

	private bool startHide;

	private string[] news;

	private GameManager gameManager;

	private void Awake()
	{
		endX = -5f;
	}

	private void Start()
	{
		ani = GetComponent<Animator>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (I18N.instance.gameLang != LanguageCode.CN && I18N.instance.gameLang != LanguageCode.TC)
		{
			base.transform.parent.Find("Canvas04/zimu_cn/Text").gameObject.SetActive(value: false);
		}
		xPosi = new float[2] { -12f, 4f };
		yPosi = new float[2] { -2f, 6f };
		StartCoroutine(SearchWebInfo());
		news = new string[10] { "^news_01", "^news_02", "^news_03", "^news_04", "^news_05", "^news_06", "^news_07", "^news_08", "^news_09", "^news_10" };
		StartCoroutine(ShowLabel());
		gameManager.Esc.GetComponent<HoldEsc>().sceneName = base.transform.parent.name;
	}

	public void SubwayMove()
	{
		if (!subwayMove)
		{
			subwayMove = true;
			base.transform.DOMoveX(endX, bakSpeed);
		}
	}

	private IEnumerator SearchWebInfo()
	{
		yield return new WaitForSeconds(bakSpeed - 1f);
		StartCoroutine(Change("Canvas05"));
		while (browse)
		{
			yield return new WaitForSeconds(webRateTime);
			BornWebBox(webInfo.GetChild(crtSpriteIndex), news[crtSpriteIndex]);
			if (crtSpriteIndex < webInfo.childCount - 1)
			{
				crtSpriteIndex++;
			}
			crtBox++;
			if (crtBox == 3)
			{
				webRateTime = 0.5f;
			}
			if (crtBox >= maxBox)
			{
				browse = false;
			}
		}
	}

	private void BornWebBox(Transform webBox, string newsInfo)
	{
		TextMesh component = webBox.Find("text").GetComponent<TextMesh>();
		component.GetComponent<I18NTextMesh>().updateTranslation2(newsInfo);
		StartCoroutine(SetAlpha(webBox, component, Time.time));
		webBox.transform.DOScale(new Vector3(1f, 1f, 1f), webMoveSpeed);
	}

	private Vector3 GetPosition()
	{
		Vector3 vector = default(Vector3);
		switch (crtBox)
		{
		case 0:
			vector = new Vector3(-8f, 5f, 0f);
			break;
		case 1:
			vector = new Vector3(-9f, 2f, 0f);
			break;
		case 2:
			vector = new Vector3(-4f, 2.5f, 0f);
			break;
		case 3:
			vector = new Vector3(-8f, 0f, 0f);
			break;
		case 4:
			vector = new Vector3(-8f, -2f, 0f);
			break;
		default:
		{
			float x = Random.Range(xPosi[0], xPosi[1]);
			float y = Random.Range(yPosi[0], yPosi[1]);
			vector = new Vector3(x, y, 0f);
			break;
		}
		}
		return vector;
	}

	private IEnumerator SetAlpha(Transform webBox, TextMesh txt, float alpStartTime)
	{
		while (webBox.transform.GetComponent<SpriteRenderer>().color.a != minAlpha)
		{
			yield return new WaitForSeconds(0.02f);
			float a = Mathf.Lerp(webBox.transform.GetComponent<SpriteRenderer>().color.a, minAlpha, (Time.time - alpStartTime) * hideSpeed);
			webBox.transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, a);
			txt.color = new Color(0.2f, 0.2f, 0.2f, a);
		}
	}

	private IEnumerator Change(string scene)
	{
		yield return new WaitForSeconds(webRateTime * 4.5f);
		if (!gameManager.holdEsc)
		{
			gameManager.startAniManager.ChangeScene(scene);
		}
	}

	private IEnumerator ShowLabel()
	{
		GameObject txt = base.transform.parent.Find("Canvas04/zimu_cn").gameObject;
		yield return new WaitForSeconds(0.5f);
		GameObject.Find("GameManager").GetComponent<GameManager>().ShowLabel(txt);
		yield return new WaitForSeconds(4f);
		GameObject.Find("GameManager").GetComponent<GameManager>().HideLabel(txt);
	}
}
