using System.Collections.Generic;
using Aluba;
using AlubaExcelData.DataClass;
using Honeti;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using _DLC8.Common;

namespace _DLC8.Game.Voice
{
	public class VoicePrintPanelDLC8 : MonoBehaviour
	{
		public List<VoicePrintRoleDLC8> printRoles;

		public Text tipText;

		public List<VoicePrintItemDLC8> waitingAreaItems;

		public List<VoicePrintItemDLC8> usedAreaItems;

		public SpriteAtlas voiceAtlas;

		private VoicePrintLevel _voicePrintLevelData;

		private string _tipStr;

		private List<string> _answerList;

		private VoicePrintRoleDLC8 _curSelectedRoleDlc8;

		private LevelRecord _levelRecord;

		private VoicePrintEvent _eventManager;

		public VoicePrintEvent EventManager
		{
			get
			{
				if (_eventManager == null)
				{
					_eventManager = VoicePrintEvent.Instance;
				}
				return _eventManager;
			}
		}

		private void Close()
		{
			base.transform.GetComponentInParent<TiTanDlc7>().ShowTotalPanel(isShow: true);
			Object.Destroy(base.gameObject);
		}

		private void Sure()
		{
			List<string> answer = _voicePrintLevelData.answer;
			for (int i = 0; i < answer.Count; i++)
			{
				string text = answer[i];
				bool flag = false;
				for (int j = 0; j < usedAreaItems.Count; j++)
				{
					VoicePrintModelDLC8 dataItem = usedAreaItems[i].DataItem;
					if (dataItem == null)
					{
						ShowResult(isSuccess: false);
						return;
					}
					string pathName = dataItem.pathName;
					if (text == pathName)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					ShowResult(isSuccess: false);
					return;
				}
			}
			ShowResult(isSuccess: true);
		}

		private void ShowResult(bool isSuccess)
		{
			if (isSuccess)
			{
				DLC8EventManager.Instance.NoticeCommonEvent(DLC8CommonEvent.FINISH_GAMME, 0);
			}
		}

		public void InitData(LevelRecord record)
		{
			_levelRecord = record;
			int key = record.MapLevel * 2 + record.Level + 1;
			_voicePrintLevelData = new VoicePrintLevel();
			_voicePrintLevelData.answer = new List<string>();
			List<string> list = new List<string>();
			for (int i = 0; i < 5; i++)
			{
				string item = $"{key.ToString().PadLeft(2, '0')}_{i + 1}";
				_voicePrintLevelData.answer.Add(item);
				list.Add(item);
			}
			_answerList = _voicePrintLevelData.answer;
			List<string> list2 = new List<string>();
			for (int j = 1; j <= 63; j++)
			{
				if (j < 43)
				{
					list2.Add($"a ({j})");
				}
				else
				{
					list2.Add($"a-({j})");
				}
			}
			_voicePrintLevelData.data = new List<VoicePrintRoleModelDLC8>();
			VoiceLevel voiceLevel = SingletonAutoMono<DLC8DataController>.GetInstance().VoiceLevelDic[key];
			int count = voiceLevel.count;
			int tagCount = voiceLevel.tagCount;
			tipText.text = string.Format(I18N.instance.getValue("^110009_common_96"), tagCount);
			List<string> list3 = new List<string>();
			for (int k = 0; k < count; k++)
			{
				string item2 = list2[Random.Range(0, list2.Count)];
				list3.Add(item2);
				list2.Remove(item2);
			}
			int num = (5 + count) / tagCount;
			int num2 = Mathf.FloorToInt(5f / (float)tagCount);
			List<List<string>> list4 = new List<List<string>>();
			for (int l = 0; l < tagCount; l++)
			{
				List<string> list5 = new List<string>();
				for (int m = 0; m < num2; m++)
				{
					string item3 = list[Random.Range(0, list.Count)];
					list5.Add(item3);
					list.Remove(item3);
				}
				list4.Add(list5);
			}
			for (int n = 0; n < list.Count; n++)
			{
				int index = Random.Range(0, list4.Count);
				list4[index].Add(list[n]);
			}
			for (int num3 = 0; num3 < list4.Count; num3++)
			{
				while (list4[num3].Count < num)
				{
					string item4 = list3[Random.Range(0, list3.Count)];
					list4[num3].Add(item4);
					list3.Remove(item4);
				}
				list4[num3] = AlubaTools.RandomList(list4[num3]);
				int num4 = -1;
				for (int num5 = 0; num5 < list4[num3].Count; num5++)
				{
					string item5 = list4[num3][num5];
					int num6 = _answerList.IndexOf(item5);
					if (num4 != -1 && num6 - num4 == 1)
					{
						list4[num3] = AlubaTools.Swap(list4[num3], num5, num5 - 1);
					}
					num4 = num6;
				}
				VoicePrintRoleModelDLC8 voicePrintRoleModelDLC = new VoicePrintRoleModelDLC8();
				voicePrintRoleModelDLC.name = num3.ToString();
				voicePrintRoleModelDLC.list = new List<string>();
				for (int num7 = 0; num7 < list4[num3].Count; num7++)
				{
					string text = list4[num3][num7];
					voicePrintRoleModelDLC.list.Add(text);
					if (_answerList.IndexOf(text) > -1)
					{
						Debug.LogError($"{text}--tag:{num3 + 1}---pos:{num7 + 1}");
					}
				}
				_voicePrintLevelData.data.Add(voicePrintRoleModelDLC);
			}
			List<VoicePrintRoleModelDLC8> data = _voicePrintLevelData.data;
			for (int num8 = 0; num8 < printRoles.Count; num8++)
			{
				VoicePrintRoleDLC8 voicePrintRoleDLC = printRoles[num8];
				VoicePrintRoleModelDLC8 voicePrintRoleModelDLC2 = null;
				if (num8 < data.Count)
				{
					voicePrintRoleModelDLC2 = data[num8];
					voicePrintRoleModelDLC2.InitVoicePrint();
				}
				voicePrintRoleDLC.InitData(voicePrintRoleModelDLC2, num8 == 0, ClickRole);
			}
			ClickRole(printRoles[0]);
		}

		private void Awake()
		{
			EventManager.SetSpriteAtlas(voiceAtlas);
			EventManager.onNoticeUsed += NoticeUsed;
		}

		private void OnDestroy()
		{
			EventManager.onNoticeUsed -= NoticeUsed;
		}

		private void NoticeUsed(string source, string path, bool isUsed)
		{
			for (int i = 0; i < printRoles.Count; i++)
			{
				VoicePrintRoleModelDLC8 curModelDlc = printRoles[i].CurModelDlc8;
				if (curModelDlc == null || curModelDlc.name != source)
				{
					continue;
				}
				for (int j = 0; j < curModelDlc.modelList.Count; j++)
				{
					VoicePrintModelDLC8 voicePrintModelDLC = curModelDlc.modelList[j];
					if (voicePrintModelDLC.pathName == path)
					{
						voicePrintModelDLC.isUsed = isUsed;
					}
				}
			}
			RefreshWaitingArea();
			if (isUsed)
			{
				Sure();
			}
		}

		private void RefreshWaitingArea()
		{
			for (int i = 0; i < waitingAreaItems.Count; i++)
			{
				VoicePrintItemDLC8 voicePrintItemDLC = waitingAreaItems[i];
				VoicePrintRoleModelDLC8 curModelDlc = _curSelectedRoleDlc8.CurModelDlc8;
				if (curModelDlc == null)
				{
					voicePrintItemDLC.SaveData(null);
					continue;
				}
				List<VoicePrintModelDLC8> modelList = curModelDlc.modelList;
				if (modelList == null || modelList.Count == 0)
				{
					voicePrintItemDLC.SaveData(null);
				}
				else if (modelList.Count <= i)
				{
					voicePrintItemDLC.SaveData(null);
				}
				else
				{
					voicePrintItemDLC.Init(modelList[i], "VoicePrint");
				}
			}
		}

		private void ClickRole(VoicePrintRoleDLC8 clickPanel)
		{
			_curSelectedRoleDlc8 = clickPanel;
			for (int i = 0; i < printRoles.Count; i++)
			{
				VoicePrintRoleDLC8 voicePrintRoleDLC = printRoles[i];
				voicePrintRoleDLC.Selected(voicePrintRoleDLC == clickPanel);
			}
			RefreshWaitingArea();
		}
	}
}
