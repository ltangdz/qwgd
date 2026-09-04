using System.Collections;
using Honeti;
using UnityEngine;

public class WarningCanvas : MonoBehaviour
{
	public GameObject beginCanvas;

	private GameManager gameManager;

	public CameraFilterPack_TV_Artefact cameraFilterPack_tv_artefact;

	public CameraFilterPack_NightVisionFX cameraFilterPack_nightvisionfx;

	public CameraFilterPack_TV_Distorted cameraFilterPack_tx_distorted;

	[SerializeField]
	private GameObject warning01;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.warningCanvas = this;
		gameManager.Esc.GetComponent<HoldEsc>().sceneName = "warning";
		if (gameManager.issteam || Application.isEditor)
		{
			ShowWarning();
		}
	}

	private IEnumerator ShowMusic()
	{
		yield return new WaitForSeconds(1f);
		gameManager.musicManager.PlayNormalMusic(0, 8);
	}

	public void ShowWarning()
	{
		Cursor.visible = false;
		if (I18N.instance.gameLang.Equals(LanguageCode.CN) || I18N.instance.gameLang.Equals(LanguageCode.TC))
		{
			Invoke("StartWarning2", 0.5f);
		}
		else
		{
			Invoke("StartWarning", 0.5f);
		}
		warning01.SetActive(value: true);
		cameraFilterPack_tv_artefact.enabled = true;
	}

	private void StartWarning()
	{
		GetComponent<Animator>().Play("ani_warningcanvas");
	}

	private void StartWarning2()
	{
		GetComponent<Animator>().Play("ani_warningcanvas2");
	}

	private void StartBeginCanvas()
	{
		cameraFilterPack_tv_artefact.enabled = false;
		cameraFilterPack_nightvisionfx.enabled = true;
		cameraFilterPack_tx_distorted.enabled = true;
		gameManager.startAniManager.ChangeScene("Canvas01");
	}

	private void StartMusic()
	{
		StartCoroutine(ShowMusic());
	}
}
