using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScanPicDialog : MonoBehaviour
{
	public GameObject[] points;

	public ScanDialog scanDialog;

	public GameObject clickPanel;

	public Canvas canvas;

	public Button btn_yes;

	public Button btn_no;

	public Button btn_close;

	public bool iscanclick = true;

	private Vector2 _pos;

	private GameObject no_result;

	private int currentpointpos = -1;

	private void Start()
	{
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		btn_yes.onClick.AddListener(ShowCorrert);
		btn_no.onClick.AddListener(delegate
		{
			iscanclick = true;
			clickPanel.GetComponent<Animator>().Play("ani_hideclickPanel");
		});
		btn_close.onClick.AddListener(delegate
		{
			if (scanDialog != null)
			{
				scanDialog.ClosePic();
			}
		});
	}

	public void ChangeContent()
	{
	}

	private void ShowAllPoint()
	{
	}

	public void ShowClick(int pos)
	{
		if (scanDialog != null)
		{
			scanDialog.transform.SetAsLastSibling();
		}
		if (no_result != null)
		{
			Object.Destroy(no_result);
		}
		if (iscanclick)
		{
			currentpointpos = pos;
			_pos = Vector2.one;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out _pos);
			clickPanel.GetComponent<Animator>().Play("ani_showclickPanel");
			iscanclick = false;
			clickPanel.GetComponent<RectTransform>().position = new Vector2(_pos.x, _pos.y - 50f);
		}
	}

	public void ShowCorrert()
	{
		clickPanel.GetComponent<Animator>().Play("ani_hideclickPanel");
		scanDialog.ShowScan((currentpointpos != -1) ? true : false);
	}

	public void ShowPoint()
	{
		iscanclick = true;
		if (currentpointpos != -1 && currentpointpos < points.Length)
		{
			points[currentpointpos].SetActive(value: true);
			points[currentpointpos].GetComponent<Animator>().Play("ani_scanpoint");
		}
	}

	public void ShowNoClub()
	{
		GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Scan/no_result"), base.transform);
		gameObject.GetComponent<RectTransform>().position = new Vector2(_pos.x, _pos.y);
		no_result = gameObject;
		StartCoroutine(HideNoClub());
	}

	private IEnumerator HideNoClub()
	{
		yield return new WaitForSeconds(3f);
		if (no_result != null)
		{
			Object.Destroy(no_result);
		}
	}
}
