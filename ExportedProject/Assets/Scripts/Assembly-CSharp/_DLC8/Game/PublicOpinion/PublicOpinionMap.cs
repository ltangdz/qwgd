using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aluba;
using AlubaExcelData.DataClass;
using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Game.PublicOpinion
{
	public class PublicOpinionMap : MonoBehaviour
	{
		public string mapName;

		public ObscuredInt personTotal;

		public ObscuredInt[] personList;

		private ObscuredInt[] _offPersonList = new ObscuredInt[2];

		private ObscuredInt _tempPositivePersons;

		private ObscuredInt _tempNegativePersons;

		public List<int> trollList;

		public Sprite[] pointColorSprites;

		private List<Image> _pointImageList;

		private Image[] _mapImages = new Image[0];

		private TweenerCore<Color, Color, ColorOptions> _mapTweener0;

		private TweenerCore<Color, Color, ColorOptions> _mapTweener1;

		private float[] _taskVal = new float[2] { 0.4f, -0.25f };

		private PublicOpinionController _controller;

		private bool _startAnimation;

		private int _middleVal;

		private bool _isSelected;

		private bool _isPlayAnimation;

		private int _onePointToPerson;

		private Coroutine _redPointRun;

		private Coroutine _greenPointRun;

		private int _positivePercent;

		private int _negativePercent;

		private List<Image> positivePercentList = new List<Image>();

		private List<Image> negativePercentList = new List<Image>();

		private List<Image> changingPoint = new List<Image>();

		public ObscuredInt TempPositivePersons => _tempPositivePersons;

		public ObscuredInt TempNegativePersons => _tempNegativePersons;

		private void OnEnable()
		{
			PublicOpinionInitData publicOpinionInitData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.PublicOpinionMapDataDic[mapName];
			personTotal = publicOpinionInitData.total;
			personList[0] = publicOpinionInitData.positive;
			personList[1] = publicOpinionInitData.negative;
			GameObject gameObject = GameObject.Find(string.Format("map/{0}{1}", mapName, "/pointbox"));
			Image component = GameObject.Find(string.Format("map/{0}{1}", mapName, "/shadow")).GetComponent<Image>();
			Image component2 = GameObject.Find(string.Format("map/{0}{1}", mapName, "/tu")).GetComponent<Image>();
			_mapImages = new Image[2] { component, component2 };
			_pointImageList = gameObject.GetComponentsInChildren<Image>().ToList();
			GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
			GetComponent<Button>().onClick.AddListener(SelectedMap);
			_onePointToPerson = Mathf.FloorToInt((float)(int)personTotal * 1f / (float)_pointImageList.Count);
			DLC8EventManager.Instance.onNoticeSelectedMap += NoticeSelectedMap;
			if ((int)personList[0] >= (int)personTotal)
			{
				personList[0] = personTotal;
				personList[1] = 0;
			}
			if ((int)personList[1] >= (int)personTotal)
			{
				personList[1] = personTotal;
				personList[0] = 0;
			}
			_tempPositivePersons = personList[0];
			_tempNegativePersons = personList[1];
			InitPoint();
			StartCoroutine("RandomPoint");
			InvokeRepeating("ChangePoint", 0f, 3f);
		}

		public void InitController(PublicOpinionController controller)
		{
			_controller = controller;
		}

		private void NoticeSelectedMap(PublicOpinionMap map)
		{
			_isSelected = map == this;
			if (_mapTweener0 != null)
			{
				_mapTweener0.Kill();
			}
			if (_mapTweener1 != null)
			{
				_mapTweener1.Kill();
			}
			_mapTweener0 = _mapImages[0].DOFade(_isSelected ? 1 : 0, 0.3f);
			_mapTweener1 = _mapImages[1].DOFade(_isSelected ? 1 : 0, 0.3f);
		}

		public void StartAnimation(float detail = 10f)
		{
			if (!_isPlayAnimation)
			{
				_isPlayAnimation = true;
				DOTween.To(() => _tempPositivePersons, delegate(int x)
				{
					_tempPositivePersons = x;
				}, personList[0], detail).SetEase(Ease.Linear);
				DOTween.To(() => _tempNegativePersons, delegate(int x)
				{
					_tempNegativePersons = x;
				}, personList[1], detail).SetEase(Ease.Linear).OnUpdate(delegate
				{
				})
					.OnComplete(delegate
					{
						_isPlayAnimation = false;
					})
					.OnComplete(delegate
					{
						_isPlayAnimation = false;
					});
			}
		}

		public void UnSelectedMap()
		{
		}

		private void SelectedMap()
		{
			if (!_isSelected)
			{
				DLC8EventManager.Instance.GameManager.soundManager.Stop();
				DLC8EventManager.Instance.GameManager.soundManager.PlaySound(36);
				DLC8EventManager.Instance.NoticeSelectedMap(this);
				_controller.ClickMap(this);
			}
		}

		public void CountVal(List<PublicOpinionInfo> infos)
		{
			Debug.LogError(string.Concat("最初：mapName:", mapName, "=====", personList[0], ":", personList[1]));
			int num = 0;
			int num2 = 0;
			bool flag = false;
			for (int i = 0; i < infos.Count; i++)
			{
				PublicOpinionInfo publicOpinionInfo = infos[i];
				if (!(publicOpinionInfo.city != mapName))
				{
					bool isCorrect = publicOpinionInfo.IsCorrect();
					num = PositiveVal(isCorrect, publicOpinionInfo);
					num2 = NegativeVal(isCorrect, publicOpinionInfo);
					flag = true;
					Debug.LogError(string.Concat("mapName:", mapName, "|||", publicOpinionInfo.positionType, ":", isCorrect.ToString(), "::", personList[0], ":", personList[1]));
				}
			}
			if (flag)
			{
				personList[0] = num;
				personList[1] = num2;
			}
			if ((int)personList[0] >= (int)personTotal)
			{
				personList[0] = personTotal;
				personList[1] = 0;
			}
			if ((int)personList[1] >= (int)personTotal)
			{
				personList[1] = personTotal;
				personList[0] = 0;
			}
			if ((int)personTotal - (int)personList[0] - (int)personList[1] < 0)
			{
				personList[1] = (int)personTotal - (int)personList[0];
			}
			Debug.LogError("=============================");
			ArchiveData archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
			archiveData.PublicOpinionMapDataDic[mapName].positive = personList[0];
			archiveData.PublicOpinionMapDataDic[mapName].negative = personList[1];
			if (_redPointRun != null)
			{
				StopCoroutine(_redPointRun);
			}
			if (_greenPointRun != null)
			{
				StopCoroutine(_greenPointRun);
			}
			_redPointRun = StartCoroutine(ShowRedPoint(isUpdate: true));
			_greenPointRun = StartCoroutine(ShowGreenPoint(isUpdate: true));
			StartAnimation();
		}

		private float StateAttributeVal()
		{
			return (float)TrollTotal() / 30f;
		}

		private int TrollTotal()
		{
			int num = 0;
			for (int i = 0; i < trollList.Count; i++)
			{
				int index = trollList[i];
				num += (int)_controller.trollDialog.tipList[index].val;
			}
			return num;
		}

		private int PositiveVal(bool isCorrect, PublicOpinionInfo info)
		{
			float num = StateAttributeVal();
			int num2 = personList[0];
			int num3 = MiddleChangeVal(isCorrect, info);
			if (isCorrect)
			{
				int num4 = MiddleVal();
				float num5 = Random.Range(0f, 1f);
				return Mathf.Max(Mathf.RoundToInt((float)num4 * num * num5 * 2f) + num2 + num3, 0);
			}
			return Mathf.Max(Mathf.FloorToInt((float)num2 * (1f + _taskVal[1] - num) + (float)num3), 0);
		}

		private int NegativeVal(bool isCorrect, PublicOpinionInfo info)
		{
			ObscuredInt obscuredInt = personList[1];
			int num = MiddleChangeVal(isCorrect, info);
			if (isCorrect)
			{
				float num2 = 1f - _taskVal[0];
				return Mathf.Max(Mathf.FloorToInt((float)(int)obscuredInt * num2 - (float)num / 3f), 0);
			}
			return Mathf.Max(Mathf.FloorToInt((int)obscuredInt - num), 0);
		}

		private int MiddleVal()
		{
			int num = personList[0];
			int num2 = personList[1];
			int num3 = (int)personTotal - num - num2;
			_middleVal = ((num3 >= 0) ? num3 : 0);
			return _middleVal;
		}

		private int MiddleChangeVal(bool isCorrect, PublicOpinionInfo info)
		{
			float num = _taskVal[(!isCorrect) ? 1u : 0u];
			float num2 = (isCorrect ? 1f : (-1f));
			return Mathf.FloorToInt((float)MiddleVal() * num * (1f + (float)(int)info.roleNum / 20f * num2));
		}

		private void CalPoint()
		{
			int num = (int)Mathf.Floor((int)personList[0] / _onePointToPerson);
			int num2 = (int)Mathf.Floor((int)personList[1] / _onePointToPerson);
			_positivePercent = (((int)personList[0] != 0) ? ((num < 1) ? 1 : num) : 0);
			_negativePercent = (((int)personList[1] != 0) ? ((num2 < 1) ? 1 : num2) : 0);
		}

		private void OnDisable()
		{
			DLC8EventManager.Instance.onNoticeSelectedMap -= NoticeSelectedMap;
			StopCoroutine("RandomPoint");
		}

		private IEnumerator ShowRedPoint(bool isUpdate = false)
		{
			yield break;
		}

		private void InitPoint()
		{
			int num = Mathf.Max((int)Mathf.Floor((float)(int)personList[0] * 1f / (float)_onePointToPerson), 1);
			for (int i = 0; i < num; i++)
			{
				if (_pointImageList.Count > 0)
				{
					Image image = _pointImageList[Random.Range(0, _pointImageList.Count)];
					image.sprite = pointColorSprites[0];
					positivePercentList.Add(image);
					_pointImageList.Remove(image);
				}
			}
			negativePercentList.AddRange(_pointImageList);
			for (int j = 0; j < positivePercentList.Count; j++)
			{
				float endValue = (float)Random.Range(8, 16) * 0.1f;
				positivePercentList[j].transform.DOScale(endValue, 0f);
			}
			for (int k = 0; k < negativePercentList.Count; k++)
			{
				float endValue2 = (float)Random.Range(5, 16) * 0.1f;
				negativePercentList[k].transform.DOScale(endValue2, 0f);
			}
		}

		private IEnumerator RandomPoint()
		{
			while (true)
			{
				int minCount = Mathf.FloorToInt((float)Mathf.Max(positivePercentList.Count, negativePercentList.Count) / 3f);
				for (int i = 0; i < minCount; i++)
				{
					if (positivePercentList.Count > 0)
					{
						Image image = positivePercentList[Random.Range(0, positivePercentList.Count)];
						if (!changingPoint.Contains(image) && positivePercentList.Count > 1)
						{
							changingPoint.Add(image);
							StartCoroutine(ChangeColor(image, toGreen: true, isRandom: true));
						}
					}
					yield return new WaitForSeconds(Random.Range(0.1f, 1f));
					if (negativePercentList.Count > 0)
					{
						Image image2 = negativePercentList[Random.Range(0, negativePercentList.Count)];
						if (!changingPoint.Contains(image2) && negativePercentList.Count > 1)
						{
							changingPoint.Add(image2);
							StartCoroutine(ChangeColor(image2, toGreen: false, isRandom: true));
						}
					}
					yield return new WaitForSeconds(Random.Range(1f, 2.5f));
				}
				yield return new WaitForSeconds(Random.Range(5f, 10f));
			}
		}

		private void ChangePoint()
		{
			int num = (((int)personList[0] != 0) ? Mathf.Max((int)((float)(int)personList[0] * 1f / (float)_onePointToPerson), 1) : 0);
			int count = positivePercentList.Count;
			int num2 = num - count;
			for (int i = 0; i < Mathf.Abs(num2); i++)
			{
				if (num2 > 0)
				{
					if (negativePercentList.Count != 0)
					{
						Image image = negativePercentList[Random.Range(0, negativePercentList.Count)];
						changingPoint.Add(image);
						positivePercentList.Add(image);
						negativePercentList.Remove(image);
						StartCoroutine(ChangeColor(image, toGreen: true, isRandom: false));
					}
				}
				else if (count != 0)
				{
					int num3 = Random.Range(0, count);
					if (positivePercentList.Count > num3)
					{
						Image image2 = positivePercentList[num3];
						changingPoint.Add(image2);
						positivePercentList.Remove(image2);
						negativePercentList.Add(image2);
						StartCoroutine(ChangeColor(image2, toGreen: false, isRandom: false));
					}
				}
			}
		}

		private IEnumerator ChangeColor(Image image, bool toGreen, bool isRandom)
		{
			yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
			image.transform.DOScale(0f, 1f).SetEase(Ease.Linear).OnComplete(delegate
			{
				image.sprite = pointColorSprites[(!toGreen) ? 1u : 0u];
			});
			yield return new WaitForSeconds(Random.Range(isRandom ? 3f : 0.5f, isRandom ? 8f : 1.5f));
			float endValue = (float)Random.Range(6, 16) * 0.1f;
			image.transform.DOScale(endValue, 1f).OnComplete(delegate
			{
				changingPoint.Remove(image);
			});
		}

		private IEnumerator ShowGreenPoint(bool isUpdate = false)
		{
			yield break;
		}

		private IEnumerator HidePoint(Image pointObj, List<Image> listObj, int val)
		{
			float num = Random.Range(Mathf.Ceil((val <= 1) ? 1 : (val - 1)), val + 1);
			yield return new WaitForSeconds(num * 1.4f);
			pointObj.GetComponent<RectTransform>().DOScale(new Vector3(0f, 0f, 0f), 0.5f);
			listObj.Remove(pointObj);
		}
	}
}
