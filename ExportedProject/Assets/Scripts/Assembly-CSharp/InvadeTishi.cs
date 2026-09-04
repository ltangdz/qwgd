using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InvadeTishi : MonoBehaviour
{
	public GameObject bk;

	public GameObject zhezhao;

	public bool invokeRun;

	public Button sureBtn;

	[Header("更新设置")]
	public Image zhuan;

	public Text percent;

	public Text sce;

	public Image sceImg;

	private void Start()
	{
		Show();
		if (invokeRun)
		{
			StartCoroutine(Run());
		}
		else
		{
			sureBtn.onClick.AddListener(Hide);
		}
	}

	private void Show()
	{
		bk.GetComponent<RectTransform>().DOScale(new Vector3(1f, 1f, 1f), 0.3f);
		bk.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
	}

	private void Hide()
	{
		Object.Destroy(base.gameObject);
	}

	private void HideZhezhao()
	{
		zhezhao.SetActive(value: false);
	}

	private IEnumerator Run()
	{
		zhuan.DOFillAmount(1f, 1f);
		int percent1 = 0;
		DOTween.To(() => percent1, delegate(int x)
		{
			percent1 = x;
		}, 100, 1f).OnUpdate(delegate
		{
			percent.text = percent1 + "%";
		});
		yield return new WaitForSeconds(1f);
		sce.gameObject.SetActive(value: true);
		sceImg.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(1f);
		Hide();
	}
}
