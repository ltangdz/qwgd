using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using tnt_deploy;

public class FishItem2 : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public Color[] focus;

	public Sprite[] boxBak;

	public Sprite[] title;

	public Sprite[] outLine;

	public Image img_ad;

	public Text txt_title;

	public Text txt_url;

	private int ID;

	private PhishingDialog1 phish1;

	private GameManager gameManager;

	private bool isSelect;

	public int getID
	{
		get
		{
			return ID;
		}
		set
		{
			ID = value;
		}
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void Click()
	{
		Transform parent = base.transform.parent;
		for (int i = 0; i < parent.childCount; i++)
		{
			parent.GetChild(i).GetComponent<FishItem2>().Blur();
			Focus();
			phish1.choiceLinkID = ID.ToString();
		}
	}

	public void Reset(DATA34 dataItem, PhishingDialog1 phish)
	{
		ID = (int)dataItem.ID;
		phish1 = phish;
		string key = dataItem.title;
		string img = dataItem.img;
		string link = dataItem.link;
		Object.Instantiate(Resources.Load<GameObject>("Link/" + img), img_ad.transform).GetComponent<Image>().SetNativeSize();
		txt_title.GetComponent<I18NText>().updateTranslation2(key);
		txt_url.GetComponent<I18NText>().updateTranslation2(link);
	}

	public void Focus()
	{
		isSelect = true;
		txt_title.color = focus[1];
		base.transform.Find("title/img_cube").GetComponent<Image>().sprite = title[1];
		base.transform.GetComponent<Image>().sprite = boxBak[1];
		base.transform.Find("img_ad/img_border").GetComponent<Image>().sprite = outLine[1];
		txt_url.color = focus[1];
	}

	public void Blur()
	{
		isSelect = false;
		txt_title.color = focus[0];
		base.transform.Find("title/img_cube").GetComponent<Image>().sprite = title[0];
		base.transform.GetComponent<Image>().sprite = boxBak[0];
		base.transform.Find("img_ad/img_border").GetComponent<Image>().sprite = outLine[0];
		txt_url.color = focus[0];
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_ = isSelect;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_ = isSelect;
	}
}
