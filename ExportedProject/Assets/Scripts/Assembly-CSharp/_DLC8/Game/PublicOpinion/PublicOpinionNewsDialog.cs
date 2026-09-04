using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aluba;
using AlubaExcelData.DataClass;
using DG.Tweening;
using UnityEngine;
using _DLC8.Common;

namespace _DLC8.Game.PublicOpinion
{
	public class PublicOpinionNewsDialog : CustomDialog
	{
		private enum ChangeType
		{
			NORAML = 0,
			TITAN = 1,
			ALUBA = 2,
			DANIEL = 3
		}

		public List<float> orderVal;

		public List<string> newsList;

		public string hotNewsLabel;

		public PublicOpinionNewsTitle titleItemPrefab;

		public PublicOpinionController controller;

		public Transform newsListContent;

		private GameObject hotNews;

		private int newsIndex;

		private GameObject alubaNews;

		private int alubaNewsIndex;

		private List<PublicOpinionNewsTitle> _itemList = new List<PublicOpinionNewsTitle>();

		private PublicOpinionNewsTitle _alubaItem;

		private PublicOpinionNewsTitle _titanItem;

		private PublicOpinionNewsTitle _danielItem;

		public float percent;

		private ArchiveData _archiveData;

		private int _titanRank;

		private int _danielRank;

		private int _alubaRank;

		private int _titanOff;

		private int _alubaOff;

		private int _danielOff;

		private int _normalOff;

		private int _totalOff;

		private List<ChangeType> _changeTypeList = new List<ChangeType>();

		public ArchiveData ArchiveData
		{
			get
			{
				if (_archiveData == null)
				{
					_archiveData = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData;
				}
				return _archiveData;
			}
		}

		private void Start()
		{
			Invoke("InitNews", 0.2f);
		}

		public void StartBalance(List<PublicOpinionInfo> infos)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			List<PublicOpinionMap> maps = controller.maps;
			for (int i = 0; i < maps.Count; i++)
			{
				PublicOpinionMap publicOpinionMap = maps[i];
				num3 += (float)(int)publicOpinionMap.personTotal;
				num2 += (float)(int)publicOpinionMap.TempNegativePersons;
				num += (float)(int)publicOpinionMap.TempPositivePersons;
			}
			float num4 = (percent = num * 1f / num3 * 100f);
			int num5 = 1;
			for (int j = 0; j < orderVal.Count; j++)
			{
				if (num4 < orderVal[j])
				{
					num5 = j + 1;
					break;
				}
			}
			if (num4 > 90f)
			{
				num5 = 12;
			}
			_alubaOff = 0;
			_danielOff = 0;
			for (int k = 0; k < infos.Count; k++)
			{
				PublicOpinionInfo publicOpinionInfo = infos[k];
				if (publicOpinionInfo.type == 2)
				{
					_alubaOff = (publicOpinionInfo.IsCorrect() ? (_alubaOff - 1) : (_alubaOff + 1));
				}
				else if (publicOpinionInfo.type == 3)
				{
					_danielOff = (publicOpinionInfo.IsCorrect() ? (_danielOff + 1) : (_danielOff - 1));
				}
			}
			_titanOff = num5 - _titanRank;
			_normalOff = Random.Range(1, 4);
			_totalOff = 0;
			if (_alubaOff != 0)
			{
				_totalOff += Mathf.Abs(_alubaOff);
				_changeTypeList.Add(ChangeType.ALUBA);
			}
			if (_titanOff != 0)
			{
				_totalOff += Mathf.Abs(_titanOff);
				_changeTypeList.Add(ChangeType.TITAN);
			}
			if (_danielOff != 0)
			{
				_totalOff += Mathf.Abs(_danielOff);
				_changeTypeList.Add(ChangeType.DANIEL);
			}
			if (_normalOff != 0)
			{
				_totalOff += Mathf.Abs(_normalOff);
				_changeTypeList.Add(ChangeType.NORAML);
			}
			StartCoroutine("ChangeRank");
		}

		private void InitNews()
		{
			_itemList.Clear();
			Debug.LogError("count:" + ArchiveData.NewsTitleList.Count);
			_ = controller.maps;
			Dictionary<string, PublicOpinionInitData> publicOpinionMapDataDic = SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.PublicOpinionMapDataDic;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < publicOpinionMapDataDic.Values.Count; i++)
			{
				PublicOpinionInitData publicOpinionInitData = publicOpinionMapDataDic.Values.ElementAt(i);
				num += publicOpinionInitData.total;
				int negative = publicOpinionInitData.negative;
				int positive = publicOpinionInitData.positive;
				num2 += negative;
				num3 += positive;
			}
			float num4 = (percent = (float)num3 * 1f / (float)num * 100f);
			int num5 = 1;
			for (int j = 0; j < orderVal.Count; j++)
			{
				if (num4 < orderVal[j])
				{
					num5 = j + 1;
					break;
				}
			}
			if (num4 > 90f)
			{
				num5 = 12;
			}
			int index = 0;
			for (int k = 0; k < ArchiveData.NewsTitleList.Count; k++)
			{
				if (ArchiveData.NewsTitleList[k].type == 1)
				{
					index = k;
					break;
				}
			}
			ArchiveData.NewsTitleList = AlubaTools.Swap(ArchiveData.NewsTitleList, num5 - 1, index);
			for (int l = 0; l < ArchiveData.NewsTitleList.Count; l++)
			{
				PublicOpinionNewsTitle publicOpinionNewsTitle = Object.Instantiate(titleItemPrefab, newsListContent);
				PublicOpinionNewsTitleInfo publicOpinionNewsTitleInfo = ArchiveData.NewsTitleList[l];
				publicOpinionNewsTitleInfo.rank = l + 1;
				if (publicOpinionNewsTitleInfo.type == 1)
				{
					_titanRank = publicOpinionNewsTitleInfo.rank;
					_titanItem = publicOpinionNewsTitle;
				}
				else if (publicOpinionNewsTitleInfo.type == 2)
				{
					_alubaRank = publicOpinionNewsTitleInfo.rank;
					_alubaItem = publicOpinionNewsTitle;
				}
				else if (publicOpinionNewsTitleInfo.type == 3)
				{
					_danielRank = publicOpinionNewsTitleInfo.rank;
					_danielItem = publicOpinionNewsTitle;
				}
				publicOpinionNewsTitle.Show(publicOpinionNewsTitleInfo);
				_itemList.Add(publicOpinionNewsTitle);
			}
		}

		private IEnumerator ChangeRank()
		{
			while (_changeTypeList.Count > 0)
			{
				ChangeType changeType = _changeTypeList[Random.Range(0, _changeTypeList.Count)];
				PublicOpinionNewsTitle curItem = null;
				bool flag = false;
				switch (changeType)
				{
				case ChangeType.TITAN:
					curItem = _titanItem;
					if (_titanOff < 0)
					{
						flag = true;
						_titanOff++;
					}
					else
					{
						_titanOff--;
					}
					if (_titanOff == 0)
					{
						_changeTypeList.Remove(changeType);
					}
					break;
				case ChangeType.ALUBA:
					curItem = _alubaItem;
					if (_alubaOff < 0)
					{
						flag = true;
						_alubaOff++;
					}
					else
					{
						_alubaOff--;
					}
					if (_alubaOff == 0)
					{
						_changeTypeList.Remove(changeType);
					}
					break;
				case ChangeType.DANIEL:
					if (_danielOff < 0)
					{
						flag = true;
						_danielOff++;
					}
					else
					{
						_danielOff--;
					}
					if (_danielOff == 0)
					{
						_changeTypeList.Remove(changeType);
					}
					curItem = _danielItem;
					break;
				case ChangeType.NORAML:
					flag = Random.Range(0, 2) == 1;
					_normalOff--;
					if (_normalOff == 0)
					{
						_changeTypeList.Remove(changeType);
					}
					curItem = FindNormalItem();
					break;
				}
				int a = ((!flag) ? 1 : (-1));
				if (!(curItem == null))
				{
					PublicOpinionNewsTitle changeItem = FindChangeItem(curItem, flag);
					if (!(changeItem == null))
					{
						curItem.content.transform.DOScaleY(0f, 0.3f).SetEase(Ease.Linear);
						changeItem.content.transform.DOScaleY(0f, 0.3f).SetEase(Ease.Linear);
						yield return new WaitForSeconds(0.4f);
						Debug.Log(curItem.Info.rank);
						curItem.SetRank(curItem.Info.rank + a);
						Debug.Log(curItem.Info.rank);
						changeItem.SetRank(changeItem.Info.rank - a);
						curItem.transform.SetSiblingIndex(curItem.Info.rank - 1);
						yield return new WaitForSeconds(0.2f);
						curItem.content.transform.DOScaleY(1f, 0.3f).SetEase(Ease.Linear);
						changeItem.content.transform.DOScaleY(1f, 0.3f).SetEase(Ease.Linear);
						yield return new WaitForSeconds(1f);
					}
				}
			}
			_titanRank = _titanItem.Info.rank;
			_alubaRank = _alubaItem.Info.rank;
			_danielRank = _danielItem.Info.rank;
			ArchiveData.NewsTitleList.Sort((PublicOpinionNewsTitleInfo x, PublicOpinionNewsTitleInfo y) => x.rank.CompareTo(y.rank));
		}

		private PublicOpinionNewsTitle FindNormalItem()
		{
			bool flag = true;
			int num = 100;
			while (flag && num > 0)
			{
				num--;
				PublicOpinionNewsTitle publicOpinionNewsTitle = _itemList[Random.Range(0, _itemList.Count)];
				int rank = publicOpinionNewsTitle.Info.rank;
				if (rank != 1 && rank != 12 && Mathf.Abs(rank - _alubaRank) > 1 && Mathf.Abs(rank - _danielRank) > 1 && Mathf.Abs(rank - _titanRank) > 1)
				{
					return publicOpinionNewsTitle;
				}
			}
			return null;
		}

		private PublicOpinionNewsTitle FindChangeItem(PublicOpinionNewsTitle curItem, bool isUp)
		{
			PublicOpinionNewsTitleInfo info = curItem.Info;
			_ = info.type;
			int rank = info.rank;
			if ((rank == 1 && isUp) || (rank == 12 && !isUp))
			{
				return null;
			}
			rank = (isUp ? (rank - 1) : (rank + 1));
			for (int i = 0; i < _itemList.Count; i++)
			{
				PublicOpinionNewsTitle publicOpinionNewsTitle = _itemList[i];
				if (publicOpinionNewsTitle.Info.rank == rank)
				{
					return publicOpinionNewsTitle;
				}
			}
			return null;
		}

		public override void AfterShowSize()
		{
		}

		public override void BeforeShowSize()
		{
		}
	}
}
