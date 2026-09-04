using System.Collections.Generic;
using Aluba;
using DG.Tweening;
using UnityEngine;
using _DLC8.Common;

namespace _DLC8.Main
{
	public class TeachDialog : MonoBehaviour
	{
		public CityMap cityMap;

		public TeachDLC8 teachDlc8;

		private TeachDialogStepType _stepType;

		private DLC8Controller _controller;

		private ArchiveData _archiveData;

		private GameObject _highObj;

		public void Show(TeachDialogStepType step, GameObject highObj)
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: false);
			base.gameObject.SetActive(value: true);
			SingletonAutoMono<DLC8DataController>.GetInstance().Controller.content.raycastTarget = false;
			_highObj = highObj;
			_controller = SingletonAutoMono<DLC8DataController>.GetInstance().Controller;
			_archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			_stepType = step;
			if (step != TeachDialogStepType.RANK)
			{
				_archiveData.teachStep = _stepType;
			}
			switch (_stepType)
			{
			case TeachDialogStepType.VIDEO_TIP:
				ShowVideoTip();
				break;
			case TeachDialogStepType.UNLOCK_LEVEL:
				UnlockLevelStep();
				break;
			case TeachDialogStepType.GAME_SUCCESS:
				GameFinishStep();
				break;
			case TeachDialogStepType.WARNING:
				ShowWarning();
				break;
			case TeachDialogStepType.UNLOCK_PUBLICOPINION:
				UnlockPublicOpinion();
				break;
			case TeachDialogStepType.UNLOCK_DOOS:
				UnlockDDOS();
				break;
			case TeachDialogStepType.RANK:
				ShowRank();
				break;
			case TeachDialogStepType.VIDEO_TIP_FINISH:
			case TeachDialogStepType.UNLOCK_LEVEL_FINISH:
			case TeachDialogStepType.GAME_SUCCESS_FINISH:
			case TeachDialogStepType.WARNING_FINISH:
			case TeachDialogStepType.UNLOCK_PUBLICOPINION_FINISH:
			case TeachDialogStepType.UNLOCK_DOOS_FINISH:
				break;
			}
		}

		private void ShowRank()
		{
			teachDlc8.ShowCourse(_highObj, hasArrow: false, new string[1] { "^110009_common_106" }, delegate
			{
				_archiveData.isFinishedRankTeach = true;
				NoticeSave();
				teachDlc8.HideCourse(isactive: false, delegate
				{
					base.gameObject.SetActive(value: false);
				});
			});
		}

		private void ShowVideoTip()
		{
			teachDlc8.ShowCourse(null, hasArrow: false, new string[0], null);
		}

		private void UnlockDDOS()
		{
			teachDlc8.ShowCourse(null, hasArrow: false, new string[1] { "^110009_common_51" }, delegate
			{
				Debug.Log("解锁ddos::" + _highObj);
				_archiveData.teachStep = TeachDialogStepType.UNLOCK_DOOS;
				teachDlc8.ShowCourse(_highObj, hasArrow: true, new string[0], null, isneedclick: true);
			});
		}

		private void UnlockPublicOpinion()
		{
			Invoke("ChangeCanClick", 0.3f);
			teachDlc8.ShowCourse(null, hasArrow: false, new string[1] { "^110009_common_49" }, delegate
			{
				teachDlc8.ShowCourse(null, hasArrow: false, new string[1] { "^110009_common_50" }, delegate
				{
					_archiveData.teachStep = TeachDialogStepType.UNLOCK_PUBLICOPINION;
					teachDlc8.ShowCourse(_highObj, hasArrow: true, new string[0], null, isneedclick: true);
				});
			});
		}

		private void ShowWarning()
		{
			Invoke("ChangeCanClick", 0.5f);
			teachDlc8.ShowCourse(_highObj, hasArrow: false, new string[1] { "^110009_common_49" }, delegate
			{
				_archiveData.teachStep = TeachDialogStepType.UNLOCK_PUBLICOPINION;
				teachDlc8.ShowCourse(SingletonAutoMono<DLC8DataController>.GetInstance().Controller.appGroup.AppItemByCityGameType(CityGameType.PUBLIC_OPINION).gameObject, hasArrow: true, new string[0], null, isneedclick: true);
			});
		}

		public void Hide()
		{
			bool flag = false;
			switch (_stepType)
			{
			case TeachDialogStepType.VIDEO_TIP:
				flag = true;
				_stepType = TeachDialogStepType.VIDEO_TIP_FINISH;
				_archiveData.teachStep = _stepType;
				break;
			case TeachDialogStepType.UNLOCK_PUBLICOPINION:
				flag = true;
				_stepType = TeachDialogStepType.UNLOCK_PUBLICOPINION_FINISH;
				_archiveData.teachStep = _stepType;
				break;
			case TeachDialogStepType.UNLOCK_DOOS:
				flag = true;
				_stepType = TeachDialogStepType.UNLOCK_DOOS_FINISH;
				_archiveData.teachStep = _stepType;
				DLC8EventManager.Instance.NoticeSpecialEvent(DLC8SpecialEvent.GUIDE_COMPLETE);
				break;
			}
			SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: true);
			if (flag)
			{
				NoticeSave();
			}
			if (teachDlc8.gameObject.activeInHierarchy)
			{
				teachDlc8.HideCourse(isactive: false, delegate
				{
					base.gameObject.SetActive(value: false);
				});
			}
		}

		private void GameFinishStep()
		{
			teachDlc8.ShowCourse(_highObj, hasArrow: false, new string[1] { "^110009_common_48" }, delegate
			{
				_archiveData.teachStep = TeachDialogStepType.GAME_SUCCESS_FINISH;
				teachDlc8.HideCourse(isactive: false, delegate
				{
					DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.SHOW_DIALOG_TIP, 3910029);
					base.gameObject.SetActive(value: false);
				});
			}, isneedclick: false, 3f);
		}

		private void OnEnable()
		{
			DLC8EventManager.Instance.onNoticeCommonEvent += NoticeCommonEvent;
		}

		private void OnDisable()
		{
			DLC8EventManager.Instance.onNoticeCommonEvent -= NoticeCommonEvent;
		}

		private void NoticeCommonEvent(DLC8CommonEvent arg1, int arg2)
		{
			if (arg1 != DLC8CommonEvent.UNLOCK_LEVEL || _archiveData.teachStep != TeachDialogStepType.UNLOCK_LEVEL)
			{
				return;
			}
			DLC8Controller dlc8Controller = SingletonAutoMono<DLC8DataController>.GetInstance().Controller;
			List<CityMap> mapList = dlc8Controller.mapList;
			CityMap cityMap = mapList[0];
			List<LaborerLevelButton> laborerLevelButtons = cityMap.ButtonList;
			for (int i = 0; i < laborerLevelButtons.Count; i++)
			{
				laborerLevelButtons[i].gameObject.SetActive(value: false);
			}
			teachDlc8.ShowCourse(cityMap.gameObject, hasArrow: false, new string[1] { "^110009_common_104" }, delegate
			{
				dlc8Controller.mapContentRt.DOLocalMove(new Vector3(0f, 445f, 0f), 0.5f).SetEase(Ease.Linear);
				for (int j = 0; j < laborerLevelButtons.Count; j++)
				{
					laborerLevelButtons[j].gameObject.SetActive(value: true);
				}
				teachDlc8.ShowCourse(mapList[1].gameObject, hasArrow: false, new string[1] { "^110009_common_112" }, delegate
				{
					dlc8Controller.mapContentRt.DOLocalMove(new Vector3(-526f, 445f, 0f), 0.5f).SetEase(Ease.Linear);
					teachDlc8.ShowCourse(null, hasArrow: false, new string[1] { "^110009_common_161" }, delegate
					{
						_archiveData.teachStep = TeachDialogStepType.UNLOCK_LEVEL_FINISH;
						teachDlc8.HideCourse(isactive: false, delegate
						{
							base.gameObject.SetActive(value: false);
							NoticeSave();
						});
					});
				});
			});
		}

		private void NoticeSave()
		{
			DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.AUTO_SAVE, 0);
		}

		private void UnlockLevelStep()
		{
			DLC8Controller controller = SingletonAutoMono<DLC8DataController>.GetInstance().Controller;
			teachDlc8.ShowCourse(controller.resourceGroup.gameObject, hasArrow: false, new string[1] { "^110009_common_103" }, delegate
			{
				for (int i = 0; i < cityMap.ButtonList.Count; i++)
				{
					LaborerLevelButton laborerLevelButton = cityMap.ButtonList[i];
					if (laborerLevelButton.LevelRecord.GameType == CityGameType.BASE_STATION)
					{
						teachDlc8.ShowCourse(laborerLevelButton.gameObject, hasArrow: true, new string[1] { "^110009_common_47" }, null, isneedclick: true);
					}
				}
			});
		}
	}
}
