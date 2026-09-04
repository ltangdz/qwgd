using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HackerButton : MonoBehaviour
{
	[SerializeField]
	private Transform img_whiteblank;

	[SerializeField]
	private Text txt_btn1;

	[SerializeField]
	private Text txt_btn2;

	[SerializeField]
	private Color bluecolor;

	[SerializeField]
	private Color blackcolor;

	private int pos;

	private GameManager gameManager;

	public HackerDialog hackerDialog;

	public bool iscanmove = true;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
	{
		if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && iscanmove)
		{
			if (img_whiteblank.localPosition.y == -58f)
			{
				img_whiteblank.DOLocalMoveY(15f, 0.1f);
				txt_btn2.color = bluecolor;
				txt_btn1.color = blackcolor;
				pos = 0;
			}
		}
		else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) && iscanmove)
		{
			if (img_whiteblank.localPosition.y == 15f)
			{
				img_whiteblank.DOLocalMoveY(-58f, 0.1f);
				txt_btn1.color = bluecolor;
				txt_btn2.color = blackcolor;
				pos = 1;
			}
		}
		else if ((Input.GetKeyDown(KeyCode.KeypadEnter) && iscanmove) || (Input.GetKeyDown(KeyCode.Return) && iscanmove))
		{
			txt_btn2.color = bluecolor;
			txt_btn1.color = bluecolor;
			iscanmove = false;
			switch (pos)
			{
			case 0:
				img_whiteblank.gameObject.SetActive(value: false);
				hackerDialog.StartLoadReLoadSystem();
				break;
			case 1:
				img_whiteblank.gameObject.SetActive(value: false);
				hackerDialog.StartLoadSelectGroup();
				break;
			}
		}
	}
}
