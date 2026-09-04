using DG.Tweening;
using UnityEngine;

namespace DLC7.Titan
{
	public abstract class TitanVirusBaseDialog : MonoBehaviour
	{
		public RectTransform contentRT;

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

		protected abstract void AfterShow();

		protected abstract void AfterHidden();

		private void Start()
		{
			Show();
		}

		public void Show()
		{
			contentRT.DOScale(1f, 0.38f).OnComplete(AfterShow);
		}

		public void Hidden()
		{
			contentRT.DOScale(0f, 0.38f).OnComplete(AfterHidden);
		}
	}
}
