using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class NewsContentItem : MonoBehaviour
{
	public Image img_news;

	public I18NText txt_content;

	public GameManager gameManager;

	public string id;

	public void SetContent(string id)
	{
		this.id = id;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		DATA13 dATA = gameManager.dataManager.dic13[id];
		txt_content.updateTranslation2(dATA.arrowid);
		img_news.sprite = Resources.Load<Sprite>("News/" + dATA.picname.Substring(1) + "small");
	}

	public void Show()
	{
		base.transform.DOLocalMoveX(0f, 1f);
	}

	public void Hide(bool isright = false)
	{
		base.transform.DOLocalMoveX(isright ? 515f : (-515f), 1f).OnComplete(delegate
		{
			Object.Destroy(base.gameObject);
		});
	}

	private void Start()
	{
	}
}
