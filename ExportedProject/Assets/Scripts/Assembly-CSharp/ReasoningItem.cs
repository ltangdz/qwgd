using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReasoningItem : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public Image img_bk;

	public float delay;

	public float speed;

	public string imgname = "";

	public GameObject arrowgroup01;

	public GameObject arrowgroup02;

	public string itemid;

	public Text txt_content;

	public bool iscanclick = true;

	public float movetoy;

	public string tishi;

	public string labelinfo;

	public GameObject tishiBox;

	private GameManager gameManager;

	public Text tishiLabel;

	private void Refresh()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (!gameManager.player.playerdata.itemlist.Contains(itemid) && !gameManager.isbug)
		{
			int num = Random.Range(5, 10);
			string text = "";
			for (int i = 0; i < num; i++)
			{
				text += "? ";
			}
			txt_content.GetComponent<I18NText>().updateTranslation2(text);
			txt_content.fontSize = 36;
			iscanclick = false;
		}
		else
		{
			txt_content.GetComponent<I18NText>().updateTranslation2(labelinfo);
		}
	}

	public void StartMoveLeft()
	{
		base.transform.DOKill();
		base.transform.DOLocalMoveX(movetoy, 1f).OnComplete(delegate
		{
			Vector3 localPosition = base.transform.localPosition;
			Vector3[] array = new Vector3[5]
			{
				localPosition,
				new Vector3(localPosition.x + 10f, localPosition.y + 15f, localPosition.z),
				new Vector3(localPosition.x + 30f, localPosition.y, localPosition.z),
				new Vector3(localPosition.x + 10f, localPosition.y - 15f, localPosition.z),
				localPosition
			};
			Vector3[] array2 = new Vector3[5]
			{
				localPosition,
				new Vector3(localPosition.x - 10f, localPosition.y - 15f, localPosition.z),
				new Vector3(localPosition.x - 30f, localPosition.y, localPosition.z),
				new Vector3(localPosition.x - 10f, localPosition.y + 15f, localPosition.z),
				localPosition
			};
			int num = Random.Range(0, 2);
			base.transform.DOLocalPath((num == 0) ? array : array2, speed).SetLoops(-1);
		});
	}

	public void Init()
	{
		Refresh();
		Invoke("StartMove", delay);
	}

	private void StartMove()
	{
		Vector3 localPosition = base.transform.localPosition;
		Vector3[] path = new Vector3[5]
		{
			localPosition,
			new Vector3(localPosition.x + 10f, localPosition.y + 15f, localPosition.z),
			new Vector3(localPosition.x + 30f, localPosition.y, localPosition.z),
			new Vector3(localPosition.x + 10f, localPosition.y - 15f, localPosition.z),
			localPosition
		};
		Vector3[] path2 = new Vector3[5]
		{
			localPosition,
			new Vector3(localPosition.x - 10f, localPosition.y - 15f, localPosition.z),
			new Vector3(localPosition.x - 30f, localPosition.y, localPosition.z),
			new Vector3(localPosition.x - 10f, localPosition.y + 15f, localPosition.z),
			localPosition
		};
		int r = Random.Range(0, 2);
		base.transform.GetComponent<CanvasGroup>().DOFade(1f, 0.5f).OnComplete(delegate
		{
			base.transform.DOLocalPath((r == 0) ? path : path2, speed).SetLoops(-1);
		});
	}

	public void GetOut()
	{
		StartCoroutine(StartGetOut());
	}

	private IEnumerator StartGetOut()
	{
		float seconds = Random.Range(0.2f, 0.6f);
		yield return new WaitForSeconds(seconds);
		base.transform.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).OnComplete(delegate
		{
			StopAllCoroutines();
			Object.Destroy(base.gameObject);
		});
	}

	public void StopMove()
	{
		if (!iscanclick)
		{
			tishiLabel.GetComponent<I18NText>().updateTranslation2(tishi);
			tishiBox.SetActive(value: true);
		}
		img_bk.color = Color.white;
		arrowgroup01.SetActive(value: true);
		arrowgroup02.SetActive(value: true);
		base.transform.DOPause();
	}

	public void ContinueMove()
	{
		if (tishiBox.activeInHierarchy)
		{
			tishiBox.SetActive(value: false);
		}
		img_bk.color = new Color(1f, 1f, 1f, 0f);
		arrowgroup01.SetActive(value: false);
		arrowgroup02.SetActive(value: false);
		base.transform.DOPlay();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (iscanclick)
		{
			if (gameManager.IsAllDlc())
			{
				ReasonPicDlc component = ((GameObject)Object.Instantiate(Resources.Load("Image/reason_dlc"), base.transform.parent.parent)).GetComponent<ReasonPicDlc>();
				component.itemId = int.Parse(itemid);
				component.InitData();
				component.Show();
			}
			else
			{
				((GameObject)Object.Instantiate(Resources.Load("Image/" + imgname), base.transform.parent.parent)).GetComponent<ReasonPic>().Show();
			}
		}
	}
}
