using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class ItemPicture : MonoBehaviour
{
	public Image img_mask;

	public Image img_icon;

	public GameManager gameManager;

	public HomeScene homeScene;

	public DATA1 data;

	public string itemid;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		homeScene = gameManager.homeScene;
	}

	public void SetContent(DATA1 data1)
	{
		data = data1;
		itemid = data1.ID.ToString();
		img_icon.gameObject.SetActive(data1.form == 3);
		if (data1.form == 3)
		{
			img_icon.sprite = Resources.Load<Sprite>("Image/" + data1.image.ToString());
		}
		else if (data1.form == 4)
		{
			_ = (GameObject)Object.Instantiate(Resources.Load("Image/" + data1.image), img_mask.transform);
		}
	}

	public void ShowPicture()
	{
		if (homeScene.middle.Find(data.image.ToString()) == null)
		{
			GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Image/" + data.image.ToString()), homeScene.middle);
			gameObject.name = data.image.ToString();
			gameObject.GetComponent<PictureDialog>().Show();
			gameManager.homeScene.pictureDialog = gameObject.GetComponent<PictureDialog>();
		}
	}
}
