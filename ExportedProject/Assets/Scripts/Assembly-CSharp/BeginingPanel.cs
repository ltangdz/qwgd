using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class BeginingPanel : MonoBehaviour
{
	public I18NText txt_status;

	public I18NText txt_number;

	public Color[] colors;

	public Sprite[] sprites;

	public Image img_statusbk;

	public Image slider01;

	public Image slider02;

	public I18NText txt_slider01;

	public I18NText txt_slider02;

	public CameraFilterPack_TV_Distorted cameraFilterPack_TV_Distorted;

	public CameraFilterPack_NightVisionFX cameraFilterPack_NightVisionFX;

	private GameManager gameManager;

	public GameObject hold_esc;

	private bool isover;

	private void StartSliderAni()
	{
		txt_status.GetComponent<Text>().color = colors[0];
		img_statusbk.sprite = sprites[0];
		txt_status.updateTranslation5(I18N.instance.getValue("^begining09") + I18N.instance.getValue("^begining10"));
		slider02.DOFillAmount(0.64f, 24f).SetEase(Ease.Linear).OnComplete(delegate
		{
			txt_status.GetComponent<Text>().color = colors[1];
			img_statusbk.sprite = sprites[1];
			txt_status.updateTranslation5(I18N.instance.getValue("^begining09") + I18N.instance.getValue("^begining11"));
			slider02.DOFillAmount(0.88f, 12f).SetEase(Ease.Linear).OnComplete(delegate
			{
				txt_status.GetComponent<Text>().color = colors[2];
				img_statusbk.sprite = sprites[2];
				txt_status.updateTranslation5(I18N.instance.getValue("^begining09") + I18N.instance.getValue("^begining12"));
				slider02.DOFillAmount(1f, 8f).SetEase(Ease.Linear).OnComplete(delegate
				{
					isover = true;
					txt_slider02.updateTranslation5(I18N.instance.getValue("^begining14") + "100%");
					CancelInvoke();
				});
			});
		});
		InvokeRepeating("StartSliderQuickAni", 1f, 0.35f);
	}

	private void StartSliderQuickAni()
	{
		float duration = Random.Range(0.2f, 0.35f);
		slider01.DOFillAmount(1f, duration).SetEase(Ease.InBounce).OnComplete(delegate
		{
			float duration2 = Random.Range(0.1f, 0.15f);
			slider01.DOFillAmount(0f, duration2).SetEase(Ease.InBounce);
		});
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.player.playerdata.lookupnews = true;
		if (cameraFilterPack_TV_Distorted != null && cameraFilterPack_NightVisionFX != null)
		{
			cameraFilterPack_TV_Distorted.enabled = false;
			cameraFilterPack_NightVisionFX.enabled = false;
		}
		gameManager.musicManager.PlayMusicLoop(7);
		txt_number.updateTranslation5(I18N.instance.getValue("^begining01") + "83511235");
		StartSliderAni();
		int num = PlayerPrefs.GetInt("isfirstshowbeginning", 0);
		hold_esc.SetActive((num != 0) ? true : false);
	}

	private void Update()
	{
		if (!isover)
		{
			txt_slider02.updateTranslation5(I18N.instance.getValue("^begining14") + (int)(slider02.fillAmount * 100f) + "%");
			txt_slider01.updateTranslation5(I18N.instance.getValue("^begining13") + (int)(slider01.fillAmount * 100f) + "%");
		}
	}
}
