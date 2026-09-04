using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DLC7.DDOS
{
	public class CardItem : AlubaSpawn
	{
		[Header("组件")]
		public Image tipImage;

		public Image attackUp;

		public FrameAnimation2D effectAnimation;

		public FrameAnimation2D queenEffectAnimation;

		public FrameAnimation2D attackAnimation;

		public Image contentImage;

		public Image frameImage;

		public Text lvText;

		public RectTransform lvGroup;

		public RectTransform target;

		public CanvasGroup canvasGroup;

		private List<string> animationNames;

		public Card _card;

		private PositionType _positionType;

		private bool _curDrag;

		private DDOSEventManager _eventManager;

		private bool _isLife;

		private string _customGuid;

		private bool _isStopShoot;

		private int _shootCount;

		private CardType _tempCardType;

		private DDOSManager _ddosManager;

		public Card Card => _card;

		public DDOSManager DdosManager
		{
			get
			{
				if (_ddosManager == null)
				{
					_ddosManager = DDOSManager.Instance;
				}
				return _ddosManager;
			}
		}

		public DDOSEventManager EventManager => _eventManager ?? DDOSEventManager.Instance;

		public void InitData(Card card, PositionType positionType)
		{
			_isStopShoot = false;
			_customGuid = GetInstanceID().ToString();
			animationNames = new List<string>
			{
				$"{_customGuid}EffectAnimation",
				$"{_customGuid}QueenEffectAnimation",
				$"{_customGuid}AttackAnimation"
			};
			_card = card;
			if (_card.Type == CardType.QUEEN)
			{
				lvGroup.gameObject.SetActive(value: true);
				lvGroup.localPosition = new Vector2(lvGroup.localPosition.x, -25f);
				if (_card.Intensify == IntensifyType.TRANSFER)
				{
					queenEffectAnimation.gameObject.SetActive(value: true);
					queenEffectAnimation.Play();
				}
			}
			else if (_card.Type == CardType.ATTAKER)
			{
				lvGroup.gameObject.SetActive(value: true);
				lvGroup.localPosition = new Vector2(lvGroup.localPosition.x, -50f);
			}
			else
			{
				lvGroup.gameObject.SetActive(value: false);
			}
			_positionType = positionType;
			Show();
		}

		public void StartDrag()
		{
			_curDrag = true;
			canvasGroup.alpha = 0.5f;
			base.transform.localScale = Vector3.one * 0.8f;
		}

		public void DragEnd()
		{
			_curDrag = false;
			canvasGroup.alpha = 1f;
			base.transform.localScale = Vector3.one;
		}

		private void HideAllEffect()
		{
			attackUp.DOFade(0f, 0f);
			attackUp.DOFillAmount(0f, 0f);
			effectAnimation.Stop();
			effectAnimation.gameObject.SetActive(value: false);
			queenEffectAnimation.Stop();
			queenEffectAnimation.gameObject.SetActive(value: false);
			attackAnimation.Stop();
			attackAnimation.gameObject.SetActive(value: false);
		}

		public void Show()
		{
			DdosManager.InitImage(Card.FramePath, frameImage);
			DdosManager.InitImage(Card.ContentPath, contentImage);
			lvText.text = Card.Lv.ToString();
			if (_positionType == PositionType.ATTACKER && (Card.Type == CardType.QUEEN || Card.Type == CardType.ATTAKER))
			{
				StopCoroutine("Power");
				StartCoroutine("Power");
			}
		}

		private IEnumerator Power()
		{
			while (_positionType == PositionType.ATTACKER && _isLife)
			{
				if (_isStopShoot)
				{
					StopPower();
				}
				yield return new WaitForSeconds((_card.Type == CardType.QUEEN) ? 8f : _card.CurInterval);
				if (_card.Type == CardType.QUEEN)
				{
					Yield();
				}
				else if (_card.Type == CardType.ATTAKER)
				{
					if (Card.isFlood)
					{
						StartCoroutine("FloodShoot");
					}
					else
					{
						Shoot();
					}
				}
			}
		}

		private void Shoot()
		{
			_shootCount++;
			switch (Card.Intensify)
			{
			case IntensifyType.NONE:
			case IntensifyType.ICE:
			case IntensifyType.FLASH:
				NormalShoot();
				break;
			case IntensifyType.BUG:
				BugShoot();
				break;
			case IntensifyType.TRANSFER:
			case IntensifyType.OVERCLOCK:
			case IntensifyType.FLOOD:
				break;
			}
		}

		private void NormalShoot()
		{
			if (attackAnimation.isActiveAndEnabled)
			{
				attackAnimation.Play(_customGuid);
			}
			DDOSBullet bullet = InitBullet();
			Enemy enemy = GetEnemyList(1)[0];
			ShootEnemy(enemy, bullet, 7f);
		}

		private IEnumerator FloodShoot()
		{
			StopCoroutine("Power");
			for (int i = 0; i < 4; i++)
			{
				List<Enemy> enmEnemies = GetEnemyList(5);
				for (int j = 0; j < enmEnemies.Count; j++)
				{
					DDOSBullet bullet = InitBullet();
					ShootEnemy(enmEnemies[j], bullet, 14f);
					yield return new WaitForSeconds(0.2f);
				}
			}
			Card.isFlood = false;
			StartCoroutine("Power");
		}

		private void BugShoot()
		{
			List<Enemy> enemyList = GetEnemyList(3);
			if (_shootCount % 3 == 0)
			{
				for (int i = 0; i < enemyList.Count; i++)
				{
					DDOSBullet bullet = InitBullet();
					ShootEnemy(enemyList[i], bullet, 7f);
				}
			}
			else
			{
				DDOSBullet bullet2 = InitBullet();
				ShootEnemy(enemyList[0], bullet2, 7f);
			}
		}

		private void ShootEnemy(Enemy enemy, DDOSBullet bullet, float speed)
		{
			if (enemy == null)
			{
				bullet.FreeShoot(GetBulletType(), speed, Card.isFlood);
			}
			else
			{
				bullet.Shoot(GetBulletType(), enemy.GetComponent<RectTransform>(), speed, Card.GetDamaged(), Card.isFlood);
			}
		}

		public void StopPower()
		{
			StopCoroutine("Power");
			StopCoroutine("FloodShoot");
		}

		private DDOSBullet InitBullet()
		{
			DDOSBullet component = DdosManager.SpawnPool.Spawn("DDOSBullet").GetComponent<DDOSBullet>();
			if (!attackAnimation.gameObject.activeSelf)
			{
				attackAnimation.gameObject.SetActive(value: true);
			}
			component.transform.position = base.transform.position;
			return component;
		}

		private List<Enemy> GetEnemyList(int number)
		{
			Vector2 b = base.transform.position;
			List<Enemy> list = new List<Enemy>();
			Dictionary<int, float> dictionary = new Dictionary<int, float>();
			List<Enemy> enemies = DdosManager.Enemies;
			for (int i = 0; i < enemies.Count; i++)
			{
				Enemy enemy = enemies[i];
				if (!enemy.IsSafe)
				{
					float value = Mathf.Abs(Vector2.Distance(enemy.transform.position, b));
					dictionary[i] = value;
				}
			}
			List<KeyValuePair<int, float>> list2 = new List<KeyValuePair<int, float>>(dictionary);
			list2.Sort((KeyValuePair<int, float> s1, KeyValuePair<int, float> s2) => s1.Value.CompareTo(s2.Value));
			for (int num = 0; num < number; num++)
			{
				if (list2.Count > num)
				{
					list.Add(enemies[list2[num].Key]);
				}
				else
				{
					list.Add(null);
				}
			}
			return list;
		}

		private BulletType GetBulletType()
		{
			switch (Card.Intensify)
			{
			case IntensifyType.ICE:
				return BulletType.ICE;
			case IntensifyType.FLASH:
				return BulletType.PALSY;
			case IntensifyType.BUG:
				return BulletType.BUG;
			case IntensifyType.FLOOD:
				return BulletType.FLOOD;
			default:
				return BulletType.NORMAL;
			}
		}

		private void Yield()
		{
			CoinItem component = DdosManager.SpawnPool.Spawn("CoinItem").GetComponent<CoinItem>();
			component.transform.position = base.transform.position;
			component.InitData(Card.Attack, isCard: true);
		}

		public void Upgrade(IntensifyType intensifyType)
		{
			Card.Intensify = intensifyType;
			EventManager.NoticeSound(DdosSound.FUSION);
			Card.Upgrade(DdosManager);
			Show();
		}

		public void QueenTransfer(int buff)
		{
			attackUp.SetNativeSize();
			if (Card == null || Card.Type != CardType.ATTAKER)
			{
				return;
			}
			if (buff == 0)
			{
				Card.ExtraAttack = buff;
				attackUp.DOFade(0f, 0f);
				return;
			}
			attackUp.DOFade(1f, 0f);
			attackUp.DOFillAmount(1f, 0f);
			if (Card.ExtraAttack < buff)
			{
				Card.ExtraAttack = buff;
			}
		}

		private void DragStart(string arg1, PointerEventData arg2, Card dragCard, string arg4)
		{
			if (!_curDrag && !dragCard.IsEffectCard() && dragCard.Type == Card.Type && dragCard.Lv == Card.Lv)
			{
				tipImage.DOFade(1f, 0.2f);
			}
		}

		private void DragEnd(string arg1, PointerEventData arg2, Card arg3, DragBagGrid<Card> arg4)
		{
			tipImage.DOFade(0f, 0.2f);
		}

		protected override void OnDespawnedCallback(SpawnPool pool)
		{
			_isLife = false;
			BagDragManager<Card> instance = BagDragManager<Card>.Instance;
			instance.onDragStart -= DragStart;
			instance.onDragEnd -= DragEnd;
			EventManager.onNoticeGameResult -= NoticeGameResult;
			HideAllEffect();
			StopCoroutine("Power");
		}

		protected override void OnSpawnedCallback(SpawnPool pool)
		{
			_isLife = true;
			_curDrag = false;
			canvasGroup.alpha = 1f;
			base.transform.localScale = Vector3.one;
			_shootCount = 0;
			tipImage.DOFade(0f, 0f);
			HideAllEffect();
			BagDragManager<Card> instance = BagDragManager<Card>.Instance;
			instance.onDragStart += DragStart;
			instance.onDragEnd += DragEnd;
			EventManager.onNoticeGameResult += NoticeGameResult;
		}

		public void OverLock()
		{
			Debug.Log("超频攻击");
			for (int i = 0; i < 10; i++)
			{
				Yield();
			}
		}

		public void Transfer()
		{
			Card.Intensify = IntensifyType.TRANSFER;
			queenEffectAnimation.gameObject.SetActive(value: true);
			queenEffectAnimation.Play();
			Debug.Log("中转策略");
		}

		public void PlayEffectAnimation(CardType cardType)
		{
			if (cardType == CardType.CARD_FLOOD)
			{
				Card.isFlood = true;
			}
			else
			{
				_tempCardType = cardType;
			}
			effectAnimation.gameObject.SetActive(value: true);
			effectAnimation.Play(_customGuid);
			if (_card.Type == CardType.QUEEN)
			{
				if (cardType == CardType.CARD_OVERCLOCK_QUEEN)
				{
					Invoke("OverLock", 1.5f);
				}
				if (cardType == CardType.CARD_TRANSFER_QUEEN)
				{
					Invoke("Transfer", 1.5f);
				}
			}
			else if (_card.Type == CardType.ATTAKER)
			{
				StopCoroutine("Power");
				Invoke("AttackerEffect", 1f);
			}
		}

		private void AttackerEffect()
		{
			switch (_tempCardType)
			{
			case CardType.CARD_BUG:
				Card.Intensify = IntensifyType.BUG;
				break;
			case CardType.CARD_ICE:
				Card.Intensify = IntensifyType.ICE;
				break;
			case CardType.CARD_FLASH:
				Card.Intensify = IntensifyType.FLASH;
				break;
			case CardType.CARD_FLOOD:
				Card.Intensify = IntensifyType.FLOOD;
				break;
			}
			Show();
		}

		private void NoticeGameResult(GameResult obj)
		{
			_isStopShoot = true;
		}

		public override string PoolName()
		{
			return "DDOS";
		}
	}
}
