using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA15")]
	public class DATA15 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _sender = "";

		private string _geter = "";

		private string _sendTime = "";

		private string _title = "";

		private string _info = "";

		private string _highlight = "";

		private string _clue = "";

		private string _Jump = "";

		private int _type;

		private string _missionID = "";

		private int _open;

		private string _downloadid = "";

		private string _reasoningID = "";

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

		[ProtoMember(3, IsRequired = false, Name = "sender", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string sender
		{
			get
			{
				return _sender;
			}
			set
			{
				_sender = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "geter", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string geter
		{
			get
			{
				return _geter;
			}
			set
			{
				_geter = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "sendTime", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string sendTime
		{
			get
			{
				return _sendTime;
			}
			set
			{
				_sendTime = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "title", DataFormat = DataFormat.Default)]
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

		[ProtoMember(7, IsRequired = false, Name = "info", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string info
		{
			get
			{
				return _info;
			}
			set
			{
				_info = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "highlight", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string highlight
		{
			get
			{
				return _highlight;
			}
			set
			{
				_highlight = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "clue", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string clue
		{
			get
			{
				return _clue;
			}
			set
			{
				_clue = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "Jump", DataFormat = DataFormat.Default)]
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

		[ProtoMember(11, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(12, IsRequired = false, Name = "missionID", DataFormat = DataFormat.Default)]
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

		[ProtoMember(13, IsRequired = false, Name = "open", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int open
		{
			get
			{
				return _open;
			}
			set
			{
				_open = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "downloadid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string downloadid
		{
			get
			{
				return _downloadid;
			}
			set
			{
				_downloadid = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "reasoningID", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string reasoningID
		{
			get
			{
				return _reasoningID;
			}
			set
			{
				_reasoningID = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
