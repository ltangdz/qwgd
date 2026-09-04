using System.Collections;
using Aluba;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8;

public class VideoTipDlC8 : LaborerBaseContentDialog
{
	public Button btn_call;

	public Text txt_name;

	public Image img_avatar;

	public GameManager gameManager;

	public Transform content;

	private int _videoGroupId;

	private Sequence _sequence;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void SetTip(string nameKey, string img_avatar, int videoGroupId)
	{
		base.gameObject.SetActive(value: true);
		if (_sequence != null)
		{
			_sequence.Kill();
			_sequence = null;
		}
		GetComponent<CanvasGroup>().alpha = 1f;
		_sequence = DOTween.Sequence();
		_sequence.Append(content.DOScale(1.1f, 1f).SetEase(Ease.Linear));
		_sequence.Append(content.DOScale(1f, 1f).SetEase(Ease.Linear));
		_sequence.SetLoops(-1).Play();
		_videoGroupId = videoGroupId;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		base.transform.SetAsLastSibling();
		SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: false);
		btn_call.onClick.RemoveAllListeners();
		gameManager.soundManager.PlaySoundLoop(2);
		StartCoroutine(GetPhone());
		txt_name.text = I18N.instance.getValue(nameKey);
		this.img_avatar.sprite = Resources.Load<Sprite>("touxiang/" + img_avatar);
		base.transform.GetComponent<RectTransform>().DOAnchorPosX(-77f, 0.5f);
	}

	private void OnDestroy()
	{
		NoticeCloseContent();
	}

	private IEnumerator GetPhone()
	{
		yield return new WaitForSeconds(0.2f);
		btn_call.onClick.AddListener(delegate
		{
			gameManager.soundManager.Stop();
			Object.Instantiate(Resources.Load<VideoDialogDLC8>("Dialog/VideoDialogDLC8"), base.DataController.Controller.content.transform).Init(_videoGroupId);
			SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: false);
			base.transform.GetComponent<RectTransform>().DOAnchorPosX(319f, 0.5f);
			GetComponent<CanvasGroup>().DOFade(0f, 0.5f).OnComplete(delegate
			{
				_sequence.Kill();
				_sequence = null;
				content.DOScale(1f, 0f);
				base.gameObject.SetActive(value: false);
			});
			SingletonAutoMono<DLC8DataController>.GetInstance().Controller.teachDialog.Hide();
			NoticeCloseContent();
		});
	}
}
