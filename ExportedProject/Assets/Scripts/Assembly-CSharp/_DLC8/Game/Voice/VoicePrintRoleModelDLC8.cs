using System.Collections.Generic;

namespace _DLC8.Game.Voice
{
	public class VoicePrintRoleModelDLC8
	{
		public string name;

		public List<string> list;

		public List<VoicePrintModelDLC8> modelList;

		public void InitVoicePrint()
		{
			if (this.list == null)
			{
				this.list = new List<string>();
			}
			List<VoicePrintModelDLC8> list = new List<VoicePrintModelDLC8>();
			for (int i = 0; i < this.list.Count; i++)
			{
				VoicePrintModelDLC8 voicePrintModelDLC = new VoicePrintModelDLC8();
				voicePrintModelDLC.sourceName = name;
				voicePrintModelDLC.pathName = this.list[i];
				list.Add(voicePrintModelDLC);
			}
			modelList = list;
		}
	}
}
