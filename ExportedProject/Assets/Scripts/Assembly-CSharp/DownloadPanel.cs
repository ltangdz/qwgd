using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class DownloadPanel : MonoBehaviour
{
	public Image img_full;

	public Image img_right;

	public Text txt_title;

	public GameManager gameManager;

	private void OnEnable()
	{
		GetComponent<Image>().fillAmount = 0f;
		GetComponent<Image>().DOFillAmount(1f, 0.5f);
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		txt_title.GetComponent<I18NText>().updateTranslation2("^download02");
	}

	public void Init(string itemid)
	{
		img_full.fillAmount = 0f;
		img_right.color = Color.gray;
		txt_title.GetComponent<I18NText>().updateTranslation2("^download02");
		img_full.DOFillAmount(1f, 2f).OnComplete(delegate
		{
			StartCoroutine(downloadover(itemid));
		});
	}

	private IEnumerator downloadover(string itemid)
	{
		txt_title.GetComponent<I18NText>().updateTranslation2("^download03");
		img_right.color = Color.white;
		yield return new WaitForSeconds(1f);
		gameManager.homeScene.notebook.AddNewItem(itemid);
		base.gameObject.SetActive(value: false);
	}
}
