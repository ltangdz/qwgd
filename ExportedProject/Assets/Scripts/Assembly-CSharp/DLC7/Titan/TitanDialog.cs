using Honeti;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DLC7.Titan
{
	public class TitanDialog : MonoBehaviour
	{
		public Text titleText;

		public Button sureButton;

		private bool _hiddenParent;

		private UnityAction _sureCallback;

		private void Start()
		{
			sureButton.onClick.AddListener(Sure);
		}

		public void InitData(string title, UnityAction sureCallback)
		{
			titleText.text = (title.StartsWith("^") ? I18N.instance.getValue(title) : title);
			_sureCallback = sureCallback;
		}

		public void Sure()
		{
			if (_sureCallback != null)
			{
				_sureCallback();
			}
			Object.Destroy(base.gameObject);
		}
	}
}
