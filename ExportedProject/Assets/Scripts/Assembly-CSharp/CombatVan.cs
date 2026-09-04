using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class CombatVan : MonoBehaviour
{
	public GameObject van;

	public Van van1;

	public Transform bk;

	public Transform imgTop;

	public Transform imgBottom;

	public Transform content;

	public ScrollRect scrollRect;

	public Text zimu;

	public List<string> vanSay = new List<string>();

	public List<string> yuyin = new List<string>();

	public DuikangDialog duikangDialog;

	public SelectGroup selectGroup;

	public List<string> replys;

	public CombatLoad combatLoad;

	public GameObject vandis;

	public GameObject tabbak;

	public List<string> signup;

	private bool inputDel;

	private Coroutine lgt;

	private float time = 0.8f;

	private bool canShowVan = true;

	private Transform delInput;

	private Coroutine vanSayCor;

	private GameManager gameManager;

	private int pos;

	public float percent;

	private I18NText zhadanlabel;

	private Button hongmo;

	private bool iscanclick = true;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		imgBottom.DOLocalMoveY(-478.5f, 0.8f);
		imgTop.DOLocalMoveY(478.5f, 0.8f);
		bk.DOScale(Vector3.one, 0.3f).OnComplete(delegate
		{
			StartCoroutine(SignUpCodeRun(0, 8, 2f));
		});
	}

	private void CanInputDel()
	{
		delInput = Object.Instantiate(Resources.Load<Transform>("Duikang/inputbox"), content);
		delInput.Find("InputField").GetComponent<InputField>().text = "";
		delInput.Find("InputField").GetComponent<InputField>().ActivateInputField();
		inputDel = true;
		GoBottom();
		if (pos != 1 && pos != 0)
		{
			return;
		}
		delInput.Find("InputField").GetComponent<InputField>().onValueChanged.AddListener(delegate
		{
			if (delInput.GetSiblingIndex() != content.childCount - 1)
			{
				Object.Destroy(content.GetChild(content.childCount - 1).gameObject);
			}
		});
	}

	private IEnumerator SignUpCodeRun(int startpos, int lastpos, float waitTime = 0f)
	{
		yield return new WaitForSeconds(waitTime);
		if (pos == 1 && startpos == 8)
		{
			tabbak.SetActive(value: true);
			for (int i = 0; i < content.childCount; i++)
			{
				Object.Destroy(content.GetChild(i).gameObject);
			}
		}
		for (int j = startpos; j < startpos + lastpos; j++)
		{
			string text = ((signup[j].IndexOf(";") > -1) ? signup[j].Split(';')[0] : signup[j]);
			Transform trm = null;
			if (text.IndexOf("img:") == -1)
			{
				trm = Object.Instantiate(Resources.Load<Transform>("Duikang/inputbox1"), content);
			}
			if (text.IndexOf("^") > -1)
			{
				RunCode(I18N.instance.getValue(text), trm);
				if (canShowVan)
				{
					canShowVan = false;
					van.GetComponent<RectTransform>().DOScaleY(0.8f, 0.3f);
					float vanSayTime = 0f;
					VanSay(I18N.instance.getValue(vanSay[0]), 0, out vanSayTime);
				}
			}
			else if (text.IndexOf("img:") > -1)
			{
				string text2 = text.Split(':')[1];
				hongmo = Object.Instantiate(Resources.Load<Button>("Duikang/" + text2), content);
				hongmo.onClick.AddListener(delegate
				{
					hongmo.GetComponent<Button>().enabled = false;
					Transform inputObj = Object.Instantiate(Resources.Load<Transform>("Duikang/inputbox2"), content);
					RunCode(I18N.instance.getValue("^vd0565"), inputObj);
					StartCoroutine(SignUpCodeRun(16, 1, time));
				});
			}
			else
			{
				RunCode(text, trm);
			}
			if (pos == 1 && startpos == 8)
			{
				float num = Mathf.Floor(45 / (startpos + lastpos));
				percent += num;
				combatLoad.SetPercent(percent, time);
			}
			yield return new WaitForSeconds(time);
			if (signup[j].IndexOf(";") > -1)
			{
				CombatInvoke combatInvoke = Object.Instantiate(Resources.Load<CombatInvoke>("Duikang/combatInvoke"), trm);
				combatInvoke.combatVan = this;
				combatInvoke.Init(signup[j].Split(';')[1]);
				zhadanlabel = trm.Find("Text").GetComponent<I18NText>();
			}
		}
		if (startpos == 0 || startpos == 16)
		{
			CanInputDel();
		}
		iscanclick = true;
	}

	private IEnumerator DelCodeRun(float waittime)
	{
		yield return new WaitForSeconds(waittime - 1f);
		StopVan();
		combatLoad.SetPercent(100f, 5f);
		yield return new WaitForSeconds(5f);
		Transform inputObj = Object.Instantiate(Resources.Load<Transform>("Duikang/inputbox1"), content);
		RunCode(I18N.instance.getValue(signup[18]), inputObj);
		yield return new WaitForSeconds(time);
		pos = 3;
	}

	private void StopVan()
	{
		vandis.SetActive(value: true);
		if (vanSayCor != null)
		{
			StopCoroutine(vanSayCor);
		}
		gameManager.soundManager.Stop();
		zimu.GetComponent<I18NText>().updateTranslation2("");
		if (yuyin.Count != 0)
		{
			gameManager.soundManager.GetComponent<AudioSource>().pitch = 0.5f;
			float num = gameManager.soundManager.PlayEventFinished(gameManager.player.GetEventId(), int.Parse(yuyin[yuyin.Count - 1].Split(':')[1]));
			Invoke("RetypeSound", num);
		}
		Invoke("HideVan", 4f);
	}

	private void RetypeSound()
	{
		gameManager.soundManager.GetComponent<AudioSource>().pitch = 1f;
	}

	private void HideVan()
	{
		Object.Destroy(van);
		vandis.SetActive(value: false);
	}

	private IEnumerator VanLastWords(int startword, int lastword)
	{
		for (int i = startword; i < startword + lastword; i++)
		{
			float vanSayTime = 10f;
			VanSay(I18N.instance.getValue(vanSay[i]), i, out vanSayTime);
			if (pos == 0)
			{
				float num = Mathf.Floor(30 / lastword);
				percent += num;
				combatLoad.SetPercent(percent, vanSayTime + 0.1f);
			}
			else if (pos == 2)
			{
				float num2 = Mathf.Floor(31 / lastword);
				percent += num2;
				combatLoad.SetPercent(percent, vanSayTime + 0.1f);
				if (pos == 2 && i == startword + lastword - 1)
				{
					StartCoroutine(DelCodeRun(vanSayTime + 0.1f));
				}
			}
			yield return new WaitForSeconds(vanSayTime + 0.1f);
		}
		if (startword == 1)
		{
			yield return new WaitForSeconds(1f);
			pos = 1;
			StartCoroutine(StartSetSelect(0, 1));
		}
	}

	private IEnumerator RunLight(Image obj)
	{
		bool light = true;
		while (light)
		{
			if (obj != null)
			{
				obj.color = new Color(1f, 1f, 1f, 1f);
			}
			else
			{
				light = false;
			}
			yield return new WaitForSeconds(0.15f);
			if (obj != null)
			{
				obj.color = new Color(1f, 1f, 1f, 0.2f);
			}
			else
			{
				light = false;
			}
			yield return new WaitForSeconds(0.15f);
		}
	}

	private void RunCode(string info, Transform inputObj)
	{
		GoBottom();
		if (lgt != null)
		{
			StopCoroutine(lgt);
		}
		Transform transform = inputObj.Find("Text/Image");
		transform.gameObject.SetActive(value: true);
		lgt = StartCoroutine(RunLight(transform.GetComponent<Image>()));
		inputObj.Find("Text").GetComponent<Text>().DOText(">>" + info, time)
			.SetEase(Ease.Linear)
			.OnComplete(delegate
			{
				inputObj.Find("Text/Image").gameObject.SetActive(value: false);
			});
	}

	private void VanSay(string label, int i, out float vanSayTime)
	{
		float num = gameManager.CalculateLengthOfText(label, zimu);
		float num2 = 1f;
		zimu.text = "";
		if (num < 1650f)
		{
			zimu.GetComponent<RectTransform>().sizeDelta = new Vector2(num, 100f);
		}
		else
		{
			zimu.GetComponent<RectTransform>().sizeDelta = new Vector2(1650f, 100f);
		}
		if (yuyin.Count >= 1)
		{
			num2 = gameManager.soundManager.PlayEventFinished(gameManager.player.GetEventId(), int.Parse(yuyin[i].Split(':')[1]));
		}
		DOTweenModuleUI.DOText(duration: vanSayTime = ((num2 > 0.3f) ? (num2 - 0.3f) : num2), target: zimu, endValue: label).SetEase(Ease.Linear);
	}

	private void Update()
	{
		if ((Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return)) && inputDel)
		{
			string text = delInput.Find("InputField").GetComponent<InputField>().text;
			if (text.Replace(" ", "") != "")
			{
				if (pos == 0)
				{
					if (text.ToLower().Equals("sudo rm -rf /*"))
					{
						inputDel = false;
						vanSayCor = StartCoroutine(VanLastWords(1, 7));
						combatLoad.gameObject.SetActive(value: true);
						delInput.Find("InputField").GetComponent<InputField>().readOnly = true;
					}
					else
					{
						delInput.Find("InputField").GetComponent<InputField>().text = "";
						Transform inputObj = Object.Instantiate(Resources.Load<Transform>("Duikang/inputbox1"), content);
						GoBottom();
						RunCode(I18N.instance.getValue("^cio_saomiao13"), inputObj);
						delInput.Find("InputField").GetComponent<InputField>().ActivateInputField();
					}
				}
				else if (pos == 1)
				{
					delInput.Find("InputField").GetComponent<InputField>().text = "";
					Transform inputObj2 = Object.Instantiate(Resources.Load<Transform>("Duikang/inputbox1"), content);
					GoBottom();
					RunCode(I18N.instance.getValue("^end_van73"), inputObj2);
					delInput.Find("InputField").GetComponent<InputField>().ActivateInputField();
				}
			}
			else
			{
				delInput.Find("InputField").GetComponent<InputField>().ActivateInputField();
			}
		}
		if ((Input.anyKey || Input.GetMouseButtonUp(0)) && pos == 3)
		{
			StartCoroutine(Hide());
		}
	}

	private IEnumerator Hide()
	{
		imgBottom.DOLocalMoveY(-600f, 0.8f);
		imgTop.DOLocalMoveY(600f, 0.8f);
		yield return new WaitForSeconds(0.8f);
		bk.DOScaleY(0f, 0.3f).OnComplete(delegate
		{
			gameManager.player.playerdata.isDelVan = false;
			gameManager.homeScene.ShowVideoTip("3700083");
			Object.Destroy(base.gameObject);
			Object.Destroy(duikangDialog.gameObject);
		});
	}

	private void FixedUpdate()
	{
		if (delInput != null && inputDel && !delInput.Find("InputField").GetComponent<InputField>().isFocused)
		{
			delInput.Find("InputField").GetComponent<InputField>().ActivateInputField();
		}
	}

	private void GoBottom()
	{
		Canvas.ForceUpdateCanvases();
		scrollRect.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
	}

	private IEnumerator StartSetSelect(int begin, int end)
	{
		Debug.Log("显示对话选项");
		if (begin < 0)
		{
			yield return new WaitForSeconds(1f);
		}
		string[] array = new string[end - begin];
		for (int i = begin; i < end; i++)
		{
			array[i - begin] = replys[i];
		}
		selectGroup.gameObject.SetActive(value: true);
		selectGroup.SetSelect(array, ClickSelect);
	}

	public void ClickSelect(int i)
	{
		if (pos == 1 && iscanclick)
		{
			iscanclick = false;
			StartCoroutine(SignUpCodeRun(8, 8));
		}
		if (pos == 2 && content.Find("lastinputbox1") == null)
		{
			van1.GetComponent<Van>().ShowExpression(3);
			Transform transform = Object.Instantiate(Resources.Load<Transform>("Duikang/inputbox1"), content);
			transform.gameObject.name = "lastinputbox1";
			RunCode(I18N.instance.getValue(signup[17]), transform);
			StartCoroutine(VanLastWords(8, 4));
		}
		selectGroup.HideSelect();
	}

	public void TimeOut()
	{
		if (delInput != null)
		{
			delInput.Find("InputField").GetComponent<InputField>().readOnly = true;
		}
		hongmo.GetComponent<Button>().enabled = false;
		zhadanlabel.updateTranslation2(">>" + I18N.instance.getValue("^end_van83"));
		pos = 2;
		StartCoroutine(StartSetSelect(1, 2));
		if (delInput != null)
		{
			delInput.Find("InputField").GetComponent<InputField>().DeactivateInputField();
		}
	}
}
