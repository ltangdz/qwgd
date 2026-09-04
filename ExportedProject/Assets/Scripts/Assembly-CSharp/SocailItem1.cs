using Honeti;
using UnityEngine;
using UnityEngine.UI;
using tnt_deploy;

public class SocailItem1 : MonoBehaviour
{
	public Image img_avatar;

	public Text txt_name;

	public Text txt_hot;

	public Text txt_read;

	public Text txt_discuss;

	public MultiplyText txt_date;

	public MultiplyText txt_location;

	public Text txt_jiami;

	public Image img_content;

	public GameObject locationGroup;

	public Image img_lock;

	public GameManager gameManager;

	public Transform discussPanel;

	public Sprite[] sprites;

	public Color orangecolor;

	public GameObject titleGroup;

	public Transform contentPanel;

	private bool imgcontentislock;

	private bool isshowBig;

	public string itemid;

	public GameObject canvas;

	public void RefreshContentPanel()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(contentPanel as RectTransform);
	}

	private void Awake()
	{
		canvas = GameObject.Find("Canvas");
	}

	public void ScaleImage()
	{
		_ = imgcontentislock;
	}

	public void Init(string id, bool isadmin, GameManager gameManager)
	{
		itemid = id;
		this.gameManager = gameManager;
		if (!gameManager.dataManager.dic16.ContainsKey(id))
		{
			return;
		}
		DATA16 dATA = gameManager.dataManager.dic16[id];
		txt_name.GetComponent<I18NText>().updateTranslation2(dATA.nickname);
		if (dATA.isimagelock == 1 && !isadmin)
		{
			GameObject obj = Object.Instantiate(Resources.Load("tbtxt_content0") as GameObject, contentPanel);
			obj.GetComponent<Text>().GetComponent<I18NText>().updateTranslation2("^socialPanel01");
			obj.GetComponent<Text>().color = orangecolor;
			LayoutRebuilder.ForceRebuildLayoutImmediate(contentPanel as RectTransform);
		}
		else
		{
			if (dATA.contenthighlight.Equals("#0"))
			{
				string[] array = dATA.content.Split(';');
				for (int i = 0; i < array.Length; i++)
				{
					Object.Instantiate(Resources.Load("tbtxt_content0") as GameObject, contentPanel).GetComponent<Text>().GetComponent<I18NText>()
						.updateTranslation2(array[i]);
				}
			}
			else
			{
				string[] array2 = dATA.contenthighlight.Substring(1).Split(';');
				string[] array3 = dATA.content.Split(';');
				string[] array4 = dATA.highlight.Split(';');
				for (int j = 0; j < array3.Length; j++)
				{
					if (array2[j].Equals("0"))
					{
						Object.Instantiate(Resources.Load("tbtxt_content0") as GameObject, contentPanel).GetComponent<Text>().GetComponent<I18NText>()
							.updateTranslation2(array3[j]);
						continue;
					}
					if (array3[j].Substring(0, 1).Equals("L"))
					{
						Object.Instantiate(Resources.Load("tblinkText") as GameObject, contentPanel).GetComponent<TBHyperLinkText>().Init(array3[j].Substring(1), array2[j]);
						continue;
					}
					GameObject gameObject = Object.Instantiate(Resources.Load("tb_info") as GameObject, contentPanel);
					gameObject.GetComponent<MultiplyText>().SetContentPanel(contentPanel);
					if (array2[j].Contains("*"))
					{
						string[] array5 = array2[j].Split('*');
						Debug.LogError("manyitems：" + array5.Length);
						gameObject.GetComponent<MultiplyText>().otheritem = new string[array5.Length];
						for (int k = 0; k < array5.Length; k++)
						{
							gameObject.GetComponent<MultiplyText>().otheritem[k] = array5[k];
						}
						gameObject.GetComponent<MultiplyText>().SetContent2(array3[j], array5[0], I18N.instance.getValue(array4[j]));
					}
					else
					{
						gameObject.GetComponent<MultiplyText>().SetContent2(array3[j], array2[j], I18N.instance.getValue(array4[j]));
					}
				}
				LayoutRebuilder.ForceRebuildLayoutImmediate(contentPanel as RectTransform);
			}
			img_content.transform.SetAsLastSibling();
		}
		if (isadmin)
		{
			if (dATA.isimagelock == 1)
			{
				txt_jiami.gameObject.SetActive(value: true);
				txt_jiami.GetComponent<I18NText>().updateTranslation2("^socialPanel02");
			}
			else
			{
				txt_jiami.gameObject.SetActive(value: false);
			}
		}
		else
		{
			txt_jiami.gameObject.SetActive(value: false);
		}
		txt_date.SetNewWidth(dATA.date);
		if (!dATA.datehighlight.Equals("#0"))
		{
			txt_date.SetContent3(dATA.date, dATA.datehighlight.Substring(1), dATA.date);
		}
		else
		{
			txt_date.SetContent(dATA.date);
		}
		if (gameManager.GameType == GameTypeEnum.DLC6 || gameManager.GameType == GameTypeEnum.DLC7)
		{
			if (!dATA.location.Trim().Equals("") && !string.IsNullOrEmpty(I18N.instance.getValue(dATA.location)))
			{
				Debug.Log(dATA.location);
				string value = I18N.instance.getValue(dATA.location);
				if (!string.IsNullOrEmpty(value.Trim()))
				{
					txt_location.SetNewWidth(value);
					txt_location.SetContent3(value, dATA.locationhigh.Substring(1), value);
				}
			}
			else
			{
				locationGroup.SetActive(value: false);
			}
		}
		else
		{
			locationGroup.SetActive(value: false);
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(txt_date.transform.parent.GetComponent<RectTransform>());
		int num = int.Parse(dATA.hotcount.Substring(1));
		if (num >= 10000)
		{
			if (I18N.instance.gameLang.Equals(LanguageCode.CN) || I18N.instance.gameLang.Equals(LanguageCode.TC))
			{
				txt_hot.GetComponent<I18NText>().updateTranslation2(num / 10000 + "W+");
			}
			else
			{
				txt_hot.GetComponent<I18NText>().updateTranslation2(num / 1000 + "K+");
			}
		}
		else
		{
			txt_hot.GetComponent<I18NText>().updateTranslation2(dATA.hotcount.Substring(1));
		}
		int num2 = int.Parse(dATA.read.Substring(1));
		if (num2 > 10000)
		{
			if (I18N.instance.gameLang.Equals(LanguageCode.CN) || I18N.instance.gameLang.Equals(LanguageCode.TC))
			{
				txt_read.GetComponent<I18NText>().updateTranslation2(num2 / 10000 + "W+");
			}
			else
			{
				txt_read.GetComponent<I18NText>().updateTranslation2(num2 / 1000 + "K+");
			}
		}
		else
		{
			txt_read.GetComponent<I18NText>().updateTranslation2(dATA.read.Substring(1));
		}
		string[] array6 = dATA.discusscount.Split(';');
		string text = array6[0];
		string text2 = array6[1];
		Debug.Log("hotcount:" + num + "---readcount0:" + num2 + "---pinglun1:" + text + "---pinglun2:" + text2);
		if (int.Parse(text) > 10000)
		{
			text = ((!I18N.instance.gameLang.Equals(LanguageCode.CN) && !I18N.instance.gameLang.Equals(LanguageCode.TC)) ? (int.Parse(text) / 1000 + "K+") : (int.Parse(text) / 10000 + "W+"));
		}
		if (int.Parse(text2) > 10000)
		{
			text2 = ((!I18N.instance.gameLang.Equals(LanguageCode.CN) && !I18N.instance.gameLang.Equals(LanguageCode.TC)) ? (int.Parse(text2) / 1000 + "K+") : (int.Parse(text2) / 10000 + "W+"));
		}
		txt_discuss.GetComponent<I18NText>().updateTranslation2(string.Format(I18N.instance.getValue("^txt_social01"), text, text2));
		string avatar = dATA.avatar;
		avatar = ((avatar.IndexOf("tb") == -1) ? (avatar + "tb") : avatar);
		img_avatar.sprite = Resources.Load<Sprite>("touxiang/" + avatar);
		if (!dATA.image.Equals("#0") && !dATA.image.Equals("#0.0"))
		{
			if (!isadmin)
			{
				imgcontentislock = dATA.isimagelock == 1;
			}
			else
			{
				imgcontentislock = false;
			}
			if (dATA.imagetype == 0)
			{
				img_content.gameObject.SetActive(value: true);
				Sprite imageBig = Resources.Load<Sprite>("Social/" + dATA.image.Substring(1) + (imgcontentislock ? "b" : ""));
				SetImageBig(imageBig);
				img_lock.gameObject.SetActive(imgcontentislock);
				if (dATA.imagehighlight != 0 && !imgcontentislock)
				{
					img_content.GetComponent<HighLightPic>().enabled = !imgcontentislock;
					img_content.GetComponent<HighLightPic>().itemid = dATA.imagehighlight.ToString();
					img_content.GetComponent<HighLightPic>().iscancollect = true;
				}
				else
				{
					img_content.GetComponent<HighLightPic>().enabled = false;
					img_content.GetComponent<HighLightPic>().iscanclick = false;
				}
			}
			else if (dATA.imagetype == 1)
			{
				img_content.gameObject.SetActive(value: false);
				_ = (GameObject)Object.Instantiate(Resources.Load("Social/" + dATA.image.Substring(1)), img_content.transform.parent);
			}
		}
		else
		{
			img_content.gameObject.SetActive(value: false);
		}
		if (dATA.discussid.Equals("") || dATA.discussid == null)
		{
			discussPanel.gameObject.SetActive(value: false);
			return;
		}
		string[] array7 = dATA.discussid.Substring(1).Split(';');
		if (array7.Length != 0)
		{
			titleGroup.SetActive(value: true);
		}
		for (int l = 0; l < array7.Length; l++)
		{
			if (!gameManager.dataManager.dic17.ContainsKey(long.Parse(array7[l]).ToString()))
			{
				Debug.Log(dATA.ID.ToString() + "****" + l + "::" + array7[l] + ":::iddd:" + long.Parse(array7[l]).ToString());
				continue;
			}
			DATA17 dATA2 = gameManager.dataManager.dic17[long.Parse(array7[l]).ToString()];
			if (!(dATA2.Replyname.Trim() == ""))
			{
				continue;
			}
			GameObject gameObject2 = (GameObject)Object.Instantiate(Resources.Load("discussitem1"), discussPanel);
			gameObject2.name = "discussitem" + l;
			gameObject2.GetComponent<DiscussItem>().SetNewWidth(I18N.instance.getValue(dATA2.nickname));
			gameObject2.transform.Find("contentPanel/img_avatar").GetComponent<Image>().sprite = Resources.Load<Sprite>("touxiang/" + dATA2.avatar.Substring(1));
			gameObject2.transform.Find("contentPanel/rightpanel/namepluscontent/txt_name").GetComponent<Text>().GetComponent<I18NText>()
				.updateTranslation2(dATA2.nickname);
			gameObject2.transform.Find("contentPanel/rightpanel/timegroup/txt_time").GetComponent<Text>().GetComponent<Text>()
				.GetComponent<I18NText>()
				.updateTranslation2(dATA2.date);
			string text3 = dATA2.contentid.Substring(1);
			if (!text3.Equals("0"))
			{
				_ = gameManager.dataManager.dic1[text3];
				gameObject2.transform.Find("contentPanel/rightpanel/namepluscontent/txt_content0").GetComponent<MultiplyText>().SetContent2(dATA2.content, text3, I18N.instance.getValue(dATA2.highlight));
			}
			else
			{
				gameObject2.transform.Find("contentPanel/rightpanel/namepluscontent/txt_content0").GetComponent<MultiplyText>().SetContent(dATA2.content, iswarp: true);
			}
			if (gameManager.player.playerdata.isCourse03 == 0 && dATA2.ID.ToString().Equals("1700036"))
			{
				gameManager.homeScene.courseManager.coursepanel03.tbname = gameObject2;
			}
			gameObject2.GetComponent<DiscussItem>().tkid = dATA2.toothbook.Substring(1);
			if (dATA2.discussid.Equals("") || dATA2.discussid == null)
			{
				continue;
			}
			string[] array8 = dATA2.discussid.Substring(1).Split(';');
			for (int m = 0; m < array8.Length; m++)
			{
				if (array8[m].Equals("0"))
				{
					continue;
				}
				if (!gameManager.dataManager.dic17.ContainsKey(array8[m]))
				{
					Debug.Log("17表不含" + array8[m]);
				}
				DATA17 dATA3 = gameManager.dataManager.dic17[array8[m]];
				if (dATA3.disscuss.Equals("#0"))
				{
					if (dATA3.contentid.Equals("#0"))
					{
						GameObject obj2 = (GameObject)Object.Instantiate(Resources.Load("discussitem2"), gameObject2.transform.Find("discussPanel"));
						obj2.name = "2discussitem" + m;
						string text4 = "<color=#4267B2>" + I18N.instance.getValue(dATA3.nickname) + "</color>";
						text4 = text4 + "  " + I18N.instance.getValue("^txt_reply") + " ";
						text4 = text4 + "<color=#4267B2>" + I18N.instance.getValue(dATA3.Replyname) + "</color>  ";
						text4 += I18N.instance.getValue(dATA3.content);
						obj2.transform.Find("txt_content0").GetComponent<Text>().GetComponent<I18NText>()
							.updateTranslation2(text4);
						obj2.transform.Find("txt_content1").gameObject.SetActive(value: false);
					}
					else
					{
						DATA1 dATA4 = gameManager.dataManager.dic1[dATA3.contentid.Substring(1)];
						GameObject obj3 = (GameObject)Object.Instantiate(Resources.Load("discussitem2"), gameObject2.transform.Find("discussPanel"));
						obj3.name = "2discussitem" + m;
						string text5 = "<color=#4267B2>" + I18N.instance.getValue(dATA3.nickname) + "</color>";
						text5 = text5 + "  " + I18N.instance.getValue("^txt_reply") + " ";
						text5 = text5 + "<color=#4267B2>" + I18N.instance.getValue(dATA3.Replyname) + "</color>  ";
						text5 += I18N.instance.getValue(dATA3.content);
						obj3.transform.Find("txt_content1").GetComponent<MultiplyText>().SetContent3(text5, dATA4.ID.ToString(), I18N.instance.getValue(dATA4.message));
						obj3.transform.Find("txt_content0").gameObject.SetActive(value: false);
					}
				}
			}
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(contentPanel as RectTransform);
	}

	private void SetImageContentSize(Sprite sprite)
	{
		if (sprite.rect.width >= sprite.rect.height)
		{
			img_content.sprite = sprite;
			img_content.GetComponent<RectTransform>().sizeDelta = new Vector2(187f, 120f);
			img_content.transform.Find("img_light").GetComponent<RectTransform>().sizeDelta = new Vector2(202f, 135f);
		}
		else
		{
			img_content.sprite = sprite;
			img_content.GetComponent<RectTransform>().sizeDelta = new Vector2(120f, 152f);
			img_content.transform.Find("img_light").GetComponent<RectTransform>().sizeDelta = new Vector2(135f, 167f);
		}
	}

	private void StartBig()
	{
		Vector2 sizeDelta = img_content.GetComponent<RectTransform>().sizeDelta;
		Vector2 sizeDelta2 = img_content.transform.Find("img_light").GetComponent<RectTransform>().sizeDelta;
		if (img_content.sprite.rect.width >= img_content.sprite.rect.height)
		{
			img_content.transform.Find("img_light").GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDelta2.x += 35.2f, sizeDelta2.y += 23.3f);
			img_content.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDelta.x += 35.2f, sizeDelta.y += 23.3f);
		}
		else
		{
			img_content.transform.Find("img_light").GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDelta2.x += 23.3f, sizeDelta2.y += 29f);
			img_content.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDelta.x += 23.3f, sizeDelta.y += 29f);
		}
		if (img_content.GetComponent<RectTransform>().sizeDelta.x >= 350f && img_content.GetComponent<RectTransform>().sizeDelta.y >= 350f / img_content.sprite.rect.width * img_content.sprite.rect.height)
		{
			CancelInvoke();
		}
	}

	private void SetImageBig(Sprite sprite)
	{
		img_content.sprite = sprite;
		if (sprite != null)
		{
			if (img_content.sprite.rect.width >= img_content.sprite.rect.height)
			{
				img_content.GetComponent<RectTransform>().sizeDelta = new Vector2(350f, 350f / img_content.sprite.rect.width * img_content.sprite.rect.height);
			}
			else
			{
				img_content.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 200f / img_content.sprite.rect.width * img_content.sprite.rect.height);
			}
		}
	}

	private void SetImageBig2(GameObject sprite)
	{
		GameObject gameObject = Object.Instantiate(sprite, img_content.transform);
		if (sprite != null)
		{
			if (gameObject.GetComponent<RectTransform>().rect.width >= gameObject.GetComponent<RectTransform>().rect.height)
			{
				img_content.GetComponent<RectTransform>().sizeDelta = new Vector2(400f, 400f / gameObject.GetComponent<RectTransform>().rect.width * gameObject.GetComponent<RectTransform>().rect.height);
				gameObject.GetComponent<RectTransform>().localScale = new Vector2(400f / gameObject.GetComponent<RectTransform>().rect.width, 400f / gameObject.GetComponent<RectTransform>().rect.width);
			}
			else
			{
				img_content.GetComponent<RectTransform>().sizeDelta = new Vector2(350f, 350f / gameObject.GetComponent<RectTransform>().rect.width * gameObject.GetComponent<RectTransform>().rect.height);
				gameObject.GetComponent<RectTransform>().localScale = new Vector2(350f / gameObject.GetComponent<RectTransform>().rect.width, 350f / gameObject.GetComponent<RectTransform>().rect.width);
			}
		}
	}

	private void StartSmall()
	{
		Vector2 sizeDelta = img_content.GetComponent<RectTransform>().sizeDelta;
		Vector2 sizeDelta2 = img_content.transform.Find("img_light").GetComponent<RectTransform>().sizeDelta;
		if (img_content.sprite.rect.width >= img_content.sprite.rect.height)
		{
			img_content.transform.Find("img_light").GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDelta2.x -= 35.2f, sizeDelta2.y -= 23.3f);
			img_content.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDelta.x -= 35.2f, sizeDelta.y -= 23.3f);
			if (img_content.GetComponent<RectTransform>().sizeDelta.x <= 187f && img_content.GetComponent<RectTransform>().sizeDelta.y <= 120f)
			{
				CancelInvoke();
			}
		}
		else
		{
			img_content.transform.Find("img_light").GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDelta.x -= 23.3f, sizeDelta.y -= 29f);
			img_content.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDelta.x -= 23.3f, sizeDelta.y -= 29f);
			if (img_content.GetComponent<RectTransform>().sizeDelta.x <= 120f && img_content.GetComponent<RectTransform>().sizeDelta.y <= 156f)
			{
				CancelInvoke();
			}
		}
	}

	public void AddDragPic()
	{
		GameObject obj = (GameObject)Object.Instantiate(Resources.Load("img_dragpic"), base.transform);
		Vector2 vector = Input.mousePosition - new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.5f);
		obj.transform.localPosition = vector / canvas.transform.localScale.x;
	}
}
