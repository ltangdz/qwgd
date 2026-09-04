using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _DLC8.Main.Rank
{
	public class LaborerRankItem : MonoBehaviour
	{
		public Text rankText;

		public Text nameText;

		public Text scoreText;

		private LaborerRankData _data;

		public void Init(LaborerRankData data)
		{
			_data = data;
			rankText.text = $"{_data.rank}.";
			nameText.text = _data.nameString;
			scoreText.text = _data.scoreString;
			if (_data.rank > 3 && _data.rank % 2 == 0)
			{
				GetComponent<Image>().DOFade(0f, 0f);
			}
		}
	}
}
