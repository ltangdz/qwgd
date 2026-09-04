using Honeti;
using UnityEngine;
using UnityEngine.UI;
using _DLC8.Main.Data;

namespace _DLC8.Main
{
	public class TitanTalkTipItem : MonoBehaviour
	{
		public Text text;

		private TalkContentInfo _info;

		public void Init(TalkContentInfo info)
		{
			_info = info;
			text.text = I18N.instance.getValue(info.content);
		}
	}
}
