using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleVoice : MonoBehaviour
{
	[SerializeField]
	private TotalPanel totalPanel;

	[SerializeField]
	private VoicePanel voicePanel;

	[SerializeField]
	private Button btn_sure;

	[SerializeField]
	private List<VoiceBlank> voiceBlanks = new List<VoiceBlank>();

	[SerializeField]
	private string correctanswer;

	public VoiceLoadingDialog voiceloadingdialog;

	public int type;

	private void Start()
	{
		btn_sure.onClick.AddListener(delegate
		{
			string text = "";
			for (int i = 0; i < voiceBlanks.Count; i++)
			{
				if (voiceBlanks[i].bigVoiceItem != null)
				{
					text += voiceBlanks[i].bigVoiceItem.id;
				}
			}
			if (correctanswer.Equals(text))
			{
				ShowLoadingDialog(1);
				Debug.Log("成功");
				if (type == 0)
				{
					totalPanel.ishascio = true;
					totalPanel.gameManager.player.playerdata.ishasciovoice = true;
				}
				else
				{
					totalPanel.ishastom = true;
					totalPanel.gameManager.player.playerdata.ishastomblancovoice = true;
				}
				totalPanel.gameManager.saveManager.SavePlayerData();
				voicePanel.RefreshCioItem();
			}
			else
			{
				ShowLoadingDialog(2);
				Debug.Log("失败");
			}
		});
	}

	private void ShowLoadingDialog(int type)
	{
		voiceloadingdialog.gameObject.SetActive(value: true);
		voiceloadingdialog.Show(type);
	}
}
