using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using tnt_deploy;

public class HomeBrowser : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public Text txt_searchcontent;

	public string searchcontent;

	public Button btn_search;

	public NewBrowserDialog newbrowserDialog;

	public InputField inputField;

	public Transform focus;

	public Sprite[] sprites;

	public GameManager gameManager;

	public GameObject inputpanel;

	public Favourite fav;

	private bool _isEnter;

	private void OnDisable()
	{
		NoteDragManager.Instance.onDragStart -= OnDragStart;
		NoteDragManager.Instance.onDraging -= OnDraging;
		NoteDragManager.Instance.onDragEnd -= OnDragEnd;
	}

	private void OnDragStart(PointerEventData eventData, DATA1 data)
	{
	}

	private void OnDraging(PointerEventData eventData, DATA1 data)
	{
	}

	private void OnDragEnd(PointerEventData eventData, DATA1 data)
	{
		if (_isEnter)
		{
			inputField.text = I18N.instance.getValue(data.message);
			search();
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_isEnter = false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_isEnter = true;
	}

	private void OnEnable()
	{
		NoteDragManager.Instance.onDragStart += OnDragStart;
		NoteDragManager.Instance.onDraging += OnDraging;
		NoteDragManager.Instance.onDragEnd += OnDragEnd;
		fav.ResetPosition();
	}

	public void Course01()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.player.playerdata.isCourse01 == 0)
		{
			StartCoroutine(AddCanvas());
			gameManager.homeScene.courseManager.coursepanel01.browser_search = inputField.gameObject;
		}
	}

	private IEnumerator AddCanvas()
	{
		yield return new WaitForSeconds(0.2f);
		inputpanel.AddComponent<Canvas>().overrideSorting = true;
		inputpanel.GetComponent<Canvas>().sortingOrder = 3;
		inputpanel.AddComponent<GraphicRaycaster>();
	}

	private void Start()
	{
		fav.ResetPosition();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		btn_search.onClick.AddListener(delegate
		{
			search();
		});
		inputField.onValueChanged.AddListener(delegate
		{
			SetButtonStatus();
		});
	}

	private void search()
	{
		if (gameManager.player.playerdata.isCourse01 != 0 || inputField.text.ToLower().Equals("tc191"))
		{
			if (gameManager.player.playerdata.isCourse01 == 0)
			{
				gameManager.homeScene.courseManager.coursepanel01.HideCourse();
			}
			searchcontent = txt_searchcontent.text;
			newbrowserDialog.AddSearchItem(searchcontent);
			inputField.text = "";
		}
	}

	private void SetButtonStatus()
	{
		Debug.Log("SetButtonStatus" + inputField.text);
		btn_search.transform.Find("img_arrow").GetComponent<Image>().sprite = sprites[(!inputField.text.Equals("")) ? 1u : 0u];
	}

	private void Update()
	{
		if ((!Input.GetKeyUp(KeyCode.Return) && !Input.GetKeyUp(KeyCode.KeypadEnter)) || !newbrowserDialog.isclick)
		{
			return;
		}
		float num = newbrowserDialog.transform.GetSiblingIndex();
		float num2 = newbrowserDialog.transform.parent.childCount;
		string text = inputField.text;
		if (base.gameObject.activeInHierarchy && num == num2 - 1f && text != "" && text != " " && (gameManager.player.playerdata.isCourse01 != 0 || inputField.text.ToLower().Equals("tc191")))
		{
			if (gameManager.player.playerdata.isCourse01 == 0)
			{
				gameManager.homeScene.courseManager.coursepanel01.HideCourse();
			}
			searchcontent = txt_searchcontent.text;
			newbrowserDialog.AddSearchItem(searchcontent);
			inputField.text = "";
		}
	}
}
