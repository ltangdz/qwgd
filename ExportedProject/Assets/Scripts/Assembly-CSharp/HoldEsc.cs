using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HoldEsc : MonoBehaviour
{
	public GameObject loadLine;

	public Image img_loadline;

	public string sceneName = "warning";

	private GameManager gameManager;

	public bool isbegin;

	public bool islast;

	public EndPanel endPanel;

	private bool isload;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.Esc = base.gameObject;
	}

	private void Update()
	{
		if (!isbegin)
		{
			if (Input.anyKeyDown && !sceneName.Equals("warning") && !sceneName.Equals("Canvas09") && !sceneName.Equals(""))
			{
				GetComponent<CanvasGroup>().alpha = 1f;
				StartCoroutine(EscLoad());
			}
		}
		else if (Input.anyKeyDown)
		{
			GetComponent<CanvasGroup>().alpha = 1f;
			StartCoroutine(EscLoad());
		}
		if (!Input.anyKey)
		{
			StopAllCoroutines();
			GetComponent<CanvasGroup>().alpha = 0f;
			img_loadline.fillAmount = 0f;
		}
	}

	private IEnumerator EscLoad()
	{
		float amount = img_loadline.fillAmount;
		while (amount < 0.98f)
		{
			yield return new WaitForSeconds(0.02f);
			amount += 0.06f;
			img_loadline.fillAmount = amount;
			if (!(amount >= 0.98f) || isload)
			{
				continue;
			}
			isload = true;
			gameManager.holdEsc = true;
			if (!isbegin)
			{
				if (!sceneName.Equals("Canvas09"))
				{
					if (gameManager.startAniManager != null)
					{
						gameManager.startAniManager.ChangeScene("Canvas09", isjump: true);
					}
					base.gameObject.SetActive(value: false);
				}
			}
			else if (islast)
			{
				endPanel.JumpTo();
			}
			else
			{
				gameManager.ShowFloatBox();
				Invoke("ChangeScene", 2f);
			}
		}
	}

	private void ChangeScene()
	{
		gameManager.musicManager.Stop();
		gameManager.txt_studio.SetActive(value: false);
		SceneManager.LoadScene("homecourse");
	}
}
