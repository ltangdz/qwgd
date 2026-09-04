using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class TijiaoAlertDlc : MonoBehaviour
{
	public Button _okButton;

	public Button _cancelButton;

	private GameManager gameManager;

	public Animator _animator;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.Is_Dlc6())
		{
			if ((bool)_okButton)
			{
				_okButton.onClick.AddListener(Sure);
			}
			if ((bool)_cancelButton)
			{
				_cancelButton.onClick.AddListener(Cancel);
			}
		}
	}

	private void Sure()
	{
		_animator.Play("Exit Panel Out");
		string time = (gameManager.player.playerdata.endTime / 60000).ToString();
		DATA11 dATA = gameManager.dataManager.dic11[gameManager.player.GetEventId()];
		if (gameManager.Is_Dlc6() && gameManager.player.playerdata.itemlist.Count >= dATA.number)
		{
			gameManager.UnlockAchievements("homesweethome");
		}
		int count = gameManager.player.playerdata.itemlist.Count;
		gameManager.player.RefreshLevel(count.ToString(), time);
		Invoke("Call", 1.5f);
		gameManager.homeScene.notebook.btn_submit.interactable = false;
	}

	private void Call()
	{
		gameManager.homeScene.ShowVideoTip("3710003");
	}

	private void Cancel()
	{
		_animator.Play("Exit Panel Out");
	}
}
