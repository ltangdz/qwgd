using Honeti;
using PathologicalGames;
using UnityEngine.UI;

namespace DLC7.DDOS
{
	public class CardTip : AlubaSpawn
	{
		public Text titleText;

		public Text descText;

		public Text extraText;

		private I18N _i18N;

		public I18N I18N
		{
			get
			{
				if (_i18N == null)
				{
					_i18N = I18N.instance;
				}
				return _i18N;
			}
		}

		public void Hide()
		{
			base.Pool.Despawn(base.transform);
		}

		public void InitData(Card card)
		{
			extraText.gameObject.SetActive(!card.IsEffectCard());
			switch (card.Type)
			{
			case CardType.ATTAKER:
				titleText.text = string.Format("[{0}]", I18N.getValue("^110008_game_91"));
				descText.text = I18N.getValue("^110008_game_34");
				extraText.text = string.Format(I18N.getValue("^110008_game_93"), card.Attack, card.ExtraAttack);
				break;
			case CardType.QUEEN:
				titleText.text = string.Format("[{0}]", I18N.getValue("^110008_game_92"));
				descText.text = I18N.getValue("^110008_game_32");
				extraText.text = string.Format(I18N.getValue("^110008_game_94"), card.Attack, card.QueenBuff);
				break;
			case CardType.CARD_ICE:
				titleText.text = I18N.getValue("^110008_game_68");
				descText.text = I18N.getValue("^110008_game_70");
				break;
			case CardType.CARD_FLASH:
				titleText.text = I18N.getValue("^110008_game_71");
				descText.text = I18N.getValue("^110008_game_73");
				break;
			case CardType.CARD_BUG:
				titleText.text = I18N.getValue("^110008_game_74");
				descText.text = I18N.getValue("^110008_game_76");
				break;
			case CardType.CARD_FLOOD:
				titleText.text = I18N.getValue("^110008_game_65");
				descText.text = I18N.getValue("^110008_game_66");
				break;
			case CardType.CARD_TRANSFER_QUEEN:
				titleText.text = I18N.getValue("^110008_game_77");
				descText.text = I18N.getValue("^110008_game_79");
				break;
			case CardType.CARD_OVERCLOCK_QUEEN:
				titleText.text = I18N.getValue("^110008_game_63");
				descText.text = I18N.getValue("^110008_game_64");
				break;
			}
		}

		protected override void OnSpawnedCallback(SpawnPool pool)
		{
		}

		protected override void OnDespawnedCallback(SpawnPool pool)
		{
		}

		public override string PoolName()
		{
			return "DDOS";
		}
	}
}
