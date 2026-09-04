using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	public Image img_bk;

	public Image img_lightframe;

	public Image img_role;

	public Sprite[] sprites;

	public int dbid;

	public Text txt_no;

	public Text txt_name;

	public Text txt_sex;

	public Text txt_age;

	public Text txt_id;

	public Text txt_status;

	private GameManager gameManager;

	public PersonPanel personPanel;

	[SerializeField]
	private Color[] colors;

	public bool ishurt;

	public void OnPointerEnter(PointerEventData eventData)
	{
		base.transform.DOKill();
		img_bk.sprite = sprites[1];
		img_lightframe.gameObject.SetActive(value: true);
		img_role.transform.DOScale(new Vector3(0.31f, 0.31f, 0.31f), 0.2f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		base.transform.DOKill();
		img_bk.sprite = sprites[0];
		img_lightframe.gameObject.SetActive(value: false);
		img_role.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.2f);
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		dbid = base.transform.GetSiblingIndex() + 1;
		Init();
	}

	private void Init()
	{
		List<string> list = gameManager.sqlManager.SelectWherePersonTable(dbid.ToString());
		if (list != null)
		{
			img_role.sprite = Resources.Load<Sprite>("Houtai/" + dbid);
			txt_no.text = I18N.instance.getValue("^begining01") + " " + (1558000 + dbid);
			txt_name.text = list[0];
			txt_sex.text = I18N.instance.getValue("^houtai12") + (list[1].ToUpper().Equals("F") ? I18N.instance.getValue("^customer_data_Gender02") : I18N.instance.getValue("^customer_data_Gender01"));
			txt_age.text = I18N.instance.getValue("^houtai13") + list[2];
			if (dbid == 18)
			{
				txt_id.text = I18N.instance.getValue("^houtai14") + " AG02119830516092";
			}
			else
			{
				txt_id.text = I18N.instance.getValue("^houtai14") + " " + list[3];
			}
			txt_status.text = (list[4].Equals("110003") ? I18N.instance.getValue("^houtai16") : I18N.instance.getValue("^houtai15"));
			txt_status.color = (list[4].Equals("110003") ? colors[1] : colors[0]);
			ishurt = list[4].Equals("110003");
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!ishurt)
		{
			personPanel.ShowSinglePerson(dbid);
		}
	}
}
