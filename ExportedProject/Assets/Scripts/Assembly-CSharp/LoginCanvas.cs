using System.Collections;
using UnityEngine;

public class LoginCanvas : MonoBehaviour
{
	public LoginPanel loginPanel;

	public LoadingPanel loadingPanel;

	public CreateUserPanel createUserPanel;

	public Animator sureWindow;

	private GameManager gameManager;

	public GameObject beginCanvas;

	public Transform panel;

	private GameObject explainAlert_cn;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Init();
		if (!gameManager.isshowexplainalert)
		{
			string text = PlayerPrefs.GetString("isShowedDlcNotice" + gameManager.str_version);
			if (string.IsNullOrEmpty(text) || text != "true")
			{
				PlayerPrefs.SetString("isShowedDlcNotice" + gameManager.str_version, "true");
				explainAlert_cn = Object.Instantiate(Resources.Load<GameObject>("Dialog/dlc6_noticeAlert"), panel);
				explainAlert_cn.SetActive(value: true);
				explainAlert_cn.GetComponent<NoticeAlertDLC>().InitInfo();
			}
			else
			{
				explainAlert_cn = Object.Instantiate(Resources.Load<GameObject>("Dialog/noticeAlert"), panel);
				explainAlert_cn.SetActive(value: true);
				explainAlert_cn.GetComponent<NoticeAlert>().InitInfo();
			}
			gameManager.isshowexplainalert = true;
		}
	}

	private IEnumerator LargeSound()
	{
		float vol = 0f;
		gameManager.musicManager.GetComponent<AudioSource>().volume = vol;
		if (gameManager.player.playerdata == null)
		{
			while (vol < 1f)
			{
				vol += 0.01f;
				yield return new WaitForSeconds(0.05f);
				gameManager.musicManager.GetComponent<AudioSource>().volume = vol;
			}
		}
		else
		{
			while (vol < PlayerPrefs.GetFloat("musicvol", 1f))
			{
				vol += 0.01f;
				yield return new WaitForSeconds(0.05f);
				gameManager.musicManager.GetComponent<AudioSource>().volume = vol;
			}
		}
	}

	private void Init()
	{
		Cursor.visible = true;
	}
}
