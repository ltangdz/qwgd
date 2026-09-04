using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class ZhadanPos : MonoBehaviour
{
	public string id;

	public List<Transform> car;

	public List<Line> line;

	public Transform posArrow;

	public List<Transform> posQuan;

	public List<float> speed;

	public Text pos;

	public Sprite succArrow;

	public Sprite succLine;

	private GameManager gameManager;

	private List<IEnumerator> carRunList = new List<IEnumerator>();

	private List<Vector3> carPosition = new List<Vector3>();

	private Sprite failedArrow;

	private Sprite failedQuan;

	public void Init(string itemid)
	{
		id = itemid;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		string message = gameManager.dataManager.dic1[id].message;
		pos.GetComponent<I18NText>().updateTranslation2(message);
		pos.transform.parent.gameObject.SetActive(value: true);
		for (int i = 0; i < posQuan.Count; i++)
		{
			posQuan[i].gameObject.SetActive(value: true);
		}
		if (failedArrow == null)
		{
			failedArrow = posArrow.GetComponent<Image>().sprite;
		}
		else
		{
			posArrow.GetComponent<Image>().sprite = failedArrow;
		}
		if (failedQuan == null)
		{
			failedQuan = posQuan[0].GetComponent<Image>().sprite;
		}
		else
		{
			for (int j = 0; j < posQuan.Count; j++)
			{
				posQuan[j].GetComponent<Image>().sprite = failedQuan;
			}
		}
		for (int k = 0; k < posQuan.Count; k++)
		{
			posQuan[k].gameObject.SetActive(value: true);
		}
		posArrow.gameObject.SetActive(value: true);
		StartCoroutine(ShowLianyi());
		StartCoroutine(ShowArrowAni());
		for (int l = 0; l < line.Count; l++)
		{
			for (int m = 0; m < line[l].lineList.Count; m++)
			{
				line[l].lineList[m].GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
				line[l].lineList[m].gameObject.SetActive(value: true);
			}
		}
		if (car.Count == 0 || line.Count == 0 || speed.Count == 0)
		{
			return;
		}
		for (int n = 0; n < car.Count; n++)
		{
			if (carPosition.Count < car.Count)
			{
				carPosition.Add(car[n].GetComponent<RectTransform>().localPosition);
			}
			else
			{
				car[n].GetComponent<RectTransform>().localPosition = carPosition[n];
			}
			IEnumerator enumerator = CarMove(n);
			StartCoroutine(enumerator);
			carRunList.Add(enumerator);
		}
	}

	public void PojieSucc()
	{
		for (int i = 0; i < car.Count; i++)
		{
			StopCoroutine(carRunList[i]);
			car[i].DOKill();
		}
		for (int j = 0; j < line.Count; j++)
		{
			for (int k = 0; k < line[j].lineList.Count; k++)
			{
				line[j].lineList[k].gameObject.SetActive(value: false);
			}
		}
		for (int l = 0; l < posQuan.Count; l++)
		{
			posQuan[l].GetComponent<Image>().sprite = succLine;
		}
		posArrow.GetComponent<Image>().sprite = succArrow;
		Invoke("Hide", 5f);
	}

	public void Hide()
	{
		StopAllCoroutines();
		for (int i = 0; i < line.Count; i++)
		{
			for (int j = 0; j < line[i].lineList.Count; j++)
			{
				line[i].lineList[j].gameObject.SetActive(value: false);
			}
		}
		posArrow.gameObject.SetActive(value: false);
		for (int k = 0; k < posQuan.Count; k++)
		{
			posQuan[k].gameObject.SetActive(value: false);
		}
		for (int l = 0; l < car.Count; l++)
		{
			car[l].DOKill();
		}
		pos.transform.parent.gameObject.SetActive(value: false);
	}

	private IEnumerator ShowLianyi()
	{
		while (true)
		{
			for (int i = 0; i < posQuan.Count; i++)
			{
				int s = i;
				posQuan[s].DOScale(Vector3.one, 3f).OnComplete(delegate
				{
					posQuan[s].localScale = Vector3.zero;
				});
				yield return new WaitForSeconds(1.6f);
				posQuan[s].GetComponent<Image>().DOFade(0f, 1.4f).OnComplete(delegate
				{
					posQuan[s].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
				});
			}
		}
	}

	private IEnumerator ShowArrowAni()
	{
		float height = posArrow.GetComponent<RectTransform>().sizeDelta.y;
		float posY = posArrow.GetComponent<RectTransform>().localPosition.y;
		while (true)
		{
			posArrow.DOLocalMoveY(posY + height / 2f, 0.5f);
			yield return new WaitForSeconds(0.5f);
			posArrow.DOLocalMoveY(posY, 0.5f);
			yield return new WaitForSeconds(0.5f);
		}
	}

	private IEnumerator CarMove(int i)
	{
		for (int j = 0; j < line[i].lineList.Count; j++)
		{
			float num = line[i].lineList[j].GetComponent<RectTransform>().sizeDelta.y / speed[i];
			Vector3 localPosition = line[i].lineList[j].localPosition;
			car[i].DOLocalMove(localPosition, num).SetEase(Ease.Linear);
			line[i].lineList[j].DOScaleY(0f, num).SetEase(Ease.Linear);
			yield return new WaitForSeconds(num);
		}
	}
}
