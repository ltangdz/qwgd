using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverColorChange : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	[Header("文字改变颜色")]
	public bool hover;

	public Text textObj;

	public Text textObj2;

	public Color enterColor;

	private Color exitColor;

	[Header("对象显示")]
	public bool objShow;

	public CanvasGroup enterObj;

	public float alpha;

	[Header("更换图片")]
	public bool changImg;

	public Image enterImg;

	public Sprite enterUrl;

	private Sprite exitUrl;

	private void Start()
	{
		if (hover)
		{
			textObj = ((textObj == null) ? base.transform.GetComponent<Text>() : textObj);
			exitColor = textObj.color;
		}
		if (changImg)
		{
			enterImg = ((enterImg == null) ? base.transform.GetComponent<Image>() : enterImg);
			exitUrl = enterImg.sprite;
		}
	}

	private void OnEnter()
	{
		if (textObj != null && hover)
		{
			textObj.color = exitColor;
			textObj.DOKill();
			textObj.DOColor(enterColor, 0.1f);
		}
		if (textObj2 != null && hover)
		{
			textObj2.color = exitColor;
			textObj2.DOKill();
			textObj2.DOColor(enterColor, 0.1f);
		}
		if (objShow && enterObj != null && enterObj.alpha != 1f)
		{
			enterObj.alpha = 0f;
			enterObj.DOKill();
			enterObj.DOFade(alpha, 0.1f);
		}
		if (changImg && enterImg != null)
		{
			enterImg.sprite = enterUrl;
		}
	}

	public void KillEnterObj()
	{
		enterObj.DOKill();
	}

	private void OnExit()
	{
		if (textObj != null && hover)
		{
			textObj.color = enterColor;
			textObj.DOKill();
			textObj.DOColor(exitColor, 0.1f);
		}
		if (textObj2 != null && hover)
		{
			textObj2.color = enterColor;
			textObj2.DOKill();
			textObj2.DOColor(exitColor, 0.1f);
		}
		if (objShow && enterObj != null && enterObj.alpha != 1f)
		{
			enterObj.alpha = alpha;
			enterObj.DOKill();
			enterObj.DOFade(0f, 0.1f);
		}
		if (changImg && enterImg != null)
		{
			enterImg.sprite = exitUrl;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		OnEnter();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		OnExit();
	}
}
