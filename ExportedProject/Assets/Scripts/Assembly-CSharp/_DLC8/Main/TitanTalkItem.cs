using Aluba;
using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Main.Data;

namespace _DLC8.Main
{
	public class TitanTalkItem : MonoBehaviour
	{
		public Image avatarImage;

		public Text nameText;

		public Text contentText;

		public GameObject vpImage;

		private TalkContentInfo _info;

		public void Init(TalkContentInfo info, bool isBig)
		{
			_info = info;
			string value = I18N.instance.getValue(_info.name);
			vpImage.SetActive(value.Equals("Daniel"));
			avatarImage.sprite = Resources.Load<Sprite>($"touxiang/{_info.avatar}");
			nameText.text = value;
			string text = "";
			text = ((!I18N.instance.gameLang.Equals(LanguageCode.CN) && !I18N.instance.gameLang.Equals(LanguageCode.TC)) ? I18N.instance.getValue(_info.content).Replace("{*Player*}", SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.NickName) : I18N.instance.GetValueNoSpacing(_info.content).Replace("{*Player*}", SingletonAutoMono<DLC8DataController>.GetInstance().ArchiveData.NickName));
			float num = AlubaTools.CalculateLengthOfText(contentText, text);
			if (isBig)
			{
				if (num < 420f)
				{
					contentText.GetComponent<LayoutElement>().preferredWidth = num;
				}
				else
				{
					contentText.GetComponent<LayoutElement>().preferredWidth = 420f;
				}
			}
			else if (num < 270f)
			{
				contentText.GetComponent<LayoutElement>().preferredWidth = num;
			}
			contentText.text = text;
		}
	}
}
