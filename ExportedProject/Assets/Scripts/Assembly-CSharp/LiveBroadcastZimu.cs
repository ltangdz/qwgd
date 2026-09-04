using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class LiveBroadcastZimu : MonoBehaviour
{
	private Text txt_zimu;

	private void Start()
	{
		txt_zimu = GetComponent<Text>();
	}

	public void Init(string key, int p, bool ismanping = false)
	{
		txt_zimu = GetComponent<Text>();
		txt_zimu.text = I18N.instance.getValue(key);
		base.transform.localPosition = new Vector3(1500f, -260 + p * 30, 0f);
		float duration = 20f;
		base.transform.DOLocalMoveX(-1017f, duration).SetEase(Ease.Linear).OnComplete(delegate
		{
			Object.Destroy(base.gameObject);
		});
	}
}
