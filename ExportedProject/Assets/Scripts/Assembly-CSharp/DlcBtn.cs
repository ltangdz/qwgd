using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class DlcBtn : MonoBehaviour
{
	public Button fb;

	public Button ytb;

	public Button tt;

	public Button wb;

	private int a = 1;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		fb.onClick.AddListener(delegate
		{
			Application.OpenURL("https://www.facebook.com/cybermanhunt");
		});
		ytb.onClick.AddListener(delegate
		{
			Application.OpenURL(" https://www.youtube.com/channel/UCB0RD4fWL_TqczCmUPt9IWg");
		});
		tt.onClick.AddListener(delegate
		{
			Application.OpenURL("https://twitter.com/cyber_manhunt");
		});
		wb.onClick.AddListener(delegate
		{
			Application.OpenURL("https://weibo.com/u/7478639973");
		});
	}

	public void OpenDlc()
	{
		if (I18N.instance.gameLang == LanguageCode.CN || I18N.instance.gameLang == LanguageCode.TC)
		{
			fb.gameObject.SetActive(value: false);
			ytb.gameObject.SetActive(value: false);
			tt.gameObject.SetActive(value: false);
			wb.gameObject.SetActive(value: true);
		}
		else
		{
			fb.gameObject.SetActive(value: true);
			ytb.gameObject.SetActive(value: true);
			tt.gameObject.SetActive(value: true);
			wb.gameObject.SetActive(value: false);
		}
		a++;
		if (a % 2 == 0)
		{
			GetComponent<RectTransform>().DOSizeDelta(new Vector2(300f, 43f), 0.2f);
		}
		else
		{
			GetComponent<RectTransform>().DOSizeDelta(new Vector2(156f, 43f), 0.2f);
		}
		gameManager.soundManager.PlaySound(16);
	}
}
