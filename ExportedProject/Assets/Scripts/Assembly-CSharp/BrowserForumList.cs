using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrowserForumList : MonoBehaviour
{
	public string timeMonth;

	public string timeDay;

	public string title;

	public string sender;

	public string info;

	public string img;

	public List<string> comment;

	public List<string> commenterName;

	public List<string> commenterTime;

	public List<string> userComment;

	public GameObject bbs;

	public GameObject bbsInfo;

	public BrowserCampus parObj;

	public GameObject content;

	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		GetComponent<Button>().onClick.AddListener(delegate
		{
			bbs.SetActive(value: false);
			bbsInfo.SetActive(value: true);
			Canvas.ForceUpdateCanvases();
			content.transform.parent.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;
			Canvas.ForceUpdateCanvases();
			parObj.openObj = bbsInfo;
			bbsInfo.GetComponent<BrowserCampusCommentInfo>().Init(this, gameManager);
		});
	}
}
