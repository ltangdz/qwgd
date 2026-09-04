using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LogPanel : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public Button btn_open;

	public Transform content;

	public RectTransform panel;

	public RectTransform scrollview;

	public ScrollRect scrollrect;

	public bool isopen;

	public I18NText txt_open;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btn_open.onClick.AddListener(delegate
		{
			OpenAllList();
		});
	}

	public void OpenAllList(int type = -1)
	{
		if (type != -1)
		{
			isopen = type == 0;
		}
		scrollview.DOKill();
		scrollview.DOSizeDelta(new Vector2(450f, (!isopen) ? 880f : 220f), 0.5f);
		if (!isopen)
		{
			gameManager.homeScene.newsPanel.OpenNews(0);
		}
		txt_open.updateTranslation2((!isopen) ? "^logpanel03" : "^logpanel02");
		isopen = !isopen;
	}

	public void Open()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.GameType != GameTypeEnum.DLC7)
		{
			panel.DOScale(Vector3.one, 0.3f).OnComplete(delegate
			{
				Init();
			});
		}
	}

	public void AddLog(string c, bool isadd = true)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (!gameManager.IsAllDlc())
		{
			((GameObject)Object.Instantiate(Resources.Load("txt_log"), content)).GetComponent<I18NText>().updateTranslation5(c);
			scrollrect.normalizedPosition = Vector2.zero;
			if (isadd)
			{
				gameManager.player.playerdata.loglist.Add(c);
			}
		}
	}

	public void Init()
	{
		if (gameManager.player.playerdata.loglist.Count > 0)
		{
			for (int i = 0; i < gameManager.player.playerdata.loglist.Count; i++)
			{
				AddLog(gameManager.player.playerdata.loglist[i], isadd: false);
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		base.transform.SetAsLastSibling();
	}
}
