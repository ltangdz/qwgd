using Aluba;
using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Main
{
	public class LaborerLevelItem : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		public Image bgImage;

		public Text nameText;

		public Text scoreText;

		public Text playButtonText;

		public Button playButton;

		private LevelRecord _levelRecord;

		private Color[] _normalTextColors = new Color[2]
		{
			new Color(0.6509804f, 0.69803923f, 40f / 51f, 1f),
			Color.white
		};

		private Color[] _buttonTextColors = new Color[2]
		{
			new Color(33f / 85f, 0.41568628f, 0.52156866f, 1f),
			new Color(0.83137256f, 44f / 51f, 83f / 85f, 1f)
		};

		private string[] _levelStrings = new string[5] { "C", "B", "A", "S", "Ω" };

		private bool _isSelected;

		private UnityAction<LaborerLevelItem> _clickCallback;

		public LevelRecord LevelRecord => _levelRecord;

		private void Start()
		{
			playButton.onClick.AddListener(PlayGame);
		}

		private void PlayGame()
		{
			SingletonAutoMono<DLC8DataController>.GetInstance().PlaySound(DLC8SoundType.CLICK_BUTTON);
			DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.START_GAME, (int)_levelRecord.GameType);
			SingletonAutoMono<DLC8DataController>.GetInstance().Controller.ShowGameContent(_levelRecord);
		}

		public void InitData(LevelRecord levelRecord, UnityAction<LaborerLevelItem> callback)
		{
			_clickCallback = callback;
			_levelRecord = levelRecord;
			nameText.text = $"{levelRecord.GetI18NName()}({_levelStrings[_levelRecord.MapLevel]}-{_levelRecord.Level + 1})";
			scoreText.text = string.Format("{0}:{1}", I18N.instance.getValue("^110009_common_29"), (_levelRecord.BestScore > 0) ? _levelRecord.GetTimeScoreString(isBestScore: true) : I18N.instance.getValue("^career_platform0303"));
			Enter(_isSelected);
		}

		public void Enter(bool isEnter)
		{
			_isSelected = isEnter;
			int num = (isEnter ? 1 : 0);
			bgImage.DOFade(num, 0f);
			nameText.color = _normalTextColors[num];
			scoreText.color = _normalTextColors[num];
			playButtonText.color = _buttonTextColors[num];
			playButton.gameObject.SetActive(isEnter);
			if (isEnter)
			{
				_clickCallback?.Invoke(this);
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			Enter(isEnter: true);
		}
	}
}
