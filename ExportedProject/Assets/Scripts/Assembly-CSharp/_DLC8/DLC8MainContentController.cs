using System.Collections;
using Aluba;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Card;
using _DLC8.Common;

namespace _DLC8
{
	public class DLC8MainContentController : MonoBehaviour
	{
		public DLC8Controller dlc8Prefab;

		public PrintCanvasDLC8 printCanvas;

		public Transform mainContent;

		public LoadingPanelDLC8 loadingPanel;

		public DLC8LoginController loginPanel;

		public TitlePanelDLC8 titlePanel;

		public Image noEscImage;

		public SettingDLC8 settingPanelDlc8;

		public PausePanelDLC8 pausePanelDlc8;

		public GameObject exitWindow;

		public GameObject backToMainWindow;

		private ArchiveData archiveData;

		private Coroutine _showEsc;

		private GameManager _gameManager;

		public GameManager GameManager
		{
			get
			{
				if (_gameManager == null)
				{
					_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
				}
				return _gameManager;
			}
		}

		private void Start()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().CanShowSetting(isCan: false);
		}

		public void ShowDLC8Controller()
		{
			DLC8Controller dLC8Controller = Object.Instantiate(dlc8Prefab, mainContent);
			dLC8Controller.printCanvasDlc8 = printCanvas;
			dLC8Controller.mapContentRt.anchoredPosition = new Vector2(archiveData.MapPositionX, archiveData.MapPositionY);
			SingletonAutoMono<DLC8DataController>.GetInstance().Controller = dLC8Controller;
		}

		public void StartLoading()
		{
			archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			loadingPanel.StartLoading(archiveData.NickName, delegate
			{
				loginPanel.gameObject.SetActive(value: false);
				if (archiveData.isShowTitle)
				{
					ShowDLC8Controller();
				}
				else
				{
					titlePanel.Show("^110009_common_1", delegate
					{
						ShowDLC8Controller();
						archiveData.isShowTitle = true;
						titlePanel.Hide();
						SingletonAutoMono<DLC8DataController>.GetInstance().SaveData();
					});
				}
			});
		}

		private IEnumerator ShowEsc()
		{
			noEscImage.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(2f);
			noEscImage.gameObject.SetActive(value: false);
		}

		private void Update()
		{
			if (!Input.GetKeyUp(KeyCode.Escape))
			{
				return;
			}
			Debug.Log(GameManager.canShowSetting);
			if (GameManager.canShowSetting == 0)
			{
				noEscImage.gameObject.SetActive(value: false);
				if (!pausePanelDlc8.gameObject.activeInHierarchy)
				{
					pausePanelDlc8.gameObject.SetActive(value: true);
					return;
				}
				pausePanelDlc8.gameObject.SetActive(value: false);
				settingPanelDlc8.gameObject.SetActive(value: false);
				backToMainWindow.SetActive(value: false);
				exitWindow.SetActive(value: false);
			}
			else
			{
				if (_showEsc != null)
				{
					StopCoroutine(_showEsc);
				}
				_showEsc = StartCoroutine(ShowEsc());
			}
		}
	}
}
