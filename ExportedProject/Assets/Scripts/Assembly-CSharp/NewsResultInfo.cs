using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class NewsResultInfo : MonoBehaviour
{
	public Text txt_data;

	private Animator ani;

	public Button sureBtn;

	public MissionResult missionResult;

	public Button rightCloseBtn;

	private GameManager gameManager;

	private DataManager dataManager;

	public Transform contentPanel;

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		dataManager = gameManager.dataManager;
		ani = base.transform.Find("bk").GetComponent<Animator>();
	}

	public void ComepleteDialog()
	{
		Init();
	}

	public void NotCompleteDialog()
	{
		Init();
	}

	private void ComMask()
	{
	}

	private IEnumerator CloseRightBox()
	{
		ani.Play("ani_newsRight0");
		yield return new WaitForSeconds(0.8f);
		base.gameObject.SetActive(value: false);
	}

	private void Init()
	{
		DATA11 dATA = dataManager.dic11[gameManager.player.GetEventId()];
		_ = gameManager.player.playerdata.startTime;
		_ = gameManager.player.playerdata.endTime;
		if (dATA.lastresult.Equals("#0"))
		{
			return;
		}
		string[] array = dATA.lastresult.Substring(1).Split(';');
		string[] array2 = dATA.lastresultcontent.Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			bool flag = true;
			string[] array3 = array[i].Split('*');
			for (int j = 0; j < array3.Length; j++)
			{
				flag = gameManager.player.playerdata.itemlist.Contains(array3[j]);
				if (!flag)
				{
					break;
				}
			}
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("resultitem"), contentPanel);
			if (flag)
			{
				gameObject.GetComponent<ResultItem>().Init(I18N.instance.getValue("^resulttitle") + (i + 1), I18N.instance.getValue(array2[i]));
			}
			else
			{
				gameObject.GetComponent<ResultItem>().Init(I18N.instance.getValue("^resulttitle") + (i + 1), "? ? ?");
			}
		}
	}
}
