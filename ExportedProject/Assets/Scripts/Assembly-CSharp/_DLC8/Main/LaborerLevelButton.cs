using System.Collections;
using System.Collections.Generic;
using Aluba;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Main
{
	public class LaborerLevelButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
	{
		public Image bgImage;

		public Sprite[] sprites;

		public Image progressImage;

		public Image progressBgImage;

		public Image effectImage;

		public Text tipText;

		public Text progressText;

		public CanvasGroup unlockCanvasGroup;

		public Image lockImage;

		public Material btnMaterial;

		private float pressDurationTime = 1f;

		public UnityAction unlockCallback;

		private bool _isDown;

		private bool _isPress;

		private float _downTime;

		private CityGameType _gameType;

		private LevelRecord _levelRecord;

		private int _cost;

		private TweenerCore<Vector3, Vector3, VectorOptions> _scaleTweener;

		private RectTransform _rt;

		private bool _isShowAnimation;

		private ArchiveData _archiveData;

		private Texture2D _mapTexture2D;

		private Vector2 _mapSize;

		private bool _showUnlockAnimation;

		private Sprite _mapSprite;

		private List<LaborerLevelButton> _buttonList = new List<LaborerLevelButton>();

		public LevelRecord LevelRecord => _levelRecord;

		public CityGameType GameType => _gameType;

		public List<LaborerLevelButton> ButtonList => _buttonList;

		public int Cost => _cost;

		public Sprite MapSprite => _mapSprite;

		private void Awake()
		{
			_rt = GetComponent<RectTransform>();
			_archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			DLC8EventManager.Instance.onNoticeControllerGameOver += NoticeControllerGameOver;
		}

		private void OnDestroy()
		{
			DLC8EventManager.Instance.onNoticeControllerGameOver -= NoticeControllerGameOver;
		}

		private void NoticeControllerGameOver(LevelRecord obj)
		{
			if (obj == _levelRecord)
			{
				LevelRecord newestLevelRecord = _archiveData.GetNewestLevelRecord((LaborerMapEnum)_levelRecord.MapLevel, _levelRecord.GameType);
				Show(newestLevelRecord.GameType, _cost, newestLevelRecord, isHideAnimation: true, _mapSprite, _buttonList);
			}
		}

		public Vector2 RandomPos()
		{
			Vector2 mapSize = _mapSize;
			int num = 40;
			int max = Mathf.FloorToInt(mapSize.x - (float)(num * 2));
			int max2 = Mathf.FloorToInt(mapSize.y - (float)num);
			int num2 = Random.Range(num, max);
			int num3 = Random.Range(num, max2);
			List<Vector2> list = new List<Vector2>();
			for (int i = 0; i < _buttonList.Count; i++)
			{
				if (!(_buttonList[i] == this))
				{
					list.Add(_buttonList[i].GetComponent<RectTransform>().anchoredPosition);
				}
			}
			bool flag = false;
			while (!flag)
			{
				for (int j = 0; j < list.Count; j++)
				{
					flag = Mathf.Abs(Vector2.Distance(list[j], new Vector2(num2, num3))) > 180f;
					if (!flag)
					{
						break;
					}
				}
				int num4 = 60;
				if (flag)
				{
					flag = _mapTexture2D.GetPixel(num2 - num4, num3).a > 0f;
				}
				if (flag)
				{
					flag = _mapTexture2D.GetPixel(num2 + num4, num3).a > 0f;
				}
				if (flag)
				{
					flag = _mapTexture2D.GetPixel(num2, num3 - num4).a > 0f;
				}
				if (flag)
				{
					flag = _mapTexture2D.GetPixel(num2, num3 + num4).a > 0f;
				}
				if (flag)
				{
					flag = _mapTexture2D.GetPixel(num2 + num4, num3 + num4).a > 0f;
				}
				if (!flag)
				{
					num2 = Random.Range(num, max);
					num3 = Random.Range(num, max2);
				}
			}
			return new Vector2(num2, num3);
		}

		public void Show(CityGameType gameType, int cost, LevelRecord record, bool isHideAnimation, Sprite mapSprite, List<LaborerLevelButton> buttonList)
		{
			if (record == null)
			{
				return;
			}
			if (record.IsUnlock && record.BestScore > 0)
			{
				base.gameObject.SetActive(value: false);
				return;
			}
			bgImage.material = new Material(btnMaterial);
			_buttonList = buttonList;
			_gameType = gameType;
			_levelRecord = record;
			_mapSprite = mapSprite;
			_mapSize = _mapSprite.rect.size;
			effectImage.transform.DOScale(0f, 0f);
			base.transform.SetAsLastSibling();
			_mapTexture2D = _mapSprite.texture;
			_isShowAnimation = true;
			lockImage.DOFade(0f, 0f);
			tipText.DOFade((!isHideAnimation) ? 1 : 0, 0f);
			if (record.isUnlock)
			{
				tipText.text = UnlockedLevelButtonTip();
			}
			else
			{
				tipText.text = I18N.instance.getValue("^110009_common_32");
			}
			unlockCanvasGroup.DOFade(0f, 0f);
			bgImage.sprite = sprites[(int)gameType];
			StartCoroutine(ShowAnimation(isHideAnimation, cost));
		}

		private string UnlockedLevelButtonTip()
		{
			return $"{I18N.instance.getValue(SingletonAutoMono<DLC8DataController>.GetInstance().GetGameNameKey(GameType))}({SingletonAutoMono<DLC8DataController>.GetInstance().LevelString(_levelRecord.MapLevel)}-{_levelRecord.Level + 1})";
		}

		private IEnumerator ShowAnimation(bool isHideAnimation, int cost)
		{
			if (isHideAnimation)
			{
				yield return new WaitForSeconds(0.8f);
				bgImage.material.DOFloat(0f, "_DissolvePower", 2f).SetEase(Ease.Linear);
				yield return new WaitForSeconds(2f);
			}
			base.transform.DOScale(0f, 0f).OnComplete(delegate
			{
				lockImage.DOFade((!_levelRecord.isUnlock) ? 1 : 0, 0f);
				tipText.DOFade(1f, 0f);
				bgImage.material.DOFloat(1f, "_DissolvePower", 0f).SetEase(Ease.Linear);
				effectImage.DOFade(1f, 0f);
				_cost = cost;
				Vector2 anchoredPosition = RandomPos();
				_rt.anchoredPosition = anchoredPosition;
				base.transform.DOScale(1f, 0.38f).OnComplete(delegate
				{
					unlockCallback?.Invoke();
					_isShowAnimation = false;
				});
			});
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (!_isShowAnimation)
			{
				if (_scaleTweener != null)
				{
					_scaleTweener.Kill();
					_scaleTweener = null;
				}
				_scaleTweener = base.transform.DOScale(1.1f, 0.38f);
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (!_isShowAnimation)
			{
				if (_scaleTweener != null)
				{
					_scaleTweener.Kill();
					_scaleTweener = null;
				}
				_scaleTweener = base.transform.DOScale(1f, 0.38f);
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (_isShowAnimation || _levelRecord.isUnlock)
			{
				return;
			}
			if (_archiveData.ResourceCount < _cost)
			{
				base.gameObject.transform.DOShakePosition(0.5f, 4f);
				DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.OUT_OF_RESOURCES, 0);
				if (!_archiveData.DdosLevel.isUnlock)
				{
					DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.SHOW_TEACHING, 11);
				}
			}
			else
			{
				_isDown = true;
				SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySound(56);
				_downTime = 0f;
				tipText.DOFade(1f, 0.3f);
				tipText.text = string.Format(I18N.instance.getValue("^110009_common_10").Replace("93f2cf", "37bad6"), _cost);
				unlockCanvasGroup.DOFade(1f, 0f);
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (!_isShowAnimation && !_showUnlockAnimation)
			{
				SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.Stop();
				_isPress = false;
				unlockCanvasGroup.DOFade(0f, 0f);
				_downTime = 0f;
				if (!_levelRecord.isUnlock)
				{
					tipText.text = I18N.instance.getValue("^110009_common_32");
				}
				_isDown = false;
			}
		}

		private void Update()
		{
			if (_isShowAnimation && _showUnlockAnimation)
			{
				return;
			}
			if (_isDown)
			{
				if (_archiveData.ResourceCount >= 0 && _archiveData.ResourceCount >= _cost)
				{
					_downTime += Time.deltaTime;
					if ((double)_downTime > 0.2)
					{
						_isPress = true;
					}
					_downTime = Mathf.Min(_downTime, pressDurationTime);
					if (_downTime >= pressDurationTime && !_levelRecord.isUnlock)
					{
						_isDown = false;
						_downTime = pressDurationTime;
						if (!_showUnlockAnimation)
						{
							SingletonAutoMono<DLC8DataController>.GetInstance().GameManager.soundManager.PlaySound(57);
						}
						_showUnlockAnimation = true;
						_archiveData.ChangeResourceCount(_cost * -1);
						DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.UNLOCK_APP, (int)_levelRecord.GameType);
						_levelRecord.isUnlock = true;
						effectImage.transform.DOScale(1f, 1f).OnComplete(delegate
						{
							DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.AUTO_SAVE, 0);
							tipText.text = UnlockedLevelButtonTip();
							effectImage.DOFade(0f, 1f).SetEase(Ease.Linear).OnComplete(delegate
							{
								lockImage.DOFade(0f, 0.3f).SetEase(Ease.Linear);
								unlockCanvasGroup.DOFade(0f, 0.3f).SetEase(Ease.Linear).OnComplete(delegate
								{
									_showUnlockAnimation = false;
									DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.UNLOCK_LEVEL, 0);
								});
							});
						}).SetEase(Ease.Linear);
					}
				}
				else if (!_archiveData.DdosLevel.isUnlock)
				{
					DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.SHOW_TEACHING, 11);
				}
			}
			progressImage.fillAmount = _downTime;
			progressText.text = $"{Mathf.FloorToInt(_downTime * 100f)}%";
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!_isPress && _levelRecord.isUnlock)
			{
				SingletonAutoMono<DLC8DataController>.GetInstance().PlaySound(DLC8SoundType.CLICK_BUTTON);
				SingletonAutoMono<DLC8DataController>.GetInstance().Controller.ShowGameContent(_levelRecord);
				DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.START_GAME, (int)_gameType);
				Debug.LogError("开始玩游戏" + _gameType);
			}
		}
	}
}
