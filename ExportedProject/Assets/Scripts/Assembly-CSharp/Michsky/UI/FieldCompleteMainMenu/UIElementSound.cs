using UnityEngine;
using UnityEngine.EventSystems;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class UIElementSound : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler
	{
		[Header("RESOURCES")]
		public AudioClip hoverSound;

		public AudioClip clickSound;

		public AudioClip notificationSound;

		[Header("SETTINGS")]
		public bool enableHoverSound = true;

		public bool enableClickSound = true;

		public bool isNotification;

		private AudioSource HoverSource => GetComponent<AudioSource>();

		private AudioSource ClickSource => GetComponent<AudioSource>();

		private AudioSource NotificationSource => GetComponent<AudioSource>();

		private void Start()
		{
			base.gameObject.AddComponent<AudioSource>();
			HoverSource.clip = hoverSound;
			ClickSource.clip = clickSound;
			HoverSource.playOnAwake = false;
			ClickSource.playOnAwake = false;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (enableHoverSound)
			{
				HoverSource.PlayOneShot(hoverSound);
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (enableClickSound)
			{
				ClickSource.PlayOneShot(clickSound);
			}
		}

		public void Notification()
		{
			if (isNotification)
			{
				NotificationSource.PlayOneShot(notificationSound);
			}
		}
	}
}
