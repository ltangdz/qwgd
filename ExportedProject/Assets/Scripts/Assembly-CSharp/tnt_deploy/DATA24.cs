using System;
using System.ComponentModel;
using ProtoBuf;

namespace tnt_deploy
{
	[Serializable]
	[ProtoContract(Name = "DATA24")]
	public class DATA24 : IExtensible
	{
		private uint _ID;

		private int _eventid;

		private string _frdreply = "";

		private string _title = "";

		private string _content = "";

		private int _replyType;

		private string _replyBtn = "";

		private string _infoType = "";

		private string _collectID = "";

		private string _highlight = "";

		private int _EndType;

		private int _TrustType;

		private string _judeg = "";

		private string _personnikename = "";

		private string _personavatar = "";

		private string _yuzhijianname = "";

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

		[ProtoMember(3, IsRequired = false, Name = "frdreply", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string frdreply
		{
			get
			{
				return _frdreply;
			}
			set
			{
				_frdreply = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "title", DataFormat = DataFormat.Default)]
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

		[ProtoMember(5, IsRequired = false, Name = "content", DataFormat = DataFormat.Default)]
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

		[ProtoMember(6, IsRequired = false, Name = "replyType", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int replyType
		{
			get
			{
				return _replyType;
			}
			set
			{
				_replyType = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "replyBtn", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string replyBtn
		{
			get
			{
				return _replyBtn;
			}
			set
			{
				_replyBtn = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "infoType", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string infoType
		{
			get
			{
				return _infoType;
			}
			set
			{
				_infoType = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "collectID", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string collectID
		{
			get
			{
				return _collectID;
			}
			set
			{
				_collectID = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "highlight", DataFormat = DataFormat.Default)]
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

		[ProtoMember(11, IsRequired = false, Name = "EndType", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int EndType
		{
			get
			{
				return _EndType;
			}
			set
			{
				_EndType = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "TrustType", DataFormat = DataFormat.TwosComplement)]
		[DefaultValue(0)]
		public int TrustType
		{
			get
			{
				return _TrustType;
			}
			set
			{
				_TrustType = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "judeg", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string judeg
		{
			get
			{
				return _judeg;
			}
			set
			{
				_judeg = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "personnikename", DataFormat = DataFormat.Default)]
		[DefaultValue("")]
		public string personnikename
		{
			get
			{
				return _personnikename;
			}
			set
			{
				_personnikename = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "personavatar", DataFormat = DataFormat.Default)]
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

		[ProtoMember(16, IsRequired = false, Name = "yuzhijianname", DataFormat = DataFormat.Default)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref extensionObject, createIfMissing);
		}
	}
}
