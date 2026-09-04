using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
	public Button bakBtn;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		bakBtn.onClick.AddListener(BakBtn);
	}

	private void BakBtn()
	{
		GetComponent<Animator>().SetBool("closeSetting", value: true);
		Invoke("HideSetting", 1f);
	}

	private void HideSetting()
	{
		base.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Delete) || Input.GetMouseButtonDown(1))
		{
			BakBtn();
		}
	}
}
