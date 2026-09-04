using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HoldJump : MonoBehaviour
{
	public GameObject loadLine;

	public Image img_loadline;

	public GameObject leftPanel;

	public GameObject rightPanel;

	public MissionResult missionResult;

	private bool isload;

	private void Update()
	{
		if (Input.anyKeyDown)
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
			amount += 0.02f;
			img_loadline.fillAmount = amount;
			if (amount >= 0.98f && !isload)
			{
				isload = true;
				missionResult.StopToResult(isend: true);
			}
		}
	}
}
