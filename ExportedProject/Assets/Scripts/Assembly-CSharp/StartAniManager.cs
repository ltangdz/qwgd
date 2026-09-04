using System.Collections;
using UnityEngine;

public class StartAniManager : MonoBehaviour
{
	public GameObject beginningCanvas;

	private GameManager gameManager;

	private GameObject crtCanvas;

	public GameObject holdesc;

	public CameraFilterPack_NightVisionFX cameraFilterPack_NightVisionFX;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void ChangeScene(string sceneName, bool isjump = false)
	{
		if (holdesc != null && !holdesc.GetComponent<HoldEsc>().enabled)
		{
			holdesc.GetComponent<HoldEsc>().enabled = true;
		}
		gameManager.ShowFloatBox();
		StartCoroutine(Change(sceneName, isjump));
	}

	private IEnumerator Change(string sceneName, bool isjump)
	{
		if (sceneName != "BeginingCanvas")
		{
			yield return new WaitForSeconds(3f);
			if (crtCanvas != null)
			{
				Object.Destroy(crtCanvas);
			}
			crtCanvas = Object.Instantiate(Resources.Load<GameObject>("Canvas/" + sceneName), base.transform);
			if (sceneName.Equals("Canvas09"))
			{
				crtCanvas.GetComponent<BeginCanvas>().isjump = isjump;
				cameraFilterPack_NightVisionFX.enabled = true;
			}
			if (sceneName != "Canvas04")
			{
				if (sceneName == "Canvas06")
				{
					crtCanvas.transform.GetChild(0).GetComponent<Canvas>().worldCamera = gameManager.startMainCanvas;
					crtCanvas.transform.GetChild(1).GetComponent<Canvas>().worldCamera = gameManager.startMainCanvas;
				}
				else if (crtCanvas.GetComponent<Canvas>() != null)
				{
					crtCanvas.GetComponent<Canvas>().worldCamera = gameManager.startMainCanvas;
				}
				else
				{
					crtCanvas.transform.GetChild(0).GetComponent<Canvas>().worldCamera = gameManager.startMainCanvas;
				}
			}
		}
		else
		{
			yield return new WaitForSeconds(1.5f);
			if (crtCanvas != null)
			{
				Object.Destroy(crtCanvas);
			}
			if (beginningCanvas != null)
			{
				beginningCanvas.SetActive(value: true);
			}
		}
	}
}
