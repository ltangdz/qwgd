using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Card
{
	public class PrintCanvasDLC8 : MonoBehaviour
	{
		public Camera printCamera;

		public WorkCard workCardPrefab;

		public CompleteCard completePrefab;

		private GameObject _curObj;

		private PrintPrefabType _curType;

		private WorkCard _workCard;

		private CompleteCard _completeCard;

		public CanvasScaler canvasScaler;

		public void Show()
		{
			base.gameObject.SetActive(value: true);
			printCamera.gameObject.SetActive(value: true);
		}

		private void ShowPrefab()
		{
			switch (_curType)
			{
			case PrintPrefabType.WORK_CARD_FRONT:
			case PrintPrefabType.WORK_CARD_BACK:
				canvasScaler.referenceResolution = new Vector2(638f, 1010f);
				if (_workCard == null)
				{
					_workCard = Object.Instantiate(workCardPrefab, base.transform);
				}
				_curObj = _workCard.gameObject;
				if ((bool)_completeCard)
				{
					_completeCard.gameObject.SetActive(value: false);
				}
				_workCard.gameObject.SetActive(value: true);
				_workCard.camera = printCamera;
				_workCard.Show(_curType, isPrint: true);
				break;
			case PrintPrefabType.STAGE_CLEAR_NORMAL:
			case PrintPrefabType.STAGE_CLEAR_PERFECT:
				canvasScaler.referenceResolution = new Vector2(2480f, 3508f);
				if (_completeCard == null)
				{
					_completeCard = Object.Instantiate(completePrefab, base.transform);
				}
				_curObj = _completeCard.gameObject;
				_completeCard.gameObject.SetActive(value: true);
				if ((bool)_workCard)
				{
					_workCard.gameObject.SetActive(value: false);
				}
				_completeCard.camera = printCamera;
				_completeCard.Show(_curType, isPrint: true);
				break;
			}
		}

		public void Hide()
		{
			base.gameObject.SetActive(value: false);
			printCamera.gameObject.SetActive(value: false);
		}

		private void NoticeCommonEvent(DLC8CommonEvent arg1, int arg2)
		{
			switch (arg1)
			{
			case DLC8CommonEvent.CLOSE_PRINT_CANVAS:
				Hide();
				break;
			case DLC8CommonEvent.CHANGE_PRINT_PREFAB:
				_curType = (PrintPrefabType)arg2;
				Debug.LogError("_curType:" + _curType);
				ShowPrefab();
				break;
			case DLC8CommonEvent.DOWNLOAD_PRINT_PREFAB:
				if (_curType == PrintPrefabType.STAGE_CLEAR_NORMAL || _curType == PrintPrefabType.STAGE_CLEAR_PERFECT)
				{
					_completeCard.SaveImage("Certificate");
				}
				else
				{
					_workCard.SaveImage((_curType == PrintPrefabType.WORK_CARD_FRONT) ? "WorkPermit1" : "WorkPermit2");
				}
				break;
			}
		}

		private void Awake()
		{
			DLC8EventManager.Instance.onNoticeCommonEvent += NoticeCommonEvent;
		}

		private void OnDestroy()
		{
			DLC8EventManager.Instance.onNoticeCommonEvent -= NoticeCommonEvent;
		}
	}
}
