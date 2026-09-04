using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA22")]
	public class DATA22 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _person = "";

		private string _personavatar = "";

		private string _title = "";

		private int _type;

		private string _content = "";

		private int _chatType;

		private string _highlight = "";

		private string _chatTask = "";

		private string _yuzhijianname = "";

		private string _money = "";

		private string _is_get = "";

		private string _is_blacklist = "";

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

		[ProtoMember(3, IsRequired = false, Name = "person", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string person
		{
			get
			{
				return _person;
			}
			set
			{
				_person = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "personavatar", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string personavatar
		{
			get
			{
				return _personavatar;
			}
			set
			{
				_personavatar = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "title", DataFormat = DataFormat.Default)]
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

		[ProtoMember(6, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(7, IsRequired = false, Name = "content", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string content
		{
			get
			{
				return _content;
			}
			set
			{
				_content = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "chatType", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int chatType
		{
			get
			{
				return _chatType;
			}
			set
			{
				_chatType = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "highlight", DataFormat = DataFormat.Default)]
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

		[ProtoMember(10, IsRequired = false, Name = "chatTask", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string chatTask
		{
			get
			{
				return _chatTask;
			}
			set
			{
				_chatTask = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "yuzhijianname", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string yuzhijianname
		{
			get
			{
				return _yuzhijianname;
			}
			set
			{
				_yuzhijianname = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "money", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string money
		{
			get
			{
				return _money;
			}
			set
			{
				_money = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "is_get", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string is_get
		{
			get
			{
				return _is_get;
			}
			set
			{
				_is_get = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "is_blacklist", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string is_blacklist
		{
			get
			{
				return _is_blacklist;
			}
			set
			{
				_is_blacklist = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
