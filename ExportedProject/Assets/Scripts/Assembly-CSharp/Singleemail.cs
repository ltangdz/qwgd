using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class Singleemail : MonoBehaviour
{
	public GameManager gameManager;

	public Text txt_sender;

	public Text txt_date;

	public Text txt_subject;

	public Text txt_content;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void Init(string emailid)
	{
		txt_content.text = "";
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		DATA15 dATA = gameManager.dataManager.dic15[emailid];
		txt_sender.text = I18N.instance.getValue("^houtai24") + I18N.instance.getValue(dATA.sender);
		txt_subject.text = I18N.instance.getValue("^houtai23") + I18N.instance.getValue(dATA.title);
		txt_date.text = I18N.instance.getValue("^houtai22") + dATA.sendTime;
		string[] array = dATA.info.Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			string value = I18N.instance.getValue(array[i].Substring(1));
			txt_content.text = txt_content.text + "\n" + (value.Substring(0, 2).Equals("  ") ? value : ("    " + value));
		}
	}
}
