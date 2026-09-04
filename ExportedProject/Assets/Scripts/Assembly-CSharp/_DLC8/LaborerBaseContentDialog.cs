using Aluba;
using UnityEngine;
using _DLC8.Common;

namespace _DLC8
{
	public class LaborerBaseContentDialog : MonoBehaviour
	{
		private ArchiveData _archiveData;

		private DLC8DataController _dataController;

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

		public ArchiveData ArchiveData
		{
			get
			{
				if (_archiveData == null)
				{
					_archiveData = DataController.ArchiveData;
				}
				return _archiveData;
			}
		}

		public DLC8DataController DataController
		{
			get
			{
				if (_dataController == null)
				{
					_dataController = SingletonAutoMono<DLC8DataController>.GetInstance();
				}
				return _dataController;
			}
		}

		protected void NoticeCloseContent()
		{
			DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.CLOSE_CONTENT, 0);
		}
	}
}
