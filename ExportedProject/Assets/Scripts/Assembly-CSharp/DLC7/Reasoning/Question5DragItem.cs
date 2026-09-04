using System.Collections.Generic;
using DLC7.DDOS;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

namespace DLC7.Reasoning
{
	public class Question5DragItem : DragBagItem<string>
	{
		public Text text;

		private void Awake()
		{
			_groupKey = "Question5";
		}

		public override void InitUI(string t)
		{
			text.text = (string.IsNullOrEmpty(t) ? "" : I18N.instance.getValue(t));
		}

		public override void DragEnd(DragBagGrid<string> bagGrid, List<Collider2D> touchList)
		{
			Question5Answer question5Answer = (Question5Answer)bagGrid;
			if (touchList.Count == 0)
			{
				question5Answer.Cancel();
				return;
			}
			Question5Answer component = touchList[0].gameObject.GetComponent<Question5Answer>();
			if (component.questionType == QuestionType.QUESTION)
			{
				question5Answer.Cancel();
				return;
			}
			component.InitData(bagGrid.DataItem);
			question5Answer.Used();
		}
	}
}
