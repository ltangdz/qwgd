using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class LiveStreamingBrowser : MonoBehaviour
{
	[SerializeField]
	private Text txt_content;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.homeScene.iszhibojian)
		{
			txt_content.text = I18N.instance.getValue("^zhibo0501");
		}
		else
		{
			txt_content.text = I18N.instance.getValue("^liveroom03");
		}
	}
}
