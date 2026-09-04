using UnityEngine;
using UnityEngine.EventSystems;

public class TuiliAniItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	[SerializeField]
	private string aniname;

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private bool isloop;

	private void Start()
	{
		if (isloop)
		{
			animator.Play(aniname);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!isloop)
		{
			animator.Play(aniname);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!isloop)
		{
			animator.Play("Empty");
		}
	}
}
