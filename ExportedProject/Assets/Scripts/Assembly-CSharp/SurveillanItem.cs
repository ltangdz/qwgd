using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using tnt_deploy;

public class SurveillanItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public Image img_bk;

	public string id;

	public Image[] img_pics;

	public Sprite[] sprites;

	public Text txt_name;

	public Text txt_count;

	public int piccount;

	public bool isselected;

	public SurveillanceDialog surveillanceDialog;

	public GameManager gameManager;

	public void Click()
	{
		isselected = true;
		img_bk.sprite = sprites[1];
		surveillanceDialog.OtherItemCancel(id, piccount);
		Debug.Log("click");
	}

	public void CancelClick()
	{
		isselected = false;
		img_bk.sprite = sprites[0];
		Debug.Log("cancelClick");
	}

	public void Init(DATA36 data36, SurveillanceDialog surveillanceDialog)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		this.surveillanceDialog = surveillanceDialog;
		id = data36.ID.ToString();
		txt_name.GetComponent<I18NText>().updateTranslation2(data36.rolename);
		piccount = 0;
		string[] array = data36.itemids.Substring(1).Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			if (gameManager.player.playerdata.itemlist.Contains(array[i]) || gameManager.isbug)
			{
				Sprite sprite = Resources.Load<Sprite>("Image/" + surveillanceDialog.gameManager.dataManager.dic1[array[i]].image);
				img_pics[i].transform.Find(img_pics[i].name).GetComponent<Image>().sprite = sprite;
				if (sprite.rect.width > sprite.rect.height)
				{
					img_pics[i].transform.Find(img_pics[i].name).GetComponent<RectTransform>().sizeDelta = new Vector2(171f / sprite.rect.height * sprite.rect.width, 171f);
				}
				else
				{
					img_pics[i].transform.Find(img_pics[i].name).GetComponent<RectTransform>().sizeDelta = new Vector2(175f, 175f / sprite.rect.width * sprite.rect.height);
				}
				piccount++;
			}
			else
			{
				img_pics[i].gameObject.SetActive(value: false);
			}
		}
		for (int j = 0; j < 3 - array.Length; j++)
		{
			img_pics[j + array.Length].gameObject.SetActive(value: false);
		}
		txt_count.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^surveillance12") + piccount);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!isselected)
		{
			base.transform.GetComponent<Image>().sprite = sprites[1];
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!isselected)
		{
			base.transform.GetComponent<Image>().sprite = sprites[0];
		}
	}
}
