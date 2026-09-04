using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChatBak : MonoBehaviour
{
	public Image img_top;

	public Image img_bottom;

	public Image img_black;

	public void ShowCourse(float wait = 0f)
	{
		img_top.transform.DOLocalMove(new Vector3(0f, 540f, 0f), 0.2f);
		img_bottom.transform.DOLocalMove(new Vector3(0f, -482f, 0f), 0.2f);
		img_black.gameObject.SetActive(value: true);
	}

	public void HideBlack()
	{
		img_top.transform.DOLocalMove(new Vector3(0f, 755f, 0f), 0.2f);
		img_bottom.transform.DOLocalMove(new Vector3(0f, -660f, 0f), 0.2f);
		base.gameObject.SetActive(value: false);
	}
}
