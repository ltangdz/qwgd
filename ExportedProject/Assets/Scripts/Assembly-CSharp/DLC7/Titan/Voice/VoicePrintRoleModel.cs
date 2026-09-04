using System.Collections.Generic;

namespace DLC7.Titan.Voice
{
	public class VoicePrintRoleModel
	{
		public string name;

		public List<string> list;

		public List<VoicePrintModel> modelList;

		public void InitVoicePrint()
		{
			if (this.list == null)
			{
				this.list = new List<string>();
			}
			List<VoicePrintModel> list = new List<VoicePrintModel>();
			for (int i = 0; i < this.list.Count; i++)
			{
				VoicePrintModel voicePrintModel = new VoicePrintModel();
				voicePrintModel.sourceName = name;
				voicePrintModel.pathName = this.list[i];
				list.Add(voicePrintModel);
			}
			modelList = list;
		}
	}
}
