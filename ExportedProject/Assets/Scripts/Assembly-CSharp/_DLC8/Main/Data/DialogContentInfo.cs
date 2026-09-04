using AlubaExcelData.DataClass;

namespace _DLC8.Main.Data
{
	public class DialogContentInfo
	{
		public int id;

		public string name;

		public string content;

		public string sound;

		public string option;

		public static DialogContentInfo CreateInfo(DialogContent content)
		{
			return new DialogContentInfo
			{
				id = content.id,
				name = content.name,
				content = content.content,
				sound = content.sound
			};
		}
	}
}
