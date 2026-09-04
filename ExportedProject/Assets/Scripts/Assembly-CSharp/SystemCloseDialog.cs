using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemCloseDialog : MonoBehaviour
{
	private GameManager gameManager;

	public Transform huaping;

	public Transform img_dialog;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.player.AddEventID(isadd: true);
		gameManager.saveManager.SavePlayerData(isshowlogo: false);
	}

	public void SetLast()
	{
		img_dialog.SetAsLastSibling();
	}

	public void End()
	{
		StartCoroutine(StartEndAni());
	}

	private IEnumerator StartEndAni()
	{
		yield return new WaitForSeconds(8f);
		gameManager.istaohuashow = false;
		gameManager.iscancollect = true;
		yield return new WaitForSeconds(0.5f);
		gameManager.CanShowSetting(-1);
		SceneManager.LoadScene(gameManager.GetHomeSceneName());
	}
}
