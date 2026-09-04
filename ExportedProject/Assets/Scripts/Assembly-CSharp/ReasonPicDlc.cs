using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ReasonPicDlc : MonoBehaviour
{
	public Button btn_close;

	public Transform content;

	public Image contentImage;

	public float opentime = 0.3f;

	public bool isreport;

	public int itemId;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btn_close.onClick.AddListener(delegate
		{
			Object.Destroy(base.gameObject);
		});
	}

	public void InitData()
	{
		int[] source = new int[3] { 11116, 11117, 11197 };
		string text = "";
		if (!source.Contains(itemId))
		{
			switch (PlayerPrefs.GetInt("language", -1))
			{
			case 0:
				text = "_cn";
				break;
			case 2:
				text = "_tw";
				break;
			default:
				text = "_en";
				break;
			}
		}
		string text2 = "_DLC/reason/" + itemId + text;
		Debug.Log(text2);
		Sprite sprite = Resources.Load<Sprite>(text2);
		contentImage.sprite = sprite;
		contentImage.SetNativeSize();
		RectTransform component = contentImage.GetComponent<RectTransform>();
		Vector2 sizeDelta = component.sizeDelta;
		Vector2 vector = component.localPosition;
		btn_close.transform.localPosition = vector + sizeDelta / 2f;
	}

	private IEnumerator ShowVideo()
	{
		gameManager.iscanhoutaiclose = false;
		if (!gameManager.player.playerdata.videotiplist.Contains("3700062"))
		{
			gameManager.homeScene.eventsystem.SetActive(value: false);
			gameManager.soundManager.PlaySound(38);
			yield return new WaitForSeconds(2.6f);
			gameManager.homeScene.ShowVideoTip("3700062");
			yield return new WaitForSeconds(1f);
			gameManager.homeScene.eventsystem.SetActive(value: true);
		}
		else
		{
			Debug.Log("已有3700062");
		}
		gameManager.iscanhoutaiclose = true;
		Object.Destroy(base.gameObject);
	}

	public void Show()
	{
		content.GetComponent<CanvasGroup>().DOFade(1f, opentime);
		content.DOScale(Vector3.one, 0.3f);
	}

	public void Hide()
	{
		content.GetComponent<CanvasGroup>().DOFade(0f, opentime);
		content.DOScale(Vector3.zero, 0.3f);
	}
}
