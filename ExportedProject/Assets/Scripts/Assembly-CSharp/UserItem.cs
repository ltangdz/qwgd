using DG.Tweening;
using Honeti;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public Image img_avatar;

	public Text txt_username;

	public Sprite[] sprites;

	public Color graycolor;

	public LoginPanel loginPanel;

	public string un;

	public Transform tweenTarget;

	public bool showHover = true;

	public Vector3 hover = new Vector3(1.1f, 1.1f, 1.1f);

	public float hoverDuration = 0.2f;

	private Vector3 _mScale;

	private bool _mStarted;

	private bool isselected;

	private void Start()
	{
		if (!_mStarted)
		{
			_mStarted = true;
			if (tweenTarget == null)
			{
				tweenTarget = base.transform;
			}
			_mScale = new Vector3(1f, 1f, 1f);
		}
	}

	public void ClickLogin()
	{
		SetSelected(isselected: true);
		loginPanel.SelectUser(txt_username.text);
	}

	public void Init(string username, LoginPanel loginPanel)
	{
		un = username;
		this.loginPanel = loginPanel;
		txt_username.GetComponent<I18NText>().updateTranslation2(username);
	}

	public void SetSelected(bool isselected)
	{
		this.isselected = isselected;
		if (isselected)
		{
			img_avatar.sprite = sprites[1];
			txt_username.color = Color.white;
		}
		else
		{
			img_avatar.sprite = sprites[0];
			txt_username.color = graycolor;
		}
		ResetButton();
	}

	public void ResetMScale()
	{
		_mScale = new Vector3(1f, 1f, 1f);
	}

	public void ResetButton()
	{
		OnHover(isOver: false);
	}

	private void OnHover(bool isOver)
	{
		if (!isselected && base.enabled && showHover)
		{
			if (!_mStarted)
			{
				Start();
			}
			Vector3 b = new Vector3((tweenTarget.localScale.x > 0f) ? hover.x : (0f - hover.x), hover.y, hover.z);
			Vector3 vector = new Vector3((tweenTarget.localScale.x > 0f) ? _mScale.x : (0f - _mScale.x), _mScale.y, _mScale.z);
			Vector3 endValue = (isOver ? Vector3.Scale(_mScale, b) : vector);
			tweenTarget.DOScale(endValue, hoverDuration).SetEase(Ease.InOutQuad);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		OnHover(isOver: true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		OnHover(isOver: false);
	}
}
