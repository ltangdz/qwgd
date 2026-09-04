using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class RadialSlider : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler
	{
		private const string PREFS_UI_SAVE_NAME = "Radial";

		[Header("OBJECTS")]
		[SerializeField]
		private Image sliderImage;

		[SerializeField]
		private Transform indicatorPivot;

		[SerializeField]
		private Text valueText;

		[Header("SETTINGS")]
		[SerializeField]
		private int sliderID;

		[SerializeField]
		private float maxValue = 100f;

		[SerializeField]
		[Range(0f, 8f)]
		private int decimals;

		[SerializeField]
		private bool isPercent;

		[SerializeField]
		private bool rememberValue;

		[SerializeField]
		private UnityEvent onValueChanged;

		private GraphicRaycaster graphicRaycaster;

		private RectTransform hitRectTransform;

		private bool isPointerDown;

		private float currentAngle;

		private float currentAngleOnPointerDown;

		private float valueDisplayPrecision;

		public float SliderAngle
		{
			get
			{
				return currentAngle;
			}
			set
			{
				currentAngle = Mathf.Clamp(value, 0f, 360f);
			}
		}

		public float SliderValue
		{
			get
			{
				return (float)(long)(SliderValueRaw * valueDisplayPrecision) / valueDisplayPrecision;
			}
			set
			{
				SliderValueRaw = value;
			}
		}

		public float SliderValueRaw
		{
			get
			{
				return SliderAngle / 360f * maxValue;
			}
			set
			{
				SliderAngle = value * 360f / maxValue;
			}
		}

		private void Awake()
		{
			graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
			if (graphicRaycaster == null)
			{
				Debug.LogWarning("Could not find GraphicRaycaster component in parent of this GameObject: " + base.name);
				Object.Destroy(base.gameObject);
			}
		}

		private void Start()
		{
			valueDisplayPrecision = Mathf.Pow(10f, decimals);
			LoadState();
			UpdateUI();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			hitRectTransform = eventData.pointerCurrentRaycast.gameObject.GetComponent<RectTransform>();
			isPointerDown = true;
			currentAngleOnPointerDown = SliderAngle;
			HandleSliderMouseInput(eventData, allowValueWrap: true);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (HasValueChanged())
			{
				SaveState();
			}
			hitRectTransform = null;
			isPointerDown = false;
		}

		public void OnDrag(PointerEventData eventData)
		{
			HandleSliderMouseInput(eventData, allowValueWrap: false);
		}

		public void LoadState()
		{
			if (rememberValue)
			{
				currentAngle = PlayerPrefs.GetFloat(sliderID + "Radial");
			}
		}

		public void SaveState()
		{
			if (rememberValue)
			{
				PlayerPrefs.SetFloat(sliderID + "Radial", currentAngle);
			}
		}

		public void UpdateUI()
		{
			float fillAmount = SliderAngle / 360f;
			indicatorPivot.transform.localEulerAngles = new Vector3(180f, 0f, SliderAngle);
			sliderImage.fillAmount = fillAmount;
			valueText.text = string.Format("{0}{1}", SliderValue, isPercent ? "%" : "");
		}

		private bool HasValueChanged()
		{
			return SliderAngle != currentAngleOnPointerDown;
		}

		private void HandleSliderMouseInput(PointerEventData eventData, bool allowValueWrap)
		{
			if (!isPointerDown)
			{
				return;
			}
			RectTransformUtility.ScreenPointToLocalPointInRectangle(hitRectTransform, eventData.position, eventData.pressEventCamera, out var localPoint);
			float num = Mathf.Atan2(0f - localPoint.y, localPoint.x) * 57.29578f + 180f;
			if (!allowValueWrap)
			{
				float sliderAngle = SliderAngle;
				if (Mathf.Abs(num - sliderAngle) >= 180f)
				{
					num = ((sliderAngle < num) ? 0f : 360f);
				}
			}
			SliderAngle = num;
			UpdateUI();
			if (HasValueChanged())
			{
				onValueChanged.Invoke();
			}
		}
	}
}
