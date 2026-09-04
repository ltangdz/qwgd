using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class VideoDialog00Last : MonoBehaviour
{
	public Text txt_name;

	public Text txt_zimu2;

	public string[] zimus;

	public string[] replys;

	public HomeScene homeScene;

	public Image img_mouse;

	public Button btn_ringoff;

	public GameManager gameManager;

	public float pos;

	public string dataid;

	public string mailid;

	public SelectGroup selectGroup;

	public bool iscanclick = true;

	private bool hundown;

	public SpriteAnimation ashley;

	[SerializeField]
	private bool isSaying;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.CanShowSetting(1);
		gameManager.homeScene.eventsystem.SetActive(value: false);
		gameManager.homeScene.computerButtonBox.iscanclick = false;
		gameManager.musicManager.LowerVol();
		Init(dataid);
	}

	public void openClick()
	{
		gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	public void Init(string dataid)
	{
		homeScene = gameManager.homeScene;
		gameManager.musicManager.LowerVol();
		Invoke("ClickZimu", 1.5f);
	}

	private void Update()
	{
		if ((Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) && !selectGroup.gameObject.activeSelf)
		{
			ClickZimu();
		}
	}

	public void ClickZimu()
	{
		img_mouse.gameObject.SetActive(value: true);
		iscanclick = false;
		iscanclick = true;
		if (pos < (float)zimus.Length)
		{
			if (!isSaying)
			{
				isSaying = true;
				ashley.SetState(1);
				StopAllCoroutines();
				txt_zimu2.GetComponent<Text>().text = "";
				gameManager.soundManager.Stop();
				float num = 0f;
				if (zimus.Length >= 1)
				{
					num = gameManager.soundManager.PlayEventFinished("110000", (int)pos + 15);
					StartCoroutine(AudioPlayFinished(num));
				}
				float num2 = gameManager.CalculateLengthOfText(string.Format(I18N.instance.getValue(zimus[(int)pos].Trim()), gameManager.player.playerdata.nickname), txt_zimu2);
				if (num2 < 1650f)
				{
					txt_zimu2.GetComponent<RectTransform>().sizeDelta = new Vector2(num2, 100f);
				}
				else
				{
					txt_zimu2.GetComponent<RectTransform>().sizeDelta = new Vector2(1650f, 100f);
				}
				num = ((num > 0.3f) ? (num - 0.3f) : num);
				txt_zimu2.DOText(string.Format(I18N.instance.getValue(zimus[(int)pos].Trim()), gameManager.player.playerdata.nickname), num).SetEase(Ease.Linear).OnComplete(delegate
				{
					pos += 1f;
					isSaying = false;
				});
			}
			else
			{
				txt_zimu2.DOKill();
				isSaying = false;
				txt_zimu2.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue(zimus[(int)pos].Trim()), gameManager.player.playerdata.nickname));
				pos += 1f;
			}
		}
		else if (pos == (float)zimus.Length)
		{
			SetSelect(0, 1);
		}
		else if (!hundown)
		{
			ashley.SetState(0);
			hundown = true;
			btn_ringoff.interactable = true;
			btn_ringoff.GetComponent<Animator>().Play("ani_breath");
			txt_zimu2.text = "";
			gameManager.soundManager.Stop();
			gameManager.soundManager.PlaySound(20);
			GetComponent<Animator>().Play("ani_videoHide");
			btn_ringoff.onClick.AddListener(delegate
			{
				gameManager.musicManager.ResumeVol();
				gameManager.homeScene.notebook.StartCloseAllDialog0();
				txt_zimu2.text = "";
				gameManager.soundManager.Stop();
				gameManager.soundManager.PlaySound(20);
				GetComponent<Animator>().Play("ani_videoHide");
			});
		}
	}

	private IEnumerator AudioPlayFinished(float time)
	{
		yield return new WaitForSeconds(time);
		ashley.SetState(0);
	}

	private IEnumerator LoadScene()
	{
		yield return new WaitForSeconds(3f);
		gameManager.homeScene.notebook.StartCloseAllDialog();
	}

	private void SetSelect(int begin, int end)
	{
		StartCoroutine(StartSetSelect(begin, end));
	}

	private IEnumerator StartSetSelect(int begin, int end)
	{
		yield return new WaitForSeconds(0f);
		if (begin < 0)
		{
			yield return new WaitForSeconds(1f);
		}
		string[] array = new string[end - begin];
		for (int i = begin; i < end; i++)
		{
			array[i - begin] = replys[i];
		}
		selectGroup.gameObject.SetActive(value: true);
		selectGroup.SetSelect(array, ClickSelect);
	}

	public void ClickSelect(int poss)
	{
		if (selectGroup.iscanclick)
		{
			gameManager.soundManager.Stop();
			iscanclick = true;
			pos += 0.5f;
			ClickZimu();
			selectGroup.HideSelect();
		}
	}

	public void HideVideoDialog()
	{
		gameManager.musicManager.ResumeVol();
		gameManager.CanShowSetting(-1);
		gameManager.homeScene.computerButtonBox.iscanclick = true;
		Object.Instantiate(Resources.Load("Dialog/missionresultDialog") as GameObject, base.transform.parent);
		gameManager.homeScene.ShowNextVideo();
		Object.Destroy(base.gameObject);
	}
}
