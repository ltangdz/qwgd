using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FolderItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	[SerializeField]
	private Image img_bk;

	[SerializeField]
	private Image img_frame;

	[SerializeField]
	private Image img_icon;

	[SerializeField]
	private Text txt_title;

	[SerializeField]
	private Text txt_content1;

	[SerializeField]
	private Text txt_content2;

	[SerializeField]
	private Text txt_content3;

	public int id;

	public string str_count;

	public string str_safelevel;

	public bool islock;

	public Color[] colors;

	public Sprite[] sprites;

	public GameObject lockpanel;

	public ResultListPanel resultListPanel;

	public GameManager gameManager;

	public HoutaiPanel houtaiPanel0;

	public List<string> zimus = new List<string>();

	public List<int> yuyins = new List<int>();

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Refresh();
	}

	private void OnEnable()
	{
		Refresh();
	}

	public void Refresh()
	{
		if (gameManager == null)
		{
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		}
		switch (id)
		{
		case 1:
			islock = !gameManager.player.playerdata.isopenfolder2;
			break;
		case 2:
			islock = !gameManager.player.playerdata.isopenfolder3;
			break;
		case 3:
			islock = !gameManager.player.playerdata.isopenfolder4;
			break;
		}
		txt_title.text = I18N.instance.getValue("^houtai112") + (id + 1);
		txt_content1.text = I18N.instance.getValue("^houtai113") + str_count;
		txt_content2.text = I18N.instance.getValue("^houtai114") + str_safelevel;
		txt_content3.text = I18N.instance.getValue(islock ? "^houtai116" : "^houtai115");
		txt_content3.color = (islock ? colors[1] : colors[0]);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		base.transform.DOKill();
		img_bk.sprite = sprites[3];
		img_frame.sprite = sprites[4];
		img_icon.sprite = sprites[5];
		base.transform.DOScale(new Vector3(1.01f, 1.01f, 1.01f), 0.2f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		base.transform.DOKill();
		img_bk.sprite = sprites[0];
		img_frame.sprite = sprites[1];
		img_icon.sprite = sprites[2];
		base.transform.DOScale(Vector3.one, 0.2f);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (islock)
		{
			if (!houtaiPanel0.showPasswordPanel)
			{
				houtaiPanel0.showPasswordPanel = true;
				lockpanel.SetActive(value: true);
			}
			return;
		}
		if (lockpanel != null)
		{
			lockpanel.SetActive(value: false);
		}
		resultListPanel.gameObject.SetActive(value: true);
		resultListPanel.ShowItems(id);
		switch (id)
		{
		case 0:
			if (!gameManager.player.playerdata.isshowhoutaizimu1)
			{
				houtaiPanel0.ShowZimu(zimus, yuyins, 0f);
				gameManager.player.playerdata.isshowhoutaizimu1 = true;
			}
			break;
		case 1:
			if (!gameManager.player.playerdata.isshowhoutaizimu2)
			{
				houtaiPanel0.ShowZimu(zimus, yuyins, 0f);
				gameManager.player.playerdata.isshowhoutaizimu2 = true;
			}
			break;
		case 2:
			if (!gameManager.player.playerdata.isshowhoutaizimu3)
			{
				houtaiPanel0.ShowZimu(zimus, yuyins, 0f);
				gameManager.player.playerdata.isshowhoutaizimu3 = true;
			}
			break;
		case 3:
			if (!gameManager.player.playerdata.isshowhoutaizimu4)
			{
				houtaiPanel0.ShowZimu(zimus, yuyins, 0f);
				gameManager.player.playerdata.isshowhoutaizimu4 = true;
			}
			break;
		}
		base.transform.parent.gameObject.SetActive(value: false);
		gameManager.saveManager.SavePlayerData();
	}
}
