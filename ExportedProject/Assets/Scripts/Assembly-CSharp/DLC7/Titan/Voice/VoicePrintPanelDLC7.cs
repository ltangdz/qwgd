using System.Collections.Generic;
using DLC7.DDOS;
using Honeti;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace DLC7.Titan.Voice
{
	public class VoicePrintPanelDLC7 : MonoBehaviour
	{
		public Text tipText;

		public Button sureButton;

		public List<string> levelStrList;

		public List<VoicePrintRole> printRoles;

		public List<VoicePrintItem> waitingAreaItems;

		public List<VoicePrintItem> usedAreaItems;

		public SpriteAtlas voiceAtlas;

		private VoicePrintLevel _level;

		private string _tipStr;

		private List<string> _answerList;

		private VoicePrintRole _curSelectedRole;

		private int documentPos;

		public Button CloseButton;

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

		private void Start()
		{
			sureButton.onClick.AddListener(Sure);
			CloseButton.onClick.AddListener(Close);
		}

		private void Close()
		{
			base.transform.GetComponentInParent<TiTanDlc7>().ShowTotalPanel(isShow: true);
			Object.Destroy(base.gameObject);
		}

		private void Sure()
		{
			List<string> answer = _level.answer;
			for (int i = 0; i < answer.Count; i++)
			{
				string text = answer[i];
				bool flag = false;
				for (int j = 0; j < usedAreaItems.Count; j++)
				{
					VoicePrintModel dataItem = usedAreaItems[i].DataItem;
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
			Object.Instantiate(Resources.Load<TitanDialog>(DLCNameUtil.Instance.GetTitanTipDialogName()), base.transform).InitData(I18N.instance.getValue(isSuccess ? "^110008_game_125" : "^110008_game_124"), delegate
			{
				if (isSuccess)
				{
					TitanEventManager.Instance.NoticeDocumentSuccess(documentPos);
					Close();
				}
				else
				{
					for (int i = 0; i < usedAreaItems.Count; i++)
					{
						usedAreaItems[i].SaveData(null);
					}
					TitanEventManager.Instance.NoticeVoiceReset();
					for (int j = 0; j < waitingAreaItems.Count; j++)
					{
						waitingAreaItems[j].WaitingReset();
					}
				}
			});
		}

		public void InitData(int pos)
		{
			documentPos = pos;
			_level = JsonConvert.DeserializeObject<VoicePrintLevel>(levelStrList[pos - 1]);
			tipText.text = I18N.instance.getValue(_level.desc);
			List<VoicePrintRoleModel> data = _level.data;
			for (int i = 0; i < printRoles.Count; i++)
			{
				VoicePrintRole voicePrintRole = printRoles[i];
				VoicePrintRoleModel voicePrintRoleModel = null;
				if (i < data.Count)
				{
					voicePrintRoleModel = data[i];
					voicePrintRoleModel.InitVoicePrint();
				}
				voicePrintRole.InitData(voicePrintRoleModel, i == 0, ClickRole);
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
				VoicePrintRoleModel curModel = printRoles[i].CurModel;
				if (curModel == null || curModel.name != source)
				{
					continue;
				}
				for (int j = 0; j < curModel.modelList.Count; j++)
				{
					VoicePrintModel voicePrintModel = curModel.modelList[j];
					if (voicePrintModel.pathName == path)
					{
						voicePrintModel.isUsed = isUsed;
					}
				}
			}
			RefreshWaitingArea();
		}

		private void RefreshWaitingArea()
		{
			for (int i = 0; i < waitingAreaItems.Count; i++)
			{
				VoicePrintItem voicePrintItem = waitingAreaItems[i];
				VoicePrintRoleModel curModel = _curSelectedRole.CurModel;
				if (curModel == null)
				{
					voicePrintItem.SaveData(null);
					continue;
				}
				List<VoicePrintModel> modelList = curModel.modelList;
				if (modelList == null || modelList.Count == 0)
				{
					voicePrintItem.SaveData(null);
				}
				else if (modelList.Count <= i)
				{
					voicePrintItem.SaveData(null);
				}
				else
				{
					voicePrintItem.Init(modelList[i], "VoicePrint");
				}
			}
		}

		private void ClickRole(VoicePrintRole clickPanel)
		{
			_curSelectedRole = clickPanel;
			for (int i = 0; i < printRoles.Count; i++)
			{
				VoicePrintRole voicePrintRole = printRoles[i];
				voicePrintRole.Selected(voicePrintRole == clickPanel);
			}
			RefreshWaitingArea();
		}
	}
}
