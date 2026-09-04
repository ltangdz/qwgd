using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ZhadanConfirm : MonoBehaviour
{
	public Button yes;

	public Button no;

	public GameObject txt;

	private GameManager gameManager;

	private ZhadanDialog zhadanDialog;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f);
		GetComponent<CanvasGroup>().DOFade(1f, 0.4f).OnComplete(delegate
		{
			yes.onClick.AddListener(delegate
			{
				zhadanDialog.Restart(1);
				Object.Destroy(base.gameObject);
			});
			no.onClick.AddListener(delegate
			{
				zhadanDialog.Restart();
				Object.Destroy(base.gameObject);
			});
		});
	}

	public void Init(ZhadanDialog zd)
	{
		zhadanDialog = zd;
	}
}
