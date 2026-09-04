using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ZhadanSuccConfirm : MonoBehaviour
{
	public Button btnOver;

	public GameObject txt;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f);
		GetComponent<CanvasGroup>().DOFade(1f, 0.4f).OnComplete(delegate
		{
			btnOver.onClick.AddListener(delegate
			{
				gameManager.homeScene.newZhadanDialog.ZhadanSuccess(gameManager.homeScene.zhadanInvade.userid);
				Object.Destroy(gameManager.homeScene.zhadanInvade.gameObject);
			});
		});
	}

	public void Init(string label)
	{
		txt.GetComponent<I18NText>().updateTranslation2(label);
	}
}
