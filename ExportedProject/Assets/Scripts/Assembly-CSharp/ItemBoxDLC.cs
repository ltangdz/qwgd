using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class ItemBoxDLC : ItemBox
{
	private void AddHighLight()
	{
		if (base.gameObject.AddComponent<Canvas>() == null)
		{
			base.gameObject.AddComponent<Canvas>().overrideSorting = true;
		}
		base.gameObject.GetComponent<Canvas>().sortingOrder = 9;
	}

	private void DeleteHighLight()
	{
		Object.Destroy(base.gameObject.GetComponent<GraphicRaycaster>());
		Object.Destroy(base.gameObject.GetComponent<Canvas>());
	}

	private void AddNoteItem(string id)
	{
		if (!gameManager.player.playerdata.itemlist.Contains(id) && !gameManager.player.playerdata.temporaryhopelist.Contains(id))
		{
			AddItem(id, isadd: true);
		}
	}

	public new void Show()
	{
		if (!isshow)
		{
			oldpos = new Vector3(750f, -140f, 0f);
			img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(750f, -140f, 0f), 0.5f);
			isshow = true;
			base.transform.SetAsLastSibling();
		}
	}

	public new void ShowSide()
	{
		if (isshow)
		{
			btn_note.ShowNoteDialog();
			img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(750f, -140f, 0f), 0.5f);
			base.transform.SetAsLastSibling();
		}
	}

	private void ShowCodeDialog(bool isneedhighlight = false)
	{
		if (codeDialog == null)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("Dialog/codeDialog"), base.transform.parent);
			codeDialog = gameObject.GetComponent<CodeDialog>();
			if (isneedhighlight)
			{
				gameObject.AddComponent<Canvas>().overrideSorting = true;
				gameObject.GetComponent<Canvas>().sortingOrder = 9;
			}
		}
	}

	public new void HideCodeDialog(string itemid = "")
	{
		if (codeDialog != null)
		{
			if (itemid.Equals("10453"))
			{
				codeDialog.ShowRed();
			}
			else
			{
				Object.Destroy(codeDialog.gameObject);
			}
		}
	}

	public new IEnumerator ShowNormalAdd(string id)
	{
		Vector3 oldpos = img_drag.GetComponent<RectTransform>().localPosition;
		base.transform.SetAsLastSibling();
		if (!isshow)
		{
			btn_note.ShowNoteDialog();
			img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(750f, -140f, 0f), 0.3f);
			yield return new WaitForSeconds(0.5f);
		}
		img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(200f, 0f, 0f), 0.3f);
		yield return new WaitForSeconds(0.3f);
		ShowCodeDialog();
		AddNoteItem(id);
		yield return new WaitForSeconds(3.5f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		img_drag.GetComponent<RectTransform>().DOLocalMove(oldpos, 0.5f);
		DATA1 dATA = gameManager.dataManager.dic1[id];
		if (!dATA.missionID.Equals("#0") && gameManager.homeScene.goalDialog != null)
		{
			string[] array = dATA.missionID.Substring(1).Split(';');
			string[] array2 = dATA.aimspercent.Substring(1).Split(';');
			for (int i = 0; i < array.Length; i++)
			{
				gameManager.homeScene.goalDialog.CompletePercentItem(array[i], float.Parse(array2[i]));
			}
		}
		if (isshow)
		{
			btn_note.ShowNoteDialog();
		}
		else
		{
			btn_note.HideNoteDialog();
		}
	}

	public new IEnumerator ShowAdd(string id, bool isneedhighlight)
	{
		if (gameManager.player.playerdata.itemlist.Contains(id) || gameManager.player.playerdata.temporaryhopelist.Contains(id))
		{
			yield break;
		}
		Vector3 oldpos = img_drag.GetComponent<RectTransform>().localPosition;
		base.transform.SetAsLastSibling();
		if (!isshow)
		{
			btn_note.ShowNoteDialog();
			if ((gameManager.homeScene.iszhibojian && iszhibojianitembox) || (!gameManager.homeScene.iszhibojian && !iszhibojianitembox))
			{
				img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(750f, -140f, 0f), 0.3f);
			}
			yield return new WaitForSeconds(0.5f);
		}
		if ((gameManager.homeScene.iszhibojian && iszhibojianitembox) || (!gameManager.homeScene.iszhibojian && !iszhibojianitembox))
		{
			img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(200f, 0f, 0f), 0.3f);
		}
		SceneLarge();
		yield return new WaitForSeconds(0.3f);
		ShowCodeDialog(isneedhighlight);
		AddNoteItem(id);
		yield return new WaitForSeconds(3f);
		SceneNormal();
		yield return new WaitForSeconds(1.5f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		if ((gameManager.homeScene.iszhibojian && iszhibojianitembox) || (!gameManager.homeScene.iszhibojian && !iszhibojianitembox))
		{
			img_drag.GetComponent<RectTransform>().DOLocalMove(oldpos, 0.5f).OnComplete(delegate
			{
				if (isneedhighlight)
				{
					DeleteHighLight();
				}
			});
		}
		DATA1 data1 = gameManager.dataManager.dic1[id];
		yield return new WaitForSeconds(0.5f);
		gameManager.homeScene.ShowNextVideo();
		Debug.Log("shownext");
		if (isshow)
		{
			btn_note.ShowNoteDialog();
		}
		else
		{
			btn_note.HideNoteDialog();
		}
		if (gameManager.player.playerdata.isstartgetemailitem == 0)
		{
			gameManager.homeScene.StartTask2();
			gameManager.player.playerdata.isstartgetemailitem = 1;
		}
		if (!data1.missionID.Equals("#0") && gameManager.homeScene.goalDialog != null)
		{
			string[] array = data1.missionID.Substring(1).Split(';');
			string[] array2 = data1.aimspercent.Substring(1).Split(';');
			for (int num = 0; num < array.Length; num++)
			{
				gameManager.homeScene.goalDialog.CompletePercentItem(array[num], float.Parse(array2[num]));
			}
		}
		if (gameManager.homeScene.invadePhoneDialog != null)
		{
			gameManager.homeScene.invadePhoneDialog.RefreshCount();
		}
	}

	public new IEnumerator ShowFirstAdd(string id)
	{
		oldpos = img_drag.GetComponent<RectTransform>().localPosition;
		base.transform.SetAsLastSibling();
		if (!isshow)
		{
			btn_note.ShowNoteDialog();
			img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(750f, -140f, 0f), 0.3f);
			yield return new WaitForSeconds(0.5f);
		}
		img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(200f, 0f, 0f), 0.3f);
		ShowCodeDialog();
		AddNoteItem(id);
		float num = 1f;
		yield return new WaitForSeconds(num + 2f + 2f);
		gameManager.homeScene.courseManager.ShowTuli1();
		yield return new WaitForSeconds(0.3f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
	}

	private IEnumerator ShowFirstAdd2()
	{
		gameManager.homeScene.eventsystem.SetActive(value: false);
		yield return new WaitForSeconds(0.5f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		gameManager.homeScene.courseManager.ShowCourse1();
		img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(750f, -140f, 0f), 0.5f);
		DATA1 dATA = gameManager.dataManager.dic1["10057"];
		if (!dATA.missionID.Equals("#0") && gameManager.homeScene.goalDialog != null)
		{
			string[] array = dATA.missionID.Substring(1).Split(';');
			string[] array2 = dATA.aimspercent.Substring(1).Split(';');
			for (int i = 0; i < array.Length; i++)
			{
				gameManager.homeScene.goalDialog.CompletePercentItem(array[i], float.Parse(array2[i]));
			}
		}
	}

	public new IEnumerator ShowManyAdd(string[] ids, bool isneedhighlight)
	{
		Vector3 oldpos = img_drag.GetComponent<RectTransform>().localPosition;
		base.transform.SetAsLastSibling();
		if (!isshow)
		{
			btn_note.ShowNoteDialog();
			img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(750f, -140f, 0f), 0.3f);
			yield return new WaitForSeconds(0.5f);
		}
		img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(200f, 0f, 0f), 0.3f);
		SceneLarge();
		yield return new WaitForSeconds(0.3f);
		ShowCodeDialog(isneedhighlight);
		for (int i = 0; i < ids.Length; i++)
		{
			if (gameManager.player.playerdata.itemlist.Contains(ids[i]))
			{
				continue;
			}
			AddNoteItem(ids[i]);
			if (gameManager.dataManager.dic1.ContainsKey(ids[i]))
			{
				DATA1 dATA = gameManager.dataManager.dic1[ids[i]];
				if (!dATA.missionID.Equals("#0") && gameManager.homeScene.goalDialog != null)
				{
					string[] array = dATA.missionID.Substring(1).Split(';');
					string[] array2 = dATA.aimspercent.Substring(1).Split(';');
					for (int j = 0; j < array.Length; j++)
					{
						gameManager.homeScene.goalDialog.CompletePercentItem(array[j], float.Parse(array2[j]));
					}
				}
			}
			yield return new WaitForSeconds(1f);
		}
		SceneNormal();
		yield return new WaitForSeconds(0.5f);
		HideCodeDialog();
		gameManager.homeScene.eventsystem.SetActive(value: true);
		img_drag.GetComponent<RectTransform>().DOLocalMove(oldpos, 0.5f).OnComplete(delegate
		{
			if (isneedhighlight)
			{
				DeleteHighLight();
			}
		});
		gameManager.saveManager.SavePlayerData();
		yield return new WaitForSeconds(0.5f);
		gameManager.homeScene.ShowNextVideo();
		if (isshow)
		{
			btn_note.ShowNoteDialog();
		}
		else
		{
			btn_note.HideNoteDialog();
		}
	}

	public new void Hide()
	{
		if (isshow)
		{
			img_drag.GetComponent<RectTransform>().localPosition = new Vector3(1251f, -140f, 0f);
			isshow = false;
			gameManager.soundManager.PlaySound(7);
			btn_note.HideNoteDialog();
		}
	}

	public new void HideAll()
	{
		if (isshow)
		{
			img_drag.GetComponent<RectTransform>().localPosition = new Vector3(gameManager.IsAllDlc() ? 1251f : 1231f, -140f, 0f);
			isshow = false;
		}
	}

	public new IEnumerator ShowLastAdd(string id)
	{
		oldpos = img_drag.GetComponent<RectTransform>().localPosition;
		base.transform.SetAsLastSibling();
		if (!isshow)
		{
			btn_note.ShowNoteDialog();
			img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(750f, -140f, 0f), 0.3f);
			yield return new WaitForSeconds(0.5f);
		}
		img_drag.GetComponent<RectTransform>().DOLocalMove(new Vector3(200f, 0f, 0f), 0.3f);
		ShowCodeDialog(isneedhighlight: true);
		AddNoteItem(id);
		if (gameManager.dataManager.dic1.ContainsKey(id))
		{
			DATA1 dATA = gameManager.dataManager.dic1[id];
			if (!dATA.missionID.Equals("#0") && gameManager.homeScene.goalDialog != null)
			{
				string[] array = dATA.missionID.Substring(1).Split(';');
				string[] array2 = dATA.aimspercent.Substring(1).Split(';');
				for (int i = 0; i < array.Length; i++)
				{
					gameManager.homeScene.goalDialog.CompletePercentItem(array[i], float.Parse(array2[i]));
				}
			}
		}
		yield return new WaitForSeconds(5.3f);
		gameManager.homeScene.eventsystem.SetActive(value: true);
		DeleteHighLight();
	}

	public new void DeleteHopePanel()
	{
		foreach (KeyValuePair<string, NoteTab> item in tablist)
		{
			if (int.Parse(item.Key) >= 3100036 && int.Parse(item.Key) < 3100046)
			{
				item.Value.notePanel.DestroyAllHopeItem();
			}
		}
		foreach (KeyValuePair<string, NoteTab> item2 in tablist)
		{
			if (int.Parse(item2.Key) >= 3100036 && int.Parse(item2.Key) < 3100046)
			{
				alltablist.Remove(item2.Value);
				Object.Destroy(item2.Value.gameObject);
			}
		}
		tablist.Clear();
	}
}
