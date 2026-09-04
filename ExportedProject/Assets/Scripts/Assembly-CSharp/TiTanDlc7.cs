using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DLC7.DDOS;
using DLC7.Titan;
using Honeti;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TiTanDlc7 : MonoBehaviour
{
	[SerializeField]
	private GameObject logo;

	[SerializeField]
	private GameObject img_start;

	private GameManager gameManager;

	public Text txt_zimu;

	private IEnumerator currentienumerator;

	public bool showPasswordPanel;

	public TitanThirdStep thirdStep;

	public TotalPanelDlc7 totalPanel;

	public Image image_brain;

	public Material brainMaterial;

	public Image image_stele;

	private Camera _mainCamera;

	private CameraFilterPack_FX_Glitch1 _glitch1;

	public Image animationImage;

	public TotalPanelDlc7 totalPanelDlc7;

	public GameObject permissionObj;

	private int _reportCount;

	public GameObject warningContent;

	public List<TitanWarningText> warningTexts;

	private CameraFilterPack_3D_Binary _binary;

	private CameraFilterPack_Color_Chromatic_Aberration _color;

	private List<List<string>> _reportDataList;

	private TitanWarningText titanWarningTextRes;

	public List<List<string>> ReportDataList
	{
		get
		{
			if (_reportDataList == null)
			{
				_reportDataList = new List<List<string>>
				{
					new List<string> { "Monitoring Report No.014&& &&Lv1&&", "ANKH No.079&& &&Lv1&&" },
					new List<string> { "Test Record No.087&& &&Lv2&&", "Action Record No.235&& &&Lv2&&", "Inspection Report No.001&&titan-11 Crybody&&Lv2&&" },
					new List<string> { "Inspection Report No.076&& &&Lv3&&2021/02/01", "Project “Brain” No.017&&titan-09 Cylo&&Lv3&&", "Internal Minutes No.034&& &&Lv3&&" },
					new List<string> { "Advanced Instruction No.014&&TITAN4 Gman&&Lv4&&", "Internal Report No.007&&TITAN1 Ilias&&Lv4&&", "Internal Report No.001&&TITAN2 Ravel&&Lv4&&" },
					new List<string> { "X&&Admin X&&LvX&&" }
				};
			}
			return _reportDataList;
		}
	}

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (gameManager.player.playerdata.titanStep < 2)
		{
			image_brain.material = brainMaterial;
		}
		else if (gameManager.player.playerdata.titanStep == 2)
		{
			ShowStep2();
		}
		else if (gameManager.player.playerdata.titanStep == 3)
		{
			image_stele.gameObject.SetActive(value: true);
			totalPanel.gameObject.SetActive(value: false);
			thirdStep.gameObject.SetActive(value: true);
		}
		for (int i = 0; i < ReportDataList.Count; i++)
		{
			for (int j = 0; j < ReportDataList[i].Count; j++)
			{
				_reportCount++;
			}
		}
		_mainCamera = Camera.main;
		_glitch1 = _mainCamera.gameObject.AddComponent<CameraFilterPack_FX_Glitch1>();
		_glitch1.Glitch = 0f;
	}

	private void ShowGlitch()
	{
		_glitch1.Glitch = 0.03f;
	}

	private void ClickLeft(int obj)
	{
		if (obj == -1)
		{
			permissionObj.SetActive(value: true);
		}
		thirdStep.Refresh((obj == -1) ? null : ReportDataList[obj]);
	}

	private void Awake()
	{
		img_start.SetActive(value: true);
		TitanEventManager.Instance.onNoticeClickLeftPanel += ClickLeft;
		TitanEventManager.Instance.onNoticeShowReport += NoticeShowReport;
	}

	private void NoticeShowReport(string obj)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (!gameManager.player.playerdata.ReportShowedList.Contains(obj))
		{
			gameManager.player.playerdata.ReportShowedList.Add(obj);
			gameManager.saveManager.SavePlayerData(isshowlogo: true, isForce: true);
		}
		if (gameManager.player.playerdata.ReportShowedList.Count == _reportCount - 1 && gameManager.player.playerdata.titanStep < 2)
		{
			Warning();
			gameManager.player.playerdata.titanStep = 2;
			gameManager.saveManager.SavePlayerData();
		}
		if (gameManager.player.playerdata.ReportShowedList.Count >= _reportCount)
		{
			Debug.Log("满了");
			Invoke("PlayEndWarning", 40f);
		}
	}

	private void PlayEndWarning()
	{
		gameManager.player.playerdata.titanStep = 4;
		gameManager.player.playerdata.showTitanButton = false;
		gameManager.saveManager.SavePlayerData(isshowlogo: true, isForce: true);
		StartCoroutine("EndWarning");
	}

	private IEnumerator EndWarning()
	{
		warningContent.SetActive(value: true);
		warningTexts[0].gameObject.SetActive(value: true);
		float time = 2f;
		yield return new WaitForSeconds(time);
		for (int i = 1; i < warningTexts.Count; i++)
		{
			time -= 0.2f;
			if (time < 0.1f)
			{
				time = 0.1f;
			}
			warningTexts[i].gameObject.SetActive(value: true);
			yield return new WaitForSeconds(time);
		}
		yield return new WaitForSeconds(5f);
		Object.Instantiate(Resources.Load<TitanDialog>(DLCNameUtil.Instance.GetTitanTipDialogName()), warningContent.transform).InitData(I18N.instance.getValue("^110008_other_246"), delegate
		{
			SceneManager.LoadScene("homeDLC7");
		});
	}

	private void Warning()
	{
		_glitch1.enabled = true;
		DOTween.To(() => _glitch1.Glitch, delegate(float x)
		{
			_glitch1.Glitch = x;
		}, 1f, 2f).SetEase(Ease.Linear);
		_binary = _mainCamera.gameObject.AddComponent<CameraFilterPack_3D_Binary>();
		_color = _mainCamera.gameObject.AddComponent<CameraFilterPack_Color_Chromatic_Aberration>();
		_binary._MatrixColor = new Color(11f / 85f, 0.77254903f, 0f);
		_binary._FixDistance = 37.7f;
		_binary.LightIntensity = -0.6f;
		_binary.MatrixSize = 2.47f;
		_binary.MatrixSpeed = -3.4f;
		_binary.Fade = 0f;
		_binary.FadeFromBinary = 0.623f;
		DOTween.To(() => _binary.Fade, delegate(float x)
		{
			_binary.Fade = x;
		}, 1f, 10f).SetEase(Ease.Linear);
		DOTween.To(() => _binary.LightIntensity, delegate(float x)
		{
			_binary.LightIntensity = x;
		}, -5f, 10f).SetEase(Ease.Linear).OnComplete(delegate
		{
			Invoke("ShowStep2", 1f);
		});
	}

	private void ShowStep2()
	{
		if (_color != null)
		{
			_color.enabled = false;
		}
		if (_binary != null)
		{
			_binary.enabled = false;
		}
		if (_glitch1 != null)
		{
			_glitch1.enabled = false;
		}
		Object.Instantiate(Resources.Load<TitanSecondStepDialog>("_DLC7/prefabs/TitanStep2"), base.transform.root);
		Object.Destroy(base.gameObject);
	}

	private void OnDestroy()
	{
		TitanEventManager.Instance.onNoticeShowReport -= NoticeShowReport;
		TitanEventManager.Instance.onNoticeClickLeftPanel -= ClickLeft;
	}

	public void Showlogo()
	{
		logo.SetActive(value: true);
		StartCoroutine(ShowStart());
	}

	public void ShowTotalPanel(bool isShow)
	{
		totalPanel.gameObject.SetActive(isShow);
	}

	private IEnumerator ShowStart()
	{
		yield return new WaitForSeconds(1.5f);
		img_start.SetActive(value: false);
		GetComponent<Animator>().Play("ani_houtai");
		totalPanelDlc7.InitData(ReportDataList);
	}

	public void ShowZimu(List<string> zimus, List<int> yuyins, float waittime)
	{
	}

	public void Stop()
	{
		if (currentienumerator != null)
		{
			StopCoroutine(currentienumerator);
		}
	}

	private IEnumerator ShowZimuAni(List<string> zimus, List<int> yuyins, float waittime)
	{
		yield return new WaitForSeconds(waittime);
		gameManager.musicManager.LowerVol();
		gameManager.soundManager.Stop();
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < zimus.Count; i++)
		{
			float num = gameManager.soundManager.PlayEventFinished(gameManager.player.GetEventId(), yuyins[i]);
			txt_zimu.DOText(I18N.instance.getValue(zimus[i]), num).SetEase(Ease.Linear);
			yield return new WaitForSeconds(num + 1f);
			txt_zimu.text = "";
		}
		txt_zimu.text = "";
		gameManager.musicManager.ResumeVol();
	}
}
