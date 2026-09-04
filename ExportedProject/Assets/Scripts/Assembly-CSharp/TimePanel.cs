using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TimePanel : MonoBehaviour
{
	[SerializeField]
	private Button btn_up;

	[SerializeField]
	private Button btn_down;

	[SerializeField]
	private Transform numgroup;

	public int current = 1;

	public GameObject currentitem;

	public bool iscanclick = true;

	[SerializeField]
	private Sprite redsprite;

	[SerializeField]
	private Sprite bluesprite;

	private Image img_bk;

	public int limitcount = 12;

	public void SetRed()
	{
		img_bk.sprite = redsprite;
		Sequence sequence = DOTween.Sequence();
		sequence.Append(img_bk.DOFade(0.2f, 0.2f));
		sequence.Append(img_bk.DOFade(1f, 0.2f));
		sequence.Play().SetLoops(3).OnComplete(delegate
		{
			img_bk.sprite = bluesprite;
		});
	}

	private void Start()
	{
		img_bk = GetComponent<Image>();
		btn_up.onClick.AddListener(delegate
		{
			if (iscanclick)
			{
				iscanclick = false;
				if (currentitem.transform.GetSiblingIndex() == 0)
				{
					int num = ((current == 1) ? limitcount : (current - 1));
					GameObject gameObject = Object.Instantiate(Resources.Load("ReasoningPanel/img_num" + num) as GameObject, numgroup);
					gameObject.transform.localPosition = new Vector3(0f, currentitem.transform.localPosition.y + 100f, 0f);
					gameObject.transform.SetAsFirstSibling();
					currentitem = gameObject;
				}
				else
				{
					currentitem = numgroup.GetChild(currentitem.transform.GetSiblingIndex() - 1).gameObject;
				}
				numgroup.DOLocalMoveY(numgroup.localPosition.y - 100f, 0.5f).OnComplete(delegate
				{
					iscanclick = true;
				});
				current = ((current == 1) ? limitcount : (current - 1));
			}
		});
		btn_down.onClick.AddListener(delegate
		{
			if (iscanclick)
			{
				iscanclick = false;
				if (currentitem.transform.GetSiblingIndex() == numgroup.childCount - 1)
				{
					int num = ((current == limitcount) ? 1 : (current + 1));
					GameObject gameObject = Object.Instantiate(Resources.Load("ReasoningPanel/img_num" + num) as GameObject, numgroup);
					gameObject.transform.localPosition = new Vector3(0f, currentitem.transform.localPosition.y - 100f, 0f);
					gameObject.transform.SetAsLastSibling();
					currentitem = gameObject;
				}
				else
				{
					currentitem = numgroup.GetChild(currentitem.transform.GetSiblingIndex() + 1).gameObject;
				}
				numgroup.DOLocalMoveY(numgroup.localPosition.y + 100f, 0.5f).OnComplete(delegate
				{
					iscanclick = true;
				});
				current = ((current == limitcount) ? 1 : (current + 1));
			}
		});
	}
}
