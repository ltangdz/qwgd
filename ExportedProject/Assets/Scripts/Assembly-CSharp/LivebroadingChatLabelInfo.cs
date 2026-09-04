using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class LivebroadingChatLabelInfo : MonoBehaviour
{
	[SerializeField]
	private bool isbak;

	[SerializeField]
	private Text txt_content;

	[SerializeField]
	private Text txt_name;

	[SerializeField]
	private MultiplyText multiplyText;

	[SerializeField]
	private GameObject img_chatinfo;

	[SerializeField]
	private GameObject loading;

	private GameManager gameManager;

	public string contentkey;

	public string collectkey;

	public string currentitemid;

	private bool iscollect;

	public LiveBroadingChatBox liveBroadingChatBox;

	public void Init(bool isani, string ckey)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		iscollect = false;
		if (GetComponent<Animator>() != null)
		{
			GetComponent<Animator>().enabled = isani;
		}
		contentkey = ckey;
		if (!isani)
		{
			ShowReply2(istype: false);
		}
		if (isbak)
		{
			txt_name.text = gameManager.player.playerdata.nickname;
		}
		else if (isani)
		{
			gameManager.soundManager.PlaySound(42);
		}
	}

	public void Init2(bool isani, string ckey, string key, string itemid)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		iscollect = true;
		if (GetComponent<Animator>() != null)
		{
			GetComponent<Animator>().enabled = isani;
		}
		contentkey = ckey;
		collectkey = key;
		currentitemid = itemid;
		if (!isani)
		{
			ShowReply2(istype: false);
		}
		if (isbak)
		{
			txt_name.text = gameManager.player.playerdata.nickname;
		}
		else if (isani)
		{
			gameManager.soundManager.PlaySound(42);
		}
	}

	public void ShowReply2(bool istype)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (loading != null)
		{
			loading.SetActive(value: false);
		}
		img_chatinfo.SetActive(value: true);
		txt_content.gameObject.SetActive(!iscollect);
		multiplyText.gameObject.SetActive(iscollect);
		if (!iscollect)
		{
			if (contentkey != null && contentkey.StartsWith("^"))
			{
				txt_content.text = string.Format(I18N.instance.getValue(contentkey), gameManager.player.playerdata.nickname);
			}
			else if (contentkey != null)
			{
				txt_content.text = contentkey;
			}
			return;
		}
		multiplyText.SetContent2(contentkey, currentitemid, I18N.instance.getValue(collectkey), istype);
		if (!gameManager.player.playerdata.islivecourse)
		{
			AddHighLight();
			if (gameManager.homeScene.liveBroadingChatBox != null)
			{
				gameManager.homeScene.liveBroadingChatBox.highlightlabelinfo = this;
				gameManager.homeScene.liveBroadingChatBox.ShowCourse1();
			}
		}
	}

	public void ShowReply()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (loading != null)
		{
			loading.SetActive(value: false);
		}
		img_chatinfo.SetActive(value: true);
		txt_content.gameObject.SetActive(!iscollect);
		multiplyText.gameObject.SetActive(iscollect);
		if (!iscollect)
		{
			if (contentkey != null && contentkey.StartsWith("^"))
			{
				txt_content.text = string.Format(I18N.instance.getValue(contentkey), gameManager.player.playerdata.nickname);
			}
			else if (contentkey != null)
			{
				txt_content.text = contentkey;
			}
			return;
		}
		multiplyText.SetContent2(contentkey, currentitemid, I18N.instance.getValue(collectkey), istypeeffect: true);
		if (!gameManager.player.playerdata.islivecourse)
		{
			AddHighLight();
			if (gameManager.homeScene.liveBroadingChatBox != null)
			{
				gameManager.homeScene.liveBroadingChatBox.highlightlabelinfo = this;
				gameManager.homeScene.liveBroadingChatBox.ShowCourse1();
			}
		}
	}

	public void AddHighLight()
	{
		base.gameObject.AddComponent<Canvas>().overrideSorting = true;
		base.gameObject.GetComponent<Canvas>().sortingOrder = 5;
		base.gameObject.AddComponent<GraphicRaycaster>();
	}

	public void DeleteHighLight()
	{
		Object.Destroy(base.gameObject.GetComponent<GraphicRaycaster>());
		Object.Destroy(base.gameObject.GetComponent<Canvas>());
	}
}
