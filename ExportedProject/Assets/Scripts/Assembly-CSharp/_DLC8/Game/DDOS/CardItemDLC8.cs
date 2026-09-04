using System.Collections;
using System.Collections.Generic;
using Aluba;
using DG.Tweening;
using DLC7;
using DLC7.DDOS;
using PathologicalGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _DLC8.Game.DDOS
{
	public class CardItemDLC8 : AlubaSpawnDLC8
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

		[FormerlySerializedAs("_card")]
		public CardDLC8 cardDlc8;

		private PositionType _positionType;

		private bool _curDrag;

		private DDOSEventManagerDLC8 _eventManagerDlc8;

		private bool _isLife;

		private string _customGuid;

		private bool _isStopShoot;

		private int _shootCount;

		private CardType _tempCardType;

		private DDOSManagerDLC8 _ddosManagerDlc8;

		public GameObject maxObj;

		public CardDLC8 CardDlc8 => cardDlc8;

		public DDOSManagerDLC8 DdosManagerDlc8
		{
			get
			{
				if (_ddosManagerDlc8 == null)
				{
					_ddosManagerDlc8 = DDOSManagerDLC8.Instance;
				}
				return _ddosManagerDlc8;
			}
		}

		public DDOSEventManagerDLC8 EventManagerDlc8 => _eventManagerDlc8 ?? DDOSEventManagerDLC8.Instance;

		public void InitData(CardDLC8 cardDlc8, PositionType positionType)
		{
			_isStopShoot = false;
			_customGuid = GetInstanceID().ToString();
			animationNames = new List<string>
			{
				$"{_customGuid}EffectAnimation",
				$"{_customGuid}QueenEffectAnimation",
				$"{_customGuid}AttackAnimation"
			};
			this.cardDlc8 = cardDlc8;
			if (this.cardDlc8.Type == CardType.QUEEN)
			{
				lvGroup.gameObject.SetActive(value: true);
				lvGroup.localPosition = new Vector2(lvGroup.localPosition.x, -25f);
				if (this.cardDlc8.Intensify == IntensifyType.TRANSFER)
				{
					queenEffectAnimation.gameObject.SetActive(value: true);
					queenEffectAnimation.Play();
				}
			}
			else if (this.cardDlc8.Type == CardType.ATTAKER)
			{
				lvGroup.gameObject.SetActive(value: true);
				lvGroup.localPosition = new Vector2(lvGroup.localPosition.x, -50f);
			}
			else
			{
				lvGroup.gameObject.SetActive(value: false);
			}
			IsMax();
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
			DdosManagerDlc8.InitImage(CardDlc8.FramePath, frameImage);
			DdosManagerDlc8.InitImage(CardDlc8.ContentPath, contentImage);
			lvText.text = CardDlc8.Lv.ToString();
			if (_positionType == PositionType.ATTACKER && (CardDlc8.Type == CardType.QUEEN || CardDlc8.Type == CardType.ATTAKER))
			{
				StopCoroutine("Power");
				StartCoroutine("Power");
			}
			IsMax();
		}

		private void IsMax()
		{
			int cardMaxLevel = SingletonAutoMono<DLC8DataController>.GetInstance().GetDDOSCityMapData().cardMaxLevel;
			if (cardDlc8.Lv == cardMaxLevel)
			{
				lvGroup.gameObject.SetActive(value: false);
				maxObj.gameObject.SetActive(value: true);
			}
			else
			{
				lvGroup.gameObject.SetActive(value: true);
				maxObj.gameObject.SetActive(value: false);
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
				yield return new WaitForSeconds((cardDlc8.Type == CardType.QUEEN) ? 8f : cardDlc8.CurInterval);
				if (cardDlc8.Type == CardType.QUEEN)
				{
					Yield();
				}
				else if (cardDlc8.Type == CardType.ATTAKER)
				{
					if (CardDlc8.isFlood)
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
			switch (CardDlc8.Intensify)
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
			DDOSBulletDLC8 bulletDlc = InitBullet();
			EnemyDLC8 enemyDlc = GetEnemyList(1)[0];
			ShootEnemy(enemyDlc, bulletDlc, 7f);
		}

		private IEnumerator FloodShoot()
		{
			StopCoroutine("Power");
			for (int i = 0; i < 4; i++)
			{
				List<EnemyDLC8> enmEnemies = GetEnemyList(5);
				for (int j = 0; j < enmEnemies.Count; j++)
				{
					DDOSBulletDLC8 bulletDlc = InitBullet();
					ShootEnemy(enmEnemies[j], bulletDlc, 14f);
					yield return new WaitForSeconds(0.2f);
				}
			}
			CardDlc8.isFlood = false;
			StartCoroutine("Power");
		}

		private void BugShoot()
		{
			List<EnemyDLC8> enemyList = GetEnemyList(3);
			if (_shootCount % 3 == 0)
			{
				for (int i = 0; i < enemyList.Count; i++)
				{
					DDOSBulletDLC8 bulletDlc = InitBullet();
					ShootEnemy(enemyList[i], bulletDlc, 7f);
				}
			}
			else
			{
				DDOSBulletDLC8 bulletDlc2 = InitBullet();
				ShootEnemy(enemyList[0], bulletDlc2, 7f);
			}
		}

		private void ShootEnemy(EnemyDLC8 enemyDlc8, DDOSBulletDLC8 bulletDlc8, float speed)
		{
			if (enemyDlc8 == null)
			{
				bulletDlc8.FreeShoot(GetBulletType(), speed, CardDlc8.isFlood);
			}
			else
			{
				bulletDlc8.Shoot(GetBulletType(), enemyDlc8.GetComponent<RectTransform>(), speed, CardDlc8.GetDamaged(), CardDlc8.isFlood);
			}
		}

		public void StopPower()
		{
			StopCoroutine("Power");
			StopCoroutine("FloodShoot");
		}

		private DDOSBulletDLC8 InitBullet()
		{
			DDOSBulletDLC8 component = DdosManagerDlc8.SpawnPool.Spawn("DDOSBulletDLC8").GetComponent<DDOSBulletDLC8>();
			if (!attackAnimation.gameObject.activeSelf)
			{
				attackAnimation.gameObject.SetActive(value: true);
			}
			component.transform.position = base.transform.position;
			return component;
		}

		private List<EnemyDLC8> GetEnemyList(int number)
		{
			Vector2 b = base.transform.position;
			List<EnemyDLC8> list = new List<EnemyDLC8>();
			Dictionary<int, float> dictionary = new Dictionary<int, float>();
			List<EnemyDLC8> enemies = DdosManagerDlc8.Enemies;
			for (int i = 0; i < enemies.Count; i++)
			{
				EnemyDLC8 enemyDLC = enemies[i];
				if (!enemyDLC.IsSafe)
				{
					float value = Mathf.Abs(Vector2.Distance(enemyDLC.transform.position, b));
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
			switch (CardDlc8.Intensify)
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
			CoinItemDLC8 component = DdosManagerDlc8.SpawnPool.Spawn("CoinItemDLC8").GetComponent<CoinItemDLC8>();
			component.transform.position = base.transform.position;
			component.InitData(CardDlc8.Attack, isCard: true);
		}

		public void Upgrade(IntensifyType intensifyType)
		{
			CardDlc8.Intensify = intensifyType;
			EventManagerDlc8.NoticeSound(DdosSound.FUSION);
			CardDlc8.Upgrade(DdosManagerDlc8);
			Show();
		}

		public void QueenTransfer(int buff)
		{
			attackUp.SetNativeSize();
			if (CardDlc8 == null || CardDlc8.Type != CardType.ATTAKER)
			{
				return;
			}
			if (buff == 0)
			{
				CardDlc8.ExtraAttack = buff;
				attackUp.DOFade(0f, 0f);
				return;
			}
			attackUp.DOFade(1f, 0f);
			attackUp.DOFillAmount(1f, 0f);
			if (CardDlc8.ExtraAttack < buff)
			{
				CardDlc8.ExtraAttack = buff;
			}
		}

		private void DragStart(string arg1, PointerEventData arg2, CardDLC8 dragCardDlc8, string arg4)
		{
			if (!_curDrag && !dragCardDlc8.IsEffectCard() && dragCardDlc8.Type == CardDlc8.Type && dragCardDlc8.Lv == CardDlc8.Lv)
			{
				tipImage.DOFade(1f, 0.2f);
			}
		}

		private void DragEnd(string arg1, PointerEventData arg2, CardDLC8 arg3, DragBagGrid<CardDLC8> arg4)
		{
			tipImage.DOFade(0f, 0.2f);
		}

		protected override void OnDespawnedCallback(SpawnPool pool)
		{
			_isLife = false;
			BagDragManager<CardDLC8> instance = BagDragManager<CardDLC8>.Instance;
			instance.onDragStart -= DragStart;
			instance.onDragEnd -= DragEnd;
			EventManagerDlc8.onNoticeGameResult -= NoticeGameResult;
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
			BagDragManager<CardDLC8> instance = BagDragManager<CardDLC8>.Instance;
			instance.onDragStart += DragStart;
			instance.onDragEnd += DragEnd;
			EventManagerDlc8.onNoticeGameResult += NoticeGameResult;
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
			CardDlc8.Intensify = IntensifyType.TRANSFER;
			queenEffectAnimation.gameObject.SetActive(value: true);
			queenEffectAnimation.Play();
			Debug.Log("中转策略");
		}

		public void PlayEffectAnimation(CardType cardType)
		{
			if (cardType == CardType.CARD_FLOOD)
			{
				CardDlc8.isFlood = true;
			}
			else
			{
				_tempCardType = cardType;
			}
			effectAnimation.gameObject.SetActive(value: true);
			effectAnimation.Play(_customGuid);
			if (cardDlc8.Type == CardType.QUEEN)
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
			else if (cardDlc8.Type == CardType.ATTAKER)
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
				CardDlc8.Intensify = IntensifyType.BUG;
				break;
			case CardType.CARD_ICE:
				CardDlc8.Intensify = IntensifyType.ICE;
				break;
			case CardType.CARD_FLASH:
				CardDlc8.Intensify = IntensifyType.FLASH;
				break;
			case CardType.CARD_FLOOD:
				CardDlc8.Intensify = IntensifyType.FLOOD;
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
			return "DDOSDLC8";
		}
	}
}
