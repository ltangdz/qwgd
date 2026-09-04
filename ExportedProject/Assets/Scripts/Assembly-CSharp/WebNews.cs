using System.Collections;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class WebNews : MonoBehaviour
{
	public Transform content;

	public Text title;

	public Text time;

	public Image newsImg;

	public DATA13 data13;

	public GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void Init(DATA13 d13)
	{
		data13 = d13;
		title.GetComponent<I18NText>().updateTranslation2(d13.title);
		time.GetComponent<I18NText>().updateTranslation2(d13.newsTime);
		if (d13.picname.Contains("*"))
		{
			if (I18N.instance.gameLang.Equals(LanguageCode.CN))
			{
				newsImg.sprite = Resources.Load<Sprite>("News/" + d13.picname.Substring(1).Replace("*", "") + "_CN");
			}
			else if (I18N.instance.gameLang.Equals(LanguageCode.TC))
			{
				newsImg.sprite = Resources.Load<Sprite>("News/" + d13.picname.Substring(1).Replace("*", "") + "_TC");
			}
			else
			{
				newsImg.sprite = Resources.Load<Sprite>("News/" + d13.picname.Substring(1).Replace("*", "") + "_EN");
			}
		}
		else
		{
			newsImg.sprite = Resources.Load<Sprite>("News/" + d13.picname.Substring(1));
		}
		newsImg.SetNativeSize();
		string[] array = d13.arrowid.Split(';');
		string[] array2 = d13.arrowidhighlight.Substring(1).Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			if (!array2[i].Equals("0"))
			{
				if (array[i].Substring(0, 1).Equals("L"))
				{
					Object.Instantiate(Resources.Load("webnewslinkText") as GameObject, content).GetComponent<TBHyperLinkText>().Init(array[i].Substring(1), array2[i]);
					continue;
				}
				GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("txt_info"), content);
				if (!array2[i].Contains("*"))
				{
					DATA1 dATA = gameManager.dataManager.dic1[array2[i]];
					Debug.Log("keyword:" + I18N.instance.getValue(dATA.message));
					if (d13.highlight == "")
					{
						gameObject.GetComponent<MultiplyText>().SetContent2(array[i], array2[i], I18N.instance.getValue(dATA.message));
						continue;
					}
					gameObject.GetComponent<MultiplyText>().SetContent2(array[i], array2[i], I18N.instance.getValue(d13.highlight.Split(';')[i]));
				}
				else if (d13.highlight != "")
				{
					gameObject.GetComponent<MultiplyText>().otheritem = array2[i].Split('*');
					gameObject.GetComponent<MultiplyText>().SetContent2(array[i], array2[i].Split('*')[0], I18N.instance.getValue(d13.highlight.Split(';')[i]));
				}
			}
			else
			{
				((GameObject)Object.Instantiate(Resources.Load("txt_info0"), content)).transform.GetChild(0).GetComponent<I18NText>().updateTranslation2(array[i]);
			}
		}
	}

	private IEnumerator ShowWeb()
	{
		yield return new WaitForSeconds(0.1f);
		GetComponent<Image>().DOFillAmount(1f, 0.1f).SetEase(Ease.InOutCirc);
	}

	private IEnumerator InitContent(MultiplyText MultiplyText, string content, string itemid, string keyword)
	{
		yield return new WaitForSeconds(0.2f);
		MultiplyText.AddContent(I18N.instance.getValue(content), itemid, keyword, istypeeffect: false);
	}

	public void Init(string titleLabel, string timeLabel, string imgName, string news)
	{
		title.GetComponent<I18NText>().updateTranslation2(titleLabel);
		time.GetComponent<I18NText>().updateTranslation2(timeLabel);
		newsImg.sprite = Resources.Load<Sprite>("News/" + imgName);
		newsImg.SetNativeSize();
	}
}
