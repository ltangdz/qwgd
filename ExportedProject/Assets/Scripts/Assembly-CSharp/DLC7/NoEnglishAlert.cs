using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7
{
	public class NoEnglishAlert : MonoBehaviour
	{
		public Transform content;

		public Button sureButton;

		public Button cancelButton;

		public Text sureButtonText;

		public Text titleText;

		public GameManager _gameManager;

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
			sureButton.onClick.AddListener(Sure);
			cancelButton.onClick.AddListener(Cancel);
			content.DOScale(0f, 0f);
			content.DOScale(1f, 0.5f);
			sureButtonText.text = ((!GameManager.IsBuyDLC(DLCEnum.HELLO_WORLD)) ? "OK" : "OK");
			titleText.text = I18N.instance.getValue((!GameManager.IsBuyDLC(DLCEnum.HELLO_WORLD)) ? "^110008_common_91" : "^110008_common_90");
		}

		private void Sure()
		{
			if (!GameManager.IsBuyDLC(DLCEnum.HELLO_WORLD))
			{
				GameManager.ValidDLC(8);
			}
			else
			{
				Object.Destroy(base.gameObject);
			}
		}

		private void Cancel()
		{
			Object.Destroy(base.gameObject);
		}
	}
}
