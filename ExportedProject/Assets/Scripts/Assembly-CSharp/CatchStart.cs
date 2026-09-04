using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class CatchStart : MonoBehaviour
{
	public AlubaLoading1 _loading;

	public Text loadingText;

	private void Start()
	{
		_loading.AddCallback(delegate
		{
			Debug.Log("loading结束");
			_loading.gameObject.SetActive(value: false);
			GetComponent<Image>().DOFade(0f, 1.5f).OnComplete(delegate
			{
				Object.Destroy(base.gameObject);
			});
		});
		Sequence sequence = DOTween.Sequence();
		sequence.Append(loadingText.DOText(I18N.instance.getValue("^vdev1000"), 0f));
		sequence.AppendInterval(10f);
		sequence.Append(loadingText.DOText(I18N.instance.getValue("^vdev1001"), 0f));
		sequence.AppendInterval(5f);
		sequence.Append(loadingText.DOText(I18N.instance.getValue("^vdev1002"), 0f));
		sequence.AppendInterval(4f);
		sequence.Append(loadingText.DOText(I18N.instance.getValue("^vdev1003"), 0f));
		sequence.AppendInterval(4f);
		sequence.Append(loadingText.DOText(I18N.instance.getValue("^vdev1004"), 0f));
		sequence.AppendInterval(3f);
		sequence.Append(loadingText.DOText(I18N.instance.getValue("^vdev1005"), 0f));
		sequence.AppendInterval(15f);
		sequence.Append(loadingText.DOText(I18N.instance.getValue("^vdev1006"), 0f));
		sequence.AppendInterval(6f);
		sequence.Append(loadingText.DOText(I18N.instance.getValue("^vdev1007"), 0f));
		sequence.AppendInterval(6.5f);
		sequence.Play();
	}
}
