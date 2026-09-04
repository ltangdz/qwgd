using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShenzhuizongItem : MonoBehaviour
{
	[SerializeField]
	private Image img_avatar;

	public int isscale;

	public ShenzhuizongPanel shenzhuizongPanel;

	public bool isright;

	private Sequence sq4;

	public void Init(bool isright, string pic)
	{
		this.isright = isright;
		if (!isright)
		{
			int num = Random.Range(10, 195);
			img_avatar.sprite = Resources.Load<Sprite>("touxiang/" + num);
		}
		else
		{
			img_avatar.sprite = Resources.Load<Sprite>("Image/" + pic + "_zz1");
		}
	}

	public void Move()
	{
		sq4 = DOTween.Sequence();
		Vector3 localPosition = base.transform.localPosition;
		sq4.Append(base.transform.DOLocalMoveX(-914f, (localPosition.x + 914f) / 300f).SetEase(Ease.Linear));
		sq4.Append(base.transform.DOLocalMoveX(1136f, 0f).SetEase(Ease.Linear).OnComplete(delegate
		{
			isscale = 0;
		}));
		sq4.Append(base.transform.DOLocalMoveX(localPosition.x, (1296f - localPosition.x) / 300f).SetEase(Ease.Linear)).SetLoops(-1);
		sq4.Play();
	}

	public void MovePause()
	{
		sq4.Pause();
	}

	public void MoveResume()
	{
		sq4.Play();
	}

	private void Update()
	{
		if (base.transform.localPosition.x < 50f && base.transform.localPosition.x > -50f && isscale == 0)
		{
			shenzhuizongPanel.curretnshenzhuizongItem = this;
			isscale = 1;
			base.transform.DOScale(1f, 0.2f);
		}
		if (base.transform.localPosition.x < -50f && isscale == 1)
		{
			shenzhuizongPanel.curretnshenzhuizongItem = null;
			isscale = 0;
			base.transform.DOScale(0.5f, 0.2f);
		}
	}
}
