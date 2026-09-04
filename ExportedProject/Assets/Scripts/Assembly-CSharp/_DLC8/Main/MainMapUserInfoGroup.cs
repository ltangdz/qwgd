using System.Collections.Generic;
using Aluba;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Main
{
	public class MainMapUserInfoGroup : MonoBehaviour
	{
		[Header("称号")]
		public Text designationText;

		public Text nameText;

		public Text departmentText;

		public Text numberText;

		public Button employeeButton;

		public Text progressNumber;

		public Text lvText;

		public Image designationFrame;

		public Image progressImage;

		public RectTransform progressIconRT;

		public RectTransform progressRT;

		public List<Sprite> designationList;

		public List<Color> designationColorList;

		private Color[] _colors = new Color[3]
		{
			new Color(0.6392157f, 73f / 85f, 0.9411765f, 1f),
			new Color(19f / 51f, 0.88235295f, 0.6627451f, 1f),
			new Color(0.85490197f, 8f / 15f, 0.0627451f, 1f)
		};

		private string[] _lvString = new string[7] { "LV1", "LV2", "LV3", "LV4", "LV5", "FTE", "STSR" };

		private void Start()
		{
			employeeButton.onClick.AddListener(ShowEmployee);
			Init();
		}

		private void ShowEmployee()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().PlaySound(DLC8SoundType.CLICK_BUTTON);
			DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.SHOW_EMPLOYEE_BOOK, 0);
		}

		public void Init()
		{
			ArchiveData archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			int num = archiveData.PositionLevel - 1;
			if (archiveData.PositionLevel == 0)
			{
				progressNumber.gameObject.SetActive(value: false);
				departmentText.gameObject.SetActive(value: false);
				designationFrame.gameObject.SetActive(value: false);
				lvText.text = "";
				progressImage.fillAmount = 0f;
				progressIconRT.gameObject.SetActive(value: false);
			}
			else if (archiveData.PositionLevel >= 1 && archiveData.PositionLevel <= 5)
			{
				progressNumber.gameObject.SetActive(value: true);
				if (archiveData.PositionLevel == 5)
				{
					progressNumber.text = "";
				}
				else
				{
					progressNumber.text = $"<size=24><color=#A9FFFC>{archiveData.PersonData}</color></size><size=16><color=#9699a3> / {SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.lvProgress[num]}</color></size>";
					_ = (float)archiveData.PersonData * 1f / (float)SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.lvProgress[num];
					_ = 1f;
					progressIconRT.gameObject.SetActive(value: false);
				}
				departmentText.color = _colors[0];
				departmentText.text = I18N.instance.getValue("^110009_common_159");
				departmentText.gameObject.SetActive(value: true);
				designationFrame.gameObject.SetActive(value: true);
				lvText.text = _lvString[archiveData.PositionLevel - 1];
				designationFrame.sprite = designationList[0];
			}
			else if (archiveData.PositionLevel > 5)
			{
				progressImage.fillAmount = 1f;
				progressNumber.gameObject.SetActive(value: false);
				designationFrame.gameObject.SetActive(value: true);
				progressIconRT.DOAnchorPosX(0f, 0f);
				departmentText.text = SingletonAutoMono<DLC8DataController>.GetInstance().GetDesignationName(num);
				departmentText.gameObject.SetActive(value: true);
				lvText.text = _lvString[num];
				if (archiveData.PositionLevel == 6)
				{
					departmentText.color = _colors[1];
					lvText.color = designationColorList[1];
					designationFrame.sprite = designationList[1];
				}
				else
				{
					departmentText.color = _colors[2];
					lvText.color = designationColorList[2];
					designationFrame.sprite = designationList[2];
				}
			}
			progressImage.DOFillAmount(1f, 0f).SetEase(Ease.Linear);
			nameText.text = archiveData.NickName;
			numberText.text = archiveData.IDNumber;
		}
	}
}
