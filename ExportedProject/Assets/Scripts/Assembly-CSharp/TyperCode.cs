using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.UI;

public class TyperCode : MonoBehaviour
{
	[SerializeField]
	private Text txt_name;

	[SerializeField]
	private Text txt_code;

	[SerializeField]
	private Image img_blank;

	[SerializeField]
	private Text txt_code0;

	public float Init(string namekey, string codekey)
	{
		if (!namekey.Equals(""))
		{
			txt_name.GetComponent<I18NText>().updateTranslation5(I18N.instance.getValue(namekey) + "> ");
		}
		else
		{
			txt_name.GetComponent<I18NText>().updateTranslation5(" > ");
		}
		Sequence sq = DOTween.Sequence();
		sq.Append(img_blank.DOFade(0f, 0.01f));
		sq.Append(img_blank.DOFade(1f, 0.01f));
		sq.Play().SetLoops(-1);
		Sequence s = DOTween.Sequence();
		s.AppendInterval(0.5f);
		s.Append(txt_code.DOText(I18N.instance.getValue(codekey), (float)I18N.instance.getValue(codekey).Length * 0.1f).SetEase(Ease.Linear).OnComplete(delegate
		{
			sq.Kill();
			img_blank.gameObject.SetActive(value: false);
		}));
		return (float)I18N.instance.getValue(codekey).Length * 0.1f + 0.5f;
	}

	public float Init2(string prekey, string[] lastkey)
	{
		txt_code0.DOText(I18N.instance.getValue(prekey), (float)I18N.instance.getValue(prekey).Length * 0.04f).SetEase(Ease.Linear).OnComplete(delegate
		{
			for (int i = 0; i < lastkey.Length; i++)
			{
				int percent = 0;
				DOTween.To(() => percent, delegate(int x)
				{
					percent = x;
				}, lastkey.Length - 1, (float)lastkey.Length * 0.1f).SetEase(Ease.Linear).OnUpdate(delegate
				{
					if (percent < lastkey.Length)
					{
						txt_code0.GetComponent<Text>().text = I18N.instance.getValue(prekey) + lastkey[percent];
					}
				});
			}
		});
		return (float)I18N.instance.getValue(prekey).Length * 0.04f + (float)lastkey.Length * 0.1f;
	}
}
