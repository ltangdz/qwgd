using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class SpecialEmail : MonoBehaviour
{
	public GameManager gameManager;

	public Text txt_sender;

	public Text txt_date;

	public Text txt_subject;

	public Text txt_content;

	public string emailid;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Init();
	}

	public void Init()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		DATA15 dATA = gameManager.dataManager.dic15[emailid];
		txt_sender.text = I18N.instance.getValue("^houtai24") + I18N.instance.getValue(dATA.sender);
		txt_subject.text = I18N.instance.getValue("^houtai23") + I18N.instance.getValue(dATA.title);
		txt_date.text = I18N.instance.getValue("^houtai22") + dATA.sendTime;
	}
}
