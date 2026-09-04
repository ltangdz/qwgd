using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA39")]
	public class DATA39 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private int _type;

		private string _content = "";

		private string _itemid = "";

		private string _reply = "";

		private string _summary = "";

		private string _videoid = "";

		private string _emailid = "";

		private string _look = "";

		private string _needbk = "";

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

		[ProtoMember(3, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "content", DataFormat = DataFormat.Default)]
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

		[ProtoMember(5, IsRequired = false, Name = "itemid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string itemid
		{
			get
			{
				return _itemid;
			}
			set
			{
				_itemid = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "reply", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string reply
		{
			get
			{
				return _reply;
			}
			set
			{
				_reply = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "summary", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string summary
		{
			get
			{
				return _summary;
			}
			set
			{
				_summary = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "videoid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string videoid
		{
			get
			{
				return _videoid;
			}
			set
			{
				_videoid = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "emailid", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string emailid
		{
			get
			{
				return _emailid;
			}
			set
			{
				_emailid = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "look", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string look
		{
			get
			{
				return _look;
			}
			set
			{
				_look = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "needbk", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string needbk
		{
			get
			{
				return _needbk;
			}
			set
			{
				_needbk = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
