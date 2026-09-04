using UnityEngine;
using UnityEngine.UI;

namespace _DLC8
{
	public class CustomCanvasScaler : MonoBehaviour
	{
		private CanvasScaler _canvasScalerTemp;

		public CanvasScaler CanvasScalerTemp
		{
			get
			{
				if (_canvasScalerTemp == null)
				{
					_canvasScalerTemp = base.transform.GetComponent<CanvasScaler>();
				}
				return _canvasScalerTemp;
			}
		}

		private void Start()
		{
			Init();
		}

		private void Init()
		{
			float x = CanvasScalerTemp.referenceResolution.x;
			float y = CanvasScalerTemp.referenceResolution.y;
			float num = 0f;
			float num2 = 0f;
			float num3 = Screen.width;
			num = Screen.height;
			float num4 = x / y;
			float num5 = num3 / num;
			if (num5 < num4)
			{
				num2 = num4 / num5;
			}
			if (num2 == 0f)
			{
				CanvasScalerTemp.matchWidthOrHeight = 1f;
			}
			else
			{
				CanvasScalerTemp.matchWidthOrHeight = 0f;
			}
		}
	}
}
