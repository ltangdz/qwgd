using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HoldEscVideo : MonoBehaviour
{
	public GameObject loadLine;

	public Image img_loadline;

	private GameManager gameManager;

	public bool isbegin;

	public bool islast;

	public EAendVideo parobj;

	private bool isload;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.Esc = base.gameObject;
	}

	private void Update()
	{
		if (!isbegin && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
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
				gameManager.holdEsc = true;
				parobj.JumpEnd();
			}
		}
	}
}
