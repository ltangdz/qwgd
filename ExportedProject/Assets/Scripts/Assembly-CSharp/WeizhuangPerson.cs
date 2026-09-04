using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class WeizhuangPerson : MonoBehaviour
{
	public Image avatarBox;

	public new Text name;

	public Text info;

	public GameObject noclick;

	public Button choiceBtn;

	private string id;

	private GameManager gameManager;

	public string ID => id;

	public void Init(string userId, GameManager gameManager, Weizhuang wz, int i, string username)
	{
		id = userId;
		choiceBtn.gameObject.SetActive(value: false);
		string head = gameManager.dataManager.dic3[userId].head;
		if (head.Trim() != "")
		{
			avatarBox.sprite = Resources.Load<Sprite>("touxiang/" + head);
		}
		name.GetComponent<I18NText>().updateTranslation2(gameManager.dataManager.dic3[userId].name);
		info.GetComponent<I18NText>().updateTranslation2(gameManager.dataManager.dic3[userId].describe);
		if (gameManager.player.playerdata.weizhuang[username][userId] == 1)
		{
			noclick.SetActive(value: true);
			choiceBtn.interactable = false;
			choiceBtn.gameObject.SetActive(value: true);
		}
		else
		{
			noclick.SetActive(value: false);
			choiceBtn.gameObject.SetActive(value: true);
			choiceBtn.onClick.RemoveAllListeners();
			choiceBtn.onClick.AddListener(delegate
			{
			});
		}
	}
}
