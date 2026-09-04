using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class LockPanel02 : MonoBehaviour
{
	[SerializeField]
	private Image img_voice;

	[SerializeField]
	private Image img_voicelight;

	public List<Sprite> sprites = new List<Sprite>();

	[SerializeField]
	private Button btn_sure;

	[SerializeField]
	private Button btn_close;

	[SerializeField]
	private Text txt_tip;

	public int anstwer;

	public int correct = 5;

	private bool iscanclick = true;

	public GameObject tomitem;

	public FolderItem folderItem;

	public GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btn_sure.onClick.AddListener(Sure);
		btn_close.onClick.AddListener(delegate
		{
			base.gameObject.SetActive(value: false);
		});
	}

	private void Sure()
	{
		if (anstwer == -1)
		{
			return;
		}
		iscanclick = false;
		img_voicelight.gameObject.SetActive(value: true);
		img_voicelight.transform.localScale = new Vector3(-1f, 1f, 1f);
		Sequence s = DOTween.Sequence();
		s.Append(img_voicelight.transform.DOLocalMoveX(525f, 1.2f));
		s.Append(img_voicelight.transform.DOScaleX(1f, 0.1f));
		s.Append(img_voicelight.transform.DOLocalMoveX(-441f, 1f));
		s.Append(img_voicelight.transform.DOScaleX(-1f, 0.1f));
		s.Append(img_voicelight.transform.DOLocalMoveX(525f, 1.2f));
		s.Append(img_voicelight.transform.DOScaleX(1f, 0.1f));
		s.Append(img_voicelight.transform.DOLocalMoveX(-441f, 1f)).OnComplete(delegate
		{
			Debug.LogError("jieshu");
			img_voicelight.gameObject.SetActive(value: false);
			iscanclick = true;
			if (anstwer == correct)
			{
				gameManager.player.playerdata.isopenfolder2 = true;
				if (folderItem != null)
				{
					folderItem.Refresh();
				}
				txt_tip.text = I18N.instance.getValue("^houtai118");
				txt_tip.color = Color.green;
				gameManager.saveManager.SavePlayerData();
				Invoke("HideDialog", 1f);
			}
			else
			{
				txt_tip.text = I18N.instance.getValue("^houtai93");
				txt_tip.color = Color.red;
				img_voice.gameObject.SetActive(value: false);
				anstwer = -1;
			}
		});
	}

	public void SelectVoice(int spritepos)
	{
		if (iscanclick)
		{
			anstwer = spritepos;
			img_voice.sprite = sprites[spritepos];
			img_voice.gameObject.SetActive(value: true);
		}
	}

	private void HideDialog()
	{
		base.gameObject.SetActive(value: false);
	}
}
