using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ArrowItem : MonoBehaviour
{
	public int id;

	public Vector3 vec_pos;

	public Vector3 vec_topos;

	public Vector3 vec_centerpos;

	public GameObject item;

	public Vector3 vec_itemtopos;

	public Reasoning4007Step02 reasoning4007Step02;

	private Sequence sq;

	private void Start()
	{
		sq = DOTween.Sequence();
		vec_pos = base.transform.localPosition;
		sq.Append(base.transform.DOLocalMove(vec_topos, 0.5f));
		sq.Append(base.transform.DOLocalMove(vec_pos, 0.5f));
		sq.Play().SetLoops(-1);
		GetComponent<Button>().onClick.AddListener(Click);
	}

	private void Click()
	{
		sq.Pause();
		base.transform.DOLocalMove(vec_centerpos, 0.2f).OnComplete(delegate
		{
			if (reasoning4007Step02.Check1(this))
			{
				base.gameObject.SetActive(value: false);
				item.transform.DOLocalMove(vec_itemtopos, 0.2f);
				item.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);
			}
		});
	}

	public void Resetpos()
	{
		base.transform.localPosition = vec_pos;
		item.GetComponent<CanvasGroup>().DOFade(0f, 0.2f).OnComplete(delegate
		{
			base.gameObject.SetActive(value: true);
			sq.Restart();
			item.transform.localPosition = new Vector3(0f, -294f, 0f);
		});
	}
}
