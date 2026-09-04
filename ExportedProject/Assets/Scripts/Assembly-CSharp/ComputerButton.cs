using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ComputerButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public Animator img_tip;

	public ComputerButtonBox buttonbox;

	public int tool;

	public bool isnotebook;

	private GameManager gameManager;

	public Image img_red;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (tool == 6)
		{
			gameManager.homeScene.computerButton = this;
		}
		GetComponent<Button>().onClick.AddListener(delegate
		{
			SelectTool();
		});
	}

	public void ShowRed(bool isshow)
	{
		img_red.gameObject.SetActive(isshow);
	}

	public void ShowNoteDialog()
	{
		base.transform.DOScale(Vector3.zero, 0.3f);
	}

	public void HideNoteDialog()
	{
		base.transform.DOScale((gameManager.GameType == GameTypeEnum.DLC7) ? Vector3.one : new Vector3(1.5f, 1.5f, 1.5f), 0.3f);
	}

	public void SelectTool()
	{
		if (isnotebook)
		{
			ShowNoteDialog();
		}
		buttonbox.FrontTool(tool);
	}

	public void SelectTool(int tool)
	{
		if (isnotebook)
		{
			ShowNoteDialog();
		}
		buttonbox.FrontTool(tool);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}
}
