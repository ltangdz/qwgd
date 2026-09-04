using UnityEngine;
using UnityEngine.UI;

namespace DLC7
{
	public class Md5Browser : MonoBehaviour
	{
		public InputField inputField;

		public Button button;

		public Text text;

		public GameObject itemObj;

		private string _answer = "V5891inV";

		private void Start()
		{
			button.onClick.AddListener(Confirm);
		}

		private void Confirm()
		{
			string text = inputField.text.Trim();
			if (!string.IsNullOrEmpty(text))
			{
				if (text == _answer)
				{
					itemObj.SetActive(value: true);
					this.text.gameObject.SetActive(value: false);
					return;
				}
				itemObj.SetActive(value: false);
				this.text.gameObject.SetActive(value: true);
				this.text.text = "";
				string text2 = AlubaTools.UserMd5(text).ToUpper();
				this.text.text = text2;
			}
		}
	}
}
