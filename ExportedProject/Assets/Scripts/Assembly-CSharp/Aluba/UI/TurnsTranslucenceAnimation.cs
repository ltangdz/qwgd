using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Aluba.UI
{
	public class TurnsTranslucenceAnimation : MonoBehaviour
	{
		public float interval = 1f;

		public float turnInterval;

		public float transVal = 0.3f;

		private List<Image> _childrenImageList;

		private int _index;

		private void Start()
		{
			_childrenImageList = GetComponentsInChildren<Image>().ToList();
			for (int i = 0; i < _childrenImageList.Count; i++)
			{
				_childrenImageList[i].DOFade(transVal, 0f);
			}
			StartCoroutine("Animation");
		}

		private IEnumerator Animation()
		{
			WaitForSeconds waitForSeconds = new WaitForSeconds(interval);
			int count = _childrenImageList.Count;
			while (count > 0)
			{
				_childrenImageList[_index].DOFade(1f, 0f);
				int num = _index - 1;
				if (num < 0)
				{
					num = count - 1;
				}
				_childrenImageList[num].DOFade(transVal, 0f);
				_index++;
				if (_index >= count)
				{
					_index = 0;
					yield return new WaitForSeconds(turnInterval);
				}
				yield return waitForSeconds;
			}
		}
	}
}
