using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NetDialogDLC : CustomDialog
{
	public Button _sureButton;

	public Image _tip;

	public InputField _numberInput;

	public InputField _nameInput;

	private string _numberOk = "BY9527";

	public override void BeforeShowSize()
	{
		_sureButton.onClick.AddListener(delegate
		{
			string value = _numberInput.text.Trim().ToUpper();
			string text = _nameInput.text.Trim().ToUpper().Replace(" ", "");
			string value2 = "Aogesi Will".Trim().ToUpper().Replace(" ", "");
			if (text.Equals(value2) && _numberOk.Equals(value))
			{
				if (gameManager.Is_Dlc6() && gameManager.player.playerdata.iamPoliceWrongCount == 0)
				{
					gameManager.UnlockAchievements("policeman");
				}
				((GameObject)Object.Instantiate(Resources.Load("_DLC/Prefabs/CatchLoading"), gameManager.homeScene.middle)).GetComponent<CatchLoading>().Begin();
				HideDialog();
			}
			else
			{
				gameManager.player.playerdata.iamPoliceWrongCount++;
				ShowTip();
			}
		});
	}

	private void ShowTip()
	{
		CanvasGroup component = _tip.GetComponent<CanvasGroup>();
		DOTween.Kill("NetDialogDlcTipSeq");
		Sequence sequence = DOTween.Sequence();
		sequence.SetId("NetDialogDlcTipSeq");
		sequence.Append(component.DOFade(1f, 0.3f));
		sequence.AppendInterval(2f);
		sequence.Append(component.DOFade(0f, 0.3f));
		sequence.Play();
	}

	public override void AfterShowSize()
	{
	}
}
