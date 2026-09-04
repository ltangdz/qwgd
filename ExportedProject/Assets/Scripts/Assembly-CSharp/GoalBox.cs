using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class GoalBox : MonoBehaviour
{
	public Dictionary<string, MissionItem> missionlists = new Dictionary<string, MissionItem>();

	public Dictionary<string, MissionBaseItem> basemissionlists = new Dictionary<string, MissionBaseItem>();

	public Transform[] basePanels;

	public GameObject[] basePanelLines;

	private GameManager gameManager;

	private List<DATA20> lists;

	private List<DATA20> baselists;

	public TypewriterEffect txt_no;

	public Image img_avatar;

	public Image img_code;

	public Button btn_sumbit;

	public AccuracyUI accuracyUI;

	public float percent;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		baselists = gameManager.dataManager.GetAllBaseMissionItems(gameManager.player.GetEventId());
		btn_sumbit.onClick.AddListener(delegate
		{
			((GameObject)Object.Instantiate(Resources.Load("Dialog/missionresultDialog"), base.transform.parent)).transform.GetChild(0).GetComponent<MissionresultDialog>().Show();
		});
	}

	private void Update()
	{
	}

	public void Init()
	{
		for (int i = 0; i < lists.Count; i++)
		{
			DATA20 dATA = lists[i];
			if (!missionlists.ContainsKey(dATA.ID.ToString()))
			{
				GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("missionitem01"), basePanels[dATA.pos]);
				gameObject.GetComponent<MissionItem>().SetInitContent(dATA);
				missionlists.Add(dATA.ID.ToString(), gameObject.GetComponent<MissionItem>());
				basePanels[dATA.pos].parent.gameObject.SetActive(value: true);
				basePanelLines[dATA.pos - 1].gameObject.SetActive(value: true);
			}
		}
		for (int j = 1; j < basePanels.Length; j++)
		{
			basePanels[j].parent.gameObject.SetActive(basePanels[j].childCount != 0);
			if (basePanels[j].childCount != 0)
			{
				basePanels[j].transform.parent.GetComponent<MissionPanel>().OpenPanel(isop: true);
			}
			basePanelLines[j - 1].gameObject.SetActive(basePanels[j].childCount != 0);
		}
	}

	public void InitBase()
	{
		txt_no.StartEffect(gameManager.player.playerdata.eventno);
		for (int i = 0; i < baselists.Count; i++)
		{
			DATA20 dATA = baselists[i];
			if (dATA.pos != 8)
			{
				GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("missionbaseitem"), basePanels[0]);
				gameObject.name = "missionbaseitem" + i;
				gameObject.GetComponent<MissionBaseItem>().InitContent(dATA);
				if (dATA.pos == 9)
				{
					basemissionlists.Add(dATA.ID.ToString(), gameObject.GetComponent<MissionBaseItem>());
				}
			}
			else
			{
				img_avatar.transform.parent.GetComponent<MissionBaseItem>().InitContent(dATA);
				basemissionlists.Add(dATA.ID.ToString(), img_avatar.GetComponent<MissionBaseItem>());
			}
		}
	}

	public void CompleteItem(string id)
	{
		string[] array = id.Split(';');
		float num = 0f;
		for (int i = 0; i < array.Length; i++)
		{
			if (missionlists.ContainsKey(array[i]))
			{
				missionlists[array[i]].CompeleteMission();
				num += float.Parse(missionlists[array[i]].date20.percent.Substring(1));
			}
			if (basemissionlists.ContainsKey(array[i]))
			{
				basemissionlists[array[i]].CompleteBaseMission();
				num += float.Parse(basemissionlists[array[i]].data20.percent.Substring(1));
			}
		}
		accuracyUI.FreshAddAcc(num);
		percent += num;
		if (percent >= 100f)
		{
			Invoke("StartSubmit", 1.5f);
		}
	}

	private void StartSubmit()
	{
		btn_sumbit.gameObject.SetActive(value: true);
		accuracyUI.transform.parent.gameObject.SetActive(value: false);
	}
}
