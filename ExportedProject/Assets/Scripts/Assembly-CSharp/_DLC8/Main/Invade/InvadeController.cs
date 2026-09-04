using System.Collections;
using System.Collections.Generic;
using Aluba;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Main.Rank;

namespace _DLC8.Main.Invade
{
	public class InvadeController : LaborerBaseContentDialog
	{
		public RunPassword runPassword;

		public GameObject passwordCreatorObj;

		public GameObject beginCrackObj;

		public Text beginCrackText;

		public Text scoreText;

		public Button invadeButton;

		public InputField inputFieldText;

		public List<InvadeProgress> invadeProgressList;

		private string _truePassword;

		public RectTransform contentRT;

		private bool _canInput;

		public Button closeButton;

		public LaborerRankGroup rankGroup;

		private string[] _passwordVal = new string[31]
		{
			"A", "B", "C", "D", "E", "F", "G", "H", "J", "K",
			"M", "N", "P", "Q", "R", "S", "T", "U", "V", "W",
			"X", "Y", "Z", "2", "3", "4", "5", "6", "7", "8",
			"9"
		};

		private void Start()
		{
			closeButton.onClick.AddListener(delegate
			{
				closeButton.interactable = false;
				CloseAnimation();
			});
			invadeButton.interactable = false;
			rankGroup.Show(CityGameType.DDOS, base.ArchiveData.DdosLevel);
			invadeButton.onClick.AddListener(PressEnter);
			invadeProgressList[0].ShowAnimation(StepFinished);
			int num = ((base.ArchiveData.DdosLevel.BestScore >= 0) ? base.ArchiveData.DdosLevel.BestScore : 0);
			scoreText.text = num.ToString();
			ShowAnimation();
		}

		private void PressEnter()
		{
			if (_canInput && inputFieldText.text.ToUpper() == _truePassword.ToUpper())
			{
				inputFieldText.readOnly = true;
				invadeProgressList[1].ProgressPointSuccess();
				invadeProgressList[2].ShowAnimation(StepFinished);
				_canInput = false;
			}
		}

		private void StepFinished(int arg0)
		{
			switch (arg0)
			{
			case 0:
				invadeProgressList[1].ShowAnimation(null);
				GetPassword();
				break;
			case 2:
				invadeProgressList[3].ShowAnimation(null);
				StartCoroutine(BeginCrack());
				break;
			case 4:
				Object.Destroy(base.gameObject);
				SingletonAutoMono<DLC8DataController>.GetInstance().Controller.ShowDDosGame();
				break;
			}
		}

		private void GetPassword()
		{
			passwordCreatorObj.SetActive(value: true);
			List<string> list = new List<string>();
			_truePassword = "";
			for (int i = 0; i < 8; i++)
			{
				string text = _passwordVal[Random.Range(0, _passwordVal.Length)];
				_truePassword += text;
				list.Add(text);
			}
			runPassword.SetPassword(list);
			_canInput = true;
			invadeButton.interactable = true;
		}

		private string RandomIp()
		{
			return $"{Random.Range(0, 255)}.{Random.Range(0, 255)}.{Random.Range(0, 255)}.{Random.Range(0, 255)}";
		}

		private IEnumerator BeginCrack()
		{
			passwordCreatorObj.SetActive(value: false);
			beginCrackObj.SetActive(value: true);
			yield return new WaitForSeconds(0.5f);
			string endValue = I18N.instance.getValue("^invade_label15") + RandomIp();
			beginCrackText.DOText(endValue, 0.3f);
			yield return new WaitForSeconds(0.3f);
			beginCrackText.GetComponent<CanvasGroup>().DOFade(0.5f, 0.3f);
			yield return new WaitForSeconds(0.3f);
			beginCrackText.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
			yield return new WaitForSeconds(0.3f);
			beginCrackText.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
			yield return new WaitForSeconds(0.3f);
			beginCrackText.text = "";
			beginCrackText.GetComponent<CanvasGroup>().alpha = 1f;
			string endValue2 = I18N.instance.getValue("^invade_label17") + Random.Range(1000, 65535);
			beginCrackText.DOText(endValue2, 0.3f);
			yield return new WaitForSeconds(0.3f);
			beginCrackText.GetComponent<CanvasGroup>().DOFade(0.5f, 0.3f);
			yield return new WaitForSeconds(0.3f);
			beginCrackText.GetComponent<CanvasGroup>().DOFade(1f, 0.3f);
			yield return new WaitForSeconds(0.3f);
			beginCrackText.GetComponent<CanvasGroup>().DOFade(0f, 0.3f);
			yield return new WaitForSeconds(0.3f);
			beginCrackText.text = "";
			beginCrackText.GetComponent<CanvasGroup>().alpha = 1f;
			string value = I18N.instance.getValue("^invade_label19_1");
			beginCrackText.DOText(value, 0.5f);
			yield return new WaitForSeconds(0.5f);
			invadeProgressList[3].ProgressPointSuccess();
			invadeProgressList[4].ShowAnimation(StepFinished);
		}

		private void Update()
		{
			if (_canInput)
			{
				string text = inputFieldText.text.ToUpper();
				inputFieldText.text = text;
				if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
				{
					PressEnter();
				}
			}
		}

		private void OnDestroy()
		{
			NoticeCloseContent();
		}

		public void ShowAnimation()
		{
			base.gameObject.transform.DOScale(1f, 0f);
			contentRT.DOScale(0f, 0f);
			contentRT.DOScaleY(2f / contentRT.sizeDelta.y, 0f);
			contentRT.DOScaleX(2f / contentRT.sizeDelta.x, 0f);
			contentRT.DOScaleX(1f, 0.3f).OnComplete(delegate
			{
				contentRT.DOScaleY(1f, 0.3f).OnComplete(delegate
				{
				});
			});
		}

		public void CloseAnimation()
		{
			GameObject o = base.gameObject;
			contentRT.DOScaleY(2f / contentRT.sizeDelta.y, 0.3f).OnComplete(delegate
			{
				contentRT.DOScaleX(0f, 0.3f).OnComplete(delegate
				{
					contentRT.DOScaleY(0f, 0f);
					o.transform.DOScale(0f, 0f);
					Object.Destroy(o);
				});
			});
		}
	}
}
