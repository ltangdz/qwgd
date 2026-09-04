using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class PicPanelInfo : MonoBehaviour
{
	public Text txt_content;

	private GameManager gameManager;

	public string itemid;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		DATA1 dATA = gameManager.dataManager.dic1[itemid];
		txt_content.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue(dATA.title) + ":" + I18N.instance.getValue(dATA.message));
	}
}
