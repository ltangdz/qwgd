using System.Collections;
using System.Text;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.Titan
{
	public class TitanSecondStepDialog : MonoBehaviour
	{
		public Image tipImage;

		public Text tipText;

		public GameObject resultCode;

		public TitanAIMessageDialog aiMessageDialog;

		private Text _curLoadingText;

		private string _curLoadingContent;

		private WaitForSeconds _loadingWaitInterval;

		public GameObject zhaDanObj;

		private GameObject _obj;

		private void Start()
		{
			tipImage.transform.DOScale(0f, 0f);
			_loadingWaitInterval = new WaitForSeconds(0.3f);
			_curLoadingText = tipText;
			_curLoadingContent = I18N.instance.getValue("^110008_game_117");
			Step1();
		}

		public void Finished(TitanSecondStep step)
		{
			switch (step)
			{
			case TitanSecondStep.AI_TALK1:
				zhaDanObj.SetActive(value: true);
				break;
			case TitanSecondStep.VIRUS:
				aiMessageDialog.gameObject.SetActive(value: true);
				aiMessageDialog.InitData(1);
				aiMessageDialog.Show();
				break;
			case TitanSecondStep.AI_TALK2:
				GameObject.Find("GameManager").GetComponent<GameManager>().player.playerdata.titanStep = 3;
				if (_obj == null)
				{
					Debug.Log("初始化");
					_obj = Object.Instantiate(Resources.Load<GameObject>(DLCNameUtil.Instance.GetPrefabPathDLC(GameTypeEnum.DLC7) + "TitanPanel"), base.transform.parent);
				}
				Object.Destroy(base.gameObject);
				break;
			}
		}

		private void Step1()
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(tipImage.transform.DOScale(1f, 0.5f).OnComplete(delegate
			{
				StartCoroutine("TextLoading");
				Invoke("RunCode", 2.4f);
			}));
			sequence.AppendInterval(5f);
			sequence.Append(tipImage.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).OnComplete(delegate
			{
				StopCoroutine("TextLoading");
				tipImage.gameObject.SetActive(value: false);
				aiMessageDialog.gameObject.SetActive(value: true);
				aiMessageDialog.InitData(0);
			}));
			sequence.Play();
		}

		private void RunCode()
		{
			resultCode.SetActive(value: true);
		}

		private IEnumerator TextLoading()
		{
			while (true)
			{
				for (int i = 0; i < 4; i++)
				{
					StringBuilder stringBuilder = new StringBuilder(_curLoadingContent);
					for (int j = 0; j < i; j++)
					{
						stringBuilder.Append(".");
					}
					_curLoadingText.text = stringBuilder.ToString();
					yield return _loadingWaitInterval;
				}
			}
		}
	}
}
