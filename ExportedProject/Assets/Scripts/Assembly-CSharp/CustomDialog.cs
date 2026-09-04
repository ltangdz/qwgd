using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class CustomDialog : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public GameObject bk;

	public Transform content;

	public float width;

	public float height;

	private RectTransform rectTransform;

	public Button btn_close;

	public int toolid = -1;

	public GameManager gameManager;

	public bool isclick = true;

	public bool isneedtolastSibling = true;

	public abstract void BeforeShowSize();

	public abstract void AfterShowSize();

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		rectTransform = bk.GetComponent<RectTransform>();
		if (btn_close != null)
		{
			btn_close.onClick.AddListener(delegate
			{
				Close();
			});
		}
	}

	public void Close()
	{
		Hide();
		gameManager.soundManager.Stop();
		gameManager.soundManager.PlaySound(8);
	}

	public void Show()
	{
		BeforeShowSize();
		if (gameManager.player.playerdata.isCourseOver != 0)
		{
			gameManager.homeScene.eventsystem.SetActive(value: false);
		}
		base.gameObject.SetActive(value: true);
		rectTransform.DOSizeDelta(new Vector2(width, height), 0.1f).OnComplete(delegate
		{
			if (gameManager.player.playerdata.isCourseOver != 0)
			{
				gameManager.homeScene.eventsystem.SetActive(value: true);
			}
			content.gameObject.SetActive(value: true);
			AfterShowSize();
		});
	}

	public void Hide()
	{
		HideDialog();
	}

	public void HideDialog()
	{
		content.gameObject.SetActive(value: false);
		rectTransform.DOSizeDelta(Vector2.zero, 0.2f).OnComplete(delegate
		{
			Object.Destroy(base.gameObject);
		});
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		SetFront();
	}

	public void SetFront()
	{
		if (isneedtolastSibling)
		{
			if (base.transform.parent.gameObject.name.Equals("otherdialogpanel") && !gameManager.homeScene.Iscanopentool())
			{
				base.transform.parent.SetAsLastSibling();
			}
			base.transform.SetAsLastSibling();
		}
	}
}
