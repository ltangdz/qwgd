using Honeti;
using UnityEngine;

public class CompanyBrowser : MonoBehaviour
{
	public GameManager gameManager;

	public MultiplyText txt_name;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.homeScene.ShowVideoTip("3700016");
		string value = I18N.instance.getValue("^message_event0216");
		txt_name.SetNewWidth(value);
		txt_name.SetContent2("^message_event0216", "10094", I18N.instance.getValue("^message_event0216"));
	}
}
