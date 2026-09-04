using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA2")]
	public class DATA2 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _title = "";

		private string _point = "";

		private string _URL = "";

		private string _word = "";

		private string _method = "";

		private string _Jump = "";

		private int _type;

		private string _missionID = "";

		private string _newsid = "";

		private string _tab = "";

		private string _pic = "";

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "ID", DataFormat = DataFormat.TwosComplement)]
		public uint ID
		{
			get
			{
				return _ID;
			}
			set
			{
				_ID = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "eventid", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int eventid
		{
			get
			{
				return _eventid;
			}
			set
			{
				_eventid = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "title", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "point", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string point
		{
			get
			{
				return _point;
			}
			set
			{
				_point = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "URL", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string URL
		{
			get
			{
				return _URL;
			}
			set
			{
				_URL = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "word", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string word
		{
			get
			{
				return _word;
			}
			set
			{
				_word = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "method", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string method
		{
			get
			{
				return _method;
			}
			set
			{
				_method = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "Jump", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string Jump
		{
			get
			{
				return _Jump;
			}
			set
			{
				_Jump = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "missionID", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string missionID
		{
			get
			{
				return _missionID;
			}
			set
			{
				_missionID = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "newsid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string newsid
		{
			get
			{
				return _newsid;
			}
			set
			{
				_newsid = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "tab", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string tab
		{
			get
			{
				return _tab;
			}
			set
			{
				_tab = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "pic", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string pic
		{
			get
			{
				return _pic;
			}
			set
			{
				_pic = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
