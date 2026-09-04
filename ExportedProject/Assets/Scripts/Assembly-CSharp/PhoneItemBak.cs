using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class PhoneItemBak : MonoBehaviour
{
	public Text talkInfo;

	private PhoneInfo parObj;

	private GameManager gameManager;

	public void Init(string label, PhoneInfo par, GameManager gm)
	{
		Debug.Log(label);
		parObj = par;
		gameManager = gm;
		talkInfo.GetComponent<I18NText>().updateTranslation2(label);
		talkInfo.GetComponent<NonBreakingSpaceTextComponent>().Refresh();
	}
}
