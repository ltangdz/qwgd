using System.Collections.Generic;
using DG.Tweening;
using DLC7.DDOS;
using Honeti;
using UnityEngine;

namespace DLC7.Titan
{
	public class TotalPanelDlc7 : MonoBehaviour
	{
		public BigItem developitem;

		public BigItem voiceitem;

		public BigItem emailitem;

		public BigItem personitem;

		[SerializeField]
		private GameObject totalpanel;

		[SerializeField]
		private GameObject voicepanel;

		[SerializeField]
		private GameObject personpanel;

		[SerializeField]
		private GameObject emailpanel;

		[SerializeField]
		private GameObject developmentpanel;

		public CanvasGroup houtaipanel;

		public GameManager gameManager;

		public FrameAnimation2D titleAnimation2d;

		private List<List<string>> _reportDataList;

		public void InitData(List<List<string>> reportDataList)
		{
			_reportDataList = reportDataList;
		}

		public void ClosePanel()
		{
			houtaipanel.DOFade(0f, 0.2f);
			houtaipanel.transform.DOScale(Vector3.zero, 0.2f).OnComplete(delegate
			{
				gameManager.musicManager.PlayMusicLoop(3);
				Object.Destroy(houtaipanel.gameObject);
			});
		}

		private void NoticeDocumentSuccess(int id)
		{
			gameManager.player.playerdata.TitanDocumentUnlock.Add(id);
			gameManager.saveManager.SavePlayerData();
			RefreshPanel();
		}

		private void Start()
		{
			gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
			Invoke("SetDlc", 2f);
			gameManager.musicManager.PlayMusicLoop(27);
		}

		private void SetDlc()
		{
			gameManager.player.playerdata.GameType = GameTypeEnum.DLC7;
			gameManager.GameType = GameTypeEnum.DLC7;
		}

		private void RefreshPanel()
		{
			developitem.RefreshData();
			voiceitem.RefreshData();
			emailitem.RefreshData();
			personitem.RefreshData();
		}

		public void ShowPanel(int panelid)
		{
			Object.Instantiate(Resources.Load<TitanDocumentDialog>("_DLC7/prefabs/Report/TitanDocumentDialog"), base.transform.parent.parent).InitData(I18N.instance.getValue($"^110008_game_{120 + panelid}"), _reportDataList[panelid], this);
			totalpanel.SetActive(value: false);
		}

		private void OnEnable()
		{
			titleAnimation2d.Play();
		}

		private void Awake()
		{
			TitanEventManager.Instance.onNoticeDocumentSuccess += NoticeDocumentSuccess;
		}

		private void OnDestroy()
		{
			TitanEventManager.Instance.onNoticeDocumentSuccess -= NoticeDocumentSuccess;
		}
	}
}
