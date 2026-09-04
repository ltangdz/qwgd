using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using tnt_deploy;

public class EmailItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	public Image img_bk;

	public Sprite[] sprites;

	public int mailpos;

	public string emailid;

	public Text txt_subject;

	public Text txt_sender;

	public Text txt_date;

	public Text txt_status;

	private GameManager gameManager;

	public EmailPanel emailPanel;

	public void OnPointerEnter(PointerEventData eventData)
	{
		base.transform.DOKill();
		img_bk.sprite = sprites[1];
		base.transform.DOScale(new Vector3(1.01f, 1.01f, 1.01f), 0.2f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		base.transform.DOKill();
		img_bk.sprite = sprites[0];
		base.transform.DOScale(Vector3.one, 0.2f);
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Init();
	}

	private void Init()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		DATA15 dATA = gameManager.dataManager.dic15[emailid];
		string text = I18N.instance.getValue(dATA.sender).Split('(')[0];
		txt_sender.text = I18N.instance.getValue("^houtai24") + text;
		txt_subject.text = I18N.instance.getValue("^houtai23") + I18N.instance.getValue(dATA.title);
		txt_date.text = I18N.instance.getValue("^houtai22") + dATA.sendTime;
		txt_status.text = I18N.instance.getValue("^invade_label18") + I18N.instance.getValue("^houtai25");
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		emailPanel.ShowSingleEmail(mailpos);
	}
}
