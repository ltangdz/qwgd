using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoTip : MonoBehaviour
{
	public Button btn_call;

	public Text txt_name;

	public Image img_avatar;

	public HomeScene homeScene;

	public GameManager gameManager;

	public string[] videodialogstring;

	public int videodialogtype;

	public GameObject noClick;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void SetTip(string name, string img_avatar, int type)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		videodialogtype = type;
		base.transform.SetAsLastSibling();
		GetComponent<Animator>().Play("ani_videotip");
		gameManager.CanShowSetting(1);
		noClick.SetActive(value: true);
		btn_call.onClick.RemoveAllListeners();
		gameManager.soundManager.PlaySoundLoop(2);
		StartCoroutine(GetPhone());
		txt_name.text = name;
		txt_name.GetComponent<I18NText>().updateTranslation2(name);
		this.img_avatar.sprite = Resources.Load<Sprite>("touxiang/" + img_avatar);
		this.img_avatar.rectTransform.sizeDelta = new Vector2(135f, 135f);
	}

	private IEnumerator GetPhone()
	{
		yield return new WaitForSeconds(0.2f);
		btn_call.onClick.AddListener(delegate
		{
			if (SceneManager.GetActiveScene().name.Equals("homecourse") && gameManager.issteam && gameManager.steamAchi != null)
			{
				gameManager.homeScene.isopenachi = false;
			}
			if (!(homeScene.middle.transform.Find(videodialogstring[videodialogtype] + ((videodialogtype == 0) ? gameManager.player.GetEventId() : "") + "(Clone)") != null))
			{
				gameManager.soundManager.Stop();
				GameObject gameObject = null;
				if (gameManager.Is_Dlc7())
				{
					gameObject = Object.Instantiate(Resources.Load<GameObject>("Dialog/VideoDialogCustomDLC7"), homeScene.middle);
					gameObject.GetComponent<VideoDialogCustom>().Init("3910001", showName: true, isStartTask: true, "1510056");
				}
				else
				{
					gameObject = Object.Instantiate(Resources.Load<GameObject>("Dialog/" + videodialogstring[videodialogtype] + ((videodialogtype == 0) ? gameManager.player.GetEventId() : "")), homeScene.middle);
				}
				gameObject.SetActive(value: true);
				gameManager.CanShowSetting(-1);
				GetComponent<Animator>().Play("ani_hidevideotip");
				noClick.SetActive(value: false);
			}
		});
	}
}
