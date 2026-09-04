using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DLC7.DDOS;
using DLC7.Titan;
using Honeti;
using UnityEngine;

namespace DLC7.SignalLight
{
	public class TitanLightController : TitanLightBase
	{
		public List<TitanLightQuestion> questionList;

		public RectTransform progressRT;

		private int _curQuestion;

		private List<int> _numberList = new List<int> { 0, 1, 2, 3 };

		private List<int> _resultList;

		private int _maxCount;

		private float progressMaxWidth;

		public float maxTime = 10f;

		private TweenerCore<Vector3, Vector3, VectorOptions> _progressTween;

		private void Start()
		{
			_resultList = new List<int>();
			_curQuestion = 0;
			questionList[3].gameObject.SetActive(_curQuestion == 2);
			RandomQuestion();
		}

		private void RandomQuestion()
		{
			_maxCount = 3;
			if (_curQuestion == 2)
			{
				_maxCount = 4;
			}
			List<int> list = AlubaTools.RandomList(_numberList.GetRange(0, _maxCount));
			int num = Random.Range(0, list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				int number = list[i];
				questionList[i].InitData(number, _curQuestion == 0 || ((num != i) ? true : false));
			}
			base.EventManager.NoticeIdle(_curQuestion);
		}

		protected override void NoticeIdle(int step)
		{
		}

		protected override void NoticeStartGame()
		{
			if (_progressTween != null)
			{
				_progressTween.Kill();
			}
			_progressTween = progressRT.DOScaleX(0f, maxTime).SetEase(Ease.Linear).OnComplete(delegate
			{
				base.EventManager.NoticeFail(_curQuestion);
			});
		}

		protected override void NoticeSelectedResult(int number, bool isSuccess)
		{
			int count = _resultList.Count;
			bool flag = false;
			if (number == count && isSuccess)
			{
				flag = true;
				_resultList.Add(count);
			}
			if (!flag)
			{
				base.EventManager.NoticeFail(_curQuestion);
				_resultList.Clear();
				_curQuestion = 0;
				questionList[3].gameObject.SetActive(_curQuestion == 2);
			}
			else if (_resultList.Count >= _maxCount)
			{
				base.EventManager.NoticeSuccess(_curQuestion);
				_curQuestion++;
				questionList[3].gameObject.SetActive(_curQuestion == 2);
				_resultList.Clear();
				if (_curQuestion < 3)
				{
					Invoke("RandomQuestion", 2f);
				}
			}
		}

		protected override void NoticeSuccess(int step)
		{
			if (_progressTween != null)
			{
				_progressTween.Kill();
			}
			progressRT.DOScale(1f, 0f);
			if (step == 2)
			{
				Object.Instantiate(Resources.Load<TitanDialog>(DLCNameUtil.Instance.GetTitanTipDialogName()), base.transform).InitData(I18N.instance.getValue("^110008_game_108"), delegate
				{
					TitanEventManager.Instance.NoticeDocumentSuccess(0);
					Object.Destroy(base.gameObject);
				});
				Camera.main.GetComponent<CameraFilterPack_FX_Glitch1>().enabled = false;
			}
		}

		protected override void NoticeFail(int step)
		{
			if (_progressTween != null)
			{
				_progressTween.Kill();
			}
			progressRT.DOScale(1f, 0f);
		}

		protected override void NoticeResetGame()
		{
			_resultList.Clear();
			_curQuestion = 0;
			if (_progressTween != null)
			{
				_progressTween.Kill();
			}
			progressRT.DOScale(1f, 0f);
			Invoke("RandomQuestion", 2f);
		}
	}
}
