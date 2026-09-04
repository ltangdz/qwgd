using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpecialDanmu : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	[SerializeField]
	private Text txt_content;

	private string content;

	private string itemid;

	[SerializeField]
	private Button btn_ok;

	[SerializeField]
	private Button btn_no;

	public LiveBroadcastingDialog liveBroadcastingDialog;

	[SerializeField]
	private Image img_bk;

	[SerializeField]
	private Sprite[] sprites;

	public bool isclick;

	public bool iscanclick = true;

	public int hopeid;

	private void Start()
	{
		btn_ok.onClick.AddListener(delegate
		{
			liveBroadcastingDialog.StartTime(itemid, hopeid);
		});
		btn_no.onClick.AddListener(delegate
		{
			Cancel();
		});
	}

	public void Hide()
	{
		GetComponent<CanvasGroup>().DOFade(0f, 0.3f).OnComplete(delegate
		{
			Object.Destroy(base.gameObject);
		});
	}

	public void Cancel()
	{
		isclick = false;
		img_bk.sprite = sprites[0];
		btn_ok.gameObject.SetActive(value: false);
		btn_no.gameObject.SetActive(value: false);
	}

	public void Init(string itemid, string str_content, Vector3 pos, int hopeid)
	{
		GetComponent<RectTransform>().localPosition = pos;
		this.hopeid = hopeid;
		this.itemid = itemid;
		txt_content.text = str_content;
		GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (iscanclick && liveBroadcastingDialog.iscanclickspecialdanmu)
		{
			liveBroadcastingDialog.CancelSpecialDanmu();
			isclick = true;
			img_bk.sprite = sprites[1];
			btn_ok.gameObject.SetActive(value: true);
			btn_no.gameObject.SetActive(value: true);
		}
	}
}
