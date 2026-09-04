using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NotePanel2 : MonoBehaviour
{
	public Transform[] hopepanels;

	public Transform[] panels;

	public GameManager gameManager;

	public ScrollRect scrollRect;

	public RectTransform contentTransform;

	public RectTransform viewPointTransform;

	public List<NoteItemTitle> noteItemTitles;

	public Text[] titlepanels;

	public Color blackcolor;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		noteItemTitles = new List<NoteItemTitle>();
	}

	public void SetGrayTitle(string hasgroup)
	{
		string[] array = hasgroup.Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			titlepanels[int.Parse(array[i])].fontStyle = FontStyle.Bold;
			titlepanels[int.Parse(array[i])].color = blackcolor;
			titlepanels[int.Parse(array[i])].transform.GetChild(0).gameObject.SetActive(value: false);
		}
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
		GetComponent<RectTransform>().localPosition = new Vector2(438f, 0f);
		GetComponent<RectTransform>().DOLocalMoveX(-5f, 0.5f).OnComplete(delegate
		{
			gameManager.homeScene.notebook.iscanchangetab = true;
		});
		GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
	}

	public void Hide()
	{
		GetComponent<RectTransform>().localPosition = new Vector2(-5f, 0f);
		GetComponent<RectTransform>().DOLocalMoveX(-443f, 0.3f);
		GetComponent<CanvasGroup>().DOFade(0f, 0.3f).OnComplete(delegate
		{
			GetComponent<RectTransform>().localPosition = new Vector2(438f, 0f);
			base.gameObject.SetActive(value: false);
		});
	}

	public void CenterOnItem(RectTransform target)
	{
		Canvas.ForceUpdateCanvases();
		Vector3 worldPointInWidget = GetWorldPointInWidget(scrollRect.GetComponent<RectTransform>(), GetWidgetWorldPointPlusHeight(target));
		Vector3 vector = GetWorldPointInWidget(scrollRect.GetComponent<RectTransform>(), GetWidgetWorldPoint(viewPointTransform)) - worldPointInWidget;
		vector.z = 0f;
		Vector2 vector2 = new Vector2(vector.x / (contentTransform.rect.width - viewPointTransform.rect.width), vector.y / (contentTransform.rect.height - viewPointTransform.rect.height));
		vector2 = scrollRect.normalizedPosition - vector2;
		vector2.x = Mathf.Clamp01(vector2.x);
		vector2.y = Mathf.Clamp01(vector2.y);
		DOTween.To(() => scrollRect.normalizedPosition, delegate(Vector2 x)
		{
			scrollRect.normalizedPosition = x;
		}, vector2, 1f);
		Canvas.ForceUpdateCanvases();
	}

	private Vector3 GetWidgetWorldPoint(RectTransform target)
	{
		Vector3 vector = new Vector3((0.5f - target.pivot.x) * target.rect.size.x, (0.5f - target.pivot.y) * target.rect.size.y, 0f);
		Vector3 position = target.localPosition + vector;
		return target.parent.TransformPoint(position);
	}

	private Vector3 GetWidgetWorldPointPlusHeight(RectTransform target)
	{
		Vector3 vector = new Vector3((0.5f - target.pivot.x) * target.rect.size.x, (0.5f - target.pivot.y) * target.rect.size.y, 0f);
		Vector3 position = target.localPosition + vector + new Vector3(0f, target.sizeDelta.y, 0f);
		return target.parent.TransformPoint(position);
	}

	private Vector3 GetWorldPointInWidget(RectTransform target, Vector3 worldPoint)
	{
		return target.InverseTransformPoint(worldPoint);
	}

	private IEnumerator SetBottom()
	{
		yield return new WaitForSeconds(0.5f);
		scrollRect.normalizedPosition = Vector2.zero;
	}

	private IEnumerator SendMail(string mailid)
	{
		yield return new WaitForSeconds(5f);
		gameManager.homeScene.SendMail(mailid);
	}

	public void CompeleteHope(int hopeid)
	{
		for (int num = panels[hopeid].childCount - 1; num >= 0; num--)
		{
			Object.Destroy(panels[hopeid].GetChild(num).gameObject);
		}
		_ = (GameObject)Object.Instantiate(Resources.Load("noteitem_over"), panels[hopeid]);
		if (!gameManager.player.playerdata.hopelist.Contains(hopeid))
		{
			gameManager.player.playerdata.hopelist.Add(hopeid);
		}
	}

	public void DestroyAllHopeItem()
	{
		for (int i = 0; i < 11; i++)
		{
			for (int num = panels[i].childCount - 1; num >= 0; num--)
			{
				Object.Destroy(panels[i].GetChild(num).gameObject);
			}
			hopepanels[i].gameObject.SetActive(value: false);
			panels[i].gameObject.SetActive(value: false);
		}
	}

	public void DestroyBoomItem(string itemid)
	{
		for (int num = panels[11].childCount - 1; num >= 0; num--)
		{
			if (panels[11].GetChild(num).GetComponent<NoteItem>().itemid.Equals(itemid))
			{
				Object.Destroy(panels[11].GetChild(num).gameObject);
			}
		}
		if (panels[11].childCount == 0)
		{
			hopepanels[11].gameObject.SetActive(value: false);
		}
	}
}
