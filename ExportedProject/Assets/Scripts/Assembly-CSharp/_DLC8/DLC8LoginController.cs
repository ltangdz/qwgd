using System;
using Aluba;
using Michsky.UI.FieldCompleteMainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8
{
	public class DLC8LoginController : MonoBehaviour
	{
		public GameObject roleCardGroup;

		public GameObject createUserGroup;

		[Header("角色卡")]
		public Text nameText;

		public Text numberText;

		public Text timeText;

		public Button deleteButton;

		[Header("创建新用户")]
		public CustomInputFieldDLC8 inputField;

		[Header("删除存档")]
		public ConfirmDialog deleteDialogGroup;

		public DLC8MainContentController mainContent;

		[Header("确认 返回按钮")]
		public Button startButton;

		public Button backButton;

		private bool _hasSaveFile;

		private void Start()
		{
			Init();
		}

		private void Init()
		{
			_hasSaveFile = SingletonAutoMono<DLC8DataController>.GetInstance().LoadSaveData();
			if (_hasSaveFile)
			{
				roleCardGroup.SetActive(value: true);
				createUserGroup.SetActive(value: false);
				ArchiveData archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
				nameText.text = archiveData.NickName;
				numberText.text = archiveData.IDNumber;
				timeText.text = (archiveData.MIN / 60).ToString("00") + ":" + (archiveData.MIN % 60).ToString("00");
			}
			else
			{
				roleCardGroup.SetActive(value: false);
				createUserGroup.SetActive(value: true);
			}
			deleteButton.onClick.AddListener(ShowDeleteDialog);
			startButton.onClick.AddListener(StartGame);
			backButton.onClick.AddListener(BackHome);
		}

		private void BackHome()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySound(16);
			SceneManager.LoadScene("home");
		}

		private void StartGame()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySound(16);
			if (_hasSaveFile)
			{
				mainContent.StartLoading();
				base.gameObject.SetActive(value: false);
			}
			else if (!inputField.isShowTip)
			{
				string text = inputField.inputText.text;
				if (!string.IsNullOrEmpty(text))
				{
					SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.NickName = text;
					SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.IDNumber = CreateIDNumber();
					mainContent.StartLoading();
					base.gameObject.SetActive(value: false);
				}
			}
		}

		private string CreateIDNumber()
		{
			try
			{
				return string.Format("R{0}", UnityEngine.Random.Range(1, 999999999).ToString("000000000"));
			}
			catch (Exception)
			{
				return string.Format("R{0}", UnityEngine.Random.Range(1, 999999999).ToString("000000000"));
			}
		}

		private void ShowDeleteDialog()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySound(16);
			deleteDialogGroup.Show(delegate
			{
				_hasSaveFile = false;
				SingletonAutoMono<DLC8DataController>.GetInstance().DeleteSaveFile();
				deleteDialogGroup.Hide();
				Init();
			});
		}
	}
}
