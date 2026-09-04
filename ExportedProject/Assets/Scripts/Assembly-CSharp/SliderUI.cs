using System.Collections;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
	public Sprite graysprite;

	public Sprite lightsprite;

	public Text txt_acc;

	public Image[] accs;

	public float currentacc;

	private bool isgoing;

	public float dengfen;

	public SurSearchingDialog surSearchingDialog;

	private void Start()
	{
		dengfen = 100f / (float)accs.Length;
	}

	public void FreshAcc(float acc)
	{
		if (!isgoing)
		{
			StartCoroutine(StartAnimation2());
		}
	}

	private IEnumerator StartAnimation2()
	{
		for (int i = 0; i < accs.Length; i++)
		{
			accs[i].sprite = lightsprite;
			txt_acc.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^surveillance07") + (((float)i * dengfen > 100f) ? 100f : ((float)i * dengfen)).ToString("f2") + "%");
			yield return new WaitForSeconds(0.02f);
		}
		txt_acc.GetComponent<I18NText>().updateTranslation2(I18N.instance.getValue("^surveillance07") + "100%");
		surSearchingDialog.SearchOver();
	}

	public void Restart()
	{
	}
}
