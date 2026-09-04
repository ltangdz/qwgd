using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SurLoseDialog : MonoBehaviour
{
	public Button btn_close;

	public Image img_black;

	private void Start()
	{
	}

	private void OnEnable()
	{
		img_black.fillAmount = 0f;
		img_black.DOFillAmount(1f, 0.5f);
	}
}
