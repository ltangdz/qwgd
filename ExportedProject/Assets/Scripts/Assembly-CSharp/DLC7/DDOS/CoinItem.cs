using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.DDOS
{
	public class CoinItem : DDosMonoBehaviour
	{
		public Button coinButton;

		private bool _noClick;

		private Vector2 _finalPosition = Vector2.zero;

		private Vector2 _startLocation;

		public RectTransform rt;

		private Image _coinImage;

		private int _coinCount = 10;

		private TweenerCore<Color, Color, ColorOptions> _hideAnimation;

		public Vector2 FinalPosition
		{
			get
			{
				if (_finalPosition == Vector2.zero)
				{
					_finalPosition = base.DdosManager.CoinTransform.position;
				}
				return _finalPosition;
			}
		}

		public Image CoinImage
		{
			get
			{
				if (_coinImage == null)
				{
					_coinImage = GetComponent<Image>();
				}
				return _coinImage;
			}
		}

		private void Start()
		{
			coinButton.onClick.AddListener(Click);
		}

		private void OnSpawned(SpawnPool pool)
		{
			base.transform.DOScale(Vector2.one, 0f);
			_noClick = true;
		}

		private void OnDespawned(SpawnPool pool)
		{
			if (_hideAnimation != null)
			{
				_hideAnimation.Kill();
				_hideAnimation = null;
			}
			CancelInvoke("Disappear");
		}

		private void Click()
		{
			if (_noClick)
			{
				return;
			}
			_noClick = true;
			CancelInvoke("Disappear");
			base.DdosEventManager.NoticeSound(DdosSound.CLICK_COIN);
			base.transform.DOMove(FinalPosition, 0.5f).OnComplete(delegate
			{
				base.transform.DOScale(Vector2.zero, 0.2f);
				if (base.DdosManager.SpawnPool.IsSpawned(base.transform))
				{
					base.DdosManager.SpawnPool.Despawn(base.transform);
				}
				base.DdosEventManager.NoticeAddCoin(_coinCount);
			}).SetEase(Ease.Linear);
		}

		private void Disappear()
		{
			if (_hideAnimation == null)
			{
				_hideAnimation = CoinImage.DOFade(0f, 3f).SetEase(Ease.Linear).OnComplete(delegate
				{
					_noClick = true;
					base.DdosManager.SpawnPool.Despawn(base.transform);
				});
			}
		}

		public void InitData(int coin, bool isCard)
		{
			CoinImage.DOFade(1f, 0f);
			_coinCount = coin;
			float num;
			float num2;
			if (Random.Range(0, 2) == 0)
			{
				num = ((Random.Range(0, 2) == 0) ? Random.Range(-1f, -0.5f) : Random.Range(0.5f, 1f));
				num2 = Random.Range(-1f, 1f);
			}
			else
			{
				num2 = ((Random.Range(0, 2) == 0) ? Random.Range(-1f, -0.5f) : Random.Range(0.5f, 1f));
				num = Random.Range(-1f, 1f);
			}
			Vector2 vector = base.transform.position;
			Vector3 endValue = new Vector3(vector.x + num, vector.y + num2, 0f);
			base.DdosEventManager.NoticeSound(DdosSound.COIN_SHOW);
			base.transform.DOJump(endValue, 0.5f, 3, 0.5f).OnComplete(delegate
			{
				_noClick = false;
			});
			Invoke("Disappear", 15f);
		}
	}
}
