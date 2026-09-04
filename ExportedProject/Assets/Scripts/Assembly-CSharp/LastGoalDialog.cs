using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class LastGoalDialog : MonoBehaviour
{
	public Text txt_title;

	public Text txt_count;

	public Image img_red;

	public VerticalLayoutGroup vGroup;

	public Dictionary<string, GoalItem> goalitemlist;

	public Dictionary<string, int> lists;

	public GameManager gameManager;

	public uint currentpos;

	public List<DATA20> allperiodlist;

	public float percent;

	public int count;

	public CoursePanel coursePanel;

	public Button btn_arrow;

	public Image img_arrow;

	public List<Transform> task;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.player.playerdata.InitMissionList(gameManager.dataManager.GetMissionItems(gameManager.player.GetEventId()));
		goalitemlist = new Dictionary<string, GoalItem>();
		allperiodlist = gameManager.dataManager.GetAllMissionItems2(gameManager.player.GetEventId());
		lists = gameManager.dataManager.GetCurrentMissionItems(gameManager.player.GetEventId());
		if (gameManager.homeScene.isgoaldialogalpha)
		{
			GetComponent<Animator>().enabled = false;
			GetComponent<CanvasGroup>().alpha = 0f;
		}
		btn_arrow.onClick.AddListener(delegate
		{
			if (vGroup.GetComponent<RectTransform>().localScale.y == 1f)
			{
				img_arrow.rectTransform.localScale = new Vector2(1f, -1f);
				vGroup.GetComponent<RectTransform>().DOScaleY(0f, 0.3f).SetEase(Ease.InOutCirc);
				vGroup.GetComponent<CanvasGroup>().DOFade(0f, 0.3f).SetEase(Ease.InOutCirc);
			}
			else if (vGroup.GetComponent<RectTransform>().localScale.y == 0f)
			{
				img_arrow.rectTransform.localScale = new Vector2(1f, 1f);
				vGroup.GetComponent<RectTransform>().DOScaleY(1f, 0.3f).SetEase(Ease.InOutCirc);
				vGroup.GetComponent<CanvasGroup>().DOFade(1f, 0.3f).SetEase(Ease.InOutCirc);
			}
		});
	}

	public void CompeleteOneItem(string id)
	{
		goalitemlist[id].CompeleteMission();
		SetFront();
	}

	public void HideAnimator()
	{
		GetComponent<Animator>().enabled = false;
	}

	public void SetFront()
	{
		base.transform.parent.SetAsLastSibling();
	}

	public void AddGoal()
	{
		StartCoroutine(StartAddGoal());
	}

	private IEnumerator StartAddGoal()
	{
		foreach (KeyValuePair<string, int> list in lists)
		{
			InitGoalItem(list.Key, list.Value, lists.Count);
			yield return new WaitForSeconds(0.3f);
		}
		gameManager.homeScene.notebook.InitItems();
		if (gameManager.homeScene.zhibojiannotebook != null)
		{
			gameManager.homeScene.zhibojiannotebook.InitItems();
		}
	}

	private GoalItem InitGoalItem(string id, int state, int totalcount)
	{
		DATA20 dATA = gameManager.dataManager.dic20[id];
		if (goalitemlist.ContainsKey(id))
		{
			return null;
		}
		GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(DLCNameUtil.Instance.GetGoalitemName()), vGroup.transform);
		GameObject gameObject2 = gameObject.transform.GetChild(1).gameObject;
		task.Add(gameObject.transform.Find("goalitem"));
		goalitemlist.Add(dATA.ID.ToString(), gameObject2.GetComponent<GoalItem>());
		gameObject2.GetComponent<GoalItem>().SetState(state, dATA);
		gameObject.GetComponent<Animator>().Play("ani_goalitemshow");
		RefreshCount(totalcount);
		return gameObject2.GetComponent<GoalItem>();
	}

	public void CompletePercentItem(string id, float percent)
	{
		if (!id.Equals("0") && goalitemlist.ContainsKey(id) && goalitemlist[id].state != 2)
		{
			goalitemlist[id].AddPercent(percent);
			SetFront();
		}
	}

	public void CompeleteGoal(string id)
	{
		count++;
		RefreshCount(goalitemlist.Count);
		SetFront();
	}

	private void RefreshCount(int totalcount)
	{
		if (count > totalcount)
		{
			txt_count.GetComponent<I18NText>().updateTranslation2(totalcount + "/" + totalcount);
		}
		else
		{
			txt_count.GetComponent<I18NText>().updateTranslation2(count + "/" + totalcount);
		}
	}

	public void CompleteItem(string id)
	{
		if (id.Equals("0"))
		{
			return;
		}
		string[] array = id.Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			if (!goalitemlist.ContainsKey(array[i]))
			{
				gameManager.player.playerdata.CompleteMissionItem(array[i]);
			}
			else if (goalitemlist[array[i]].state != 2)
			{
				txt_title.GetComponent<I18NText>().updateTranslation2(goalitemlist[array[i]].data20.period);
				SetFront();
			}
		}
	}

	private IEnumerator StartShowEnd()
	{
		foreach (KeyValuePair<string, GoalItem> item in goalitemlist)
		{
			item.Value.RemoveItem();
			yield return new WaitForSeconds(1f);
		}
		goalitemlist.Clear();
		txt_title.GetComponent<I18NText>().updateTranslation2("^taskdialog01");
	}

	public void ShowEnd()
	{
		StartCoroutine(StartShowEnd());
	}
}
