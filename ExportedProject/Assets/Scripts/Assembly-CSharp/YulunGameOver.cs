using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YulunGameOver : MonoBehaviour
{
	[SerializeField]
	private Transform img_whiteblank;

	[SerializeField]
	private Text txt_btn1;

	[SerializeField]
	private Text txt_btn2;

	[SerializeField]
	private Shadow shadow_btn1;

	[SerializeField]
	private Shadow shadow_btn2;

	[SerializeField]
	private Color bluecolor;

	[SerializeField]
	private Color blackcolor;

	[SerializeField]
	private Color blueshadowcolor;

	[SerializeField]
	private Color blackshadowcolor;

	private int pos;

	private GameManager gameManager;

	[SerializeField]
	private GameObject img_light;

	[SerializeField]
	private GameObject img_bk;

	public GameObject window;

	private bool windowOpend;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.musicManager.PlayMusicLoop(3);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(img_light.transform.DOScale(10f, 2f).OnComplete(delegate
		{
		}));
		sequence.Append(img_bk.GetComponent<CanvasGroup>().DOFade(1f, 0.3f));
		sequence.Play();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
		{
			if (img_whiteblank.localPosition.y == -58f)
			{
				img_whiteblank.DOLocalMoveY(15f, 0.1f);
				txt_btn2.color = bluecolor;
				shadow_btn2.effectColor = blueshadowcolor;
				txt_btn1.color = blackcolor;
				shadow_btn1.effectColor = blackshadowcolor;
				pos = 0;
			}
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
		{
			if (img_whiteblank.localPosition.y == 15f)
			{
				img_whiteblank.DOLocalMoveY(-58f, 0.1f);
				txt_btn1.color = bluecolor;
				shadow_btn1.effectColor = blueshadowcolor;
				txt_btn2.color = blackcolor;
				shadow_btn2.effectColor = blackshadowcolor;
				pos = 1;
			}
		}
		else
		{
			if (!Input.GetKeyDown(KeyCode.KeypadEnter) && !Input.GetKeyDown(KeyCode.Return))
			{
				return;
			}
			if (windowOpend)
			{
				BakMain();
				return;
			}
			switch (pos)
			{
			case 0:
				Cursor.visible = true;
				gameManager.txt_studio.SetActive(value: false);
				gameManager.istaohuashow = false;
				gameManager.iscancollect = true;
				Object.Instantiate(Resources.Load<GameObject>("Dialog/Yulun/yulunDialog"), gameManager.homeScene.middle);
				Object.Destroy(base.gameObject);
				break;
			case 1:
				windowOpend = true;
				window.SetActive(value: true);
				window.GetComponent<Animator>().Play("Exit Panel In");
				break;
			}
		}
	}

	public void Cancle()
	{
		window.GetComponent<Animator>().Play("Exit Panel Out");
		Invoke("HideWindow", 1f);
		windowOpend = false;
	}

	public void BakMain()
	{
		Cancle();
		gameManager.saveManager.SavePlayerData();
		gameManager.txt_studio.SetActive(value: true);
		SceneManager.LoadScene("mainScene");
		gameManager.soundManager.Stop();
		gameManager.musicManager.PlayMusicLoop(8);
	}

	private void HideWindow()
	{
		window.SetActive(value: false);
	}
}
