using System.Collections.Generic;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class Singleperson : MonoBehaviour
{
	public GameManager gameManager;

	public Text txt_name;

	public Text txt_no;

	public Text txt_birth;

	public Text txt_sex;

	public Text txt_id;

	public Text txt_add;

	public Text txt_tel;

	public Text txt_email;

	public Text txt_hitalk;

	public Text txt_position;

	public Image img_role;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void Init(int dbid)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		List<string> list = gameManager.sqlManager.SelectWherePersonTable2(dbid.ToString());
		if (list != null)
		{
			img_role.sprite = Resources.Load<Sprite>("Houtai/" + dbid);
			txt_name.text = list[0];
			txt_no.text = I18N.instance.getValue("^begining01") + " " + (1558000 + dbid);
			txt_sex.text = I18N.instance.getValue("^houtai12") + " " + (list[1].ToUpper().Equals("F") ? I18N.instance.getValue("^customer_data_Gender02") : I18N.instance.getValue("^customer_data_Gender01"));
			txt_birth.text = I18N.instance.getValue("^houtai13") + " " + list[2];
			if (dbid == 18)
			{
				txt_id.text = I18N.instance.getValue("^houtai14") + " AG02119830516092";
			}
			else
			{
				txt_id.text = I18N.instance.getValue("^houtai14") + " " + list[3];
			}
			txt_add.text = I18N.instance.getValue("^houtai19") + " " + I18N.instance.getValue(list[4]);
			txt_hitalk.text = I18N.instance.getValue("^clue_title0009") + ": " + list[5];
			txt_tel.text = I18N.instance.getValue("^houtai20") + " " + list[6];
			txt_email.text = I18N.instance.getValue("^houtai17") + " " + list[7];
			txt_position.text = I18N.instance.getValue("^houtai18") + " " + list[8];
		}
	}
}
