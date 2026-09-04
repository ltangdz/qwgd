using System.Collections;
using System.Collections.Generic;
using Aluba;
using AlubaExcelData.DataClass;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Main
{
	public class MapLockGroup : MonoBehaviour
	{
		public List<Image> circleList;

		public Text titleText;

		public Text levelText;

		public Text designationText;

		public Button unLockButton;

		public Text costText;

		public Image lockImage;

		private ArchiveData _archiveData;

		private string[] _levelStrings = new string[5] { "C", "B", "A", "S", "Ω" };

		private CityMapData _cityMapData;

		private float time = 3f;

		public void Init(CityMapData cityMapData)
		{
			_archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			_cityMapData = cityMapData;
			titleText.text = I18N.instance.getValue(_cityMapData.name);
			levelText.text = string.Format("{0}{1}", I18N.instance.getValue("^110009_common_8"), _levelStrings[_cityMapData.level]);
			unLockButton.onClick.AddListener(Unlock);
			costText.text = string.Format(I18N.instance.getValue("^110009_common_10"), _cityMapData.cost);
			designationText.text = string.Format(I18N.instance.getValue("^110009_common_115"), SingletonAutoMono<DLC8DataController>.GetInstance().GetDesignationName(_cityMapData.level));
		}

		private void Unlock()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().PlaySound(DLC8SoundType.CLICK_BUTTON);
			if (_archiveData.PositionLevel <= _cityMapData.level)
			{
				DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.OUT_OF_LV, 0);
			}
			else if (_archiveData.ResourceCount - _cityMapData.cost > 0)
			{
				_archiveData.ChangeResourceCount(_cityMapData.cost * -1);
				_archiveData.UnlockMap(_cityMapData.id);
				unLockButton.interactable = false;
				StartCoroutine("ShowLight");
			}
			else
			{
				lockImage.transform.DOShakePosition(1f, 4f);
				DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.OUT_OF_RESOURCES, 0);
			}
		}

		private IEnumerator ShowLight()
		{
			int a = 0;
			int times = 0;
			costText.GetComponent<CanvasGroup>().DOFade(0f, 1f).SetEase(Ease.Linear);
			designationText.GetComponent<CanvasGroup>().DOFade(0f, 1f).SetEase(Ease.Linear);
			unLockButton.GetComponent<Image>().DOFade(0f, 1f).SetEase(Ease.Linear);
			unLockButton.GetComponentInChildren<Text>().DOFade(0f, 1f).SetEase(Ease.Linear);
			while (times < 3)
			{
				SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySound(58);
				circleList[a].GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
				circleList[a].GetComponent<CanvasGroup>().alpha = 1f;
				circleList[a].GetComponent<RectTransform>().DOScale(new Vector3(2f, 2f, 2f), time);
				circleList[a].GetComponent<CanvasGroup>().DOFade(0f, time);
				a = ((a + 1 < circleList.Count) ? (a + 1) : 0);
				times++;
				yield return new WaitForSeconds(time / (float)circleList.Count);
			}
			yield return new WaitForSeconds(2f);
			DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.UNLOCK_MAP, (int)_archiveData.GetLaborerMapEnum(_cityMapData.id));
			titleText.DOFade(0f, 1f).SetEase(Ease.Linear);
			lockImage.DOFade(0f, 1f).SetEase(Ease.Linear);
			levelText.DOFade(0f, 1f).SetEase(Ease.Linear);
			DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.AUTO_SAVE, 0);
			yield return new WaitForSeconds(1f);
			base.gameObject.SetActive(value: false);
		}
	}
}
