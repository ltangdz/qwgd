using DG.Tweening;
using DLC7.DDOS;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DLC7
{
	public class HackerFailDialog : MonoBehaviour
	{
		private int _selectedNum;

		public Image _gameImage;

		public Text _gameText;

		public Text _gameSelectText;

		public Image _homeImage;

		public Text _homeText;

		public Text _homeSelectText;

		private void Start()
		{
			SelectedUp();
		}

		private void SelectedUp()
		{
			_selectedNum = 0;
			_gameImage.DOFade(1f, 0f);
			_gameSelectText.gameObject.SetActive(value: true);
			_gameText.gameObject.SetActive(value: false);
			_homeImage.DOFade(0f, 0f);
			_homeText.gameObject.SetActive(value: true);
			_homeSelectText.gameObject.SetActive(value: false);
		}

		private void SelectedDown()
		{
			_selectedNum = 1;
			_gameImage.DOFade(0f, 0f);
			_gameSelectText.gameObject.SetActive(value: false);
			_gameText.gameObject.SetActive(value: true);
			_homeImage.DOFade(1f, 0f);
			_homeText.gameObject.SetActive(value: false);
			_homeSelectText.gameObject.SetActive(value: true);
		}

		private void Selected()
		{
			if (_selectedNum == 0)
			{
				DLCEventManager.Instance.NoticeBackGame();
			}
			else
			{
				SceneManager.LoadScene("mainScene");
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
			{
				SelectedUp();
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
			{
				SelectedDown();
			}
			else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
			{
				Selected();
			}
		}
	}
}
