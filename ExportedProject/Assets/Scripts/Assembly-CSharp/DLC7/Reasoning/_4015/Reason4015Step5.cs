using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DLC7.Reasoning._4015
{
	public class Reason4015Step5 : MonoBehaviour
	{
		public List<Question5Answer> questions;

		public List<Question5Answer> answers;

		public List<string> answerStrList = new List<string> { "^110008_tuili_22", "^110008_tuili_25", "^110008_tuili_29", "^110008_tuili_30" };

		private void Start()
		{
			for (int i = 0; i < 9; i++)
			{
				string key = $"^110008_tuili_{22 + i}";
				questions[i].Init(key);
			}
		}

		public void Ok(UnityAction callback)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < answers.Count; i++)
			{
				string dataItem = answers[i].DataItem;
				list.Add(dataItem);
			}
			bool flag = AlubaTools.ListEquals(list, answerStrList);
			for (int j = 0; j < answers.Count; j++)
			{
				answers[j].PlayAnimation(flag);
			}
			for (int k = 0; k < questions.Count; k++)
			{
				questions[k].PlayAnimation(flag);
			}
			if (flag)
			{
				callback?.Invoke();
			}
		}
	}
}
