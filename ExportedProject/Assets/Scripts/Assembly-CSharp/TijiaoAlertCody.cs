using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class TijiaoAlertCody : MonoBehaviour
{
	public Text txt_content;

	public Animator tijiaoAlert;

	private GameManager gameManager;

	public PhoneListItem phoneListItem;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void Sure()
	{
		if (phoneListItem != null)
		{
			CancleTishi();
			phoneListItem.Click(isshow: false);
		}
	}

	public void CancleTishi()
	{
		tijiaoAlert.Play("Exit Panel Out");
	}

	public void Refresh()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		DATA11 dATA = gameManager.dataManager.dic11[gameManager.player.GetEventId()];
		string text = "";
		text = ((!gameManager.player.playerdata.itemlist.Contains("10453")) ? (gameManager.player.playerdata.itemlist.Count + " / " + dATA.number) : (gameManager.player.playerdata.itemlist.Count - 1 + " / " + dATA.number));
		txt_content.text = I18N.instance.getValue("^tips0501") + "\n" + string.Format(I18N.instance.getValue("^tips0502"), text);
		base.transform.SetAsLastSibling();
		tijiaoAlert.Play("Exit Panel In");
	}
}
