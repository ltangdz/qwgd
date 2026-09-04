using UnityEngine;

public class ButtonBox : MonoBehaviour
{
	public ToolButton btn_sql;

	public ToolButton btn_chat;

	public ToolButton btn_weizhuang;

	public ToolButton btn_dingwei;

	public ToolButton btn_pic;

	public ToolButton btn_pojie;

	public ToolButton btn_cctv;

	public Transform dialogtool;

	public GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void CloseOneTool(int toolid)
	{
		switch (toolid)
		{
		case 0:
			btn_sql.CloseTool();
			break;
		case 4:
			btn_pic.CloseTool();
			break;
		case 5:
			btn_pojie.CloseTool();
			break;
		}
	}

	public void OpenTool(int toolid)
	{
		switch (toolid)
		{
		case 0:
			((GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetSqlDialogName()), dialogtool)).transform.parent.gameObject.SetActive(value: true);
			break;
		case 1:
		{
			GameObject obj3 = (GameObject)Object.Instantiate(Resources.Load("Chat/chatDialog"), dialogtool);
			obj3.transform.parent.gameObject.SetActive(value: true);
			obj3.GetComponent<ChatBox>().Show();
			break;
		}
		case 4:
		{
			GameObject obj2 = (GameObject)Object.Instantiate(Resources.Load("Dialog/scanDialog"), dialogtool);
			obj2.transform.parent.gameObject.SetActive(value: true);
			obj2.GetComponent<ScanDialog>().Show();
			break;
		}
		case 5:
		{
			GameObject obj = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetPasswordDialogName()), dialogtool);
			obj.transform.parent.gameObject.SetActive(value: true);
			obj.GetComponent<PasswordDialog1>().Show();
			break;
		}
		}
		dialogtool.SetAsLastSibling();
	}
}
